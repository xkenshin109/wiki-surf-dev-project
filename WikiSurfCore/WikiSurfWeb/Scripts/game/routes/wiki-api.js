

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
        function (data) {
            document.getElementById('test').innerHTML = data.currentPage;
        });
    return false;
}

module.exports.startGame = loadStartingWords;
module.exports.nextPage = userClicked;