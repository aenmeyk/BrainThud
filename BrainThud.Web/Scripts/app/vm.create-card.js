define('vm.create-card', ['ko', 'editor', 'dom', 'model', 'amplify', 'config'],
    function (ko, editor, dom, model, amplify, config) {
        var
            card = ko.observable(new model.Card()),

            init = function () {
                amplify.subscribe(config.pubs.createCard, function () {
                    dom.resetNewCard();
                    editor.refreshPreview('create');
                });
            },
            
            isValid = ko.computed(function() {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function () { },

            createCard = function () {
                var newCard = {
                    quizDate: new Date(),
                    level: 0
                };
                dom.getCardValues(newCard, 'create');
                amplify.publish(config.pubs.createNewCard, newCard);
            },

            createAndNewCommand = ko.asyncCommand({
                execute: function (complete) {
                        $.when(createCard())
                            .always(complete);
                        return;
                },
                canExecute: function (isExecuting) {
                    return isValid();
                }
            });

        init();

        return {
            activate: activate,
            card: card,
            createAndNewCommand: createAndNewCommand
        };
    }
);