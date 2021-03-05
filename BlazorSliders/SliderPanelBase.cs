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

        /// <summary>
        /// This is called when 
        /// a) The window itself (browser) is resized, and
        /// b) The slider is moved, in which case NewSliderValue will contain the new X or Y
        /// </summary>
        /// <param name="NewSliderValue"></param>
        public void Resize(int NewSliderValue = 0)
        {
            if (this.GetType() == typeof(VerticalSliderPanel))
            {
                var me = (VerticalSliderPanel)this;
                int SliderX = 0;

                // Has someone moved the slider?
                if (NewSliderValue > 0)
                {
                    // Yes! The new slider value becomes our new LEFT panel width
                    SubtractLeft = 0;
                    GetSubtractLeft(this);
                    
                    SliderX = NewSliderValue - SubtractLeft;
                }
         
                else
                    SliderX = me.leftPanelWidth;

                // Ensure that we are not going under the minimum width
                if (Parent != null)
                {
                    int minimumSliderX = 0;
                    if (Parent.Width > me.MinimumRightPanelWidth)
                        minimumSliderX = Parent.Width - me.MinimumRightPanelWidth;
                    else
                        minimumSliderX = me.MinimumRightPanelWidth;

                    if (minimumSliderX != 0 && SliderX > minimumSliderX)
                        SliderX = minimumSliderX;
                }

                // Now set my overall width and height
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

                if (SliderX < me.MinimumLeftPanelWidth)
                    SliderX = me.MinimumLeftPanelWidth;

                // Set the slider position (left panel width)
                me.leftPanelWidth = SliderX;

                if (me.LeftPanel != null)
                {
                    me.LeftPanel.Resize();
                }
                if (me.RightPanel != null)
                {
                    me.RightPanel.Resize();
                }
            }
            else if (this.GetType() == typeof(HorizontalSliderPanel))
            {
                var me = (HorizontalSliderPanel)this;
                int SliderY = 0;
                SubtractTop = 0;
                GetSubtractTop(me);

                // Has someone moved the slider?
                if (NewSliderValue > 0)
                {
                    // Yes! The new slider value becomes our new TOP panel height
                    SliderY = NewSliderValue - SubtractTop;
                }
                else
                    SliderY = me.topPanelHeight;

                // Ensure that we are not going under the minimum width
                if (Parent != null)
                {
                    int minimumSliderY = 0;
                    if (Parent.Height > me.MinimumBottomPanelHeight)
                        minimumSliderY = Parent.Height - me.MinimumBottomPanelHeight;
                    else
                        minimumSliderY = me.MinimumBottomPanelHeight;

                    if (minimumSliderY != 0 && SliderY > minimumSliderY)
                        SliderY = minimumSliderY;
                }

                if (SliderY < me.MinimumTopPanelHeight)
                    SliderY = me.MinimumTopPanelHeight;

                // Set the slider position (top panel height)
                me.topPanelHeight = SliderY;

                // Now set my overall width and height
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
                    me.TopPanel.Resize();
                }
                if (me.BottomPanel != null)
                {
                    me.BottomPanel.Resize();
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
