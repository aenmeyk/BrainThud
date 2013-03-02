define('vm.library', ['ko', 'card-manager', 'underscore', 'router', 'global'],
    function (ko, cardManager, _, router, global) {
        var
            selectedDeckSlug = ko.observable(''),
            
            isCheckedForBatch = ko.observable(false),
            
            filteredCards = ko.computed(function() {
                var slug = selectedDeckSlug();
                return _.filter(cardManager.cards(), function(item) {
                    return item.deckNameSlug() === slug;
                });
            }),

            selectedDeckName = ko.computed(function () {
                var slug = selectedDeckSlug(),
                    cardDeck = _.find(cardManager.cardDecks(), function (item) {
                        return item.deckNameSlug() === slug;
                    });

                if (cardDeck) {
                    return cardDeck.deckName();
                } else {
                    return "Card Decks";
                }
            }),

            cardDecks = ko.computed(function () {
                return cardManager.cardDecks();
            }),

            activate = function (routeData) {
                cardManager.getCardDecks();

                // 1) Navigate to the deck specified in the route
                // 2) If no deck specified in route, navigate to the previously selected deck
                // 3) If no previously selected deck, navigate to the first deck
                // 4) If there are no cards, clear the selected deck
                if (routeData.deckNameSlug) {
                    selectedDeckSlug(routeData.deckNameSlug);
                } else if (selectedDeckSlug() !== '') {
                    navigateToSlug(selectedDeckSlug());
                } else if (cardManager.cardDecks().length > 0) {
                    navigateToSlug(cardManager.cardDecks()[0].deckNameSlug());
                } else {
                    selectedDeckSlug('');
                }
            },

            editCard = function (card) {
                router.navigateTo(global.routePrefix + 'cards/' + card.entityId() + '/edit');
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            filterCards = function (deckNameSlug) {
                var $cardDeckName = $('.card-deck-name');
                
                if ($cardDeckName.is(":visible")) {
                    $cardDeckName.click();
                    setTimeout(function () { navigateToSlug(deckNameSlug); }, 400);
                } else {
                    navigateToSlug(deckNameSlug);
                }
            },
            
            navigateToSlug = function(slug) {
                router.navigateTo(global.routePrefix + 'library/' + slug);
            },

            isDeckSelected = function (deckNameSlug) {
                return selectedDeckSlug() === deckNameSlug;
            },

            toggleIsCheckedForBatch = function () {
                _.each(filteredCards(), function(item) {
                    item.isCheckedForBatch(!isCheckedForBatch());
                });
            },

            showDeleteDialog = function (card) {
                cardManager.deleteCard(card);
            },

            showCardInfoDialog = function (card) {
                cardManager.showCardInfo(card);
            };

        selectedDeckSlug.subscribe(function (deckNameSlug) {
            if (deckNameSlug) {
                cardManager.getCards(deckNameSlug);
            }
        }.bind(this));

        return {
            cardDecks: cardDecks, 
            filteredCards: filteredCards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            filterCards: filterCards,
            selectedDeckName: selectedDeckName,
            isDeckSelected: isDeckSelected,
            isCheckedForBatch: isCheckedForBatch,
            toggleIsCheckedForBatch: toggleIsCheckedForBatch,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog
        };
    }
);