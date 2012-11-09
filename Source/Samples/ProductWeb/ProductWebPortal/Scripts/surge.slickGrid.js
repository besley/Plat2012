// Title: surge.slickGrid.js

(function ($) {
    // #region Usage Docs

    // Class: surge.slickGrid
    // Wraps the SlickGrid component in a jQuery UI widget interface. Provides additional support
    // for features like header and footer templates and declarative columns.
    //
    // Example:
    // (code)
    // <div class="my-grid" data-widget="slickGrid" data-options="forceFitColumns: true">
    //   <table>
    //     <tr>
    //       <th data-id="Id">Identifier</th>
    //       <th data-id="Name">Name</th>
    //     </tr>
    //   </table>
    // </div>
    //
    // <script type="text/javascript">
    //   $.viewReady(function() {
    //     $(".my-grid", this).slickGrid("data", [
    //       { Id: 1, Name: "Foo" },
    //       { Id: 2, Name: "Bar" }
    //     ]);
    //   });
    // </script>
    // (end)
    //
    // Declarative Columns:
    // The slickGrid widget supports markup-based declaration of grid columns in lieu of JS-based configuration.
    // The above example uses the most basic possible configuration - table headers with a data-id attribute.
    // The data-id attribute, by default, specifies both the internal ID of the column used by SlickGrid and the
    // field on the supplied data array which the value will be pulled from. Other available options include:
    //
    //	data-formatter - A JavaScript expression which should evaluate to a function. This function will be used to format the model value for display.
    //	data-editor - The editor to use when inline editing is enabled for this column.
    //	data-field - The model field which should be used instead of data-id to populate the value of this column.
    //	data-width - The width of the column in pixels.
    //	data-special - The name of a special column type. This is just a simple facade for common types of ui and overrides other options. See <$.surge.slickGrid.specialColumns>.
    //	data-sortable - True or false. Enables sorting for the column. Note that SlickGrid does not actually implement a sorting algorithm, this will only adjust styling and trigger the 'gridsort' event when the column header is clicked.
    //  data-onCellChange - (default undefined) The name of an event handler function to call when the cell's value changes. The function receives a single argument containing the same data as the 'ui' object for the gridcellchange event.
    //  data-onCellClick - (default undefined) The name of an event handler function to call when the cell is clicked. The function receives a single argument containing the same data as the 'ui' object for the gridcellclick event.
    //  data-template - (default undefined) A jQuery selector for a template. If present, this template will be used to format cell contents for the column. A template is just a special case of a formatter and so its presence of a template overrides the precense of a formatter. In addition, it stands to notice that SlickGrid does not size rows automatically, you might have to use the rowHeight option if your template forces the row to be higher than a single line.
    //
    // Options:
    //     rowHeight                - (default 25px) Row height in pixels.
    //     enableAddRow             - (default false) If true, a blank row will be displayed at the bottom - typing values in that row will add a new one.
    //     leaveSpaceForNewRows     - (default false)
    //     editable                 - (default false) If false, no cells will be switched into edit mode.
    //     autoEdit                 - (default true) Cell will not automatically go into edit mode when selected.
    //     enableCellNavigation     - (default true) If false, no cells will be selectable.
    //     enableCellRangeSelection - (default false) If true, user will be able to select a cell range.  onCellRangeSelected event will be fired.
    //     defaultColumnWidth       - (default 80px) Default column width in pixels (if columns[cell].width is not specified).
    //     enableColumnReorder      - (default true) Allows the user to reorder columns.
    //     asyncEditorLoading       - (default false) Makes cell editors load asynchronously after a small delay.
    //                                This greatly increases keyboard navigation speed.
    //     asyncEditorLoadDelay     - (default 100msec) Delay after which cell editor is loaded. Ignored unless asyncEditorLoading is true.
    //     forceFitColumns          - (default false) Force column sizes to fit into the viewport (avoid horizontal scrolling).
    //     enableAsyncPostRender    - (default false) If true, async post rendering will occur and asyncPostRender delegates on columns will be called.
    //     asyncPostRenderDelay     - (default 60msec) Delay after which async post renderer delegate is called.
    //     autoHeight               - (default false) If true, vertically resizes to fit all rows.
    //     editorLock               - (default Slick.GlobalEditorLock) A Slick.EditorLock instance to use for controlling concurrent data edits.
    //     showSecondaryHeaderRow   - (default false) If true, an extra blank (to be populated externally) row will be displayed just below the header columns.
    //     secondaryHeaderRowHeight - (default 25px) The height of the secondary header row.
    //     syncColumnCellResize     - (default false) Synchronously resize column cells when column headers are resized
    //     rowCssClasses            - (default null) A function which (given a row's data item as an argument) returns a space-delimited string of CSS classes that will be applied to the slick-row element. Note that this should be fast, as it is called every time a row is displayed.
    //     cellHighlightCssClass    - (default "highlighted") A CSS class to apply to cells highlighted via setHighlightedCells().
    //     cellFlashingCssClass     - (default "flashing") A CSS class to apply to flashing cells (flashCell()).
    //     formatterFactory         - (default null) A factory object responsible to creating a formatter for a given cell.
    //                                Must implement getFormatter(column).
    //     editorFactory            - (default null) A factory object responsible to creating an editor for a given cell.
    //                                Must implement getEditor(column).
    //     multiSelect              - (default false) Enable multiple row selection.
    //     columns                  - An array of column definitions. Generally, in platform applications, the declarative column syntax should be used instead.
    //     data                     - The data source for the grid. This should be an array of rows that matches the format specified by your column definitions.
    //     noSearch                 - (default false) Prevents this column from being included in gridtextsearch if set to true.
    //	   headerTmpl				- jQuery selector for jQuery Template that will produce the grid header. Requires <surge.jQuery.templates>
    //	   footerTmpl				- jQuery selector for jQuery Template that will produce the grid footer. Requires <surge.jQuery.templates>
    //     escapeHtml               - (default true) Escape html when no formatter is provided
    //
    // Group: Editing and Formatting
    // Cell editing and formatting.
    //
    // The grid supports a spreadsheet-like editing mode as well as custom formatting for cell values through the
    // column definitions' editor and formatter properties. To use this functionality, simply set the data-editor
    // or data-formatter attribute on a column definition to the name of an editor or formatter, respectively. To
    // use edit mode, you must also enable the editable, and optionally, enableAddRow options on the grid itself.
    //
    // Some formatters and editors support their own configuration options. See the specific documentation for those
    // classes for details.
    //
    // See Also: https://github.com/mleibman/SlickGrid/wiki/Writing-custom-cell-editors
    //
    // Example:
    // (code)
    //<div class="editors-grid" data-widget="slickGrid" data-options="forceFitColumns: true, editable: true, enableAddRow: true">
    //	<table>
    //		<tr>
    //			<th data-id="Name" data-editor="TextCellEditor">
    //				Name
    //			</th>
    //			<th data-id="Birthdate" data-editor="Surge.SlickGrid.DateEditor" data-formatter="Surge.SlickGrid.DateFormatter" data-onCellChange="SomeHandler">
    //				Birthdate
    //			</th>
    //			<th data-id="TakesLunchAt" data-editor="Surge.SlickGrid.TimeEditor" data-formatter="Surge.SlickGrid.TimeFormatter">
    //				Takes Lunch At
    //			</th>
    //			<th data-id="StartWork,EndWork" data-editor="Surge.SlickGrid.TimeRangeEditor" data-formatter="Surge.SlickGrid.TimeRangeFormatter">
    //				Workday
    //			</th>
    //		</tr>
    //	</table>
    //</div>
    // (end)
    //
    // Visibility and Resizing:
    //
    // SlickGrid relies on layout calculations that depend on its size, and thus when resizing
    // or showing a previously hidden grid, you must update the grid's dimensions using SlickGrid's
    // .resizeCanvas() and .autosizeColumns() methods.
    //
    // (code)
    // <div style="display: none">
    //   <div class=my-grid data-widget=slickGrid>
    //     <table>
    //       <tr>
    //         <th data-id=Name>Name</th>
    //         <th data-id=DateOfBirth>DOB</th>
    //         <th data-id=NumberOfZombiesSlain>Zombies</th>
    //     </table>
    //   </div>
    // </div>
    //
    // <script>
    //   $.viewReady(function() {
    //     $('.my-grid')
    //			.parents(':hidden')
    //				.fadeIn('slow')
    //			.end()
    //		.slickGrid('resize');
    //   });
    // </script>
    // (end)
    //
    // Group: Events
    //
    // Event: gridselectionchange
    // Fired when the grid selection changes.
    // Example:
    //
    // (based on the summary example)
    // (code)
    // $.viewReady(function() {
    //   $(".my-grid", this).bind("gridselectionchange", function () {
    //     _log("Row # " + $(this).slickGrid("selection") + " was selected.");
    //   });
    // });
    // (end)
    //
    // Event: gridcellchange
    // Fired when an editable grid cell is modified.
    //
    // Parameters:
    //  e - The jQuery event object. Always null for this event.
    //  ui - An object with two properties, row and cell, specifying the indexes of the row and cell that changed.
    //
    // Event: gridaddnewrow
    // Fired when a new row is about to be added to an editable grid. This event allows for preprocessing
    // of the new data row before it is added to the grid.
    //
    // Parameters:
    //  e - The jQuery event object. Always null for this event.
    //  ui - Am object with two properties: item and columnDefinition. The item property contains the newly created
    //       data row which will be inserted into the grid's data array. An event handler may set the ui.item
    //       property to replace the created item with a different row object. The columnDefinition property contains
    //       the column metadata for the cell in the creation row which the user edited.
    //
    // Example:
    // (code)
    // // based on summary example, assumes enableAddRow option has been turned on
    // $(".my-grid", this).bind("gridaddnewrow", function (e, ui) {
    //   // add some extra non-visible data to the new row.
    //   ui.item = $.extend({}, { hiddenField: 123 }, ui.item); 
    // });
    // (end)
    //
    // Event: gridbeforecelledit
    // Fired before a cell enters edit mode. The handler may cancel the edit.
    //
    // Parameters:
    //  e - The jQuery event object. Always null for this event.
    //  ui - An object with four properties: row and cell give the row and cell indices for the cell to be edited;
    //       data gives the data for the row to be edited, and cancel is a flag which may be set to true by the
    //       handler to cancel the edit.
    //
    // Event: gridrowmove
    // Fired when a row is moved using the special column "reorderColumn".
    //
    // Parameters:
    //  e - The jQuery event object.
    //  ui - An object with two properties: insertBefore and rows. The insertBefore property contains the index of the row
    //       after the moved row.
    //       The rows object is an array with the ID's of the moved rows. At this time only 1 row can be moved at a time, so this
    //       property will contain one value, the ID of the moved row.
    //
    // Example:
    // (code)
    // // requires underscore jQuery library.
    // $(".my-grid", this).bind("gridrowmove", function (e, ui) {
    //      // Sets the Sequence property of the data elements
    //      _($(".my-grid", this).slickGrid("data")).each(function(e, i){ e.Sequence = i; });
    // });
    // (end)
    //
    // Event: gridcellclick
    // Fired when a cell is clicked.
    //
    // Parameters:
    //  e - The jQuery event object.
    //  ui - An object with two properties: cell and row. The "cell" property is the column index of the row clicked. The "row" property
    //       is the index of the row of the cell that was clicked.
    //
    // Example:
    // (code)
    // // requires underscore jQuery library.
    // $(".my-grid", this).bind("gridcellchange", function (event, ui) {
    //      var row = ui.row;
    //      var cell = ui.cell;
    //      var columns = $(".my-grid", this).slickGrid("grid").getColumns();
    //      var fieldName = columns[cell].field;
    //      var data = $(".my-grid", this).slickGrid("data");
    //      alert(data[row][fieldName]);
    // });
    // (end)
    //
    // Event: griddataupdate
    // This event is triggered anytime .data is called with new data. This includes any time the list is refiltered.
    //
    // Parameters:
    //  e - The jQuery event object.
    //
    // Example:
    // (code)
    // // When ever the data is changed, make an element show "5 items" or "2 of 5 items" depending upon if items are filtered out or not.
    // $(".my-grid", this).bind("griddataupdate", function() {
    //  var filteredCount = pickList.slickGrid("rowCount", true);
    //  var count = pickList.slickGrid("rowCount");
    //  $(".my-grid-count", this).text(filteredCount == count ? (count + " items") : [filteredCount, "of", count, "items"].join(" "));
    // });
    // (end)
    //
    // Event: gridtextsearch
    // This event is triggered by the application, not by the platform, to cause text based searching across the grid. All defined columns
    // are included that contain numerical or string based data, and do not set their noSearch option to true.
    //
    // Parameters:
    //  e - The jQuery event object.
    //  searchText - The text that is being searched.
    //
    // Example:
    // (code)
    // // This code is executing inside of the header, so triggering gridtextsearch on the textbox will cause it to bubble to the grid control. 
    // $(".search", this).textboxHelperText("Search").bind("textchange", function() {
    //  $(this).trigger("gridtextsearch", $(this).val());
    // });
    // (end)

    // #endregion Usage Docs

    // #region Local Utilities

    if (!window.Surge) {
        return console.error("Surge SlickGrid requires Surge.Core to be loaded, but was not found");
    }

    var Define = Surge.Define,
		Class = Surge.Class,
		Console = Surge.Console,
		__bind = function (fn, me) { return function () { return fn.apply(me, arguments); }; },
		applyFormat = $.format ? __bind($.format, $) : (window.Globalize && __bind(Globalize.format, Globalize)) ||
			(function () { Console.error("You are trying to do something that requires the jQuery Globalize Plugin which is not found"); }),
		makeTemplate = function (template, options) {
		    if (!$.fn.tmpl) {
		        Console.error("You are trying to do something which requires the jQuery Templates Plugin which is not found")
		        return $(); //prevent further errors by returning an empty matching set
		    }
		    return $(template).tmpl(options);
		},
		utcToLocalTime = Surge && Surge.Date && Surge.Date.utcToLocalTime || (function (date) {
		    if (date == null) return date;
		    var result = new Date(date);
		    result.setMinutes(result.getMinutes() + result.getTimezoneOffset());
		    return result;
		})

    if (!$.widget && Console && Console.error) {
        return Console.error("Surge SlickGrid Extensions interact with the jQuery Ui Widget system yet a reference to it was not found.");
    }
    // #endregion Local Utilities

    // #region SlickGrid Widget

    // Group: slickGrid jQuery Ui Widget
    // jQuery Ui Widget wrapping the SlickGrid tool.
    //
    //(code)
    //  var grid = $('div.the-grid').slickGrid(options),	// initializes widget
    //		underlyingGrid = grid.slickGrid('grid');		// invokes the 'grid' method to get the current grid
    //  grid.data('data', newDataObject);					// sets the widget instance's 'data' property (the data in the SlickGrid)
    //(end)
    // 
    // Depends on:
    // - <jQuery at http://jquery.com>
    // - <jQuery Ui Widget Factory at http://wiki.jqueryui.com/w/page/12138135/Widget%20factory>
    // - <SlickGrid at http://github.com/mleibman/SlickGrid/wiki>
    $.widget("surge.slickGrid", {
        widgetEventPrefix: "grid",
        options: { data: [], columns: [], multiSelect: false, escapeHtml: true },
        _create: function () {
            var self = this,
				headerRow = this.element.children("table").find("tr:eq(1)"),
				appendTemplate = function (templateSelector, destination, data, wrapInClass) {
				    var wrappedDestination = $('<div>').addClass(wrapInClass).appendTo(destination);
				    $.fn.tmplTo ? $(templateSelector).tmplTo($(wrappedDestination), data)
							: makeTemplate(templateSelector, data).appendTo($(wrappedDestination));
				}
            this.element.addClass("sfx-grid-wrapper");
            this.buildColumns(this.element.children("table").find("tr:first-child th"));
            this.element.empty();
            this.filters = {};
            this._filteredData = [];

            this.options.headerTmpl && appendTemplate(this.options.headerTmpl, this.element, this.options, 'sfx-grid-header');

            this._gridElement = $("<div class='sfx-grid'></div>").appendTo(this.element);

            this.options.footerTmpl && appendTemplate(this.options.footerTmpl, this.element, this.options, 'sfx-grid-footer');

            this._grid = new Slick.Grid(this._gridElement, this.options.data, this.options.columns, this.options);

            if (this.options.autoHeight)
                this._gridElement.css("height", "auto");

            if (headerRow.length)
                this.buildHeaderRow(headerRow);

            // Compatibility
            if (Slick.Event && !this._grid.getHeaderRow) {
                _log("warning: Slick.Event is present, but SlickGrid 2.0 is not loaded. Assuming SlickGrid 1.x behavior.");
                self._slickGridV2 = false;
            } else if (Slick.Event) {
                self._slickGridV2 = true;
            }

            if (this._grid.getCurrentCell)
                this._grid.getActiveCell = this._grid.getCurrentCell;

            function mapV2Event(name, fn) {
                var eventMap = {
                    onAddNewRow: function (e, args) { return [args.item, args.column, args]; },
                    onMoveRows: function (e, args) { return [args.rows, args.insertBefore, args]; },
                    onClick: function (e, args) { return [e, args.row, args.cell, args]; },
                    onCellChange: function (e, args) { return [args.row, args.cell, args.item, args]; },
                    onSelectedRowsChanged: function (e, args) { return [args]; },
                    onBeforeEditCell: function (e, args) { return [args.row, args.cell, args.item, args]; },
                    onSort: function (e, args) { return [args.sortCol, args.sortAsc, args]; }
                };

                return function (e, args) {
                    if (eventMap[name])
                        return fn.apply(this, eventMap[name](e, args));

                    return fn.apply(this, arguments);
                };
            };

            function subscribe(name, fn) {
                if (!Slick.Event || !self._slickGridV2) // old style SlickGrid 1.x events
                    self._grid[name] = fn;
                else if (!self._grid[name])
                    _error("grid event " + name + " is missing");
                else // SlickGrid 2.0 events
                    self._grid[name].subscribe(mapV2Event(name, fn));
            };

            subscribe('onAddNewRow', function (item, colDef) {
                var eventArgs = { item: item, columnDefinition: colDef };
                self._trigger("addnewrow", null, eventArgs);

                if (eventArgs.item !== item)
                    item = eventArgs.item;

                var data = this.getData();
                data.push(item);

                this.updateRowCount();
                this.updateRow(data.length - 1);
                this.render();
            });

            // TODO: Make this more extensible and robust.
            // In particular, we should not be constructing a brand new data array
            // and need to expose some hooks so that JSONDataSource can do something
            // appropriate (like tracking what offsets must be applied to incremental load results).

            if (self._slickGridV2 && Slick.RowMoveManager) {
                self._rowMoveManager = new Slick.RowMoveManager();
                self._grid.registerPlugin(self._rowMoveManager);
                self._grid.onMoveRows = self._rowMoveManager.onMoveRows;
            }

            if (self._slickGridV2 && Slick.RowSelectionModel)
                self._grid.setSelectionModel(new Slick.RowSelectionModel());

            if (!self._slickGridV2 || self._grid.onMoveRows) {
                subscribe('onMoveRows', function (rows, insertBefore) {
                    var extractedRows = [], left, right;
                    var data = self._grid.getData();
                    left = data.slice(0, insertBefore);
                    right = data.slice(insertBefore, data.length);

                    for (var i = 0; i < rows.length; i++) {
                        extractedRows.push(data[rows[i]]);
                    }

                    rows.sort().reverse();

                    for (var i = 0; i < rows.length; i++) {
                        var row = rows[i];
                        if (row < insertBefore)
                            left.splice(row, 1);
                        else
                            right.splice(row - insertBefore, 1);
                    }

                    data = left.concat(extractedRows.concat(right));

                    var selectedRows = [];
                    for (var i = 0; i < rows.length; i++)
                        selectedRows.push(left.length + i);

                    self._grid.setData(data);
                    self._grid.setSelectedRows(selectedRows);
                    self._grid.render();
                    self._trigger("rowmove", null, { rows: rows, insertBefore: insertBefore });
                });
            }

            subscribe('onClick', function (e, row, cell) {
                var column = self.options.columns[cell];

                if (column && column.onCellClick)
                    column.onCellClick.call(self.element, {
                        row: row, cell: cell, column: column
                    });
                self._trigger("cellclick", e, { row: row, cell: cell, column: column });
            });

            subscribe('onDblClick', function (e, row, cell) {
                var column = self.options.columns[cell];

                if (column && column.onDblClick)
                    column.onDblClick.call(self.element, {
                        row: row, cell: cell, column: column
                    });
                self._trigger("celldoubleclick", e, { row: row, cell: cell, column: column });
            });

            subscribe('onCellChange', function (row, cell, item) {
                var column = self.options.columns[cell];

                if (column && column.onCellChange)
                    column.onCellChange.call(self.element, {
                        row: row,
                        cell: cell,
                        item: item,
                        column: column,
                        value: item[column.field]
                    });

                self._trigger("cellchange", null, {
                    row: row,
                    cell: cell,
                    item: item,
                    column: self.options.columns[cell]
                });
            });

            subscribe('onSelectedRowsChanged', function () {
                if (self.element.triggerHandler("gridbeforeselectionchange") === false)
                    return;

                self._trigger("selectionchange");
            });

            subscribe('onBeforeEditCell', function (row, cell, data) {
                var eventArgs = { row: row, cell: cell, data: data, cancel: false };
                self._trigger("beforecelledit", null, eventArgs);

                return !(eventArgs.cancel === true);
            });

            //ON SORT
            subscribe('onSort', function (col, isAsc) {
                // Check for a bound event and fire that if it exists.
                if ($(self.element).data('events').gridsort) {
                    var eventArgs = { sortCol: col, sortAsc: isAsc };
                    self._trigger("sort", null, eventArgs);
                    return !(eventArgs.cancel == true);
                }

                // No events - use the below default sort.
                var sortcol = col.field;
                var sortdir = isAsc ? 1 : -1;
                function comparer(a, b) {
                    var x = a[sortcol], y = b[sortcol];
                    return sortdir * (x == y ? 0 : (x > y ? 1 : -1));
                }

                var data = this.getData();
                data.sort(comparer);
                this.invalidate();
                return true;
            });

            if ($.surge.slickGrid.findIn && $.surge.slickGrid.findIn.call) {
                this.element.bind("gridtextsearch.platform", function (e, searchText) {
                    var data = self._grid.getData();
                    var columns = self._grid.getColumns();
                    var matches = $.surge.slickGrid.findIn(data, searchText, columns);
                    self.data(matches);
                });
            }
        },

        destroy: function () {
            $.Widget.prototype.destroy.apply(this, arguments); // default destroy

            if (this._grid)
                this._grid.destroy();
        },

        _getFormatter: function (name) {
            if ($.surge.slickGrid.formatters[name])
                return $.surge.slickGrid.formatters[name];

            return eval(name);
        },

        buildColumns: function (thList) {
            var self = this, columns = [],
								evalData = function ($element, key) {
								    return eval($element.data(key) || $element.data(key.toLowerCase()))
								}
            thList.each(function () {
                var th = $(this);
                var formatter = th.data("formatter") ? self._getFormatter(th.data("formatter")) : undefined;
                var editor = evalData(th, "editor");
                var options = evalData(th, "options");
                //var onvaluechange = th.data("onvaluechange") ? eval(th.data("onvaluechange")) : null;

                var template = $(th.data("template"));

                if (template.length)
                    formatter = function (row, cell, value, columnDef, dataContext) {
                        return $("<div>").append(makeTemplate(template, {
                            row: row,
                            cell: cell,
                            value: value,
                            columnDef: columnDef,
                            dataContext: dataContext
                        })).html();
                    };

                if (!formatter && $.surge.slickGrid.escapeHtml && self.options.escapeHtml !== false)
                    formatter = function (row, cell, value) { return typeof value === "undefined" || value === null ? "" : $.surge.slickGrid.escapeHtml(value); };

                var col = { minWidth: 0 };

                if (th.data("special"))
                    $.extend(col, self.specialColumn(th.data("special"), th.data("function")));

                var html = th.html();

                $.extend(col, {
                    id: th.data("id"),
                    name: html && html.length ? $.trim(html) : undefined,
                    field: th.data("field") || th.data("id"),
                    formatter: col.formatter || formatter,
                    editor: editor,
                    width: th.data("width"),
                    originalWidth: th.data("width"),
                    minWidth: th.data("minWidth"),
                    maxWidth: th.data("maxWidth"),
                    cssClass: th.data("cssClass") || th.data("cssclass"),
                    sortable: th.data("sortable"),
                    behavior: th.data("behavior"),
                    toolTip: th.data("toolTip"),
                    unselectable: th.data("unselectable"),
                    options: options,
                    resizable: th.data("resizable"),
                    onvaluechange: th.data("onvaluechange"),
                    dynamicoptions: th.data("dynamicoptions"),
                    template: template,
                    onCellChange: evalData(th, "onCellChange"),
                    onCellClick: evalData(th, "onCellClick")
                });

                columns.push(col);
            });

            if (columns.length > 0)
                this.options.columns = columns;
        },

        buildHeaderRow: function (headerRow) {
            var self = this;

            if (!Slick.Event) {
                headerRow.remove();
                return;
            }

            var row = $(this._grid.getHeaderRow());
            var tds = $("> td", headerRow);

            for (var i = 0; i < this.options.columns.length; ++i) {
                var column = this.options.columns[i];
                var headerColumn = this._grid.getHeaderRowColumn(column.id);

                $(headerColumn).html($(tds[i]).html());
            }

            function resizeHeaderColumns() {
                for (var i = 0; i < self.options.columns.length; ++i) {
                    var column = self.options.columns[i];
                    var headerColumn = self._grid.getHeaderRowColumn(column.id);

                    $(headerColumn).width(column.width - (self.options.headerColumnInset || 0));
                }
            };

            self._grid.onColumnsResized.subscribe(function (e, args) {
                resizeHeaderColumns();
            });

            self._grid.onColumnsReordered.subscribe(function (e, args) {
                resizeHeaderColumns();
            });

            resizeHeaderColumns();
            Surge.initializeWidgets(row);
        },

        // Method: specialColumn
        // Return the column overrides expressed by the special column mechanism. See <$.surge.slickGrid.specialColumns>
        specialColumn: function (colType, colFunction) {
            var col = $.surge.slickGrid.specialColumns[colType];
            return col && col(colFunction);
        },

        // Method: grid
        // Fetch the widget's underlying grid object
        grid: function () { return this._grid; },

        // Method: resize
        // Recalculate the widget's size.
        resize: function () {
            if (this.options.forceFitColumns) {
                this._grid.resizeCanvas();

                for (var i = 0; i < this.options.columns.length; ++i) {
                    var col = this.options.columns[i];

                    if (col.originalWidth)
                        col.width = col.originalWidth;
                }

                this._grid.autosizeColumns();
            }

            this._grid.resizeCanvas();
        },

        // Property: data
        // Get or set the data the underlying grid is bound to.
        data: function (newData) {
            if (newData !== undefined) {
                this.options.data = newData;
                //this._filteredData = this.filterData(newData);
                this._grid.setData(newData);
                this._grid.updateRowCount();
                this._grid.render();
                this._trigger("datachange");

                this.element.trigger("griddataupdate");
                return this.element;
            }
            return this._grid.getData();
        },

        // Method: runFilters
        // Resets the grid data, ensuring any changed filters are applied.
        runFilters: function () {
            this.data(this.options.data);
        },

        // Method: selection
        // Get the currently selected rows as tracked by the underlying SlickGrid
        selection: function () {
            var rows = this._grid.getSelectedRows();

            if (!this.options.multiSelect)
                return rows[0];
            else
                return rows;
        },

        // Method: rowCount
        // Get the amount of rows currently in the grid
        rowCount: function (filtered) {
            return filtered ? (this._filteredData ? this._filteredData.length : 0) : (this.options.data ? this.options.data.length : 0);
        },

        _setOption: function (key, value) {
            switch (key) {
                case "columns":
                    this._grid.setColumns(value);
                    break;
                case "data":
                    this.data(value);
                    break;
                default:
                    var dict = {};
                    dict[key] = value;
                    this._grid.setOptions(dict);
                    break;
            }
        }
    });

    // #endregion SlickGrid Widget

    // #region JSONDataSource

    // Class: Surge.SlickGrid.JSONDataSource
    //
    // Provides incrementally loaded data from a JSON data service into a grid. This
    // allows extremely large data sets to be efficiently edited. To use the data
    // source, you must set the Url and optionally the QueryParameters property,
    // then call <bind> to associate the data source with your target grid.
    //
    // Example:
    // (code)
    // var dataSource = new Surge.SlickGrid.JSONDataSource();
    // dataSource.setUrl("/_surge/ReferenceCode/List");
    // dataSource.setQueryParameters({codeType: 4}); // retrieve states
    // dataSource.bind($(".my-grid"));
    // (end)
    //
    // JSON Format:
    // The data source expects the URL provided to accept certain query parameters
    // and return data in a specific format. The service should accept the following
    // as query parameters:
    //
    // from - The start of the range of data to return, inclusive.
    // to   - The end of the data range to return, exclusive.
    //
    // It should return JSON data in the following format.
    //
    // (code)
    // {
    //   TotalLength: 123, // total number of records in the set
    //   Data: [
    //     {property1: 1, property2: 2, ...}, ... // each data row
    //   ]
    // }
    // (end)
    //
    // The returned Data elements in the array will be directly inserted into the
    // grid's data collection at the appropriate location.
    //
    // Limitations:
    //
    //  * Incremental data loading may not currently be used in concert with row movement.
    //  * No cache eviction is performed, so once a row is loaded, it stays loaded forever.
    //  * SlickGrid viewport changes are not aggregated, so sometimes this class will make
    //    numerous requests for small amounts of data.
    //
    // Depends On:
    // - <surge.core>
    // - <jQuery at http://jquery.com>
    // - <SlickGrid at http://github.com/mleibman/SlickGrid/wiki>
    Surge.Class("Surge.SlickGrid.JSONDataSource", Surge.Component, {
        // Property: Url
        // A string specifying the URL from which to load data.
        Url: new Surge.Property(),
        // Property: Method
        // HttpMethod with which to make the request to the server
        Method: new Surge.Property(),
        // Property: IsJSON
        // Whether the query paramters should be sent in JSON format. 
        IsJSON: new Surge.Property(),
        // Property: QueryParameters
        // Optional. A dictionary of additional query parameters to be passed to the data service.
        QueryParameters: new Surge.Property({ "default": {} }),

        constructor: function (properties) {
            this.constructor.superclass.call(this, properties);
            this._data = { length: 0, initialized: false };
            this.setMethod('GET');
            this.setIsJSON(false);
        },

        // Method: bind
        // Binds the data source to a grid. Target should be a DOM element, jQuery object, or selector
        // that identifies an instance of the surge.slickGrid jQuery UI widget. This method will
        // not accept a raw Slick.Grid instance.
        bind: function (target, targetCtx) {
            var self = this;
            var grid = this._grid = $(target, targetCtx).slickGrid("grid");

            if (grid.onViewportChanged && grid.onViewportChanged.subscribe)
                grid.onViewportChanged.subscribe(__bind(this._onViewportChanged, this));
            else
                grid.onViewportChanged = __bind(this._onViewportChanged, this);

            this._onViewportChanged();
            this._data.initialized = true;

            $(target, targetCtx).slickGrid("data", this._data);
        },

        _onViewportChanged: function () {
            var rowRange = this._grid.getViewport(),
				data = this._data,
				min, max;

            for (var i = rowRange.top; i < rowRange.bottom; ++i) {
                if (data[i] === undefined) {
                    if (min === undefined)
                        min = i;

                    max = i;
                }
            }
            if (min !== undefined && (max < data.length || data.initialized === false)) {
                this.loadData(min, max + 1);
            }
        },

        // Method: refreshData
        // Refresh the current view of the data from the server. Useful if one of the properties has changed.
        //
        // (code)
        // $('input[name=search]').keyup(function () {
        //	dataSource.setQueryParameters({ search: this.value });
        //	dataSource.refreshData();
        // });
        // (end)
        refreshData: function () {
            vp = this._grid.getViewport();
            this.loadData(vp.top, vp.bottom);
        },

        // Method: loadData
        // Load data a range of from the server
        //
        // Parameters
        // - from
        // - to
        loadData: function (from, to) {
            var query = $.extend({}, this.getQueryParameters(), { from: from, to: to }),
				self = this, data = this._data;

            $.ajax({
                url: this.getUrl(),
                dataType: 'json',
                data: this.getIsJSON() ? JSON.stringify(query) : query,
                type: this.getMethod(),
                contentType: this.getIsJSON() ? "application/json; charset=utf-8" : "application/x-www-form-urlencoded",
                success: function (json) {
                    data.length = json.TotalLength;
                    for (var i = 0; i < json.Data.length; ++i) {
                        data[from + i] = json.Data[i];
                        if (self._grid.invalidateRow) {
                            self._grid.invalidateRow(from + i);
                        }
                        else {
                            self._grid.removeRow(from + i);
                        }
                    }

                    self._grid.updateRowCount();
                    self._grid.render();
                }
            });
        }
    });

    // #endregion JSONDataSource

    // #region Standard set of Formaters provided by Surge

    // Namespace: Surge.SlickGrid
    Surge.Define("Surge.SlickGrid", {
        // Function: HiddenFormatter
        // Formatter which returns nothing.
        HiddenFormatter: function (row, cell, value, columnDef, dataContext) {
            return '';
        },

        // Function: TimeRangeFormatter
        // Formatter function which presents time ranges in a grid. This formatter
        // expects two fields, both integers specifying milliseconds since midnight,
        // which form a range of time. The two fields are specified by setting the
        // column's field to a comma-separated string, e.g. data-field="MyTimeStart,MyTimeEnd".
        // The string must not contain spaces.
        //
        // Depends On:
        // - <jQuery.globalize at http://wiki.jqueryui.com/w/page/39118647/Globalize>
        TimeRangeFormatter: function (row, cell, value, columnDef, dataContext) {
            var fields = columnDef.field.split(",");
            var zeroTime = new Date(utcToLocalTime(new Date(0)));

            if (dataContext[fields[0]] == undefined && dataContext[fields[1]] == undefined)
                return "";

            var startTime = dataContext[fields[0]] != null ?
				applyFormat(utcToLocalTime(new Date(dataContext[fields[0]])), "t") : "???";
            var endTime = dataContext[fields[1]] != null ?
				applyFormat(utcToLocalTime(new Date(dataContext[fields[1]])), "t") : "???";

            return startTime + " - " + endTime;
        },

        // Function: TimeFormatter
        // Formatter function which presents time values in a grid. Time is stored
        // as an integer in milliseconds since midnight.
        //
        // Depends On:
        // - <jQuery.globalize at http://wiki.jqueryui.com/w/page/39118647/Globalize>
        TimeFormatter: function (row, cell, value, columnDef, dataContext) {
            return value !== undefined ? applyFormat(utcToLocalTime(value), "t") : "";
        },

        // Function: DurationFormatter
        // Formatter function which presents duration values in a grid.  Time is stored
        // as an integer in milliseconds since midnight.
        //
        // Depends On:
        // - surge.datetime.js
        DurationFormatter: function (row, cell, value, columnDef, dataContext) {
            return value !== undefined ? applyFormat($.formatDuration(value), "hms") : "";
        },

        // Function: DateFormatter
        // Grid formatter that will format the value in an appropriate date format.
        //
        // Depends On:
        // - <jQuery.globalize at http://wiki.jqueryui.com/w/page/39118647/Globalize>
        DateFormatter: function (row, cell, value, columnDef, dataContext) {
            return value ? applyFormat(value instanceof Date ? value : new Date(value), "d") : "";
        },

        // Function: DateFormatter
        // Grid formatter that will format the value in an appropriate long-form (written out) date format.
        //
        // Depends On:
        // - <jQuery.globalize at http://wiki.jqueryui.com/w/page/39118647/Globalize>
        LongDateFormatter: function (row, cell, value, columnDef, dataContext) {
            return value ? applyFormat(value instanceof Date ? value : new Date(value), "MMM d, yyyy") : "";
        },

        // Function: DateFormatter
        // Grid formatter that will format the value in an appropriate date format.
        TwoDecimalNumberFormatter: function (row, cell, value, columnDef, dataContext) {
            return value.toFixed(2);
        },

        // Function: YesNoCellFormatter 
        // Display value as "Yes" or "No"
        // Originally from slickGrid Sample Editors.
        YesNoCellFormatter: function (row, cell, value, columnDef, dataContext) {
            return value ? "Yes" : "No";
        }
    });

    // #endregion Standard set of Formaters provided by Surge

    // #region Standard set of editors provided by Surge

    // Class: Surge.SlickGrid.CellEditor
    // Base class for SlickGrid cell editors. For information on how to use cell
    // editors, see <surge.slickGrid>.
    //
    // When an editor is specified for a grid
    // column, and the grid's editable option is enabled, an instance of the editor
    // is created when the user enters edit mode on a grid cell. The editor manages
    // all of the interaction logic for that cell edit operation, including loading
    // and saving the data from the underlying row, as well as managing UI elements.
    //
    // Cell editors have a well defined lifecycle. [[TBD]]
    Surge.Class("Surge.SlickGrid.CellEditor", Surge.Component, {
        // Property: Grid
        // The grid with which this cell editor is associated.
        Grid: new Surge.Property(),
        Position: new Surge.Property(),

        // Property: Container
        // The container into which the cell editor UI is rendered. Subclasses should
        // create their UI inside this container.
        Container: new Surge.Property(),

        // Property: Column
        // The column definition for the cell which is being edited.
        Column: new Surge.Property(),

        // Property: Item
        // The data row for the cell which is being edited.
        Item: new Surge.Property(),

        constructor: function (args) {
            this.setGrid(args.grid);
            this.setPosition(args.gridPosition);
            this.setContainer(args.container);
            this.setColumn(args.column);
            this.setItem(args.item);

            this.commitChanges = args.commitChanges;
            this.cancelChanges = args.cancelChanges;

            this._initialize();
        },

        // Function: _initialize
        // This method initializes the UI for the cell editor. It should be overridden by
        // all subclasses. It is called immediately when the cell editor is created, after
        // initializing its basic properties.
        _initialize: function () {
        },

        // Function: destroy
        // Removes all UI elements associated with the cell editor. Subclasses should 
        // remove their UI from the DOM in this method.
        destroy: function () {
        },

        // Function: focus
        // Called by the grid to set focus on the cell editor's main editing element.
        // This should focus the primary control which the user will be manipulating.
        focus: function () {
        },

        // Function: isValueChanged
        // Returns true if the value has been changed by the user since editing began.
        // If false, no changes will be made to the underlying data object.
        isValueChanged: function () {
            return false;
        },

        // Function: serializeValue
        // Returns a serialized value for the cell. This may be any object, so long as
        // <applyValue> is able to use it to apply changes to the underlying data object.
        // Note that this value may be used after the cell editor's UI is destroyed, so
        // it must not be dependent upon any UI element.
        serializeValue: function () {
        },

        // Function: loadValue
        // Loads the cell value from the underlying data item into the editor. This method
        // should populate the cell editor's UI with data from the row.
        loadValue: function (item) {
        },

        // Function: applyValue
        applyValue: function (item, state) {
            item[this.getColumn().field] = state;
        },

        // Function: validate
        validate: function () {
            return { valid: true, msg: null };
        }
    });

    // Class: Surge.SlickGrid.TimeEditor
    // A cell editor which allows the user to edit a time value. The time
    // is stored as an integer in milliseconds since midnight.
    Surge.Class("Surge.SlickGrid.TimeEditor", Surge.SlickGrid.CellEditor, {
        _initialize: function () {
            if (!$.fn.timeEntry)
                return Console.error("TimeEditor requires the jQuery timeEntry plugin but it was not found");
            this._input = $("<input type='text' class='surge-cell-time'/>")
				.timeEntry().appendTo(this.getContainer()).focus().select();
        },

        focus: function () { this._input.focus(); },
        destroy: function () { this._input.remove(); },

        serializeValue: function () {
            var date = this._input.timeEntry("getTime");
            return date ? date.getTime() - new Date(0, 0, 0).getTime() : null;
        },

        loadValue: function (item) {
            var offset = item[this.getColumn().field];

            if (offset != null)
                this._input.timeEntry("setTime", new Date(offset + new Date(0, 0, 0).getTime()));

            this._originalValue = this.serializeValue();
        },

        isValueChanged: function () {
            var val = this.serializeValue();
            return val != this._originalValue;
        }
    });

    // Class: Surge.SlickGrid.DateEditor
    // Provides nice datetime widget for editing slickgrid dates.
    // Expects dates to be in the format of new Date.getTime() (milliseconds since Jan 1 1970)
    // but will work ok with date as a string or Date object.
    //
    // Depends On:
    // - <jQuery Ui Datepicker at http://jqueryui.com/demos/datepicker/>
    // - <jQuery.globalize at http://wiki.jqueryui.com/w/page/39118647/Globalize>
    Surge.Class("Surge.SlickGrid.DateEditor", Surge.SlickGrid.CellEditor, {
        _initialize: function () {
            var self = this;
            this._input = $("<INPUT type=text class='editor-text' />");
            this._input.appendTo(this.getContainer());
            this._input.focus().select();
            this._input.datepicker({
                beforeShow: function () { self._calendarOpen = true },
                onClose: function () { self._calendarOpen = false }
            }).datepicker("show");
            this._input.width(this._input.width() - 18);
        },

        destroy: function () {
            try {
                $.datepicker.dpDiv.stop(true, true);
                this._input.datepicker("hide");
                this._input.datepicker("destroy");
                this._input.remove();
            } catch (e) { } //TODO - GM - Why do we need this?
        },

        show: function () {
            if (this._calendarOpen) {
                $.datepicker.dpDiv.stop(true, true).show();
            }
        },

        hide: function () {
            if (this._calendarOpen) {
                $.datepicker.dpDiv.stop(true, true).hide();
            }
        },

        position: function (position) {
            if (!this._calendarOpen) return;
            $.datepicker.dpDiv
				.css("top", position.top + 30)
				.css("left", position.left);
        },

        focus: function () {
            this._input.focus();
        },

        loadValue: function (item) {
            var defaultValue = this._defaultValue = item[this.getColumn().field];
            if (defaultValue)
                this._input.val(applyFormat(defaultValue instanceof Date ? defaultValue : new Date(defaultValue), "d"));
            else
                this._input.val('');
            this._input[0].defaultValue = defaultValue;
            this._input.select();
        },

        serializeValue: function () {
            var dt = this._input.datepicker("getDate");
            return dt ? dt.getTime() : undefined;
        },

        isValueChanged: function () {
            return this.serializeValue() != this._defaultValue;
        }
    });

    // Class: Surge.SlickGrid.TimeRangeEditor
    // A cell editor which allows the user to edit a time range. This editor
    // expects two fields on the row object, both integers specifying milliseconds since midnight,
    // which form a range of time. The two fields are specified by setting the
    // column's field property to a comma-separated string, e.g. data-field="MyTimeStart,MyTimeEnd".
    // The string must not contain spaces.
    Surge.Class("Surge.SlickGrid.TimeRangeEditor", Surge.SlickGrid.CellEditor, {
        _initialize: function () {
            var self = this;

            if (!$.fn.timeEntry)
                return Console.error("TimeRangeEditor requires the jQuery timeEntry plugin but it was not found");

            this._rangeStart = $("<input type='text' class='surge-cell-time'/>")
				.timeEntry();
            this._rangeEnd = $("<input type='text' class='surge-cell-time'/>").timeEntry();

            var c = this.getContainer();
            this._rangeStart.appendTo(c);
            $("<span> - </span>").appendTo(c);
            this._rangeEnd.appendTo(c);
            this._rangeStart.focus().select();
            $(c).bind("keydown.timeNav", function (e) {
                if (document.activeElement == self._rangeStart[0] && e.keyCode == $.ui.keyCode.TAB && !e.shiftKey) {
                    self._rangeEnd.focus().select();
                    return false;
                } else if (document.activeElement == self._rangeEnd[0] && e.keyCode == $.ui.keyCode.TAB && e.shiftKey) {
                    self._rangeStart.focus().select();
                    return false;
                }
            });
        },

        destroy: function () {
            $(this.getContainer()).unbind('keydown.timeNav');
            this._rangeStart.remove();
            this._rangeEnd.remove();
        },

        focus: function () {
            this._rangeStart.focus();
        },

        serializeValue: function () {
            var startDate = this._rangeStart.timeEntry("getTime");
            var endDate = this._rangeEnd.timeEntry("getTime");
            return {
                Start: startDate ? startDate.getTime() - new Date(0, 0, 0).getTime() : null,
                End: endDate ? endDate.getTime() - new Date(0, 0, 0).getTime() : null
            };
        },

        loadValue: function (item) {
            var fields = this.getColumn().field.split(',');
            var start = item[fields[0]];
            var end = item[fields[1]];

            if (start != null)
                this._rangeStart.timeEntry("setTime", new Date(start + new Date(0, 0, 0).getTime()));

            if (end != null)
                this._rangeEnd.timeEntry("setTime", new Date(end + new Date(0, 0, 0).getTime()));

            this._originalValue = this.serializeValue();
        },

        applyValue: function (item, state) {
            var fields = this.getColumn().field.split(',');

            item[fields[0]] = state.Start;
            item[fields[1]] = state.End;
        },

        isValueChanged: function () {
            var val = this.serializeValue();
            return !this._originalValue || val.Start != this._originalValue.Start || val.End != this._originalValue.End;
        }
    });


    Surge.Class("Surge.SlickGrid.PasswordEditor", Surge.SlickGrid.CellEditor, {
        _initialize: function () {
            this._input = $("<input type='password' class='surge-cell-password'/>").appendTo(this.getContainer()).focus().select();
        },

        focus: function () { this._input.focus(); },
        destroy: function () { this._input.remove(); },

        serializeValue: function () {
            return this._input.val();
        },

        loadValue: function (item) {
        },

        isValueChanged: function () {
            var val = this.serializeValue();
            return val != this._originalValue;
        }
    });

    Surge.Class("Surge.SlickGrid.DurationEditor", Surge.SlickGrid.CellEditor, {
        _initialize: function () {
            this._input = $('<input type="text" />').appendTo(this.getContainer()).focus().select();
        },

        focus: function () { this._input.focus(); },
        destroy: function () { this._input.remove(); },

        serializeValue: function () {
            return Surge.Date.parseDuration(this._input.val());
        },

        loadValue: function (item) {
            var val = $.formatDuration(item[this.getColumn().field]);
            this._input.val(val);
            this._originalValue = val;
        },

        isValueChanged: function () {
            var val = this.serializeValue();
            return val != this._originalValue;
        }
    });

    // #endregion Standard set of editors provided by Surge

    // #region Useful Editors from SlickGrid Sample Editors

    Surge.Define('Surge.SlickGrid', {
        // Function: TextCellEditor
        // Edit field with an text input.
        // Originally from SlickGrid Sample Editors.
        TextCellEditor: function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $input = $("<INPUT type=text class='editor-text' />")
					.appendTo(args.container)
					.bind("keydown.nav", function (e) {
					    if (e.keyCode === $.ui.keyCode.LEFT || e.keyCode === $.ui.keyCode.RIGHT) {
					        e.stopImmediatePropagation();
					    }
					})
					.focus()
					.select();
            };

            this.destroy = function () {
                $input.remove();
            };

            this.focus = function () {
                $input.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field] || "";
                $input.val(defaultValue);
                $input[0].defaultValue = defaultValue;
                $input.select();
            };

            this.serializeValue = function () {
                return $input.val();
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
            };

            this.validate = function () {
                if (args.column.validator) {
                    var validationResults = args.column.validator($input.val());
                    if (!validationResults.valid)
                        return validationResults;
                }

                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        /*
        * Function: LongTextCellEditor
        * An example of a "detached" editor. The UI is added onto document BODY and .position(), .show() and .hide() are implemented.
        * KeyDown events are also handled to provide handling for Tab, Shift-Tab, Esc and Ctrl-Enter.
        * Originally from SlickGrid Sample Editors.
        */
        LongTextCellEditor: function (args) {
            var $input, $wrapper, defaultValue;
            var scope = this,
                grid = args.grid;

            this.init = function () {
                var $container = $("body");

                $wrapper = $("<DIV style='z-index:10000;position:absolute;background:white;padding:5px;border:3px solid gray; -moz-border-radius:10px; border-radius:10px;'/>")
                    .appendTo($container);

                $input = $("<TEXTAREA hidefocus rows=5 style='backround:white;width:250px;height:80px;border:0;outline:0'>")
                    .appendTo($wrapper);

                $("<DIV style='text-align:right'><BUTTON>Save</BUTTON><BUTTON>Cancel</BUTTON></DIV>")
                    .appendTo($wrapper);

                $wrapper.find("button:first").bind("click", this.save);
                $wrapper.find("button:last").bind("click", this.cancel);
                $input.bind("keydown", this.handleKeyDown);

                scope.position(args.position);
                $input.focus().select();
            };

            this.handleKeyDown = function (e) {
                if (e.which == $.ui.keyCode.ENTER && e.ctrlKey) {
                    scope.save();
                }
                else if (e.which == $.ui.keyCode.ESCAPE) {
                    e.preventDefault();
                    scope.cancel();
                }
                else if (e.which == $.ui.keyCode.TAB && e.shiftKey) {
                    e.preventDefault();
                    grid.navigatePrev();
                }
                else if (e.which == $.ui.keyCode.TAB) {
                    e.preventDefault();
                    grid.navigateNext();
                }
            };

            this.save = function () {
                args.commitChanges();
            };

            this.cancel = function () {
                $input.val(defaultValue);
                args.cancelChanges();
            };

            this.hide = function () {
                $wrapper.hide();
            };

            this.show = function () {
                $wrapper.show();
            };

            this.position = function (position) {
                $wrapper
                    .css("top", position.top - 5)
                    .css("left", position.left - 5)
            };

            this.destroy = function () {
                $wrapper.remove();
            };

            this.focus = function () {
                $input.focus();
            };

            this.loadValue = function (item) {
                $input.val(defaultValue = item[args.column.field]);
                $input.select();
            };

            this.serializeValue = function () {
                return $input.val();
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
            };

            this.validate = function () {
                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        // Function: YesNoCheckboxCellEditor
        // Edit a boolean value with a checkbox.
        // Originally from SlickGrid Sample Editors.
        YesNoCheckboxCellEditor: function (args) {
            var $select;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $select = $("<INPUT type=checkbox value='true' class='editor-checkbox' hideFocus>");
                $select.appendTo(args.container);
                $select.focus();
            };

            this.destroy = function () {
                $select.remove();
            };

            this.focus = function () {
                $select.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                if (defaultValue)
                    $select.attr("checked", "checked");
                else
                    $select.removeAttr("checked");
            };

            this.serializeValue = function () {
                return ($select.attr("checked") == true) ? true : false;
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return ($select.attr("checked") != defaultValue);
            };

            this.validate = function () {
                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        // Function: CheckboxCellEditor
        // Edit a boolean value with a checkbox.
        // Originally from SlickGrid Sample Editors.
        CheckboxCellEditor: function (args) {
            var $select;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $select = $("<INPUT type=checkbox value='true' class='editor-checkbox' hideFocus>");
                $select.appendTo(args.container);
                $select.focus();
            };

            this.destroy = function () {
                $select.remove();
            };

            this.focus = function () {
                $select.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                if (defaultValue)
                    $select.attr("checked", "checked");
                else
                    $select.removeAttr("checked");
            };

            this.serializeValue = function () {
                return ($select.attr("checked") == true) ? true : false;
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return ($select.attr("checked") != defaultValue);
            };

            this.validate = function () {
                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        // Function: FloatCellEditor
        // Edit a floating point value with an input dialog that limits input to numerical values only.
        // Originally from SlickGrid Sample Editors.
        FloatCellEditor: function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $input = $("<INPUT type=text class='editor-text' />");

                $input.bind("keydown.nav", function (e) {
                    if (e.keyCode === $.ui.keyCode.LEFT || e.keyCode === $.ui.keyCode.RIGHT) {
                        e.stopImmediatePropagation();
                    }
                });

                $input.appendTo(args.container);
                $input.focus().select();
            };

            this.destroy = function () {
                $input.remove();
            };

            this.focus = function () {
                $input.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                $input.val(defaultValue || "");
                $input[0].defaultValue = defaultValue;
                $input.select();
            };

            this.serializeValue = function () {
                return parseFloat($input.val(), 10).toFixed(2) || (args.column.dynamicoptions === undefined ? 0 : args.column.dynamicoptions);
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
            };

            this.validate = function () {
                if (isNaN($input.val()))
                    return {
                        valid: false,
                        msg: "Please enter a valid integer"
                    };

                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        // Function: CurrencyCellEditor
        // Edit a numerical value as a standard 2-digit floating value
        // Originally from SlickGrid Sample Editors.
        CurrencyCellEditor: function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $input = $("<INPUT type=text class='editor-text' />");

                $input.bind("keydown.nav", function (e) {
                    if (e.keyCode === $.ui.keyCode.LEFT || e.keyCode === $.ui.keyCode.RIGHT) {
                        e.stopImmediatePropagation();
                    }
                });

                $input.appendTo(args.container);
                $input.focus().select();
            };

            this.destroy = function () {
                $input.remove();
            };

            this.focus = function () {
                $input.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                $input.val(defaultValue || "");
                $input[0].defaultValue = defaultValue;
                $input.select();
            };

            this.serializeValue = function () {
                return parseFloat($input.val(), 10).toFixed(2) || (args.column.dynamicoptions === undefined ? 0 : args.column.dynamicoptions);
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
            };

            this.validate = function () {
                if (isNaN($input.val()))
                    return {
                        valid: false,
                        msg: "Please enter a valid float"
                    };

                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        },

        // Function: IntegerCellEditor
        // Edit a value with a text input that doesn't allow non-textual input.
        // Originally from SlickGrid Sample Editors.
        IntegerCellEditor: function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $input = $("<INPUT type=text class='editor-text' />");

                $input.bind("keydown.nav", function (e) {
                    if (e.keyCode === $.ui.keyCode.LEFT || e.keyCode === $.ui.keyCode.RIGHT) {
                        e.stopImmediatePropagation();
                    }
                });

                $input.appendTo(args.container);
                $input.focus().select();
            };

            this.destroy = function () {
                $input.remove();
            };

            this.focus = function () {
                $input.focus();
            };

            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                $input.val(defaultValue || "");
                $input[0].defaultValue = defaultValue;
                $input.select();
            };

            this.serializeValue = function () {
                return parseInt($input.val(), 10) || 0;
            };

            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };

            this.isValueChanged = function () {
                return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
            };

            this.validate = function () {
                if (isNaN($input.val()))
                    return {
                        valid: false,
                        msg: "Please enter a valid integer"
                    };

                return {
                    valid: true,
                    msg: null
                };
            };

            this.init();
        }
    });
    // #endregion Useful Editors from SlickGrid Sample Editors

    // #region formatter aliases and special column extension point

    // Namespace: $.surge.slickGrid
    $.extend($.surge.slickGrid, {
        

        // Function: escapeHtml
        // Function to be used for escaping html with the signature
        // (code)
        // function (term) {
        //   ..
        //   return htmlEscapedTerm;    
        // }
        // (end)
        // A default implementation is provided
        escapeHtml: Surge.String && Surge.String.escapeHtml ||  function(term) {
            return (term + "").replace(/[&<>"'`]/g, function(t) { return "&#" + t.charCodeAt(0) + ";"; });
        },

        // Function: findIn
        // Function to be used for built-in search. It should be of the format
        // (code)
        // function (rows, searchText, columns) {
        //   ..
        //   return matchedRows;    
        // }
        // (end)
        // A default implementation is provided in the optional surge.utility.js
        findIn: Surge.Utility && Surge.Utility.findIn,

        // Object: formatters
        // These are aliases for formatters that can easily be set up and referenced throughout your project in markup rather than having to use the fully qualified object name
        // Currently available date, time, timeRange, hidden, 2decimal
        //
        // To add your own aliases
        // (code)
        // $.extend($.surge.slickGrid.formatters, {
        //   myFormatterAlias: MyApp.SlickGrid.MyFormatter
        // })
        // ..
        // <th data-id="someProperty", data-formatter="myFormatterAlias">My Property</th>
        // (end)
        formatters: {
            date: Surge.SlickGrid.DateFormatter,
            time: Surge.SlickGrid.TimeFormatter,
            timeRange: Surge.SlickGrid.TimeRangeFormatter,
            hidden: Surge.SlickGrid.HiddenFormatter,
            '2decimal': Surge.SlickGrid.TwoDecimalNumberFormatter,
            yesno: Surge.SlickGrid.YesNoCellFormatter,
            YesNoCellFormatter: Surge.SlickGrid.YesNoCellFormatter
        },
        // Object: specialColumns
        //	The following are currently implemented values for the data-special tag. Note that these will rarely actually implement the desired action,
        //	more commonly this will simply set up the corresponding UI. You may add special columns by adding a new key to this object. The object returned by the
        //  corresponding function is used to extend the default SlickGrid column options.
        //
        //	delete - Creates a formatted column with a trash icon. Bind to the "gridcellclick" event to wire up delete logic.
        //  edit   - Creates a formatted column with a pencil icon.  Bind to the "gridcellclick" event to wire up edit logic. 
        //	reorder - Creates a column that allows reordering of rows with the mouse. This will actually impelment drag-drop of rows, 
        //				only the column UI for it. To implement bind to the the "gridrowmove" event. 
        //				If you are using v2 you must remember to include rowmovemanager.v2.js.
        //  select - Creates a column containing a checkbox with name select-row and the value = to row index
        specialColumns: {
            "deleteColumn": function (colFunction) {
                // Avoid using this. It's deprecated.                          
                return {
                    id: "delete-column", name: "", width: 26, cssClass: "slick-cellCenter", unselectable: true,
                    formatter: function (row, cell, value, columnDef, dataContext) {
                        if (dataContext.Active || dataContext.Id == null || dataContext.Id == -1) {
                            return "<a href='#' class='ui-icon ui-icon-trash' style='display:block;margin:2px auto 0;width:16px;' onclick='" + colFunction + "(" + row + "," + cell + "); return false;'>[X]</a>";
                        }
                        return "<a href='#' class='ui-icon ui-icon-arrowreturnthick-1-n' style='display:block;margin:2px auto 0;width:16px;' onclick='" + colFunction + "(" + row + "," + cell + "); return false;'>[X]</a>";
                    }
                };
            },
            "delete": function () {
                return {
                    id: "delete-column", width: 26, minWidth: 26, maxWidth: 26, unselectable: true,
                    formatter: function (row, cell, value, columnDef, dataContext) {
                        return "<a href='javascript:void(0)' class='ui-icon ui-icon-trash'></a>";
                    }
                };
            },
            "edit": function () {
                return {
                    id: "edit-column", width: 26, minWidth: 26, maxWidth: 26, unselectable: true,
                    formatter: function (row, cell, value, columnDef, dataContext) {
                        return "<a href='javascript:void(0)' class='ui-icon ui-icon-pencil'></a>";
                    }
                };
            },
            "newDeleteColumn": function () { return $.surge.slickGrid.specialColumns['delete'].apply(this, arguments); },
            "reorder": function () {
                return { id: "resize-column", width: 26, minWidth: 26, maxWidth: 26, behavior: "selectAndMove", unselectable: true, resizable: false, cssClass: "dnd",
                    formatter: function (row, cell, value, columnDef, dataContext) {
                        return "<a href='javascript:void(0)' style='display:block; margin: 0 auto; width: 16px;' class='ui-icon ui-icon-grip-solid-horizontal'></a>";
                    }
                };
            },
            "reorderColumn": function () { return $.surge.slickGrid.specialColumns['reorder'].apply(this, arguments); },
            "select": function () {
                return {
                    id: "select-column", width: 26, minWidth: 26, maxWidth: 26, unselectable: true,
                    formatter: function (row, cell, value, columnDef, dataContext) {
                        return '<input type="checkbox" name="select-row" value="' + row + '" />';
                    }
                };
            }
        }
    });

    // #endregion formatter aliases and special column extension point

})(jQuery);
