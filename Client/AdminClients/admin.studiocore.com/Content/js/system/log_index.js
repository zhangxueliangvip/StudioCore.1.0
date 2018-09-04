layui.config({
    base: '/Content/lib/system/'
}).use(['jquery', 'element', 'laydate', 'table', 'layer', 'form', 'common'], function () {
    var $ = layui.$, layer = layui.layer, form = layui.form, element = layui.element, common = layui.common, laydate = layui.laydate, table = layui.table;

    var pageTable = table.render({
        elem: '#pageData'
        , url: '/Home/GetPageDataLogs'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'displayID', width: 80, title: 'ID', sort: true, fixed: 'left' }
            , { field: 'displayLogTitle', title: '名称' }
            , { field: 'displayLogContents', title: '内容' }
            , { field: 'displayCTime', width: 160, title: '创建时间', fixed: 'right' }
            //, { field: 'displayOperation', title: '操作', sort: true, fixed: 'right' }
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
                    LogTitle: $('#LogTitle').val(),
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


});
