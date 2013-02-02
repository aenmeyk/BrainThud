define('vm.library', ['ko', 'underscore', 'amplify', 'config', 'router', 'global', 'data-context'],
    function (ko, _, amplify, config, router, global, dataContext) {
        var
            selectedDeckSlug = ko.observable(''),
            cards = ko.observableArray([]),

            selectedDeckName = ko.computed(function () {
                var slug = selectedDeckSlug(),
                    card = _.find(cards(), function (item) {
                        return item.deckNameSlug() === slug;
                    });

                if (card) {
                    return card.deckName();
                } else {
                    return "Card Decks";
                }
            }),

            cardDecks = ko.computed(function () {
                var sortedCards = _.sortBy(cards(), function (item) {
                    return item.deckName().toLowerCase();;
                });

                return _.uniq(sortedCards, true, function (item) {
                    return item.deckName();
                });
            }),

            filteredCards = ko.computed(function () {
                var slug = selectedDeckSlug();
                return _.filter(cards(), function (item) {
                    return item.deckNameSlug() === slug;
                });
            }),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);

                    if (data && data.length > 0) {
                        var sortedCards = _.sortBy(data, function (item) {
                            return item.deckName().toLowerCase();;
                        });

                        selectedDeckSlug(sortedCards[0].deckNameSlug());
                    } else {
                        selectedDeckSlug('');
                    }
                });
            },

            activate = function (routeData) {
                if (routeData.deckNameSlug) {
                    selectedDeckSlug(routeData.deckNameSlug);
                }
                dataContext.card.getData({
                    results: cards
                });
            },

            editCard = function (card) {
                amplify.publish(config.pubs.showEditCard, card.entityId());
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
                amplify.publish(config.pubs.showDeleteCard, card);
            },

            showCardInfoDialog = function (card) {
                amplify.publish(config.pubs.showCardInfo, card);
            };

        init();

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