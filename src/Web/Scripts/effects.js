// @reference jquery-1.8.3.js

(function ($, undefined) {
  "use strict";

  var fadeStart = '#ff9933';
  var duration = 500;

  $.fn.fadeDelete = function (callback) {
    var self = this;
    return this.css('background-color', fadeStart)
      .animate({
        opacity: 0
      }, duration, function () {
        self.remove();
        if (typeof(callback) === 'function') {
          callback();
        }
      });
  };

})(jQuery);