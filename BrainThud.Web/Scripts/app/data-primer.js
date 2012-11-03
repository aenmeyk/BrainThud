define('data-primer', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            fetch = function () {
                return $.Deferred(function (def) {
                    $.when(dataContext.cards.getData()
                        .fail(function () { def.reject(); }))
                        .done(function () { def.resolve(); });
                }).promise();
            };

        return {
            fetch: fetch
        };
    }
);