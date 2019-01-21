var num_p;
var num_d;
var globalPage;
var globalLimit;
var UIdtable = new Array();//保存当前表格内数据是否被选中 在批量删除中使用
var DIdtable = new Array();//保存当前表格内数据是否被选中 在批量删除中使用
var CIdtable = new Array();//保存当前表格内数据是否被选中 在批量删除中使用
var Rid_Rolename = new Array();//保存UId 编辑的时候用
var tId_Teamname = new Array();
var gId_Groupname = new Array();
var RId = 0;
var tId = 0;
var gId = 0;
var userDetail = new FormData();
//var userDetail;
layui.config({
    version: false //一般用于更新模块缓存，默认不开启。设为true即让浏览器不缓存。也可以设为一个固定的值，如：201610
    ,debug: false //用于开启调试模式，默认false，如果设为true，则JS模块的节点会保留在页面
    ,base: '' //设定扩展的Layui模块的所在目录，一般用于外部模块扩展
});
//记住密码
function getCookie(data) {
    var name = data + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
}
//登录
function send() {
    if ($('input[name="LoginCode"]').val() === "") {
        //提示错误
        $('input[name="LoginCode"]').attr('placeholder', '请输入用户名');
        $('input[name="LoginCode"]').addClass("red");
    }
    else if ($('input[name="LoginPwd"]').val() === "") {
        //提示错误
        $('input[name="LoginPwd"]').attr('placeholder', '请输入密码');
        $('input[name="LoginPwd"]').addClass("red");
    } 
    else if ($('input[name="vCode"]').val() === "") {
        //提示错误
        $('input[name="vCode"]').attr('placeholder', '请输入验证码');
        $('input[name="vCode"]').addClass("red");
    }
    else {
        $.post("/UserLogin/ProcessLogin", {
            LoginCode: $("input[name='LoginCode']").val(),
            LoginPwd: $("input[name='LoginPwd']").val(),
            vCode: $("input[name='vCode']").val()
        }, function (data) {
            if (data === 'OK') {
                document.cookie = 'LoginCode=;';//删除
                document.cookie = 'LoginPwd=;';
                if (document.getElementById("rePwd").checked) {
                    var date = new Date();
                    date.setTime(date.getTime() + (1 * 60 * 60 * 1000));
                    document.cookie = 'LoginCode=' + $("input[name='LoginCode']").val() + ';' + "expires=" + date.toGMTString();
                    document.cookie = 'LoginPwd=' + $("input[name='LoginPwd']").val() + ';' + "expires=" + date.toGMTString();
                    name = $("input[name='LoginCode']").val();
                    console.log(name);
                } else {
                    var date = new Date();
                    date.setTime(date.getTime() + (1 * 60 * 60 * 1000));
                    document.cookie = 'LoginCode=' + $("input[name='LoginCode']").val() + ';' + "expires=" + date.toGMTString();
                }
                window.location.href = '../Home/Home';
            }
            else if (data === '验证码错误!') {
                //提示错误
                $(".vcodeerror").show();
                changeCheckCode();
            }
            else if (data === '用户名密码错误!') {
                //提示错误
                $(".pwderror").show();
                changeCheckCode();
            }
            else if (data === '用户状态异常!') {
                //提示错误
                $(".nameerror").show();
                changeCheckCode();
            }
        })
    }
    //var xhr = new XMLHttpRequest();
    //xhr.open('POST', "/UserLogin/ProcessLogin");
    //xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
    //xhr.send('LoginCode=' + $("input[name='LoginCode']").val()
    //    + '&LoginPwd=' + $("input[name='LoginPwd']").val()
    //    + '&vCode=' + $("input[name='vCode']").val());
    //xhr.onreadystatechange = function () {
    //    if (this.readyState !== 4) return;
    //    if (this.responseText === 'OK') {
    //        window.location.href = '../Device/Devicemanage';
    //    }
    //    else if (this.responseText === '验证码错误！') {
    //        alert("验证码错误!");
    //        changeCheckCode();
    //    }
    //    else if (this.responseText === '用户名密码错误！') {
    //        alert("用户名密码错误！");
    //        changeCheckCode();
    //    }
    //}
}
//注销
function logout() {
    $.post('/UserLogin/Logout', {}, function (data) {
        if (data === 'ok') {
            //清除cookie
            var date = new Date();
            date.setTime(date.getTime() - 1);
            var keys = document.cookie.match(/[^ =;]+(?=\=)/g);
            if (keys) {
                for (var i = keys.length; i--;)
                    document.cookie = keys[i] + "=; expire=" + date.toGMTString() + "; path=/UserLogin";
            }
            //document.cookie = 'LoginCode=;';//删除
            //document.cookie = 'LoginPwd=;';
            window.location.href = '../UserLogin/Index';
        }
    })
}
//显示左上角的用户信息
function userMessage() {
    $.get("/UserInfo/GetUserPicture", {}, function (data) {
        if (data.Picture !== null) {
            $('p[name="userName"]').html(data.UCode);
            //$('img[name="tPicture"]').prop('src', data.Picture);
            $('#tPicture').html('<img src=" '+ data.Picture + ' " name="tPicture" height="50" width="50" />');
        }
        else {
            $('p[name="userName"]').html(data.UCode);
            $('#tPicture').html('<img src=" ' + "../Content/DH/img/人物.png" + ' " name="tPicture" height="50" width="50" />');
            //$('img[name="tPicture"]').prop('src', "../Content/DH/img/人物.png");
        }
    })
}
function changeCheckCode() {
    var old = $("#imgCode").attr("src");
    var now = new Date();
    var str = old +
        now.getDay() +
        now.getSeconds() +
        now.getMinutes();
    $("#imgCode").attr("src", str);
}     
//员工相关
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_ry'
        //, height: 520
        , url: '/UserInfo/GetAllUserInfos' //数据接口
        , title: "员工管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            //, { field: 'DeviceId', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'UCode', title: '员工编码', minWidth: 80, align: 'center' }
            , { field: 'UName', title: '姓名', minWidth: 80, sort: true, align: 'center' }
            , { field: 'RoleName', title: '角色名', minWidth: 150, align: 'center' }
            , { field: 'StatusFlag', title: '角色状态', minWidth: 80, align: 'center' }
            , { field: 'TName', title: '班号', minWidth: 80, align: 'center' }
            , { field: 'GName', title: '组号', minWidth: 80, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , parseData: function (res) { //res 即为原始返回的数据
            for (var i = 0; i < res.data.length; i++) {//把状态数据用中文表示
                if (res.data[i].StatusFlag === false)
                    res.data[i].StatusFlag = "无效";
                else if (res.data[i].StatusFlag === true)
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
            Power('user');
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
        if (layEvent === 'del') { //删除
            layer.confirm('确定删除？', function (index) {
                layer.close(index);
                //向服务端发送删除指令
                ids = "" + data.UId;
                $.post("/UserInfo/Delete", { ids: ids }, function (data) {
                num_p = num_p - 1;
                globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                if (num_p % globalLimit === 0) globalPage -= 1;//超过分页值 页码加1
                //表格重载
                updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "员工管理", globalPage, globalLimit);
                })
            });
        } else if (layEvent === 'edit') { //编辑 
            layerShowEdituser('编辑员工', 'LayerEdituser', 500, 450, obj.data);
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'adduser');
            var body = layer.getChildFrame('body', index);
            if (res.UName === "") {
                $(body).find('input[name="UName"]').attr('placeholder', '员工名字不能为空');
                $(body).find('input[name="UName"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.UCode === "") {
                $(body).find('input[name="UCode"]').attr('placeholder', '员工编码不能为空');
                $(body).find('input[name="UCode"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.Pwd === "") {
                $(body).find('input[name="Pwd"]').attr('placeholder', '初试密码不能为空');
                $(body).find('input[name="Pwd"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            }else {
                //ajax发送post请求 给后端发送数据
                for (var i = 0; i < Rid_Rolename.length; i++) {
                    if (Rid_Rolename[i][1] === res.RoleName) {
                        RId = Rid_Rolename[i][0];
                        break;
                    }
                }
                for (var i = 0; i < tId_Teamname.length; i++) {
                    if (tId_Teamname[i][1] === res.TName) {
                        tId = tId_Teamname[i][0];
                        break;
                    }
                }
                for (var i = 0; i < gId_Groupname.length; i++) {
                    if (gId_Groupname[i][1] === res.GName) {
                        gId = gId_Groupname[i][0];
                        break;
                    }
                }
                $.post("/UserInfo/Edit", {
                    UName: res.UName, UCode: res.UCode, RId: RId, Remark: res.Remark, StatusFlag: res.StatusFlag, UId: data.UId, tId: tId, gId: gId
                });
                globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                //表格重载
                updatatable('table_ry', '#table_ry', 550, "/UserInfo/GetAllUserInfos", "员工管理", globalPage, globalLimit);
                layer.close(index);
            }
            ////当点击‘确定’按钮的时候，获取弹出层返回的值
            //var res = window["layui-layer-iframe" + index].callbackdata(index, 'adduser');
            //for (var i = 0; i < Rid_Rolename.length; i++) {
            //    if (Rid_Rolename[i][1] === res.RoleName) {
            //        RId = Rid_Rolename[i][0];
            //        break;
            //    }
            //} 
            ////ajax发送post请求 给后端发送数据
            //$.post("/UserInfo/Edit", {
            //    UName: res.UName, UCode: res.UCode, RId: RId, Remark: res.Remark, StatusFlag: res.StatusFlag, Id: data.UId
            //});

            //globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            //globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            ////表格重载
            //updatatable('table_ry', '#table_ry', 550, "/UserInfo/GetAllUserInfos", "员工管理", globalPage, globalLimit);
            ////最后关闭弹出层
            //layer.close(index);
        },
        success: function (layero, index) {
            //获取iframe页面     
            var body = layer.getChildFrame('body', index);
            $(body).find('input[name="UName"]').attr("value", data.UName);//输入父页面的姓名
            $(body).find('input[name="UCode"]').attr("value", data.UCode);//输入父页面的角色编码
            $(body).find('textarea[name="Remark"]').val(data.Remark);//输入父页面的备注
            if (data.StatusFlag == "无效") {//输入父页面的角色状态
                $(body).find('input[title="有效"]').attr('checked', false);
                $(body).find('input[title="无效"]').attr('checked', true);
            }
            else {
                $(body).find('input[title="有效"]').attr('checked', true);
                $(body).find('input[title="无效"]').attr('checked', false);
            }
            $.get("/UserInfo/GetAllRoles", {}, function (d) {
                for (var i = 0; i < d.length; i++) {
                    if (d[i].RoleName === data.RoleName) {
                        var e = $('<option value="' + d[i].RoleName + '" selected>' + d[i].RoleName + '</option>');
                    }
                    else {
                        var e = $('<option value="' + d[i].RoleName + '">' + d[i].RoleName + '</option>');  
                    }
                    $(body).find('select[name="RoleName"]').append(e);
                }
                for (var i = 0; i < d.length; i++) {//保存查询用
                    Rid_Rolename[i] = [d[i].Id, d[i].RoleName];
                }
                layui.use('form', function () {
                    var form = layui.form;
                    form.render('select');
                });
                //获取新窗口对象
                var iframeWindow = layero.find('iframe')[0].contentWindow;
                //重新渲染
                iframeWindow.layui.form.render();
            })
            $.get("/UserInfo/GetAllTeams", {}, function (d) {
                for (var i = 0; i < d.length; i++) {
                    if (d[i].TName === data.TName) {
                        var e = $('<option value="' + d[i].TName + '" selected>' + d[i].TName + '</option>');
                    }
                    else {                        
                        var e = $('<option value="' + d[i].TName + '">' + d[i].TName + '</option>');
                    }
                    $(body).find('select[name="TName"]').append(e);
                }
                for (var i = 0; i < d.length; i++) {//保存查询用
                    tId_Teamname[i] = [d[i].Id, d[i].TName];
                }
                layui.use('form', function () {
                    var form = layui.form;
                    form.render('select');
                });
                //获取新窗口对象
                var iframeWindow = layero.find('iframe')[0].contentWindow;
                //重新渲染
                iframeWindow.layui.form.render();
            })
            $.get("/UserInfo/GetAllGroups", {}, function (d) {
                for (var i = 0; i < d.length; i++) {
                    if (d[i].GName === data.GName) {
                        var e = $('<option value="' + d[i].GName + '" selected>' + d[i].GName + '</option>');
                    }
                    else {
                        var e = $('<option value="' + d[i].GName + '">' + d[i].GName + '</option>');
                    }
                    $(body).find('select[name="GName"]').append(e);
                }
                for (var i = 0; i < d.length; i++) {//保存查询用
                    gId_Groupname[i] = [d[i].Id, d[i].GName];
                }
                layui.use('form', function () {
                    var form = layui.form;
                    form.render('select');
                });
                //获取新窗口对象
                var iframeWindow = layero.find('iframe')[0].contentWindow;
                //重新渲染
                iframeWindow.layui.form.render(); 
            })
            //$.get("/UserInfo/GetAllRoles", {}, function (data_return) {
            //    var obj = data_return;
            //    for (var i = 0; i < obj.length; i++) {
            //        if (obj[i].RoleName === data.RoleName)
            //            $(body).find('select[name="RoleName"]').val(data.RoleName);
            //    }
            //    for (var i = 0; i < obj.length; i++) {//保存查询用
            //        Rid_Rolename[i] = [obj[i].Id, obj[i].RoleName];
            //    }
            //    layui.use('form', function () {
            //        var form = layui.form;
            //        form.render('select', 'RoleName');
            //    });
            //    $.get("/UserInfo/GetAllTeams", {}, function (data_return1) {
            //        var obj = data_return1;
            //        for (var i = 0; i < obj.length; i++) {
            //            if (obj[i].TName === data.TName) {
            //                $(body).find('select[name="TName"]').val(data.TName);
            //            }
            //        }
            //        tId_Teamname = new Array();
            //        for (var i = 0; i < obj.length; i++) {//保存查询用
            //            tId_Teamname[i] = [obj[i].Id, obj[i].TName];
            //        }
            //        layui.use('form', function () {
            //            var form = layui.form;
            //            form.render('select', 'TName');
            //        });
            //        $.get("/UserInfo/GetAllGroups", {}, function (data_return2) {
            //            var obj = data_return2;
            //            for (var i = 0; i < obj.length; i++) {
            //                if (obj[i].GName === data.GName) {
            //                    //$(body).find('select[name="GName"]').val(data.GName);
            //                    $(body).find('select[name="GName"]').attr("value", data.GName);
            //                }
            //            }
            //            gId_Groupname = new Array();
            //            for (var i = 0; i < obj.length; i++) {//保存查询用
            //                gId_Groupname[i] = [obj[i].Id, obj[i].GName];
            //            }
            //            layui.use('form', function () {
            //                var form = layui.form;
            //                form.render('select', 'GName');
            //            });
            //            //获取新窗口对象
            //            var iframeWindow = layero.find('iframe')[0].contentWindow;
            //            //重新渲染
            //            iframeWindow.layui.form.render();
            //        })
            //    })     
            //})
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
            var body = layer.getChildFrame('body', index);
            if (res.UName === "") {
                $(body).find('input[name="UName"]').attr('placeholder', '员工名字不能为空');
                $(body).find('input[name="UName"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.UCode === "") {
                $(body).find('input[name="UCode"]').attr('placeholder', '员工编码不能为空');
                $(body).find('input[name="UCode"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.Pwd === "") {
                $(body).find('input[name="Pwd"]').attr('placeholder', '初试密码不能为空');
                $(body).find('input[name="Pwd"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (typeof (res.StatusFlag) == "undefined") {
                $(body).find(".StatusFlagerror").show();
                $(window.frames[0].document).scrollTop("70");//垂直滚动条移动
            }
            else { 
                for (var i = 0; i < Rid_Rolename.length; i++) {
                    if (Rid_Rolename[i][1] === res.RoleName) {
                        RId = Rid_Rolename[i][0];
                        break;
                    }
                }
                for (var i = 0; i < tId_Teamname.length; i++) {
                    if (tId_Teamname[i][1] === res.TName) {
                        tId = tId_Teamname[i][0];
                        break;
                    }
                }
                for (var i = 0; i < gId_Groupname.length; i++) {
                    if (gId_Groupname[i][1] === res.GName) {
                        gId = gId_Groupname[i][0];
                        break;
                    }
                }
                //ajax发送post请求 给后端发送数据
                $.post("/UserInfo/Add", {UName: res.UName, UCode: res.UCode, Pwd: res.Pwd, RId: RId, Remark: res.Remark, StatusFlag: res.StatusFlag, tId: tId, gId: gId},
                   function (data) {
                   if (data !== 'fail') {
                        //表格重载 跳转到操作页面
                        globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                        globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                        if (num_p % globalLimit === 0) globalPage += 1;//超过分页值 页码加1
                        updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "员工管理", globalPage, globalLimit);
                    }else {
                        layui.use('layer', function () {
                            var layer = layui.layer;
                            layer.msg('<span style="font-size:24px;vertical-align:middle;line-height:76px;">员工已存在</span>', {
                                time: 2000,
                                area: ['200px', '100px'],
                                shade: 0.4,
                                shadeClose: true
                            });
                        });
                    }
                    layer.close(index);
                });
            }
        },
        success: function (layero, index) {
            $.get("/UserInfo/GetAllRoles", {}, function (data) {
                for (var i = 0; i < data.length; i++) {//保存查询用
                    Rid_Rolename[i] = [data[i].Id, data[i].RoleName];
                }
            })
            $.get("/UserInfo/GetAllTeams", {}, function (data) {
                for (var i = 0; i < data.length; i++) {//保存查询用
                    tId_Teamname[i] = [data[i].Id, data[i].TName];
                }
            })
            $.get("/UserInfo/GetAllGroups", {}, function (data) {
                for (var i = 0; i < data.length; i++) {//保存查询用
                    gId_Groupname[i] = [data[i].Id, data[i].GName];
                }
            })
            //var xhr = new XMLHttpRequest();
            //xhr.open('GET', "/UserInfo/GetAllRoles");
            //xhr.send();
            //xhr.onreadystatechange = function () {
            //    if (this.readyState !== 4) return;
            //    var obj = eval("(" + this.responseText + ")");//JSON.parse安全
            //    for (var i = 0; i < obj.length; i++) {//保存查询用
            //        console.log(obj[i].Id + "+" + obj[i].RoleName)
            //        Rid_Rolename[i] = [obj[i].Id, obj[i].RoleName];
            //    }
            //}
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, "searchuser");
            console.log("搜索" + res);
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "员工管理", 1, globalLimit, res);
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
                StatusFlag: $('input[name^="StatusFlag"]:checked').val(),//前缀为StatusFlag
                TName: $('select[name="TName"] option:selected').val(),
                GName: $('select[name="GName"] option:selected').val(),
            }
            break;
        case 'adddevice':
            var data = {
                DeviceId: $('input[name="DeviceId"]').val()
            }
            break;
        case 'searchuser':
            var data = {
                UCode: $('input[name="UCode"]').val(),
                RoleName: $('select[name="RoleName"] option:selected').val(),
                TName: $('select[name="TName"] option:selected').val(),
                GName: $('select[name="GName"] option:selected').val(),
            }
            break;
        case 'searchdevice':
            var data = {
                DeviceId: $('input[name="DeviceId"]').val(),
                StatusFlag: $('select[name="StatusFlag"] option:selected').val(),
            }
            break;
        case 'searcholog':
            var data = {
                //FirstTime: Date.parse(new Date($('input[name="FirstTime"]').val())),
                //LastTime: Date.parse(new Date($('input[name="LastTime"]').val())),
                FirstTime: $('input[name="FirstTime"]').val(),
                LastTime: $('input[name="LastTime"]').val(),
                UName: $('input[name="UName"]').val(),
                ActionType: $('select[name="ActionType"] option:selected').val(),
                ActionName: $('select[name="ActionName"] option:selected').val()
            }
            break;
        case 'warning':
            var data = {
                DeviceId: $('input[name="DeviceId"]').val(),
                WarningMessage: $('select[name="WarningMessage"] option:selected').val(),
                WarningStartTime: $('input[name="WarningStartTime"]').val()
            }
            break;
        case 'addclass':
            var data = {
                TName: $('input[name="TName"]').val(),
                StartTime: $('input[name="StartTime"]').val(),
                StopTime: $('input[name="StopTime"]').val()
            }
            break;
        case 'searchclass':
            var data = {
                schTName: $('input[name="schTName"]').val(),
                schTime: $('input[name="schTime"]').val(),
            }
            break;
    }
    return data;
}

function someDel(assort) {
    var delId = new Array();//批量删除时给后台发的字符串存储变量
    if (assort === 'user') {
        for (var i = 0; i < UIdtable.length; i++) {
            if (UIdtable[i][1] === 1) {
                delId[delId.length] = UIdtable[i][0];
            }
        }
        //delId = delId.slice(0, delId.length - 1);
        layer.confirm('确定删除？', function (index) {
            layer.close(index);
            $.ajax({
                type: 'POST',
                url: "/UserInfo/Delete",
                data: { ids: delId },
                traditional: true,
                success: function (data) {
                    console.log(data);
                }
            })
            //$.post("/UserInfo/Delete", { ids: delId });//发送字符串
            //表格重载
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "员工管理", 1, globalLimit);
        });
    }
    else if (assort === 'device') {
        for (var i = 0; i < DIdtable.length; i++) {
            if (DIdtable[i][1] === 1) {
                delId[delId.length] = DIdtable[i][0];
            }
        }
        //delId = delId.slice(0, delId.length - 1);
        console.log(delId);
        layer.confirm('确定删除？', function (index) {
            layer.close(index);
            $.ajax({
                type: 'POST',
                url: "/Device/Delete",
                data: { ids: delId },
                traditional: true,
                success: function (data) {
                    console.log(data);
                }
            })
            //$.post("/Device/Delete", { ids: delId });//发送字符串
            //表格重载
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "员工管理", 1, globalLimit);
        });
    }
    else if (assort === 'class') {
        for (var i = 0; i < CIdtable.length; i++) {
            if (CIdtable[i][1] === 1) {
                delId[delId.length] = CIdtable[i][0];
            }
        }
        //delId = delId.slice(0, delId.length - 1);
        layer.confirm('确定删除？', function (index) {
            layer.close(index);
            $.ajax({
                type: 'POST',
                url: "/TeamInfo/Delete",
                data: { ids: delId },
                traditional: true,
                success: function (data) {
                    console.log(data);
                }
            })
            //$.post("/TeamInfo/Delete", { ids: delId });//发送字符串
            //表格重载
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable('table_class', '#table_class', 550, '/TeamInfo/GetTeamInfo', "班号管理", 1, globalLimit);
        });
    }
}
function updatatable_search(id, elem, height, url, title, page, limit, res) {//表格重载 跳转到操作页面 
    var table = layui.table;
    if (id === 'table_ry') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { SchCode: res.UCode, SchRoleName: res.RoleName }
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
                globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                Power('user');
                UIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    UIdtable[i] = [res.data[i].UId, 0];
                }
                num_p = count;
            }
        });
    }
    else if (id === 'table_device') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { deviceId: res.DeviceId, statusFlag: res.StatusFlag }
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
                globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                Power('device');
                DIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    DIdtable[i] = [res.data[i].Id, 0];
                }
                num_d = count;   
            }
        });
    }
    else if (id === 'table_olog') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { FirstTime: res.FirstTime, LastTime: res.LastTime, UName: res.UName, ActionType: res.ActionType, ActionName: res.ActionName }
            , method: 'post'
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
            }
        });
    }
    else if (id === 'table_warning') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { schDeviceId: res.DeviceId, schMessage: res.WarningMessage, firsTime: res.WarningStartTime }
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
            }
        });
    }
    else if (id === 'table_class') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { schTName: res.schTName, schTime: res.schTime }
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
            }
        });
    }
    else if (id === 'table_relationDevice') {
        table.reload(id, {
            elem: elem
            //, height: height
            , url: url//数据接口
            , title: title
            , page: {
                curr: page
            }//重新制定page和limit
            , limit: limit
            , where: { deviceId: res.DeviceId, statusFlag: res.StatusFlag, gId: getdata() }
            , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
                console.log("表格重载完成");
            }
        });
    }
}
function updatatable(id, elem, height, url, title, page, limit) {//表格重载 跳转到操作页面
    var table = layui.table;
    table.reload( id, {
        elem: elem
        //, height: height
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
                Power('user');
                UIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    UIdtable[i] = [res.data[i].UId, 0];
                }
                num_p = count;
            }
            else if (id === 'table_device') {
                Power('device');
                DIdtable = [];//清空数组
                for (var i = 0; i < (count % globalLimit === 0 ? globalLimit : count % globalLimit); i++) {
                    DIdtable[i] = [res.data[i].Id, 0];
                }
                num_d = count;
            }
        }
    });
}
function getRolename() {
    $.get("/UserInfo/GetAllRoles", {}, function (data) {
        for (var i = 0; i < data.length; i++) {
            var e = $('<option value="' + data[i].RoleName + '">' + data[i].RoleName + '</option>');
            //$(body).find('select[name="RoleName"]').append(e);
            $('select[name="RoleName"]').append(e);
        }
        layui.use('form', function () {
            var form = layui.form;
            form.render('select');
        });
    })
    $.get("/UserInfo/GetAllTeams", {}, function (data) {
        for (var i = 0; i < data.length; i++) {
            var e = $('<option value="' + data[i].TName + '">' + data[i].TName + '</option>');
            //$(body).find('select[name="RoleName"]').append(e);
            $('select[name="TName"]').append(e);
        }
        layui.use('form', function () {
            var form = layui.form;
            form.render('select');
        });
    })
    $.get("/UserInfo/GetAllGroups", {}, function (data) {
        for (var i = 0; i < data.length; i++) {
            var e = $('<option value="' + data[i].GName + '">' + data[i].GName + '</option>');
            //$(body).find('select[name="RoleName"]').append(e);
            $('select[name="GName"]').append(e);
        }
        layui.use('form', function () {
            var form = layui.form;
            form.render('select');
        });
    })
}




