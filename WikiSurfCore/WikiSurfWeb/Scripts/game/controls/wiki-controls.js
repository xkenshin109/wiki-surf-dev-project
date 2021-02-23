var wikiText = require('./wiki-text');
var wikiButton = require('./wiki-button');

function WikiControls(appView, app) {
    var self = this;
    self.appView = appView;
    self.app = app;

}
WikiControls.prototype.addButtonToView = function (x, y, img, eventHandler) {
    var self = this;
    var button = wikiButton.wikiButton(x, y, img, eventHandler);
    self.app.addControlToAppView(button);
    return button; 
}
WikiControls.prototype.addTextToView = function() {

}

module.exports.wikiControls = WikiControls;