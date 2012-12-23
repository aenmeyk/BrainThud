define('vm.nav', ['router'],
    function (router) {
        var
            goToCreateCard = function () {
                router.navigateTo('#/create-card');
            },
            goToCards = function() {
                router.navigateTo('#/cards');
            },
            goToTodaysQuiz = function() {
                router.navigateTo(global.quizMenuUri);
            },
            goToSignOut = function() {
                router.navigateTo('/Account/SignOut');
            };

        return {
            goToCreateCard: goToCreateCard,
            goToCards: goToCards,
            goToTodaysQuiz: goToTodaysQuiz,
            goToSignOut: goToSignOut
        };
    }
);
