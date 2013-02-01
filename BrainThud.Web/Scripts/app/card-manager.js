define('card-manager', ['jquery', 'ko', 'data-context', 'global', 'underscore'],
    function (jquery, ko, dataContext, global, _) {
        var
            cards = ko.observableArray([]),
            quizCards = ko.observableArray([]),
            
            init = function() {
                dataContext.card.getData({
                    results: cards
                });
            },

            createCard = function (card) {
                var def = dataContext.card.createData({
                    data: card
                });

                $.when(def).done(function() {
                    // TODO: do stuff after ajax call
                });

                return def;
            },

            updateCard = function (card) {
                var def = dataContext.card.updateData({
                    data: card
                });

                $.when(def).done(function () {
                    // TODO: do stuff after ajax call
                });

                return def;
            },

            deleteCard = function (card) {
                var def = dataContext.card.deleteData({
                    data: card
                });

                $.when(def).done(function () {
                    // TODO: do stuff after ajax call
                });

                return def;
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
                quizCards(_.shuffle(quizCards()));
            },

            applyQuizResult = function () {
                // TODO: 
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