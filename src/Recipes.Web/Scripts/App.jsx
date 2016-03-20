var React = require('react');
var ReactDOM = require('react-dom');
var AppRouter = require('./components/AppRouter.jsx');

ReactDOM.render(<AppRouter history={true} />, document.getElementById('main-content'))