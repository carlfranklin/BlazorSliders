# BlazorSliders - Blazor Slider Panel Component Library

**Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.**

BlazorSliders is a .NET 9.0 Blazor component library that provides vertical and horizontal slider panels with resizable splitters. The library includes both a reusable component package and two test applications (WebAssembly and Blazor Server).

## Working Effectively

### Prerequisites and Setup
- **CRITICAL**: Requires .NET 9.0 SDK (specified in global.json). The system may have .NET 8.0 but you MUST install .NET 9.0.
- Install .NET 9.0 SDK locally:
  ```bash
  curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0 --install-dir ./dotnet
  export PATH="$PWD/dotnet:$PATH"
  ```
- Verify installation: `dotnet --version` should show 9.0.x

### Build Process
- **NEVER CANCEL BUILDS**: Build takes 30-60 seconds. ALWAYS set timeout to 120+ seconds.
- Build the entire solution:
  ```bash
  export PATH="$PWD/dotnet:$PATH"
  dotnet build BlazorSliders.sln
  ```
- Expected build time: ~30 seconds with some warnings (auto-property suggestions are normal)

### Running Test Applications
- **WebAssembly Test App**:
  ```bash
  export PATH="$PWD/dotnet:$PATH"
  dotnet run --project BlazorSlidersWasmTestApp
  ```
  Runs on http://localhost:5117 (check console output for exact port)

- **Blazor Server Test App**:
  ```bash
  export PATH="$PWD/dotnet:$PATH"
  dotnet run --project BlazorSlidersServerTestApp
  ```
  Runs on http://localhost:5201 (check console output for exact port)

### Documentation Tools
- **MarkdownSnippets** (used in CI):
  ```bash
  export PATH="$PWD/dotnet:$PATH"
  dotnet tool install --global MarkdownSnippets.Tool
  mdsnippets .
  ```
- Updates README.md with code snippets from test app files

## Validation Scenarios

**ALWAYS manually validate any code changes by running through these complete scenarios:**

### Essential User Scenarios
1. **Basic Slider Functionality**:
   - Start WASM test app: `dotnet run --project BlazorSlidersWasmTestApp`
   - Navigate to app in browser
   - Test vertical slider by dragging the splitter between left/right panels
   - Click "Horizontal" to test horizontal splitter functionality
   - Verify panels resize properly and content remains accessible

2. **Complex Panel Layouts**:
   - Click "4 Panels" to test nested vertical/horizontal combinations
   - Verify all splitters work independently
   - Test "Crazy" layout for complex nesting scenarios

3. **Sticky Headers Feature**:
   - Click "Horizontal" navigation
   - Scroll content in top and bottom panels
   - Verify headers remain sticky at top of panels during scroll

4. **Test All Navigation Links**:
   - Verify each demo page loads without errors
   - Test Golden Ratio, Custom Classes, Parent Contained, etc.
   - Confirm each layout demonstrates different slider features

### Component Integration Test
- **For component library changes**: Always test in both WASM and Server apps
- **For new features**: Add test pages to both test applications  
- **For API changes**: Update both VerticalSliderPanel and HorizontalSliderPanel consistently

## Repository Structure

### Core Projects
- **BlazorSliders/**: Main component library (.NET 9.0 Razor component library)
  - Key components: `VerticalSliderPanel.razor`, `HorizontalSliderPanel.razor`, `AbsolutePanel.razor`
  - JavaScript interop: `wwwroot/Sliders.js`
  - Styles: `*.razor.css` files for component-specific styling

- **BlazorSlidersWasmTestApp/**: WebAssembly test application
  - All demo pages in `Pages/` directory
  - Shows component usage patterns and examples

- **BlazorSlidersServerTestApp/**: Blazor Server test application  
  - Mirror of WASM app for server-side testing
  - Validates components work in both hosting models

### Documentation and CI
- **README.md**: Main documentation with embedded code snippets
- **NEW_SLIDER_API.md**: API documentation for new slider position features
- **.github/workflows/on-push-do-docs.yml**: Automated documentation updates using MarkdownSnippets
- **mdsnippets.json**: Configuration for documentation generation
- **UnitTests/**: Visual test verification files (PNG images, HTML snapshots)

## Common Tasks

### Building and Testing
```bash
# Full build (30-60 seconds, NEVER CANCEL)
export PATH="$PWD/dotnet:$PATH"
dotnet build BlazorSliders.sln

# Run WASM test app for development
dotnet run --project BlazorSlidersWasmTestApp

# Run Server test app for testing
dotnet run --project BlazorSlidersServerTestApp

# Update documentation
dotnet tool install --global MarkdownSnippets.Tool
mdsnippets .
```

### Development Workflow
1. **Always build first**: `dotnet build BlazorSliders.sln`
2. **Test component changes**: Run both test apps and manually verify slider functionality
3. **Update documentation**: Run `mdsnippets .` if you modify code examples
4. **Validation**: Test at least 3 different panel layouts (vertical, horizontal, 4-panels)

## Key Implementation Details

### Component Architecture
- **SliderPanelBase.cs**: Base class with common functionality
- **SliderInterop.cs**: JavaScript interop for mouse/touch events  
- **Panel positioning**: Uses absolute positioning with dynamic sizing
- **Styling**: Component-specific CSS with scoped styles

### API Patterns
- **New API**: Use `InitialSliderPosition`, `CurrentSliderPosition` properties
- **Legacy API**: `SliderPosition` parameter still supported for backward compatibility
- **Events**: `SliderPositionChanged` for position update notifications
- **Sizing**: Support for pixels, percentages, and rem units

### Browser Compatibility
- **Modern browsers**: Full support for drag/resize functionality
- **Touch devices**: Touch events supported via JavaScript interop
- **Responsive**: Panels adapt to parent container sizing

## Timing Expectations

- **Cold build**: 45-60 seconds (NEVER CANCEL - set timeout to 120+ seconds)
- **Incremental build**: 15-30 seconds  
- **App startup**: 5-10 seconds for first load
- **Documentation generation**: 5-15 seconds
- **Test validation**: 2-3 minutes for complete manual testing

**CRITICAL**: Always wait for builds and long-running processes to complete. Build cancellation will cause incomplete states.

## Troubleshooting

### Common Issues
- **Build fails with SDK not found**: Install .NET 9.0 SDK using the commands above
- **Apps don't start**: Check console for port conflicts, try different ports
- **Sliders don't work**: Verify JavaScript files are included and SliderInterop is registered
- **Documentation missing snippets**: File paths in README.md must match actual file locations

### When Changes Don't Work
1. Clean and rebuild: `dotnet clean && dotnet build`
2. Restart test applications
3. Clear browser cache for component updates
4. Verify PATH includes local dotnet installation

Always test your changes thoroughly using the validation scenarios above before considering them complete.