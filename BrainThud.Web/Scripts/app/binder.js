define('binder', ['jquery', 'ko', 'vm'],

function ($, ko, vm) {
    var
        bind = function () {
            ko.applyBindings(vm.card, getView('createCard-view'));
//            ko.applyBindings(vm.cards, getView('questions-view'));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});