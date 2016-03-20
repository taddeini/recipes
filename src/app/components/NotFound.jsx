var React = require('react');

var NotFound = React.createClass({
  render: function() {
    return (
      <div id='not-found'>
        <h1>404 Page not Found</h1>
      </div>
    );
  }
});

module.exports = NotFound;
