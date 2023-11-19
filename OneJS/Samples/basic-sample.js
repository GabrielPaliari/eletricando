"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.App = void 0;
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var App = function () {
    var ref = (0, hooks_1.useRef)();
    return ((0, preact_1.h)("div", { ref: ref },
        (0, preact_1.h)("label", { text: "Select something to remove from your suitcase:" }),
        (0, preact_1.h)("box", null,
            (0, preact_1.h)("toggle", { name: "boots", label: "Boots", value: true }),
            (0, preact_1.h)("toggle", { name: "helmet", label: "Helmet", value: false }),
            (0, preact_1.h)("toggle", { name: "cloak", label: "Cloak of invisibility" })),
        (0, preact_1.h)("radiobuttongroup", null,
            (0, preact_1.h)("radiobutton", { name: "sword", label: "Sword", value: true }),
            (0, preact_1.h)("radiobutton", { name: "bow", label: "Bow", value: false }),
            (0, preact_1.h)("radiobutton", { name: "axe", label: "Axe", value: false })),
        (0, preact_1.h)("box", null,
            (0, preact_1.h)("button", { name: "cancel", text: "Cancel", onClick: function (e) {
                    log("Foo");
                    e.currentTarget.Blur();
                } }),
            (0, preact_1.h)("button", { name: "ok", text: "OK" }),
            (0, preact_1.h)("textfield", { onInput: function (e) { return log(e.newData); }, onKeyDown: function (e) { return log(e.keyCode); } }))));
};
exports.App = App;
