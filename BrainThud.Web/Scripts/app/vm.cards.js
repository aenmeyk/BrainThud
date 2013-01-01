define('vm.cards', ['jquery', 'ko', 'data-context', 'router', 'amplify', 'config'],
    function ($, ko, dataContext, router, amplify, config) {
        var
            cards = ko.observableArray([]),
            
            dataOptions = {
                results: cards
            },
            
            editCard = function (card) {
                router.navigateTo('#/cards/' + card.entityId() + '/edit');
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            activate = function () {
                dataContext.card.getData(dataOptions);
            },
            
            showDeleteDialog = function (card) {
                amplify.publish(config.pubs.showDeleteCard, {
                    data: card,
                    callback: function() {
                        dataContext.card.getData(dataOptions);
                    }
                });
            };

        return {
            cards: cards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            showDeleteDialog: showDeleteDialog
        };
    }
);
