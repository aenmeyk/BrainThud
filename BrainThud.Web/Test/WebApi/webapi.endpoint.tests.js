(function () {
    QUnit.config.testTimeout = 10000;

    var stringformat = QUnit.stringFormat;

    module('Web API GET Endpoints respond successfully');

    var apiUrls = ['/api/nuggets/'];

    var apiUrlsLength = apiUrls.length;

    // Test only that the Web API responded to the request with 'success'
    var endpointTest = function (url) {
        $.ajax({
            url: url,
            dataType: 'json',
            success: function (result) {
                ok(true, 'GET succeeded for ' + url);
                ok(!!result, 'GET retrieved some data');
                start();
            },
            error: function (result) {
                ok(false,
                    stringformat('GET on \'{0}\' failed with status=\{1}\': {2}',
                        url, result.status, result.responseText));
                start();
            }
        });
    };

    // Returns an endpointTest function for a given URL
    var endpointTestGenerator = function (url) {
        return function () { endpointTest(url); };
    };

    for (var i = 0; i < apiUrlsLength; i++) {
        var apiUrl = apiUrls[i];
        asyncTest('API can be reached: ' + apiUrl, endpointTestGenerator(apiUrl));
    };

})();