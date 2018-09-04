layui.config({
    base: '/Content/lib/system/'
}).use(['jquery', 'element' , 'laydate','table','layer', 'form', 'common'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form, element = layui.element, common = layui.common, laydate = layui.laydate, table = layui.table;

    var pageTable = table.render({
        elem: '#pageData'
        , url: '/Menu/GetPageDataIcons'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'displayID', width: 80, title: 'ID', sort: true, fixed: 'left' }
            , { field: 'displayIconName', title: '名称' }
            , { field: 'displayIconClass', title: '类名' }
            , { field: 'displayCTime', title: '创建时间' }
            , { field: 'displayOperation', title: '操作', sort: true, fixed: 'right' }
        ]], id: 'QuerySearch',
        page: true
    });

    var active = {
        reload: function () {
            table.reload('QuerySearch', {
                page: {
                    curr: 1
                }
                , where: {
                    IconName: $('#IconName').val(),
                    StartCTime: $("#StartCTime").val(),
                    EndCTime: $("#EndCTime").val()
                }
            });
        }
    };

    $('#btnSearch').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    laydate.render({
        elem: '#StartCTime'
    });
    laydate.render({
        elem: '#EndCTime'
    });


    $("#AddData").click(function () {
        layer.open({
            type: 2,
            title: '添加记录',
            shadeClose: true,
            shade: false,
            maxmin: true,
            area: ['560px', '360px'],
            content: '/Menu/AddIconsDataBox'
        });

    });

    });


function Updated(openId) {
    layer.open({
        type: 2,
        title: '修改记录',
        shadeClose: true,
        shade: false,
        maxmin: true,
        area: ['560px', '360px'],
        content: '/Menu/AddIconsDataBox?openId=' + openId
    });
}
function Deleted(openId) {
    layer.confirm('确定要删除？', {
        btn: ['确定', '取消'] //按钮
    }, function () {
        var url = "/Menu/DeleteDataIcons";
        var data = {};
        data.openId = openId;
        $.post(url, data, function (result) {
            layer.msg(result.Message, function () {
                if (result.Code === 1) {
                    location.reload();
                }
            });
        });
    }, function () {
        layer.closeAll();
    });
}