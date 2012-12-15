define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'gestures', 'dom'],

function ($, binder, routeConfig, dataPrimer, gestures, dom) {
    var run = function () {
        dom.initializeEditors();
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function () {
                gestures.register();
                routeConfig.register();
            });
    };

    return {
        run: run
    };
});