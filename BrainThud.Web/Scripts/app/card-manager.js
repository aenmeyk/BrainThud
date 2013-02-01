define('card-manager', ['ko', 'data-context', 'global'],
    function (ko, dataContext, global) {
        var
            cards = ko.observableArray([]),
            quizCards = ko.observableArray([]),
            
            init = function() {
                dataContext.card.getData({
                    results: cards
                });
            },

            createCard = function () {

            },

            updateCard = function () {

            },

            deleteCard = function () {

            },

            getQuizCards = function (year, month, day) {
                dataContext.quizResult.getData({
                    results: quizResults,
                    params: {
                        datePath: moment([year, month, day]).format('YYYY/M/D'),
                        userId: global.userId
                    }
                });
            },

            shuffleQuizCards = function () {

            },

            applyQuizResult = function () {

            };

        init();
        
        return {
            cards: cards,
            quizCards: quizCards,
            createCard: createCard,
            updateCard: updateCard,
            deleteCard: deleteCard,
            getQuizCards: getQuizCards,
            shuffleQuizCards: shuffleQuizCards,
            applyQuizResult: applyQuizResult
        };
    }
);