define(['jshelper', 'jstree', 'cutetoolbar', 'fancybox', 'Controllers/Storage/Storage-List'], function () {
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
    }

    function onStorageListSearchClick() {
        alert("search Storage");
    }

    //渲染输出
    render();

    return {
        render: render
    }
})