var Reflux = require('reflux');

var RecipeActions = Reflux.createActions({
    load: { children: ['completed', 'failed'] },
});

RecipeActions.load.listen(function () {
    this.completed([{
        title: 'Chicken Cacciatore',
        description: 'Translated to "Hunter Style Chicken", this is a rustic chicken and vegetable stew.',
        serves: 12,
        prepTime: 30,
        totalTime: 45,
        ingredients: [],
        directions: []
    }]);
});

module.exports = RecipeActions;