define('vm.card', ['jquery', 'ko', 'data-context', 'presenter'],
    function ($, ko, dataContext, presenter) {
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
            },
            
            hideSuccess = function () {
                presenter.hideSuccess();
            };

        return {
            activate: activate,
            question: question,
            answer: answer,
            saveCard: saveCard,
            hideSuccess: hideSuccess
        };
    }
);