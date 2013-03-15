// @reference jquery-1.8.3.js
// @reference core.js
// @reference flash.js

(function ($, undefined) {
  "use strict";

  var duration = 500;

  $.fn.fadeDelete = function (callback) {
    var self = this;
    return this.addClass('fadeout')
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