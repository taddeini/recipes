require('./../../node_modules/skeleton-css/css/normalize.css');
require('./../../node_modules/skeleton-css/css/skeleton.css');

var React = require('react');
var ReactDOM = require('react-dom');
var AppRouter = require('./components/AppRouter.jsx');

ReactDOM.render(<AppRouter history="true" />, document.getElementById('main'));