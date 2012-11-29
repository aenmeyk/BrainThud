define('gestures', ['vm'],
    function (vm) {
        var 
            register = function() {
                $('#card').swipe({
                    swipe: function (event, direction, distance, duration, fingerCount) {
                        if (direction === 'left') {
                            vm.quiz.showNextCard();
                        } else if (direction === 'right') {
                            vm.quiz.showPreviousCard();
                        }
                    }
                });
            };

        return {
            register: register
        };
    }
);