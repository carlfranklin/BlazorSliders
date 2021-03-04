# BlazorSliders

Create multiple panels separated by sliding splitters.

Github: https://github.com/carlfranklin/BlazorSliders

## Live Demo:

https://blazorslidertest.azurewebsites.net/

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



## Requirements

Add to *_Imports.razor*:

```c#
@using BlazorSliders
```

Change your *\Shared\MainLayout.razor* to the following:

```
@inherits LayoutComponentBase
@Body
```

**For Blazor Server:**

`ServerPrerendered` is not supported. In your *_Hosts.cshtml* file, set the following:

```xml
<component type="typeof(App)" render-mode="Server" />
```

Add the following line to `ConfigureServices` in *Startup.cs*:

```c#
services.AddScoped<SliderInterop>();
```

**For Blazor WebAssembly:**

Add the following to `Main()` in *Program.cs*:

```
builder.Services.AddScoped<SliderInterop>();
```



### Simple Vertical Split:

```xml
@page "/"

<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="400">
        <LeftChildContent>
            <div style="padding:10px;">
                <h3>Left Content</h3>
                This is a demo of a single vertical slider panel.
            </div>
        </LeftChildContent>
        <RightChildContent>
            <div style="padding:10px;">
                <h3>Right Content</h3>
                <NavMenu/>
            </div>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>
```



### Simple Horizontal Split:

```xml
@page "/horizontals"

<AbsolutePanel AutoResize="true">
    <HorizontalSliderPanel TopPanelHeight="200">
        <TopChildContent>
            <div style="padding:10px;">
                <h3>Top Content</h3>
                This is a demo of a single horizontal slider panel.
            </div>
        </TopChildContent>
        <BottomChildContent>
            <div style="padding:10px;">
                <h3>Bottom Content</h3>
                <NavMenu />
            </div>
        </BottomChildContent>
    </HorizontalSliderPanel>
</AbsolutePanel>
```



### Four Panels:

```xml
@page "/fourpanels"

<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="400">
        <LeftChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Left"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
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
                        <NavMenu />
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Right"
                                   TopStyleString="background-color:orange;"
                                   BottomStyleString="background-color:yellow;"
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
```



### Initial size and position based on width of browser:

```xml
@page "/windowresize"

<Window WindowResized="OnWindowResized" />

@(WindowSize != null)
{
<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="@StartingWindowWidth">
        <LeftChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Left"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
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
                        <NavMenu />
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Right"
                                   TopStyleString="background-color:orange;"
                                   BottomStyleString="background-color:yellow;"
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
                var height = WindowSize.Height * StartingWindowHeightPercent / 100;
                return height;
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



### Complex Nesting:

> Note: Currently you can only nest Horizontal panels inside Vertical panels, and vice versa.

```xml
@page "/crazy"

<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="400">
        <LeftChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Left"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
                                   TopPanelHeight="200">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 1</h3>
                        This is the craziest demo of all
                    </div>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 1</h3>
                        <NavMenu />
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Right"
                                   BottomStyleString="background-color:violet;"
                                   TopPanelHeight="600">
                <TopChildContent>
                    <VerticalSliderPanel PanelPosition="PanelPosition.Top"
                                         LeftPanelStartingWidth="700">
                        <LeftChildContent>
                            <HorizontalSliderPanel PanelPosition="PanelPosition.Left"
                                                   TopStyleString="background-color:orange;"
                                                   BottomStyleString="background-color:lightblue;"
                                                   TopPanelHeight="300">
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
                        </LeftChildContent>
                        <RightChildContent>
                            <HorizontalSliderPanel PanelPosition="PanelPosition.Right"
                                                   TopStyleString="background-color:pink;"
                                                   BottomStyleString="background-color:lightgreen;"
                                                   TopPanelHeight="400">
                                <TopChildContent>
                                    <div style="padding:10px;">
                                        <h3>Top Content 3</h3>
                                    </div>
                                </TopChildContent>
                                <BottomChildContent>
                                    <div style="padding:10px;">
                                        <h3>Bottom Content 3</h3>
                                    </div>
                                </BottomChildContent>
                            </HorizontalSliderPanel>
                        </RightChildContent>
                    </VerticalSliderPanel>
                </TopChildContent>
                <BottomChildContent>
                    <div style="padding:10px;">
                        <h3>Bottom Content 4</h3>
                    </div>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>
```


### NavMenu used in demos:

```xml
<div>
    <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
        <span class="oi oi-resize-width" aria-hidden="true"></span> Vertical
    </NavLink>
    <NavLink class="nav-link" href="horizontals">
        <span class="oi oi-resize-height" aria-hidden="true"></span> Horizontal
    </NavLink>
    <NavLink class="nav-link" href="fourpanels">
        <span class="oi oi-resize-both" aria-hidden="true"></span> 4 Panels
    </NavLink>
    <NavLink class="nav-link" href="windowresize">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Percent Width
    </NavLink>
    <NavLink class="nav-link" href="crazy">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Crazy
    </NavLink>
</div>
```

