//Creates Applet for WikiSurf
window.onload = function () {
    var appContainer = this.document.getElementById('app-container');    

    var app = new PIXI.Application({
        width: appContainer.offsetWidth,
        height: 600,
        backgroundColor: 0x1099bb,
        resolution: window.devicePixelRatio || 1,
    });
    var a = app.view.width;
    var h = app.view.height;
    appContainer.appendChild(app.view);

    var mv = new WikiSurfApp(app);
    mv.startGameloop();
}
