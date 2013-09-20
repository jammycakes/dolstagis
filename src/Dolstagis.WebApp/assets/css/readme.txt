CSS stylesheet folder layout
============================

All stylesheets in this folder are compiled using Less CSS.

Only stylesheets in the top level folder are loaded directly into the page.
Stylesheets in subfolders are loaded in to the main stylesheet using an @import
directive. This way, we can keep our stylesheets modular.

You should have at most one stylesheet for each layout page. The top level
stylesheet should have a name that mirrors the name of the layout page itself
(e.g. _Layout.cshtml -> layout.less) and any imports into that stylesheet should
be placed in a subfolder with a matching name, e.g. layout.

Mixins, constants and other Less declarations that do not directly translate to
markup should be placed in the "core" folder.