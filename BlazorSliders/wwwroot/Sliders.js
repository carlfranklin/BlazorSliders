// Slider.js by Carl Franklin
// Version 1.2.0

export function registerWindow(dotNetComponent) {
    var component = dotNetComponent;

    window.addEventListener("resize", function () {
        if (component != null)
            setTimeout(raiseEvent, 1, component, "OnWindowResized", window.innerWidth, window.innerHeight);
    });

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }
}

export function forceResize(dotNetComponent) {
    dotNetComponent.invokeMethodAsync("OnWindowResized", window.innerWidth, window.innerHeight)
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
        leftPanel.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (rightPanel != null) {
        // mouse
        rightPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        rightPanel.addEventListener("touchend", function (ev) {
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
        slider.addEventListener("touchstart", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                leftPanel.style.cursor = "e-resize";
                rightPanel.style.cursor = "e-resize";
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchend", function (ev) {
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
        topPanel.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
        });
    }

    if (bottomPanel != null) {
        // mouse
        bottomPanel.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        bottomPanel.addEventListener("touchend", function (ev) {
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
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";
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
        slider.addEventListener("touchstart", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                topPanel.style.cursor = "n-resize";
                bottomPanel.style.cursor = "n-resize";
                setTimeout(raiseEvent, 1, component, "MouseDown", ev.clientX, ev.clientY);
            }
        });

        slider.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";
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