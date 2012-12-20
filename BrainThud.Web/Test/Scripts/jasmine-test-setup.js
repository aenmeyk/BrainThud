/// <reference path="../../Scripts/jasmine/jasmine.js" />

var modules = [];
var define = function (name, deps, callback) {
    modules[name] = callback;
};
