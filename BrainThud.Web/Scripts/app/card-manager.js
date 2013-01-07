define('card-manager', ['jquery', 'ko', 'router', 'data-context', 'utils', 'dom', 'amplify', 'config', 'global'],
    function ($, ko, router, dataContext, utils, dom, amplify, config, global) {
        var
            deleteCardOptions,
            register = function () {
                var $deleteDialog = $('#deleteDialog');
                $('body').on('click.modal.data-api', '[data-toggle="delete"]', function () {
                    deleteCard();
                });

                amplify.subscribe(config.pubs.createNewCard, function (newCard) {
                    createCard(newCard);
                });

                amplify.subscribe(config.pubs.showEditCard, function (cardId) {
                    router.navigateTo('#/cards/' + cardId + '/edit');
                });

                amplify.subscribe(config.pubs.showDeleteCard, function (data) {
                    deleteCardOptions = {
                        currentCard: data,
                    };
                    $deleteDialog.modal('show');
                });
            },
            
            createCard = function(newCard) {
                $.when(dataContext.card.createData({ data: newCard }))
                .then(function () {
                    toastr.success('Success!');
                    amplify.publish(config.pubs.createCard);
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
                    amplify.publish(config.pubs.deleteCard);
                });
            };

        
        return {
            register: register
        };
    }
);