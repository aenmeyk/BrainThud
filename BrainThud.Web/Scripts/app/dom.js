define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (rowKey) {
                $('#quizMenu').attr('href', '#/quizzes/' + utils.getDatePath() + '/' + rowKey);
            };

        return {
            setQuizMenuUri: setQuizMenuUri
        };
    }
);