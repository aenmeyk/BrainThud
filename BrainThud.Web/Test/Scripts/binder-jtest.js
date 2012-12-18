﻿/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="jtest-setup.js" />

/// <reference path="../../Scripts/jquery-1.8.2.js" />
/// <reference path="../../Scripts/knockout-2.2.0.debug.js" />

/// <reference path="../../Scripts/app/binder.js" />

describe("A binder module, when bind() is called", function () {
    var
        get,
        applyBindings,
        vm = {
            card: 'card ViewModel',
            createCard: 'createCard ViewModel',
            cards: 'cards ViewModel',
            quiz: 'quiz ViewModel'
        },
        config = {
            viewIds: {
                card: 'card view ID',
                createCard: 'createCard view ID',
                cards: 'cards view ID',
                quiz: 'quiz view ID'
            }
        };
    
    beforeEach(function () {
        get = spyOn(jQuery.fn, "get").andCallFake(function() {
            return this.selector;
        });
        applyBindings = spyOn(ko, "applyBindings");
        modules['binder'](jQuery, ko, vm, config).bind();
    });

    it('should bind the card view model to the card view', function () {
        expect(ko.applyBindings).toHaveBeenCalledWith(vm.card, config.viewIds.card);
    });

    it('should bind the create card view model to the create card view', function () {
        expect(ko.applyBindings).toHaveBeenCalledWith(vm.createCard, config.viewIds.createCard);
    });

    it('should bind the cards view model to the cards view', function () {
        expect(ko.applyBindings).toHaveBeenCalledWith(vm.cards, config.viewIds.cards);
    });

    it('should bind the quiz view model to the quiz view', function () {
        expect(ko.applyBindings).toHaveBeenCalledWith(vm.quiz, config.viewIds.quiz);
    });
});
