require(['jshelper', 'fancybox'], function () {
    function start() {
        bindEvents();
    }

    //绑定保存按钮事件
    function bindEvents() {
        $("#btnProductFormSave").bind('click', onProductFormSaveClick);
        //, parent.jQuery.fancybox.close()
    }

    //保存操作
    function onProductFormSaveClick() {
        var productId = $("#txtProductFormProductId").val();
        var productName = $("#txtProductFormProductName").val();
        var productType = $("#txtProductFormProductType").val();
        var unitPrice = $("#txtProductFormUnitPrice").val();
        var notes = $("#txtProductFormNotes").val();
        var dataProduct = {
            "ID": productId,
            "ProductName": productName,
            "ProductType": productType,
            "UnitPrice": unitPrice,
            "Notes": notes
        };

        var opType = $("#hdnProductFormOpType").val();
        if (opType == "A") {
            $.doHttpClientSave("/ProductSys.WebAPI/api/Product/Create",
                JSON.stringify(dataProduct),
                function (result) {
                    alert(result);
                    window.location.href = "/RequireMVC199/Product/List";
                });
        }
        else {
            $.doHttpClientUpdate("/ProductSys.WebAPI/api/Product/Update",
                JSON.stringify(dataProduct),
                function (result) {
                    alert(result);
                });
        }
    }

    //界面初始化的操作
    start();
})