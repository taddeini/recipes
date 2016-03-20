var Reflux = require('reflux');

var RecipeActions = Reflux.createActions({
  load: { children: ['completed', 'failed'] },
});

RecipeActions.load.listen(function() {
  this.completed(_recipes);
});

var _recipes = [
  {
    id: 1,
    urlTitle: 'chicken-cacciatore',
    title: 'Chicken Cacciatore',
    description: 'Translated to "Hunter Style Chicken", this is a rustic chicken and vegetable stew.',
    serves: 12,
    prepTime: 30,
    totalTime: 45,
    ingredients: [],
    directions: []
  },
  {
    id: 2,
    urlTitle: 'nonnas-tomato-sauce',
    title: 'Nonna\'s Tomato Sauce',
    description: '',
    serves: 4,
    prepTime: 20,
    totalTime: 90,
    ingredients: [],
    directions: []
  }
];

module.exports = RecipeActions;