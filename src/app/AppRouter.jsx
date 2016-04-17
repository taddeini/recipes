var React = require('react');
var RouterMixin = require('react-mini-router').RouterMixin;
var Search = require('./components/search/Search.jsx');
var NotFound = require('./components/NotFound.jsx');
var RecipeDetail = require('./components/recipeDetail/RecipeDetail.jsx');

var AppRouter = React.createClass({
  mixins: [RouterMixin],

  routes: {
    '/recipes/:urlTitle?': 'recipeDetail',
    '/': 'search'
  },

  search: function() {
    return <Search />;
  },

  recipeDetail: function(urlTitle) {
    return <RecipeDetail urlTitle={urlTitle }/>;
  },

  notFound: function() {
    return <NotFound />
  },

  render: function() {
    return this.renderCurrentRoute();
  }
});

module.exports = AppRouter;