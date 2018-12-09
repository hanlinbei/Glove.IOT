layui.config({
    version: false //一般用于更新模块缓存，默认不开启。设为true即让浏览器不缓存。也可以设为一个固定的值，如：201610
    ,debug: false //用于开启调试模式，默认false，如果设为true，则JS模块的节点会保留在页面
    ,base: '' //设定扩展的Layui模块的所在目录，一般用于外部模块扩展
});
layui.use('element',function () {
    //实例化element
    var element = layui.element;
    //初始化动态元素，一些动态生成的元素如果不设置初始化，将不会有默认的动态效果
    element.init();

    element.on('nav(sidebar)',function (elem) {
        //这边写点击后的动作
    });

})
/*$(document).ready(function () {
    $(".tck-close").click(function () {
        $(".tck").hide();
    });
});*/
function tck_show(title, url, w, h, data) {
    console.log(data);
    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shadeClose: true,
        shade: 0.4,
        title: title,
        content: url,
        btn: ['确定'],
        yes: function(index){

            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
            //获取iframe页面
            var body = layer.getChildFrame('body', index);
            $(body).find("input").eq(0).val(data.UName);//读取父页面的姓名
            $(body).find("input").eq(1).val(data.UCode);//读取父页面的角色编码
            switch (data.Remark) {//读取父页面的角色类型
                case "超级管理员":
                    $(body).find("select").val(2);
                    break;
                case "组长":
                    $(body).find("select").val(1);
                    break;
                case "操作工":
                    $(body).find("select").val(0);
                    break;
                default:
                    $(body).find("select").val(0);
                    break;
            }
            $(body).find("textarea").val(data.Pwd);//读取父页面的描述 
            if (data.DelFlag === 0) {//读取父页面的角色状态
                $(body).find("input").eq(3).attr('checked', false);
                $(body).find("input").eq(4).attr('checked', true);
            }
            else {
                $(body).find("input").eq(3).attr('checked', true);
                $(body).find("input").eq(4).attr('checked', false);
            }
            //获取新窗口对象
            var iframeWindow = layero.find('iframe')[0].contentWindow;
            //重新渲染
            iframeWindow.layui.form.render();
        }
    });
}

function tck_show1(title, url, w, h, data) {
    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shadeClose: true,
        shade: 0.4,
        title: title,
        content: url,
        btn: ['确定'],
        yes: function (index) {
            //当点击‘确定’按钮的时候，获取弹出层返回的值
            var res = window["layui-layer-iframe" + index].callbackdata(index);
            //打印返回的值，看是否有我们想返回的值。
            console.log(res);
            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
            //获取iframe页面
            //var body = layer.getChildFrame('body', index);
            //获取新窗口对象
            //var iframeWindow = layero.find('iframe')[0].contentWindow;
            //重新渲染
            //iframeWindow.layui.form.render();
        }
    });
}
function callbackdata(index) {
    var data = {
        UName: $(".Username").val(),
        UCode: $(".Usercode").val(),
        Remark: $(".Userremark").val(),
        Pwd: $(".Userpwd").val(),
        DelFlag: $(".Userdelflag").val()
    }
    return data;
}
$(document).ready(function () {
    $(".w").click(function () {
        tck_show1('员工编辑', 'tck_ry_tj', 500, 450, "null");
    });

});