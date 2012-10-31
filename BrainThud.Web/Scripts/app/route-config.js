define('route-config', ['config', 'vm', 'router'],
    function (config, vm, router) {
        var register = function() {
            var routeData = [
                
                // todaysCards routes
                {
                    view: config.viewIds.todaysCards,
                    routes: [{
                        isDefault: true,
                        route: config.hashes.todaysCards,
                        title: "Today's Cards",
                        callback: vm.cards.activate
                        }]
                },
                
                // createCard routes
                {
                    view: config.viewIds.createCard,
                    routes: [{
                        route: config.hashes.createCard,
                        title: "Create Card",
                        callback: vm.card.activate
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