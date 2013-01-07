define('data-primer', ['jquery', 'ko', 'data-context', 'utils', 'global'],

function ($, ko, dataContext, utils, global) {
    var
        userConfiguration = ko.observableArray([]),
        configOptions = {
            results: userConfiguration
        },
        fetch = function () {
            return $.Deferred(function (def) {
                $.when(dataContext.config.getData(configOptions))
                    .done(function () {
                        if (userConfiguration()[0]) {
                            global.userId = userConfiguration()[0].userId;
                            $.when(dataContext.quizResult.getData({
                                params: {
                                    datePath: utils.getDatePath(),
                                    userId: global.userId
                                }
                            }), dataContext.card.getData({ results: ko.observableArray([]) }))
                            .fail(function() { def.reject(); })
                            .done(function() { def.resolve(); });
                        } else {
                            def.reject();
                        }
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});