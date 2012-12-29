; (function ($) {
    $.buildcuteToolBar = function (object, options) {
        if ($(object).data("cutetoolbar")) { return false; }

        options = $.extend({}, $.fn.cuteToolBarCtrl.defaults, options);

        var cuteToolBarClass = {
            object: null,
            selectButton: function (trigger) {
                var me = $(trigger);
                if (options.onSelectButton) {
                    (options.onSelectButton)({ id: me.attr("id"), rel: me.attr("rel") });
                }
            },

            bindButton: function(ref, fnc){
                switch (ref) {
                    case "search":
                        $("#cutetoolbar_search").bind("click", fnc);
                        break;
                    case "add":
                        break;
                    case "edit":
                        break;
                    case "delete":
                        break;
                    case "save":
                        break;
                }
            }
        };

        //构造html的通用方法
        buildHTML = function (tag, html, attrs) {
            // you can skip html param
            if (typeof (html) != 'string') {
                attrs = html;
                html = null;
            }
            var h = '<' + tag;
            for (attr in attrs) {
                if (attrs[attr] === false) continue;
                h += ' ' + attr + '="' + attrs[attr] + '"';
            }
            return h += html ? ">" + html + "</" + tag + ">" : "/>";
        };

        //构造默认按钮Html格式
        if (options.defaultCRUDButtons) {
            var buttonsHtml = [];
            var img = buildHTML("img", {src: "/MvcApplication199/Content/Images/query.png", alt: ""});
            var a = buildHTML("a", img + "查找", {id: "cutetoolbar_search", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/MvcApplication199/Content/Images/add.png", alt: "" });
            var a = buildHTML("a", img + "新增", {id: "cutetoolbar_add", href: "#" });
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/MvcApplication199/Content/Images/edit.png", alt: "" });
            var a = buildHTML("a", img + "编辑", {id: "cutetoolbar_edit", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/MvcApplication199/Content/Images/delete.png", alt: "" });
            var a = buildHTML("a", img + "删除", {id: "cutetoolbar_delete", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/MvcApplication199/Content/Images/save.png", alt: "" });
            var a = buildHTML("a", img + "保存", {id: "cutetoolbar_save", href: "#"});
            buttonsHtml.push(a);

            var html = "<li>" + buttonsHtml.join("</li><li>") + "</li>";

            var element = $(">ul>li:eq(0)", object);
            if (element.length > 0) {
                element.before(html);
            }
            else {
                $(">ul:eq(0)", object).wrapInner(html);
            }
        }

        $(object).data("cutetoolbar", {
            init: true,
            options: options,
            version: "0.1"
        });

        $(">ul>li", object).each(function () {
            $("a:eq(0)", this).bind("click", function (e) {
                (cuteToolBarClass.selectButton)(this);
                return false;
            });
        });

        $(object).data("cutetoolbar.class", cuteToolBarClass);
    };

    $.fn.cuteToolBarCtrl = function (options) {
        $.buildcuteToolBar(this, options);
    };

    $.fn.cuteToolBarBind = function(ref, fnc){
        return this.each(function(){
            if ($(this).data("cutetoolbar")){
                $(this).data("cutetoolbar.class").bindButton(ref, fnc);
            }
        });
    };

})(jQuery)


//return this.each(function() { 
//    if($(this).data("officebar")) {
//        $(this).data("officebar.class").bindButton(ref, fnc); }
//});