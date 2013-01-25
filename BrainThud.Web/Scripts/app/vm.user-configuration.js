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
            
            updateUserConfiguration = function () {
                var item = ko.toJS(userConfiguration);
                $.when(dataContext.userConfiguration.updateData({
                    data: item
                }))
                .then(function () {
                    toastr.success('Success!');
                });
            },

            updateCommand = ko.asyncCommand({
                execute: function (complete) {
                    $.when(updateUserConfiguration())
                        .always(complete);
                    return;
                },
                canExecute: function (isExecuting) {
                    return true;
                }
            });

        return {
            activate: activate,
            userConfiguration: userConfiguration,
            updateCommand: updateCommand
        };
    }
);
