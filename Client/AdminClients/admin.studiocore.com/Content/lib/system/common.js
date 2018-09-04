//自定义公共方法
layui.define(['jquery', 'element', 'layer'], function (exports) {
    var $ = layui.$, layer = layui.layer, element = layui.element;
    var obj = {
        $Enter: function (el) {
            $("body").keydown(function () {
                if (event.keyCode == "13") {//keyCode=13是回车键
                    $(el).click();
                }
            });
        },
        $loading: function () {
            var index = layer.load(1, {
                shade: [0.1, '#fff'] //0.1透明度的白色背景
            });

            return index;
        },
        $closeAll: function () {
            layer.closeAll();
        },
        $closeloading: function (index) {
            layer.close(index);
        },
        $disabled: function (el) {
            $(el).addClass("layui-btn-disabled");
        },
        $removedisabled: function (el) {
            $(el).removeClass("layui-btn-disabled");
        },
        $sideShrink: function (el) {
            $("#LAY_app_flexible").click(function () {
                if ($(el).hasClass('layadmin-side-shrink')) {
                    $(el).removeClass("layadmin-side-shrink");
                } else {
                    $(el).addClass("layadmin-side-shrink");

                }

            });

        },
        $refresh: function () {
            window.location.reload();
        }


    }

    exports('common', obj);
});



