﻿define('vm.quiz-card', ['jquery', 'ko', 'data-context', 'utils', 'router'],
    function ($, ko, dataContext, utils, router) {
        var
            init = function () {
                datePath = utils.getDatePath();
                // Certain browsers highlight the div when clicked.  Remove this highlight.
                var cardElement = $('#card');
                cardElement.click(function () {
                    cardElement.blur();
                });
            },
            datePath,
            questionSideVisible = ko.observable(true),
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
            previousCard = ko.observable(),
            cards = ko.observableArray([]),
            userId = ko.observable(),

            currentCard = {
                cardId: ko.observable(''),
                question: ko.observable(''),
                answer: ko.observable(''),
            },

            currentCardText = ko.computed(function () {
                var cardText = '';
                if (currentCard) {
                    if (questionSideVisible()) {
                        return currentCard.question();
                    } else {
                        return currentCard.answer();
                    }
                }

                return cardText;
            }),

            dataOptions = {
                results: quiz,
                params: {
                    datePath: datePath,
                    userId: userId()
                }
            },

            activate = function (routeData) {
                var existingCards = ko.observableArray([]);
                
                // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. 
                // (We need the same store because if a card is updated on one ViewModel we need the value to be updated on the other ViewModels too.)
                $.when(dataContext.quizCard.getData(dataOptions), dataContext.card.getData({ results: existingCards }))
                    .then(function () {
                        var quizCards = quiz()[0].cards;
                        cards = ko.observableArray([]);
                        for (var i = 0; i < quizCards.length; i++) {
                            for (var j = 0; j < existingCards().length; j++) {
                                if (quizCards[i].partitionKey() == existingCards()[j].partitionKey() && quizCards[i].rowKey() == existingCards()[j].rowKey()) {
                                    cards().push(existingCards()[j]);
                                    break;
                                }
                            }
                        }

                        userId(quiz()[0].userId);
                        setCurrentCard(routeData.cardId);
                    });
            },

            setCurrentCard = function (cardId) {
                for (var i = 0; i < cards().length; i++) {
                    if (cards()[i].cardId() === parseInt(cardId)) {
                        currentCard.cardId(cards()[i].cardId());

                        var questionText = cards()[i].questionHtml();
                        currentCard.question(questionText);

                        var answerText = cards()[i].answerHtml();
                        currentCard.answer(answerText);

                        var nextCardIndex = i + 1;
                        if (nextCardIndex >= cards().length) {
                            nextCardIndex = 0;
                        }

                        nextCard(getCardUri(nextCardIndex));

                        var previousCardIndex = i - 1;
                        if (previousCardIndex < 0) {
                            previousCardIndex = cards().length - 1;
                        }

                        previousCard(getCardUri(previousCardIndex));

                        questionSideVisible(true);
                        return;
                    }
                }
            },

            flipCard = function () {
                questionSideVisible(!questionSideVisible());
            },

            getCardUri = function (cardIndex) {
                return '#/quizzes/' + userId() + '/' + datePath + '/' + cards()[cardIndex].cardId();
            },

            getQuizResultConfig = function (isCorrect) {
                return {
                    data: {
                        cardId: currentCard.cardId(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: datePath,
                        userId: userId()
                    }
                };
            },

            showNextCard = function () {
                router.navigateTo(nextCard());
            },

            showPreviousCard = function () {
                router.navigateTo(previousCard());
            },

            submitCorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(true));
                showNextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(false));
                showNextCard();
            };

        editCard = function () {
            router.navigateTo('#/cards/' + currentCard.cardId() + '/edit');
        },

    init();

        return {
            userId: userId,
            cards: cards,
            currentCard: currentCard,
            currentCardText: currentCardText,
            questionSideVisible: questionSideVisible,
            activate: activate,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            flipCard: flipCard
        };
    }
);
