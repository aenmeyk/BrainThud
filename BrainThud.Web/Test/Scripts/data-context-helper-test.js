/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="jasmine-test-setup.js" />
/// <reference path="../../Scripts/jquery-1.8.2.js" />
/// <reference path="../../Scripts/app/data-context-helper.js" />

describe("Given a new data-context-helper.EntitySet", function () {
    var dataContextHelper, utils, deferred, EntitySet,
        promiseResult = 'jquery promise result';

    beforeEach(function () {
        spyOn(jQuery, "Deferred").andCallFake(function() {
            deferred = jasmine.createSpyObj('deferred', ['resolve', 'reject', 'promise']);
            deferred.promise.andCallFake(function () {
                return promiseResult;
            });
            return deferred;
        });
        utils = jasmine.createSpyObj('utils', ['hasProperties']);
        utils.hasProperties.andCallFake(function() {
            return true;
        });
        dataContextHelper = modules['data-context-helper'];
        EntitySet = new dataContextHelper(jQuery, utils).EntitySet();
    });
    
    describe("when a getData() is called with no cached results", function () {
        var result,
            optionsResults = function () {};

        beforeEach(function () {
            result = EntitySet.getData({ results: optionsResults });
        });

        it('then the result of the jquery promise is returned', function () {
            expect(result).toEqual(promiseResult);
        });

        it('then the jquery deferred.resolve should be resolved with the options.results', function () {
            expect(deferred.resolve).toHaveBeenCalledWith(optionsResults);
        });
    });
});