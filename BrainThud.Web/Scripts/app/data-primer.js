define('data-primer', ['jquery', 'ko', 'data-context', 'vm'],
    function ($, ko, dataContext, vm) {
        var
            datePath,
            quiz = ko.observableArray([]),
            dataOptions = function () {
                var today = new Date(),
                year = today.getFullYear(),
                month = today.getMonth() + 1,
                day = today.getDate();

                datePath = year + '/' + month + '/' + day;

                return {
                    results: quiz,
                    params: {
                        datePath: datePath
                    }
                };
            },

            fetch = function () {
                return $.Deferred(function (def) {
                    $.when(dataContext.quiz.getData(dataOptions())
                        .fail(function () { def.reject(); }))
                        .done(function() {
                            def.resolve();
                            setQuizMenuUri(quiz()[0].cards[0].rowKey());
                        });
//                    def.resolve();
                }).promise();
            },
            

        setQuizMenuUri = function(rowKey) {
            $('#quizMenu').attr('href', '#/quizzes/' + datePath + '/' + rowKey);
        };


        return {
            fetch: fetch
        };
    }
);