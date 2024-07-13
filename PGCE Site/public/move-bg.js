var x = 0;
var y = 0;
var body = document.body;
var dx = -0.1;
var dy = -0.1;

function loop() {
    x += dx;
    y += dy;
    body.style = "background-position: right " + x.toString() + "px bottom " + y.toString() + "px;";
}

function startLoop() {
    loop();
    setTimeout(
        startLoop, 1
    );
}

startLoop();