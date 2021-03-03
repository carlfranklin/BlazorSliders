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
        public RenderFragment TopChildContent { get; set; }

        [Parameter]
        public RenderFragment BottomChildContent { get; set; }

        [Parameter]
        public string TopStyleString { get; set; } = "";

        [Parameter]
        public string BottomStyleString { get; set; } = "";

        [Parameter]
        public int SliderHeight { get; set; } = 5;

        [Parameter]
        public int TopPanelHeight
        {
            get
            {
                return topPanelHeight;
            }
            set
            {
                if (originalTopPanelHeight == 0)
                {
                    originalTopPanelHeight = value;
                    topPanelHeight = value;
                }
            }
        }

        [Parameter]
        public int MinimumTopPanelHeight { get; set; } = 200;

        [Parameter]
        public int MinimumBottomPanelHeight { get; set; } = 200;

        protected string TopPanelHeightPx { get { return topPanelHeight.ToString() + "px"; } }
        protected string BottomPanelHeightPx { get { return BottomPanelHeight.ToString() + "px"; } }
        protected string BottomPanelTopPx { get { return (topPanelHeight + SliderHeight).ToString() + "px"; } }
        protected string SliderHeightPx { get { return SliderHeight.ToString() + "px"; } }

        public int BottomPanelHeight
        {
            get
            {
                if (Parent != null)
                    return Parent.Height - (TopPanelHeight + SliderHeight);
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
            Resize(Y);
            ParentResized();
            await InvokeAsync(StateHasChanged);
        }



        protected override async Task OnAfterRenderAsync(bool FirstRender)
        {
            var myObject = DotNetObjectReference.Create(this);

            if (FirstRender)
            {
                await jsInterop.RegisterHorizontalSliderPanel(SliderId, TopPanelId, BottomPanelId, myObject);
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
