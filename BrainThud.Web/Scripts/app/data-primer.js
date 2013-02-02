define('data-primer', ['jquery', 'ko', 'data-context', 'global', 'card-manager'],

function ($, ko, dataContext, global, cardManager) {
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
                            
                            $.when(cardManager.init())
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