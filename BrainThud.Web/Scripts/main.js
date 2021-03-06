﻿
// TODO: Use pub/sub instead of global
var global = {
    userId: '',
    previousUrl: '',
    currentUrl: '',
    quizMenuUri: ''
};

(function () {
    var root = this;
    defineThirdPartyModules();

    function defineThirdPartyModules() {
        // These modules are already loaded via bundles
        // We define them and put them in the root object
        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
        define('sammy', [], function () { return root.Sammy; });
        define('amplify', [], function () { return root.amplify; });
        define('toastr', [], function () { return root.toastr; });
        define('markdown', [], function () { return root.Markdown; });
        define('moment', [], function () { return root.moment; });
        define('underscore', [], function () { return root._; });
    }

    //requirejs.config({
    //    baseUrl: 'Scripts/app'
    //});

    require(['bootstrapper'],
        function (bootstrapper) {
            if (window.location.pathname !== "/Account/Login") {
                bootstrapper.run();
            }
        });
})();