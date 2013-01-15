define('vm.library', ['ko', 'underscore', 'amplify', 'config', 'router'],
    function (ko, _, amplify, config, router) {
        var
            selectedDeck = ko.observable(''),
            cards = ko.observableArray([]),
            
            cardDecks = ko.computed(function() {
                var sortedCards = _.sortBy(cards(), function (item) {
                        return item.deckName();
                    });

                return _.uniq(sortedCards, true, function(item) {
                    return item.deckName();
                });
            }),
            
            filteredCards = ko.computed(function () {
                var deck = selectedDeck();
                return _.filter(cards(), function (item) {
                    return item.deckNameSlug() === deck;
                });
            }),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);
                    
                    if (data && data.length > 0) {
                        var sortedCards = _.sortBy(data, function (item) {
                            return item.deckName();
                        });

                        selectedDeck(sortedCards[0].deckNameSlug());
                    } else {
                        selectedDeck('');
                    }
                });
            },

            editCard = function (card) {
                amplify.publish(config.pubs.showEditCard, card.entityId());
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            activate = function (routeData) {
                if (routeData.deckNameSlug) {
                    selectedDeck(routeData.deckNameSlug);
                }
            },
            
            filterCards = function (deckNameSlug) {
                router.navigateTo('#/library/' + deckNameSlug);
            },
            
            isDeckSelected = function (deckNameSlug) {
                return selectedDeck() === deckNameSlug;
            },

            showDeleteDialog = function (card) {
                amplify.publish(config.pubs.showDeleteCard, card);
            };

        init();

        return {
            cardDecks: cardDecks,
            filteredCards: filteredCards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            filterCards: filterCards,
            isDeckSelected: isDeckSelected,
            showDeleteDialog: showDeleteDialog
        };
    }
);