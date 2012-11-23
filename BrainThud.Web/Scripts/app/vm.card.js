define('vm.card', ['jquery', 'ko', 'data-context', 'presenter'],
    function ($, ko, dataContext, presenter) {
        var question = ko.observable(''),
            answer = ko.observable(''),
            deckName = ko.observable(''),
            tags = ko.observable(''),

            saveCard = function () {
                $.when(dataContext.card.saveData({
                    data: {
                        question: question(),
                        answer: answer(),
                        deckName: deckName(),
                        tags: tags(),
                        quizDate: new Date(),
                        level: 0
                    }}))
                    .then(function() {
                        createNewCard();
                    });
            },

            activate = function () {
                // do nothing
            },

            hideSuccess = function () {
                presenter.hideSuccess();
            },

            createNewCard = function () {
                question('');
                answer('');
            };

        return {
            activate: activate,
            question: question,
            answer: answer,
            deckName: deckName,
            tags: tags,
            saveCard: saveCard,
            hideSuccess: hideSuccess
        };
    }
);