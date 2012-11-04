define('model.mapper', ['model'],
    function(model) {
        var card = {
            fromDto: function(dto) {
                var item = new model.Card();
                item.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .question(dto.question)
                    .answer(dto.answer);
                return item;
            }
        };

        return {
            card: card
        };
    }
);