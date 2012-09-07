(function () {
    QUnit.config.testTimeout = 10000;

    var stringformat = QUnit.stringFormat;

    module('Web API GET Endpoints respond successfully');

    var apiUrls = ['/api/nuggets/'];

    var apiUrlsLength = apiUrls.length;

    // Step 1: Get the nugget
    function getTestNugget(succeed) {
        var msgPrefix = 'GET' + testMsgBase;
        $.ajax({
            type: 'GET',
            url: testUrl,
            dataType: 'json',
            success: function (result) {
                onCallSuccess(msgPrefix);
                okAsync(result.rowKey === testRowKey, "returned key matches testNugget RowKey.");
                if (typeof succeed !== 'function') {
                    start(); // no 'succeed' callback; end of the line
                    return;
                } else {
                    succeed(result);
                }
                ;
            },
            error: function (result) { onError(result, msgPrefix); }
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