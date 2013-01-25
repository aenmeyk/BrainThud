define('data-context', ['data-service', 'model.mapper', 'data-context-helper', 'data-subs', 'config'],
    function (dataService, modelMapper, dataContextHelper, dataSubs, config) {
        var
            userConfiguration = new dataContextHelper.EntitySet({
                get: dataService.userConfiguration.get,
                update: dataService.userConfiguration.update,
                mapper: modelMapper.userConfiguration
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
                mapper: modelMapper.card,
                cardChangedPub: config.pubs.quizCardCacheChanged
            }),
            
            quizResult = new dataContextHelper.EntitySet({
                get: dataService.quizResult.get,
                create: dataService.quizResult.create,
                update: dataService.quizResult.update,
                mapper: modelMapper.quizResult,
                cardChangedPub: config.pubs.quizResultCacheChanged
            });

        return {
            userConfiguration: userConfiguration,
            card: card,
            quizCards: quizCards,
            quizResult: quizResult
        };
    }
);