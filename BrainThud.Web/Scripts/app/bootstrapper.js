//function ($, dataprimer, binder, routeConfig, presenter) {
//    var run = function () {
//        presenter.toggleActivity(true);
//
//        $.when(dataprimer.fetch())
//            .done(binder.bind)
//            .done(routeConfig.register)
//            .always(function () {
//                presenter.toggleActivity(false);
//            });
//    };
//
//    return {
//        run: run
//    };
//}
define('bootstrapper',
    ['dataprimer'],

function (dataprimer) {
    var run = function () {
        dataprimer.fetch();
    };

    return {
        run: run
    };
});