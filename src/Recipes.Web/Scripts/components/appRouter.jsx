var React = require('react');
var RouterMixin = require('react-mini-router').RouterMixin;
var Search = require('./search/Search.jsx');
var RecipeDetail = require('./recipeDetail/RecipeDetail.jsx');

var AppRouter = React.createClass({
   mixins: [RouterMixin],

   routes: {
      '/recipes/:urlTitle?': 'recipeDetail',
      '/': 'search'
   },

   search: function () {
      return <Search />;
   },

   recipeDetail: function (urlTitle) {
      return <RecipeDetail urlTitle={urlTitle }/>;
   },

   render: function () {
      return this.renderCurrentRoute();
   }
});

module.exports = AppRouter;