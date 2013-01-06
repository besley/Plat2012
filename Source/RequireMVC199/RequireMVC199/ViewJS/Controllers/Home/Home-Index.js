define(['jquery', 'jshelper', 'cutemenutab', 'jstree'], function () {
    var pwpHomeIndex = (function () {

        var homeIndexClass = {};

        homeIndexClass.init = function() {
            console.log('home index iniit begin...');

            //各Tab页初始化
            var treeTab = $("#treeSubShow_01");
            var treeContDiv = $("#treeSSCont");
            treeTab.initTabMenu(treeTab, treeContDiv);

            var topTab = $("#topSubShow_01");
            var topContDiv = $("#topSSCont");
            topTab.initTabMenu(topTab, topContDiv);

            var bottomTab = $("#bottomSubShow_01");
            var bottomContDiv = $("#bottomSSCont");
            bottomTab.initTabMenu(bottomTab, bottomContDiv);

            //树控件加载
            //$("#treeContainerProductSys").buildJsTree("/ProductSys.WebApi/api/FunctionMenu/GetJsTreeView",
            //    function (data) {
            //        ;
            //    }
            //);

            $("#treeContainerProductSys").jstree({
                "json_data": {
                    "ajax": {
                        "url": "/ProductSys.WebApi/api/FunctionMenu/GetJsTreeView",
                        "type": "GET",
                        "dataType": "json",
                        "contentType": "application/json charset=utf-8",
                        "success": function (data) { }
                    }
                },
                "plugins": ["themes", "json_data", "ui"]
            });

            $("#treeContainerProductSys").bind("select_node.jstree", function (event, data) {
                var functionId = $('#treeContainerProductSys').jstree('get_selected').attr('id');
                loadMainGridData(functionId);
            });
        }

        function gotoClick() {
            loadMainGridData(6);
        }

        function loadMainGridData(functionId) {
            //if (functionId == 6) {
                require(['Controllers/Product/Product-List'], function (listObject) {
                    $("#mainGridContainer").html("")
                        .load('/RequireMVC199/Product/List', function () {
                            listObject.buildToolBar();
                            $("#mainGridContainer").focus();
                        });
                });
            //}
        }

        return homeIndexClass;
    })();

    pwpHomeIndex.init();

    return {
        init: pwpHomeIndex.init,
    }
})

