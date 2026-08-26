# AI Memory

Durable knowledge from previous sessions: design decisions, non-obvious behavior, solutions, APIs, dependencies, config, lessons learned.
(Initial version created 2026-08-25.)

## Project overview
- BlazorSliders is a NuGet component library (author Carl Franklin, App vNext) that creates multiple panels separated by sliding splitters.
- Main components: `AbsolutePanel` (container, window-resize events), `Window`, `VerticalSliderPanel`, `HorizontalSliderPanel`.
- Upgraded to .NET 10 on 2026-08-25: all 3 projects target net10.0; library Version is now 10.0. Solution contains exactly 3 projects (BlazorSliders, BlazorSlidersServerTestApp, BlazorSlidersWasmTestApp); the UnitTests / BlazorSliderTestWasm / BlazorSlidersTest folders are leftovers with no csproj.

## Known baseline warnings (pre-existing as of net9.0, unchanged on net10.0)
- 6x BL0007 "component parameter should be auto property" in the library (SliderPanelBase.Id, HorizontalSliderPanel.TopPanelHeight, VerticalSliderPanel.LeftPanelStartingWidth + SliderPosition, AbsolutePanel.Top + Left).
- 2x per test app: CS8618 Horizontals.razor(64) `SliderPanel` property, CS8625 WindowResize.razor(55) null literal. Total 10 warnings, 0 errors on a clean build.

## Key files (BlazorSliders/)
- `SliderPanelBase.cs` — shared base logic for the slider panels.
- `VerticalSliderPanel.razor(.cs)` / `HorizontalSliderPanel.razor(.cs)` — the two panel components.
- `AbsolutePanel.razor` — container; has `ChildPanel` property and calls `ChildPanel.Resize()` on resize.
- `SliderInterop.cs` + `wwwroot\Sliders.js` — JS interop (pointer/drag behavior, sizing).
- Value types: `Dimensions.cs`, `Position.cs`, `Size.cs`, `SizeUnit.cs`, `PanelPosition.cs`.

