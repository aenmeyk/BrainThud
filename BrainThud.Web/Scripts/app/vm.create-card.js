define('vm.create-card', ['ko', 'data-context', 'editor', 'dom', 'model', 'amplify', 'config'],
    function (ko, dataContext, editor, dom, model, amplify, config) {
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
                        .always(function () {
                            dataContext.quizCard.setCacheInvalid();
                            toastr.success('Success!');
                            dom.resetNewCard();
                            editor.refreshPreview('create');
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