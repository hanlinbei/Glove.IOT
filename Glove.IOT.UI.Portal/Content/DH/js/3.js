var num_p;
var num_d;
var globalPage;
var globalLimit;
var delId = "";//批量删除时给后台发的字符串存储变量
var UIdtable = new Array();//保存当前表格内数据是否被选中 在批量删除中使用
var DIdtable = new Array();//保存当前表格内数据是否被选中 在批量删除中使用
var Rid_Rolename = new Array();
var RId = 0;
layui.config({
    version: false //一般用于更新模块缓存，默认不开启。设为true即让浏览器不缓存。也可以设为一个固定的值，如：201610
    ,debug: false //用于开启调试模式，默认false，如果设为true，则JS模块的节点会保留在页面
    ,base: '' //设定扩展的Layui模块的所在目录，一般用于外部模块扩展
});
//人员相关
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_ry'
        //, height: 520
        , url: '/UserInfo/GetAllUserInfos' //数据接口
        , title: "人员管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            //, { field: 'DeviceId', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'UCode', title: '角色编码', minWidth: 80, align: 'center' }
            , { field: 'UName', title: '姓名', minWidth: 80, sort: true, align: 'center' }
            , { field: 'RoleName', title: '角色名', minWidth: 150, align: 'center' }
            , { field: 'StatusFlag', title: '角色状态', minWidth: 80, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i = 0; i < res.data.length; i++) {//把状态数据用中文表示
                if (res.data[i].StatusFlag === 0)
                    res.data[i].StatusFlag = "无效";
                else if (res.data[i].StatusFlag === 1)
                    res.data[i].StatusFlag = "有效";
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
        , toolbar: true
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            UIdtable = [];//清空
            if (curr === Math.floor(count / globalLimit) + 1) {
                var length = (count % globalLimit === 0 ? globalLimit : count % globalLimit);
            }
            else {
                var length = globalLimit;
            }
            for (var i = 0; i < length; i++) {
                UIdtable[i] = [res.data[i].UId, 0];
            }
            console.log(UIdtable);
            num_p = count;
        }
        , skin: 'line'

    });
    table.on('tool(table_ry)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象

        if (layEvent === 'detail') { //查看
            
        } else if (layEvent === 'del') { //删除
            console.log(data);
            layer.confirm('确定删除？', function (index) {
                layer.close(index);
                //向服务端发送删除指令
                ids = "" + data.UId;
                $.post("/UserInfo/Delete", { ids: ids });//发送字符串
                //obj.del(); //删除对应行（tr）的DOM结构，并更新缓存 这是不刷新页面的方式
                num_p = num_p - 1;
                globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                if (num_p % globalLimit === 0) globalPage -= 1;//超过分页值 页码加1
                //表格重载
                updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理", globalPage, globalLimit);
            });
        } else if (layEvent === 'edit') { //编辑
            layerShowEdituser('编辑人员', 'LayerEdituser', 500, 450, obj.data);
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
        if (obj.type === "all") {
            if (obj.checked === true) {
                for (var i = 0; i < UIdtable.length; i++) {
                    UIdtable[i][1] = 1;
                }
            }
            else {
                for (var i = 0; i < UIdtable.length; i++) {
                    UIdtable[i][1] = 0;
                }
            }
        }
        else if (obj.checked === true) {
            for (var i = 0; i < UIdtable.length; i++) {
                if (UIdtable[i][0] === obj.data.UId) {
                    UIdtable[i][1] = 1;
                    break;
                }
            }
        }
        else if (obj.checked === false) {
            for (var i = 0; i < UIdtable.length; i++) {
                if (UIdtable[i][0] === obj.data.UId) {
                    UIdtable[i][1] = 0;
                    break;
                }
            }
        }
    });
});
function layerShowEdituser(title, url, w, h, data) {
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
            for (var i = 0; i < Rid_Rolename.length; i++) {
                if (Rid_Rolename[i][1] === res.RoleName) {
                    RId = Rid_Rolename[i][0];
                    break;
                }
            } 
            //ajax发送post请求 给后端发送数据
            var xhr = new XMLHttpRequest();
            xhr.open('POST', "/UserInfo/Edit");
            xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
            xhr.send('UName=' + res.UName
                + '&UCode=' + res.UCode
                + '&Pwd=' + res.Pwd
                + '&RId=' + RId
                + '&Remark=' + res.Remark
                + '&StatusFlag=' + res.StatusFlag
                + '&Id=' + data.UId);//多发一个id数据
            //xhr.send(`UName=${res.UName}&UCode=${res.UName}&Remark=${res.Remark}&Pwd=${res.Pwd}&DelFlag=${res.DelFlag}`)//反单引号 模板字符串
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                console.log(this.responseText);
            }
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            //表格重载
            updatatable('table_ry', '#table_ry', 550, "/UserInfo/GetAllUserInfos", "人员管理", globalPage, globalLimit);
            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
            //获取iframe页面
            console.log(data);
            var body = layer.getChildFrame('body', index);
            $(body).find('input[name="UName"]').attr("value", data.UName);//输入父页面的姓名
            $(body).find('input[name="UCode"]').attr("value", data.UCode);//输入父页面的角色编码
            switch (data.RoleName) {//输入父页面的角色类型
                case "超级管理员":
                    $(body).find('select[name="RoleName"]').val("超级管理员");
                    break;
                case "admin2":
                    $(body).find('select[name="RoleName"]').val("admin2");
                    break;
                case "admin3":
                    $(body).find('select[name="RoleName"]').val("admin3");
                    break;
                default:
                    $(body).find('select[name="RoleName"]').val("这是静态的");
                    break;
            }
            $(body).find('textarea[name="Remark"]').val(data.Remark);//输入父页面的描述 
            if (data.StatusFlag === 0) {//输入父页面的角色状态
                $(body).find('input[title="有效"]').attr('checked', false);
                $(body).find('input[title="无效"]').attr('checked', true);
            }
            else {
                $(body).find('input[title="有效"]').attr('checked', true);
                $(body).find('input[title="无效"]').attr('checked', false);
            }
            //获取新窗口对象
            var iframeWindow = layero.find('iframe')[0].contentWindow;
            //重新渲染
            iframeWindow.layui.form.render();
            var xhr = new XMLHttpRequest();
            xhr.open('GET', "/UserInfo/GetAllRoles");
            xhr.send();
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                var obj = eval("(" + this.responseText + ")");//JSON.parse安全
                for (var i = 0; i < obj.length; i++) {//保存查询用
                    console.log(obj[i].Id + "+" + obj[i].RoleName)
                    Rid_Rolename[i] = [obj[i].Id, obj[i].RoleName];
                }
            }
        }
    });
}
function layerShowAdduser(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'adduser');
            //ajax发送post请求 给后端发送数据
            for (var i = 0; i < Rid_Rolename.length; i++) {
                if (Rid_Rolename[i][1] === res.RoleName) {
                    RId = Rid_Rolename[i][0];
                    break;
                }
            }     
            var xhr = new XMLHttpRequest();
            xhr.open('POST', "/UserInfo/Add");
            xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');         
            xhr.send('UName=' + res.UName
                + '&UCode=' + res.UCode
                + '&Pwd=' + res.Pwd
                + '&RId=' + RId
                + '&Remark=' + res.Remark
                + '&StatusFlag=' + res.StatusFlag);
            //xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
            //xhr.send(`UName=${res.UName}&UCode=${res.UName}&Remark=${res.Remark}&Pwd=${res.Pwd}&DelFlag=${res.DelFlag}`)//反单引号 模板字符串
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                console.log(this.responseText);
            }
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            globalPage = Math.ceil(num_p / globalLimit);//获取页码值
            if (num_p % globalLimit === 0) globalPage += 1;//超过分页值 页码加1
            updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理", globalPage, globalLimit);
            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', "/UserInfo/GetAllRoles");
            xhr.send();
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                var obj = eval("(" + this.responseText + ")");//JSON.parse安全
                for (var i = 0; i < obj.length; i++) {//保存查询用
                    console.log(obj[i].Id + "+" + obj[i].RoleName)
                    Rid_Rolename[i] = [obj[i].Id, obj[i].RoleName];
                }
            }
            //获取iframe页面
            //var body = layer.getChildFrame('body', index);
            //获取新窗口对象
            //var iframeWindow = layero.find('iframe')[0].contentWindow;
            //重新渲染
            //iframeWindow.layui.form.render();
        }
    });
}
function layerShowSearchuser(title, url, w, h, data) {
    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shadeClose: true,
        shade: 0.4,
        title: title,
        content: url,
        btn: ['查找'],
        yes: function (index) {
            //当点击‘确定’按钮的时候，获取弹出层返回的值
            var res = window["layui-layer-iframe" + index].callbackdata_search(index);
            console.log("搜索" + res);
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理", 1, globalLimit, res);
            //最后关闭弹出层
            layer.close(index);
        },
        skin: 'demo-class'
    });
}
function callbackdata(index, retrieval) {//获取弹窗用户输入的数据
    switch (retrieval) {
        case 'adduser':
            var data = {
                UName: $('input[name="UName"]').val(),
                UCode: $('input[name="UCode"]').val(),
                Pwd: $('input[name="Pwd"]').val(),
                RoleName: $('select[name="RoleName"] option:selected').val(),
                Remark: $('textarea[name="Remark"]').val(),
                StatusFlag: $('input[name^="StatusFlag"]:checked').val()//前缀为StatusFlag
            }
            break;
        case 'adddevice':
            var data = {
                DeviceId: $('input[name="DeviceId"]').val()
            }
            break;

    }
    return data;
}
function callbackdata_search(index) {//获取弹窗用户输入的数据
    var data = {
        UCode: $('input[name="UCode"]').val(),
        RoleName: $('select[name="RoleName"] option:selected').val(),
    }
    return data;
}
function someDel(assort) {
    delId = "";//清空
    if (assort === 'user') {
        for (var i = 0; i < UIdtable.length; i++) {
            if (UIdtable[i][1] === 1) {
                delId += UIdtable[i][0];
                delId += ",";
            }
        }
        delId = delId.slice(0, delId.length - 1);
        console.log(delId);
        layer.confirm('确定删除？', function (index) {
            layer.close(index);
            $.post("/UserInfo/Delete", { ids: delId });//发送字符串
            //表格重载
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "人员管理", 1, globalLimit);
        });
    }
    else if (assort === 'device') {
        for (var i = 0; i < DIdtable.length; i++) {
            if (DIdtable[i][1] === 1) {
                delId += DIdtable[i][0];
                delId += ",";
            }
        }
        delId = delId.slice(0, delId.length - 1);
        console.log(delId);
        layer.confirm('确定删除？', function (index) {
            layer.close(index);
            $.post("/Device/Delete", { ids: delId });//发送字符串
            //表格重载
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "人员管理", 1, globalLimit);
        });
    }
}
function updatatable_search(elem, height, url, title, page, limit, res) {//表格重载 跳转到操作页面
    var table = layui.table;
    table.reload('table_ry', {
        elem: elem
        , height: height
        , url: url//数据接口
        , title: title
        , page: {
            curr: page
        }//重新制定page和limit
        , limit: limit
        , where: { SchCode: res.UCode, SchRoleName: res.RoleName }
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            console.log("表格渲染完成");
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            UIdtable = [];//清空数组
            for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                UIdtable[i] = [res.data[i].UId, 0];
            }
            num_p = count;
        }
    });
}
function updatatable(id, elem, height, url, title, page, limit) {//表格重载 跳转到操作页面
    var table = layui.table;
    table.reload( id, {
        elem: elem
        , height: height
        , url: url//数据接口
        , title: title
        , page: {
            curr: page
        }//重新制定page和limit
        , limit: limit
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            console.log("表格渲染完成");
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            if (id === 'table_ry') {
                UIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    UIdtable[i] = [res.data[i].UId, 0];
                }
                num_p = count;
            }
            else if (id === 'table_device'){
                DIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    DIdtable[i] = [res.data[i].DeviceId, 0];
                }
                num_d = count;
            }
        }
    });
}
function getRolename() {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', "/UserInfo/GetAllRoles");
    xhr.send();//多发一个id数据
    //xhr.send(`UName=${res.UName}&UCode=${res.UName}&Remark=${res.Remark}&Pwd=${res.Pwd}&StatusFlag=${res.StatusFlag}`)//反单引号 模板字符串
    xhr.onreadystatechange = function () {
        if (this.readyState !== 4) return;
        var obj = eval("(" + this.responseText + ")");//JSON.parse安全
        //var body = layer.getChildFrame('body', index);
        for (var i = 0; i < obj.length; i++) {
            var e = $('<option value="' + obj[i].RoleName + '">' + obj[i].RoleName + '</option>');
            //$(body).find('select[name="RoleName"]').append(e);
            $('select[name="RoleName"]').append(e);
        }
        layui.use('form', function () {
            var form = layui.form;
            form.render('select');
        });
    }
}




