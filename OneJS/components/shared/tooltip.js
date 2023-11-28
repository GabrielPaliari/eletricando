"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Tooltip = void 0;
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var Tooltip = function (_a) {
    var children = _a.children, content = _a.content;
    var _b = (0, hooks_1.useState)(false), isVisible = _b[0], setVisible = _b[1];
    return ((0, preact_1.h)(preact_1.Fragment, null,
        (0, preact_1.h)("div", { onMouseEnter: function () { return setVisible(true); }, onMouseLeave: function () { return setVisible(false); } },
            children,
            isVisible && ((0, preact_1.h)("span", { style: {
                    left: "100%",
                    top: "50%",
                    translate: "10% -50%",
                }, class: "bg-gray-800 text-white p-2 rounded absolute" }, content)))));
};
exports.Tooltip = Tooltip;
