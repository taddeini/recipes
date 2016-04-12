webpackJsonp([0],{

/***/ 0:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	__webpack_require__(3);
	__webpack_require__(7);
	__webpack_require__(9);

	var React = __webpack_require__(13);
	var ReactDOM = __webpack_require__(170);
	var AppRouter = __webpack_require__(171);

	ReactDOM.render(React.createElement(AppRouter, { history: 'true' }), document.getElementById('main'));

/***/ },

/***/ 3:
/***/ function(module, exports) {

	// removed by extract-text-webpack-plugin

/***/ },

/***/ 7:
/***/ function(module, exports) {

	// removed by extract-text-webpack-plugin

/***/ },

/***/ 9:
/***/ function(module, exports) {

	// removed by extract-text-webpack-plugin

/***/ },

/***/ 170:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	module.exports = __webpack_require__(15);


/***/ },

/***/ 171:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);
	var RouterMixin = __webpack_require__(172).RouterMixin;
	var Search = __webpack_require__(182);
	var NotFound = __webpack_require__(206);
	var RecipeDetail = __webpack_require__(207);

	var AppRouter = React.createClass({
	  displayName: 'AppRouter',

	  mixins: [RouterMixin],

	  routes: {
	    '/recipes/:urlTitle?': 'recipeDetail',
	    '/': 'search'
	  },

	  search: function search() {
	    return React.createElement(Search, null);
	  },

	  recipeDetail: function recipeDetail(urlTitle) {
	    return React.createElement(RecipeDetail, { urlTitle: urlTitle });
	  },

	  notFound: function notFound() {
	    return React.createElement(NotFound, null);
	  },

	  render: function render() {
	    return this.renderCurrentRoute();
	  }
	});

	module.exports = AppRouter;

/***/ },

/***/ 172:
/***/ function(module, exports, __webpack_require__) {

	module.exports = {
	    RouterMixin: __webpack_require__(173),
	    navigate: __webpack_require__(180)
	};

/***/ },

