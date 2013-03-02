define('data-context-helper', ['jquery'],
    function ($) {
        
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

                                def.resolve(results);
                            },
                            error: function (xhr) {
                                handleError(def, xhr);
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
                                if (!cachedResults) cachedResults = [];
                                cachedResults.push(newItem);
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                def.resolve(newItem);
                            },
                            error: function (xhr) {
                                handleError(def, xhr);
                            }
                        });
                    });
                },
                
                updateData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.update(options.data, {
                            params: options.params,
                            success: function (dto) {
                                var updatedItem = entitySetConfig.mapper.mapResult(dto);
                                if (cachedResults && cachedResults.length > 0) {
                                    for (var i = 0; i < cachedResults.length; i++) {
                                        if (cachedResults[i].partitionKey() === dto.partitionKey && cachedResults[i].rowKey() === dto.rowKey) {
                                            cachedResults[i] = updatedItem;
                                            break;
                                        }
                                    }
                                }
                                
                                options.data.dirtyFlag().reset();
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                if (options.callback) options.callback(updatedItem);
                                def.resolve(updatedItem);
                            },
                            error: function (xhr) {
                                handleError(def, xhr);
                            }
                        });
                    });
                },
                
                deleteData = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.deleteItem({
                            data: options.data,
                            success: function () {
                                if (cachedResults && cachedResults.length > 0) {
                                    for (var i = 0; i < cachedResults.length; i++) {
                                        if (cachedResults[i].partitionKey() === options.data.partitionKey && cachedResults[i].rowKey() === options.data.rowKey) {
                                            cachedResults.splice(i, 1);
                                            break;
                                        }
                                    }
                                }
                                
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
                                def.resolve();
                            },
                            error: function (xhr) {
                                handleError(def, xhr);
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
                },
                
                handleError = function (def, xhr) {
                    if (xhr.status === 401) {
                        $("#sessionExpiredDialog").modal('show');
                    } else {
                        toastr.error('An error occurred');
                    }

                    if (def.reject) def.reject();
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