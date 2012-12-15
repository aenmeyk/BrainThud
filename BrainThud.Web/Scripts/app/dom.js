define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (userId, cardId) {
                $('#quizMenu').attr('href', '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId);
            },

            getNewCardValues = function (data) {
                data.deckName = $('#new-card-deckname').val();
                data.tags = $('#new-card-tags').val();
                data.question = $('#wmd-input-question').val();
                data.answer = $('#wmd-input-answer').val();
            },

            resetNewCard = function () {
                $('#wmd-input-question').val('');
                $('#wmd-input-answer').val('');
            },

            isCreateCardRendered = function () {
                return $('#wmd-input-question').length != 0;
            };

        return {
            setQuizMenuUri: setQuizMenuUri,
            getNewCardValues: getNewCardValues,
            resetNewCard: resetNewCard,
            isCreateCardRendered: isCreateCardRendered
        };
    }
);