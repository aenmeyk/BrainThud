define('vm.card-info', ['ko', 'model'],
    function (ko, model) {
        var
            card = ko.observable(new model.Card()),

            activate = function (data) {
                card(data);
            };

        return {
            card: card,
            activate: activate
        };
    }
);