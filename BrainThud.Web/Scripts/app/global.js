define('global', [],

function () {
    var
        userId = '',
        routePrefix = '',
        previousUrl = '',
        currentUrl = '';

    return {
        userId: userId,
        routePrefix: routePrefix,
        previousUrl: previousUrl,
        currentUrl: currentUrl
    };
});