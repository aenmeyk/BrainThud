define('vm.quiz', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            nextCard = ko.observable(),
            cards = ko.observableArray([]),
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

//            activate = function (routeData) {
//                $.when(dataContext.quiz.getData(dataOptions()))
//                    .always(function () {
//                         console.log(cards().length);
//                         console.log('setCurrentCard');
//                    });
//            },
            activate = function (routeData) {
                var deferred = dataContext.quiz.getData(dataOptions());
                $.when(deferred)
                    .then(setCurrentCard(routeData.rowKey));
            },

            setCurrentCard = function (rowKey) {
                console.log('rowKey ' + rowKey);
                console.log('setCurrentCard ' + cards().length);
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
                    $('#card').removeClass('answer');
                    $('#card').addClass('question');
                },
            
            showAnswer = function () {
                    currentCard.text(currentCard.answer());
                    $('#card').removeClass('question');
                    $('#card').addClass('answer');
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
