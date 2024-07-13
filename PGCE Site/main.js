const express = require('express')
var fs = require('fs');
const { pathToFileURL } = require('url');
const app = express()
const port = 3000

var pageHeader = "";

app.use('/', express.static('public'))

function prep(res, path)
{
	fs.readFile("templates/replaces/pageheader.html", "utf8", function(err, data){
		pageHeader = data;
	});
	sendfile(res, path);
}

app.get("/", function (req, res) {
	prep(res, "templates/index.html");
})

function sendfile(res, path)
{
	fs.readFile(path, "utf8", function(err, data){
		var outData = data;
		outData = outData.replace("<!-- PAGE HEADER -->", pageHeader);
		res.send(outData);
	});
}

app.get("/about", function (req, res) {
	prep(res, "templates/about.html");
})

app.get("/changelog", function (req, res) {
	prep(res, "templates/changelog.html");
})

app.listen(port, () => {
  console.log(`Listening on port ${port}`)
})