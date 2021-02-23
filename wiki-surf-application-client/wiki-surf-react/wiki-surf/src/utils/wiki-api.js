import axios from 'axios';
const baseUrl = "http://192.168.213.1:9000/"; //Dev testing

function WikiApi(){
    let self = this;
    self.sent = false;
}
WikiApi.prototype.fetchGet = function(method,data,api){
  return fetch(baseUrl + method)
      .then((res) =>{
          return res.json();
      })
      .then((res)=>{
          return res;
      });
};
WikiApi.prototype.fetchPost = function(method,data,api){
    return fetch(baseUrl + method,{
        method:'POST',
        headers:{'Content-Type':'application/json'},
        body: JSON.stringify(data)
    })
        .then(res=>{
            return res.json();
        })
        .then(res =>{
            return res;
        });
};
WikiApi.prototype.get = async(method,data,api) => {
    return axios({
        method:'post',
        url: baseUrl + method,
        headers:{
            "Content-Type":"application/json",
            "Authorization":"Bearer "+(api.session.WikiSessionId !== undefined ? api.session.WikiSessionId : '')
        }
    }).then((res) =>{
        return res.data.data;
    });
};

WikiApi.prototype.startGame = function(sessionId){
    let self = this;
    return self.fetchGet('gameEngine/StartGame/' + sessionId,null,self)
        .then(res=>{
            return res;
        });
};

WikiApi.prototype.newSession = function(){
    let self = this;
    return self.fetchGet('session/NewSession',null, self)
      .then((data)=>{
          return data;
      });
};
WikiApi.prototype.linkClicked = function(wikiLinkId, word,sessionId){
    let self = this;
    return self.fetchPost('gameEngine/LinkClicked/' + sessionId,{
        WikiLinkId:wikiLinkId,
        Word: word
    },self)
        .then((data)=>{
            return data;
        })
}
export default new WikiApi();