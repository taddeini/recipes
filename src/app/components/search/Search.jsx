var React = require('react');
var Reflux = require('reflux');
var RecipeActions = require('../../actions/RecipeActions.js');
var RecipeStore = require('../../stores/RecipeStore.js');
var SearchResult = require('./SearchResult.jsx');

var Search = React.createClass({
  mixins: [Reflux.ListenerMixin],

  getInitialState: function() {
    return {
      searchResults: RecipeStore.getRecipes()
    };
  },

  componentDidMount: function() {
    this.listenTo(RecipeStore, this.onRecipesLoaded);
    RecipeActions.load();
  },

  onRecipesLoaded: function() {
    this.setState({ searchResults: RecipeStore.getRecipes() });
  },

  render: function() {
    return (
      <div id='search' className='container'>
        <h1>Find a Recipe</h1>
        <form>
          <input type='search' autoFocus placeholder='Search by recipe title or ingredient...' />
          <span className='search-icon fa fa-search' />
        </form>
        <div className='searchResults'>
          {
            this.state.searchResults.map(function(result, key) {
              return <SearchResult key={key} {...result} />
            })
          }
        </div>
      </div>
    );
  }
});

module.exports = Search;
