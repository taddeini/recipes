var React = require('react');
var RouterMixin = require('react-mini-router').RouterMixin;
var Search = require('./search/search.jsx');
var RecipeDetail = require('./recipeDetail/recipeDetail.jsx');

var AppRouter = React.createClass({
   mixins: [RouterMixin],

   routes: {
      '/recipe/:urlTitle?': 'recipeDetail',
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