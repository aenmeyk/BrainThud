var modules = [];
var define = function (name, deps, callback) {
    modules[name] = callback;
};