/***/ 173:
/***/ function(module, exports, __webpack_require__) {

	var React = __webpack_require__(13),
	    ReactDOM = __webpack_require__(170),
	    EventListener = __webpack_require__(174),
	    getEventTarget = __webpack_require__(93),
	    pathToRegexp = __webpack_require__(176),
	    urllite = __webpack_require__(178),
	    detect = __webpack_require__(179);

	var PropValidation = {
	    path: React.PropTypes.string,
	    root: React.PropTypes.string,
	    useHistory: React.PropTypes.bool
	};

	module.exports = {

	    propTypes: PropValidation,

	    contextTypes: PropValidation,

	    childContextTypes: PropValidation,

	    getChildContext: function() {
	        return {
	            path: this.state.path,
	            root: this.state.root,
	            useHistory: this.state.useHistory
	        }
	    },

	    getDefaultProps: function() {
	        return {
	            routes: {}
	        };
	    },

	    getInitialState: function() {
	        return {
	            path: getInitialPath(this),
	            root: this.props.root || this.context.path || '',
	            useHistory: (this.props.history || this.context.useHistory) && detect.hasPushState
	        };
	    },

	    componentWillMount: function() {
	        this.setState({ _routes: processRoutes(this.state.root, this.routes, this) });
	    },

	    componentDidMount: function() {
	        var _events = this._events = [];

	        _events.push(EventListener.listen(ReactDOM.findDOMNode(this), 'click', this.handleClick));

	        if (this.state.useHistory) {
	            _events.push(EventListener.listen(window, 'popstate', this.onPopState));
	        } else {
	            if (window.location.hash.indexOf('#!') === -1) {
	                window.location.hash = '#!/';
	            }

	            _events.push(EventListener.listen(window, 'hashchange', this.onPopState));
	        }
	    },

	    componentWillUnmount: function() {
	        this._events.forEach(function(listener) {
	           listener.remove();
	        });
	    },

	    onPopState: function() {
	        var url = urllite(window.location.href),
	            hash = url.hash || '',
	            path = this.state.useHistory ? url.pathname : hash.slice(2);

	        if (path.length === 0) path = '/';

	        this.setState({ path: path + url.search });
	    },

	    renderCurrentRoute: function() {
	        var path = this.state.path,
	            url = urllite(path),
	            queryParams = parseSearch(url.search);

	        var parsedPath = url.pathname;

	        if (!parsedPath || parsedPath.length === 0) parsedPath = '/';

	        var matchedRoute = this.matchRoute(parsedPath);

	        if (matchedRoute) {
	            return matchedRoute.handler.apply(this, matchedRoute.params.concat(queryParams));
	        } else if (this.notFound) {
	            return this.notFound(parsedPath, queryParams);
	        } else {
	            throw new Error('No route matched path: ' + parsedPath);
	        }
	    },

	    handleClick: function(evt) {
	        var self = this,
	            url = getHref(evt);

	        if (url && self.matchRoute(url.pathname)) {
	            if(evt.preventDefault) {
	                evt.preventDefault();
	            } else {
	                evt.returnValue = false;
	            }

	            // See: http://facebook.github.io/react/docs/interactivity-and-dynamic-uis.html
	            // Give any component event listeners a chance to fire in the current event loop,
	            // since they happen at the end of the bubbling phase. (Allows an onClick prop to
	            // work correctly on the event target <a/> component.)
	            setTimeout(function() {
	                var pathWithSearch = url.pathname + (url.search || '');
	                if (pathWithSearch.length === 0) pathWithSearch = '/';

	                if (self.state.useHistory) {
	                    window.history.pushState({}, '', pathWithSearch);
	                } else {
	                    window.location.hash = '!' + pathWithSearch;
	                }

	                self.setState({ path: pathWithSearch});
	            }, 0);
	        }
	    },

	    matchRoute: function(path) {
	        if (!path) {
	            return false;
	        }

	        var matchedRoute = {};

	        this.state._routes.some(function(route) {
	            var matches = route.pattern.exec(path);

	            if (matches) {
	                matchedRoute.handler = route.handler;
	                matchedRoute.params = matches.slice(1, route.params.length + 1);

	                return true;
	            }

	            return false;
	        });

	        return matchedRoute.handler ? matchedRoute : false;
	    }

	};

	function getInitialPath(component) {
	    var path = component.props.path || component.context.path,
	        hash,
	        url;

	    if (!path && detect.canUseDOM) {
	        url = urllite(window.location.href);

	        if (component.props.history) {
	            path = url.pathname + url.search;
	        } else if (url.hash) {
	            hash = urllite(url.hash.slice(2));
	            path = hash.pathname + hash.search;
	        }
	    }

	    return path || '/';
	}

	function getHref(evt) {
	    if (evt.defaultPrevented) {
	        return;
	    }

	    if (evt.metaKey || evt.ctrlKey || evt.shiftKey) {
	        return;
	    }

	    if (evt.button !== 0) {
	        return;
	    }

	    var elt = getEventTarget(evt);

	    // Since a click could originate from a child element of the <a> tag,
	    // walk back up the tree to find it.
	    while (elt && elt.nodeName !== 'A') {
	        elt = elt.parentNode;
	    }

	    if (!elt) {
	        return;
	    }

	    if (elt.target && elt.target !== '_self') {
	        return;
	    }

	    if (!!elt.attributes.download) {
	        return;
	    }

	    var linkURL = urllite(elt.href);
	    var windowURL = urllite(window.location.href);

	    if (linkURL.protocol !== windowURL.protocol || linkURL.host !== windowURL.host) {
	        return;
	    }

	    return linkURL;
	}

	function processRoutes(root, routes, component) {
	    var patterns = [],
	        path, pattern, keys, handler, handlerFn;

	    for (path in routes) {
	        if (routes.hasOwnProperty(path)) {
	            keys = [];
	            pattern = pathToRegexp(root + path, keys);
	            handler = routes[path];
	            handlerFn = component[handler];

	            patterns.push({ pattern: pattern, params: keys, handler: handlerFn });
	        }
	    }

	    return patterns;
	}

	function parseSearch(str) {
	    var parsed = {};

	    if (str.indexOf('?') === 0) str = str.slice(1);

	    var pairs = str.split('&');

	    pairs.forEach(function(pair) {
	        var keyVal = pair.split('=');

	        parsed[decodeURIComponent(keyVal[0])] = decodeURIComponent(keyVal[1]);
	    });

	    return parsed;
	}


/***/ },

