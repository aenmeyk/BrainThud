﻿define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            nextCard = ko.observable(),
            cards = ko.observableArray(),
            currentCard = {
                question: ko.observable(''),
                answer: ko.observable(''),
                text: ko.observable('')
            },
        
            dataOptions = function () {
                return {
                    results: cards
                };
            },

            activate = function (routeData) {
                $.when(dataContext.cards.getData(dataOptions()))
                    .always(setCurrentCard(routeData.rowKey));
            },

            setCurrentCard = function (rowKey) {
                for (var i = 0; i < cards().length; i++) {
                    if(cards()[i].rowKey() === rowKey) {
                        currentCard.question(cards()[i].question());
                        currentCard.answer(cards()[i].answer());
                        currentCard.text(cards()[i].question());

                        var nextCardIndex = i + 1;
                        if (nextCardIndex >= cards().length - 1) {
                            nextCardIndex = 0;
                        }

                        nextCard('#/quiz/' + cards()[nextCardIndex].rowKey());
                        showQuestion();
                        return;
                    }
                }
            },
            
            showQuestion = function () {
                    currentCard.text(currentCard.question());
                    $('#card').removeClass('btn-info');
                    $('#card').addClass('btn-warning');
                },
            
            showAnswer = function () {
                    currentCard.text(currentCard.answer());
                    $('#card').removeClass('btn-warning');
                    $('#card').addClass('btn-info');
                },


            flipCard = function () {
                if(currentCard.text() == currentCard.question()) {
                    showAnswer();
                } else {
                    showQuestion();
                }
            };

        return {
            cards: cards,
            currentCard: currentCard,
            nextCard: nextCard,
            activate: activate,
            flipCard: flipCard
        };
    }
);
