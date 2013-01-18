define('router', ['jquery', 'underscore', 'sammy', 'presenter', 'config', 'global'],
    function ($, _, Sammy, presenter, config, global) {
        var startupUrl = '',

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
                    _.each(options.routes, function(route) {
                        registerRoute({
                            isDefault: route.isDefault,
                            route: route.route,
                            title: route.title,
                            callback: route.callback,
                            view: options.view,
                        });
                    });

                    return;
                }

                // Register 1 route
                registerRoute(options);
            },

            registerRoute = function (options) {
                sammy.get(options.route, function (context) {
                    global.previousUrl = global.currentUrl;
                    global.currentUrl = sammy.last_location[1];
                    options.callback(context.params);
                    $('.view').hide();
                    presenter.transitionTo($(options.view));
                });
            },
            
            getDefaultRoute = function() {
                return global.routePrefix + 'cards/new';
            },

            run = function () {

                // 1) if i browse to a location, use it
                // 2) otherwise use the default route
                startupUrl = sammy.getLocation() || getDefaultRoute();

                if (!startupUrl) {
                    return;
                }
                sammy.run();
                navigateTo(startupUrl);
            };

        return {
            navigateTo: navigateTo,
            register: register,
            run: run
        };
    }
);