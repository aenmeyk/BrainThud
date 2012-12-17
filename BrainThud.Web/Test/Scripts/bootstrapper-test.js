/// <reference path="../../Scripts/qunit.js" />
/// <reference path="test-setup.js" />
/// <reference path="../../Scripts/app/bootstrapper.js" />

// Setup mocks
var
    dataPrimerFetchWasCalled = false,
    binderBindWasCalled = false,
    routeConfigRegisterWasCalled = false,
    jqueryDone = {
        done: function(callback) {
            callback();
            return jqueryDone;
        }
    },
    $ = {
        when: function (value) {
            return jqueryDone;
        }
    },
    binder = {
        bind: function() {
            binderBindWasCalled = true;
        }
    },
    routeConfig = {
        register: function () {
            routeConfigRegisterWasCalled = true;
        }
    },
    dataPrimer = {
        fetch: function() {
            dataPrimerFetchWasCalled = true;
        }
    };

// Tests
(function () {
    module('Given a bootstrapper module, when run() is called', {
        setup: function () {
            itemToTest($, binder, routeConfig, dataPrimer).run();
        }
    });

    test('Then then fetch() is called on dataPrimer', function () {
        ok(dataPrimerFetchWasCalled);
    });

    test('Then then bind() is called on binder', function () {
        ok(binderBindWasCalled);
    });

    test('Then then register() is called on routeConfig', function () {
        ok(routeConfigRegisterWasCalled);
    });

})();