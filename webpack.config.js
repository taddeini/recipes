var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var production = (process.env.NODE_ENV === 'production');

var plugins = [
  new webpack.optimize.CommonsChunkPlugin({
    name: 'vendor'
  }),
  new ExtractTextPlugin('[name].css')
];

if (production) {
  plugins = plugins.concat([

    // This plugin looks for similar chunks and files
    // and merges them for better caching by the user
    new webpack.optimize.DedupePlugin(),

    // This plugins optimizes chunks and modules by
    // how much they are used in your app
    new webpack.optimize.OccurenceOrderPlugin(),

    // This plugin prevents Webpack from creating chunks
    // that would be too small to be worth loading separately
    new webpack.optimize.MinChunkSizePlugin({
      minChunkSize: 51200, // ~50kb
    }),

    // This plugin minifies all the Javascript code of the final bundle
    new webpack.optimize.UglifyJsPlugin({
      mangle: true,
      compress: {
        warnings: false, // Suppress uglification warnings
      },
    }),

    // This plugins defines various variables that we can set to false
    // in production to avoid code related to them from being compiled
    // in our final bundle
    new webpack.DefinePlugin({
      __SERVER__: !production,
      __DEVELOPMENT__: !production,
      __DEVTOOLS__: !production,
      'process.env': {
        BABEL_ENV: JSON.stringify(process.env.NODE_ENV),
      },
    }),

  ]);
}

module.exports = {
  debug: !production,
  devtool: production ? false : 'eval',
  entry: {
    app: './src/app/App.jsx',
    vendor: [
      'react',
      'react-dom',
      'react-mini-router',
      'reflux',
      'underscore'
    ]
  },
  output: {
    path: './dist',
    filename: '[name].js',
  },
  module: {
    loaders: [
      {
        test: /\.jsx?$/,
        exclude: /node_modules/,
        loader: 'babel',
        query: {
          presets: ['es2015', 'react']
        },
      },
      {
        test: /\.css?$/,
        loader: ExtractTextPlugin.extract('style-loader', 'css-loader')
      },
      {
        test: /\.less?$/,
        exclude: /node_modules/,
        loader: ExtractTextPlugin.extract('style-loader', 'css-loader!less-loader')
      },
      {
        test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        loader: "url-loader?limit=10000&minetype=application/font-woff"
      },
      {
        test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        loader: "file-loader"
      }
    ],
  },
  plugins: plugins
};