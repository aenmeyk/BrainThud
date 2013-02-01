define('vm.library', ['ko', 'card-manager', 'underscore', 'amplify', 'config', 'router', 'global', 'data-context'],
    function (ko, cardManager, _, amplify, config, router, global, dataContext) {
        var
            selectedDeckSlug = ko.observable(''),

            selectedDeckName = ko.computed(function () {
                var slug = selectedDeckSlug(),
                    card = _.find(cardManager.cards(), function (item) {
                        return item.deckNameSlug() === slug;
                    });

                if (card) {
                    return card.deckName();
                } else {
                    return "Card Decks";
                }
            }),

            cardDecks = ko.computed(function () {
                var sortedCards = _.sortBy(cardManager.cards(), function (item) {
                    return item.deckName().toLowerCase();;
                });

                return _.uniq(sortedCards, true, function (item) {
                    return item.deckName();
                });
            }),

            filteredCards = ko.computed(function () {
                var slug = selectedDeckSlug();
                return _.filter(cardManager.cards(), function (item) {
                    return item.deckNameSlug() === slug;
                });
            }),

            activate = function (routeData) {
                if (routeData.deckNameSlug) {
                    selectedDeckSlug(routeData.deckNameSlug);
                } else if (cardDecks().length > 0) {
                    var sortedCards = _.sortBy(cardManager.cards(), function (item) {
                        return item.deckName().toLowerCase();
                    });
                
                    navigateToSlug(sortedCards[0].deckNameSlug());
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

            showDeleteDialog = function (card) {
                cardManager.deleteCard(card);
            },

            showCardInfoDialog = function (card) {
                cardManager.showCardInfo(card);
            };

        return {
            cardDecks: cardDecks,
            filteredCards: filteredCards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            filterCards: filterCards,
            selectedDeckName: selectedDeckName,
            isDeckSelected: isDeckSelected,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog
        };
    }
);