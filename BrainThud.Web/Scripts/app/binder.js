define('binder', ['jquery', 'ko', 'vm'],

function ($, ko, vm) {
    var
        bind = function () {
            ko.applyBindings(vm.card, getView('#create-card-view'));
            ko.applyBindings(vm.cards, getView('#todays-cards-view'));
            ko.applyBindings(vm.cards, getView('#quiz-view'));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});