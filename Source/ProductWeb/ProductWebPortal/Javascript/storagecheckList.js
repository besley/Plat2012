; (function (pwpStorageCheckList, $, undefined) {
    var gridStorageCheck = null;
    var dsStorageCheck = [];
    var changedItemsStorageCheck = [];
    var dataViewStoargeCheck;

    var columnsStorageCheck = [
        { id: "id", name: "编号", field: "ID", width: 70, cssClass: "cell-title" },
        { id: "ProductId", name: "产品编号", field: "ProductId", width: 60 },
        { id: "CheckTypeId", name: "出入库类型", field: "CheckTypeId", editor: Slick.Editors.Text },
        { id: "CheckAmount", name: "数量", field: "CheckAmount", editor: Slick.Editors.Text },
        { id: "CheckPerson", name: "操作用户", field: "CheckPerson", editor: Slick.Editors.Text },
        { id: "CheckDatetime", name: "日期", field: "CheckDatetime", editor: Slick.Editors.Date }
    ];

    var checkboxStorageCheck = new Slick.CheckboxSelectColumn({
        cssClass: "slick-cell-checkboxsel"
    });
    columnsStorageCheck.splice(0, 0, checkboxStorageCheck.getColumnDefinition());

    var optionsStorageCheck = {
        editable: true,
        enableAddRow: true,
        enableCellNavigation: true,
        asyncEditorLoading: true,
        forceFitColumns: false
    };

    function init() {
        $(".grid-header .ui-icon")
            .addClass("ui-state-default ui-corner-all")
            .mouseover(function (e) {
                $(e.target).addClass("ui-state-hover")
            })
            .mouseout(function (e) {
                $(e.target).removeClass("ui-state-hover")
            });

        doHttpClientGet("/ProductSys.WebAPI/api/StorageCheck/GetAll", function (dataJson) {
            dsStorageCheck = dataJson;
            InitializeSlickGridData();
        });
    }

    function InitializeSlickGridData() {
        dataViewStoargeCheck = new Slick.Data.DataView();
        gridStorageCheck = new Slick.Grid("#myGridStorageCheck", dataViewStoargeCheck, columnsStorageCheck, optionsStorageCheck);
        gridStorageCheck.setSelectionModel(new Slick.RowSelectionModel());

        var pager = new Slick.Controls.Pager(dataViewStoargeCheck, gridStorageCheck, $("#myPagerStorageCheck"));
        var columnpicker = new Slick.Controls.ColumnPicker(columnsStorageCheck, gridStorageCheck, optionsStorageCheck);

        gridStorageCheck.registerPlugin(checkboxStorageCheck);

        gridStorageCheck.onCellChange.subscribe(function (e, args) {
            var item = args.item;
            changedItemsStorageCheck.push(item);
            dataViewStoargeCheck.updateItem(item.id, item);
        });

        gridStorageCheck.onDblClick.subscribe(function (e, args) {
            var index = args.row;
            var item = dataViewStoargeCheck.getItemByIdx(index);
            parent.$mainGridRowSelected(item);
        });

        dataViewStoargeCheck.onRowsChanged.subscribe(function (e, args) {
            gridStorageCheck.invalidateRows(args.rows);
            gridStorageCheck.render();
        });

        dataViewStoargeCheck.beginUpdate();
        dataViewStoargeCheck.setItems(dsStorageCheck, "ID");
        dataViewStoargeCheck.endUpdate();
        dataViewStoargeCheck.syncGridSelection(gridStorageCheck, true);
    }

    pwpStorageCheckList.onAddClick = function () {
        var url = "/ProductWebPortal/StorageCheck/Create";

        $("#storageCheckFormFancybox").attr("href", url);
        $("#storageCheckFormFancybox").attr("display", "inline");
        $("#storageCheckFormFancybox").click();
    };

    function getSelectedDataItem() {
        var selectedDataItem;
        var indexs = gridStorageCheck.getSelectedRows();
        if (indexs != null && indexs[0] >= 0) {
            selectedDataItem = dataViewStoargeCheck.getItemByIdx(indexs[0]);
        }
        return selectedDataItem;
    }

    pwpStorageCheckList.onEditClick = function () {
        var item = getSelectedDataItem()
        var id = item.ID;
        var url = "/ProductWebPortal/StorageCheck/Detail/" + id;

        $("#storageCheckFormFancybox").attr("href", url);
        $("#storageCheckFormFancybox").attr("display", "inline");
        $("#storageCheckFormFancybox").click();
    };

    pwpStorageCheckList.onDeleteClick = function () {
        var delData = [];
        var isOk = confirm("确实要删除吗?");
        if (isOk) {
            var item = getSelectedDataItem();
            if (item != null && item.ID >= 0) {
                delData.push(item.ID);
                var jsonValue = JSON.stringify(delData);
                doAjaxPost('/ProductWebPortal/StorageCheck/Delete',
                    jsonValue,
                    function (data) {
                        alert(data);
                        window.location.reload();
                    });
            }
            else {
                alert("请选中要删除的记录，然后再删除!");
            }
        }
    };

    function clearChangedItems() {
        changedItemsStorage = [];
    }


    pwpStorageCheckList.onSaveClick = function () {
        var jsonStorageCheck = JSON.stringify(changedItemsStorageCheck);
        doHttpClientUpdate(
            '/ProductSys.WebAPI/api/StorageCheck',
            jsonStorageCheck,
            function (data) {
                alert(data);
            });
        clearChangedItems();
    };

    init();

})(window.pwpStorageCheckList = window.pwpStorageCheckList || {}, jQuery)