define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'card-manager', 'dom'],
function ($, binder, routeConfig, dataPrimer, cardManager, dom) {
    var
        run = function () {
            dom.showLoading();
            $.when(dataPrimer.fetch())
                .done(binder.bind)
                .done(routeConfig.register)
                .always(dom.hideLoading);
        };

    return {
        run: run
    };
});