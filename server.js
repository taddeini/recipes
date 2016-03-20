var app = require("./src/server/app");
var http = require("http");
var appInstance = app();

var server = http.createServer(appInstance).listen('3000', function() {
	var host = server.address().address;
  var port = server.address().port;

  console.log("Recipes listening at http://%s:%s", host, port);
});

process.on("SIGTERM", function () {
  server.close(function () {
    process.exit(0);
  });
});
