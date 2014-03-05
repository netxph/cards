var gzippo = require('gzippo');
var express = require('express');
var app = express();

app.use(express.logger('dev'));
app.use(gzippo.staticGzip("" + __dirname + "/dist"));

app.get("/", function(request, response) {
    response.redirect("/index.html");
});

app.listen(process.env.PORT || 3000);
