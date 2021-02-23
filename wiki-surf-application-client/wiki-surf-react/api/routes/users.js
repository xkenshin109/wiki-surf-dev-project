let express = require('express');
let router = express.Router();
let wikiApi = require('../utils/gameEngineApi');
/* GET users listing. */
router.get('/NewSession', function(req, res, next) {
  return wikiApi.newSession(wikiApi.config, req)
      .then(rr =>{
        return res.send(rr);
      });
});

module.exports = router;
