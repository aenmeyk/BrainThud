(function() {
    requirejs.config({
        baseUrl: 'Scripts/app'
    });

    require(['bootstrapper'],
        function(bootstrapper) {
            bootstrapper.run();
        });
})();