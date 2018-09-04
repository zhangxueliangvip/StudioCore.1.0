layui.config({
    base: '/Content/lib/system/'
}).use(['jquery', 'element', 'layer', 'form', 'common'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form, element = layui.element, common = layui.common;
    form.on('checkbox(Title)', function (data) {//角色
        $(this).parent().parent().children().find(".layui-form-checkbox").each(function () {
            $(this).removeClass("layui-form-checked");
        });
        if (data.elem.checked) {
            $(this).parent().parent().children().find(".layui-form-checkbox").each(function () {
                $(this).addClass("layui-form-checked");
            });
        }

    });

    $("#btn_submit").click(function () {
        var data = {
            menuArray: [],
            RoleArray: []
        };
        $("input[name='Name']").each(function () {
            var nextel = $(this).next();
            if ($(nextel).hasClass("layui-form-checked")) {
                data.RoleArray.push($(this).val());
            };

        });
        $(" input[name='Title']").each(function () {
            var MenuData = {
                menuTitle: '',
                menuOpenId: '',
                PageArray: []
            };

            var nextel = $(this).next();
            if ($(nextel).hasClass("layui-form-checked")) {
                MenuData.menuTitle = $(this).attr("Title");
                MenuData.menuOpenId = $(this).val();
                $(this).parent().parent().children().find("input[name='childTitle']").each(function () {
                    var PageData = {
                        PageTitle: '',
                        PageOpenId: '',
                        OperationArray: []
                    };
                    var nextel = $(this).next();
                    if ($(nextel).hasClass("layui-form-checked")) {
                        PageData.PageTitle = $(this).attr("Title");
                        PageData.PageOpenId = $(this).val();
                        $(this).parent().parent().children().find("input[name='OperationType']").each(function () {
                            var OperationData = {
                                OperationTitle: '',
                                OperationOpenId: ''
                            };
                            var nextel = $(this).next();
                            if ($(nextel).hasClass("layui-form-checked")) {
                                OperationData.OperationTitle = $(this).attr("Title");
                                OperationData.OperationOpenId = $(this).val();
                                PageData.OperationArray.push(OperationData);
                            }

                        });

                        MenuData.PageArray.push(PageData);

                    }


                });

                data.menuArray.push(MenuData);
            }
        });
        var url = '/Home/SetPllocationUsersCheck';
        //$.ajaxSettings.async = false;
        $.post(url, data, function (result) {
            layer.msg(result.Message, function () {
                if (result.Code === 1) {
                    window.parent.layer.closeAll();
                }
            });
        });
    });

    
});









