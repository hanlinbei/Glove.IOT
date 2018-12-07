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
$(document).ready(function () {
    $(".tck-close").click(function () {
        $(".tck").hide();
    });
});
