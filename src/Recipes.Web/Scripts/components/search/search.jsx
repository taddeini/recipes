var React = require('react');

var Search = React.createClass({
    render: function () {
        return (
            <div id='search'>
                <h1>Find a Recipe</h1> 
                <form>
                    <input type='search' autoFocus placeholder='Search by name or ingredient...' />
                    <span className='search-icon fa fa-search' />
                </form>   
            </div>
        );
    }
});

module.exports = Search;
