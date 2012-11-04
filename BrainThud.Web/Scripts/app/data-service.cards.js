define('data-service.cards', ['jquery'],
    function ($) {

        var root = '/api/',
            get = function(callbacks) {
                var url = root + 'cards';
                $.getJSON(url)
                .done(function(result, status) {
                    callbacks.success(result);
                })
                .fail(function (result, status) {
                    callbacks.error(result);
                });
            },

            save = function (options) {
                $.ajax({
                    url: root + 'cards',
                    type: 'POST',
                    data: JSON.stringify(options.data),
                    contentType: 'application/json',
                    success: function (result) {
                        options.success(result);
                    },
                    error: function (result) {
                        options.error(result);
                    }
                });
            };

        return {
            get: get,
            save: save
        };
    }
);