// @reference ../effects.js
// @reference ../jquery-1.8.3.js

(function ($, undefined) {
  "use strict";

  $(function () {

    $('#user-sessions').on('click', '.delete', function (e) {
      var self = this;
      var sessionID = $(this).data('sessionid');
      $.ajax({
        type: 'DELETE',
        url: '/user/deletesession',
        data: { id: sessionID },
        success: function (data, textStatus, jqXHR) {
          $(self).parents('tr').first().fadeDelete();
        }
      });
      e.preventDefault();
    });

  });
})(jQuery);