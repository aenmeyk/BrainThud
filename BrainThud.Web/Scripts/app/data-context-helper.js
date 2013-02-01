define('data-context-helper', ['jquery', 'underscore', 'utils', 'amplify'],
    function ($, _, utils, amplify) {
        
        var EntitySet = function (entitySetConfig) {
            var
                cachedResults,
                mostRecentGetParams,
                isCacheInvalid = false,
                
                getData = function (options) {
                    var def = new $.Deferred();
                    mostRecentGetParams = options.params;

                    if (options.invalidateCache || isCacheInvalid) {
                        cachedResults = undefined;
                    }

                    var results = options && options.results;
                    if (!cachedResults) {
                        entitySetConfig.get({
                            params: options.params,
                            success: function (dto) {
                                cachedResults = [];

                                entitySetConfig.mapper.mapResults(dto, cachedResults);
                                isCacheInvalid = false;

                                if (results) {
                                    results(cachedResults);
                                }

                                publishCacheChanged();
                                def.resolve(results);
                            },
                            error: function () {
                                toastr.error('An error occurred');
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
                        entitySetConfig.create(options.data, cachedResults, {
                            params: options.params,
                            success: function (result) {
                                var newItem = entitySetConfig.mapper.mapResult(result);
                                cachedResults.push(newItem);
                                publishCacheChanged();
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                def.resolve(newItem);
                            },
                            error: function () {
                                toastr.error('An error occurred');
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
                                var updatedItem;
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === dto.partitionKey && cachedResults[i].rowKey() === dto.rowKey) {
                                        cachedResults[i] = entitySetConfig.mapper.mapResult(dto);
                                        updatedItem = cachedResults[i];
                                        break;
                                    }
                                }
                                
                                options.data.dirtyFlag().reset();
                                publishCacheChanged();
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                if (options.callback) options.callback(cachedResults[i]);
                                def.resolve(updatedItem);
                            },
                            error: function () {
                                toastr.error('An error occurred');
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },
                
                deleteData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.deleteItem({
                            data: options.data,
                            success: function () {
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === options.data.partitionKey && cachedResults[i].rowKey() === options.data.rowKey) {
                                        cachedResults.splice(i, 1);
                                        break;
                                    }
                                }

                                publishCacheChanged();
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                def.resolve();
                            },
                            error: function () {
                                toastr.error('An error occurred');
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },

                updateCachedItem = function (item) {
                    if (cachedResults && cachedResults.length > 0) {
                        for (var i = 0; i < cachedResults.length; i++) {
                            if (cachedResults[i].partitionKey() === item.partitionKey() && cachedResults[i].rowKey() === item.rowKey()) {
                                cachedResults[i] = item;
                                break;
                            }
                        }

                        publishCacheChanged();
                    }
                },

                removeCachedItem = function (item) {
                    if (cachedResults && cachedResults.length > 0) {
                        for (var i = 0; i < cachedResults.length; i++) {
                        if (cachedResults[i].partitionKey() === item.partitionKey() && cachedResults[i].rowKey() === item.rowKey()) {
                            cachedResults.splice(i, 1);
                            break;
                        }
                    }

                    publishCacheChanged();
                    }
                },

                publishCacheChanged = function () {
                    if (entitySetConfig.cacheChangedPub) {
                        amplify.publish(entitySetConfig.cacheChangedPub, cachedResults);
                    }
                },
                
                refreshCache = function () {
                    if (mostRecentGetParams) {
                        getData({
                            invalidateCache: true,
                            params: mostRecentGetParams
                        });
                    }
                },
                
                setCacheInvalid = function () {
                    isCacheInvalid = true;
                };
            
            return {
                getData: getData,
                createData: createData,
                updateData: updateData,
                deleteData: deleteData,
                updateCachedItem: updateCachedItem,
                removeCachedItem: removeCachedItem,
                refreshCache: refreshCache,
                setCacheInvalid: setCacheInvalid
            };
        };

        return {
            EntitySet: EntitySet
        };
    });