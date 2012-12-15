define('gestures', ['vm'],
    function (vm) {
        var 
            register = function() {
                $('#todays-quiz-view').swipe({
                    swipeLeft: function (event, direction, distance, duration, fingerCount) {
                        vm.quiz.showNextCard();
                    },
                    swipeRight: function (event, direction, distance, duration, fingerCount) {
                        vm.quiz.showPreviousCard();
                    },
                    click: function(event, target) {
                        vm.quiz.flipCard();
                    },
                    threshold: 75
                });
            };

        return {
            register: register
        };
    }
);