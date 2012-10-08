define('vm.nugget', ['jquery', 'ko'],
    function ($, ko) {
        
        var question = ko.observable('How long is a piece of string?');
        
        return {
            question: question
        };
    }
);