## Panel parent/child wiring (non-obvious)
- `SliderPanelBase.Parent` is a `[CascadingParameter(Name="directParent")]`; `directWidth`/`directHeight` cascades feed `DirectWidth`/`DirectHeight`.
- Each panel self-registers with its parent in `OnInitialized()` (e.g. `((VerticalSliderPanel)Parent).LeftPanel = this`), and `Resize()` recursively walks children (`LeftPanel.Resize()` etc.). Stale child refs on a disposed panel caused the "panels alternately disappear when toggled" bug.
- Fix (2026-08-25, from PR #32 which we re-implemented cleanly): both slider panels implement `IDisposable`; `Dispose()` nulls the matching parent refs. Verified pattern: the Blazor renderer calls `(Component as IDisposable)?.Dispose()` (in `ComponentState.cs`) when a component leaves the render tree, and `ComponentBase` does NOT implement IDisposable, so adding it to a component is safe with no `base.Dispose()` to call.
- Demo apps have a "Visibility" test page (`/visibility`) in both server + Wasm that toggles a nested panel to exercise this bug.
- Demo page convention (2026-08-25): every demo page in both test apps carries a one-paragraph description of what it demonstrates, placed just above the `<NavMenu />` in the menu area (or in the top header bar for `/visibility`). New demo pages should follow this.

## The PanelPosition trap and the directCell fix (2026-08-26)
- The `PanelPosition` attribute on nested panels was load-bearing in two places: `OnInitialized()` self-registration into the parent's `Top/Bottom/Left/RightPanel` fields, and `GetSubtractLeft/Top()` mouse-coordinate offset math. Declaring the wrong position (e.g. a panel nested in `TopChildContent` but written `PanelPosition="Bottom"`) silently froze all descendant sliders: the offset walk added the parent's `TopPanelHeight + SliderHeight`, so every mouse Y landed below the range and clamped to `MinimumTopPanelHeight`. The Golden Ratio page had exactly this (L5 in L4's top cell, declared Bottom) → innermost slider (L6) never moved no matter how many outer sliders were dragged.
- Fix: each cell in the .razor files now cascades its actual position (`<CascadingValue Value=@PanelPosition.Top Name="directCell">` etc.); `SliderPanelBase` gained `[CascadingParameter(Name="directCell")] public PanelPosition? DirectCell` and `protected PanelPosition CellPosition => DirectCell ?? PanelPosition`, which `OnInitialized` and `GetSubtract*` now use. A wrong or missing `PanelPosition` attribute can no longer desync coordinates; the parameter is kept for backward compatibility.
- Runtime detail: Sliders.js fires mousemove TWICE per movement (slider-level and window-level handlers both fire while the pointer is over the 5px bar). The second firing lands after the render flush, which is what pushes fresh cascades into descendants — don't chase it as a bug.
- Debug technique that cracked it: temporary `Console.WriteLine` in `Resize()` (entry + clamped result) and Width/Height getters/setters, plus `console.log` in the JS `raiseEvent`; the server log showed the child's `Resize` receiving the correct fresh `DirectHeight` but clamping to its minimum → a coordinate-offset bug, not stale cascade state. All instrumentation was removed afterwards (Sliders.js byte-identical to pre-debug, verified via git diff).
- Verification: headless Playwright drag test (`l6-test.js` in the session files dir; use `executablePath` to the existing chromium-1169 — the playwright 1.56.0-alpha CLI install is silently broken). After the fix, L6 moves on the first drag once an outer slider frees its range.

## PR status (as of 2026-08-25)
- Closed #34 (community net10 PR — superseded by our local net10-only upgrade) and #32 (visibility fix — re-implemented on master, test pages kept).
- Still open: stale Copilot-bot drafts #26 (Disabled property), #28 (SliderContent RenderFragment), #30 (Copilot instructions) from Aug 2025 — candidates for closing if Carl wants.

## Non-obvious / gotchas
- `global.json` pins SDK 10.0.400 with `rollForward: latestFeature`.
- `SliderInterop` is registered **scoped** in the demo apps and holds a `JSModuleReference`. On circuit teardown the scoped disposal ran `module.DisposeAsync()` after JS was gone → `JSDisconnectedException` on ordinary navigation/reload (pre-existing on net9, found 2026-08-25). Fixed by catching `JSDisconnectedException`/`OperationCanceledException` around `DisposeAsync()` in `SliderInterop.DisposeAsync()`. Any future disposal of JS modules from component/scope teardown needs the same guard.
- `AI_RULES.md` documents the task-complete sound protocol: `[System.Media.SystemSounds]::Tada` is broken here (null property); use synchronous `PlaySound` with `0x20000` on `ahem.wav`.
- `msnippets.json` drives README code snippets; `.cr` file present at repo root (editor rules).
- **Docs workflow** (`.github/workflows/on-push-do-docs.yml`): on every push it runs `mdsnippets` (MarkdownSnippets.Tool, InPlaceOverwrite) over the repo, then commits+pushes any README drift as "Docs changes". **Missing snippet source files are a hard failure (exit 1)** — every `<!-- snippet: path -->` in README.md must point at a real file. Regenerate locally with `dotnet tool install --global MarkdownSnippets.Tool` then `mdsnippets .` (v28.4.2 matches the runner); verify idempotency by running it twice (second run must exit 0 with zero file changes). It also normalizes EOF newlines in files containing code fences (it touched NEW_SLIDER_API.md/startup.md once) — keep that in the commit so the workflow doesn't push a stray commit. 2026-08-26: fixed two stale refs (phantom `BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor` → `Pages/Horizontals.razor`, which IS the sticky-headers demo; `BlazorSlidersWasmTestApp/Shared/NavMenu.razor` → `Pages/NavMenu.razor`) and de-snippeted the "Simple Horizontal Split" section to avoid inlining the same 60-line page twice.
- **Browser testing**: no local `node_modules` with playwright; the working pattern (see `l6-test.js` in the session files dir) is `require("C:/Users/carl/AppData/Roaming/npm/node_modules/@playwright/mcp/node_modules/playwright")` (absolute path — `require('playwright')` and NODE_PATH both failed from the session files dir) with `executablePath: C:\Users\carl\AppData\Local\ms-playwright\chromium-1169\chrome-win\chrome.exe`. Slider div IDs are per-instance GUIDs, not "slider" — locate them with `div.defaultSlider`. Drag behavior: the slider panel's edge snaps to the cursor's absolute position inside the AbsolutePanel (`Resize(X)`), so a drag delta ≠ mouse delta by the grab offset — expected, not a bug.
