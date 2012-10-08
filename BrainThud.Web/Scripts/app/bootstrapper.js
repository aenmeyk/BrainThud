define('bootstrapper', ['jquery', 'binder'],

function ($, binder) {
    var run = function () {
        //        dataprimer.fetch();
        binder.bind();
    };

    return {
        run: run
    };
});