/***/ 174:
/***/ function(module, exports, __webpack_require__) {

	/* WEBPACK VAR INJECTION */(function(process) {/**
	 * Copyright 2013-2015, Facebook, Inc.
	 *
	 * Licensed under the Apache License, Version 2.0 (the "License");
	 * you may not use this file except in compliance with the License.
	 * You may obtain a copy of the License at
	 *
	 * http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 *
	 * @providesModule EventListener
	 * @typechecks
	 */

	'use strict';

	var emptyFunction = __webpack_require__(175);

	/**
	 * Upstream version of event listener. Does not take into account specific
	 * nature of platform.
	 */
	var EventListener = {
	  /**
	   * Listen to DOM events during the bubble phase.
	   *
	   * @param {DOMEventTarget} target DOM element to register listener on.
	   * @param {string} eventType Event type, e.g. 'click' or 'mouseover'.
	   * @param {function} callback Callback function.
	   * @return {object} Object with a `remove` method.
	   */
	  listen: function (target, eventType, callback) {
	    if (target.addEventListener) {
	      target.addEventListener(eventType, callback, false);
	      return {
	        remove: function () {
	          target.removeEventListener(eventType, callback, false);
	        }
	      };
	    } else if (target.attachEvent) {
	      target.attachEvent('on' + eventType, callback);
	      return {
	        remove: function () {
	          target.detachEvent('on' + eventType, callback);
	        }
	      };
	    }
	  },

	  /**
	   * Listen to DOM events during the capture phase.
	   *
	   * @param {DOMEventTarget} target DOM element to register listener on.
	   * @param {string} eventType Event type, e.g. 'click' or 'mouseover'.
	   * @param {function} callback Callback function.
	   * @return {object} Object with a `remove` method.
	   */
	  capture: function (target, eventType, callback) {
	    if (target.addEventListener) {
	      target.addEventListener(eventType, callback, true);
	      return {
	        remove: function () {
	          target.removeEventListener(eventType, callback, true);
	        }
	      };
	    } else {
	      if (process.env.NODE_ENV !== 'production') {
	        console.error('Attempted to listen to events during the capture phase on a ' + 'browser that does not support the capture phase. Your application ' + 'will not receive some events.');
	      }
	      return {
	        remove: emptyFunction
	      };
	    }
	  },

	  registerDefault: function () {}
	};

	module.exports = EventListener;
	/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(16)))

/***/ },

/***/ 175:
/***/ function(module, exports) {

	/**
	 * Copyright 2013-2015, Facebook, Inc.
	 * All rights reserved.
	 *
	 * This source code is licensed under the BSD-style license found in the
	 * LICENSE file in the root directory of this source tree. An additional grant
	 * of patent rights can be found in the PATENTS file in the same directory.
	 *
	 * @providesModule emptyFunction
	 */

	"use strict";

	function makeEmptyFunction(arg) {
	  return function () {
	    return arg;
	  };
	}

	/**
	 * This function accepts and discards inputs; it has no side effects. This is
	 * primarily useful idiomatically for overridable function endpoints which
	 * always need to be callable, since JS lacks a null-call idiom ala Cocoa.
	 */
	function emptyFunction() {}

	emptyFunction.thatReturns = makeEmptyFunction;
	emptyFunction.thatReturnsFalse = makeEmptyFunction(false);
	emptyFunction.thatReturnsTrue = makeEmptyFunction(true);
	emptyFunction.thatReturnsNull = makeEmptyFunction(null);
	emptyFunction.thatReturnsThis = function () {
	  return this;
	};
	emptyFunction.thatReturnsArgument = function (arg) {
	  return arg;
	};

	module.exports = emptyFunction;

/***/ },

