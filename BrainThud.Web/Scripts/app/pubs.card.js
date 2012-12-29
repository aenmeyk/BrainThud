define('pubs.card', ['config', 'amplify'],

function (config, amplify) {
	var
        update = function (data) {
        	amplify.publish(config.pubs.updateCard, data);
        },
        deleteItem = function (data) {
            amplify.publish(config.pubs.deleteCard, data);
        };

    return {
    	update: update,
        deleteItem: deleteItem
    };
});