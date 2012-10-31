define('data-primer', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var fetch = function () {
            return $.Deferred(function (def) {
                // TODO: Prefetch data
                //                var data = {
                //                    cards: ko.observable()
                //                };

                //                $.when(dataContext.cards.getData({ results: data.cards })
                //                    .fail(function() { def.reject(); }))
                //                    .done(function () { def.resolve(); });


                def.resolve();
            }).promise();
        };

        return {
            fetch: fetch
        };
    }
);