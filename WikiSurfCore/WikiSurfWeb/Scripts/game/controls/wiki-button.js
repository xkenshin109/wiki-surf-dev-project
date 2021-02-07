function wikiButton(x, y, img, eventHandler) {
    let self = this;

    self.image = PIXI.Texture.from(img);
    self.button = new PIXI.Sprite(self.image);
    self.button.anchor.set(0.5);
    self.button.x = x;
    self.button.y = y;
    self.button.interactive = true;
    self.button.buttonMode = true;
    self.button.on('pointerdown', onButtonDown)
        .on('pointerup', onButtonUp)
        .on('pointerupoutside', onButtonUp)
        .on('pointerover', onButtonOver)
        .on('pointerout', onButtonOut);
    self.button.click = eventHandler;
    
};

function onButtonDown() {
    let self = this;
    self.isdown = true;
    self.tint = 50 * 0xFFFFF;
    self.alpha = 1;
}

function onButtonUp() {
    let self = this;
    self.isdown = false;
    this.tint = 0xFFFFFF;
}

function onButtonOver() {
    this.isOver = true;
    if (this.isdown) {
        return;
    }
    this.tint = 1 * 0xFFFFFF;
}

function onButtonOut() {
    this.isOver = false;
    if (this.isdown) {
        return;
    }
}