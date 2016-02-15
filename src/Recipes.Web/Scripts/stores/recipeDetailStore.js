var Reflux = require('reflux');
var RecipeActions = require('../actions/recipeActions.js');

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
    }
});

module.exports = RecipeDetailStore;