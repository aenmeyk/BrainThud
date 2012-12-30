define('vm.nav', ['router', 'utils'],
    function (router, utils) {
        var
            goToCreateCard = function () {
                router.navigateTo('#/create-card');
            },
            goToCards = function () {
                router.navigateTo('#/cards');
            },
            goToQuiz = function () {
                router.navigateTo('#/quizzes/' + global.userId + '/' + utils.getDatePath());
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
