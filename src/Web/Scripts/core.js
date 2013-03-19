// @reference jquery-1.8.3.js

var dolstagis = window.dolstagis = {

  define: function define(ns, target) {
    "use strict";

    var parts = ns.split('.');
    var obj = dolstagis;
    while (parts.length > 0) {
      var part = parts.shift();
      obj = obj[part] = obj[part] || parts.length ? { } : target;
    }
  }
};

var console = window.console = window.console || {};
console.log = console.log || function () { };