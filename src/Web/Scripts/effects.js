// @reference jquery-1.8.3.js
// @reference core.js

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

  dolstagis.ui = {
    flash: function (message, level) {

      var container = document.getElementById('flash');
      if (!container) {
        container = document.createElement('ul');
        container.id = 'flash';
        var anchor = document.getElementById('topbanner');
        anchor.parentNode.insertBefore(container, anchor.nextSibling)
      }

      var $anchor = $(container).children('.' + level).last();
      var newElement = document.createElement('li');
      newElement.className = level;
      newElement.style.display = "none";
      newElement.innerHTML = message;
      if ($anchor.length) {
        $anchor.after(newElement);
      }
      else {
        container.appendChild(newElement);
      }
      $(newElement).slideDown();
    }
  };

})(jQuery);