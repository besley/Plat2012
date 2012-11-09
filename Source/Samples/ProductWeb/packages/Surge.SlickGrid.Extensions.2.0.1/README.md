About
=====
The Surge SlickGrid Extensions provided by the Surge Consulting Group is a set of open-sourced tools that will make using grids in your application a breeze. The product can be conceptually broken down into the following:

* A jQuery widget which wraps the open-source SlickGrid tool and gives an api for working with it that is more natural to a jQuery user. This includes the ability to define grid columns in mark-up.
* Various extensions that add some nice bits of functionality to the widget. Including:
  * Use jQuery Templates for Headers, Footers, and formatting columns
  * JSONDataSource object to easily support bottomless scrolling
  * Commonly referenced "Special Columns" and formatter aliases
* A standard set of editors and formatters.
* Utility code in the Surge namespace that is used in implementing the above.

Installation
============
As Surge.SlickGrid Extensions is entirely a client-side library just reference the provided javascript and css files as you normally would. [See article.](http://blog.surgeforward.com/node/22)

Dependencies
============
Surge SlickGrid Extensions currently has the following dependencies:

* **Required** - Surge SlickGrid will not run without the following
  * [jQuery](http://jquery.com) (not included)
  * [jQuery UI widget factory](http://jqueryui.com/download) (not included)
  * SlickGrid v1.4.3  (included slick.grid.css, slick.grid.js) - SlickGrid v2.0 support is experimental
  * Surge.Core (included surge.core.js)
  * Surge.SlickGrid (included surge.slickGrid.css, surge.slickGrid.js)
* **Optional** - Certain features of Surge SlickGrid will not work without the following
  * [jQuery.event.drag 2.0](http://threedubmedia.com/code/event/drag)
  * [jQuery Templates](http://api.jquery.com/category/plugins/templates/)
  * [jQuery Globalize](http://wiki.jqueryui.com/w/page/39118647/Globalize)
  * [jQuery TimeEntry](http://keith-wood.name/timeEntry.html)

Always check the browser console log for errors that might occur due to missing dependencies.

References
==========
 * [Surge Consulting Group](http://www.surgeforward.com/)
 * [Introductory Tutorial Index](https://github.com/surgeforward/Surge-SlickGrid-Extensions/wiki/Announcing-Surge-Slickgrid-Extensions)
 * [SlickGrid Extensions Demo](http://platformdemo.surgeforward.com/Grid/)
 * [Surge API Reference](http://docs.surgeforward.com/Javascript/files/surge-slickGrid-js.html)
 * [Original SlickGrid Documentation](http://github.com/mleibman/SlickGrid/wiki)