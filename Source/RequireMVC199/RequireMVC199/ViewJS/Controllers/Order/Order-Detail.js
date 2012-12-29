require(['jshelper', 'fancybox'], function () {
    function start() {
        bindEvents();
    }

    //绑定保存按钮事件
    function bindEvents() {
        $("#btnOrderFormSave").bind('click', onOrderFormSaveClick);
        //, parent.jQuery.fancybox.close()
    }

    function onOrderFormSaveClick() {
        var orderId = $("#txtOrderFormOrderId").val();
        var productId = $("#txtOrderFormProductId").val();
        var productName = $("#txtOrderFormProductName").val();
        var buyAmount = $("#txtOrderFormBuyAmount").val();
        var buyDate = $("#txtOrderFormBuyDate").val();
        var buyPerson = $("#txtOrderFormBuyPerson").val();
        var isArrivaled = $("#txtOrderFormIsArrivaled").val();
        var arrivaledDate = $("#txtOrderFormArrivaledDate").val();
        var notes = $("#txtNotes").val();
        var dataOrder = {
            "ID": orderId,
            "ProductId": productId,
            "ProductName": productName,
            "BuyAmount": buyAmount,
            "BuyDate": buyDate,
            "BuyPerson": buyPerson,
            "IsArrivaled": isArrivaled,
            "ArrivaledDate": arrivaledDate,
            "Notes": notes
        };

        var opType = $("#hdnOrderFormOpType").val();
        if (opType == "A") {
            $.doHttpClientSave("/ProductSys.WebAPI/api/OrderView/Create",
                    JSON.stringify(dataOrder),
                    function (result) {
                        alert(result);
                        window.location.href = "/RequreiveMVC199/Order/Index";
                    });
        }
        else {
            $.doHttpClientUpdate("/ProductSys.WebAPI/api/OrderView/Update",
                   JSON.stringify(dataOrder),
                   function (result) {
                       alert(result);

                   });
        }
    }
    
    //界面初始化的操作
    start();
})