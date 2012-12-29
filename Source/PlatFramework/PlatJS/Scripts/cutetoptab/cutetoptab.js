; (function ($) {
    $.buildcuteTopTab = function (object, options) {
        if ($(object).data("cutetoptab")) { return false; }

        options = $.extend({}, $.fn.cuteTopTabCtrl.defaults, options);

        var cuteTopTabClass = {
            object: null,
            selectTab: function (trigger) {
                var activeTab = $(object).data("cutetoptab.activeTab");

                if (activeTab) {
                    $("div:eq(0)", activeTab)
                    .hide()
                    .parent()
                    .removeClass('current');
                }

                $(object).data("cutetoptab.activeTab",
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

        $(object).data("cutetoptab", {
            init: true,
            options: options,
            version: "0.1"
        });

        $(object).data("cutetoptab.activeTab", null);

        $(">ul>li", object).each(function () {
            var isSelected = $(this).hasClass("current");
            if (isSelected) {
                $(object).data("cutetoptab.activeTab", this);
            }

            $(">ul", this).wrap('<div class="cutetoptab"></div>')
                .parent()
                .css('display', isSelected ? 'block' : 'none');

            $("a:eq(0)", this).bind("click", function (e) {
                (cuteTopTabClass.selectTab)(this);
                return false;
            });
        });

        $("div.cutetoptab>ul>li", object).each(function () {
            //$(this).wrapInner('<div class="panel"></div>');
        });

        $(object).data("cutetoptab.class", cuteTopTabClass);
    }

    $.fn.cuteTopTabCtrl = function (options) {
        return this.each(function () {
            $.buildcuteTopTab(this, options);
        });
    };

})(jQuery)
