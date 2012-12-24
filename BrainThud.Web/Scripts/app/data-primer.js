define('data-primer', ['jquery', 'ko', 'data-context', 'utils'],

function ($, ko, dataContext, utils) {
    var
        quiz = ko.observableArray([]),
        quizOptions = {
            results: quiz,
            params: {
                datePath: utils.getDatePath()
            }
        },
        userConfiguration = ko.observableArray([]),
        configOptions = {
            results: userConfiguration
        },
        fetch = function () {
            return $.Deferred(function (def) {
                $.when(dataContext.quizCard.getData(quizOptions)
                        ,dataContext.config.getData(configOptions))
                    .fail(function () { def.reject(); })
                    .done(function () {
                        def.resolve();
                        var userId = userConfiguration()[0].userId;
                        var firstCard = quiz()[0].cards[0];
                        if (firstCard) {
                            var cardId = firstCard.cardId();
                            global.quizMenuUri = '#/quizzes/' + userId + '/' + utils.getDatePath();
//                            global.quizMenuUri = '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId;
                            global.userId = userId;
                        }
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});