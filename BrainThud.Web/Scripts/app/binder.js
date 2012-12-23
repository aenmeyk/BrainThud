define('binder', ['jquery', 'ko', 'vm', 'config'],

function ($, ko, vm, config) {
    var
        bind = function () {
            ko.applyBindings(vm.card, getView(config.viewIds.card));
            ko.applyBindings(vm.createCard, getView(config.viewIds.createCard));
            ko.applyBindings(vm.cards, getView(config.viewIds.cards));
            ko.applyBindings(vm.quiz, getView(config.viewIds.quiz));
            ko.applyBindings(vm.nav, getView(config.viewIds.nav));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});