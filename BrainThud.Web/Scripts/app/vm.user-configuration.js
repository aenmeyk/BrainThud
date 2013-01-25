define('vm.user-configuration', ['ko', 'data-context', 'model', 'global'],
    function (ko, dataContext, model, global) {
        var
            userConfiguration = ko.observable(new model.UserConfiguration()),
            
            activate = function () {
                var userConfigurations = ko.observableArray([]);
                $.when(dataContext.userConfiguration.getData({
                        results: userConfigurations,
                        params: {
                            userId: global.userId
                        }
                    }))
                    .done(function() {
                        userConfiguration(userConfigurations()[0]);
                    });
            };

        return {
            activate: activate,
            userConfiguration: userConfiguration
        };
    }
);
