define('vm.nav', ['router'],
    function (router) {
        var
            goToCreateCard = function () {
                router.navigateTo('#/create-card');
            },
            goToCards = function () {
                router.navigateTo('#/cards');
            },
            goToQuiz = function () {
//                router.navigateTo('#/quizzes/19/2012/12/24/156');
                router.navigateTo(global.quizMenuUri);
            },
            goToSignOut = function () {
                router.navigateTo('/Account/SignOut');
            };

        return {
            goToCreateCard: goToCreateCard,
            goToCards: goToCards,
            goToQuiz: goToQuiz,
            goToSignOut: goToSignOut
        };
    }
);
