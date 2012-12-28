define('pubs.card', ['config', 'amplify'],

function (config, amplify) {
	var
        update = function (data) {
        	amplify.publish(config.pubs.updateCard, data);
        };

    return {
    	update: update
    };
});