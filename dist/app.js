webpackJsonp([0],{

/***/ 0:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\n__webpack_require__(3);\n__webpack_require__(7);\n__webpack_require__(9);\n__webpack_require__(17);\n\nvar React = __webpack_require__(21);\nvar ReactDOM = __webpack_require__(178);\nvar AppRouter = __webpack_require__(179);\n\nReactDOM.render(React.createElement(AppRouter, null), document.getElementById('main'));\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/App.jsx\n ** module id = 0\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/App.jsx?");

/***/ },

/***/ 3:
/***/ function(module, exports) {

	eval("// removed by extract-text-webpack-plugin\n\n/*****************\n ** WEBPACK FOOTER\n ** ./~/skeleton-css/css/normalize.css\n ** module id = 3\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./~/skeleton-css/css/normalize.css?");

/***/ },

/***/ 7:
/***/ function(module, exports) {

	eval("// removed by extract-text-webpack-plugin\n\n/*****************\n ** WEBPACK FOOTER\n ** ./~/skeleton-css/css/skeleton.css\n ** module id = 7\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./~/skeleton-css/css/skeleton.css?");

/***/ },

/***/ 9:
/***/ function(module, exports) {

	eval("// removed by extract-text-webpack-plugin\n\n/*****************\n ** WEBPACK FOOTER\n ** ./~/font-awesome/css/font-awesome.css\n ** module id = 9\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./~/font-awesome/css/font-awesome.css?");

/***/ },

/***/ 17:
/***/ function(module, exports) {

	eval("// removed by extract-text-webpack-plugin\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/styles/main.less\n ** module id = 17\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/styles/main.less?");

/***/ },

/***/ 179:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\nvar RouterMixin = __webpack_require__(180).RouterMixin;\nvar Search = __webpack_require__(190);\nvar NotFound = __webpack_require__(214);\nvar RecipeDetail = __webpack_require__(215);\n\nvar AppRouter = React.createClass({\n  displayName: 'AppRouter',\n\n  mixins: [RouterMixin],\n\n  routes: {\n    '/recipes/:urlTitle?': 'recipeDetail',\n    '/': 'search'\n  },\n\n  search: function search() {\n    return React.createElement(Search, null);\n  },\n\n  recipeDetail: function recipeDetail(urlTitle) {\n    return React.createElement(RecipeDetail, { urlTitle: urlTitle });\n  },\n\n  notFound: function notFound() {\n    return React.createElement(NotFound, null);\n  },\n\n  render: function render() {\n    return this.renderCurrentRoute();\n  }\n});\n\nmodule.exports = AppRouter;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/AppRouter.jsx\n ** module id = 179\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/AppRouter.jsx?");

/***/ },

/***/ 190:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };\n\nvar React = __webpack_require__(21);\nvar Reflux = __webpack_require__(191);\nvar RecipeActions = __webpack_require__(210);\nvar RecipeStore = __webpack_require__(211);\nvar SearchResult = __webpack_require__(213);\n\nvar Search = React.createClass({\n  displayName: 'Search',\n\n  mixins: [Reflux.ListenerMixin],\n\n  getInitialState: function getInitialState() {\n    return {\n      searchResults: RecipeStore.getRecipes()\n    };\n  },\n\n  componentDidMount: function componentDidMount() {\n    this.listenTo(RecipeStore, this.onRecipesLoaded);\n    RecipeActions.load();\n  },\n\n  onRecipesLoaded: function onRecipesLoaded() {\n    this.setState({ searchResults: RecipeStore.getRecipes() });\n  },\n\n  render: function render() {\n    return React.createElement(\n      'div',\n      { id: 'search', className: 'container' },\n      React.createElement(\n        'h1',\n        null,\n        'Find a Recipe'\n      ),\n      React.createElement(\n        'form',\n        null,\n        React.createElement('input', { type: 'search', autoFocus: true, placeholder: 'Search by recipe title or ingredient...' }),\n        React.createElement('span', { className: 'search-icon fa fa-search' })\n      ),\n      React.createElement(\n        'div',\n        { className: 'searchResults' },\n        this.state.searchResults.map(function (result, key) {\n          return React.createElement(SearchResult, _extends({ key: key }, result));\n        })\n      )\n    );\n  }\n});\n\nmodule.exports = Search;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/search/Search.jsx\n ** module id = 190\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/search/Search.jsx?");

/***/ },

