﻿define('vm.card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'dom', 'editor', 'router', 'global', 'model'],
    function ($, ko, dataContext, presenter, toastr, dom, editor, router, global, model) {
        var
            card = ko.observable(new model.Card()),
            cards = ko.observableArray(),
            dataOptions = {
                results: cards
            },

            updateCard = function () {
                var item = ko.toJS(card);
                dom.getCardValues(item, 'edit');
                $.when(dataContext.card.updateData({
                    data: item
                }))
                .then(function () {
                    toastr.success('Success!');
                    router.navigateTo(global.previousUrl);
                });
            },

            activate = function (routeData) {
                $.when(dataContext.card.getData(dataOptions))
                    .then(function () {
                        var cardId = parseInt(routeData.cardId);
                        var item = _.find(cards(), function(val) {
                            return val.entityId() === cardId;
                        });
                        card(item);
                    })
                    .then(function () {
                        editor.refreshPreview('edit');
                    });
            };

        return {
            activate: activate,
            card: card,
            updateCard: updateCard
        };
    }
);