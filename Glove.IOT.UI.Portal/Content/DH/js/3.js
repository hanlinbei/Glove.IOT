var num_p;
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
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_ry'
        , height: 550
        , url: '/UserInfo/GetAllUserInfos' //数据接口
        , title: "人员管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            , { field: 'Id', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'UCode', title: '工号', minWidth: 100, align: 'center' }
            , { field: 'UName', title: '姓名', minWidth: 100, sort: true, align: 'center' }
            , { field: 'RoleName', title: '角色名', minWidth: 200, align: 'center' }
            , { field: 'StatusFlag', title: '角色状态', minWidth: 100, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 150, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            console.log(res);
            console.log(curr);
            console.log(count);
            num_p = count;
        }
        , skin: 'line'
    });
    table.on('tool(table_ry)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象

        if (layEvent === 'detail') { //查看
            alert("查看");
            $("#tck").show();
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除？', function (index) {
                layer.close(index);
                //向服务端发送删除指令
                ids = "" + data.Id;
                $.post("/UserInfo/Delete", { ids: ids });//发送字符串
                obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                num_p = num_p - 1;
                //表格重载
                //updatatable('#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理");
            });
        } else if (layEvent === 'edit') { //编辑
            // $(".tck").show();
            tck_show_ry_bj('员工编辑', 'tck_ry_bj', 500, 450, obj.data);
            //同步更新缓存对应的值
            /*obj.update({
                UName: '123'
                , title: 'xxx'
            });*/
        }
    });
    table.on('checkbox(table_ry)', function (obj) {
        console.log(obj.checked); //当前是否选中状态
        console.log(obj.data); //选中行的相关数据
        console.log(obj.type); //如果触发的是全选，则为：all，如果触发的是单选，则为：one
    });
});
function tck_show_ry_bj(title, url, w, h, data) {
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
            //ajax发送post请求 给后端发送数据
            console.log(res);
            var xhr = new XMLHttpRequest();
            xhr.open('POST', "/UserInfo/Edit");
            xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
            xhr.send('UName=' + res.UName
                + '&UCode=' + res.UCode
                + '&Remark=' + res.Remark
                + '&Pwd=' + res.Pwd
                + '&StatusFlag=' + res.StatusFlag
                + '&Id=' + data.Id);//多发一个id数据
            //xhr.send(`UName=${res.UName}&UCode=${res.UName}&Remark=${res.Remark}&Pwd=${res.Pwd}&StatusFlag=${res.StatusFlag}`)//反单引号 模板字符串
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                console.log(this.responseText);
            }
            //表格重载
            updatatable('#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理");
            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
            //获取iframe页面
            var body = layer.getChildFrame('body', index);
            $(body).find("input").eq(0).val(data.UName);//读取父页面的姓名
            $(body).find("input").eq(1).val(data.UCode);//读取父页面的角色编码
            switch (data.RoleName) {//读取父页面的角色类型
                case "超级管理员":
                    $(body).find("select").val("超级管理员");
                    break;
                case "组长":
                    $(body).find("select").val("组长");
                    break;
                case "操作工":
                    $(body).find("select").val("操作工");
                    break;
                default:
                    $(body).find("select").val("操作工");
                    break;
            }
            $(body).find("textarea").val(data.Pwd);//读取父页面的描述 
            if (data.StatusFlag === 0) {//读取父页面的角色状态
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

function tck_show_ry_tj(title, url, w, h, data) {
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
        yes: function (index) {
            //当点击‘确定’按钮的时候，获取弹出层返回的值
            var res = window["layui-layer-iframe" + index].callbackdata(index);
            //ajax发送post请求 给后端发送数据
            console.log(res);
            var xhr = new XMLHttpRequest();
            xhr.open('POST', "/UserInfo/Add");
            xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');         
            xhr.send('UName=' + res.UName
                + '&UCode=' + res.UCode
                + '&Remark=' + res.Remark
                + '&Pwd=' + res.Pwd
                + '&StatusFlag=' + res.StatusFlag);
            //xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
            //xhr.send(`UName=${res.UName}&UCode=${res.UName}&Remark=${res.Remark}&Pwd=${res.Pwd}&StatusFlag=${res.StatusFlag}`)//反单引号 模板字符串
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                console.log(this.responseText);
            }
            //表格重载
            updatatable('#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理", );
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
function callbackdata(index) {//获取弹窗用户输入的数据
    var data = {
        UName: $('input[name="UName"]').val(),
        UCode: $('input[name="UCode"]').val(),
        Remark: $('select[name="Remark"] option:selected').val(),
        Pwd: $('textarea[name="Pwd"]').val(),
        StatusFlag: $('input[name="StatusFlag"]:checked').val()
    }
    return data;
}

function updatatable(elem, height, url, title, page, limit) {//表格重载
    var table = layui.table;
    table.render({
        elem: elem
        , height: height
        , url: url//数据接口
        , title: title
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , request: {
            pageName: page //页码的参数名称，默认：page
            , limitName: limit //每页数据量的参数名，默认：limit
        }
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            , { field: 'Id', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'UCode', title: '角色编码', minWidth: 100, align: 'center' }
            , { field: 'UName', title: '姓名', minWidth: 100, sort: true, align: 'center' }
            , { field: 'Remark', title: '角色名', minWidth: 200, align: 'center' }
            , { field: 'StatusFlag', title: '角色状态', minWidth: 100, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 150, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            console.log(res);
            console.log(curr);
            console.log(count);
            num_p = count;
        }
        , skin: 'line'
    });
}
$(document).ready(function () {
    $("button[name='添加人员']").click(function () {
        tck_show_ry_tj('员工编辑', 'tck_ry_tj', 500, 450, "null");
    });

});