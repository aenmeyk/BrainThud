define('router', ['jquery', 'ko', 'underscore', 'sammy', 'presenter', 'config', 'global', 'utils'],
    function ($, ko, _, Sammy, presenter, config, global, utils) {
        var
            startupUrl = '',
            userHasCards = ko.observable(false),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    userHasCards(data.length > 0);
                });
            },

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
                if (userHasCards()) {
                    return global.routePrefix + 'quizzes/' + utils.getDatePath();
                } else {
                    return global.routePrefix + 'cards/new';
                }
            },
        

        run = function () {

            // 1) if I browse to a location, use it
            // 2) otherwise if I have cards, go to the quiz
            // 3) otherwise go to the Create Card view
            startupUrl = sammy.getLocation() || getDefaultRoute();

            if (!startupUrl) {
                return;
            }
            
            sammy.run();
            navigateTo(startupUrl);
        };


        init();

        return {
            navigateTo: navigateTo,
            register: register,
            run: run
        };
    }
);