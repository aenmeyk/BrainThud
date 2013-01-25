define('data-service.user-configuration', ['amplify', 'config'],
    function (amplify, config) {
        var
            init = function () {
                amplify.request.define('user-configuration', 'ajax', {
                    url: config.routes.userConfiguration,
                    dataType: 'json',
                    type: 'GET'
                });
            },
        
            get = function (options) {
                return amplify.request({
                    resourceId: 'user-configuration',
                    success: options.success,
                    error: options.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);
