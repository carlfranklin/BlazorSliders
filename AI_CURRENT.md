# AI Current

Checkpoint of the active task. Keep focused on current work only; move durable knowledge to AI_MEMORY.md / AI_RULES.md when a task completes.

## Current task
Fixed the failing `on-push-do-docs` GitHub Actions workflow (run 32912252052, triggered by push of b7f91ec). Root cause: mdsnippets hard-fails (exit 1) when a `<!-- snippet: -->` reference in README.md points at a missing file, and two references were stale: `BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor` (never committed — the sticky-headers demo lives in `Pages/Horizontals.razor` in both apps) and `BlazorSlidersWasmTestApp/Shared/NavMenu.razor` (actual file is `Pages/NavMenu.razor`). Fix (working tree, 3 files, uncommitted): repointed both snippet references, de-snippeted the "Simple Horizontal Split" section (kept as a hand-written minimal example, since the real Horizontals.razor is now the 60-line sticky-headers demo and would have been inlined twice), then regenerated with mdsnippets 28.4.2 locally — all demo snippets now match the current pages (incl. the page descriptions from b7f91ec), TOC corrected (stale "Install with NuGet" entry removed, missing "Pre-rendering" entry added), and NEW_SLIDER_API.md/startup.md got EOF newlines from the tool. Verified: `mdsnippets .` exits 0 and a second run produces zero changes (idempotent), so after Carl's commit the workflow will hit "nothing to commit" and pass.

## Next steps
Deliver the one-paragraph commit message for the docs workflow fix; Carl commits. Stale Copilot draft PRs #26/#28/#30 remain open pending Carl's call.
