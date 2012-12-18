/// <reference path="../../Scripts/qunit.js" />
/// <reference path="test-setup.js" />
/// <reference path="../../Scripts/app/data-context.js" />

// Setup mocks
var
    cashedResultsAreMappedToResults = false,
    entitySetMock = {
        get: function (callbacks) {
            callbacks.success(dto);
        },
        create: function (data, callbacks) {
            callbacks.success(dto);
        },
        update: function (data, callbacks) {
            callbacks.success(dto);
        },
        mapper: {
            mapResults: function (dto, cachedResults) {
            
            }
        },
    },

    $ = {
        Deferred: function () {
            return {
                promise: function () {

                },
                resolve: function (results) {

                }
            };
        }
    },

    dataService = {
        card: entitySetMock,
        quiz: entitySetMock,
        quizResult: entitySetMock
    },

    utils = {
        hasProperties: function (results) {

        }
    },

    model = {
    },

    modelMapper = {
        cardHtml: {
            mapResults: function (dto, cachedResults) {
                cashedResultsAreMappedToResults = true;
            }
        }
    },

    options = {
        results: function (cachedResults) {

        }
    },

    dto = {

    };

// Tests
(function () {
    var result;

    module('Given a data-context.cards entitSet, when getData is called', {
        setup: function () {
            result = itemToTest($, dataService, utils, model, modelMapper).cards.getData();
        }
    });

    test('Then the cached results are mapped to the actual results', function () {
        ok(cashedResultsAreMappedToResults);
    });
})();