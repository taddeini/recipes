﻿var React = require('react');
var Reflux = require('reflux');
var RecipeActions = require('../../actions/RecipeActions.js');
var RecipeDetailStore = require('../../stores/RecipeStore.js');
var Ingredients = require('./Ingredients.jsx');
var Directions = require('./Directions.jsx');

var RecipeDetail = React.createClass({
   mixins: [Reflux.ListenerMixin],

   getInitialState: function () {
      return RecipeDetailStore.getRecipeByUrlTitle(this.props.urlTitle);
   },

   componentDidMount: function () {
      this.listenTo(RecipeDetailStore, this.onRecipeDetailsChanged);
      RecipeActions.load();
   },

   onRecipeDetailsChanged: function () {
      this.setState(RecipeDetailStore.getRecipeByUrlTitle(this.props.urlTitle));
   },

   render: function () {
      return (
         <div id='recipeDetail'>
            <section id='intro'>
               <h1>{this.state.title}</h1>
               <div className='row facts'>
                  <div className='four columns fact'>
                     Serves&nbsp;<strong>{this.state.serves}</strong>
                  </div>
                  <div className='four columns fact'>
                     Total&nbsp;Time&nbsp;<strong>{this.state.totalTime}&nbsp;mins</strong>
                  </div>
                  <div className='four columns fact'>
                     Prep&nbsp;Time&nbsp;<strong>{this.state.prepTime}&nbsp;mins</strong>
                  </div>
               </div>
               <div className='row'>
                  <div className='twelve columns'>{this.state.description}</div>
               </div>
            </section>
            <section id='ingredients' className='row'>
               <h3>Ingredients</h3>
               <Ingredients ingredients={this.state.ingredients} />
            </section>
            <section id='directions' className='row'>
               <h3>Directions</h3>
               <Directions directions={this.state.directions} />
            </section>
         </div>
      );
   }
});

module.exports = RecipeDetail;