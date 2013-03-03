define('card-manager', ['jquery', 'ko', 'data-context', 'global', 'underscore', 'data-service', 'model.mapper', 'vm.card-info'],

function ($, ko, dataContext, global, _, dataService, modelMapper, cardInfo) {
    var
        $deleteDialog,
        $cardInfoDialog,
        deleteCardOptions,
        currentDeckNameSlug,
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

                $.when(getCardDecks())
                .done(function() {
                    def.resolve();
                });
            }).promise();
        },
            
        getCards = function (deckNameSlug) {
            if (deckNameSlug !== currentDeckNameSlug) dataContext.card.setCacheInvalid();
            currentDeckNameSlug = deckNameSlug;
            return dataContext.card.getData({
                params: {
                    userId: global.userId,
                    deckNameSlug: deckNameSlug
                },
                results: cards
            });
        },
            
        getCardDecks = function () {
            return dataContext.cardDeck.getData({
                params: {
                    userId: global.userId
                },
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

            $.when(getCardDecks(), getCards(currentDeckNameSlug), getQuizCards(quizYear(), quizMonth(), quizDay()))
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
                dataContext.cardDeck.setCacheInvalid();
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
                dataContext.card.updateCachedItem(updatedCard);
                dataContext.quizCard.updateCachedItem(updatedCard);
                dataContext.cardDeck.setCacheInvalid();
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
            var data = new Array();
            data[0] = ko.toJS(card);

            deleteCardOptions = {
                cards: data,
                callback: callback
            };
            
            $deleteDialog.modal('show');
        },

        deleteCardBatch = function (batchCards) {
            deleteCardOptions = {
                cards: ko.toJS(batchCards),
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
                data: deleteCardOptions.cards
            })).then(function () {
                dataContext.quizCard.setCacheInvalid();
                dataContext.quizResult.setCacheInvalid();
                dataContext.cardDeck.setCacheInvalid();

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
        getCards: getCards,
        getCardDecks: getCardDecks,
        createCard: createCard,
        updateCard: updateCard,
        deleteCard: deleteCard,
        deleteCardBatch: deleteCardBatch,
        showCardInfo: showCardInfo,
        getQuizCards: getQuizCards,
        applyQuizResult: applyQuizResult
    };
});