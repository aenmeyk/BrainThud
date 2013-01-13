define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'card-manager', 'quiz-navigator'],
function ($, binder, routeConfig, dataPrimer, cardManager, quizNavigator) {
    var
        run = function () {
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function() {
                routeConfig.register();
                cardManager.register();
            });
        };

    return {
        run: run
    };
});