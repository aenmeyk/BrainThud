define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (rowKey) {
                $('#quizMenu').attr('href', '#/quizzes/19/' + utils.getDatePath() + '/' + rowKey);
            };

        return {
            setQuizMenuUri: setQuizMenuUri
        };
    }
);