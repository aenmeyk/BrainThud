define('presenter', [],
    function () {

        transitionTo = function ($view) {
            var $activeViews = $('.view-active');

            if ($activeViews.length) {
                $activeViews.hide();
                $('.view').removeClass('view-active');
            }

            if ($view.length) {
                $view.show();
                $view.addClass('view-active');
            }
        };

        return {
            transitionTo: transitionTo
        };
    }
);