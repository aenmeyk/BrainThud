define('bootstrapper', ['jquery', 'binder', 'route-config'],

function ($, binder, routeConfig) {
    var run = function () {
        //        dataprimer.fetch();
        binder.bind();
        routeConfig.register();
    };

    return {
        run: run
    };
});