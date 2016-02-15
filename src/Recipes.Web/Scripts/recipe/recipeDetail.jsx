var React = require('react');

var RecipeDetail = React.createClass({
    render: function () {
        return (
            <div id='recipeDetail'>
                <section id='intro'>
                    <h1>Chicken Cacciatore</h1>
                    <div className='row facts'>
                        <div className='four columns fact'>
                            Total&nbsp;Time&nbsp;<strong>45&nbsp;mins</strong>
                        </div>
                        <div className='four columns fact'>
                            Prep&nbsp;Time&nbsp;<strong>30&nbsp;mins</strong>
                        </div>
                        <div className='four columns fact'>
                            Serves&nbsp;<strong>12</strong>
                        </div>
                    </div>
                    <div className='row'>
                        <div className='twelve columns'>
                            Semper urna. In nec maximus metus. Proin mi dolor, pellentesque non euismod quis, tristique eu quam. Vivamus
                            dapibus faucibus mattis. Maecenas scelerisque quam in ex scelerisque, sit amet ultrices felis elementum.
                        </div>
                    </div>
                </section>
                <section id='ingredients' className='row'>
                    <h3>Ingredients</h3>
                    <ul>
                        <li><span className='amount'>1</span> proin mi dolor</li>
                        <li><span className='amount'>1/2</span> non euismod quis</li>
                        <li><span className='amount'>3</span> vivamus dapibus</li>
                        <li><span className='amount'>1/4</span> proin mi dolor</li>
                        <li><span className='amount'>1 1/2</span> vivamus dapibus</li>
                    </ul>
                </section>
                <section id='directions' className='row'>
                    <h3>Directions</h3>
                    <ol>
                        <li>
                            Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique. Fusce orci ex, varius vitae dui ut,
                            placerat scelerisque eros.
                        </li>
                        <li>
                            Maecenas vitae ante sit amet felis luctus volutpat at quis ligula. Pellentesque lectus mi, suscipit id
                            iaculis vitae, euismod dignissim quam.
                        </li>
                        <li>Proin mi dolor, pellentesque non euismod quis, tristique eu quam.</li>
                        <li>
                            Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique. Fusce orci ex, varius vitae dui ut,
                            placerat scelerisque eros.
                        </li>
                        <li>
                            Maecenas vitae ante sit amet felis luctus volutpat at quis ligula. Pellentesque lectus mi, suscipit id
                            iaculis vitae, euismod dignissim quam.
                        </li>
                        <li>Proin mi dolor, pellentesque non euismod quis, tristique eu quam.</li>
                    </ol>
                </section>
            </div>
        )
    }
});

module.exports = RecipeDetail;