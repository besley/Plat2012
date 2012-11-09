(function ($) {
	// Title: surge.core.js
	//
	// Provides common functionality used by other components. The functions and classes in this file
	// define the basis of the Surge Platform JS libraries, and it must be included before any others.

	function Class(className, superClass, newMethods) {
		// validate className and super class
		if (className == null || superClass == null) {
			throw "Class name and super class must be valid";
		}

		if (typeof (className) !== 'string') {
			return AnonymousClass(className, superClass);
		}

		// validate className
		var components = className.split('.');
		for (var i = 0; i < components.length; i++) {
			if (!(/^[a-zA-Z_]\w*$/.test(components[i]))) {
				throw "Class name is not valid";
			}
		}

		var klass = (arguments.length == 2) ? AnonymousClass(Object, superClass) : AnonymousClass(superClass, newMethods);
		klass.className = components.pop();
		klass.classNamespace = components.join('.');

		var ns = window;
		for (var i = 0; i < components.length; ++i) {
			if (!ns[components[i]])
				ns[components[i]] = {};

			ns = ns[components[i]];
		}

		ns[klass.className] = klass;
		return klass;
	};


	function AnonymousClass(superClass, newMethods) {
			var scp = superClass.prototype;
			var fn = function () { }, subProto;
			// subClass = newMethods.constructor if provided or the execution of superClass (passing through subClass context and aruguments)
			var subClass = newMethods.constructor != Object.prototype.constructor ? newMethods.constructor : function () { superClass.apply(this, arguments); };
			// Merge any properties from superClass into subClass
			$.extend(subClass, superClass);
			
			// Create new function with a prototype of superClass.prototype and set it as
			// subClass.prototype
			fn.prototype = superClass.prototype;
			subClass.prototype = subProto = new fn();
			subClass.prototype.constructor = subClass;
			subClass.superclass = superClass;

			// Copy superClass properties into subClass
			subClass.properties = $.extend({}, superClass.properties);

			// setup subclasses properties
			if (!superClass.subclasses)
					superClass.subclasses = [];
			subClass.subclasses = [];
			superClass.subclasses.push(subClass);

			// Push members to new prototype
			for (var method in newMethods) {
					var item = newMethods[method];
					if (Surge.Property && item instanceof Surge.Property) {
							item.extendClass(subClass, method);
					} else if (method.charAt(0) == '$') {
							subClass[method.substr(1)] = item;
					} else {
							subClass.prototype[method] = item;
					}
			}

			if (subClass.initializeClass)
					subClass.initializeClass();

			return subClass;
	};

	function Define(ns, dict) {
			var components = ns.split("."), location = window;

			for (var i = 0; i < components.length; ++i) {
					if (!location[components[i]])
							location[components[i]] = {};

					location = location[components[i]];
			}

			return $.extend(location, dict);
	}

	// Namespace: Surge
	// The root namespace for classes and functions provided by the Surge Platform.
	$.extend(true, window, {
			Surge: { }
	});

	Define('Surge', {
		// Function: Class
		// Defines a new class. Details TBD.
		//
		// Example:
		// (code)
		// Class("MyApp.NiftyThing", Surge.Component, {
		//   MyProperty: new Surge.Property(), // auto property
		//   MyTrackedProperty: new Surge.Property({changeTracked: true}), // see below
		//
		//   constructor: function (myProperty) {
		//     this.setMyProperty(myProperty); // setter is defined automatically
		//   },
		//
		//   someMethod: function () {
		//     return this.getMyProperty() * 100;
		//   },
		//
		//   $staticMethod: function () {
		//     alert("It's super effective!");
		//   }
		// });
		//
		// var instance = new MyApp.NiftyThing(10);
		// alert(instance.someMethod());
		// MyApp.NiftyThing.staticMethod();
		// instance.listen('valueChanged', function (propertyName, oldValue, newValue) {
		//   alert(newValue);
		// });
		// instance.setMyTrackedProperty(7);
		// (end)
		Class: Class,
		// Function: Define
		//
		// Adds a dictionary of items at a named location, creating any intermediate objects required.
		//
		// Parameters:
		//  ns - The name of a namespace. This should be a dotted list of components, e.g. Surge.Math.Geometry.
		//  dict - An object. Each property of the object will be added to the namespace referenced by ns.
		//
		// Example:
		// (begin code)
		// Surge.Define("Foo.Bar", {
		//     hello: function () {
		//         alert("Hello, World!");
		//     }
		// });
		//
		// Foo.Bar.hello();
		// (end)
		Define: Define
	});



	(function () {
		var identifierTable = {};

		// Function: Surge.generateIdentifier
		// Generates a unique identifier with the given prefix. Each prefix has its own counter.
		//
		// Example:
		// (code)
		// Surge.generateIdentifier("foo"); // foo-1
		// Surge.generateIdentifier("foo"); // foo-2
		// Surge.generateIdentifier("foo"); // foo-3
		// Surge.generateIdentifier("bar"); // bar-1
		// (end)
		$.extend(Surge, {
			generateIdentifier: function (prefix) {
				if (identifierTable[prefix])
					return prefix + "-" + (++identifierTable[prefix]);
				return prefix + "-" + (identifierTable[prefix] = 1);
			}
		});
	})();

	// Class: Surge.Component
	// A base class which provides common mechanisms for events, properties, and naming.
	//
	// Constructor Parameters:
	// properties - optional hash to set property values
	Class("Surge.Component", Object, {
			$initializeClass: function () {
					var defaults = {};

					for (var propertyName in this.properties) {
							var property = this.properties[propertyName];

							if (property.options['default']) {
									defaults[property.name] = property.options['default'];
									property.setValue(this.prototype, property.options['default']);
							}
					}

					this.prototype.defaults = defaults;
			},

			constructor: function (properties) {
					if (properties)
							this.setPropertyValues(properties);
			},

			// Method: componentId
			// Returns a unique string identifier for the component. The ID is generated the first
			// time it is requested.
			componentId: function () {
					if (!this.__componentId)  
							this.__componentId = Surge.generateIdentifier(this.constructor.className ? this.constructor.className : "Component");

					return this.__componentId;
			},

			// Method: setPropertyValues
			// Sets multiple property values simultaneously from a dictionary.
			setPropertyValues: function (propertyValues) {
					for (var member in propertyValues) {
							var val = propertyValues[member];
							this.constructor.properties[member].setValue(this, val);
					}
			},

			// Method: listen
			// Listens for an event.
			//
			// Parameters:
			// eventName - The name of the event to listen for. This must exactly match the event name passed to <signal>. 
			//             A special name, *, may be used to listen for any event.
			// handler - A function to be invoked when the event is signalled. The handler function is provided
			//           the target object as its 'this' value, and any additional arguments to <signal> will also
			//           be passed to the handler function.
			listen: function (eventName, handler) {
					if (!this._listeners)
							this._listeners = {};

					var listeners = this._listeners[eventName];
					if (!listeners)
							listeners = this._listeners[eventName] = [];

					listeners.push(handler);
			},

			// Method: unlisten
			// Removes an event handler that was previously added using <listen>.
			unlisten: function (eventName, listener) {
					if (!this._listeners)
							return;

					var listeners = this._listeners[eventName];

					if (listeners) {
							for (var i = 0; i < listeners.length; ++i) {
									if (listeners[i] == listener) {
											listeners.splice(i, 1);
											return true;
									}
							}
					}

					return false;
			},

			// Method: signal
			// Signals an event.
			signal: function (eventName) {
					var eventArgs = [];

					for (var i = 1; i < arguments.length; ++i)
							eventArgs.push(arguments[i]);

					if (!this._listeners)
							return;

					if (this._eventSuppressions && this._eventSuppressions[eventName])
							return;

					if (eventName != '*')
							this.signal.apply(this, ['*', arguments]);

					var listeners = this._listeners[eventName];
					if (!listeners)
							return;

					for (var i = listeners.length - 1; i >= 0; --i)
							listeners[i].apply(this, eventArgs);
			},

			// Method: suppress
			// Suppresses all handlers for an event until <restore> is called. If suppress
			// is called for an event name which is already suppressed, the method does nothing:
			// the next call to restore will still end the suppression (no nesting is allowed).
			suppress: function (eventName) {
					if (!this._eventSuppressions)
							this._eventSuppressions = {};

					this._eventSuppressions[eventName] = true;
			},

			// Method: restore
			// Remove an event suppression set by <suppress>.
			restore: function (eventName) {
					if (!this._eventSuppressions)
							return;

					this._eventSuppressions[eventName] = false;
			},

			dispose: function () {
			}
	});

	// Class: Surge.Property
	// Property generator used to generate get and set methods. Can also generate properies which will raise 'change' events
	// Once instantiated use extendClass() to extend an object with these abilities
	// see <Class>
	//
	// Constructor Paremters Object Hash:
	//						changeTracked - inject extra code to signal 'valueChanged' when the setter is invoked
	//						nameOfOperation - 'private' indicates methods for that operation will be marked as private to the best of the frameworks abilities
	//						getter - custom function that will act as the property getter
	//						setter - custom function that wil act as the property setter
	Class("Surge.Property", Object, {
			$defaultOptions: {},

			constructor: function (options) {
					this.options = $.extend({}, this.constructor.defaultOptions, options);
			},

			// Method: getMethodName
			// Get the method name of preforming the given operation following conventions
			getMethodName: function (operation) {
					return (this.options[operation] == 'private' ? "_" : "") + operation + this.name;
			},

			// Method: getValue
			// Use the getter to get value of this property on the given object
			getValue: function (obj) {
					return this._getFn.call(obj);
			},

			// Method: setValue
			// Use the setter to set the value of this property on the given object
			setValue: function (obj, val) {
					return this._setFn.call(obj, val);
			},

			// Method: isLoaded
			isLoaded: function (obj) {
					return true;
			},

			_updateSubclasses: function (klass, name) {
					for (var i = 0; i < klass.subclasses.length; ++i) {
							var subClass = klass.subclasses[i];

							if (!subClass.properties[name])
									subClass.properties[name] = this;

							if (subClass.subclasses)
									this._updateSubclasses(subClass, name);
					}
			},

			// Method: extendClass
			// Inject property into the given class' prototype
			// including appending this to the class' list of properties and updating any subclasses
			// 
			// Parameters:
			// klass - The class to be extended
			// name - The name of this property
			extendClass: function (klass, name) {
					this.name = name;
					this.generateCode(klass.prototype);
					klass.properties[name] = this;

					if (klass.subclasses)
							this._updateSubclasses(klass, name);
			},

			// Method: generateCode
			// Inject code for property operations (eg getters and setters) into the provided function prototype
			// Prefer extendClass
			generateCode: function (proto) {
					var getId = this.getMethodName('get');
					var setId = this.getMethodName('set');
					var fieldId = '_' + this.name;
					var THIS = this;

					if (this.options.getter)
							proto[getId] = this._getFn = this.options.getter;
					else
							proto[getId] = this._getFn = new Function("return this." + fieldId + ";");

					if (this.options.setter) {
							proto[setId] = this._setFn = this.options.setter;
					} else {
							if (this.options.changeTracked) {
									proto[setId] = this._setFn = function (val) {
											var oldval = this[fieldId];
											this[fieldId] = val;
											this.signal('valueChanged', THIS.name, val, oldval);
									};
							} else {
									proto[setId] = this._setFn = function (val) {
											this[fieldId] = val;
									}
							}
					}
			}
	});


	// Namespace: Surge.Console
	// Encapsulates a backlogging wrapper to the standard window.console object.
	//
	// The purpose of wrapping window.console into Surge.Console is that window.console is not
	// gaurenteed to be defined. Another draw back is that not all browsers support the
	// modern feature set that Firebug/Chrome support (see: http://getfirebug.com/logging )
	// This prevents fatal exceptions from occuring in code in some browsers.
	//
	// Surge.Console.log and Surge.Console.error are aliased as _log and _error.
    Surge.Define("Surge.Console", {
			_backlog: [],
			_groupDepth: 0, // Used by software emulated begin/end group
			_times: {},
			_groupEmulation: false,
			_call: function (type, args) {
					if (!window.console)
							Surge.Console._backlog.push({ type: type, args: args });
					else {
							if (Surge.Console._backlog.length > 0)
									Surge.Console.backlog();

							// Emulate unsupported features or throw an error.
							if (!window.console[type]) {
									var joinedArgs = Array.prototype.join.call(args, " ");
									if (type == "group") {
											Surge.Console._groupEmulation = true;
											Surge.Console._groupDepth++;
											Surge.Console.log("Group: " + Array.prototype.join.call(args, " "));
									} else if (type == "groupEnd") {
											if (--Surge.Console._groupDepth <= 0) {
													Surge.Console._groupDepth = 0;
													Surge.Console._groupEmulation = false;
											}
									} else if (type == "time") {
											Surge.Console._times[joinedArgs] = new Date().getTime();
									} else if (type == "timeEnd") {
											Surge.Console.log([joinedArgs, ": ", new Date().getTime() - (Surge.Console._times[joinedArgs] || 0), "ms"].join(""));
									} else if (window.console.error_) { console.error("Unsupported console event of type '" + type + "' with arguments [" + Array.prototype.join.call(args, ", ") + "]"); }
							} else if (window.console[type]) {
									if (type == "log" && Surge.Console._groupEmulation)
											args[0] = [new Array(Surge.Console._groupDepth + 1).join("-"), "> ", args[0]].join("");

									if (console[type].apply)
											console[type].apply(console, args); // IE8 does not support .apply like this.
									else
											console[type](Array.prototype.join.call(args, " "));
							}
					}
			},

			// Function: Surge.Console.backlog
			// Flushes the backlog to the display. This will happen automatically the first time Surge.Console is called and window.console exists.
			backlog: function () {
					if (!Surge.Console._backlog.length)
							return console.log("** Backlog is empty");
					Surge.Console.group("** Begin backlog of " + Surge.Console._backlog.length + " items");
					$.each(Surge.Console._backlog, function () { Surge.Console[this.type].apply(Surge.Console, this.args); })
					Surge.Console.groupEnd();
					Surge.Console._backlog = [];
			},

			// Function Surge.Console.dumpObject
			// Recursively dumps an object/array structure to Surge.Console.log
			dumpObject: function (object, maxDepth, curDepth, name) {
					function canRecurseObject() { return $.isArray(object) || $.isPlainObject(object); }
					function __log(text) { Surge.Console.log(text.join("")); }
					if (curDepth == undefined) curDepth = 0;
					if (maxDepth && ++curDepth >= maxDepth) return;

					var showName = name != undefined ? name : "unnamed";
					if ($.isArray(object)) {
							Surge.Console.group(["Array (length: ", object.length, ", name: ", showName, ")"].join(""));
							$.each(object, function (index, value) {
									if (canRecurseObject(value))
											Surge.Console.dumpObject(value, maxDepth, curDepth, index);
									else
											__log(["[", index, "] ", value]);
							});
							Surge.Console.groupEnd();
					} else if ($.isPlainObject(object)) {
							var keys = _(object).keys();
							Surge.Console.group(["Object (length: ", keys.length, ", name: ", showName, "):"].join(""));
							$.each(keys, function () {
									if (canRecurseObject(object[this]))
											Surge.Console.dumpObject(object[this], maxDepth, curDepth, this);
									else
											__log(["[", this, "] ", object[this]]);
							});
							Surge.Console.groupEnd();
					} else
							__log(["[", showName, "] ", object]);
			}
	});

	$.each(["log", "error", "debug", "info", "warn", "time", "timeEnd", "profile", "profileEnd", "trace", "group", "groupEnd", "dir", "dirxml"], function (index, functionName) {
			Surge.Console[functionName] = function () { Surge.Console._call(functionName, arguments); };
	});

	$.extend(true, window, {
		// Function: _log()
		// Alias for <Surge.Console>.log
		_log: Surge.Console.log,
		// Function: _error()
		// Alias for <Surge.Console>.error
		_error: Surge.Console.error
	});

	// Function: $.fn.dumpObject(detailed)
	// Dump information about a DOM element. Including any attached jQuery events and templating data.
	// 
	// Parameters:
	//	detailed - If set to true handler functions for events and templating functions are also printed to the console,
	//                 additionally parent and children are also checked for common events or templating data.
	// Depends On
	// - <Surge.Console>
	$.extend($.fn, {
		dumpObject: function (detailed) {
			function _getSelector(obj, noParents) {
				var selector = [];
				
				var tag = obj[0] && obj[0].tagName ? obj[0].tagName : "";
				if (tag)
					selector.push(tag);

				if (obj.attr("id"))
					selector.push("#" + obj.attr("id"));

				var classes = $.trim(obj.attr("class")).substr(0, 50);
				if (classes && tag.toLowerCase() != "html")
					selector.push("." + classes.replace(/\s+/g, " ").split(" ").join("."));

				if (noParents !== true)
					$(".content-container.double-wide").parents().each(function() { selector.unshift(_getSelector($(this), true) + " > "); });

				return selector.join("") || "(No Selector)";
			}
			Surge.Console.group(['$("', this.selector, '")'].join(""));
			var selfTop = this;
			this.each(function (index) {
				Surge.Console.group(['$("', selfTop.selector, '")[', index, "]"].join(""));
				var self = $(this);
				var data = $(this).data();
				if (data["events"]) {
					Surge.Console.group("Events");
					$.each(data["events"], function (name, object) {
						var description = ["type: ", name, ", amount: ", object.length].join("");
						if (detailed === true) {
							Surge.Console.group(description);
							function _hasSameEvent(obj) { return $(obj).data("events") && ($(obj).data("events")[name] !== undefined || ($(obj).data("events").live && _.any($(obj).data("events").live, function (f) { f.origType == name; }))); }
							var parents = _.select(self.parents(), _hasSameEvent);
							var children = _.select(self.find("*"), _hasSameEvent);
							_log(parents.length + " parents have this event" + (parents.length ? (": " + _.map(parents, function (obj) { return '$("' + _getSelector($(obj)) + '")' })) : ""));
							_log(children.length + " children have this event" + (children.length ? (": " + _.map(children, function (obj) { return '$("' + _getSelector($(obj)) + '")' })) : ""));

							$.each(object, function (index, event) {
								Surge.Console.log(["[", index, "]: ", event.handler].join(""));
							});
							Surge.Console.groupEnd();
						} else
							_log(description);
					});
					Surge.Console.groupEnd();
				} else
					_log("No events");

				self.parents().each(function(i, t) {
					var parent = $(this);
					var data = $(this).data();
					$.each(data, function(key, val) {
						if (val && val.events && val.events.live)
							$.each(val.events.live, function(index, event) {
								if (self.is(event.selector))
									_log("Parent " + _getSelector(parent) + " has delegate mirroring $(\"" + event.selector + "\")." + event.origType + (detailed ? "(" + event.origHandler + ")" : ""));
							});
					});
				});

				function _fromTemplate(obj) {
					tmplItem = $(obj).data("tmplItem");
					if (tmplItem)
						_dumpTemplateData($(obj), tmplItem);

					return !!tmplItem;
				}
				function _dumpTemplateData(obj, tmplItem) {
					Surge.Console.group("Templating Data for $(\"" + _getSelector(obj) + "\")");
					_log("Data:");
					_log(tmplItem.data);
					if (detailed) {
						_log("Generated Templating Function:");
						_log(tmplItem.tmpl);
					}
					Surge.Console.groupEnd();
				}
				if (!_fromTemplate(this))
					_log("Primary object has no templating data");

				if (detailed) {
					var parents = _.select(self.parents(), _fromTemplate);
					var children = _.select(self.find("*"), _fromTemplate);
				}

				Surge.Console.groupEnd();
			});
			Surge.Console.groupEnd();

			return this;
		}
	});

})(jQuery);
