define('presenter', [],
    function () {
        var
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
            },

            showSuccess = function () {
                $('.alert-success').removeClass('hidden');
            },

            hideSuccess = function () {
                $('.alert-success').addClass('hidden');
            };

        return {
            transitionTo: transitionTo,
            showSuccess: showSuccess,
            hideSuccess: hideSuccess
        };
    }
);