define('data-context', ['data-service', 'model.mapper', 'data-context-helper'],
    function (dataService, modelMapper, dataContextHelper) {
        var
            userConfiguration = new dataContextHelper.EntitySet({
                get: dataService.userConfiguration.get,
                update: dataService.userConfiguration.update,
                mapper: modelMapper.userConfiguration,
                showSuccessToastr: true
            }),
            
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                updateBatch: dataService.card.updateBatch,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card,
                showSuccessToastr: true
            }),
            
            cardDeck = new dataContextHelper.EntitySet({
                get: dataService.cardDeck.get,
                mapper: modelMapper.cardDeck
            }),
            
            quizCard = new dataContextHelper.EntitySet({
                get: dataService.card.getForQuiz,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card
            }),
            
            quizResult = new dataContextHelper.EntitySet({
                get: dataService.quizResult.get,
                create: dataService.quizResult.create,
                update: dataService.quizResult.update,
                mapper: modelMapper.quizResult
            });

        return {
            userConfiguration: userConfiguration,
            card: card,
            cardDeck: cardDeck,
            quizCard: quizCard,
            quizResult: quizResult
        };
    }
);