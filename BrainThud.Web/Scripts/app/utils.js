define('utils', [],
    function () {
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
                var today = new Date(),
                    year = today.getFullYear(),
                    month = today.getMonth() + 1,
                    day = today.getDate();

                return year + '/' + month + '/' + day;
            };
        
        return {
            hasProperties: hasProperties,
            getDatePath: getDatePath
        };
    }
);