define(['jshelper', 'slickgrid'], function () {
    function render() {
        $("#storageCuteToolBar").cuteToolBarCtrl({
            defaultCRUDButtons: true,
            defaultPrefixName: "prefixStorageToolBar",
        });

        require(['Controllers/Storage/Storage-List'], function (pwpStorageList) {
            $("#storageCuteToolBar").cuteToolBarBind("search", onStorageListSearchClick);
            $("#storageCuteToolBar").cuteToolBarBind("add", pwpStorageList.onAddClick);
            $("#storageCuteToolBar").cuteToolBarBind("edit", pwpStorageList.onEditClick);
            $("#storageCuteToolBar").cuteToolBarBind("delete", pwpStorageList.onDeleteClick);
            $("#storageCuteToolBar").cuteToolBarBind("save", pwpStorageList.onSaveClick);
        });

        $("#storageFormFancybox").fancybox({
            maxWidth: 600,
            maxHeight: 400,
            fitToView: false,
            width: '70%',
            height: '70%',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none'
        });

        pwpStroageList.init();
    }

    function onStorageListSearchClick() {
        alert("search Storage");
    }

    var pwpStroageList = (function () {
        var storageListClass = {};

        var gridStorage = null;
        var dsStorage = [];
        var changedItemsStorage = [];
        var dataViewStorage;

        var columnsStorage = [
            { id: "id", name: "编号", field: "ID", width: 70, cssClass: "cell-title" },
            { id: "ProductId", name: "产品编号", field: "ProductId", width: 60 },
            { id: "StorageAmount", name: "库存数量", field: "StorageAmount", editor: Slick.Editors.Text },
            { id: "LastUpdateDatet", name: "更新日期", field: "LastUpdatedDate", editor: Slick.Editors.Date }
        ];

        var checkboxStorage = new Slick.CheckboxSelectColumn({
            cssClass: "slick-cell-checkboxsel"
        });
        columnsStorage.splice(0, 0, checkboxStorage.getColumnDefinition());

        var optionsStorage = {
            editable: true,
            enableAddRow: true,
            enableCellNavigation: true,
            asyncEditorLoading: true,
            forceFitColumns: false,
            enableColumnReorder: false
        };

        storageListClass.init = function() {
            $(".grid-header .ui-icon")
                .addClass("ui-state-default ui-corner-all")
                .mouseover(function (e) {
                    $(e.target).addClass("ui-state-hover")
                })
                .mouseout(function (e) {
                    $(e.target).removeClass("ui-state-hover")
                });

            $.doHttpClientGet("/ProductSys.WebAPI/api/Storage/GetAll", function (dataJson) {
                dsStorage = dataJson;
                storageListClass.fillData();
            });
        }

        storageListClass.fillData = function() {
            //dataViewStorage = new Slick.Data.DataView({ inlineFilters: true });
            dataViewStorage = new Slick.Data.DataView();
            gridStorage = new Slick.Grid($("#myGridStorage"), dataViewStorage, columnsStorage, optionsStorage);
            gridStorage.setSelectionModel(new Slick.RowSelectionModel());

            var pager = new Slick.Controls.Pager(dataViewStorage, gridStorage, $("#myPagerStorage"));
            var columnpicker = new Slick.Controls.ColumnPicker(columnsStorage, gridStorage, optionsStorage);

            gridStorage.registerPlugin(checkboxStorage);

            gridStorage.onCellChange.subscribe(function (e, args) {
                var item = args.item;
                changedItemsStorage.push(item);
                dataViewStorage.updateItem(item.id, item);
            });

            gridStorage.onDblClick.subscribe(function (e, args) {
                var index = args.row;
                var item = dataViewStorage.getItemByIdx(index);
                parent.$mainGridRowSelected(item);
            });

            dataViewStorage.onRowsChanged.subscribe(function (e, args) {
                gridStorage.invalidateRows(args.rows);
                gridStorage.render();
            });

            dataViewStorage.beginUpdate();
            dataViewStorage.setItems(dsStorage, "ID");

            dataViewStorage.endUpdate();
            dataViewStorage.syncGridSelection(gridStorage, true);
        }

        storageListClass.onAddClick= function() {
            var url = "/RequireMVC199/Storage/Create";

            $("#storageFormFancybox").attr("href", url);
            $("#storageFormFancybox").attr("display", "inline");
            $("#storageFormFancybox").click();
        };

        function getSelectedDataItem() {
            var selectedDataItem;
            var indexs = gridStorage.getSelectedRows();
            if (indexs != null && indexs[0] >= 0) {
                selectedDataItem = dataViewStorage.getItemByIdx(indexs[0]);
            }
            return selectedDataItem;
        }

        storageListClass.onEditClick = function() {
            var item = getSelectedDataItem();
            var id = item.ID;
            var url = "/RequireMVC199/Storage/Detail/" + id;

            $("#storageFormFancybox").attr("href", url);
            $("#storageFormFancybox").attr("display", "inline");
            $("#storageFormFancybox").click();
        };

        storageListClass.onDeleteClick = function() {
            var delData = [];
            var isOk = confirm("确实要删除吗?");
            if (isOk) {
                var item = getSelectedDataItem();
                if (item != null && item.ID >= 0) {
                    delData.push(item.ID);
                    var jsonValue = JSON.stringify(delData);
                    doAjaxPost('/RequireMVC199/Storage/Delete',
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

        storageListClass.onSaveClick = function() {
            var jsonStorage = JSON.stringify(changedItemsStorage);
            $.doHttpClientUpdate(
                '/ProductSys.WebAPI/api/Storage',
                jsonStorage,
                function (data) {
                    alert(data);
                });
            clearChangedItems();
        };

        return storageListClass;
    })();


    //执行初始化操作
    render();

    return {
        render: render
    }
})