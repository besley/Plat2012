require(['jshelper'], function(){
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

            //绑定href的单击事件
            bindButton: function (ref, fnc) {
                var prefix = options.defaultPrefixName;
                var hrefName = "#" + prefix + "_cutetoolbar_" + ref;
                $(hrefName).bind("click", fnc);
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
            //href 的名称应该全局唯一，否则在同一页面的多表格下会引起冲突
            var prefix = options.defaultPrefixName + "_cutetoolbar_";
            var img = buildHTML("img", {src: "/PlatJS/Scripts/cutetoolbar/images/query.png", alt: ""});
            var a = buildHTML("a", img + "查找", {id: prefix + "search", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/PlatJS/Scripts/cutetoolbar/images/add.png", alt: "" });
            var a = buildHTML("a", img + "新增", {id: prefix + "add", href: "#" });
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/PlatJS/Scripts/cutetoolbar/images/edit.png", alt: "" });
            var a = buildHTML("a", img + "编辑", {id: prefix + "edit", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/PlatJS/Scripts/cutetoolbar/images/delete.png", alt: "" });
            var a = buildHTML("a", img + "删除", {id: prefix + "delete", href: "#"});
            buttonsHtml.push(a);

            img = buildHTML("img", { src: "/PlatJS/Scripts/cutetoolbar/images/save.png", alt: "" });
            var a = buildHTML("a", img + "保存", {id: prefix + "save", href: "#"});
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

    $.fn.cuteToolBarBind = function (ref, fnc) {
        //why each function?!
        return this.each(function(){
            if ($(this).data("cutetoolbar")){
                $(this).data("cutetoolbar.class").bindButton(ref, fnc);
            }
        });
    };

})
