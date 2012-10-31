define('data-service', ['jquery'],
    function ($) {

        var root = '/api/',
            getCards = function(callbacks) {
                var url = root + 'cards';
                $.getJSON(url)
                .done(function(result, status) {
                    callbacks.success(result);
                })
                .fail(function (result, status) {
                    callbacks.error(result);
                });
            },

            saveCard = function (options) {
                //                callbacks.success("Your card was saved");
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
            getCards: getCards,
            saveCard: saveCard
        };
    }
);