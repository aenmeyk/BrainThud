define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            setQuizMenuUri = function (userId, cardId) {
                $('#quizMenu').attr('href', '#/quizzes/' + userId + '/' + utils.getDatePath() + '/' + cardId);
            },

            getCardValues = function (card, editorName) {
                card.deckName = $('#new-card-deckname-' + editorName).val();
                card.tags = $('#new-card-tags-' + editorName).val();
                card.question = $('#wmd-input-question-' + editorName).val();
                card.answer = $('#wmd-input-answer-' + editorName).val();
            },

            resetNewCard = function () {
                $('#wmd-input-question-create').val('');
                $('#wmd-input-answer-create').val('');
            },

            isCreateCardRendered = function () {
                return $('#wmd-input-question-create').length != 0;
            };

        return {
            setQuizMenuUri: setQuizMenuUri,
            getCardValues: getCardValues,
            resetNewCard: resetNewCard,
            isCreateCardRendered: isCreateCardRendered
        };
    }
);