define(['jshelper', 'jstree', 'cutetoolbar', 'fancybox', 'Controllers/StorageCheck/StorageCheck-List'], function () {
    function render() {
        $("#storageCheckCuteToolBar").cuteToolBarCtrl({
            defaultCRUDButtons: true,
            defaultPrefixName: "prefixStorageCheckToolbar",
        });

        require(['Controllers/StorageCheck/StorageCheck-List'], function (pwpStorageCheckList) {
            $("#storageCheckCuteToolBar").cuteToolBarBind("search", onStorageCheckListSearchClick);
            $("#storageCheckCuteToolBar").cuteToolBarBind("add", pwpStorageCheckList.onAddClick);
            $("#storageCheckCuteToolBar").cuteToolBarBind("edit", pwpStorageCheckList.onEditClick);
            $("#storageCheckCuteToolBar").cuteToolBarBind("delete", pwpStorageCheckList.onDeleteClick);
            $("#storageCheckCuteToolBar").cuteToolBarBind("save", pwpStorageCheckList.onSaveClick);
        });

        $("#storageCheckFormFancybox").fancybox({
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

    function onStorageCheckListSearchClick() {
        alert("search Storage Check List");
    }

    //渲染输出
    render();

    return {
        render: render
    }
})
