var React = require('react');

var Ingredients = require('./ingredients.jsx');
var Directions = require('./directions.jsx');

var RecipeDetail = React.createClass({
    getInitialState: function () {
        return {
            title: 'Chicken Cacciatore',
            description: 'Semper urna. In nec maximus metus. Proin mi dolor, pellentesque non euismod quis, tristique eu quam. Vivamus dapibus faucibus mattis. Maecenas scelerisque quam in ex scelerisque, sit amet ultrices felis elementum.',
            serves: 12,
            prepTime: 30,
            totalTime: 45,
            ingredients: [],
            directions: []
        }
    },

    render: function () {
        return (
            <div id='recipeDetail'>
                <section id='intro'>
                    <h1>{this.state.title}</h1>
                    <div className='row facts'>
                        <div className='four columns fact'>
                            Total&nbsp;Time&nbsp;<strong>{this.state.totalTime}&nbsp;mins</strong>
                        </div>
                        <div className='four columns fact'>
                            Prep&nbsp;Time&nbsp;<strong>{this.state.prepTime}&nbsp;mins</strong>
                        </div>
                        <div className='four columns fact'>
                            Serves&nbsp;<strong>{this.state.serves}</strong>
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