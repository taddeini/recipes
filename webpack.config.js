module.exports = {
  entry: './src/app/App.jsx',
  output: {
    path: './dist',
    filename: 'app.js',
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
        loaders: ['css'],
      }
    ],
  }
};