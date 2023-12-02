"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var building_menu_1 = require("components/buildings-menu/building-menu");
var preact_1 = require("preact");
var App = function () {
    return (0, preact_1.h)(building_menu_1.BuildingMenu, null);
};
(0, preact_1.render)((0, preact_1.h)(App, null), document.body);
