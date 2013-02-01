define('card-manager', ['jquery', 'ko', 'data-context', 'global', 'underscore'],
    function (jquery, ko, dataContext, global, _) {
        var
            cards = ko.observableArray([]),
            quizCards = ko.observableArray([]),
            quizYear = ko.observable(0),
            quizMonth = ko.observable(0),
            quizDay = ko.observable(0),

            quizDate = ko.computed(function () {
                return moment([quizYear(), quizMonth() - 1, quizDay()]).format('L');
            }),

            init = function() {
                dataContext.card.getData({
                    results: cards
                });
            },

            createCard = function (card) {
                var def = new $.Deferred();

                $.when(dataContext.card.createData({
                    data: card
                })).done(function (newCard) {
                    if (moment(newCard.quizDate).format('L') == quizDate) {
                        quizCards.push(newCard);
                    }
                    dataContext.quizCard.setCacheInvalid();
                    def.resolve(newCard);
                }).fail(function () {
                    def.reject();
                });

                return def;
            },

            updateCard = function (card) {
                var def = new $.Deferred();

                $.when(dataContext.card.updateData({
                    data: card
                })).done(function (updatedCard) {
                    // TODO: do stuff after ajax call
                    def.resolve(updatedCard);
                }).fail(function() {
                    def.reject();
                });

                return def;
            },

            deleteCard = function (card) {
                var def = new $.Deferred();

                $.when(dataContext.card.deleteData({
                    data: card
                })).done(function () {
                    // TODO: do stuff after ajax call
                    def.resolve();
                }).fail(function () {
                    def.reject();
                });

                return def;
            },

            getQuizCards = function (year, month, day) {
                quizYear(year);
                quizMonth(month);
                quizDay(day);

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