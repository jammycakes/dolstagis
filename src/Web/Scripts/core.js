// @reference jquery-1.8.3.js
// @reference jquery.colorbox.js

(function () {
  "use strict";

  var console = window.console = window.console || {};
  console.log = console.log || function () { };

  var dolstagis = window.dolstagis = {
    define: function define(ns, target) {
      var parts = ns.split('.');
      var obj = dolstagis;
      while (parts.length > 0) {
        var part = parts.shift();
        obj = obj[part] = obj[part] || parts.length ? {} : target;
      }
    }
  };
}).call(this);