let config = require('./config');
const _ = require('lodash');

module.exports = function(){
    let self = this;
    self.host = config['host'];
    self.port = config['port'];
    self.api = {};
    _.forEach(config.api,a=>{
        self.api[a.name] = a;
    });
    self.getApiConfig = function(str){
        return self.api[str];
    };
    return self;
};