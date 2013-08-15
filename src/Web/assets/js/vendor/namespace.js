/* ====== namespace.js ====== */

/**
 * Declares a namespace in JavaScript.
 *
 * @param {String} ns
 *  The name of the namespace to declare.
 * @param {Array} usings
 *  An array of objects (e.g. other namespaces) for which we want to create
 *  aliases in our namespace declaration. This parameter is optional.
 * @param {function(aliases...)} fn
 *  A function which is called to set up the namespace. It will receive the
 *  namespace object as its value for `this`, and the elements of the `usings`
 *  array as its arguments. To add new functions, classes etc to the namespace,
 *  assign them as properties of `this`.
 * @returns {Object}
 *  The namespace object.
 */

function namespace(ns, usings, fn) {
    "use strict";

    if (typeof fn === 'undefined') {
        fn = usings;
        usings = [];
    }
    if (usings.constructor !== Array) {
        throw "Usings must be an array.";
    }

    var path = ns.split('.');
    var target = window;
    for (var i = 0; i < path.length; i++) {
        var part = path[i];
        if (target.hasOwnProperty(part)) {
            target = target[part];
        }
        else {
            target = target[part] = {};
        }
    }

    if (typeof fn === "function") {
        fn.apply(target, usings);
    }
    else {
        for (var key in fn) {
            if (fn.hasOwnProperty(key)) {
                target[key] = fn[key];
            }
        }
    }
    return target;
}
