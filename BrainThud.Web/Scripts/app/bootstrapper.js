define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'gestures'],

function ($, binder, routeConfig, dataPrimer, gestures) {
    var run = function () {
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function() {
                if (Modernizr.touch) {
                    gestures.register();
                }
                routeConfig.register();
            });
        };

    return {
        run: run
    };
});