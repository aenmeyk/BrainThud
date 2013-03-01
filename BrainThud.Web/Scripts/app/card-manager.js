define('card-manager', ['jquery', 'ko', 'data-context', 'global', 'underscore', 'data-service', 'model.mapper', 'vm.card-info'],

function ($, ko, dataContext, global, _, dataService, modelMapper, cardInfo) {
    var
        $deleteDialog,
        $cardInfoDialog,
        deleteCardOptions,
        cards = ko.observableArray([]),
        cardDecks = ko.observableArray([]),
        quizCards = ko.observableArray([]),
        quizYear = ko.observable(0),
        quizMonth = ko.observable(0),
        quizDay = ko.observable(0),

        cardDeckNames = ko.computed(function () {
            var sortedCardDecks = _.sortBy(cardDecks(), function (item) {
                return item.deckName().toLowerCase();;
            });
                            
            return _.map(sortedCardDecks, function (item) {
                return item.deckName();
            });
        }),

        quizDate = ko.computed(function () {
            return moment([quizYear(), quizMonth() - 1, quizDay()]).format('L');
        }),

        quizCardCount = ko.computed(function () {
            return quizCards().length;
        }),

        init = function () {
            return $.Deferred(function (def) {
                $deleteDialog = $('#deleteDialog');
                $cardInfoDialog = $('#card-info-dialog');
                $('body').on('click.modal.data-api', '[data-toggle="delete"]', function () {
                    executeDelete();
                });

                $.when(getCards(), getCardDecks())
                .done(function() {
                    def.resolve();
                });
            }).promise();
        },
            
        getCards = function () {
            return dataContext.card.getData({
                results: cards
            });
        },
            
        getCardDecks = function () {
            return dataContext.cardDeck.getData({
                results: cardDecks
            });
        },

        getQuizCards = function (year, month, day) {
            var def = new $.Deferred();
            if (year && year >= 0) {
                // If the quiz date has changed, invalidate the cache
                if (quizYear() !== year || quizMonth() !== month || quizDay() !== day) {
                    quizYear(year);
                    quizMonth(month);
                    quizDay(day);
                    dataContext.quizCard.setCacheInvalid();
                }

                def = dataContext.quizCard.getData({
                    results: quizCards,
                    params: {
                        datePath: moment([year, month - 1, day]).format('YYYY/M/D'),
                        userId: global.userId
                    }
                });
            } else {
                def.resolve();
            }

            return def;
        },
                
        refreshCards = function () {
            var def = new $.Deferred();

            $.when(getCards(), getQuizCards(quizYear(), quizMonth(), quizDay()))
            .done(function() {
                def.resolve();
            })
            .fail(function() {
                def.reject();
            });

            return def;
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
                getCards();
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
                dataContext.quizCard.updateCachedItem(updatedCard);
                $.when(refreshCards())
                .done(function() {
                    def.resolve(updatedCard);
                });
            }).fail(function() {
                def.reject();
            });

            return def;
        },

        deleteCard = function (card, callback) {
            deleteCardOptions = {
                card: card,
                callback: callback
            };
            $deleteDialog.modal('show');
        },

        showCardInfo = function (card) {
            cardInfo.activate(card);
            $cardInfoDialog.modal('show');
        },

        executeDelete = function () {
            $("#deleteDialog").modal('hide');
            $.when(dataContext.card.deleteData({
                data: ko.toJS(deleteCardOptions.card)
            })).then(function () {
                dataContext.quizCard.setCacheInvalid();
                dataContext.quizResult.setCacheInvalid();
                
                $.when(refreshCards())
                .done(function() {
                    if (deleteCardOptions.callback) {
                        deleteCardOptions.callback(deleteCardOptions.card);
                    }
                });
            });
        },

        applyQuizResult = function (quizResult) {
            dataService.card.getSingle({
                params: {
                    userId: global.userId,
                    entityId: quizResult.cardId()
                },
                success: function (dto) {
                    var result = modelMapper.card.mapResult(dto);
                    dataContext.quizCard.updateCachedItem(result);
                    dataContext.card.updateCachedItem(result);
                    refreshCards();
                }
            });
        };
        
    return {
        init: init,
        cards: cards,
        cardDecks: cardDecks,
        cardDeckNames: cardDeckNames,
        quizCards: quizCards,
        quizCardCount: quizCardCount,
        createCard: createCard,
        updateCard: updateCard,
        deleteCard: deleteCard,
        showCardInfo: showCardInfo,
        getQuizCards: getQuizCards,
        applyQuizResult: applyQuizResult
    };
});