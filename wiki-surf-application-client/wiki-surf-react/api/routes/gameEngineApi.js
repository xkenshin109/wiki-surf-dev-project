let express = require("express");
let router = express.Router();
let wikiApi = require('../utils/gameEngineApi');

router.get("/", function(req,res,next){
   res.render('index', { title: 'Wiki Api Page - Up and running' });
});

router.get("/StartGame/:sessionid", async function(req,res,next){
    return wikiApi.startGame(req)
        .then(rr =>{
            return res.send(rr);
        });
});

router.post("/LinkClicked/:sessionid", async function(req,res,next){
   return wikiApi.linkClicked(req)
       .then(r=>{
           return res.send(r);
       })
});
module.exports = router;