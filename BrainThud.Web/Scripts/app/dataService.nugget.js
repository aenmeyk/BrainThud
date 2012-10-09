define('dataService.nugget', ['jquery'],
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
        
        return {
            getNuggets: getNuggets
        };
    }
);