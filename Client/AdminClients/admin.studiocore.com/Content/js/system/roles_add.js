layui.config({
    base: '/Content/lib/system/'
}).use(['jquery', 'element', 'layer', 'form', 'common'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form, element = layui.element, common = layui.common;

    common.$Enter("#btn_sub");



    form.on('submit(sub)', function (data) {
        var index = common.$loading();
        common.$disabled(data.elem);
        var url = data.form.action;
        var dt = data.field;
        $.post(url, dt, function (result) {
            layer.msg(result.Message, function () {
                common.$closeloading(index);
                common.$removedisabled("#btn_sub");
                if (result.Code === 1) {
                    window.parent.layer.closeAll();
                    window.parent.location.reload();
                }
            });
        });

        return false;
    });



});

