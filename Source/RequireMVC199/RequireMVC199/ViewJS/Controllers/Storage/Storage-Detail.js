require(['jshelper', 'fancybox'], function () {
    function start() {
        bindEvents();
    }

    //绑定保存按钮事件
    function bindEvents() {
        $("#btnStorageFormSave").bind('click', onStorageFormSaveClick);
        //, parent.jQuery.fancybox.close()
    }

    function onStorageFormSaveClick() {
        var storageId = $("#hdnStorageFormStorageId").val();
        var productIdStorage = $("#txtStorageFormProductId").val();
        var storageAmount = $("#txtStorageFormStorageAmount").val();
        var lastUpdatedDate = $("#txtStorageFormLastUpdatedDate").val();

        var dataStorage = {
            "ProductId": productIdStorage,
            "StorageAmount": storageAmount,
            "LastUpdatedDate": lastUpdatedDate
        };

        var opType =  $("#hdnStorageFormOpType").val();
        if (opType == "A") {
            $.doHttpClientSave("/ProductSys.WebAPI/api/Storage/Create",
                    JSON.stringify(dataStorage),
                    function (result) {
                        alert(result);
                        window.location.href = "/ProductWebPortal/Product/List";
                    });
        }
        else {
            $.doHttpClientUpdate("/ProductSys.WebAPI/api/Storage/Update",
                   JSON.stringify(dataStorage),
                   function (result) {
                       alert(result);

                   });
        }
    }

    //界面初始化的操作
    start();
})