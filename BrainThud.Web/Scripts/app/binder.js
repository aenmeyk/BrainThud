define('binder', ['jquery', 'ko', 'vm.card'],

function ($, ko, card) {
    var
        bind = function () {
            ko.applyBindings(card, getView('questions-view'));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});