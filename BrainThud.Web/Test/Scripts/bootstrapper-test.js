/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="jasmine-test-setup.js" />
/// <reference path="../../Scripts/jquery-1.8.2.js" />
/// <reference path="../../Scripts/app/bootstrapper.js" />

describe("A bootstrapper module, when run() is called", function () {
    var binder, routeConfig, dataPrimer, $;

    beforeEach(function () {
        var done = function(callback) {
            callback();
            return {
                done: done
            };
        };

        spyOn(jQuery, "when").andCallFake(function () {
            return {
                done: done
            };
        });

        binder = jasmine.createSpyObj('binder', ['bind']);
        routeConfig = jasmine.createSpyObj('routeConfig', ['register']);
        dataPrimer = jasmine.createSpyObj('dataPrimer', ['fetch']);

        modules['bootstrapper'](jQuery, binder, routeConfig, dataPrimer).run();
    });

    it('data should be fetched from the dataPrimer', function () {
        expect(dataPrimer.fetch).toHaveBeenCalled();
    });

    it('bind should be called on the the binder', function () {
        expect(binder.bind).toHaveBeenCalled();
    });

    //it('register should be called on the the routeConfig', function () {
    //    expect(routeConfig.register).toHaveBeenCalled();
    //});
});