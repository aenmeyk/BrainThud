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
            },
            
            updateCommand = ko.asyncCommand({
                execute: function (complete) {
                    var item = ko.toJS(userConfiguration);
                    $.when(dataContext.userConfiguration.updateData({
                            data: item
                        }))
                        .always(function() {
                            complete();
                        });
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && userConfiguration().dirtyFlag().isDirty();
                }
            });


        return {
            activate: activate,
            userConfiguration: userConfiguration,
            updateCommand: updateCommand
        };
    }
);
