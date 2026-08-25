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

## PR status (as of 2026-08-25)
- Closed #34 (community net10 PR — superseded by our local net10-only upgrade) and #32 (visibility fix — re-implemented on master, test pages kept).
- Still open: stale Copilot-bot drafts #26 (Disabled property), #28 (SliderContent RenderFragment), #30 (Copilot instructions) from Aug 2025 — candidates for closing if Carl wants.

## Non-obvious / gotchas
- `global.json` pins SDK 10.0.400 with `rollForward: latestFeature`.
- `SliderInterop` is registered **scoped** in the demo apps and holds a `JSModuleReference`. On circuit teardown the scoped disposal ran `module.DisposeAsync()` after JS was gone → `JSDisconnectedException` on ordinary navigation/reload (pre-existing on net9, found 2026-08-25). Fixed by catching `JSDisconnectedException`/`OperationCanceledException` around `DisposeAsync()` in `SliderInterop.DisposeAsync()`. Any future disposal of JS modules from component/scope teardown needs the same guard.
- `AI_RULES.md` documents the task-complete sound protocol: `[System.Media.SystemSounds]::Tada` is broken here (null property); use synchronous `PlaySound` with `0x20000` on `ahem.wav`.
- `msnippets.json` drives README code snippets; `.cr` file present at repo root (editor rules).
