let axios = require('axios');
const baseUrl = "http://localhost:54303/api/"; //Dev testing
let config = require('../config/index')();
//let c = config();
const _ = require('lodash');

function WikiApi(){
    let self = this;
    self.config = config.getApiConfig('wikiapi');
    self.sent = false;
    self.post =  (method,data,req) => {
        let self = this;
        let headers = {
            'Content-Type':'application/json'
        };
        if(req.params !== undefined){
            if(req.params.sessionid){
                headers['sessionid'] = req.params.sessionid;
            }
        }
        return axios.post(self.config.baseUrl + method,data,{
            headers: headers
        })
            .then(res=>{
                return res.data.data;
            })
            .catch(e=>{
                console.log(e);
            });
    };
    self.fetchGet =  function(method,data,parent){
        let self = parent;
        return axios.get(self.config.baseUrl + method)
            .then((res) =>{
                return res.data.data;
            });
    };
    return self;
}

WikiApi.prototype.startGame = function(req){
    let self = this;
    return self.post('GameEngine/StartGame',null,req);
};

WikiApi.prototype.newSession = function(req){
    let self = this;

    return self.post('GameEngine/NewSession',null, req);
};

WikiApi.prototype.linkClicked = function(req){
    let self = this;
    return self.post("Session/LinkClicked",req.body,req);
};
module.exports = new WikiApi();