/***/ 210:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar Reflux = __webpack_require__(191);\n\nvar RecipeActions = Reflux.createActions({\n  load: { children: ['completed', 'failed'] }\n});\n\nRecipeActions.load.listen(function () {\n  this.completed(_recipes);\n});\n\nvar _recipes = [{\n  id: 1,\n  urlTitle: 'chicken-cacciatore',\n  title: 'Chicken Cacciatore',\n  description: 'Translated to \"Hunter Style Chicken\", this is a rustic chicken and vegetable stew.',\n  serves: 12,\n  prepTime: 30,\n  totalTime: 45,\n  ingredients: [],\n  directions: []\n}, {\n  id: 2,\n  urlTitle: 'nonnas-tomato-sauce',\n  title: 'Nonna\\'s Tomato Sauce',\n  description: '',\n  serves: 4,\n  prepTime: 20,\n  totalTime: 90,\n  ingredients: [],\n  directions: []\n}];\n\nmodule.exports = RecipeActions;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/actions/RecipeActions.js\n ** module id = 210\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/actions/RecipeActions.js?");

/***/ },

/***/ 211:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar Reflux = __webpack_require__(191);\nvar RecipeActions = __webpack_require__(210);\nvar _ = __webpack_require__(212);\n\nvar RecipeDetailStore = Reflux.createStore({\n  listenables: [RecipeActions],\n\n  init: function init() {\n    this.recipes = [];\n  },\n\n  onLoadCompleted: function onLoadCompleted(recipes) {\n    this.recipes = recipes;\n    this.trigger();\n  },\n\n  getRecipes: function getRecipes() {\n    return this.recipes;\n  },\n\n  getRecipeByUrlTitle: function getRecipeByUrlTitle(urlTitle) {\n    return _.find(this.recipes, function (recipe) {\n      return recipe.urlTitle === urlTitle;\n    }) || {};\n  }\n});\n\nmodule.exports = RecipeDetailStore;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/stores/RecipeStore.js\n ** module id = 211\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/stores/RecipeStore.js?");

/***/ },

/***/ 213:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\nvar navigate = __webpack_require__(180).navigate;\n\nvar SearchResult = React.createClass({\n  displayName: 'SearchResult',\n\n  onRecipeClick: function onRecipeClick(evt) {\n    evt.preventDefault();\n    navigate('/recipes/' + this.props.urlTitle);\n  },\n\n  render: function render() {\n    return React.createElement(\n      'div',\n      null,\n      React.createElement(\n        'a',\n        { href: 'javascript:void(0);', onClick: this.onRecipeClick },\n        this.props.title\n      )\n    );\n  }\n});\n\nmodule.exports = SearchResult;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/search/SearchResult.jsx\n ** module id = 213\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/search/SearchResult.jsx?");

/***/ },

/***/ 214:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\n\nvar NotFound = React.createClass({\n  displayName: 'NotFound',\n\n  render: function render() {\n    return React.createElement(\n      'div',\n      { id: 'notFound', className: 'container' },\n      React.createElement(\n        'h1',\n        null,\n        '404 Page not Found'\n      )\n    );\n  }\n});\n\nmodule.exports = NotFound;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/NotFound.jsx\n ** module id = 214\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/NotFound.jsx?");

/***/ },

/***/ 215:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\nvar Reflux = __webpack_require__(191);\nvar RecipeActions = __webpack_require__(210);\nvar RecipeStore = __webpack_require__(211);\nvar Ingredients = __webpack_require__(216);\nvar Directions = __webpack_require__(217);\n\nvar RecipeDetail = React.createClass({\n  displayName: 'RecipeDetail',\n\n  mixins: [Reflux.ListenerMixin],\n\n  getInitialState: function getInitialState() {\n    return RecipeStore.getRecipeByUrlTitle(this.props.urlTitle);\n  },\n\n  componentDidMount: function componentDidMount() {\n    this.listenTo(RecipeStore, this.onRecipeDetailsChanged);\n    RecipeActions.load();\n  },\n\n  onRecipeDetailsChanged: function onRecipeDetailsChanged() {\n    this.setState(RecipeStore.getRecipeByUrlTitle(this.props.urlTitle));\n  },\n\n  render: function render() {\n    return React.createElement(\n      'div',\n      { id: 'recipeDetail', className: 'container' },\n      React.createElement(\n        'section',\n        { id: 'intro' },\n        React.createElement(\n          'h1',\n          null,\n          this.state.title\n        ),\n        React.createElement(\n          'div',\n          { className: 'row facts' },\n          React.createElement(\n            'div',\n            { className: 'four columns fact' },\n            'Serves  ',\n            React.createElement(\n              'strong',\n              null,\n              this.state.serves\n            )\n          ),\n          React.createElement(\n            'div',\n            { className: 'four columns fact' },\n            'Total  Time  ',\n            React.createElement(\n              'strong',\n              null,\n              this.state.totalTime,\n              '  mins'\n            )\n          ),\n          React.createElement(\n            'div',\n            { className: 'four columns fact' },\n            'Prep  Time  ',\n            React.createElement(\n              'strong',\n              null,\n              this.state.prepTime,\n              '  mins'\n            )\n          )\n        ),\n        React.createElement(\n          'div',\n          { className: 'row' },\n          React.createElement(\n            'div',\n            { className: 'twelve columns' },\n            this.state.description\n          )\n        )\n      ),\n      React.createElement(\n        'section',\n        { id: 'ingredients', className: 'row' },\n        React.createElement(\n          'h3',\n          null,\n          'Ingredients'\n        ),\n        React.createElement(Ingredients, { ingredients: this.state.ingredients })\n      ),\n      React.createElement(\n        'section',\n        { id: 'directions', className: 'row' },\n        React.createElement(\n          'h3',\n          null,\n          'Directions'\n        ),\n        React.createElement(Directions, { directions: this.state.directions })\n      )\n    );\n  }\n});\n\nmodule.exports = RecipeDetail;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/recipeDetail/RecipeDetail.jsx\n ** module id = 215\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/recipeDetail/RecipeDetail.jsx?");

