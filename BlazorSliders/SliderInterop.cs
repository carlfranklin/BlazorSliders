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

        public async Task RegisterWindow(DotNetObjectReference<Window> Component, string Id, bool ParentContained)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("registerWindow", Component, Id, ParentContained);
        }

        public async Task ForceResize(DotNetObjectReference<Window> Component, string Id, bool ParentContained)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("forceResize", Component, Id, ParentContained);
        }

        public async Task GetVerticalParentDimensions(string Id, DotNetObjectReference<VerticalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentDimensions", Id, Component);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task GetHorizontalParentDimensions(string Id, DotNetObjectReference<HorizontalSliderPanel> Component)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeAsync<string>("getParentDimensions", Id, Component);
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
