define('vm.card', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var question = ko.observable(''),
            answer = ko.observable(''),

            saveCard = function () {
                dataContext.card.saveData({
                    data: {
                        question: question(),
                        answer: answer()
                    }
                });
            },

            activate = function () {
                // do nothing
            };

        return {
            activate: activate,
            question: question,
            answer: answer,
            saveCard: saveCard
        };
    }
);