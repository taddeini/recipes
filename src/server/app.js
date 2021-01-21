const express = require("express");
const bodyParser = require("body-parser");
const compression = require("compression");

module.exports = (env) => {
  const app = express();

  app.use(compression());
  app.use(bodyParser.json());

  return app;
};
