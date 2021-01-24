const webpack = require("webpack");
const path = require("path");
const production = process.env.NODE_ENV === "production";

module.exports = {
  mode: production ? "production" : "development",
  devtool: production ? false : "source-map",
  entry: {
    app: "./src/client/App.js",
  },
  output: {
    path: path.resolve(__dirname, "dist"),
    filename: "[name].js",
  },
  module: {
    rules: [
      {
        test: /\.(js)$/,
        exclude: /node_modules/,
        use: ["babel-loader"],
      },
    ],
  },
};