/***/ 176:
/***/ function(module, exports, __webpack_require__) {

	var isarray = __webpack_require__(177)

	/**
	 * Expose `pathToRegexp`.
	 */
	module.exports = pathToRegexp
	module.exports.parse = parse
	module.exports.compile = compile
	module.exports.tokensToFunction = tokensToFunction
	module.exports.tokensToRegExp = tokensToRegExp

	/**
	 * The main path matching regexp utility.
	 *
	 * @type {RegExp}
	 */
	var PATH_REGEXP = new RegExp([
	  // Match escaped characters that would otherwise appear in future matches.
	  // This allows the user to escape special characters that won't transform.
	  '(\\\\.)',
	  // Match Express-style parameters and un-named parameters with a prefix
	  // and optional suffixes. Matches appear as:
	  //
	  // "/:test(\\d+)?" => ["/", "test", "\d+", undefined, "?", undefined]
	  // "/route(\\d+)"  => [undefined, undefined, undefined, "\d+", undefined, undefined]
	  // "/*"            => ["/", undefined, undefined, undefined, undefined, "*"]
	  '([\\/.])?(?:(?:\\:(\\w+)(?:\\(((?:\\\\.|[^()])+)\\))?|\\(((?:\\\\.|[^()])+)\\))([+*?])?|(\\*))'
	].join('|'), 'g')

	/**
	 * Parse a string for the raw tokens.
	 *
	 * @param  {String} str
	 * @return {Array}
	 */
	function parse (str) {
	  var tokens = []
	  var key = 0
	  var index = 0
	  var path = ''
	  var res

	  while ((res = PATH_REGEXP.exec(str)) != null) {
	    var m = res[0]
	    var escaped = res[1]
	    var offset = res.index
	    path += str.slice(index, offset)
	    index = offset + m.length

	    // Ignore already escaped sequences.
	    if (escaped) {
	      path += escaped[1]
	      continue
	    }

	    // Push the current path onto the tokens.
	    if (path) {
	      tokens.push(path)
	      path = ''
	    }

	    var prefix = res[2]
	    var name = res[3]
	    var capture = res[4]
	    var group = res[5]
	    var suffix = res[6]
	    var asterisk = res[7]

	    var repeat = suffix === '+' || suffix === '*'
	    var optional = suffix === '?' || suffix === '*'
	    var delimiter = prefix || '/'
	    var pattern = capture || group || (asterisk ? '.*' : '[^' + delimiter + ']+?')

	    tokens.push({
	      name: name || key++,
	      prefix: prefix || '',
	      delimiter: delimiter,
	      optional: optional,
	      repeat: repeat,
	      pattern: escapeGroup(pattern)
	    })
	  }

	  // Match any characters still remaining.
	  if (index < str.length) {
	    path += str.substr(index)
	  }

	  // If the path exists, push it onto the end.
	  if (path) {
	    tokens.push(path)
	  }

	  return tokens
	}

	/**
	 * Compile a string to a template function for the path.
	 *
	 * @param  {String}   str
	 * @return {Function}
	 */
	function compile (str) {
	  return tokensToFunction(parse(str))
	}

	/**
	 * Expose a method for transforming tokens into the path function.
	 */
	function tokensToFunction (tokens) {
	  // Compile all the tokens into regexps.
	  var matches = new Array(tokens.length)

	  // Compile all the patterns before compilation.
	  for (var i = 0; i < tokens.length; i++) {
	    if (typeof tokens[i] === 'object') {
	      matches[i] = new RegExp('^' + tokens[i].pattern + '$')
	    }
	  }

	  return function (obj) {
	    var path = ''
	    var data = obj || {}

	    for (var i = 0; i < tokens.length; i++) {
	      var token = tokens[i]

	      if (typeof token === 'string') {
	        path += token

	        continue
	      }

	      var value = data[token.name]
	      var segment

	      if (value == null) {
	        if (token.optional) {
	          continue
	        } else {
	          throw new TypeError('Expected "' + token.name + '" to be defined')
	        }
	      }

	      if (isarray(value)) {
	        if (!token.repeat) {
	          throw new TypeError('Expected "' + token.name + '" to not repeat, but received "' + value + '"')
	        }

	        if (value.length === 0) {
	          if (token.optional) {
	            continue
	          } else {
	            throw new TypeError('Expected "' + token.name + '" to not be empty')
	          }
	        }

	        for (var j = 0; j < value.length; j++) {
	          segment = encodeURIComponent(value[j])

	          if (!matches[i].test(segment)) {
	            throw new TypeError('Expected all "' + token.name + '" to match "' + token.pattern + '", but received "' + segment + '"')
	          }

	          path += (j === 0 ? token.prefix : token.delimiter) + segment
	        }

	        continue
	      }

	      segment = encodeURIComponent(value)

	      if (!matches[i].test(segment)) {
	        throw new TypeError('Expected "' + token.name + '" to match "' + token.pattern + '", but received "' + segment + '"')
	      }

	      path += token.prefix + segment
	    }

	    return path
	  }
	}

	/**
	 * Escape a regular expression string.
	 *
	 * @param  {String} str
	 * @return {String}
	 */
	function escapeString (str) {
	  return str.replace(/([.+*?=^!:${}()[\]|\/])/g, '\\$1')
	}

	/**
	 * Escape the capturing group by escaping special characters and meaning.
	 *
	 * @param  {String} group
	 * @return {String}
	 */
	function escapeGroup (group) {
	  return group.replace(/([=!:$\/()])/g, '\\$1')
	}

	/**
	 * Attach the keys as a property of the regexp.
	 *
	 * @param  {RegExp} re
	 * @param  {Array}  keys
	 * @return {RegExp}
	 */
	function attachKeys (re, keys) {
	  re.keys = keys
	  return re
	}

	/**
	 * Get the flags for a regexp from the options.
	 *
	 * @param  {Object} options
	 * @return {String}
	 */
	function flags (options) {
	  return options.sensitive ? '' : 'i'
	}

	/**
	 * Pull out keys from a regexp.
	 *
	 * @param  {RegExp} path
	 * @param  {Array}  keys
	 * @return {RegExp}
	 */
	function regexpToRegexp (path, keys) {
	  // Use a negative lookahead to match only capturing groups.
	  var groups = path.source.match(/\((?!\?)/g)

	  if (groups) {
	    for (var i = 0; i < groups.length; i++) {
	      keys.push({
	        name: i,
	        prefix: null,
	        delimiter: null,
	        optional: false,
	        repeat: false,
	        pattern: null
	      })
	    }
	  }

	  return attachKeys(path, keys)
	}

	/**
	 * Transform an array into a regexp.
	 *
	 * @param  {Array}  path
	 * @param  {Array}  keys
	 * @param  {Object} options
	 * @return {RegExp}
	 */
	function arrayToRegexp (path, keys, options) {
	  var parts = []

	  for (var i = 0; i < path.length; i++) {
	    parts.push(pathToRegexp(path[i], keys, options).source)
	  }

	  var regexp = new RegExp('(?:' + parts.join('|') + ')', flags(options))

	  return attachKeys(regexp, keys)
	}

	/**
	 * Create a path regexp from string input.
	 *
	 * @param  {String} path
	 * @param  {Array}  keys
	 * @param  {Object} options
	 * @return {RegExp}
	 */
	function stringToRegexp (path, keys, options) {
	  var tokens = parse(path)
	  var re = tokensToRegExp(tokens, options)

	  // Attach keys back to the regexp.
	  for (var i = 0; i < tokens.length; i++) {
	    if (typeof tokens[i] !== 'string') {
	      keys.push(tokens[i])
	    }
	  }

	  return attachKeys(re, keys)
	}

	/**
	 * Expose a function for taking tokens and returning a RegExp.
	 *
	 * @param  {Array}  tokens
	 * @param  {Array}  keys
	 * @param  {Object} options
	 * @return {RegExp}
	 */
	function tokensToRegExp (tokens, options) {
	  options = options || {}

	  var strict = options.strict
	  var end = options.end !== false
	  var route = ''
	  var lastToken = tokens[tokens.length - 1]
	  var endsWithSlash = typeof lastToken === 'string' && /\/$/.test(lastToken)

	  // Iterate over the tokens and create our regexp string.
	  for (var i = 0; i < tokens.length; i++) {
	    var token = tokens[i]

	    if (typeof token === 'string') {
	      route += escapeString(token)
	    } else {
	      var prefix = escapeString(token.prefix)
	      var capture = token.pattern

	      if (token.repeat) {
	        capture += '(?:' + prefix + capture + ')*'
	      }

	      if (token.optional) {
	        if (prefix) {
	          capture = '(?:' + prefix + '(' + capture + '))?'
	        } else {
	          capture = '(' + capture + ')?'
	        }
	      } else {
	        capture = prefix + '(' + capture + ')'
	      }

	      route += capture
	    }
	  }

	  // In non-strict mode we allow a slash at the end of match. If the path to
	  // match already ends with a slash, we remove it for consistency. The slash
	  // is valid at the end of a path match, not in the middle. This is important
	  // in non-ending mode, where "/test/" shouldn't match "/test//route".
	  if (!strict) {
	    route = (endsWithSlash ? route.slice(0, -2) : route) + '(?:\\/(?=$))?'
	  }

	  if (end) {
	    route += '$'
	  } else {
	    // In non-ending mode, we need the capturing groups to match as much as
	    // possible by using a positive lookahead to the end or next path segment.
	    route += strict && endsWithSlash ? '' : '(?=\\/|$)'
	  }

	  return new RegExp('^' + route, flags(options))
	}

	/**
	 * Normalize the given path string, returning a regular expression.
	 *
	 * An empty array can be passed in for the keys, which will hold the
	 * placeholder key descriptions. For example, using `/user/:id`, `keys` will
	 * contain `[{ name: 'id', delimiter: '/', optional: false, repeat: false }]`.
	 *
	 * @param  {(String|RegExp|Array)} path
	 * @param  {Array}                 [keys]
	 * @param  {Object}                [options]
	 * @return {RegExp}
	 */
	function pathToRegexp (path, keys, options) {
	  keys = keys || []

	  if (!isarray(keys)) {
	    options = keys
	    keys = []
	  } else if (!options) {
	    options = {}
	  }

	  if (path instanceof RegExp) {
	    return regexpToRegexp(path, keys, options)
	  }

	  if (isarray(path)) {
	    return arrayToRegexp(path, keys, options)
	  }

	  return stringToRegexp(path, keys, options)
	}


/***/ },

/***/ 177:
/***/ function(module, exports) {

	module.exports = Array.isArray || function (arr) {
	  return Object.prototype.toString.call(arr) == '[object Array]';
	};


/***/ },

/***/ 178:
/***/ function(module, exports) {

	(function() {
	  var URL, URL_PATTERN, defaults, urllite,
	    __hasProp = {}.hasOwnProperty;

	  URL_PATTERN = /^(?:(?:([^:\/?\#]+:)\/+|(\/\/))(?:([a-z0-9-\._~%]+)(?::([a-z0-9-\._~%]+))?@)?(([a-z0-9-\._~%!$&'()*+,;=]+)(?::([0-9]+))?)?)?([^?\#]*?)(\?[^\#]*)?(\#.*)?$/;

	  urllite = function(raw, opts) {
	    return urllite.URL.parse(raw, opts);
	  };

	  urllite.URL = URL = (function() {
	    function URL(props) {
	      var k, v, _ref;
	      for (k in defaults) {
	        if (!__hasProp.call(defaults, k)) continue;
	        v = defaults[k];
	        this[k] = (_ref = props[k]) != null ? _ref : v;
	      }
	      this.host || (this.host = this.hostname && this.port ? "" + this.hostname + ":" + this.port : this.hostname ? this.hostname : '');
	      this.origin || (this.origin = this.protocol ? "" + this.protocol + "//" + this.host : '');
	      this.isAbsolutePathRelative = !this.host && this.pathname.charAt(0) === '/';
	      this.isPathRelative = !this.host && this.pathname.charAt(0) !== '/';
	      this.isRelative = this.isSchemeRelative || this.isAbsolutePathRelative || this.isPathRelative;
	      this.isAbsolute = !this.isRelative;
	    }

	    URL.parse = function(raw) {
	      var m, pathname, protocol;
	      m = raw.toString().match(URL_PATTERN);
	      pathname = m[8] || '';
	      protocol = m[1];
	      return new urllite.URL({
	        protocol: protocol,
	        username: m[3],
	        password: m[4],
	        hostname: m[6],
	        port: m[7],
	        pathname: protocol && pathname.charAt(0) !== '/' ? "/" + pathname : pathname,
	        search: m[9],
	        hash: m[10],
	        isSchemeRelative: m[2] != null
	      });
	    };

	    return URL;

	  })();

	  defaults = {
	    protocol: '',
	    username: '',
	    password: '',
	    host: '',
	    hostname: '',
	    port: '',
	    pathname: '',
	    search: '',
	    hash: '',
	    origin: '',
	    isSchemeRelative: false
	  };

	  module.exports = urllite;

	}).call(this);


/***/ },

/***/ 179:
/***/ function(module, exports) {

	var canUseDOM = !!(
	    typeof window !== 'undefined' &&
	    window.document &&
	    window.document.createElement
	);

	module.exports = {
	    canUseDOM: canUseDOM,
	    hasPushState: canUseDOM && window.history && 'pushState' in window.history,
	    hasHashbang: function() {
	        return canUseDOM && window.location.hash.indexOf('#!') === 0;
	    },
	    hasEventConstructor: function() {
	        return typeof window.Event == "function";
	    }
	};


/***/ },

/***/ 180:
/***/ function(module, exports, __webpack_require__) {

	var detect = __webpack_require__(179);
	var event = __webpack_require__(181);

	module.exports = function triggerUrl(url, silent) {
	    if (detect.hasHashbang()) {
	        window.location.hash = '#!' + url;
	    } else if (detect.hasPushState) {
	        window.history.pushState({}, '', url);
	        if (!silent) {
	            window.dispatchEvent(event.createEvent('popstate'));
	        }
	    } else {
	        console.error("Browser does not support pushState, and hash is missing a hashbang prefix!");
	    }
	};


/***/ },

/***/ 181:
/***/ function(module, exports, __webpack_require__) {

	var detect = __webpack_require__(179);

	module.exports = {
	    createEvent: function(name) {
	        if (detect.hasEventConstructor()) {
	            return new window.Event(name);
	        } else {
	            var event = document.createEvent('Event');
	            event.initEvent(name, true, false);
	            return event;
	        }
	    }
	};


/***/ },

/***/ 182:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

	var React = __webpack_require__(13);
	var Reflux = __webpack_require__(183);
	var RecipeActions = __webpack_require__(202);
	var RecipeStore = __webpack_require__(203);
	var SearchResult = __webpack_require__(205);

	var Search = React.createClass({
	  displayName: 'Search',

	  mixins: [Reflux.ListenerMixin],

	  getInitialState: function getInitialState() {
	    return {
	      searchResults: RecipeStore.getRecipes()
	    };
	  },

	  componentDidMount: function componentDidMount() {
	    this.listenTo(RecipeStore, this.onRecipesLoaded);
	    RecipeActions.load();
	  },

	  onRecipesLoaded: function onRecipesLoaded() {
	    this.setState({ searchResults: RecipeStore.getRecipes() });
	  },

	  render: function render() {
	    return React.createElement(
	      'div',
	      { id: 'search', className: 'container' },
	      React.createElement(
	        'h1',
	        null,
	        'Find a Recipe'
	      ),
	      React.createElement(
	        'form',
	        null,
	        React.createElement('input', { type: 'search', autoFocus: true, placeholder: 'Search by recipe title or ingredient...' }),
	        React.createElement('span', { className: 'search-icon fa fa-search' })
	      ),
	      React.createElement(
	        'div',
	        { className: 'searchResults' },
	        this.state.searchResults.map(function (result, key) {
	          return React.createElement(SearchResult, _extends({ key: key }, result));
	        })
	      )
	    );
	  }
	});

	module.exports = Search;

/***/ },

/***/ 202:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var Reflux = __webpack_require__(183);

	var RecipeActions = Reflux.createActions({
	  load: { children: ['completed', 'failed'] }
	});

	RecipeActions.load.listen(function () {
	  this.completed(_recipes);
	});

	var _recipes = [{
	  id: 1,
	  urlTitle: 'chicken-cacciatore',
	  title: 'Chicken Cacciatore',
	  description: 'Translated to "Hunter Style Chicken", this is a rustic chicken and vegetable stew.',
	  serves: 12,
	  prepTime: 30,
	  totalTime: 45,
	  ingredients: [],
	  directions: []
	}, {
	  id: 2,
	  urlTitle: 'nonnas-tomato-sauce',
	  title: 'Nonna\'s Tomato Sauce',
	  description: '',
	  serves: 4,
	  prepTime: 20,
	  totalTime: 90,
	  ingredients: [],
	  directions: []
	}];

	module.exports = RecipeActions;

/***/ },

/***/ 203:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var Reflux = __webpack_require__(183);
	var RecipeActions = __webpack_require__(202);
	var _ = __webpack_require__(204);

	var RecipeDetailStore = Reflux.createStore({
	  listenables: [RecipeActions],

	  init: function init() {
	    this.recipes = [];
	  },

	  onLoadCompleted: function onLoadCompleted(recipes) {
	    this.recipes = recipes;
	    this.trigger();
	  },

	  getRecipes: function getRecipes() {
	    return this.recipes;
	  },

	  getRecipeByUrlTitle: function getRecipeByUrlTitle(urlTitle) {
	    return _.find(this.recipes, function (recipe) {
	      return recipe.urlTitle === urlTitle;
	    }) || {};
	  }
	});

	module.exports = RecipeDetailStore;

/***/ },

/***/ 205:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);

	var SearchResult = React.createClass({
	  displayName: 'SearchResult',

	  render: function render() {
	    return React.createElement(
	      'div',
	      null,
	      React.createElement(
	        'a',
	        { href: 'recipes/' + this.props.urlTitle },
	        this.props.title
	      )
	    );
	  }
	});

	module.exports = SearchResult;

/***/ },

