define('binder', ['jquery', 'ko', 'vm'],

function ($, ko, vm) {
    var
        bind = function () {
            ko.applyBindings(vm.createCard, getView('#create-card-view'));
            ko.applyBindings(vm.card, getView('#card-view'));
            ko.applyBindings(vm.cards, getView('#cards-view'));
            ko.applyBindings(vm.quiz, getView('#todays-quiz-view'));
        },

        getView = function(viewName) {
            return $(viewName).get(0);
        };

    return {
        bind: bind
    };
});