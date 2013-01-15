define('route-config', ['underscore', 'config', 'vm', 'router'],
    function (_, config, vm, router) {
        var register = function() {
            var routeData = [
                
                {
                    view: config.viewIds.card,
                    routes: [{
                        title: "Card",
                        route: config.hashes.cardEdit,
                        callback: vm.card.activate
                        }]
                },
                
                {
                    view: config.viewIds.createCard,
                    routes: [{
                        isDefault: true,
                        title: "Create Card",
                        route: config.hashes.createCard,
                        callback: vm.createCard.activate
                        }]
                },
                
                {
                    view: config.viewIds.library,
                    routes: [{
                        title: "Library",
                        route: config.hashes.library,
                        callback: vm.library.activate
                        }]
                },
                
                {
                    view: config.viewIds.library,
                    routes: [{
                        title: "Card Deck",
                        route: config.hashes.deck,
                        callback: vm.library.activate
                        }]
                },
                
                {
                    view: config.viewIds.quiz,
                    routes: [{
                        title: "Quiz",
                        route: config.hashes.quiz,
                        callback: vm.quiz.activate
                        }]
                },
                
                {
                    view: config.viewIds.quizCard,
                    routes: [{
                        title: "QuizCard",
                        route: config.hashes.quizCard,
                        callback: vm.quizCard.activate
                        }]
                }];

            _.each(routeData, function (route) {
                router.register(route);
            });

            router.run();
        };

        return {
            register: register
        };
    }
);