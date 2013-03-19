// @reference ../ui.js
// @reference ../jquery-1.8.3.js

(function ($, undefined) {
  "use strict";

  $(function () {

    $('#user-sessions').on('click', '.delete', function (e) {
      var self = this;
      var sessionID = $(this).data('sessionid');
      $.ajax({
        type: 'DELETE',
        url: '/user/ajax/session?id=' + encodeURIComponent(sessionID),
        success: function (data, textStatus, jqXHR) {
          $(self).parents('tr').first().fadeDelete();
        }
      });
      e.preventDefault();
    });

  });
})(jQuery);