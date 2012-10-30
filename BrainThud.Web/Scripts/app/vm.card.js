define('vm.card', ['jquery', 'ko', 'dataContext'],
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
            };

        return {
            question: question,
            answer: answer,
            saveCard: saveCard
        };
    }
);