; (function ($) {
    $.buildcuteTabBar = function (object, options) {
        if ($(object).data("cutetabbar")) { return false; }

        options = $.extend({}, $.fn.cuteTabBarCtrl.defaults, options);

        var cuteTabBarClass = {
            object: null,
            selectTab: function (trigger) {
                var activeTab = $(object).data("cutetabbar.activeTab");

                if (activeTab) {
                    $("div:eq(0)", activeTab)
                    .hide()
                    .parent()
                    .removeClass('current');
                }

                $(object).data("cutetabbar.activeTab",
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

        $(object).data("cutetabbar", {
            init: true,
            options: options,
            version: "0.1"
        });

        $(object).data("cutetabbar.activeTab", null);

        $(">ul>li", object).each(function () {
            var isSelected = $(this).hasClass("current");
            if (isSelected) {
                $(object).data("cutetabbar.activeTab", this);
            }

            $(">ul", this).wrap('<div class="cutetab"></div>')
                .parent()
                .css('display', isSelected ? 'block' : 'none');

            $("a:eq(0)", this).bind("click", function (e) {
                (cuteTabBarClass.selectTab)(this);
                return false;
            });
        });

        $("div.cutetab>ul>li", object).each(function () {
            //$(this).wrapInner('<div class="panel"></div>');
        });

        $(object).data("cutetabbar.class", cuteTabBarClass);
    }

    $.fn.cuteTabBarCtrl = function (options) {
        return this.each(function () {
            $.buildcuteTabBar(this, options);
        });
    };

})(jQuery)
