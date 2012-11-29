﻿define('gestures', ['vm'],
    function (vm) {
        var 
            register = function() {
                $('#card').swipe({
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