///////////////////////////////////////////////////////////////////////////////////////////////////
//设备相关
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_device'
        //, height: 500
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
                if (res.data[i].StatusFlag === "0")
                    res.data[i].StatusFlag = "关机中";
                else if (res.data[i].StatusFlag === "1")
                    res.data[i].StatusFlag = "运行中";
                else if (res.data[i].StatusFlag === "2")
                    res.data[i].StatusFlag = "暂停中";
                else if (res.data[i].StatusFlag === "3")
                    res.data[i].StatusFlag = "故障中";
                else if (res.data[i].StatusFlag === "未连接")
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
            Power('device');
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
                $.post("/Device/Delete", { ids: ids }, function (data) {
                    if (data === 'ok') {
                        num_d = num_d - 1;
                        globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                        globalPage = Math.ceil(num_d / globalLimit);//获取页码值
                        if (num_d % globalLimit === 0) globalPage -= 1;//超过分页值 页码加1
                        //表格重载
                        updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "设备管理", globalPage, globalLimit);
                    }
                    else {
                        alert("你没有权限删除");
                    }
                });
            });
        } else if (layEvent === 'group') { //分组
            layerShowAddgroup('设备分组', 'LayerAddgroup', 500, 250, obj.data);
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
            if (res.DeviceId !== "") {
                //ajax发送post请求 给后端发送数据
                $.post("/Device/Add", { DeviceId: res.DeviceId }, function (data) {
                    if (data !== 'fail') {//判断是否重复
                        //表格重载 跳转到操作页面
                        globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                        globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                        if (num_p % globalLimit === 0) globalPage += 1;//超过分页值 页码加1
                        updatatable('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "员工管理", globalPage, globalLimit);
                    }
                    else {
                        layui.use('layer', function () {
                            var layer = layui.layer;
                            layer.msg('<span style="font-size:24px;vertical-align:middle;line-height:76px;">设备已存在</span>', {
                                time: 2000,
                                area: ['200px', '100px'],
                                shade: 0.4,
                                shadeClose:true
                            });
                        }); 
                    }
                });
                //最后关闭弹出层
                layer.close(index);
            }
            else {
                var body = layer.getChildFrame('body', index);
                $(body).find('input[name="DeviceId"]').attr('placeholder', '设备ID不能为空');
                $(body).find('input[name="DeviceId"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            }
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, "searchdevice");
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('table_device', '#table_device', 550, '/Device/GetAllDeviceInfos', "设备管理", 1, globalLimit, res);
            //最后关闭弹出层
            layer.close(index);
        },
        skin: 'demo-class'
    });
}
function Power(index) {
    if (index === 'device') {
        $.post("/ActionInfo/GetActions", {}, function (data) {
            console.log(data);
            power = data;
            for (var i = 0; i < data.length; i++) {
                if (data[i] === '删除设备') {
                    $("button[name='删除设备']").removeAttr('disabled');
                    $("button[name='删除设备']").removeClass('layui-btn-disabled');
                    $("a[name='删除']").removeClass('a_disabled');
                    $("a[name='删除']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '添加设备') {
                    $("button[name='添加设备']").removeAttr('disabled');
                    $("button[name='添加设备']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '查看设备') {
                    $("a[name='查看']").removeClass('a_disabled');
                    $("a[name='查看']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '查找设备') {
                    $("button[name='查找设备']").removeAttr('disabled');
                    $("button[name='查找设备']").removeClass('layui-btn-disabled');
                }
            }
        });
    }
    else if (index === 'user') {
        $.post("/ActionInfo/GetActions", {}, function (data) {
            console.log(data);
            for (var i = 0; i < data.length; i++) {
                if (data[i] === '编辑员工') {
                    $("a[name='编辑']").removeClass('a_disabled');
                    $("a[name='编辑']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '删除员工') {
                    $("button[name='删除员工']").removeAttr('disabled');
                    $("button[name='删除员工']").removeClass('layui-btn-disabled');
                    $("a[name='删除']").removeClass('a_disabled');
                    $("a[name='删除']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '添加员工') {
                    $("button[name='添加员工']").removeAttr('disabled');
                    $("button[name='添加员工']").removeClass('layui-btn-disabled');
                }
                else if (data[i] === '查找员工') {
                    $("button[name='查找员工']").removeAttr('disabled');
                    $("button[name='查找员工']").removeClass('layui-btn-disabled');
                }
            }
        });
    }
}
function layerShowAddgroup(title, url, w, h, data) {
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
            var body = layer.getChildFrame('body', index);
            if (res.UName === "") {
                $(body).find('input[name="UName"]').attr('placeholder', '员工名字不能为空');
                $(body).find('input[name="UName"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.UCode === "") {
                $(body).find('input[name="UCode"]').attr('placeholder', '员工编码不能为空');
                $(body).find('input[name="UCode"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else if (res.Pwd === "") {
                $(body).find('input[name="Pwd"]').attr('placeholder', '初试密码不能为空');
                $(body).find('input[name="Pwd"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else {
                //ajax发送post请求 给后端发送数据
                for (var i = 0; i < Rid_Rolename.length; i++) {
                    if (Rid_Rolename[i][1] === res.RoleName) {
                        RId = Rid_Rolename[i][0];
                        break;
                    }
                }
                $.post("/UserInfo/Edit", {
                    UName: res.UName, UCode: res.UCode, RId: RId, Remark: res.Remark, StatusFlag: res.StatusFlag, Id: data.UId
                });
                globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                //表格重载
                updatatable('table_ry', '#table_ry', 550, "/UserInfo/GetAllUserInfos", "员工管理", globalPage, globalLimit);
                layer.close(index);
            }
        },
        success: function (layero, index) {
            //获取iframe页面   
            console.log(data.Id);
            var body = layer.getChildFrame('body', index);
            $(body).find('input[name="UName"]').attr("value", data.UName);//输入父页面的姓名

            $.get("/UserInfo/GetAllRoles", {}, function (data_return) {
                var obj = data_return;
                for (var i = 0; i < obj.length; i++) {
                    if (obj[i].RoleName === data.RoleName)
                        $(body).find('select[name="RoleName"]').val(data.RoleName);
                }
                for (var i = 0; i < obj.length; i++) {//保存查询用
                    Rid_Rolename[i] = [obj[i].Id, obj[i].RoleName];
                }
                layui.use('form', function () {
                    var form = layui.form;
                    form.render('select');
                });
                //获取新窗口对象
                var iframeWindow = layero.find('iframe')[0].contentWindow;
                //重新渲染
                iframeWindow.layui.form.render();
            })
        }
    });
}

layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_olog'
        //, height: 520
        , url: '/OperationLog/GetAllOperationLogs' //数据接口
        , title: "员工管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            //{ field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center', fixed: 'left'}
            , { field: 'UName', title: '员工姓名', minWidth: 80, align: 'center' }
            , { field: 'ActionType', title: '操作功能', minWidth: 80, sort: true, align: 'center' }
            , { field: 'ActionName', title: '操作类型', minWidth: 80, align: 'center' }
            , { field: 'OperationObj', title: '操作对象', minWidth: 80, align: 'center' }
            , { field: 'SubTime', title: '操作时间', minWidth: 80, align: 'center' }
           // , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , parseData: function (res) { //修改原始数据
            console.log(res.data[1].SubTime);
            for (var i = 0; i < res.data.length; i++) {   
                res.data[i].SubTime = (eval(res.data[i].SubTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d HH:mm:ss");
            } 
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
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
    table.on('tool(table_olog)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
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
                $.post("/UserInfo/Delete", { ids: ids }, function (data) {
                    num_p = num_p - 1;
                    globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                    globalPage = Math.ceil(num_p / globalLimit);//获取页码值
                    if (num_p % globalLimit === 0) globalPage -= 1;//超过分页值 页码加1
                    //表格重载
                    updatatable('table_ry', '#table_ry', 550, '/UserInfo/GetAllUserInfos', "员工管理", globalPage, globalLimit);
                })
            });
        } else if (layEvent === 'edit') { //编辑
            layerShowEdituser('编辑员工', 'LayerEdituser', 500, 450, obj.data);
            //同步更新缓存对应的值
            /*obj.update({
                UName: '123'
                , title: 'xxx'
            });*/
        }
    });
    table.on('checkbox(table_olog)', function (obj) {
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
function layerShowSearcholog(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, "searcholog");
            var body = layer.getChildFrame('body', index);
            console.log("Searcholog内容如下");
            console.log(res);
            if (res.FirstTime === "" || res.LastTime === "") {
                $(body).find('input[name="FirstTime"]').attr('placeholder', '时间不能为空');
                $(body).find('input[name="FirstTime"]').addClass("red");
                $(body).find('input[name="LastTime"]').attr('placeholder', '时间不能为空');
                $(body).find('input[name="LastTime"]').addClass("red");
                layui.use('form', function () {
                    var form = layui.form;
                    form.render();
                });
            } else {
                //表格重载 跳转到操作页面
                globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                updatatable_search('table_olog', '#table_olog', 550, '/OperationLog/SearchOperationLogs', "操作日志", 1, globalLimit, res);
                //最后关闭弹出层
                layer.close(index);
            }
        },
        skin: 'demo-class'
    });
}
function uploadUserdetail(data) {
    if (data === "get") {
        $.post("/UserInfo/GetUserDetail", {}, function (data) {
            console.log(data.Phone);
            if (data.Picture !== null) {
                $('#Hportrait').prop('src', data.Picture);
            }
            else {
                $('#Hportrait').prop('src', "../Content/DH/img/人物.png");
            }
            $("input[name='RoleName']").val(data.RoleName);
            $("input[name='UName']").val(data.UName);
            if (data.Gender === '男') {
                $("input[title='男']").attr('checked', true);
            }
            else if (data.Gender === '女') {
                $("input[title='女']").attr('checked', true);
            }
            $("input[name='Phone']").val(data.Phone);
            $("input[name='Email']").val(data.Email);
            $("textarea[name='Remark']").val(data.Remark);
            layui.use('form', function () {
                var form = layui.form;
                form.render();
            });
        })
    }
    else if (data === "upload") {
        //头像文件在上传的时候已经添加
        userDetail.append('RoleName', $('input[name="MyRoleName"]').val());
        userDetail.append('UName', $('input[name="MyUName"]').val());
        if ($('input:radio[title="男"]:checked').val() === 'true') {
            console.log($('input:radio[title="男"]:checked').val());
            userDetail.append('Gender', '男');
        }
        else if ($('input:radio[title="女"]:checked').val() === 'true') {
            userDetail.append('Gender', '女');
        }
        else {
            userDetail.append('Gender', '');
        }
        userDetail.append('Phone', $('input[name="Pnumber"]').val());
        userDetail.append('Email', $('input[name="Email"]').val());
        userDetail.append('Remark', $('input[name="Remark"]').val());
        //console.log(userDetail.getAll('Picture'));
        //var xhr = new XMLHttpRequest();
        //xhr.open('POST', "/UserInfo/EditUserDetail");
        ////xhr.setRequestHeader('content-Type', 'application/x-www-form-urlencoded');
        //xhr.send(userDetail);
        //xhr.onreadystatechange = function () {
        //    if (this.readyState !== 4) return;
        //    userDetail = new FormData();//全部清空 释放旧的
        //}


        $.ajax({
            url: "/UserInfo/EditUserDetail",
            type: "POST",
            data: userDetail,
            cache: false,
            processData: false,  // 直接发送formdata格式要特殊处理 告诉jQuery不要去处理发送的数据
            contentType: false,   // 告诉jQuery不要去设置Content-Type请求头
            success: function () {
                userDetail = new FormData();//全部清空 释放旧的
            }
        });
        //console.log(userDetail);
        //$.post("/UserInfo/EditUserDetail", { 'Picture': $('#chooseImage').val()} , function (data) {
            
        //})
    }
}
/////////////////////////////////报警表格/////////////////////////
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    table.render({
        elem: '#table_warning'
        //, height: 520
        , url: '/WarningInfo/GetWarningInfo' //数据接口
        , title: "当前报警"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            //{ field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center', fixed: 'left' }
            , { field: 'DeviceId', title: '设备ID', minWidth: 80, align: 'center' }
            , { field: 'WarningMessage', title: '报警信息', minWidth: 80, sort: true, align: 'center' }
            , { field: 'WarningStartTime', title: '开始时间', minWidth: 80, align: 'center' }
            , { field: 'WarningTime', title: '报警时长', minWidth: 80, align: 'center' }
            // , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , parseData: function (res) { //修改原始数据
            for (var i = 0; i < res.data.length; i++) {
                res.data[i].WarningStartTime = (eval(res.data[i].WarningStartTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d HH:mm:ss");
                var m = Math.floor(res.data[i].minute % 60);
                var h = Math.floor(res.data[i].minute / 60 % 24);
                var d = Math.floor(res.data[i].minute / 60 / 24);
                if (m < 10) m = "0" + m;
                if (h < 10) h = "0" + h;
                if (d < 10) d = "0" + d;  
                res.data[i].WarningTime = d + ' 天 ' + h + ' 时 ' + m + ' 分';
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量

        }
        , skin: 'line'
    });
});
function layerShowSearchwarning(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'warning');
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('table_warning', '#table_warning', 550, '/WarningInfo/GetWarningInfo', "当前报警", 1, globalLimit, res);
            //最后关闭弹出层
            layer.close(index);
        },
        success: function (layero, index) {
           
        }
    });
}
////////////////////////////班号设置////////////////////////////////
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_class'
        //, height: 520
        , url: '/TeamInfo/GetTeamInfo' //数据接口
        , title: "班号管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            //, { field: 'DeviceId', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'TName', title: '班号', minWidth: 80, align: 'center' }
            , { field: 'wTime', title: '工作时间', minWidth: 80, sort: true, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , parseData: function (res) { //修改原始数据
            for (var i = 0; i < res.data.length; i++) {      
                res.data[i].wTime = (eval(res.data[i].StartTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss")
                    + ' ~ ' + (eval(res.data[i].StopTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss");
            }
            return {
                "code": res.code, //解析接口状态
                "msg": res.msg, //解析提示文本
                "count": res.count, //解析数据长度
                "data": res.data //解析数据列表
            };
        }
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
            CIdtable = [];//清空
            if (curr === Math.floor(count / globalLimit) + 1) {
                var length = (count % globalLimit === 0 ? globalLimit : count % globalLimit);
            }
            else {
                var length = globalLimit;
            }
            for (var i = 0; i < length; i++) {
                CIdtable[i] = [res.data[i].Id, 0];
            }
            console.log(CIdtable);
        }
        , skin: 'line'

    });
    table.on('tool(table_class)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象

        if (layEvent === 'del') { //删除
            console.log(data);
            layer.confirm('确定删除？', function (index) {
                layer.close(index);
                //向服务端发送删除指令
                ids = "" + data.Id;
                $.post("/TeamInfo/Delete", { ids: ids }, function (data) {
                    globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                    //表格重载
                    updatatable('table_class', '#table_class', 550, '/TeamInfo/GetTeamInfo', "班号管理", 1, globalLimit);
                })
            });
        } else if (layEvent === 'edit') { //编辑
            layerShowEditclass('编辑员工', 'LayerEditclass', 500, 350, obj.data);
        }
    });
    table.on('checkbox(table_class)', function (obj) {
        console.log(obj.checked); //当前是否选中状态
        console.log(obj.data); //选中行的相关数据
        console.log(obj.type); //如果触发的是全选，则为：all，如果触发的是单选，则为：one
        if (obj.type === "all") {
            if (obj.checked === true) {
                for (var i = 0; i < CIdtable.length; i++) {
                    CIdtable[i][1] = 1;
                }
            }
            else {
                for (var i = 0; i < CIdtable.length; i++) {
                    CIdtable[i][1] = 0;
                }
            }
        }
        else if (obj.checked === true) {
            for (var i = 0; i < CIdtable.length; i++) {
                if (CIdtable[i][0] === obj.data.Id) {
                    CIdtable[i][1] = 1;
                    break;
                }
            }
        }
        else if (obj.checked === false) {
            for (var i = 0; i < CIdtable.length; i++) {
                if (CIdtable[i][0] === obj.data.Id) {
                    CIdtable[i][1] = 0;
                    break;
                }
            }
        }
    });
});
function layerShowEditclass(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'addclass');
            var body = layer.getChildFrame('body', index);
            console.log(res.TName);
            if (res.TName === '') {
                $(body).find('input[name="TName"]').attr('placeholder', '班号名字不能为空');
                $(body).find('input[name="TName"]').addClass("red");
            }
            else if (res.StartTime === '') {
                $(body).find('input[name="TName"]').attr('placeholder', '开始工作时间不能为空');
                $(body).find('input[name="TName"]').addClass("red");
            }
            else if (res.StopTime === '') {
                $(body).find('input[name="StopTime"]').attr('placeholder', '结束工作时间不能为空');
                $(body).find('input[name="StopTime"]').addClass("red");
            }
            else {
                $.post("/TeamInfo/Edit", { Id: data.Id, TName: res.TName, StartTime: res.StartTime, StopTime: res.StopTime },
                    function (data) {
                        if (data === "Ok") {
                            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                            updatatable('table_class', '#table_class', 550, "/TeamInfo/GetTeamInfo", "班号管理", globalPage, globalLimit);
                            layer.close(index);
                        }
                        else {
                            layer.close(index);
                            layui.use('layer', function () {
                                var layer = layui.layer;
                                layer.msg('<span style="font-size:16px;vertical-align:middle;line-height:76px;">添加失败</span>', {
                                    time: 2000,
                                    area: ['200px', '100px'],
                                    shade: 0.4,
                                    shadeClose: true
                                });
                            });
                        }

                    });
            }
        },
        success: function (layero, index) {
            //获取iframe页面     
            var body = layer.getChildFrame('body', index);
            console.log(data);
            $(body).find('input[name="TName"]').attr("value", data.TName);//自动添加班号
            $(body).find('input[name="StartTime"]').attr("value",
                (eval(data.StartTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss"));//自动添加时间
            $(body).find('input[name="StopTime"]').attr("value",
                (eval(data.StopTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss"));//自动添加时间
            
            layui.use('form', function () {
                var form = layui.form;
                form.render('select');
            });
            //获取新窗口对象
            var iframeWindow = layero.find('iframe')[0].contentWindow;
            //重新渲染
            iframeWindow.layui.form.render();
        }
    });
}
function layerShowAddclass(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'addclass');
            var body = layer.getChildFrame('body', index);
            console.log(res.TName);
            if (res.TName === '') {
                $(body).find('input[name="TName"]').attr('placeholder', '班号名字不能为空');
                $(body).find('input[name="TName"]').addClass("red");
            }
            else if (res.StartTime === '') {
                $(body).find('input[name="TName"]').attr('placeholder', '开始工作时间不能为空');
                $(body).find('input[name="TName"]').addClass("red");
            }
            else if (res.StopTime === '') {
                $(body).find('input[name="StopTime"]').attr('placeholder', '结束工作时间不能为空');
                $(body).find('input[name="StopTime"]').addClass("red");
            }
            else {
                $.post("/TeamInfo/AddTeamInfo", { TName: res.TName, StartTime: res.StartTime, StopTime: res.StopTime },
                    function (data) {
                        if (data === "Ok") {
                            globalPage = $(".layui-laypage-skip").find("input").val();//获取页码值
                            globalLimit = $(".layui-laypage-limits").find("option:selected").val();//获取分页数目
                            updatatable('table_class', '#table_class', 550, "/TeamInfo/GetTeamInfo", "班号管理", globalPage, globalLimit);
                            layer.close(index);
                        }
                        else {
                            layer.close(index);
                            layui.use('layer', function () {
                                var layer = layui.layer;
                                layer.msg('<span style="font-size:16px;vertical-align:middle;line-height:76px;">添加失败</span>', {
                                    time: 2000,
                                    area: ['200px', '100px'],
                                    shade: 0.4,
                                    shadeClose: true
                                });
                            });
                        }

                    });
            }
        },
        success: function (layero, index) {

        }
    });
}
function layerShowSearchclass(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, 'searchclass');
            var body = layer.getChildFrame('body', index);     
            $.post("/TeamInfo/GetTeamInfo", { schTName: res.schTName, schTime: res.schTime },function (data) {
                layer.close(index);
                //表格重载
                globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
                updatatable_search('table_class', '#table_class', 550, '/TeamInfo/GetTeamInfo', "设班号管理", 1, globalLimit, res);
            });
        },
        success: function (layero, index) {

        }
    });
}
////////////////////////////组号设置////////////////////////////////
layui.use('table', function () {//打开网页刷新表格
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_group'
        //, height: 520
        , url: '/GroupInfo/GetGroupDevices?id=0' //数据接口
        , title: "组号关联设备"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            //, { field: 'DeviceId', title: '序号', minWidth: 100, sort: true, align: 'center' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'DeviceId', title: '设备ID', minWidth: 80, align: 'center' }
            //, { field: 'wTime', title: '工作时间', minWidth: 80, sort: true, align: 'center' }
            , { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        //, parseData: function (res) { //修改原始数据
        //    for (var i = 0; i < res.data.length; i++) {
        //        res.data[i].wTime = (eval(res.data[i].StartTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss")
        //            + ' ~ ' + (eval(res.data[i].StopTime.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("HH:mm:ss");
        //    }
        //    return {
        //        "code": res.code, //解析接口状态
        //        "msg": res.msg, //解析提示文本
        //        "count": res.count, //解析数据长度
        //        "data": res.data //解析数据列表
        //    };
        //}
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量

        }
        , skin: 'line'

    });
});
layui.use('table', function () {//打开网页刷新表格
    var href = window.location.href;
    var Id = href.split("?Id=");
    data = Id[1];
    var table = layui.table;
    //第一个实例
    table.render({
        elem: '#table_relationDevice'
        //, height: 500
        , url: '/GroupInfo/GetAllDeviceInfos?gId=' + data //数据接口
        , title: "设备管理"
        , page: true //开启分页
        , limit: 10
        , limits: [5, 10, 15, 20]
        , cols: [[ //表头
            { field: 'Checkbox', type: 'checkbox', minWidth: 50, fixed: 'left' }
            , { field: 'index', title: '序号', minWidth: 50, type: "numbers", align: 'center' }
            , { field: 'DeviceId', title: '设备ID', minWidth: 80, align: 'center' }
            , { field: 'StatusFlag', title: '运行状态', minWidth: 80, align: 'center' }
            //, { fixed: 'right', title: '操作', minWidth: 120, align: 'center', toolbar: '#barDemo' }
        ]]
        , toolbar: true
        , done: function (res, curr, count) {//如果是异步请求数据方式，res即为你接口返回的信息, curr是当前的页码，count是得到的数据总量
            //var num = Array(res.data.length);
        }
        , skin: 'line'

    });
});
function layerShowSearchdevice_g(title, url, w, h, data) {
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
            var res = window["layui-layer-iframe" + index].callbackdata(index, "searchdevice");
            //表格重载 跳转到操作页面
            globalLimit = $(".layui-laypage-limits").find("option:selected").val() //获取分页数目
            updatatable_search('table_relationDevice', '#table_relationDevice', 550, '/GroupInfo/GetAllDeviceInfos', "关联设备", 1, globalLimit, res);
            //最后关闭弹出层
            layer.close(index);
        },
        skin: 'demo-class'
    });
}
//日期表
layui.use('laydate', function () {
    var laydate = layui.laydate;

    laydate.render({
        elem: '#date1' //指定元素
        , type: 'datetime'//日期时间选择器
    });
    laydate.render({
        elem: '#date2' //指定元素
        , type: 'datetime'//日期时间选择器
    });
    laydate.render({
        elem: '#first-classWorktime' //指定元素
        , type: 'time'//日期时间选择器
    });
    laydate.render({
        elem: '#last-classWorktime' //指定元素
        , type: 'time'//日期时间选择器
    });
    laydate.render({
        elem: '#WarningStartTime' //指定元素
        , type: 'datetime'//日期时间选择器
    });
});
//全局加载进度条
$(document)
    .ajaxStart(function () {
        NProgress.start();
    })
    .ajaxStop(function () {
        NProgress.done();
    })
