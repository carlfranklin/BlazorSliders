// Slider.js by Carl Franklin
// Version 1.2.8

export function registerWindow(dotNetComponent, id = "", parentContained = false) {
    var component = dotNetComponent;

    window.addEventListener("resize", function () {
        if (component != null) {
            if (parentContained) {

                var parentContainer = window.document.getElementById(id).parentElement;
                var parentCalc = window.getComputedStyle(parentContainer);
                var parentWidth = parseInt(parentCalc.width);
                var parentHeight = parseInt(parentCalc.height);
                setTimeout(raiseEvent, 1, component, "OnWindowResized", parentWidth, parentHeight);
            } else {
                setTimeout(raiseEvent, 1, component, "OnWindowResized", window.innerWidth, window.innerHeight);
            }
        }
            
    });

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }
}

export function forceResize(dotNetComponent, id = "", parentContained = false) {
    var component = dotNetComponent;

    if (parentContained) {
        var doc = window.document;
        var parentContainer = doc.getElementById(id).parentElement;
        var parentCalc = window.getComputedStyle(parentContainer);
        var parentWidth = parseInt(parentCalc.width);
        var parentHeight = parseInt(parentCalc.height);

        component.invokeMethodAsync("OnWindowResized", parentWidth, parentHeight)
    } else
        component.invokeMethodAsync("OnWindowResized", window.innerWidth, window.innerHeight)
 


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
                var absoluteElement = ev.target;

                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                leftPanel.style.cursor = "e-resize";
                rightPanel.style.cursor = "e-resize";
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);
                setTimeout(raiseEvent, 1, component, "MouseDown", relativeX, relativeY);
            }
        });

        slider.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                leftPanel.style.cursor = "default";
                rightPanel.style.cursor = "default";
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);
                setTimeout(raiseEvent, 1, component, "MouseUp", relativeX, relativeY);
            }
        });

        slider.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving) {
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);
                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }

                
        });

        function getAbsoluteParent(element) {
            if (element.classList.contains("AbsolutePanel"))
                return element;
            else
                return element.parentElement;
        }

        window.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving) {
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);
                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }

        });

        window.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        slider.addEventListener("touchstart", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                leftPanel.style.cursor = "e-resize";
                rightPanel.style.cursor = "e-resize";
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;


                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY) - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseDown", relativeX, relativeY);
            }
        });

        slider.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                leftPanel.style.cursor = "default";
                rightPanel.style.cursor = "default";

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                setTimeout(raiseEvent, 1, component, "MouseUp", parseInt(rect.left), parseInt(rect.top));
            }
        });

        slider.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving) {

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;


                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY) - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
        });

        window.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving) {

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;

                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY)- parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
        });

        window.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
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

                var absoluteElement = ev.target;

                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseDown", relativeX, relativeY);
            }
        });

        slider.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";

                var absoluteElement = ev.target;

                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseUp", relativeX, relativeY);
            }
        });

        slider.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving) {
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
                
        });

        window.addEventListener("mousemove", function (ev) {
            if (component != null && sliderIsMoving) {
                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();
                var relativeX = ev.clientX - parseInt(rect.left);
                var relativeY = ev.clientY - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
                
        });

        window.addEventListener("mouseup", function (ev) {
            sliderIsMoving = false;
        });

        // touch
        slider.addEventListener("touchstart", function (ev) {
            sliderIsMoving = true;
            if (component != null) {
                topPanel.style.cursor = "n-resize";
                bottomPanel.style.cursor = "n-resize";

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;


                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY) - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseDown", relativeX, relativeY);
            }
        });

        slider.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
            if (component != null) {
                topPanel.style.cursor = "default";
                bottomPanel.style.cursor = "default";

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                setTimeout(raiseEvent, 1, component, "MouseUp", parseInt(rect.left), parseInt(rect.top));
            }
        });

        slider.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving) {

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;


                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY) - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
        });

        window.addEventListener("touchmove", function (ev) {
            if (component != null && sliderIsMoving) {

                var absoluteElement = ev.target;
                while (!absoluteElement.classList.contains("AbsolutePanel")) {
                    absoluteElement = getAbsoluteParent(absoluteElement);
                }
                var rect = absoluteElement.getBoundingClientRect();

                var clientX = ev.touches[0].clientX;
                var clientY = ev.touches[0].clientY;


                var relativeX = parseInt(clientX) - parseInt(rect.left);
                var relativeY = parseInt(clientY) - parseInt(rect.top);

                setTimeout(raiseEvent, 1, component, "MouseMove", relativeX, relativeY);
            }
        });

        window.addEventListener("touchend", function (ev) {
            sliderIsMoving = false;
        });

    }

    function raiseEvent(comp, eventname, x, y) {
        comp.invokeMethodAsync(eventname, x, y);
    }

    function getAbsoluteParent(element) {
        if (element.classList.contains("AbsolutePanel"))
            return element;
        else
            return element.parentElement;
    }
}