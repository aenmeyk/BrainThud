define('vm.create-card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'editor', 'dom'],
    function ($, ko, dataContext, presenter, toastr, editor, dom) {
        var
            deckName = ko.observable(''),
            tags = ko.observable(''),
            question = ko.observable(''),
            answer = ko.observable(''),

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
            deckName: deckName,
            tags: tags,
            question: question,
            answer: answer,
            createCard: createCard
        };
    }
);