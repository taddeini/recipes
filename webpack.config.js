const webpack = require("webpack");
const production = process.env.NODE_ENV === "production";

module.exports = {
  mode: production ? "production" : "development",
  devtool: production ? false : "source-map",
  entry: {
    app: "/src/app/App.js",
    vendor: ["react", "react-dom"],
  },
  output: {
    path: "/dist",
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
