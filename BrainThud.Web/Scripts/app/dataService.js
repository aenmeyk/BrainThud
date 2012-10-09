define('dataService', ['jquery'],
    function ($) {

        var root = '/api/',
            getNuggets = function(callbacks) {
                var url = root + 'nuggets';
                $.getJSON(url)
                .done(function(result, status) {
                    callback.success(result);
                })
                .fail(function (result, status) {
                    callbacks.error(result);
                });
            };

        var questions = function() {
            return [
                'How long is a piece of string?',
                'When does 1 + 1 != 2?',
                'What is black and white and read all over?'
            ];
        };

        return {
//            getNuggets: getNuggets
            getNuggets: questions
        };
    }
);