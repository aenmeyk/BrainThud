define('data-primer', ['jquery', 'ko', 'data-context', 'utils', 'global'],

function ($, ko, dataContext, utils, global) {
    var
        userConfiguration = ko.observableArray([]),
        configOptions = {
            results: userConfiguration
        },
        fetch = function () {
            return $.Deferred(function (def) {
                $.when(dataContext.userConfiguration.getData(configOptions))
                    .done(function () {
                        if (userConfiguration()[0]) {
                            global.userId = userConfiguration()[0].userId();
                            global.routePrefix = '#/' + global.userId + '/';
                            var datePath = utils.getDatePath();
                            $.when(
                                dataContext.quizResult.getData({
                                    params: {
                                        datePath: datePath,
                                        userId: global.userId
                                    }
                                }),
                                dataContext.card.getData({}),
                                dataContext.quizCard.getData({
                                    params: {
                                        datePath: datePath,
                                        userId: global.userId
                                    }
                                }))
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