; (function ($) {
    $.buildTreeTabPanel = function (object, options) {
        if ($(object).data("treeTabPanel")) { return false; }

        options = $.extend({}, $.fn.treeTabPanelCtrl.defaults, options);

        var treeTabPanelClass = {
            object: null,
            selectTab: function (trigger) {
                var activeTab = $(object).data("treeTabPanel.activeTab");

                if (activeTab) {
                    $("div:eq(0)", activeTab)
                    .hide()
                    .parent()
                    .removeClass('current');
                }

                $(object).data("treeTabPanel.activeTab",
                    $(trigger)
                    .next()
                    .show()
                    .end()
                    .parent()
                    .addClass('current')
                    .get(0));

                var me = $(trigger);
                if (options.onSelectTab) {
                    (options.onSelectTab)({ id: me.attr("id"), rel: me.attr("rel") });
                }
            }
        }

        $(object).data("treeTabPanel", {
            init: true,
            options: options,
            version: "0.1"
        });

        $(object).data("treeTabPanel.activeTab", null);

        $(">ul>li", object).each(function () {
            var isSelected = $(this).hasClass("current");
            if (isSelected) {
                $(object).data("treeTabPanel.activeTab", this);
            }

            $(">ul", this).wrap('<div class="treetabpanel"></div>')
                .parent()
                .css('display', isSelected ? 'block' : 'none');

            $("a:eq(0)", this).bind("click", function (e) {
                (treeTabPanelClass.selectTab)(this);
                return false;
            });
        });

        $("div.treetabpanel>ul>li", object).each(function () {
            //$(this).wrapInner('<div class="panel"></div>');
        });

        $(object).data("treeTabPanel.class", treeTabPanelClass);
    }

    $.fn.treeTabPanelCtrl = function (options) {
        return this.each(function () {
            $.buildTreeTabPanel(this, options);
        });
    };

})(jQuery)
