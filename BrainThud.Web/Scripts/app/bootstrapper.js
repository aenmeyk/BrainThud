define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer'],

function ($, binder, routeConfig, dataPrimer) {
    var run = function () {
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function() {
                routeConfig.register();
            });
        };

    return {
        run: run
    };
});