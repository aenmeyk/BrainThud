define('card-manager', ['jquery', 'ko', 'data-context', 'global', 'underscore', 'data-service', 'model.mapper', 'vm.card-info'],
    function (jquery, ko, dataContext, global, _, dataService, modelMapper, cardInfo) {
        var
            $deleteDialog,
            $cardInfoDialog,
            deleteCardOptions,
            cards = ko.observableArray([]),
            quizCards = ko.observableArray([]),
            quizYear = ko.observable(0),
            quizMonth = ko.observable(0),
            quizDay = ko.observable(0),

            quizDate = ko.computed(function () {
                return moment([quizYear(), quizMonth() - 1, quizDay()]).format('L');
            }),

            quizCardCount = ko.computed(function () {
                return quizCards().length;
            }),

            init = function () {
                getCards();

                $deleteDialog = $('#deleteDialog');
                $cardInfoDialog = $('#card-info-dialog');
                $('body').on('click.modal.data-api', '[data-toggle="delete"]', function () {
                    executeDelete();
                });
            },
            
            getCards = function () {
                dataContext.card.getData({
                    results: cards
                });
            },

            getQuizCards = function (year, month, day) {
                if (year && year >= 0) {
                    quizYear(year);
                    quizMonth(month);
                    quizDay(day);

                    dataContext.quizCard.getData({
                        results: quizCards,
                        params: {
                            datePath: moment([year, month, day]).format('YYYY/M/D'),
                            userId: global.userId
                        }
                    });
                }
            },
                
            refreshCards = function() {
                getCards();
                getQuizCards(quizYear(), quizMonth(), quizDay());
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
                    dataContext.quizCard.setCacheInvalid();
                    getCards();
                    def.resolve(updatedCard);
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
                    data: {
                        userId: global.userId,
                        partitionKey: deleteCardOptions.card.partitionKey(),
                        rowKey: deleteCardOptions.card.rowKey(),
                        entityId: deleteCardOptions.card.entityId()
                    }
                })).then(function () {
                    refreshCards();
                    if (deleteCardOptions.callback) {
                        deleteCardOptions.callback();
                    }
                });
            },

            shuffleQuizCards = function () {
                quizCards(_.shuffle(quizCards()));
                toastr.success('Cards Shuffled');
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
            quizCards: quizCards,
            quizCardCount: quizCardCount,
            createCard: createCard,
            updateCard: updateCard,
            deleteCard: deleteCard,
            showCardInfo: showCardInfo,
            getQuizCards: getQuizCards,
            shuffleQuizCards: shuffleQuizCards,
            applyQuizResult: applyQuizResult
        };
    }
);