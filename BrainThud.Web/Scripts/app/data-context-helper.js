define('data-context-helper', ['jquery', 'utils'],
    function ($, utils) {
        var EntitySet = function(config) {
            var cachedResults = [],
                getData = function(options) {
                    var def = new $.Deferred();
                    
                    if (options.invalidateCache) {
                        cachedResults = [];
                    }

                    var results = options && options.results;
                    if (!cachedResults || !utils.hasProperties(cachedResults)) {
                        config.get({
                            params: options.params,
                            success: function(dto) {
                                cachedResults = [];

                                config.mapper.mapResults(dto, cachedResults);

                                if (results) {
                                    results(cachedResults);
                                }

                                def.resolve(results);
                            },
                            error: function() {
                                if (def.reject) def.reject();
                            }
                        });
                    } else {
                        results(cachedResults);
                        def.resolve(results);
                    }

                    return def.promise();
                },
                createData = function(options) {
                    return $.Deferred(function(def) {
                        config.create(options.data, {
                            params: options.params,
                            success: function (result) {
                                config.mapper.mapResults([result], cachedResults);
                                def.resolve();
                            },
                            error: function(response) {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },
                updateData = function(options) {
                    return $.Deferred(function(def) {
                        config.update(options.data, {
                            params: options.params,
                            success: function (dto) {
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === dto.partitionKey && cachedResults[i].rowKey() === dto.rowKey) {
                                        cachedResults[i] = config.mapper.mapResult(dto);
                                        break;
                                    }
                                }

                                def.resolve();
                            },
                            error: function(response) {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                },
                deleteData = function(options) {
                    return $.Deferred(function(def) {
                        config.deleteItem({
                            params: options.params,
                            success: function () {
                                for (var i = 0; i < cachedResults.length; i++) {
                                    if (cachedResults[i].partitionKey() === options.params.partitionKey && cachedResults[i].rowKey() === options.params.rowKey) {
                                        cachedResults.splice(i, 1);
                                        break;
                                    }
                                }

                                def.resolve();
                            },
                            error: function(response) {
                                if (def.reject) def.reject();
                            }
                        });
                    });
                };

            return {
                getData: getData,
                createData: createData,
                updateData: updateData,
                deleteData: deleteData
            };
        };

        return {
            EntitySet: EntitySet
        };
    });