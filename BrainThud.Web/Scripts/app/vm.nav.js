define('vm.nav', ['router', 'utils', 'global'],
    function (router, utils, global) {
        var
            goToCreateCard = function () {
                router.navigateTo('#/cards/new');
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
