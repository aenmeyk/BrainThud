define('data-context-helper', ['jquery', 'underscore', 'utils', 'amplify'],
    function ($, _, utils, amplify) {
        var EntitySet = function (entitySetConfig) {
            var
                mostRecentGetParams = {},
                cachedResults = [],
                
                getData = function (options) {
                    var def = new $.Deferred();
                    mostRecentGetParams = options.params;

                    if (options.invalidateCache) {
                        cachedResults = [];
                    }

                    var results = options && options.results;
                    if (!cachedResults || !utils.hasProperties(cachedResults)) {
                        entitySetConfig.get({
                            params: options.params,
                            success: function (dto) {
                                cachedResults = [];

                                entitySetConfig.mapper.mapResults(dto, cachedResults);

                                if (results) {
                                    results(cachedResults);
                                }

                                publishCacheChanged();
                                def.resolve(results);
                            },
                            error: function () {
                                if (def.reject) def.reject();
                            }
                        });
                    } else {
                        results(cachedResults);
                        def.resolve(results);
                    }

                    return def.promise();
                },
                
                createData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.create(options.data, {
                            params: options.params,
                            success: function (result) {
                                entitySetConfig.mapper.mapResults([result], cachedResults);
                                publishCacheChanged();
                                def.resolve();
                            },
                            error: function () {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },
                
                updateData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.update(options.data, {
                            params: options.params,
                            success: function (dto) {
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === dto.partitionKey && cachedResults[i].rowKey() === dto.rowKey) {
                                        cachedResults[i] = entitySetConfig.mapper.mapResult(dto);
                                        break;
                                    }
                                }

                                publishCacheChanged();
                                def.resolve();
                            },
                            error: function () {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },
                
                deleteData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.deleteItem({
                            params: options.params,
                            success: function () {
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === options.params.partitionKey && cachedResults[i].rowKey() === options.params.rowKey) {
                                        cachedResults.splice(i, 1);
                                        break;
                                    }
                                }

                                publishCacheChanged();
                                def.resolve();
                            },
                            error: function () {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },

                publishCacheChanged = function () {
                    if (entitySetConfig.cardChangedPub) {
                        amplify.publish(entitySetConfig.cardChangedPub, cachedResults);
                    }
                },
                
                refreshCache = function() {
                    getData({
                        invalidateCache: true,
                        params: mostRecentGetParams
                    });
                };
            
            return {
                getData: getData,
                createData: createData,
                updateData: updateData,
                deleteData: deleteData,
                refreshCache: refreshCache
            };
        };

        return {
            EntitySet: EntitySet
        };
    });