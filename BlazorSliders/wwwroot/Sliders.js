export function registerWindow(dotNetComponent) {
    var component = dotNetComponent;

    window.addEventListener("resize", function () {
        if (component != null)
            //component.invokeMethodAsync("OnWindowResized", window.innerWidth, window.innerHeight)
            setTimeout(raiseEvent, 1, component, "OnWindowResized", window.innerWidth, window.innerHeight);
    });

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }
}

export function forceResize(dotNetComponent) {
    dotNetComponent.invokeMethodAsync("OnWindowResized", window.innerWidth, window.innerHeight)
}

export function getParentWidth(Id, dotnetComponent) {
    var component = dotnetComponent;
    setTimeout(getWidth, 10, Id);
    function getWidth(Id) {
        var element = document.getElementById(Id);
        if (element == null)
            setTimeout(getWidth, 10, Id);
        else {
            component.invokeMethodAsync("ParentWidthChanged", element.style.width);
        }
    }
}
export function getParentHeight(Id, dotnetComponent) {
    var component = dotnetComponent;
    setTimeout(getHeight, 10, Id);
    function getHeight(Id) {
        var element = document.getElementById(Id);
        if (element == null)
            setTimeout(getHeight, 10, Id);
        else {
            component.invokeMethodAsync("ParentHeightChanged", element.style.height);
        }
    }
}

export function getParentLeft(Id, dotnetComponent) {
    var component = dotnetComponent;
    setTimeout(getLeft, 10, Id);
    function getLeft(Id) {
        var element = document.getElementById(Id);
        if (element == null)
            setTimeout(getLeft, 10, Id);
        else {
            component.invokeMethodAsync("ParentLeftChanged", element.style.left);
        }
    }
}

export function getParentTop(Id, dotnetComponent) {
    var component = dotnetComponent;
    setTimeout(getTop, 10, Id);
    function getTop(Id) {
        var element = document.getElementById(Id);
        if (element == null)
            setTimeout(getTop, 10, Id);
        else {
            component.invokeMethodAsync("ParentTopChanged", element.style.top);
        }
    }
}

export function registerVerticalSliderPanel(SliderId, LeftPanelId, RightPanelId, dotNetComponent) {
    var component = dotNetComponent;
    var sliderIsMoving = false;
    var leftPanel = document.getElementById(LeftPanelId);
    var rightPanel = document.getElementById(RightPanelId);
    var slider = document.getElementById(SliderId)

    if (leftPanel != null) {
        // mouse
        leftPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        leftPanel.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (rightPanel != null) {
        // mouse
        rightPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        rightPanel.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (slider != null) {

        // mouse
        slider.addEventListener("mousedown", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                leftPanel.style.cursor = "e-resize";
                rightPanel.style.cursor = "e-resize";
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                leftPanel.style.cursor = "default";
                rightPanel.style.cursor = "default";
                setTimeout(raiseEvent, 1, component, "MouseUp", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

        window.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);

        });

        // touch
        slider.addEventListener("touchdown", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                leftPanel.style.cursor = "e-resize";
                rightPanel.style.cursor = "e-resize";
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                leftPanel.style.cursor = "default";
                rightPanel.style.cursor = "default";
                setTimeout(raiseEvent, 1, component, "MouseUp", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

        window.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });
    }

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }
}

export function registerHorizontalSliderPanel(SliderId, TopPanelId, BottomPanelId, dotNetComponent) {
    var component = dotNetComponent;
    var sliderIsMoving = false;
    var topPanel = document.getElementById(TopPanelId);
    var bottomPanel = document.getElementById(BottomPanelId);
    var slider = document.getElementById(SliderId)

    if (topPanel != null) {
        // mouse
        topPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        topPanel.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (bottomPanel != null) {
        // mouse
        bottomPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        bottomPanel.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (slider != null) {

        // mouse
        slider.addEventListener("mousedown", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                topPanel.style.cursor = "n-resize";
                bottomPanel.style.cursor = "n-resize";
                //component.invokeMethodAsync("MouseDown", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";
                //component.invokeMethodAsync("MouseUp", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseUp", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving)
                //component.invokeMethodAsync("MouseMove", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

        window.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving)
                //component.invokeMethodAsync("MouseMove", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

        // touch
        slider.addEventListener("touchdown", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                topPanel.style.cursor = "n-resize";
                bottomPanel.style.cursor = "n-resize";
                //component.invokeMethodAsync("MouseDown", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";
                //component.invokeMethodAsync("MouseUp", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseUp", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving)
                //component.invokeMethodAsync("MouseMove", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

        window.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving)
                //component.invokeMethodAsync("MouseMove", ev.clientX, ev.clientY)
                setTimeout(raiseEvent, 1, component, "MouseMove", ev.clientX, ev.clientY);
        });

    }

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }
}