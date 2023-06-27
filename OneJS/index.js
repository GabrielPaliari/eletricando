"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var preact_1 = require("preact");
var hooks_1 = require("preact/hooks");
var App = function () {
    var ref = (0, hooks_1.useRef)();
    return ((0, preact_1.h)("div", { ref: ref },
        (0, preact_1.h)("div", { class: "max-w-xl h-full font-sans bg-indigo-600" },
            (0, preact_1.h)("nav", { className: "flex flex-row" }, [
                ["Teste", "/dashboard"],
                ["Team", "/team"],
                ["Projects", "/projects"],
                ["", ""],
                ["Reports", "/reports"],
            ].map(function (_a) {
                var title = _a[0], url = _a[1];
                return title ? ((0, preact_1.h)("div", { class: "basis-1/5 rounded bg-white text-lg p-3 m-3" },
                    (0, preact_1.h)("img", { src: __dirname + "/assets/battery-sprite.png" }))) : ((0, preact_1.h)("div", { class: "basis-1/5" }));
            })))));
};
(0, preact_1.render)((0, preact_1.h)(App, null), document.body);
