"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MenuItem = void 0;
var preact_1 = require("preact");
var MenuItem = function (props) {
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
exports.MenuItem = MenuItem;
