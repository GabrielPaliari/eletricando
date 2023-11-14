"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SignalsSpec = void 0;
var preact_1 = require("preact");
var SignalsSpec = function (_a) {
    var inputs = _a.inputs, outputs = _a.outputs;
    return ((0, preact_1.h)("div", { class: "" },
        (0, preact_1.h)("h2", { class: "text-lg" }, "Entradas"),
        (0, preact_1.h)("div", null, inputs.map(function (inputArray) { return ((0, preact_1.h)(BitArrayDisplay, { values: inputArray })); })),
        (0, preact_1.h)("h2", { class: "text-lg" }, "Sa\u00EDdas"),
        (0, preact_1.h)("div", null, outputs.map(function (inputArray) { return ((0, preact_1.h)(BitArrayDisplay, { values: inputArray })); }))));
};
exports.SignalsSpec = SignalsSpec;
var BitArrayDisplay = function (_a) {
    var values = _a.values;
    return ((0, preact_1.h)("div", { class: "flex" }, values.map(function (value) { return ((0, preact_1.h)(Bit, { value: value })); })));
};
var Bit = function (_a) {
    var value = _a.value;
    return (0, preact_1.h)("div", null, value);
};
