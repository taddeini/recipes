var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var compression = require('compression');
var favicon = require('serve-favicon');
var expressHandlebars = require('express-handlebars');

module.exports = function(env) {
  var app = express();

  app.set('views', path.join(__dirname, './views'));
  app.engine('handlebars', expressHandlebars());
  app.set('view engine', 'handlebars');
  //app.use(favicon(path.join(__dirname, '../../dist/favicon.ico')));
  app.use(compression());
  app.use(bodyParser.json());

  // Routes
  app.use('/', express.static(path.join(__dirname, '../../dist')));
  
  require('./controllers/index')(app);

  return app;
};
