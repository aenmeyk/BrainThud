define('dataService', ['jquery', 'model'],
    function ($, model) {

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

            saveCard = function (callbacks) {
                callbacks.success("Your data was saved");
            };

        return {
            getCards: getCards,
            saveCard: saveCard
        };
    }
);