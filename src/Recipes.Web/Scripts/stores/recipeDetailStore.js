var Reflux = require('reflux');
var RecipeActions = require('../actions/recipeActions.js');
var _ = require('underscore');

var RecipeDetailStore = Reflux.createStore({
   listenables: [RecipeActions],

   init: function () {
      this.recipes = [];
   },

   onLoadCompleted: function (recipes) {
      this.recipes = recipes;
      this.trigger();
   },

   getRecipes: function () {
      return this.recipes;
   },

   getRecipeByUrlTitle: function (urlTitle) {
      return _.find(this.recipes, function (recipe) {
         return (recipe.urlTitle === urlTitle);
      }) || {};
   }
});

module.exports = RecipeDetailStore;