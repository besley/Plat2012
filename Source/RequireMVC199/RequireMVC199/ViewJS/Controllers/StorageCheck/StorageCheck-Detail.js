require(['jshelper', 'fancybox'], function () {
    function start() {
        bindEvents();
    }

    //绑定保存按钮事件
    function bindEvents() {
        $("#btnStorageCheckFormSave").bind('click', onStorageCheckFormSaveClick);
        //, parent.jQuery.fancybox.close()
    }

    function onStorageCheckFormSaveClick() {
        var storageCheckId = $("#hdnStorageCheckFormStorageCheckId").val();
        var productId = $("#txtStorageCheckFormProductId").val();
        var checkAmount = $("#txtStorageCheckFormCheckAmount").val();
        var checkPerson = $("#txtStorageCheckFormCheckPerson").val();
        var checkDatetime = $("#txtStorageCheckFormCheckDatetime").val();

        var dataStorageCheck = {
            "ID": storageCheckId,
            "ProductId": productId,
            "CheckAmount": checkAmount,
            "CheckPerson": checkPerson,
            "CheckDatetime": checkDatetime
        };

        var opType = $("#hdnStorageCheckFormOpType").val();
        if (opType == "A") {
            $.doHttpClientSave("/ProductSys.WebAPI/api/StorageCheck/Create",
                    JSON.stringify(dataStorageCheck),
                    function (result) {
                        alert(result);
                        window.location.href = "/ProductWebPortal/StorageCheck/List";
                    });
        }
        else {
            $.doHttpClientUpdate("/ProductSys.WebAPI/api/StorageCheck/Update",
                   JSON.stringify(dataStorageCheck),
                   function (result) {
                       alert(result);

                   });
        }
    }

    //界面初始化的操作
    start();
})