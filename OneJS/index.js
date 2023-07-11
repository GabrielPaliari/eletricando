"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var placementSystem = require("placementSystem");
var wireSystem = require("wireSystem");
var MenuComp = function (props) {
    var defaultInnerStyle = {
        borderRadius: 4,
        paddingBottom: "100%",
        backgroundImage: __dirname + "/assets/" + props.imgFile,
        justifyContent: "Center",
        alignItems: "Center",
        color: "white",
    };
    return ((0, preact_1.h)("div", { class: "rounded bg-white m-3 w-16", onClick: props.onClick },
        (0, preact_1.h)("div", { style: defaultInnerStyle })));
};
var App = function () {
    var ref = (0, hooks_1.useRef)();
    var navStyle = {
        borderRadius: 8,
    };
    var menuData = [
        ["wire-sprite.png", function () { return wireSystem.EnterWireMode(); }],
        ["battery-sprite.png", function () { return placementSystem.StartPlacement(3); }],
        ["resistor-sprite.png", function () { return placementSystem.StartPlacement(1); }],
        ["remove-icon.png", function () { return placementSystem.StartRemoving(); }],
    ];
    return ((0, preact_1.h)("div", { ref: ref, class: "container mt-auto w-screen" },
        (0, preact_1.h)("nav", { className: "font-sans bg-indigo-800 flex flex-row mx-auto rounded-lg" }, menuData.map(function (_a) {
            var imgFile = _a[0], onClick = _a[1];
            return ((0, preact_1.h)(MenuComp, { imgFile: imgFile, length: menuData.length, onClick: onClick }));
        }))));
};
(0, preact_1.render)((0, preact_1.h)(App, null), document.body);
