var React = require('react');

var SearchResult = React.createClass({
  render: function() {
    return (
      <div><a href={'recipes/' + this.props.urlTitle}>{this.props.title}</a></div>
    );
  }
});

module.exports = SearchResult;
