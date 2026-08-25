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
- `SliderInterop.cs` + `wwwroot\Sliders.js` — JS interop (pointer/drag behavior, sizing).
- Value types: `Dimensions.cs`, `Position.cs`, `Size.cs`, `SizeUnit.cs`, `PanelPosition.cs`.

## Non-obvious / gotchas
- `global.json` pins SDK 9.0.102 with `rollForward: latestFeature` — builds may use a newer 9.0.x feature band.
- `AI_RULES.md` documents the task-complete sound protocol: `[System.Media.SystemSounds]::Tada` is broken here (null property); use synchronous `PlaySound` with `0x20000` on `ahem.wav`.
- `msnippets.json` drives README code snippets; `.cr` file present at repo root (editor rules).
