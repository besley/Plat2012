(function ($) {
    $.fn.initTabMenu = function (objMenu, objContent, options) {
        //Menu绑定click事件  ----  unbind()删除所有绑定事件
        $("label", objMenu).unbind().bind("click", function () {
            //删掉所有label的选中class
            $("label", objMenu).removeClass("selected");
            //当前对象选中Class
            $(this).addClass("selected");
            //所有内容DIV 隐藏
            objContent.children("div").hide();
            //当前选中tab的内容DIV显示
            objContent.children().eq($("label", objMenu).index($(this))).show();
        });
    }
})(jQuery)