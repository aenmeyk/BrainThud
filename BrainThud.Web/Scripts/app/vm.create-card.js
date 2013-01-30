﻿define('vm.create-card', ['ko', 'data-context', 'editor', 'dom', 'model'],
    function (ko, dataContext, editor, dom, model) {
        var
            card = ko.observable(new model.Card()),
            
            isValid = ko.computed(function() {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function () { },

            createAndNewCommand = ko.asyncCommand({
                execute: function (complete) {
                    var newCard = {
                        quizDate: new Date(),
                        level: 0
                    };
                    dom.getCardValues(newCard, 'create');
                    
                    $.when(dataContext.card.createData({ data: newCard }))
                    .done(function () {
                        dataContext.quizCard.setCacheInvalid();
                        dom.resetNewCard();
                        editor.refreshPreview('create');
                    })
                    .always(function() {
                        complete();
                    });
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && isValid();
                }
            });

        return {
            activate: activate,
            card: card,
            createAndNewCommand: createAndNewCommand
        };
    }
);