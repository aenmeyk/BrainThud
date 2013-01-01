define('card-manager', ['ko', 'router', 'data-context', 'utils', 'amplify', 'config', 'global'],
    function (ko, router, dataContext, utils, amplify, config, global) {
        var
            deleteCardOptions,
            register = function () {
                var $deleteDialog = $('#deleteDialog');
                $('body').on('click.modal.data-api', '[data-toggle="delete"]', function () {
                    deleteCard();
                });

                amplify.subscribe(config.pubs.showEditCard, function (cardId) {
                    router.navigateTo('#/cards/' + cardId + '/edit');
                });

                amplify.subscribe(config.pubs.showDeleteCard, function (options) {
                    deleteCardOptions = {
                        currentCard: options.data,
                        currentCallback: options.callback                      
                    };
                    $deleteDialog.modal('show');
                });
            },

            deleteCard = function () {
                $.when(dataContext.card.deleteData({
                    params: {
                        userId: global.userId,
                        partitionKey: deleteCardOptions.currentCard.partitionKey(),
                        rowKey: deleteCardOptions.currentCard.rowKey(),
                        entityId: deleteCardOptions.currentCard.entityId()
                    }
                })).then(function () {
                    $("#deleteDialog").modal('hide');
                    deleteCardOptions.currentCallback();
                    amplify.publish(config.pubs.deleteCard);
                });
            };

        
        return {
            register: register
        };
    }
);