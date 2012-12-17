/// <reference path="../../Scripts/qunit.js" />
/// <reference path="../../Scripts/app/binder.js" />

var bindingsApplied = false;

var $ = function(viewName) {
    var get = function(value) {
        if (value == config.viewIds.card) {
            return 'Correct view returned';
        } else {
            return 'Incorrect view returned';
        }
    };
    return {
        get: get
    };
};

var setViewModel,
    setRootNode;

var ko = function () {
    
    var
        applyBindings = function (viewModel, rootNode) {
            setViewModel = viewModel;
            setRootNode = rootNode;
            bindingsApplied = true;
        };
    return {
        applyBindings: applyBindings,
    };
}();

var vm = {
    card: 'Card ViewModel'
};
var config = {
    viewIds: {
        card: 'Card View ID'   
    }
};

(function () {
    module('Given a binder module');

    test('When bind() is called', function() {
        itemToTest($, ko, vm, config).bind();
        equal(setViewModel, vm.card, "Then the card view model should be bound to the card view");
    });
})();