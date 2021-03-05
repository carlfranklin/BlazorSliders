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
                {
                    leftPanelId = NewGuid();
                }
                return leftPanelId;
            }
        }

        string rightPanelId = "";
        public string RightPanelId
        {
            get
            {
                if (rightPanelId == "")
                {
                    rightPanelId = NewGuid();
                }
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
        public string RightStyleString { get; set; } = "";

        [Parameter]
        public int SliderWidth { get; set; } = 5;


        [Parameter]
        public int LeftPanelStartingWidth
        {
            get
            {
                return originalLeftPanelWidth;
            }
            set
            {
                if (originalLeftPanelWidth == 0)
                {
                    originalLeftPanelWidth = value;
                    leftPanelWidth = value;
                }
            }
        }

        public int LeftPanelWidth
        {
            get
            {
                return leftPanelWidth;
            }
        }

        [Parameter]
        public int MinimumLeftPanelWidth { get; set; } = 200;

        [Parameter]
        public int MinimumRightPanelWidth { get; set; } = 200;

        protected string LeftPanelWidthPx { get { return leftPanelWidth.ToString() + "px"; } }
        protected string RightPanelWidthPx { get { return RightPanelWidth.ToString() + "px"; } }
        protected string RightPanelLeftPx { get { return (leftPanelWidth + SliderWidth).ToString() + "px"; } }
        protected string SliderWidthPx { get { return SliderWidth.ToString() + "px"; } }

        public int RightPanelWidth
        {
            get
            {
                if (Parent != null)
                {
                    if (this.GetType() == typeof(VerticalSliderPanel) && PanelPosition == PanelPosition.Left)
                        return Width - (LeftPanelWidth + SliderWidth);
                    else
                        return Parent.Width - (LeftPanelWidth + SliderWidth);
                }
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
            Resize(X);
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool FirstRender)
        {
            var myObject = DotNetObjectReference.Create(this);

            if (FirstRender)
            {
                await jsInterop.RegisterVerticalSliderPanel(SliderId, LeftPanelId, RightPanelId, myObject);
            }
        }

        protected override void OnInitialized()
        {
            if (Parent != null)
            {
                if (Parent.GetType() == typeof(AbsolutePanel))
                {
                    ((AbsolutePanel)Parent).ChildPanel = this;
                }
                else if (Parent.GetType() == typeof(VerticalSliderPanel))
                {
                    if (PanelPosition == PanelPosition.Left)
                    {
                        ((VerticalSliderPanel)Parent).LeftPanel = this;
                    }
                    else if (PanelPosition == PanelPosition.Right)
                    {
                        ((VerticalSliderPanel)Parent).RightPanel = this;
                    }
                }
                else if (Parent.GetType() == typeof(HorizontalSliderPanel))
                {
                    if (PanelPosition == PanelPosition.Top)
                    {
                        ((HorizontalSliderPanel)Parent).TopPanel = this;
                    }
                    else if (PanelPosition == PanelPosition.Bottom)
                    {
                        ((HorizontalSliderPanel)Parent).BottomPanel = this;
                    }
                }
            }
        }
    }
}
