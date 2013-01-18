define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'card-manager'],
function ($, binder, routeConfig, dataPrimer, cardManager) {
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