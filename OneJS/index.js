"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var building_menu_1 = require("components/buildings-menu/building-menu");
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var App = function () {
    var ref = (0, hooks_1.useRef)();
    return ((0, preact_1.h)("div", { ref: ref, class: "container" },
        (0, preact_1.h)(building_menu_1.BuildingMenu, null)));
};
(0, preact_1.render)((0, preact_1.h)(App, null), document.body);
