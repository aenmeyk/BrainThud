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
                        entitySetConfig.create({
                            data: options.data,
                            cache: cachedResults,
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
                        entitySetConfig.update({
                            data: options.data,
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
                
                updateBatch = function (options) {
                    return $.Deferred(function (def) {
                        entitySetConfig.updateBatch({
                            data: options.data,
                            params: options.params,
                            success: function (dto) {
//                                var updatedItems = [];
//                                entitySetConfig.mapper.mapResults(dto, updatedItems);
//                                if (cachedResults && cachedResults.length > 0) {
//                                    for (var j = 0; j < updatedItems.length; j++) {
//                                        for (var i = 0; i < cachedResults.length; i++) {
//                                            if (cachedResults[i].partitionKey() === updatedItems[j].partitionKey && cachedResults[i].rowKey() === updatedItems[j].rowKey) {
//                                                cachedResults[i] = updatedItems[j];
//                                                break;
//                                            }
//                                        }
//                                    }
//                                }
//                                
//                                options.data.dirtyFlag().reset();
                                if (entitySetConfig.showSuccessToastr) toastr.success('Success!');
//                                if (options.callback) options.callback(updatedItems);
                                //                                def.resolve(updatedItems);
                                setCacheInvalid();
                                def.resolve();
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
                                    var array = options.data;
                                    // If the data is not an array, convert it to an array with a single item
                                    if (Object.prototype.toString.call(array) !== '[object Array]') {
                                        array = new Array();
                                        array[0] = options.data;
                                    }

                                    for (var j = 0; j < array.length; j++) {
                                        for (var i = 0; i < cachedResults.length; i++) {
                                            if (cachedResults[i].partitionKey() === array[j].partitionKey && cachedResults[i].rowKey() === array[j].rowKey) {
                                                cachedResults.splice(i, 1);
                                                break;
                                            }
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
                updateBatch: updateBatch,
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