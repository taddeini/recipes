var React = require('react');
var navigate = require('react-mini-router').navigate;

var SearchResult = React.createClass({
  onRecipeClick: function(evt) {
    evt.preventDefault();
    navigate('/recipes/' + this.props.urlTitle);
  },

  render: function() {
    return (
      <div><a href='javascript:void(0);' onClick={this.onRecipeClick}>{this.props.title}</a></div>
    );
  }
});

module.exports = SearchResult;
