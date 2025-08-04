using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorSliders
{
    public partial class HorizontalSliderPanel : SliderPanelBase
    {
        public SliderPanelBase TopPanel { get; set; }
        public SliderPanelBase BottomPanel { get; set; }

        public int topPanelHeight;
        private int originalTopPanelHeight;

        string topPanelId = "";
        public string TopPanelId
        {
            get
            {
                if (topPanelId == "")
                {
                    topPanelId = NewGuid();
                }
                return topPanelId;
            }
        }

        string bottomPanelId = "";
        public string BottomPanelId
        {
            get
            {
                if (bottomPanelId == "")
                {
                    bottomPanelId = NewGuid();
                }
                return bottomPanelId;
            }
        }

		[Parameter]
        public RenderFragment TopHeaderContent { get; set; }

		[Parameter]
        public RenderFragment TopChildContent { get; set; }

        [Parameter]
        public RenderFragment BottomHeaderContent { get; set; }

        [Parameter]
        public RenderFragment BottomChildContent { get; set; }

		[Parameter]
		public RenderFragment SliderContent { get; set; }

		[Parameter]
        public string TopStyleString { get; set; } = "";

        [Parameter]
        public string TopClassString { get; set; } = "";

        [Parameter]
        public string BottomStyleString { get; set; } = "";

        [Parameter]
        public string BottomClassString { get; set; } = "";

        [Parameter]
        public int SliderHeight { get; set; } = 5;

        [Parameter]
        public string SliderClassString { get; set; } = "";

        [Parameter]
        public bool OverrideSliderStyle { get; set; }

        public string DefaultSliderClass { get; set; }

        [Parameter]
        public SizeUnit HeightUnit { get; set; }

        [Parameter]
        public int TopPanelHeight
        {
            get => topPanelHeight;
            set
            {
                if (originalTopPanelHeight <= 0)
                {
                    switch (HeightUnit)
                    {
                        case SizeUnit.Percent:
                            originalTopPanelHeight = DirectHeight * value / 100;
                            topPanelHeight = DirectHeight * value / 100;
                            break;
                        case SizeUnit.Rem:
                            var pixelValue = value * 16;
                            originalTopPanelHeight = pixelValue;
                            topPanelHeight = pixelValue;
                            break;
                        default:
                            originalTopPanelHeight = value;
                            topPanelHeight = value;
                            break;
                    }
                }
            }
        }

        [Parameter]
        public int MinimumTopPanelHeight { get; set; } = 200;

        [Parameter]
        public int MinimumBottomPanelHeight { get; set; } = 200;

        [Parameter]
        public int InitialSliderPosition { get; set; }

        /// <summary>
        /// Gets the current slider position. Use this property to read the current position value.
        /// </summary>
        public int CurrentSliderPosition => topPanelHeight;

        [Parameter]
        public EventCallback<int> SliderPositionChanged { get; set; }

        protected string TopPanelHeightPx { get { return topPanelHeight.ToString() + "px"; } }
        protected string BottomPanelHeightPx { get { return BottomPanelHeight.ToString() + "px"; } }
        protected string BottomPanelTopPx { get { return (topPanelHeight + SliderHeight).ToString() + "px"; } }
        protected string SliderHeightPx { get { return SliderHeight.ToString() + "px"; } }

        public int BottomPanelHeight
        {
            get
            {
                if (Parent != null)
                    return DirectHeight - (TopPanelHeight + SliderHeight);
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
            var oldPosition = topPanelHeight;
            Resize(Y);
            if (topPanelHeight != oldPosition)
            {
                await SliderPositionChanged.InvokeAsync(topPanelHeight);
            }
            await InvokeAsync(StateHasChanged);
        }



        protected override async Task OnAfterRenderAsync(bool FirstRender)
        {
            var myObject = DotNetObjectReference.Create(this);

            if (FirstRender)
                await SliderInterop.RegisterHorizontalSliderPanel(SliderId, TopPanelId, BottomPanelId, myObject);

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

        protected override void OnParametersSet()
        {
            // Set initial slider position if provided - this takes priority over TopPanelHeight
            if (InitialSliderPosition > 0)
            {
                topPanelHeight = InitialSliderPosition;
                if (originalTopPanelHeight <= 0)
                    originalTopPanelHeight = InitialSliderPosition;
            }
        }
    }
}
