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
                $.when(dataContext.config.getData(configOptions))
                    .done(function() {
                        global.userId = userConfiguration()[0].userId;
                        $.when(dataContext.quiz.getData({
                                params: {
                                    datePath: utils.getDatePath(),
                                    userId: global.userId
                                }
                            }), dataContext.card.getData({ results: ko.observableArray([]) }))
                            .fail(function() { def.reject(); })
                            .done(function() {
                                def.resolve();
                            });
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});