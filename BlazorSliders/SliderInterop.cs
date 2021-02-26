using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSliders
{
    public class SliderInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public SliderInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorSliders/Sliders.js").AsTask());
        }

        public async Task RegisterWindow(DotNetObjectReference<Window> Component)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("registerWindow", Component);
        }

        public async Task ForceResize(DotNetObjectReference<Window> Component)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("forceResize", Component);
        }

        public async Task GetVerticalParentWidth(string Id, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentWidth", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetVerticalParentHeight(string Id, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentHeight", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetVerticalParentTop(string Id, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentTop", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetVerticalParentLeft(string Id, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentLeft", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetHorizontalParentWidth(string Id, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentWidth", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetHorizontalParentHeight(string Id, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentHeight", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetHorizontalParentTop(string Id, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentTop", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetHorizontalParentLeft(string Id, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentLeft", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task RegisterVerticalSliderPanel(string SliderId, string LeftPanelId, string RightPanelId, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("registerVerticalSliderPanel", SliderId, LeftPanelId, RightPanelId, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task RegisterHorizontalSliderPanel(string SliderId, string TopPanelId, string BottomPanelId, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("registerHorizontalSliderPanel", SliderId, TopPanelId, BottomPanelId, Component);
        }

        //public async Task ResizeElementAbsolute(string Id, int Top, int Left, int Width, int Height)
        //{
        //    var module = await moduleTask.Value;
        //    await module.InvokeVoidAsync("resizeElementAbsolute", Id, Top, Left, Width, Height);
        //}

        public async ValueTask DisposeAsync()
        {
            if (moduleTask != null && moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
