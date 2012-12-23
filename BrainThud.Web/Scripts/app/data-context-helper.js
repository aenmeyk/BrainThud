define('data-context-helper', ['jquery', 'utils'],
    function ($, utils) {
        var EntitySet = function(config) {
            var cachedResults = [],
                getData = function(options) {
                    var def = new $.Deferred();

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
                            success: function(result) {
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
                            success: function(result) {
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
                updateData: updateData
            };
        };

        return {
            EntitySet: EntitySet
        };
    });