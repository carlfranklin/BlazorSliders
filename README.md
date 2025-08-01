# <img src="icon.png" height="30px"> BlazorSliders

**Nuget**: https://www.nuget.org/packages/BlazorSliders/

```
Install-Package BlazorSliders
```

Create multiple panels separated by sliding splitters.

Github: https://github.com/carlfranklin/BlazorSliders

<!-- toc -->
## Contents

  * [Live Demo:](#live-demo)
  * [YouTube Demo (BlazorTrain):](#youtube-demo-blazortrain)
  * [Install with NuGet:](#install-with-nuget)
  * [Description](#description)
    * [AbsolutePanel](#absolutepanel)
    * [Window](#window)
    * [VerticalSliderPanel](#verticalsliderpanel)
    * [HorizontalSliderPanel](#horizontalsliderpanel)
  * [Usage](#usage)
    * [Simple Vertical Split:](#simple-vertical-split)
    * [Simple Horizontal Split:](#simple-horizontal-split)
    * [Horizontal Split with Sticky Headers:](#horizontal-split-with-sticky-headers)
    * [Four Panels:](#four-panels)
    * [Initial size and position based on percent size of browser:](#initial-size-and-position-based-on-percent-size-of-browser)
      * [Alternate Method for Defining Panels by Percentage of Containing Element](#alternate-method-for-defining-panels-by-percentage-of-containing-element)
      * [Define Width/Height by Rem Units](#define-widthheight-by-rem-units)
    * [Custom Class Setup:](#custom-class-setup)
    * [Containing the Panels to Its Parent Container:](#containing-the-panels-to-its-parent-container)
    * [Complex Nesting:](#complex-nesting)
      * [Vertical Panel Nested in Vertical Panel:](#vertical-panel-nested-in-vertical-panel)
      * [Horizontal Panel Nested in Horizontal Panel:](#horizontal-panel-nested-in-horizontal-panel)
      * [Crazy Nesting Inception:](#crazy-nesting-inception)
    * [NavMenu used in demos:](#navmenu-used-in-demos)
  * [Icon](#icon)<!-- endToc -->


## Live Demo:

https://blazorslidertest.azurewebsites.net/


## YouTube Demo (BlazorTrain):

https://youtu.be/fNDd7moZJ4c


## Description

There are four main components:


#### AbsolutePanel

AbsolutePanel is the container for a page. It provides events for when the window is resized.


#### Window

Window is used by the AbsolutePanel but can also be used on its own. It provides an event for when the window (browser) is resized, passing the Width and Height.


#### VerticalSliderPanel

Provides two content components and a vertical splitter between the two. Handles all UI requirements.


#### HorizontalSliderPanel

Provides two content components and a horizontal splitter between the two. Handles all UI requirements.

**New in this version**: Added support for sticky headers that remain at the top of panels when content is scrollable:
- `TopHeaderContent` - Optional sticky header for the top panel
- `BottomHeaderContent` - Optional sticky header for the bottom panel

These headers use CSS `position: sticky` to remain visible at the top of their respective panels when the content below scrolls.


## Usage

Add to *_Imports.razor*:

```c#
@using BlazorSliders    
```

For the demo, change your *\Shared\MainLayout.razor* to the following:

```
@inherits LayoutComponentBase
@Body
```

For the demo, move *NavMenu.razor* from the *Layout* folder to the *Components* folder and change to this:

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
    <NavLink class="nav-link" href="doublevertical">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Double Vertical
    </NavLink>
    <NavLink class="nav-link" href="doublehorizontal">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Double Horizontal
    </NavLink>
    <NavLink class="nav-link" href="customclasses">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Custom Classes
    </NavLink>
    <NavLink class="nav-link" href="parentcontained">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Parent Contained
    </NavLink>
    <NavLink class="nav-link" href="goldenratio">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Golden Ratio
    </NavLink>
</div>
```

#### `Pre-rendering` is not supported. 

If running in server render mode, set the following in your *App.razor* file:

```xml
<HeadOutlet @rendermode="new InteractiveServerRenderMode(false)" />
<Routes @rendermode="new InteractiveServerRenderMode(false)" />
```

For the demo, Add styles to *wwwroot/css/app.css*:

```css
.panel-base {
    color: #B8C8C8;
}

.left-panel {
    background-color: #1E1E1E;
}

.right-panel {
    background-color: #181818;
}

.custom-slider {
    background-color: #3E3E42;
    cursor: e-resize;
}
```

Add this to *Program.cs*

```c#
builder.Services.AddScoped<SliderInterop>();
```

### Simple Vertical Split:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/Index.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/Index.razor'></a>

```razor
@page "/"

<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="75">
        <LeftChildContent>
            <div style="padding:10px;">
                <h3>Left Content</h3>
                This is a demo of a single vertical slider panel.
            </div>
        </LeftChildContent>
        <RightChildContent>
            <div style="padding:10px;">
                <h3>Right Content</h3>
                <NavMenu />
            </div>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>

```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/Index.razor#L1-L19' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/Index.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Simple Horizontal Split:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/Horizontals.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/Horizontals.razor'></a>
```razor
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
<sup><a href='/BlazorSlidersWasmTestApp/Pages/Horizontals.razor#L1-L18' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/Horizontals.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Horizontal Split with Sticky Headers:

The HorizontalSliderPanel now supports optional sticky headers that remain at the top of panels when content scrolls:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor'></a>
```razor
@page "/stickyheaders"

<AbsolutePanel AutoResize="true">
    <HorizontalSliderPanel TopPanelHeight="300" SliderHeight="10">
        <TopHeaderContent>
            <div style="padding:10px; background-color:#f0f0f0; border-bottom:1px solid #ccc;">
                <h4 style="margin:0;">Top Panel Sticky Header</h4>
                <p style="margin:5px 0 0 0;">This header sticks to the top when content scrolls</p>
            </div>
        </TopHeaderContent>
        <TopChildContent>
            <div style="padding:10px;">
                <h3>Top Content (Scrollable)</h3>
                <p>This content scrolls while the header above stays fixed.</p>
                @for (int i = 1; i <= 20; i++)
                {
                    <p>Scrollable content line @i...</p>
                }
            </div>
        </TopChildContent>
        <BottomHeaderContent>
            <div style="padding:10px; background-color:#e0e0e0; border-bottom:1px solid #ccc;">
                <h4 style="margin:0;">Bottom Panel Sticky Header</h4>
                <p style="margin:5px 0 0 0;">This header also sticks to the top of its panel</p>
            </div>
        </BottomHeaderContent>
        <BottomChildContent>
            <div style="padding:10px;">
                <h3>Bottom Content (Scrollable)</h3>
                <p>This content also scrolls while the header above stays fixed.</p>
                @for (int i = 1; i <= 30; i++)
                {
                    <p>More scrollable content line @i...</p>
                }
            </div>
        </BottomChildContent>
    </HorizontalSliderPanel>
</AbsolutePanel>
```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor#L1-L35' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/StickyHeaders.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

**Key Features:**
- Headers are completely optional - existing code works unchanged
- Headers use CSS `position: sticky` for smooth scrolling performance  
- Headers automatically inherit panel background styling
- Can be used on either panel independently (mixed usage)
- Content scrolls normally while headers remain visible


### Four Panels:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/FourPanels.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/FourPanels.razor'></a>
```razor
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
<sup><a href='/BlazorSlidersWasmTestApp/Pages/FourPanels.razor#L1-L43' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/FourPanels.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Initial size and position based on percent size of browser:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/WindowResize.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/WindowResize.razor'></a>
```razor
@page "/windowresize"

<Window WindowResized="OnWindowResized" />

@(WindowSize != null)
{
<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="@VerticalLeftPanelWidth">
        <LeftChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Left"
                                   TopStyleString="background-color:antiquewhite;"
                                   BottomStyleString="background-color:aliceblue;"
                                   TopPanelHeight="@LeftHorizontalTopPanelHeight">
                <TopChildContent>
                    <div style="padding:10px;">
                        <h3>Top Content 1</h3>
                        This demo sets the location of the sliders based on a percentage of the
                        initial size of the browser.
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
                                   TopPanelHeight="@RightHorizontalTopPanelHeight">
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

    int VerticalLeftPanelWidthPercent = 80;
    int LeftHorizontal1TopPanelHeightPercent = 40;
    int RightHorizontal1TopPanelHeightPercent = 60;

    int VerticalLeftPanelWidth
    {
        get
        {
            if (WindowSize != null)
            {
                return WindowSize.Width * VerticalLeftPanelWidthPercent / 100;
            }
            else
                return 0;
        }
    }

    int LeftHorizontalTopPanelHeight
    {
        get
        {
            if (WindowSize != null)
            {
                var height = WindowSize.Height * LeftHorizontal1TopPanelHeightPercent / 100;
                return height;
            }
            else
                return 0;
        }
    }

    int RightHorizontalTopPanelHeight
    {
        get
        {
            if (WindowSize != null)
            {
                var height = WindowSize.Height * RightHorizontal1TopPanelHeightPercent / 100;
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
<sup><a href='/BlazorSlidersWasmTestApp/Pages/WindowResize.razor#L1-L103' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/WindowResize.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<kbd><img src="UnitTests/IntegrationTest.WindowResize.01.verified.png" width="400px"></kbd>

#### Alternate Method for Defining Panels by Percentage of Containing Element

Alternatively, you can define the property `HeightUnit` or `WidthUnit` (depending on if you're defining a horizontal panel or vertical panel) to `SizeUnit.Percent` and then set the initial percent for either the `TopPanelHeight` or `LeftPanelStartingWidth` properties. 

This method finds the width or height of the containing element and calculates the left panel's width or top panel's height.

<!-- snippet: BlazorSlidersWasmTestApp/Pages/GoldenRatio.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/GoldenRatio.razor'></a>
```razor
@page "/goldenratio"


<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="62">
        <LeftChildContent>
            <div style="padding:10px;">
                <h3>Approximate Golden Ratio Example</h3>
                <NavMenu />
            </div>
        </LeftChildContent>
        <RightChildContent>
            <HorizontalSliderPanel PanelPosition="PanelPosition.Right" HeightUnit="SizeUnit.Percent" TopPanelHeight="62">
                <BottomChildContent>
                    <VerticalSliderPanel PanelPosition="PanelPosition.Bottom" WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="38">
                        <LeftChildContent>
                            <HorizontalSliderPanel PanelPosition="PanelPosition.Left" HeightUnit="SizeUnit.Percent" TopPanelHeight="38">
                                <TopChildContent>
                                    <VerticalSliderPanel PanelPosition="PanelPosition.Bottom" WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="62" MinimumRightPanelWidth="100" MinimumLeftPanelWidth="100">
                                        <RightChildContent>
                                            <HorizontalSliderPanel PanelPosition="PanelPosition.Right" HeightUnit="SizeUnit.Percent" TopPanelHeight="50" MinimumBottomPanelHeight="100" MinimumTopPanelHeight="100">

                                            </HorizontalSliderPanel>
                                        </RightChildContent>
                                    </VerticalSliderPanel>
                                </TopChildContent>
                            </HorizontalSliderPanel>
                        </LeftChildContent>
                    </VerticalSliderPanel>
                </BottomChildContent>
            </HorizontalSliderPanel>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>

@code {

}
```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/GoldenRatio.razor#L1-L38' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/GoldenRatio.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<kbd><img src="UnitTests/IntegrationTest.GoldenRatio.01.verified.png" width="400px"></kbd>

#### Define Width/Height by Rem Units

Another option for the `HeightUnit` and `WidthUnit` is `SizeUnit.Rem` which allows the user to define the initial width/height of a panel by rem units (this assumes the default of 16px per 1 rem).

### Custom Class Setup:

To add custom classes to the panels, add classes with the parameter `TopClassString`, `BottomClassString`, `LeftClassString`, or `RightClassString`. These classes cannot be defined in the isolated page css file (i.e. *Page*.razor.css) as the nested component won't be recognized as part of the page at compile time.

To add custom classes to the slider, set the `OverrideSliderStyle` parameter to `true` and add the classes to the parameter `SliderClassCss`. When overriding the slider's base css class, make sure to define a slider background color as well as a cursor to indicate the slider is draggable.

<!-- snippet: BlazorSlidersWasmTestApp/Pages/CustomClasses.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/CustomClasses.razor'></a>
```razor
@page "/customclasses"

<AbsolutePanel AutoResize="true">
    <VerticalSliderPanel LeftPanelStartingWidth="400" 
                         OverrideSliderStyle="true" 
                         LeftClassString="@leftClasses" 
                         RightClassString="@rightClasses" 
                         SliderClassString="custom-slider">
        <LeftChildContent>
            <div style="padding:10px;">
                <h3>Left Content</h3>
                This is a demo of custom class styling for the panels and slider.
            </div>
        </LeftChildContent>
        <RightChildContent>
            <div style="padding:10px;">
                <h3>Right Content</h3>
                <NavMenu />
            </div>
        </RightChildContent>
    </VerticalSliderPanel>
</AbsolutePanel>

@code {

    string leftClasses = "panel-base left-panel";
    string rightClasses = "panel-base right-panel";

}
```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/CustomClasses.razor#L1-L29' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/CustomClasses.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<kbd><img src="UnitTests/IntegrationTest.CustomClasses.01.verified.png" width="400px"></kbd>


### Containing the Panels to Its Parent Container:

To contain the overall panel system to its parent container, set the parameter `ParentContained` to `true` in the Absolute Panel component. It is necessary to specify the parent's css position property to ensure the Absolute Panel will be contained within the parent div. 

Ensure the parent container also has a defined height and width (in the example provided, the height is set to `100vh - 2rem` where 2rem is the height of the sample top bar and the width is set to `100vw - 20rem` where 20rem is the width of the sample side container).

<!-- snippet: BlazorSlidersWasmTestApp/Pages/ParentContained.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/ParentContained.razor'></a>
```razor
@page "/parentcontained"

<div class="page">
    <div class="top-nav">
        <ul>
            <li> <a href="#">Link 1</a></li>
            <li> <a href="#">Link 2</a></li>
            <li> <a href="#">Link 3</a></li>
        </ul>
    </div>
    <div class="content-wrapper">
        <div class="side-sample">
            <h3>This is a sample sidebar that doesn't live in the adjustable panels</h3>
        </div>
        <div class="panels-parent">
            <AbsolutePanel AutoResize="true" ParentContained="true">
                <VerticalSliderPanel LeftPanelStartingWidth="600">
                    <LeftChildContent>
                        <VerticalSliderPanel PanelPosition="PanelPosition.Left" LeftPanelStartingWidth="400">
                            <LeftChildContent>
                                <div style="padding:10px;">
                                    <h3>Left Content in the Left Parent Panel</h3>
                                    This is a demo of a nested vertical slider panel.
                                </div>
                            </LeftChildContent>
                            <RightChildContent>
                                <div style="padding:10px;">
                                    <h3>Right Content in the Left Parent Panel</h3>
                                </div>
                            </RightChildContent>
                        </VerticalSliderPanel>
                    </LeftChildContent>
                    <RightChildContent>
                        <VerticalSliderPanel PanelPosition="PanelPosition.Right" LeftPanelStartingWidth="400">
                            <LeftChildContent>
                                <div style="padding:10px;">
                                    <h3>Left Content in the Right Parent Panel</h3>
                                </div>
                            </LeftChildContent>
                            <RightChildContent>
                                <div style="padding:10px;">
                                    <h3>Right Content in the Right Parent Panel</h3>
                                    <NavMenu />
                                </div>
                            </RightChildContent>
                        </VerticalSliderPanel>

                    </RightChildContent>
                </VerticalSliderPanel>
            </AbsolutePanel>
        </div>
    </div>

</div>

@code {

}
```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/ParentContained.razor#L1-L58' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/ParentContained.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<!-- snippet: BlazorSlidersWasmTestApp/Pages/ParentContained.razor.css -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/ParentContained.razor.css'></a>
```css
body {
}

.page{
    display: flex;
    flex-wrap: wrap;
}

.top-nav{
    flex: 0 0 100%;
    height: 2rem;
    background-color: gray;

}
.top-nav ul{
    margin-left: auto;
    margin-right: auto;
    max-width: 20rem;
    display: flex;
    list-style-type: none;
    justify-content: space-between;
}
.top-nav li{
    text-decoration: none;
}

.content-wrapper{
    display: flex;
}

.side-sample{
    width: 20rem;
    height: calc(100vh - 2rem);
    background-color: antiquewhite;
}

.panels-parent{
    position: relative;
    width: calc(100vw - 20rem);
    height: calc(100vh - 2rem);
}
```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/ParentContained.razor.css#L1-L41' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/ParentContained.razor.css' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

<kbd><img src="UnitTests/IntegrationTest.ParentContained.01.verified.png" width="400px"></kbd>

### Complex Nesting:

> Note: Vertical nesting in vertical panels and horizontal nesting in horizontal panels has now been implemented as shown below.

#### Vertical Panel Nested in Vertical Panel:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/DoubleVertical.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/DoubleVertical.razor'></a>
```razor
@page "/doublevertical"


        <AbsolutePanel AutoResize="true">
            <VerticalSliderPanel WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="50">
                <LeftChildContent>
                    <VerticalSliderPanel PanelPosition="PanelPosition.Left" WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="25">
                        <LeftChildContent>
                            <div style="padding:10px;">
                                <h3>Left Content in the Left Parent Panel</h3>
                                This is a demo of a nested vertical slider panel.
                            </div>
                        </LeftChildContent>
                        <RightChildContent>
                            <div style="padding:10px;">
                                <h3>Right Content in the Left Parent Panel</h3>
                            </div>
                        </RightChildContent>
                    </VerticalSliderPanel>
                </LeftChildContent>
                <RightChildContent>
                    <VerticalSliderPanel PanelPosition="PanelPosition.Right" WidthUnit="SizeUnit.Percent" LeftPanelStartingWidth="50">
                        <LeftChildContent>
                            <div style="padding:10px;">
                                <h3>Left Content in the Right Parent Panel</h3>
                            </div>
                        </LeftChildContent>
                        <RightChildContent>
                            <div style="padding:10px;">
                                <h3>Right Content in the Right Parent Panel</h3>
                                <NavMenu />
                            </div>
                        </RightChildContent>
                    </VerticalSliderPanel>

                </RightChildContent>
            </VerticalSliderPanel>
        </AbsolutePanel>


```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/DoubleVertical.razor#L1-L40' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/DoubleVertical.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

#### Horizontal Panel Nested in Horizontal Panel:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/DoubleHorizontal.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/DoubleHorizontal.razor'></a>
```razor
@page "/doublehorizontal"


    <AbsolutePanel AutoResize="true">
        <HorizontalSliderPanel TopPanelHeight="400">
            <TopChildContent>
                <HorizontalSliderPanel PanelPosition="PanelPosition.Top" TopPanelHeight="200">
                    <TopChildContent>
                        <div style="padding:10px;">
                            <h3>Top Content in the Top Parent Panel</h3>
                            This is a demo of a nested horizontal slider panel.
                        </div>
                    </TopChildContent>
                    <BottomChildContent>
                        <div style="padding:10px;">
                            <h3>Bottom Content in the Top Parent Panel</h3>
                        </div>
                    </BottomChildContent>
                </HorizontalSliderPanel>
            </TopChildContent>
            <BottomChildContent>
                <HorizontalSliderPanel PanelPosition="PanelPosition.Bottom" TopPanelHeight="600">
                    <TopChildContent>
                        <div style="padding:10px;">
                            <h3>Top Content in the Bottom Parent Panel</h3>
                        </div>
                    </TopChildContent>
                    <BottomChildContent>
                        <div style="padding:10px;">
                            <h3>Bottom Content in the Bottom Parent Panel</h3>
                            <NavMenu />
                        </div>
                    </BottomChildContent>
                </HorizontalSliderPanel>
            </BottomChildContent>
        </HorizontalSliderPanel>
    </AbsolutePanel>

```
<sup><a href='/BlazorSlidersWasmTestApp/Pages/DoubleHorizontal.razor#L1-L38' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/DoubleHorizontal.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

#### Crazy Nesting Inception:

<!-- snippet: BlazorSlidersWasmTestApp/Pages/Crazy.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Pages/Crazy.razor'></a>
```razor
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
<sup><a href='/BlazorSlidersWasmTestApp/Pages/Crazy.razor#L1-L75' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Pages/Crazy.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### NavMenu used in demos:

<!-- snippet: BlazorSlidersWasmTestApp/Shared/NavMenu.razor -->
<a id='snippet-BlazorSlidersWasmTestApp/Shared/NavMenu.razor'></a>
```razor
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
    <NavLink class="nav-link" href="doublevertical">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Double Vertical
    </NavLink>
    <NavLink class="nav-link" href="doublehorizontal">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Double Horizontal
    </NavLink>
    <NavLink class="nav-link" href="customclasses">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Custom Classes
    </NavLink>
    <NavLink class="nav-link" href="parentcontained">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Parent Contained
    </NavLink>
    <NavLink class="nav-link" href="goldenratio">
        <span class="oi oi-resize-both" aria-hidden="true"></span> Golden Ratio
    </NavLink>
</div>
```
<sup><a href='/BlazorSlidersWasmTestApp/Shared/NavMenu.razor#L1-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-BlazorSlidersWasmTestApp/Shared/NavMenu.razor' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->



## Icon

[slide](https://thenounproject.com/term/slide/3631831/) designed by [Eucalyp](https://thenounproject.com/eucalyp) from [The Noun Project](https://thenounproject.com/). Licensed under [Creative Commons](https://creativecommons.org/licenses/by/3.0/us/legalcode).
