define('route-config', ['config', 'vm', 'router'],
    function (config, vm, router) {
        var register = function() {
            var routeData = [
                
                // editCard routes
                {
                    view: config.viewIds.card,
                    routes: [{
                        route: config.hashes.cardEdit,
                        title: "Card",
                        callback: vm.card.activate
                        }]
                },
                
                // createCard routes
                {
                    view: config.viewIds.createCard,
                    routes: [{
                        isDefault: true,
                        route: config.hashes.createCard,
                        title: "Create Card",
                        callback: vm.createCard.activate
                        }]
                },
                
                // cards routes
                {
                    view: config.viewIds.cards,
                    routes: [{
                        route: config.hashes.cards,
                        title: "Today's Cards",
                        callback: vm.cards.activate
                        }]
                },
                
                // quiz
                {
                    view: config.viewIds.quiz,
                    routes: [{
                        route: config.hashes.quiz,
                        title: "Quiz",
                        callback: vm.quiz.activate
                        }]
                }
                
            ];

            for (var i = 0; i < routeData.length; i++) {
                router.register(routeData[i]);
            }

            // Crank up the router
            router.run();
        };

        return {
            register: register
        };
    }
);