// @reference ../vendor/jquery-2.0.3.js
// @reference ../core.js
// @reference ../ui.js

(function ($, undefined) {
  "use strict";

  function allocateInvitations(e) {
    var ids = $('input[name="select"]:checked')
      .map(function (ix, el) { return $(el).val(); })
      .toArray();
    var count = parseInt($('#invitations').val(), 10);

    $.post(
      '/admin/ajax/AllocateInvitations/1',
      { ids: ids, count: count },
      function (data) {
        console.log(data);
      }
    );




    e.preventDefault();
  }

  $(function () {
    $('#ctlAllocateInvitations').on('click', allocateInvitations);
  });
})(jQuery);