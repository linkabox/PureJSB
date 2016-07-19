# PureJSBFramework
简化原JSB框架代码，抽离出核心代码建库，并对原代码重构整理，新增了一些特性。
[https://github.com/qcwgithub/qjsbunitynew](https://github.com/qcwgithub/qjsbunitynew)

## JSB编译流程

操作说明 | 编辑器选项
------------ | -------------
导出C#类接口(框架层没改动可跳过这步) | Generate JS and CS Bindings
分析业务代码脚本添加JsType属性 | Add SharpKit JsType Attribute
生成Mono2JsComConfig和JsTypeConfig配置信息 | Generate Mono2JsComConfig and JsTypeConfig
编译JsCode | Build Mobile JsCode
简化JsCode文件大小（可选） | Minify All JsCode
修复Windows下编辑器运行异常 | Fix Win Load Dll not found

## 参考资料

- [JSB热更方案作者Blog](http://www.cnblogs.com/answerwinner/p/4469021.html)
- [qjsbunitynew工程](https://github.com/qcwgithub/qjsbunitynew)
- [qjsbmozillajswrap工程](https://github.com/qcwgithub/qjsbmozillajswrap)
- [修改过的SharpKit分支](https://github.com/linkabox/SharpKit)
- [MDN查询JS API](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference)
- [jsFiddle在线编辑测试Js代码](https://jsfiddle.net/)
- [SharpKit在线编辑](http://sharpkit.net/Live.aspx)
- [Bridge.NET在线编辑](http://live.bridge.net/#)
- [DuoCode在线编辑](http://duoco.de/try)
- [.Net Fiddle在线测试C#代码](https://dotnetfiddle.net/)

具体详情查看[JSB_Guides文档](https://github.com/linkabox/PureJSB/blob/master/Docs/JSB_Guides.md)


