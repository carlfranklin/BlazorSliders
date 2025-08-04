using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorSliders
{
    public partial class VerticalSliderPanel : SliderPanelBase
    {
        public SliderPanelBase LeftPanel { get; set; }

        public SliderPanelBase RightPanel { get; set; }

        string leftPanelId = "";
        public int leftPanelWidth;
        private int originalLeftPanelWidth;

        public string LeftPanelId
        {
            get
            {
                if (leftPanelId == "")
                    leftPanelId = NewGuid();
                return leftPanelId;
            }
        }

        string rightPanelId = "";
        public string RightPanelId
        {
            get
            {
                if (rightPanelId == "")
                    rightPanelId = NewGuid();
                return rightPanelId;
            }

        }

        [Parameter]
        public RenderFragment LeftChildContent { get; set; }

        [Parameter]
        public RenderFragment RightChildContent { get; set; }

        [Parameter]
        public string LeftStyleString { get; set; } = "";

        [Parameter]
        public string LeftClassString { get; set; } = "";

        [Parameter]
        public string RightStyleString { get; set; } = "";

        [Parameter]
        public string RightClassString { get; set; } = "";

        [Parameter]
        public int SliderWidth { get; set; } = 5;

        [Parameter]
        public string SliderClassString { get; set; } = "";

        [Parameter]
        public bool OverrideSliderStyle { get; set; }

        public string DefaultSliderClass { get; set; }

        [Parameter]
        public SizeUnit WidthUnit { get; set; }

        [Parameter]
        public int LeftPanelStartingWidth
        {
            get => originalLeftPanelWidth;
            set
            {
                if (originalLeftPanelWidth <= 0)
                {
                    switch (WidthUnit)
                    {
                        case SizeUnit.Percent:
                            originalLeftPanelWidth = DirectWidth * value / 100;
                            leftPanelWidth = DirectWidth * value / 100;
                            break;
                        case SizeUnit.Rem:
                            var pixelValue = value * 16;
                            originalLeftPanelWidth = pixelValue;
                            leftPanelWidth = pixelValue;
                            break;
                        default:
                            originalLeftPanelWidth = value;
                            leftPanelWidth = value;
                            break;
                    }
                }
            }
        }

        public int LeftPanelWidth { get => leftPanelWidth; }

        [Parameter]
        public int MinimumLeftPanelWidth { get; set; } = 200;

        [Parameter]
        public int MinimumRightPanelWidth { get; set; } = 200;

        [Parameter]
        public int SliderPosition
        {
            get => leftPanelWidth;
            set
            {
                if (leftPanelWidth != value)
                {
                    leftPanelWidth = value;
                    SliderPositionChanged.InvokeAsync(value);
                    StateHasChanged();
                }
            }
        }

        [Parameter]
        public EventCallback<int> SliderPositionChanged { get; set; }

        protected string LeftPanelWidthPx { get { return leftPanelWidth.ToString() + "px"; } }
        protected string RightPanelWidthPx { get { return RightPanelWidth.ToString() + "px"; } }
        protected string RightPanelLeftPx { get { return (leftPanelWidth + SliderWidth).ToString() + "px"; } }
        protected string SliderWidthPx { get { return SliderWidth.ToString() + "px"; } }


        
        public int RightPanelWidth
        {
            get
            {
                if (Parent != null)
                    return DirectWidth - (LeftPanelWidth + SliderWidth);
                else
                    return 0;
            }
        }

        [JSInvokable]
        public async Task MouseDown(int X, int Y)
        {
            await InvokeAsync(StateHasChanged);
        }

        [JSInvokable]
        public async Task MouseUp(int X, int Y)
        {
            await InvokeAsync(StateHasChanged);
        }

        [JSInvokable]
        public async Task MouseMove(int X, int Y)
        {
            var oldPosition = leftPanelWidth;
            await InvokeAsync(StateHasChanged);
            Resize(X);
            if (leftPanelWidth != oldPosition)
            {
                await SliderPositionChanged.InvokeAsync(leftPanelWidth);
            }
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool FirstRender)
        {
            var myObject = DotNetObjectReference.Create(this);

            if (FirstRender)
                await SliderInterop.RegisterVerticalSliderPanel(SliderId, LeftPanelId, RightPanelId, myObject);
        }

        protected override void OnInitialized()
        {
            if (OverrideSliderStyle)
                DefaultSliderClass = "";
            else
                DefaultSliderClass = "defaultSlider";

            if (Parent != null)
            {
                if (Parent.GetType() == typeof(AbsolutePanel))
                    ((AbsolutePanel)Parent).ChildPanel = this;
                else if (Parent.GetType() == typeof(VerticalSliderPanel))
                {
                    if (PanelPosition == PanelPosition.Left)
                        ((VerticalSliderPanel)Parent).LeftPanel = this;
                    else if (PanelPosition == PanelPosition.Right)
                        ((VerticalSliderPanel)Parent).RightPanel = this;
                }
                else if (Parent.GetType() == typeof(HorizontalSliderPanel))
                {
                    if (PanelPosition == PanelPosition.Top)
                        ((HorizontalSliderPanel)Parent).TopPanel = this;
                    else if (PanelPosition == PanelPosition.Bottom)
                        ((HorizontalSliderPanel)Parent).BottomPanel = this;
                }
            }
        }
    }
}