$(document).ready(function () {
    $("button[name='添加员工']").click(function () {
        layerShowAdduser('添加员工', 'LayerAdduser', 550, 450, "null");
    });
    $("button[name='删除员工']").click(function () {
        someDel('user');
    });
    $("button[name='查找员工']").click(function () {
        layerShowSearchuser('查找员工', 'LayerSearchuser', 500, 380, "null");
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
    $("button[name='登录']").click(function () {
        send();
    });
    $("button[name='查找日志']").click(function () {
        layerShowSearcholog('查找日志', 'LayerSearcholog', 500, 450, "null");
    });
    //$("button[name='确认修改']").click(function () {
    //    uploadDevicedetail('upload');
    //});
    $("button[name='确认修改']").click(function () {    
        var myReg = /^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;//邮箱格式的正则表达式
        if ($("input[name='Phone']").val() !== '' && String($("input[name='Phone']").val()).length !== 11) {
            $(".phoneerror").show();
        }
        else if ($("input[name='Email']").val() !== '' && !(myReg.test($("input[name='Email']").val()))) {
            $(".emailerror").show();
        }
        else {
             $("form").ajaxSubmit({
                url: "/UserInfo/EditUserDetail",
                type: "POST",
                 success: function (data) {
                     if (data === 'ok') {
                         window.location.href = '../UserInfo/Userdetail';
                     }
                     layui.use('layer', function () {
                         var layer = layui.layer;
                         layer.msg('<span style="font-size:16px;vertical-align:middle;line-height:76px;">' + data + '</span>', {
                             time: 2000,
                             area: ['200px', '100px'],
                             shade: 0.4,
                             shadeClose: true
                         });
                     });
                }
             })
        }
    });
    $("button[name='查找报警']").click(function () {
        layerShowSearchwarning('查找报警', 'LayerSearchwarning', 550, 450, "null");
    });
    $("button[name='添加班号']").click(function () {
        layerShowAddclass('添加班组', 'LayerAddclass', 550, 450, "null");
    });
    $("button[name='查找班号']").click(function () {
        layerShowSearchclass('查找班组', 'LayerSearchclass', 550, 350, "null");
    });
    $("button[name='删除班号']").click(function () {
        someDel('class');
    });
    //用户注销
    $('#logout').click(function () {
        logout();
    })
});