/***/ 206:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);

	var NotFound = React.createClass({
	  displayName: 'NotFound',

	  render: function render() {
	    return React.createElement(
	      'div',
	      { id: 'not-found' },
	      React.createElement(
	        'h1',
	        null,
	        '404 Page not Found'
	      )
	    );
	  }
	});

	module.exports = NotFound;

/***/ },

/***/ 207:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);
	var Reflux = __webpack_require__(183);
	var RecipeActions = __webpack_require__(202);
	var RecipeStore = __webpack_require__(203);
	var Ingredients = __webpack_require__(208);
	var Directions = __webpack_require__(209);

	var RecipeDetail = React.createClass({
	  displayName: 'RecipeDetail',

	  mixins: [Reflux.ListenerMixin],

	  getInitialState: function getInitialState() {
	    return RecipeStore.getRecipeByUrlTitle(this.props.urlTitle);
	  },

	  componentDidMount: function componentDidMount() {
	    this.listenTo(RecipeStore, this.onRecipeDetailsChanged);
	    RecipeActions.load();
	  },

	  onRecipeDetailsChanged: function onRecipeDetailsChanged() {
	    this.setState(RecipeStore.getRecipeByUrlTitle(this.props.urlTitle));
	  },

	  render: function render() {
	    return React.createElement(
	      'div',
	      { id: 'recipeDetail', className: 'container' },
	      React.createElement(
	        'section',
	        { id: 'intro' },
	        React.createElement(
	          'h1',
	          null,
	          this.state.title
	        ),
	        React.createElement(
	          'div',
	          { className: 'row facts' },
	          React.createElement(
	            'div',
	            { className: 'four columns fact' },
	            'Serves  ',
	            React.createElement(
	              'strong',
	              null,
	              this.state.serves
	            )
	          ),
	          React.createElement(
	            'div',
	            { className: 'four columns fact' },
	            'Total  Time  ',
	            React.createElement(
	              'strong',
	              null,
	              this.state.totalTime,
	              '  mins'
	            )
	          ),
	          React.createElement(
	            'div',
	            { className: 'four columns fact' },
	            'Prep  Time  ',
	            React.createElement(
	              'strong',
	              null,
	              this.state.prepTime,
	              '  mins'
	            )
	          )
	        ),
	        React.createElement(
	          'div',
	          { className: 'row' },
	          React.createElement(
	            'div',
	            { className: 'twelve columns' },
	            this.state.description
	          )
	        )
	      ),
	      React.createElement(
	        'section',
	        { id: 'ingredients', className: 'row' },
	        React.createElement(
	          'h3',
	          null,
	          'Ingredients'
	        ),
	        React.createElement(Ingredients, { ingredients: this.state.ingredients })
	      ),
	      React.createElement(
	        'section',
	        { id: 'directions', className: 'row' },
	        React.createElement(
	          'h3',
	          null,
	          'Directions'
	        ),
	        React.createElement(Directions, { directions: this.state.directions })
	      )
	    );
	  }
	});

	module.exports = RecipeDetail;

