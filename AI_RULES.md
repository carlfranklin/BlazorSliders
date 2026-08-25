# AI Rules

Permanent rules, architecture, conventions, and constraints for the BlazorSliders project.
(Initial version created 2026-08-25; refine as conventions are established.)

## Identity & session protocol
- My name is Laforge (Carl's name for me). Protocol details live in `startup.md`.
- Memory files: `AI_RULES.md` (this file), `AI_MEMORY.md` (durable knowledge), `AI_CURRENT.md` (active task checkpoint). Read all three at session start; update continuously while working, not just at the end.
- Current code is the source of truth over memory files; correct memory if they conflict.

## Build & completion rules
- Get a clean build before claiming a task complete.
- Do NOT commit — Carl commits himself. Provide a one-paragraph commit message (no bullets, no extra spaces, no CRLF) summarizing all session work.
- After each completed task, play `ahem.wav` (repo root) via synchronous Win32 `PlaySound` with flag `0x20000` (SND_FILENAME). See startup.md for the exact working PowerShell snippet; `[System.Media.SystemSounds]::Tada` does NOT work on this machine, and other flag values play nothing.

## Project conventions (established so far)
- All projects target net10.0 (upgraded 2026-08-25); repo pinned via `global.json` to SDK 10.0.400 with `rollForward: latestFeature`.
- Solution: `BlazorSliders.sln` — main library `BlazorSliders/`, tests in `UnitTests/`, demo apps (`BlazorSlidersTest`, `BlazorSlidersWasmTestApp`, `BlazorSlidersServerTestApp`, `BlazorSliderTestWasm`).
- Component pattern: code-behind `.razor.cs` files paired with `.razor` markup in `BlazorSliders/`.
- JS interop lives in `BlazorSliders/Sliders.js` (wwwroot) + `SliderInterop.cs`.
- `NEW_SLIDER_API.md` in repo root describes work on a new slider API — read before changing the public slider surface.
