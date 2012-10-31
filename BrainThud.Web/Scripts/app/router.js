define('router', ['jquery', 'sammy', 'presenter'],
    function ($, Sammy, presenter) {
        var startupUrl = '',
            defaultRoute = '',

            navigateTo = function (url) {
                sammy.setLocation(url);
            },

            sammy = new Sammy.Application(function () {
                if (Sammy.Title) {
                    this.use(Sammy.Title);
                    this.setTitle(config.title);
                }

                this.get('', function () {
                    this.app.runRoute('get', startupUrl);
                });
            }),

            register = function (options) {
                if (options.routes) {
                    // Register a list of routes
                    for (var i = 0; i < options.routes.length; i++) {
                        var route = options.routes[i];
                        registerRoute({
                            isDefault: route.isDefault,
                            route: route.route,
                            title: route.title,
                            callback: route.callback,
                            view: options.view,
                        });
                    }

                    //                    _.each(options.routes, function (route) {
                    //                        registerRoute({
                    //                            route: route.route,
                    //                            title: route.title,
                    //                            callback: route.callback,
                    //                            view: options.view,
                    //                        });
                    //                    });

                    return;
                }

                // Register 1 route
                registerRoute(options);
            },

            registerRoute = function (options) {
                if (options.isDefault) {
                    defaultRoute = options.route;
                }

                sammy.get(options.route, function (context) {
                    options.callback();
                    $('.view').hide();
                    presenter.transitionTo($(options.view));
                });
            },

            run = function () {

                // 1) if i browse to a location, use it
                // 2) otherwise use the default route
                startupUrl = sammy.getLocation() || defaultRoute;

                if (!startupUrl) {
                    return;
                }
                sammy.run();
//                registerBeforeLeaving();
                navigateTo(startupUrl);
            };

        return {
            navigateTo: navigateTo,
            register: register,
            run: run
        };
    }
);