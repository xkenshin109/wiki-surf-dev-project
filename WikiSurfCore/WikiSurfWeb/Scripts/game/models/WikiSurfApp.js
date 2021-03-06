﻿var wikiApi = require('../routes/wiki-api');
var wikiControls = require('../controls/wiki-controls');

function WikiSurfApp(app) {
    var self = this;
    self.appView = app;
    $.getJSON('Scripts/game/config/game-dev.json', function (json) {
        self.config = json;
    });
    self.wordBank = [];
    self.wikiControls = wikiControls.wikiControls(app, self);
};

WikiSurfApp.prototype.startGameloop = function () {
    var self = this;
    var defaultView = 'MainMenu';
    //const container = new PIXI.Container();

    //self.app.stage.addChild(container);
    //// Create a new texture
    //const texture = PIXI.Texture.from('Content/img/download.jpg');

    //// Create a 5x5 grid of bunnies
    //const bunny = new PIXI.Sprite(texture);
    //bunny.anchor.set(0.5);
    //bunny.x = 5 * 40;
    //bunny.y = Math.floor(1 / 5) * 40;
    //container.addChild(bunny);

    //// Move container to the center
    //container.x = self.app.screen.width / 2;
    //container.y = self.app.screen.height / 2;

    //// Center bunny sprite in local container coordinates
    //container.pivot.x = container.width / 2;
    //container.pivot.y = container.height / 2;
    var mainMenu = new MainMenu(self.appView, self);
    mainMenu.loadScreen();
    // Listen for animate update
    self.app.ticker.add((delta) => {
        //// rotate the container!
        //// use delta to create frame-independent transform
      //  container.rotation -= 0.01 * delta;
    });
};

//called when navigating screens. Placed here as placeholder 
WikiSurfApp.prototype.clearCanvas = function () {
    var self = this;
    for (var i = self.app.stage.children.length - 1; i >= 0; i--) {
        self.app.stage.removeChild(self.app.stage.children[i]);
    }
};

WikiSurfApp.prototype.addControlToAppView = function(control) {

}