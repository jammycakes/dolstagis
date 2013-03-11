// @reference ../effects.js
// @reference ../jquery-1.8.3.js

(function ($, undefined) {
  "use strict";

  $(function () {
    $('#user-sessions').on('click', '.delete', function (e) {
      $(this).parents('tr').first().fadeDelete();
      e.preventDefault();
    });
  });

})(jQuery);