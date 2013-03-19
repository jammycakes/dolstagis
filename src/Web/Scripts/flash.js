// @reference jquery-1.8.3.js
// @reference core.js

(function ($, undefined) {

  dolstagis.define('ui.flash', function (message, level) {
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
  });


  $(function () {
    $(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
      dolstagis.ui.flash('Sorry, an error occurred when communicating with the web server.', 'error');
    });
  });

})(jQuery);