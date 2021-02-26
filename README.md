# BlazorSliders

Create multiple panels separated by sliding splitters.

## Install with NuGet:

```
Install-Package BlazorSliders
```

### Description

There are four main components:

#### AbsolutePanel

AbsolutePanel is the container for a page. It provides events for when the window is resized.

#### Window

Window is used by the AbsolutePanel but can also be used on its own. It provides an event for when the window (browser) is resized, passing the Width and Height.

#### VerticalSliderPanel

Provides two content components and a vertical splitter between the two. Handles all UI requirements.

#### HorizontalSliderPanel

Provides two content components and a horizontal splitter between the two. Handles all UI requirements.

### Simple Horizontal Split:

```xml
@page "/"

<AbsolutePanel @ref=absolutePanel
               Id="ParentPanel"
               AutoResize="true">
    <VerticalSliderPanel @ref=verticalPanel
                         Container=absolutePanel
                         ParentId="ParentPanel"
                         LeftPanelId="LeftPanel1"
                         RightPanelId="RightPanel1"
                         SliderWidth="10"
                         Width="absolutePanel.Width"
                         Height="absolutePanel.Height"
                         LeftPanelStartingWidth="400">
        <LeftChildContent>
            <div id="LeftPanel1" style="padding:10px;">
                <h3>Left Content</h3>
                This is a demo of a single vertical slider panel.
            </div>
        </LeftChildContent>
        <RightChildContent>
            <div id="RightPanel1" style="padding:10px;">
                <h3>Right Content</h3>
            </div>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>

@code
{
    AbsolutePanel absolutePanel { get; set; }
    VerticalSliderPanel verticalPanel { get; set; }
}
```



### Simple Vertical Split:

```xml
@page "/horizontals"

<AbsolutePanel @ref=absolutePanel
               Id="ParentPanel"
               AutoResize="true">
    <HorizontalSliderPanel @ref=horizontalPanel
                           Container=absolutePanel
                           ParentId="ParentPanel"
                           TopPanelId="TopPanel1"
                           BottomPanelId="BottomPanel1"
                           SliderHeight="10"
                           Width="absolutePanel.Width"
                           Height="absolutePanel.Height"
                           TopPanelHeight="400">
        <TopChildContent>
            <div id="TopPanel1" style="padding:10px;">
                <h3>Top Content</h3>
                This is a demo of a single horizontal slider panel.
            </div>
        </TopChildContent>
        <BottomChildContent>
            <div id="BottomPanel1" style="padding:10px;">
                <h3>Bottom Content</h3>
            </div>
        </BottomChildContent>
    </HorizontalSliderPanel>
</AbsolutePanel>

@code
{
    AbsolutePanel absolutePanel { get; set; }
    HorizontalSliderPanel horizontalPanel { get; set; }
}
```



### Four Panels:

```xml
@page "/fourpanels"

<AbsolutePanel @ref=absolutePanel
               Id="ParentPanel"
               AutoResize="true">
    <VerticalSliderPanel @ref=verticalPanel
                         Container=absolutePanel
                         ParentId="ParentPanel"
                         LeftPanelId="LeftPanel1"
                         RightPanelId="RightPanel1"
                         SliderWidth="10"
                         Width="absolutePanel.Width"
                         Height="absolutePanel.Height"
                         LeftPanelStartingWidth="400">
        <LeftChildContent>
            <HorizontalSliderPanel Container=absolutePanel
                                   ParentId="LeftPanel1"
                                   BottomPanelId="BottomPanelId1"
                                   SliderHeight="10"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
                                   Width="@verticalPanel.LeftPanelWidth"
                                   Height="@absolutePanel.Height"
                                   TopPanelHeight="200">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 1</h3>
                        This is a demo of four panels with styling
                    </div>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 1</h3>
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel Container=absolutePanel
                                   ParentId="RightPanel1"
                                   SliderHeight="10"
                                   TopStyleString="background-color:orange;"
                                   BottomStyleString="background-color:yellow;"
                                   Width="@verticalPanel.RightPanelWidth"
                                   Height="@absolutePanel.Height"
                                   TopPanelHeight="400">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 2</h3>
                    </div>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 2</h3>
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>

@code
{
    AbsolutePanel absolutePanel { get; set; }
    VerticalSliderPanel verticalPanel { get; set; }

}
```



### Initial size and position based on width of browser:

```xml
@page "/windowresize"

<Window WindowResized="OnWindowResized" />

@(WindowSize != null)
{
<AbsolutePanel @ref=absolutePanel
               Id="ParentPanel"
               AutoResize="true">
    <VerticalSliderPanel @ref=verticalPanel
                         Container=absolutePanel
                         ParentId="ParentPanel"
                         LeftPanelId="LeftPanel1"
                         RightPanelId="RightPanel1"
                         SliderWidth="10"
                         Width="absolutePanel.Width"
                         Height="absolutePanel.Height"
                         LeftPanelStartingWidth="@StartingWindowWidth">
        <LeftChildContent>
            <HorizontalSliderPanel Container=absolutePanel
                                   ParentId="LeftPanel1"
                                   BottomPanelId="BottomPanelId1"
                                   SliderHeight="10"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
                                   Width="@verticalPanel.LeftPanelWidth"
                                   Height="@absolutePanel.Height"
                                   TopPanelHeight="@StartingWindowHeight">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 1</h3>
                        This demo initializes the size and location of the sliders as a percentage of the 
                        initial width of the browser.
                    </div>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 1</h3>
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel Container=absolutePanel
                                   ParentId="RightPanel1"
                                   SliderHeight="10"
                                   TopStyleString="background-color:orange;"
                                   BottomStyleString="background-color:yellow;"
                                   Width="@verticalPanel.RightPanelWidth"
                                   Height="@absolutePanel.Height"
                                   TopPanelHeight="@StartingWindowHeight">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 2</h3>
                    </div>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 2</h3>
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>
}

@code
{
    AbsolutePanel absolutePanel { get; set; }
    VerticalSliderPanel verticalPanel { get; set; }
    Size WindowSize { get; set; } = null;
    int StartingWindowWidthPercent = 50;
    int StartingWindowHeightPercent = 50;

    int StartingWindowWidth
    {
        get
        {
            if (WindowSize != null)
            {
                return WindowSize.Width * StartingWindowWidthPercent / 100;
            }
            else
                return 0;
        }
    }

    int StartingWindowHeight
    {
        get
        {
            if (WindowSize != null)
            {
                return WindowSize.Height * StartingWindowHeightPercent / 100;
            }
            else
                return 0;
        }
    }

    void OnWindowResized(Size Size)
    {
        WindowSize = Size;
    }
}
```
