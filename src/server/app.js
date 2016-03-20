var express = require('express');
var path = require('path');
var compression = require('compression');
var expressHandlebars = require('express-handlebars');

module.exports = function (env) {
  var app = express();

  app.set('views', path.join(__dirname, './views'));
  app.engine('handlebars', expressHandlebars());
  app.set('view engine', 'handlebars');
  app.disable('etag');
  app.use(compression());

  // Routes
  app.use('/', express.static(path.join(__dirname, '../dist')));
  require('./controllers/index')(app);

  return app;
};
