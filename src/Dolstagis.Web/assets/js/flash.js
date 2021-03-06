﻿// @reference vendor/jquery-2.0.3.js
// @reference vendor/namespace.js

namespace("dolstagis.ui", [jQuery], function ($, undefined) {
    "use strict";

    var flash = this.flash = function(message, level) {
        var container = document.getElementById('flash');
        if (!container) {
            container = document.createElement('ul');
            container.id = 'flash';
            var anchor = document.getElementsByTagName('header')[0];
            anchor.appendChild(container);
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
    };


    $(function () {
        $(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
            flash('Sorry, an error occurred when communicating with the web server.', 'error');
        });

        $(document).on('mouseover', '#flash li', function () {
            var $link = $(this).find('.close');
            if ($link.length == 0)
                $(this).prepend('<a href="#" class="close" title="Dismiss this message">Close</a>');
        });

        $(document).on('click', '#flash li a.close', function (e) {
            e.preventDefault();
            var $li = $(this).parents('li').first();
            var $ul = $li.parent();
            var $toFade = $ul.children().length > 1 ? $li : $ul;
            $toFade.fadeDelete();
        });
    });
});
