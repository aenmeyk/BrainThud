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
                        , dataContext.config.getData(configOptions)
                        , dataContext.card.getData({ results: ko.observableArray([]) }))
                    .fail(function () { def.reject(); })
                    .done(function () {
                        def.resolve();
                        var userId = userConfiguration()[0].userId;
                        global.quizMenuUri = '#/quizzes/' + userId + '/' + utils.getDatePath();
                        global.userId = userId;
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});