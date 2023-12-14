"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BuildingMenu = void 0;
var preact_1 = require("preact");
var menu_item_1 = require("./menu-item");
var hooks_1 = require("preact/hooks");
var tooltip_1 = require("components/shared/tooltip");
var placementSystem = require("placementSystem");
var levelManager = require("levelManager");
var wireSystem = require("wireSystem");
var getComponentFunction = function (id, setSelected) {
    var componentFunction;
    switch (id) {
        case -1:
            componentFunction = function () { return placementSystem.StartRemoving(); };
            break;
        case 0:
            componentFunction = function () { return wireSystem.EnterWireMode(); };
            break;
        default:
            componentFunction = function () { return placementSystem.StartPlacement(id); };
            break;
    }
    return function () {
        setSelected(id);
        componentFunction();
    };
};
var BuildingMenu = function () {
    var _a = (0, hooks_1.useState)(-100), selected = _a[0], setSelected = _a[1];
    var componentsData = [];
    var componentsAvailable = levelManager._selectedLevel.componentsAvailable.objectsData;
    componentsAvailable.ForEach(function (comp) {
        componentsData.push([
            comp.ID,
            comp.Name,
            getComponentFunction(comp.ID, setSelected),
            comp.menuImage,
        ]);
    });
    var menuData = componentsData;
    return ((0, preact_1.h)("div", { class: "bg-indigo-800 rounded-lg w-20" }, menuData.map(function (_a) {
        var id = _a[0], name = _a[1], onClick = _a[2], image = _a[3];
        return ((0, preact_1.h)(tooltip_1.Tooltip, { content: name },
            (0, preact_1.h)(menu_item_1.MenuItem, { name: name, onClick: onClick, image: image, isSelected: selected === id })));
    })));
};
exports.BuildingMenu = BuildingMenu;
