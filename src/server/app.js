var express = require('express');
var bodyParser = require('body-parser');
var compression = require('compression');

module.exports = function(env) {
  var app = express();

  app.use(compression());
  app.use(bodyParser.json());

  return app;
};
