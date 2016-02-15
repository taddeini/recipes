var React = require('react');

var IngredientsDetail = React.createClass({
    render: function () {
        return (
            <ul>
                <li><span className='amount'>1</span> proin mi dolor</li>
                <li><span className='amount'>1/2</span> non euismod quis</li>
                <li><span className='amount'>3</span> vivamus dapibus</li>
                <li><span className='amount'>1/4</span> proin mi dolor</li>
                <li><span className='amount'>1 1/2</span> vivamus dapibus</li>
            </ul>
        );
    }
});

module.exports = IngredientsDetail;