/***/ },

/***/ 208:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);

	var Ingredients = React.createClass({
	  displayName: 'Ingredients',

	  render: function render() {
	    return React.createElement(
	      'ul',
	      null,
	      React.createElement(
	        'li',
	        null,
	        React.createElement(
	          'span',
	          { className: 'amount' },
	          '1'
	        ),
	        ' proin mi dolor'
	      ),
	      React.createElement(
	        'li',
	        null,
	        React.createElement(
	          'span',
	          { className: 'amount' },
	          '1/2'
	        ),
	        ' non euismod quis'
	      ),
	      React.createElement(
	        'li',
	        null,
	        React.createElement(
	          'span',
	          { className: 'amount' },
	          '3'
	        ),
	        ' vivamus dapibus'
	      ),
	      React.createElement(
	        'li',
	        null,
	        React.createElement(
	          'span',
	          { className: 'amount' },
	          '1/4'
	        ),
	        ' proin mi dolor'
	      ),
	      React.createElement(
	        'li',
	        null,
	        React.createElement(
	          'span',
	          { className: 'amount' },
	          '1 1/2'
	        ),
	        ' vivamus dapibus'
	      )
	    );
	  }
	});

	module.exports = Ingredients;

/***/ },

/***/ 209:
/***/ function(module, exports, __webpack_require__) {

	'use strict';

	var React = __webpack_require__(13);

	var Directions = React.createClass({
	  displayName: 'Directions',

	  render: function render() {
	    return React.createElement(
	      'ol',
	      null,
	      React.createElement(
	        'li',
	        null,
	        'Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique.Fusce orci ex, varius vitae dui ut, placerat scelerisque eros.'
	      ),
	      React.createElement(
	        'li',
	        null,
	        'Maecenas vitae ante sit amet felis luctus volutpat at quis ligula.Pellentesque lectus mi, suscipit id iaculis vitae, euismod dignissim quam.'
	      ),
	      React.createElement(
	        'li',
	        null,
	        'Proin mi dolor, pellentesque non euismod quis, tristique eu quam.'
	      ),
	      React.createElement(
	        'li',
	        null,
	        'Nullam auctor ipsum nec arcu imperdiet, ut dignissim massa tristique.Fusce orci ex, varius vitae dui ut, placerat scelerisque eros.'
	      ),
	      React.createElement(
	        'li',
	        null,
	        'Maecenas vitae ante sit amet felis luctus volutpat at quis ligula.Pellentesque lectus mi, suscipit id iaculis vitae, euismod dignissim quam.'
	      ),
	      React.createElement(
	        'li',
	        null,
	        'Proin mi dolor, pellentesque non euismod quis, tristique eu quam.'
	      )
	    );
	  }
	});

	module.exports = Directions;

/***/ }

});