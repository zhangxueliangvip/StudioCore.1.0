layui.config({
    base: '/Content/lib/system/'
}).use(['jquery', 'element', 'layer', 'form', 'common'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form, element = layui.element, common = layui.common;



    common.$Enter("#btn_sub");

    $('#imgcode').click(function () {
        var newSrc = "/Users/GetCodeImg" + "?t=" + (new Date()).getTime();
        this.src = newSrc;
        return false;
    });

    form.on('submit(sub)', function (data) {
        var index = common.$loading();
        common.$disabled(data.elem);
        var url = data.form.action;
        var dt = data.field;
        $.post(url, dt, function (result) {
            layer.msg(result.Message, function () {
                common.$closeAll();
                common.$removedisabled("#btn_sub");
                if (result.Code === 0) {
                    $("#imgcode").click();
                    return false;
                }
                if (result.Code === 1) {
                    window.location.href = '/Home/Index';
                }
            });
        });

        return false;
    });

        $("#btn_reg").click(function () {//找回密码
        layer.msg('正在开发中……', { icon: 5 });
    });


});





