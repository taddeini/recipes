﻿require('./../../node_modules/skeleton-css/css/normalize.css');
require('./../../node_modules/skeleton-css/css/skeleton.css');
require('./../../node_modules/font-awesome/css/font-awesome.css');
require('./styles/main.less');

var React = require('react');
var ReactDOM = require('react-dom');
var AppRouter = require('./AppRouter.jsx');

ReactDOM.render(<AppRouter />, document.getElementById('main'));