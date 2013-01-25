define('vm.nav', ['router', 'utils', 'global'],
    function (router, utils, global) {
        var
            goToCreateCard = function () {
                router.navigateTo(global.routePrefix + 'cards/new');
            },
            goToLibrary = function () {
                router.navigateTo(global.routePrefix + 'library');
            },
            goToQuiz = function () {
                router.navigateTo(global.routePrefix + 'quizzes/' + utils.getDatePath());
            },
            goToUserConfiguration = function () {
                router.navigateTo(global.routePrefix + 'user-configuration');
            },
            goToSignOut = function () {
                router.navigateTo('/Account/SignOut');
            };

        return {
            goToCreateCard: goToCreateCard,
            goToLibrary: goToLibrary,
            goToQuiz: goToQuiz,
            goToUserConfiguration: goToUserConfiguration,
            goToSignOut: goToSignOut
        };
    }
);
