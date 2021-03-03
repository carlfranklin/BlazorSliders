using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorSliders
{
    public class SliderPanelBase : ComponentBase
    {
        [CascadingParameter]
        public SliderPanelBase Parent { get; set; }

        [Parameter]
        public PanelPosition PanelPosition { get; set; }

        protected int SubtractLeft = 0;
        protected int SubtractTop = 0;
        private Dimensions dimensions = new Dimensions();
        private int OriginalWidth;
        private int OriginalHeight;

        protected void GetSubtractLeft(SliderPanelBase panel)
        {
            if (panel == null) return;
            if (panel.Parent != null)
            {
                if (panel.Parent.GetType() == typeof(AbsolutePanel))
                {
                    SubtractLeft += panel.Parent.Left;
                }
                else if (panel.Parent.GetType() == typeof(VerticalSliderPanel))
                {
                    var parent = (VerticalSliderPanel)panel.Parent;
                    if (panel.PanelPosition == PanelPosition.Right)
                        SubtractLeft += parent.LeftPanelWidth + parent.SliderWidth;
                }
                GetSubtractLeft(panel.Parent);
            }
        }

        protected void GetSubtractTop(SliderPanelBase panel)
        {
            if (panel == null) return;
            if (panel.Parent != null)
            {
                if (panel.Parent.GetType() == typeof(AbsolutePanel))
                {
                    SubtractTop += panel.Parent.Top;
                }
                else if (panel.Parent.GetType() == typeof(HorizontalSliderPanel))
                {
                    var parent = (HorizontalSliderPanel)panel.Parent;
                    if (panel.PanelPosition == PanelPosition.Bottom)
                        SubtractTop += parent.TopPanelHeight + parent.SliderHeight;
                }
                GetSubtractTop(panel.Parent);
            }
        }

        public void Resize(int newValue)
        {
            if (this.GetType() == typeof(VerticalSliderPanel))
            {
                var me = (VerticalSliderPanel)this;
                SubtractLeft = 0;
                GetSubtractLeft(this);

                int NewWidth = newValue - SubtractLeft;

                if (NewWidth < me.MinimumLeftPanelWidth)
                    NewWidth = me.MinimumLeftPanelWidth;
                else if (Parent != null && NewWidth > Parent.Width - me.MinimumRightPanelWidth)
                    NewWidth = Parent.Width - me.MinimumRightPanelWidth;

                me.leftPanelWidth = NewWidth;
            }
            else if (this.GetType() == typeof(HorizontalSliderPanel))
            {
                var me = (HorizontalSliderPanel)this;
                
                SubtractTop = 0;
                GetSubtractTop(me);

                int NewHeight = newValue - SubtractTop;

                if (NewHeight < me.MinimumTopPanelHeight)
                    NewHeight = me.MinimumTopPanelHeight;
                else if (Parent != null && NewHeight > Parent.Height - me.MinimumBottomPanelHeight)
                    NewHeight = Parent.Height - me.MinimumBottomPanelHeight;
                me.topPanelHeight = NewHeight;
            }

        }

        public void ParentResized()
        {
            if (this.GetType() == typeof(VerticalSliderPanel))
            {
                var me = (VerticalSliderPanel)this;
                if (PanelPosition == PanelPosition.Top)
                {
                    // I'm a vertical slider inside the top pane of a horizontal slider
                    var parent = (HorizontalSliderPanel)Parent;
                    Height = parent.TopPanelHeight;
                    Width = parent.Width;
                }
                else if (PanelPosition == PanelPosition.Bottom)
                {
                    // I'm a vertical slider inside the left pane of a horizontal slider
                    var parent = (HorizontalSliderPanel)Parent;
                    Height = Parent.Height - (parent.TopPanelHeight + parent.SliderHeight);
                    Width = parent.Width;
                }
                else
                {
                    Height = Parent.Height;
                    Width = Parent.Width;
                }

                if (me.LeftPanel != null)
                {
                    me.LeftPanel.ParentResized();
                }
                if (me.RightPanel != null)
                {
                    me.RightPanel.ParentResized();
                }
            }
            else if (this.GetType() == typeof(HorizontalSliderPanel))
            {
                var me = (HorizontalSliderPanel)this;

                if (PanelPosition == PanelPosition.Left)
                {
                    // I'm a horizontal slider inside the left pane of a vertical slider
                    var parent = (VerticalSliderPanel)Parent;
                    Width = parent.LeftPanelWidth;
                    Height = Parent.Height;
                }
                else if (PanelPosition == PanelPosition.Right)
                {
                    // I'm a horizontal slider inside the right pane of a vertical slider
                    var parent = (VerticalSliderPanel)Parent;
                    Width = Parent.Width - (parent.LeftPanelWidth + parent.SliderWidth);
                    Height = Parent.Height;
                }
                else
                {
                    Height = Parent.Height;
                    Width = Parent.Width;
                }

                if (me.TopPanel != null)
                {
                    me.TopPanel.ParentResized();
                }
                if (me.BottomPanel != null)
                {
                    me.BottomPanel.ParentResized();
                }
            }
        }

        public int Top
        {
            get
            {
                return dimensions.Top;
            }
            set
            {
                dimensions.Top = value;
            }
        }

        public int Left
        {
            get
            {
                return dimensions.Left;
            }
            set
            {
                dimensions.Left = value;
            }
        }

        public int Width
        {
            get
            {
                if (dimensions.Width == 0 && Parent != null)
                    dimensions.Width = Parent.Width;
                return dimensions.Width;
            }
            set
            {
                if (Parent != null && Parent.Width > 0)
                {
                    var Diff = Parent.Width - value;
                    dimensions.Width = value;
                    if (Parent.Left > 0)
                        dimensions.Left += Diff;
                    StateHasChanged();
                }
                else
                {
                    dimensions.Width = value;
                }

                //dimensions.Width = value;
                if (OriginalWidth == 0)
                    OriginalWidth = value;
            }
        }

        public int Height
        {
            get
            {
                if (dimensions.Height == 0 && Parent != null)
                    dimensions.Height = Parent.Height;
                return dimensions.Height;
            }
            set
            {
                dimensions.Height = value;
                if (OriginalHeight == 0)
                    OriginalHeight = value;
            }
        }

        public string TopPx
        {
            get
            {
                return dimensions.Top.ToString() + "px";
            }
        }

        public string LeftPx
        {
            get
            {
                return dimensions.Left.ToString() + "px";
            }
        }

        public string WidthPx
        {
            get
            {
                return dimensions.Width.ToString() + "px";
            }
        }

        public string HeightPx
        {
            get
            {
                return dimensions.Height.ToString() + "px";
            }
        }

        string id = "";
        [Parameter]
        public string Id
        {
            get
            {
                if (id == "") id = NewGuid();
                return id;
            }
            set
            {
                id = value;
            }
        }

        string sliderId = "";
        public string SliderId
        {
            get
            {
                if (sliderId == "")
                {
                    sliderId = NewGuid();
                }
                return sliderId;
            }
        }

        protected string NewGuid()
        {
            var obj = new object();
            int seed = obj.GetHashCode();
            var rnd = new Random(seed);
            var bytes = new byte[16];
            rnd.NextBytes(bytes);
            var guid = new Guid(bytes);
            return guid.ToString();
        }
    }
}
