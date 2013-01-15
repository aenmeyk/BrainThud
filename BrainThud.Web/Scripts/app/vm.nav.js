define('vm.nav', ['router', 'utils', 'global'],
    function (router, utils, global) {
        var
            goToCreateCard = function () {
                router.navigateTo('#/cards/new');
            },
            goToLibrary = function () {
                router.navigateTo('#/library');
            },
            goToQuiz = function () {
                router.navigateTo('#/quizzes/' + global.userId + '/' + utils.getDatePath());
            },
            goToSignOut = function () {
                router.navigateTo('/Account/SignOut');
            };

        return {
            goToCreateCard: goToCreateCard,
            goToLibrary: goToLibrary,
            goToQuiz: goToQuiz,
            goToSignOut: goToSignOut
        };
    }
);
