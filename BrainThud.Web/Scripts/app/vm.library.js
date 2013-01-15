define('vm.library', ['ko', 'underscore', 'amplify', 'config', 'router', 'sammy'],
    function (ko, _, amplify, config, router, Sammy) {
        var
            selectedDeck = ko.observable(''),
            cards = ko.observableArray([]),
            
            cardDecks = ko.computed(function() {
                var deckNames = _.map(cards(), function(item) {
                        return item.deckName();
                    }),
                    sortedNames = _.sortBy(deckNames, function(item) {
                        return item;
                    });

                return _.uniq(sortedNames, true);
            }),
            
            filteredCards = ko.computed(function () {
                var deck = selectedDeck();
                return _.filter(cards(), function (item) {
                    return item.deckName() === deck;
                });
            }),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);
                    
                    if (data && data.length > 0) {
                        var sortedCards = _.sortBy(data, function (item) {
                            return item.deckName();
                        });

                        selectedDeck(sortedCards[0].deckName());
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
                if (routeData.deckName) {
                    selectedDeck(routeData.deckName);
                }
            },
            
            filterCards = function(deckName) {
                router.navigateTo('#/library/' + deckName);
            },
            
            isDeckSelected = function (deckName) {
                return selectedDeck() === deckName;
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