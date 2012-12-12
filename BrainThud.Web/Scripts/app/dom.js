define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (userId, cardId) {
                $('#quizMenu').attr('href', '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId);
            };

        return {
            setQuizMenuUri: setQuizMenuUri
        };
    }
);