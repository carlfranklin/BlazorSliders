# BlazorSliders - New Slider Position API

## Problem Solved

The original `SliderPosition` parameter caused cyclical binding issues when used with two-way binding (`@bind-SliderPosition`). Users couldn't reliably read current slider positions (e.g., in `DisposeAsync`) and setting values in the `SliderPositionChanged` event handler caused infinite loops.

## Solution

Added new properties to both `HorizontalSliderPanel` and `VerticalSliderPanel`:

### New Properties

1. **`InitialSliderPosition`** (Parameter) - Set the initial position without binding conflicts
2. **`CurrentSliderPosition`** (Read-only Property) - Get the current position anytime
3. **`SliderPositionChanged`** (Event) - Receive position change notifications (unchanged)

### Usage Examples

#### Setting Initial Position and Reading Current Position

```csharp
<HorizontalSliderPanel @ref="horizontalSlider"
                       InitialSliderPosition="200" 
                       SliderPositionChanged="OnPositionChanged">
    <TopChildContent>
        <div>Top Panel Content</div>
    </TopChildContent>
    <BottomChildContent>
        <div>Bottom Panel Content</div>
    </BottomChildContent>
</HorizontalSliderPanel>

@code {
    private HorizontalSliderPanel horizontalSlider;

    private void OnPositionChanged(int newPosition)
    {
        // Handle position changes without cyclical binding
        Console.WriteLine($"Position changed to: {newPosition}");
    }

    // Read current position anytime (e.g., in DisposeAsync)
    public async ValueTask DisposeAsync()
    {
        if (horizontalSlider != null)
        {
            int currentPosition = horizontalSlider.CurrentSliderPosition;
            // Save position or perform cleanup
            await SavePositionAsync(currentPosition);
        }
    }
}
```

#### For Vertical Sliders

```csharp
<VerticalSliderPanel @ref="verticalSlider"
                     InitialSliderPosition="250"
                     SliderPositionChanged="OnVerticalPositionChanged">
    <LeftChildContent>
        <div>Left Panel Content</div>
    </LeftChildContent>
    <RightChildContent>
        <div>Right Panel Content</div>
    </RightChildContent>
</VerticalSliderPanel>

@code {
    private VerticalSliderPanel verticalSlider;

    private void OnVerticalPositionChanged(int newPosition)
    {
        // Handle changes without cyclical binding issues
        Console.WriteLine($"Vertical position: {newPosition}");
    }

    // Read current position
    private void LogCurrentPosition()
    {
        int current = verticalSlider?.CurrentSliderPosition ?? 0;
        Console.WriteLine($"Current vertical position: {current}");
    }
}
```

## Backward Compatibility

The existing `SliderPosition` parameter still works for backward compatibility, but now avoids cyclical binding issues. However, we recommend using the new API for better control:

- Use `InitialSliderPosition` to set starting positions
- Use `CurrentSliderPosition` to read current positions
- Use `SliderPositionChanged` for event handling

## Benefits

✅ **No Cyclical Binding** - Bound properties won't change unexpectedly  
✅ **Reliable Position Reading** - Access current positions anytime (perfect for DisposeAsync)  
✅ **Clean API** - Separate concerns: initial vs. current vs. change notifications  
✅ **Backward Compatible** - Existing code continues to work  
✅ **Consistent** - Same API for both horizontal and vertical sliders  

## Migration Guide

### Before (Problematic)
```csharp
@bind-SliderPosition="myPosition" // Caused cyclical binding
```

### After (Recommended)
```csharp
InitialSliderPosition="@myInitialPosition"
SliderPositionChanged="OnPositionChanged"
@ref="mySlider"

// Read with: mySlider.CurrentSliderPosition
```