define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (userId, cardId) {
                $('#quizMenu').attr('href', '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId);
            },

            initializeEditors = function () {
                var converter = Markdown.getSanitizingConverter();
                var editor = new Markdown.Editor(converter);
                editor.run();

                var editor2 = new Markdown.Editor(converter, "-second");
                editor2.run();
            };

        return {
            setQuizMenuUri: setQuizMenuUri,
            initializeEditors: initializeEditors
        };
    }
);