///////////////////////////////////////////////////////////////////////////////////////////////////
//设备相关
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_device'
        , height: 500
        , url: '/Device/GetAllDeviceInfos' //数据接口
        , title: "设备管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            //, { field: 'DeviceId', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'DeviceId', title: '设备ID', minWidth: 80, align: 'center' }
            , { field: 'StatusFlag', title: '运行状态', minWidth: 80, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i = 0; i < res.data.length; i++) {//把状态数据用中文表示
                if (res.data[i].StatusFlag === 0)
                    res.data[i].StatusFlag = "关机中";
                else if (res.data[i].StatusFlag === 1)
                    res.data[i].StatusFlag = "运行中";
                else if (res.data[i].StatusFlag === 2)
                    res.data[i].StatusFlag = "暂停中";
                else if (res.data[i].StatusFlag === 3)
                    res.data[i].StatusFlag = "故障中";
                else if (res.data[i].StatusFlag === 4)
                    res.data[i].StatusFlag = "未连接";
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
        , toolbar: true
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            DIdtable = [];//清空
            if (curr === Math.floor(count / globalLimit) + 1) {
                var length = (count % globalLimit === 0 ? globalLimit : count % globalLimit);
            }
            else {
                var length = globalLimit;
            }
            for (var i = 0; i < length; i++) {
                DIdtable[i] = [res.data[i].Id, 0];
            }
            num_d = count;
        }
        , skin: 'line'

    });
    table.on('tool(table_device)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        if (layEvent === 'detail') { //查看
            console.log("点击了查看");
            window.location.href = 'Devicedetail?DeviceId=' + data.DeviceId;
        } else if (layEvent === 'del') { //删除
            console.log(data);
            layer.confirm('确定删除？', function (index) {
                layer.close(index);
                //向服务端发送删除指令
                ids = "" + data.Id;
                $.post("/Device/Delete", { ids: ids });//发送字符串
                //obj.del(); //删除对应行（tr）的DOM结构，并更新缓存 这是不刷新页面的方式
                num_d = num_d - 1;
                globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                globalPage = Math.ceil(num_d / globalLimit);//获取页码值
                if (num_d % globalLimit === 0) globalPage -= 1;//超过分页值 页码加1
                //表格重载
                updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "设备管理", globalPage, globalLimit);
            });
        }
    });
    table.on('checkbox(table_device)', function (obj) {
        console.log(obj.checked); //当前是否选中状态
        console.log(obj.data); //选中行的相关数据
        console.log(obj.type); //如果触发的是全选，则为：all，如果触发的是单选，则为：one
        if (obj.type === "all") {
            if (obj.checked === true) {
                for (var i = 0; i < DIdtable.length; i++) {
                    DIdtable[i][1] = 1;
                }
            }
            else {
                for (var i = 0; i < DIdtable.length; i++) {
                    DIdtable[i][1] = 0;
                }
            }
        }
        else if (obj.checked === true) {
            for (var i = 0; i < DIdtable.length; i++) {
                if (DIdtable[i][0] === obj.data.Id) {
                    DIdtable[i][1] = 1;
                    console.log(DIdtable[i][0]);
                    break;
                }
            }
        }
        else if (obj.checked === false) {
            for (var i = 0; i < DIdtable.length; i++) {
                if (DIdtable[i][0] === obj.data.Id) {
                    DIdtable[i][1] = 0;
                    break;
                }
            }
        }
    });
});
function layerShowAdddevice(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'adddevice');
            //ajax发送post请求 给后端发送数据
            var xhr = new XMLHttpRequest();
            xhr.open('POST', "/Device/Add");
            xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
            xhr.send('DeviceId=' + res.DeviceId);//多发一个id数据
            xhr.onreadystatechange = function () {
                if (this.readyState !== 4) return;
                console.log(this.responseText);
                if (this.responseText === "fail") {
                    layui.use('layer', function () {//弹窗
                        var layer = layui.layer;
                        layer.msg('<span style = "font-size: 20px; line-height:90px; vertical-align:middle;">ID已存在</span>', {
                            time: 2000,
                            area:['200px',  '120px']
                        });
                    });
                }
                else{
                    //表格重载 跳转到操作页面
                    globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                    globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                    if (num_p % globalLimit === 0) globalPage += 1;//超过分页值 页码加1
                    updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "人员管理", globalPage, globalLimit);
                }
            };   
            //最后关闭弹出层
            layer.close(index);
        }
    });
}
function layerShowSearchdevice(title, url, w, h, data) {
    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: false, //不固定
        maxmin: true,
        shadeClose: true,
        shade: 0.4,
        title: title,
        content: url,
        btn: ['查找'],
        yes: function (index) {
            //当点击‘确定’按钮的时候，获取弹出层返回的值
            var res = window["layui-layer-iframe" + index].callbackdata_search(index);
            console.log("搜索" + res);
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('#table_ry', 550, '/Device/GetAllDeviceInfos', "设备管理", 1, globalLimit, res);
            //最后关闭弹出层
            layer.close(index);
        },
        skin: 'demo-class'
    });
}
$(document).ready(function () {
    $("button[name='添加人员']").click(function () {
        layerShowAdduser('添加人员', 'LayerAdduser', 500, 450, "null");
    });
    $("button[name='删除人员']").click(function () {
        someDel('user');
    });
    $("button[name='查找人员']").click(function () {
        layerShowSearchuser('查找人员', 'LayerSearchuser', 500, 380, "null");
    });
    $("button[name='添加设备']").click(function () {
        layerShowAdddevice('添加设备', 'LayerAdddevice', 500, 200, "null");
    });
    $("button[name='删除设备']").click(function () {
        someDel('device');
    });
    $("button[name='查找设备']").click(function () {
        layerShowSearchdevice('查找设备', 'LayerSearchdevice', 500, 380, "null");
    });

});