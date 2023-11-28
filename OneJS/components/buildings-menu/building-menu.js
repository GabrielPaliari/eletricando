"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BuildingMenu = void 0;
var preact_1 = require("preact");
var menu_item_1 = require("./menu-item");
var hooks_1 = require("preact/hooks");
var tooltip_1 = require("components/shared/tooltip");
var placementSystem = require("placementSystem");
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
    var _a;
    var _b = (0, hooks_1.useState)(-100), selected = _b[0], setSelected = _b[1];
    var componentsDatabase = (_a = placementSystem.database) === null || _a === void 0 ? void 0 : _a.objectsData;
    var componentsData = [];
    componentsDatabase.ForEach(function (_a) {
        var Name = _a.Name, ID = _a.ID, menuImage = _a.menuImage;
        return componentsData.push([
            ID,
            Name,
            getComponentFunction(ID, setSelected),
            menuImage,
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
