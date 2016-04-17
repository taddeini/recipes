var React = require('react');

var NotFound = React.createClass({
  render: function() {
    return (
      <div id='notFound' className='container'>
        <h1>404 Page not Found</h1>
      </div>
    );
  }
});

module.exports = NotFound;
