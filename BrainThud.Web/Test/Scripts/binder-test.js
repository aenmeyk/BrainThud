/// <reference path="../../Scripts/qunit.js" />
/// <reference path="test-setup.js" />
/// <reference path="../../Scripts/app/binder.js" />

// Setup mocks
var
    $ = function (viewName) {
        var get = function (value) {
            return viewName;
        };
        return {
            get: get
        };
    },

    ko = function () {
        var applyBindings = function (viewModel, rootNode) {
            appliedBindings[viewModel] = rootNode;
        };
        return {
            applyBindings: applyBindings,
        };
    }(),

    appliedBindings = [],
    vm = {
        card: 'card ViewModel',
        createCard: 'createCard ViewModel',
        cards: 'cards ViewModel',
        quiz: 'quiz ViewModel',
    },
    config = {
        viewIds: {
            card: 'card view ID',
            createCard: 'createCard view ID',
            cards: 'cards view ID',
            quiz: 'quiz view ID',
        }
    };

// Tests
(function () {
    module('Given a binder module, when bind() is called', {
        setup: function () {
            itemToTest($, ko, vm, config).bind();
        }
    });

    test('Then the create card view model should be bound to the create card view', function () {
        equal(appliedBindings[vm.card], config.viewIds.card);
    });

    test('Then the card view model should be bound to the card view', function () {
        equal(appliedBindings[vm.createCard], config.viewIds.createCard);
    });

    test('Then the cards view model should be bound to the cards view', function () {
        equal(appliedBindings[vm.cards], config.viewIds.cards);
    });

    test('Then the quiz view model should be bound to the quiz view', function () {
        equal(appliedBindings[vm.quiz], config.viewIds.quiz);
    });
})();