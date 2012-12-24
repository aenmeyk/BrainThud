define('data-primer', ['jquery', 'ko', 'data-context', 'utils'],

function ($, ko, dataContext, utils) {
    var
        quiz = ko.observableArray([]),
        dataOptions = function () {
            return {
                results: quiz,
                params: {
                    datePath: utils.getDatePath()
                }
            };
        },
        fetch = function () {
            return $.Deferred(function (def) {
                $.when(dataContext.quizCard.getData(dataOptions())
                    .fail(function () { def.reject(); }))
                    .done(function () {
                        def.resolve();
                        var firstCard = quiz()[0].cards[0];
                        if (firstCard) {
                            var userId = firstCard.userId();
                            var cardId = firstCard.cardId();
                            global.quizMenuUri = '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId;
                        }
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});