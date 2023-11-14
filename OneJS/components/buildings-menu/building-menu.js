"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BuildingMenu = void 0;
var preact_1 = require("preact");
var menu_item_1 = require("./menu-item");
var placementSystem = require("placementSystem");
var wireSystem = require("wireSystem");
var BuildingMenu = function () {
    var menuData = [
        ["Fio", function () { return wireSystem.EnterWireMode(); }],
        ["LED", function () { return placementSystem.StartPlacement(1); }],
        ["Switch", function () { return placementSystem.StartPlacement(2); }],
        ["Inversor", function () { return placementSystem.StartPlacement(3); }],
        ["Remover", function () { return placementSystem.StartRemoving(); }],
    ];
    return ((0, preact_1.h)("nav", { className: "font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg" }, menuData.map(function (_a) {
        var name = _a[0], onClick = _a[1];
        return (0, preact_1.h)(menu_item_1.MenuItem, { name: name, onClick: onClick });
    })));
};
exports.BuildingMenu = BuildingMenu;
