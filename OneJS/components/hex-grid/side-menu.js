"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var __rest = (this && this.__rest) || function (s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.Img = exports.LevelDetailsPane = void 0;
var preact_1 = require("preact");
var UnityEngine_1 = require("UnityEngine");
var signals_spec_1 = require("./signals-spec");
var LevelDetailsPane = function (_a) {
    var id = _a.id, title = _a.title, description = _a.description, componentImg = _a.componentImg, signals = __rest(_a, ["id", "title", "description", "componentImg"]);
    return ((0, preact_1.h)("div", { class: "rounded-l-lg bg-teal-700 mt-3 p-4 max-w-sm ml-auto" },
        (0, preact_1.h)("h1", { class: "text-2xl" }, title),
        (0, preact_1.h)("div", { class: "w-20" },
            (0, preact_1.h)(exports.Img, { fileUrl: componentImg })),
        (0, preact_1.h)("p", { class: "text-base" }, description),
        (0, preact_1.h)(signals_spec_1.SignalsSpec, __assign({}, signals)),
        (0, preact_1.h)("div", { class: "rounded-lg bg-indigo-500", onClick: function () { return UnityEngine_1.Debug.Log("Jogar level" + id); } },
            (0, preact_1.h)("p", { class: "text-lg text-center" }, "Jogar Level"))));
};
exports.LevelDetailsPane = LevelDetailsPane;
var Img = function (_a) {
    var fileUrl = _a.fileUrl;
    var src = __dirname + "../../../assets/" + fileUrl;
    var defaultInnerStyle = {
        width: "100%",
        paddingBottom: "100%",
        backgroundImage: src,
        unityBackgroundScaleMode: "ScaleAndCrop",
    };
    return (0, preact_1.h)("div", { style: defaultInnerStyle });
};
exports.Img = Img;
