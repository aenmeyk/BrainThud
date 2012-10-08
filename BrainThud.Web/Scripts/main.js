(function () {

    var root = this;
    defineThirdPartyModules();

    function defineThirdPartyModules() {
        // These modules are already loaded via bundles
        // We define them and put them in the root object
        define('jquery', [], function() { return root.jQuery; });
        define('ko', [], function() { return root.ko; });
    }

    //requirejs.config({
    //    baseUrl: 'Scripts/app'
    //});

    require(['bootstrapper'],
        function(bootstrapper) {
            bootstrapper.run();
        });
})();