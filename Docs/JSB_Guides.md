<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [JSB框架热更原理](#jsb%E6%A1%86%E6%9E%B6%E7%83%AD%E6%9B%B4%E5%8E%9F%E7%90%86)
- [JSB框架结构](#jsb%E6%A1%86%E6%9E%B6%E7%BB%93%E6%9E%84)
- [框架层代码注意事项](#%E6%A1%86%E6%9E%B6%E5%B1%82%E4%BB%A3%E7%A0%81%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9)
- [业务层代码注意事项](#%E4%B8%9A%E5%8A%A1%E5%B1%82%E4%BB%A3%E7%A0%81%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9)
- [JSB框架优化与改进](#jsb%E6%A1%86%E6%9E%B6%E4%BC%98%E5%8C%96%E4%B8%8E%E6%94%B9%E8%BF%9B)
  - [针对原JSB框架优化与调整](#%E9%92%88%E5%AF%B9%E5%8E%9Fjsb%E6%A1%86%E6%9E%B6%E4%BC%98%E5%8C%96%E4%B8%8E%E8%B0%83%E6%95%B4)
  - [RoadMap](#roadmap)
- [JSB编译流程](#jsb%E7%BC%96%E8%AF%91%E6%B5%81%E7%A8%8B)
- [参考资料](#%E5%8F%82%E8%80%83%E8%B5%84%E6%96%99)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## JSB框架热更原理

@(Unity_Notes)

- JSB框架与现成的Lua框架的热更原理类似，都是保持框架层代码不变，将框架层接口导出，供业务层代码调用，而JSB框架解决最核心的问题就是处理框架层与业务层之间的数据交互。
- JSB框架与Lua框架最大的区别是，Lua框架的业务代码是由Lua来直接编写的，而JSB框架是由C#编写，然后通过转换工具一键转换为Js代码。
- 所有继承MonoBehaviour的组件转换之后，统一由JsComponent组件及其派生类来与对应的Js对象进行交互，将接收到的MonoBehaviour事件转发到Js层

## JSB框架结构
- JSBinding框架
	- 负责与JS引擎之间的数据交换，以及接口回调处理
	- 导出框架层接口，生成相应的Js代码供业务层调用
- [SpiderMonkey](https://developer.mozilla.org/en-US/docs/Mozilla/Projects/SpiderMonkey)引擎，由Mozilla用C/C++开发的Js引擎
- JSB框架外部工具，这些工具可以在JSBExternalTools目录下找到
	- SharpKit Compiler：将C#代码转换Js代码
	- JsTypeGenerator：因为SharpKit转换Js代码时需要在C#类中添加JsTypeAttribute来标记，为了不污染业务代码，所以通过这个工具来生成JsTypeInfo.cs来统一标记哪些C#类需要转换
	- closure-compiler：Google出品的Js代码压缩工具，可以在出包前对Js代码压缩处理，可以提高运行效率，减少Js代码的文件大小

## 框架层代码注意事项

1. 在Js层调用C#层接口时，如果需要传递容器类型的参数时，不能使用List，Dictionary，HashSet，这些会与jsclr定义的冲突，导致C#取到的参数值为null，按照如下规则定义框架层接口：
	- List，HashSet类型参数，改为直接传递数组:`void Add(int[] list)`
	- Dictionary类型参数，改为传递Hashtable:`void Add(Hashtable dic)`
	- 复合类型参数，需要先转json:`void Add(string obj)`
2. 在Android的Dll热更时遇到过这种情况，需要增加字段来满足新需求，如UISprite上增加置灰属性，这时候旧包加载Bundle时会报序列化异常的错误，导致加载失败。这是因为新增public字段使得脚本打包时生成的Hash值变更了。
	- 方案1：不定义public字段，只能通过添加private字段，然后通过代码动态设置其属性值。
	- 方案2：如果组件改动太大，可以直接将组件改名后，再重新打包Bundle
	- 方案3：打包Bundle后将Bundle内相关Hash值清零，打包apk后将apk内相关Hash值清零
3. 按照以下原则封装C#接口供业务层调用：
	- 线程操作，js中不支持线程操作，所有线程操作应该封装到C#中的线程管理器中实现，通过回调的方式通知js层。
	- 字节操作，js层处理字节操作非常低效，可以将字节数组封装为一个ByteArray对象并提供一系列操作接口供业务层使用。
	- 平台相关调用，有关平台特性相关的接口，最好封装成统一的接口供业务层调用，避免在业务层中用宏定义来进行条件编译，否则将产生多套业务js代码，不利于版本控制。
	- 不能导出协程方法接口供js调用，C#的协程只能由C#启动，改为C#中启动协程，处理完毕后回调通知js层。

## 业务层代码注意事项

由于SharpKit工具、JSB框架的不完善，以及Js代码运行效率的问题，所以编写业务代码时，会受到一些限制。需要清楚的一点，JSB框架下编写的业务代码，其实是在用C#来写Js，清楚了这一概念下面的一些问题就比较容易理解了。

1. 销毁GameObject时，记得及时清空在此GameObject上相关组件的引用
	- 在Mono环境下，销毁一个GameObject时，Unity会自动将其关联到的所有组件引用清空
	- 在Js环境下，虽然C#层对应的JsCompoent销毁了，但是关联的Js对象还是存在的，如果不手动置空或者重新赋值，这个Js对象会一直存在，不会被Js引擎GC回收的
	- 以PlayerView的人物名称组件为例:

		```cs
		if (_titleHUDView != null)  
		{
		    Destroy(_titleHUDView.gameObject);
		    _titleHUDView = null;
		}
		```

2. XXXModel尽量不要缓存其他Model中的引用，而是调用的时候通过其接口获取相关引用，如果一定要缓存，记得再适当的时机清空该引用
3. 如果需要记录DateTime的值到本地时，最好按照以下方式处理：
	- 因为直接记录DateTime.Ticks的值是long类型，转换到js那边使用会有溢出的风险
	- 当然也可以先转UnixTimeStamp再保存，获取时从UnixTimeStamp转回DateTime使用，不过过程比较繁琐
	- 还有一个概念需要明确一下，转换到Js后所有DateTime都是当作Js中的Date来使用的，Js的Date与C#的DateTime计时规则是不一样的
		- Js使用的是UnixTimestamp的计时规则，起点时间是1970/1/1 00:00:00
		- C#计时起点则是0001/1/1 00:00:00

	```cs
	var dateStr = dateTime.ToString("M/d/yyyy h:mm:ss tt");
	var dt = DateTime.Parse(dateStr);
	```

4. 如非必要，尽量少用LINQ相关方法，SharpKit的jsclr库支持不太完善

	```
	目前已知有问题的接口:
	Concat
	```

5. Json序列化操作，统一调用JsHelper的接口，如需直接将json直接转为List<T>时，按照如下方式调用:

	```cs
	//这种方式只在C#下有效，因为转换到Js代码后，会丢失掉<T>类型信息
	//所以加多一个泛型参数，保证转换js后依然知道List容器内的类型信息
	//var list = JsHelper.ToObject<List<T>>(json); 
	var list = JsHelper.ToCollection<List<T>， T>(json);
	```

6. IO文件流操作，统一调用FileHelper的接口

7. 编写NGUI组件的Controller时，不要使用OnClick来作为方法名使用，应该定义更具体的响应方法名，如：OnClickItem，然后用EventDelegate.Set来注册。如果用OnClick来定义，会产生两个问题：
	- OnClick事件是由UICamera通过GameObject.SendMessage的方式来转发给指定的GameObject的，如果这个OnClick方法同时也用EventDelegate.Set注册了，这个OnClick方法将会调用两次，产生冗余的运算。
	- 有时候只定义OnClick方法，但没有使用EventDelegate.Set的方式注册，在Mono模式下是可以正确执行；但在JSB模式下，JSComponent是不会转发OnClick这个事件的，所以不会执行。

	```
	不要以下面方法名定义响应方法，避免不必要的混乱:
	OnHover (isOver)
	OnPress (isDown)
	OnSelect (selected)
	OnClick ()
	OnDoubleClick ()
	OnDragStart ()
	OnDrag (delta)
	OnDragOver (draggedObject)
	OnDragOut (draggedObject)
	OnDragEnd ()
	
	OnTooltip (show)
	OnScroll (float delta)
	OnNavigate (KeyCode key)
	OnPan (Vector2 delta)
	OnKey (KeyCode key)
	```

8. 平台相关的原生插件，以及SDK接口的代码，不能放到`Assets/Scripts`下，这里只放可以更新的游戏逻辑代码，统一放到`Assets/Standard Assets/ThirdParty`或`Assets/Plugins`下
9. 循环中设置匿名方法，导致js闭包异常的问题，这个是SharpKit的bug，不支持这种处理方式，需要手动调整代码

	```cs
	原写法:
		for(int i=0;i<_plotInfo.characterList.Count;++i){
			CharacterEntity character = _plotInfo.characterList[i];
			if(character.active){
				_sequence.InsertCallback(character.startTime，()=>{
					GenerateCharacter(character);
				});
			}
		}
	
	修复闭包问题的写法:
		Action<CharacterEntity> initCharacter = character =>
		{
		    _sequence.InsertCallback(character.startTime， () => {
		        GenerateCharacter(character);
		    });
		};
		for(int i=0;i<_plotInfo.characterList.Count;++i){
			CharacterEntity character = _plotInfo.characterList[i];
			if(character.active)
			{
			    initCharacter(character);
			}
		}
	```

10. 基础类型转换时，尽量用`int.Parse`，`long.Parse`或者`(int)` `(long)`类型强制转换等方法，避免使用`Convert.ToXXX()`系列方法，因为clrlibrary缺少`Convert.ToXXX`的支持，只能调用C#导出的接口，这样调用效率更低。
11. 如非必要不要使用`OnGUI`相关方法来编写Js代码，因为通过`OnGUI`来绘制UI信息，会使每帧产生大量Js->CS的调用，效率非常低下。如果需要用`OnGUI`来输出一些详细Debug信息时，可以考虑通过NGUI来创建GM控制台界面进行输出。
12. 代码中尽量避免冗余的`ToString()`代码，~~尤其是数值常量的`ToString()`一定要去掉，这种写法转换到Js后属于语法错误，SharpKit转换的Bug，如:`Debug.Log("MyID:"+31488.ToString());`~~（**修改SharpKit源码解决**）
13. 计时器操作拆分为CSTimer和JSTimer，CSTimer框架层使用，JSTimer业务层使用
去除使用CoolDownManager的导出接口的方式是为了减少Js->Cs的调用，以及产生不必要的计时器任务委托对象
14. 谨慎使用`string.Format`方法，对于下面的例子，没必要用到`string.Format`，直接通过'+'进行拼接
因为无论C#或者js，使用`string.Format`都会产生额外的GC

	```cs
	var str = string.Format("#{0}"，i);
	var str = "#{0}" + i;
	```

15. 使用静态初始化的方式实现单例时，不要在某个单例的构造方法中调用另一个单例，因为在js中只是模拟实现这种静态初始化构造的方式，实际上还是有构造先后次序的。  
例子如下:  
`MissionDataModel`构造方法中调用了`MissionViewModel`和`MissionNpcModel`的单例，
由于`MissionDataModel`定义在前面，静态构造方法先执行，所以此时`MissionViewModel`和`MissionNpcModel`的单例对象为空，导致运行报错
16. `obj is (int|float|double)`这种类型判断需要特殊处理一下，统一调用`JsHelper.IsInt|IsFloat`方法进行判断，由于SharpKit的bug会导致一下情况：

	```cs
	public static int CheckType(object obj){
		if(obj is List<int>){
			return 1;
		}else if(obj is int){
			return 2;
		}else if(obj is float){
			return 3;
		}
		return 0;
	}
	```
	转换之后会变成这样，判断条件都变成true了：
	
	```javascript
	function CheckType(obj){
	    if (Is(obj, System.Collections.Generic.List$1.ctor)){
	        return 1;
	    }
	    else if (true){
	        return 2;
	    }
	    else if (true){
	        return 3;
	    }
	    return 0;
	};
	```
17. 继承自UnityEngine.Object的对象都使用`UnityEngine.Object.GetInstanceID()`来获取唯一ID，用`System.Object.GetHashCode()`不靠谱.
18. 在项目开发过程中，发现了SharpKit对`ref` `out`关键字的处理方式考虑不周全，当标记的参数是数组元素时调用会有异常，~~还存在个非常隐蔽的bug，由于延迟赋值的原因，递归循环调用时会导致栈溢出。~~（**修改SharpKit源码解决**）示例代码如下：

	```cs
	using System.Collections.Generic;
	using SharpKit.JavaScript;
	using SharpKit.Html;
	using SharpKit.jQuery;
	using SharpKit.JavaScript.Compilation;
	using SharpKit.JavaScript.Server;
	
	namespace SharpKitWebApp2
	{
	    [JsType(JsMode.Clr, Filename = "res/Default.js")]
	    public class RefTest
	    {
	        public static JsObject<int> _gItem;
	        public JsObject<int> _item;
	        public void MyMethod()
	        {
	            var a = new JsObject<int>();
	            a.Add("a", 133);
	            a.Add("b", 4512);
	            var b = UpdateItem(a, ref _item);
	            HtmlContext.console.log("a:" + a["b"]);
	            HtmlContext.console.log("b:" + b["b"]);
	        }
	        public JsObject<int> UpdateItem(JsObject<int> newItem, ref JsObject<int> item)
	        {
	            if (item == newItem)
	                return null;
	            item = newItem;
	            ExecuteItem(newItem);
	            return newItem;
	        }
	        public void ExecuteItem(JsObject<int> newItem)
	        {
	            UpdateItem(newItem, ref _gItem);
	        }
	    }
	
	    [JsType(JsMode.Global, Filename = "res/Default.js")]
	    public class DefaultClient
	    {
	        static void DefaultClient_Load()
	        {
	            JsCompilerGlobal.Compile();
	            new jQuery(HtmlContext.document.body).append("Ready<br/>");
	        }
	        static void btnTest_click(DOMEvent e)
	        {
	            var refTest = new RefTest();
	            refTest.MyMethod();
	        }
	    }
	}
	```

	```javascript
	/* Generated by SharpKit 5 v5.5.0 */
	
	if (typeof(JsTypes) == "undefined")
	    var JsTypes = [];
	var SharpKitWebApp2$RefTest = {
	    fullname: "SharpKitWebApp2.RefTest",
	    baseTypeName: "System.Object",
	    staticDefinition: {
	        cctor: function (){
	            SharpKitWebApp2.RefTest._gItem = null;
	        }
	    },
	    assemblyName: "SharpKitWebApp2",
	    Kind: "Class",
	    definition: {
	        ctor: function (){
	            this._item = null;
	            System.Object.ctor.call(this);
	        },
	        MyMethod: function (){
	            var a = new Object();
	            a ["a"] = 133;
	            a ["b"] = 4512;
	            var b = (function (){
	                var $1 = {
	                    Value: this._item
	                };
	                var $res = this.UpdateItem(a, $1);
	                this._item = $1.Value;
	                return $res;
	            }).call(this);
	            console.log(["a:" + a["b"]]);
	            console.log(["b:" + b["b"]]);
	        },
	        UpdateItem: function (newItem, item){
	            if (item.Value == newItem)
	                return null;
	            //在C#中执行下面这句话时，已经将a的值赋给_item了
	            //但SharpKit这种实现方式只是赋给了一个临时对象的Value值
	            //如果后面继续递归调用这个函数,_item的值会一直为空，无法跳出递归循环，导致栈溢出
	            item.Value = newItem;
	            this.ExecuteItem(newItem);
	            return newItem;
	        },
	        ExecuteItem: function (newItem){
	            (function (){
	                var $1 = {
	                    Value: SharpKitWebApp2.RefTest._gItem
	                };
	                var $res = this.UpdateItem(newItem, $1);
	                SharpKitWebApp2.RefTest._gItem = $1.Value;
	                return $res;
	            }).call(this);
	        }
	    }
	};
	JsTypes.push(SharpKitWebApp2$RefTest);
	function DefaultClient_Load(){
	    Compile();
	    $(document.body).append("Ready<br/>");
	};
	function btnTest_click(e){
	    var refTest = new SharpKitWebApp2.RefTest.ctor();
	    refTest.MyMethod();
	};
	```

## JSB框架优化与改进
### 针对原JSB框架优化与调整
因为原作者（浅唱）编写的JSB框架属于比较初期阶段，好多细节都不够完善，所以结合自身项目需求对原JSB框架做了以下变更：

- Add 支持Mac环境下编译js代码，需要brew install mono
- Add 支持导出dll中指定命名空间的C#接口，如：DoTween（参考slua实现）
- Add Vector2,Vector3部分方法改为js原生实现
- Fixed 修复CSGenerator生成代码缺少命名空间，导致编译报错（导出DoTween代码时发现）
- Refactor 从JSBindingSettings抽离出相关路径代码到JSPathSettings中
- Refactor MonoBehaviour2JSComponentName改为Mono2JsComConfig，调整为json格式加载与存储
- Refactor 添加JsType属性流程优化，通过JsTypeGenerator生成JsTypeInfo.cs来记录哪些类需要转换js代码，如果类定义已经带有JsType属性，优先使用类定义的，将不会记录到JsTypeInfo中
- Delete 简化JsCom生成类，去除一些使用频率非常低的接口，减少JsCom派生类的总量
- Refactor 汇总Update,LateUpdate,FixedUpdate事件到JsEngine统一发送
	- Awake阶段注册到JsComManager中，OnDestroy时注销
	- OnEnable和OnDisable更新JsCom的激活状态
	- 当JsEngine发送Update事件时，根据JsCom的激活状态调用其相应Update方法
- Refactor Time.deltaTime、Time.unscaledDeltaTime改为使用JsComManager每帧缓存的值，来减少每帧Js -> Cs的调用
- Refactor Time.fixedDeltaTime写死在JsComManager中，这样写有两个原因，一就是fixedDeltaTime很少会改，二是js调用C#接口返回值精度有一点损失
- Fixed 保持C#与Js协程调用的一致性，具体修改查看UnityEngine_MonoBehaviour文件
- Add 兼容导出C#原生容器类，支持同时使用js下List<>和C#的List<>
- Fixed 解决WeakReference.Target == null的问题
- Fixed 解决addJSFunCSDelegateRel操作相同key冲突的问题

### RoadMap
- 升级到SpiderMonkey 38/45，优化mozjswrap代码
- 替换SharpKit工具，由于SharpKit已经好久没更新了，加上本身bug不少，可以替换其它更流行的工具
	- [Duocode(收费)](http://duoco.de/)
	- [Bridge.NET(开源)](http://bridge.net/)

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





