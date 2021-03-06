﻿define('data-service.user-configuration', ['amplify', 'config'],
    function (amplify, config) {
        var
            init = function () {
                amplify.request.define('user-configuration', 'ajax', {
                    url: config.routes.userConfiguration,
                    dataType: 'json',
                    type: 'GET'
                });
                amplify.request.define('updateUserConfiguration', 'ajax', {
                    url: config.routes.userConfiguration,
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
            },
        
            get = function (options) {
                return amplify.request({
                    resourceId: 'user-configuration',
                    success: options.success,
                    error: options.error
                });
            },

            update = function (options) {
                return amplify.request({
                    resourceId: 'updateUserConfiguration',
                    data: options.data,
                    success: options.success,
                    error: options.error
                });
            };

        init();

        return {
            get: get,
            update: update
        };
    }
);
