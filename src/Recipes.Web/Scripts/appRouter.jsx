var React = require('react');
var RouterMixin = require('react-mini-router').RouterMixin;

var Search = require('./search/search.jsx');

var AppRouter = React.createClass({
    mixins: [RouterMixin],

    routes: {
        '/': 'search'
    },

    search: function () {
        return <Search />
    },

    render: function () {
        return this.renderCurrentRoute();
    }
});

module.exports = AppRouter;