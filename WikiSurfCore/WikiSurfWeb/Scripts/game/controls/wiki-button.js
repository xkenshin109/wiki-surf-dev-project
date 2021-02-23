function wikiButton(x, y, img, eventHandler) {
    var self = this;

    self.image = PIXI.Texture.from(img);
    self.button = new PIXI.Sprite(self.image);
    self.button.anchor.set(0.5);
    self.button.x = x;
    self.button.y = y;
    self.button.interactive = true;
    self.button.buttonMode = true;
    self.button.on('pointerdown', self.onButtonDown)
        .on('pointerup', self.onButtonUp)
        .on('pointerupoutside', self.onButtonUp)
        .on('pointerover', self.onButtonOver)
        .on('pointerout', self.onButtonOut);
    self.button.click = eventHandler;

    self.onButtonDown = function onButtonDown() {
        self.isdown = true;
        self.tint = 50 * 0xFFFFF;
        self.alpha = 1;
    }

    self.onButtonUp = function onButtonUp() {
        self.isdown = false;
        this.tint = 0xFFFFFF;
    }

    self.onButtonOver = function onButtonOver() {
        self.isOver = true;
        if (this.isdown) {
            return;
        }
        this.tint = 1 * 0xFFFFFF;
    }

    self.onButtonOut = function onButtonOut() {
        self.isOver = false;
        if (this.isdown) {
            return;
        }
    }
};

module.exports.wikiButton = wikiButton;