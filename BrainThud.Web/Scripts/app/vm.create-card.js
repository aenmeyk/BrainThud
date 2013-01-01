define('vm.create-card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'editor', 'dom', 'model'],
    function ($, ko, dataContext, presenter, toastr, editor, dom, model) {
        var
            card = ko.observable(new model.Card()),

            createCard = function () {
                var cardData = {
                    quizDate: new Date(),
                    level: 0
                };
                dom.getCardValues(cardData, 'create');
                $.when(dataContext.card.createData({
                    data: cardData
                }))
                .then(function () {
                    toastr.success('Success!');
                    createNewCard();
                });
            },

            activate = function () {
                // do nothing
            },

            createNewCard = function () {
                dom.resetNewCard();
                editor.refreshPreview('create');
            };

        return {
            activate: activate,
            card: card,
            createCard: createCard
        };
    }
);