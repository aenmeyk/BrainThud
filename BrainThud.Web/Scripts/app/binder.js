define('binder', ['jquery', 'ko', 'vm.nugget'],

function ($, ko, nugget) {
    var
        bind = function () {
            ko.applyBindings(nugget, getView('questions-view'));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});