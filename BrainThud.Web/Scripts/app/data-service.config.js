define('data-service.config', ['amplify'],
    function (amplify) {
        var
            init = function () {
                amplify.request.define('config', 'ajax', {
                    url: '/api/config',
                    dataType: 'json',
                    type: 'GET'
                });
            },
        
            get = function (config) {
                return amplify.request({
                    resourceId: 'config',
                    success: config.success,
                    error: config.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);
