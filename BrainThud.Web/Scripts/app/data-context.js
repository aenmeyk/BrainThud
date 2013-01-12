define('data-context', ['data-service', 'model.mapper', 'data-context-helper', 'data-subs', 'config'],
    function (dataService, modelMapper, dataContextHelper, dataSubs, config) {
        var
            userConfig = new dataContextHelper.EntitySet({
                get: dataService.config.get,
                mapper: modelMapper.config
            }),
            
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card,
                cardChangedPub: config.pubs.cardCacheChanged
            }),
            
            quizCards = new dataContextHelper.EntitySet({
                get: dataService.card.getForQuiz,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card
            }),
            
            quizResult = new dataContextHelper.EntitySet({
                get: dataService.quizResult.get,
                create: dataService.quizResult.create,
                update: dataService.quizResult.update,
                mapper: modelMapper.quizResult,
                cardChangedPub: config.pubs.quizResultCacheChanged
            });

        return {
            config: userConfig,
            card: card,
            quizCards: quizCards,
            quizResult: quizResult
        };
    }
);