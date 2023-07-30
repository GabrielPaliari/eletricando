"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var placementSystem = require("placementSystem");
var wireSystem = require("wireSystem");
var MenuComp = function (props) {
    var defaultInnerStyle = {
        borderRadius: 4,
        justifyContent: "Center",
        alignItems: "Center",
        color: "white",
        padding: 20,
        fontSize: 20,
    };
    return ((0, preact_1.h)("div", { class: "rounded bg-teal-700 m-3", onClick: props.onClick },
        (0, preact_1.h)("div", { style: defaultInnerStyle }, props.name)));
};
var App = function () {
    var ref = (0, hooks_1.useRef)();
    var navStyle = {
        borderRadius: 8,
    };
    var menuData = [
        ["Fio", function () { return wireSystem.EnterWireMode(); }],
        ["LED", function () { return placementSystem.StartPlacement(1); }],
        ["Switch", function () { return placementSystem.StartPlacement(2); }],
        ["Remover", function () { return placementSystem.StartRemoving(); }],
    ];
    return ((0, preact_1.h)("div", { ref: ref, class: "container mt-auto w-screen" },
        (0, preact_1.h)("nav", { className: "font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg" }, menuData.map(function (_a) {
            var name = _a[0], onClick = _a[1];
            return (0, preact_1.h)(MenuComp, { name: name, onClick: onClick });
        }))));
};
(0, preact_1.render)((0, preact_1.h)(App, null), document.body);