/***/ },

/***/ 216:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\n\nvar Ingredients = React.createClass({\n  displayName: 'Ingredients',\n\n  render: function render() {\n    return React.createElement(\n      'ul',\n      null,\n      React.createElement(\n        'li',\n        null,\n        React.createElement(\n          'span',\n          { className: 'amount' },\n          '1'\n        ),\n        ' proin mi dolor'\n      ),\n      React.createElement(\n        'li',\n        null,\n        React.createElement(\n          'span',\n          { className: 'amount' },\n          '1/2'\n        ),\n        ' non euismod quis'\n      ),\n      React.createElement(\n        'li',\n        null,\n        React.createElement(\n          'span',\n          { className: 'amount' },\n          '3'\n        ),\n        ' vivamus dapibus'\n      ),\n      React.createElement(\n        'li',\n        null,\n        React.createElement(\n          'span',\n          { className: 'amount' },\n          '1/4'\n        ),\n        ' proin mi dolor'\n      ),\n      React.createElement(\n        'li',\n        null,\n        React.createElement(\n          'span',\n          { className: 'amount' },\n          '1 1/2'\n        ),\n        ' vivamus dapibus'\n      )\n    );\n  }\n});\n\nmodule.exports = Ingredients;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/recipeDetail/Ingredients.jsx\n ** module id = 216\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/recipeDetail/Ingredients.jsx?");

/***/ },

/***/ 217:
/***/ function(module, exports, __webpack_require__) {

	eval("'use strict';\n\nvar React = __webpack_require__(21);\n\nvar Directions = React.createClass({\n  displayName: 'Directions',\n\n  render: function render() {\n    return React.createElement(\n      'ol',\n      null,\n      React.createElement(\n        'li',\n        null,\n        'Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique.Fusce orci ex, varius vitae dui ut, placerat scelerisque eros.'\n      ),\n      React.createElement(\n        'li',\n        null,\n        'Maecenas vitae ante sit amet felis luctus volutpat at quis ligula.Pellentesque lectus mi, suscipit id iaculis vitae, euismod dignissim quam.'\n      ),\n      React.createElement(\n        'li',\n        null,\n        'Proin mi dolor, pellentesque non euismod quis, tristique eu quam.'\n      ),\n      React.createElement(\n        'li',\n        null,\n        'Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique.Fusce orci ex, varius vitae dui ut, placerat scelerisque eros.'\n      ),\n      React.createElement(\n        'li',\n        null,\n        'Maecenas vitae ante sit amet felis luctus volutpat at quis ligula.Pellentesque lectus mi, suscipit id iaculis vitae, euismod dignissim quam.'\n      ),\n      React.createElement(\n        'li',\n        null,\n        'Proin mi dolor, pellentesque non euismod quis, tristique eu quam.'\n      )\n    );\n  }\n});\n\nmodule.exports = Directions;\n\n/*****************\n ** WEBPACK FOOTER\n ** ./src/app/components/recipeDetail/Directions.jsx\n ** module id = 217\n ** module chunks = 0\n **/\n//# sourceURL=webpack:///./src/app/components/recipeDetail/Directions.jsx?");

/***/ }

});