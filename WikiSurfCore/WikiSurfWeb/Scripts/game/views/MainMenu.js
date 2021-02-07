
function MainMenu(app,parent) {
    var self = this;
    self.app = app;
    self.parent = parent;
};

MainMenu.prototype.loadScreen = function () {
    var self = this;
    self.startBtn = new wikiButton(100, 100, 'Content/img/start_btn_img.png', function () {
        async.parallel({
            startingWords: loadStartingWords
        }, function (err, r) {

            document.getElementById('test').innerHTML = r.startingWords.currentPage;
        });
    });

    self.app.stage.addChild(self.startBtn.button);
}

function loadStartingWords(callback) {
    $.get("/Home/GetStartingWords", function (data) {
        callback(null, data);
    });
}

function userClicked(word, wikiLinkId) {
    $.get("/Home/UserClicked",
        {
            word: word,
            wikiLinkId: wikiLinkId
        },
        function(data) {
            document.getElementById('test').innerHTML = data.currentPage;
        });
    return false;
}