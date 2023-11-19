"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MenuItem = void 0;
var preact_1 = require("preact");
var MenuItem = function (props) {
    var defaultInnerStyle = {
        borderRadius: 4,
        height: 40,
        backgroundImage: props.image,
        unityBackgroundScaleMode: "ScaleToFit",
    };
    var borderStyle = {
        borderColor: props.isSelected ? "white" : "transparent",
        borderWidth: 4,
    };
    return ((0, preact_1.h)("div", { style: borderStyle, class: "rounded bg-teal-700 m-1 p-2", onClick: props.onClick },
        (0, preact_1.h)("div", { style: defaultInnerStyle })));
};
exports.MenuItem = MenuItem;
