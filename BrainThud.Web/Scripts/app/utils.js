define('utils', ['moment'],
    function (moment) {
        var
            hasProperties = function (obj) {
                for (var prop in obj) {
                    if (obj.hasOwnProperty(prop)) {
                        return true;
                    }
                }
                return false;
            },
            getDatePath = function() {
                return moment(new Date()).format('YYYY/M/D');
            },
            entityExists = function(array, entity) {
                for (var i = 0; i < array.length; i++) {
                    if (array[i].partitionKey == entity.partitionKey && array[i].rowKey == entity.rowKey) {
                        return true;
                    }
                }

                return false;
            };
        
        return {
            hasProperties: hasProperties,
            getDatePath: getDatePath,
            entityExists: entityExists
        };
    }
);