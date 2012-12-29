; (function ($) {
    $.buildcuteBar = function (object, options) {
        if ($(object).data("cutebar")) { return false; }

        options = $.extend({}, $.fn.cuteTabBarCtrl.defaults, options);

        var cuteBarClass = {
            object: null,
            selectTab: function (trigger) {
                var activeTab = $(object).data("cutebar.activeTab");

                if (activeTab) {
                    $("div:eq(0)", activeTab)
                    .hide()
                    .parent()
                    .removeClass('current');
                }

                $(object).data("cutebar.activeTab",
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

        $(object).data("cutebar", {
            init: true,
            options: options,
            version: "0.1"
        });

        $(object).data("cutebar.activeTab", null);

        $(">ul>li", object).each(function () {
            var isSelected = $(this).hasClass("current");
            if (isSelected) {
                $(object).data("cutebar.activeTab", this);
            }

            $(">ul", this).wrap('<div class="cutetab"></div>')
                .parent()
                .css('display', isSelected ? 'block' : 'none');

            $("a:eq(0)", this).bind("click", function (e) {
                (cuteBarClass.selectTab)(this);
                return false;
            });
        });

        $("div.cutetab>ul>li", object).each(function () {
            $(this).wrapInner('<div class="panel"></div>');
        });

        $(object).data("cutebar.class", cuteBarClass);
    }

    $.fn.cuteTabBarCtrl = function (options) {
        return this.each(function () {
            $.buildcuteBar(this, options);
        });
    };

})(jQuery)
