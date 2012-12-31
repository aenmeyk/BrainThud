define('vm.cards', ['jquery', 'ko', 'data-context', 'router', 'amplify', 'config', 'global'],
    function ($, ko, dataContext, router, amplify, config, global) {
        var
            activeCard,
            cards = ko.observableArray([]),
            
            dataOptions = function() {
                return {
                    results: cards
                };
            },
            
            editCard = function (card) {
                router.navigateTo('#/cards/' + card.entityId() + '/edit');
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            activate = function (routeData) {
                dataContext.card.getData(dataOptions());
            },
            
            showDeleteDialog = function (card) {
                activeCard = card;
                $("#cards-view .deleteDialog").modal('show');
            },

            deleteCard = function () {
                $.when(dataContext.card.deleteData({
                    params: {
                        userId: global.userId,
                        partitionKey: activeCard.partitionKey(),
                        rowKey: activeCard.rowKey(),
                        entityId: activeCard.entityId()
                    }
                })).then(function () {
                    $("#cards-view .deleteDialog").modal('hide');
                    amplify.publish(config.pubs.deleteCard);
                    activate();
                });
            };

        return {
            cards: cards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            showDeleteDialog: showDeleteDialog,
            deleteCard: deleteCard
        };
    }
);
