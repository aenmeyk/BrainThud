define('model', ['ko'],

function (ko) {
    var card = function() {
        return {
            partitionKey: ko.observable(),
            rowKey: ko.observable(),
            question: ko.observable(),
            answer: ko.observable(),
            additionalInformation: ko.observable()
        };
    };

    return {
        card: card
    };
});