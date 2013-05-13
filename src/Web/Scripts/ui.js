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

  $(function () {
    $('table').on('change', 'input.select-all:checkbox', function () {
      var checked = this.checked;
      $(this).parents('table').find('input:checkbox').each(function () {
        this.checked = checked;
      });
    });

    $('.colorbox').colorbox({ iframe: true, width: "640px", height: "480px", initialWidth: "400px", initialHeight: "300px" });

  });
})(jQuery);