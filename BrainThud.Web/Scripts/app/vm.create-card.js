define('vm.create-card', ['ko', 'card-manager', 'dom', 'model'],
    
function (ko, cardManager, dom, model) {
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
                    
                $.when(cardManager.createCard(newCard))
                .done(function () {
                    var cardItem = card();
                    cardItem.question('');
                    cardItem.answer('');
                    dom.resetNewCard();
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
});