var wikiApi = require('../routes/wiki-api');

function MainMenu(app,parent) {
    var self = this;
    self.appView = app;
    self.parent = parent;
    self.startingWord = '';
    self.endingWord = '';
};

MainMenu.prototype.loadScreen = function () {
    var self = this;
    self.startBtn = self.parent.wikiControls.addButtonToView(50, 50, 'Content/img/start_btn_img.png', function () {
        async.parallel({
            startingWords: wikiApi.startGame
        }, function (err, r) {

            document.getElementById('test').innerHTML = r.startingWords.currentPage;
        });
    });
};