﻿define('card-manager', ['jquery', 'ko', 'router', 'data-context', 'utils', 'dom', 'amplify', 'config', 'global', 'vm'],
    function ($, ko, router, dataContext, utils, dom, amplify, config, global, vm) {
        var
            deleteCardOptions,
            register = function () {
                var $deleteDialog = $('#deleteDialog');
                var $cardInfoDialog = $('#card-info-dialog');
                $('body').on('click.modal.data-api', '[data-toggle="delete"]', function () {
                    deleteCard();
                });

                amplify.subscribe(config.pubs.showEditCard, function (cardId) {
                    router.navigateTo(global.routePrefix + 'cards/' + cardId + '/edit');
                });

                amplify.subscribe(config.pubs.showDeleteCard, function (data, callback) {
                    deleteCardOptions = {
                        currentCard: data,
                        callback: callback
                    };
                    $deleteDialog.modal('show');
                });

                amplify.subscribe(config.pubs.showCardInfo, function (data) {
                    vm.cardInfo.activate(data);
                    $cardInfoDialog.modal('show');
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
                    dataContext.quizCard.removeCachedItem(deleteCardOptions.currentCard);
                    amplify.publish(config.pubs.deleteCard);
                    if (deleteCardOptions.callback) {
                        deleteCardOptions.callback();
                    }
                });
            };

        
        return {
            register: register
        };
    }
);