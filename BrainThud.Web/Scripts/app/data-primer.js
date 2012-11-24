define('data-primer', ['jquery', 'ko', 'data-context', 'utils', 'dom'],
    function ($, ko, dataContext, utils, dom) {
        var
            quiz = ko.observableArray([]),
            dataOptions = function() {
                return {
                    results: quiz,
                    params: {
                        datePath: utils.getDatePath()
                    }
                };
            },
            fetch = function() {
                return $.Deferred(function(def) {
                    $.when(dataContext.quiz.getData(dataOptions())
                        .fail(function() { def.reject(); }))
                        .done(function() {
                            def.resolve();
                            dom.setQuizMenuUri(quiz()[0].cards[0].rowKey());
                        });
                }).promise();
            };

        return {
            fetch: fetch
        };
    }
);