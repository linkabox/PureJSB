using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

//#region C#导出接口特殊处理
//[assembly: CsExportedMethod(TargetType = typeof(NGUITools), TargetMethodName = "AddMissingComponent", JsCode = @"
//        AddMissingComponent$1: function(T, go) { 
//            var t = go.GetComponent$1(T);
//            if (t == null){
//                t = go.AddComponent$1(T);
//            }
//            return t; //Ret: T
//        },")]
//#endregion
public static class JSBCodeGenSettings
{
	/// <summary>
	/// 以下文件或目录不参与转换为js脚本
	/// </summary>
	public static string[] PathsNotToJavaScript =
	{
		"JSBinding/",
		"NGUI/",
		"Assets/Plugins/",
		"Resources/",
		"Standard Assets/",
		"StreamingAssets/",
		"WebPlayerTemplates/",
	};

	/// <summary>
	/// 参与编译js脚本白名单
	/// </summary>
	public static string[] PathsToJavaScript =
	{
		
	};

    /// <summary>
    ///     导出指定Assembly内的指定命名空间下的类型，若没有指定命名空间列表则导出这个dll的所有接口
    ///     导出dll内的根命名空间用null来表示
    /// </summary>
    public static readonly Dictionary<string, List<string>> CustomAssemblyConfig = new Dictionary
        <string, List<string>>
    {
        {"UnityEngine", new List<string> {"UnityEngine"}},
        //{"UnityEngine.UI", null},
        {"DOTween", new List<string> {"DG.Tweening","DG.Tweening.Core"}},
        //{"Assembly-CSharp-firstpass", new List<string> {null, "GameResource"}}
    };

    /// <summary>
    ///     导出指定类型
    /// </summary>
    public static readonly List<Type> CustomTypeConfig = new List<Type>
    {
        //JSBExportTest
        typeof (TestExtensionMethod),
        typeof (APIExportTest),
        typeof (APIExportTest.RefObject),
        //JSBExportTest
        
        //mscorlib
        typeof (IEnumerator),
        typeof (ICollection),
        typeof (IDisposable),
        typeof (IConvertible),
        typeof (IList),
        typeof (IDictionary),
        typeof (List<>),
        typeof (Dictionary<,>),
        typeof (KeyValuePair<,>),
        typeof (Dictionary<,>.KeyCollection),
        typeof (Dictionary<,>.ValueCollection),
        typeof (HashSet<>),
        typeof (Hashtable),

        typeof (WeakReference),
        typeof (Stopwatch),
        //typeof (StringBuilder),
        //typeof (TimeSpan),
        //typeof (DateTime),
        typeof (Math),
        typeof (System.Random),

        typeof (Regex),
        typeof (Capture),
        typeof (Group),
        typeof (GroupCollection),
        typeof (Match),

        typeof (Convert),
        typeof (Encoding),

        typeof (File),
        //typeof (FileInfo),
        typeof (Directory),
        //typeof (DirectoryInfo),
        //typeof (FileStream),
        //typeof (Stream),
        //typeof (Path),

        typeof (ICloneable),
        typeof (IEnumerable),
        typeof (System.Runtime.Serialization.IDeserializationCallback),
        typeof (System.Runtime.Serialization.ISerializable),
        typeof (System.Runtime.InteropServices._Exception),

        //typeof (XmlNode),
        //typeof (System.Runtime.Serialization.ISurrogateSelector),
        //typeof (IXPathNavigable),
        //typeof (XmlDocument),
        //typeof (XmlNodeList),
        //typeof (XmlElement),
        //typeof (XmlLinkedNode),
        //typeof (XmlAttributeCollection),
        //typeof (XmlNamedNodeMap),
        //typeof (XmlAttribute),
        //mscorlib

        //UnityEngine

        //UnityEngine

        ////NGUI
        ////typeof(LanguageSelection),
        ////typeof(TypewriterEffect),
        //typeof (UIButton),
        //typeof (UIButtonActivate),
        //typeof (UIButtonColor),
        //typeof (UIButtonMessage),
        //typeof (UIButtonOffset),
        //typeof (UIButtonRotation),
        //typeof (UIButtonScale),
        //typeof (UICenterOnChild),
        //typeof (UICenterOnClick),
        //typeof (UIDragCamera),
        //typeof (UIDragDropContainer),
        //typeof (UIDragDropItem),
        //typeof (UIDragDropRoot),
        //typeof (UIDraggableCamera),
        //typeof (UIDragObject),
        ////typeof(UIDragResize),
        //typeof (UIDragScrollView),
        //typeof (UIEventTrigger),
        ////typeof(UIForwardEvents),
        //typeof (UIGrid),
        ////typeof(UIImageButton),
        ////typeof(UIKeyBinding),
        ////typeof(UIKeyNavigation),
        //typeof (UIPlayAnimation),
        //typeof (UIPlaySound),
        //typeof (UIPlayTween),
        //typeof (UIPopupList),
        //typeof (UIProgressBar),
        ////typeof(UISavedOption),
        //typeof (UIScrollBar),
        //typeof (UIScrollView),
        //typeof (UISlider),
        //typeof (UISoundVolume),
        //typeof (UITable),
        //typeof (UIToggle),
        ////typeof(UIToggledComponents),
        ////typeof(UIToggledObjects),
        //typeof (UIWidgetContainer),
        //typeof (UIWrapContent),
        //typeof (ActiveAnimation),
        ////typeof(AnimationOrTween<>),
        //typeof (BetterList<>),
        //typeof (BMFont),
        //typeof (BMGlyph),
        //typeof (BMSymbol),
        ////typeof(ByteReader),
        //typeof (EventDelegate),
        ////typeof(Localization),
        //typeof (NGUIDebug),
        //typeof (NGUIMath),
        //typeof (NGUIText),
        //typeof (NGUITools),
        ////typeof(PropertyBinding),
        ////typeof(PropertyReference),
        //typeof (RealTime),
        //typeof (SpringPanel),
        //typeof (UIBasicSprite),
        //typeof (UIDrawCall), //modify by senkay at 2015-08-13
        //typeof (UIEventListener),
        //typeof (UIGeometry),
        //typeof (UIRect),
        ////typeof(UISnapshotPoint),
        //typeof (UIWidget),
        ////typeof(AnimatedAlpha),
        ////typeof(AnimatedColor),
        ////typeof(AnimatedWidget),
        //typeof (SpringPosition),
        //typeof (TweenAlpha),
        //typeof (TweenColor),
        //typeof (TweenFOV),
        //typeof (TweenHeight),
        //typeof (TweenOrthoSize),
        //typeof (TweenPosition),
        //typeof (TweenRotation),
        //typeof (TweenScale),
        //typeof (TweenTransform),
        //typeof (TweenVolume),
        //typeof (TweenWidth),
        //typeof (UITweener),
        //typeof (UI2DSprite),
        //typeof (UI2DSpriteAnimation),
        //typeof (UIAnchor),
        //typeof (UIAtlas),
        //typeof (UICamera),
        //typeof (UICamera.MouseOrTouch),
        //typeof (UIFont),
        //typeof (UIInput), //modify by senkay at 2015-08-13
        ////typeof(UIInputOnGUI),
        //typeof (UILabel),
        ////typeof(UILocalize),
        ////typeof(UIOrthoCamera),
        //typeof (UIPanel),
        //typeof (UIRoot),
        //typeof (UISprite),
        //typeof (UISpriteAnimation),
        //typeof (UISpriteData),
        ////typeof(UIStretch),
        //typeof (UITextList),
        //typeof (UITexture),
        ////typeof(UITooltip),
        //typeof (UIViewport),
        //typeof (UIRect.AnchorPoint),
        //typeof (UIPageGrid),
        //typeof (PageScrollView),
        ////NGUI
    };

    // if uselist return a white list, don't check noUseList(black list) again
    /// <summary>
    ///     类型白名单，在白名单内的类型一定会导出，忽略所有过滤条件
    /// </summary>
    public static readonly HashSet<Type> TypeWhiteSet = new HashSet<Type>();

    /// <summary>
    ///     类型黑名单，忽略导出一下类型
    /// </summary>
    public static readonly List<string> TypeBlackSet = new List<string>
    {
		"GUIStyleState",
        "Canvas",
        "CanvasRenderer",
        "RectTransform",
        "HideInInspector",
        "ExecuteInEditMode",
        "AddComponentMenu",
        "ContextMenu",
        "RequireComponent",
        "DisallowMultipleComponent",
        "SerializeField",
        "AssemblyIsEditorAssembly",
        "Attribute",
        "Types",
        "UnitySurrogateSelector",
        //"TrackedReference",
        "TypeInferenceRules",
        "FFTWindow",
        "RPC",
        "Network",
        "MasterServer",
        "BitStream",
        "HostData",
        "ConnectionTesterStatus",
        //"GUI",
        "EventType",
        "EventModifiers",
        "FontStyle",
        "TextAlignment",
        "TextEditor",
        "TextEditorDblClickSnapping",
        "TextGenerator",
        "TextClipping",
        "Gizmos",
        "ADBannerView",
        "ADInterstitialAd",
        "Android",
        "Tizen",
        "jvalue",
        "iPhone",
        "iOS",
        "CalendarIdentifier",
        "CalendarUnit",
        "CalendarUnit",
        "ClusterInput",
        "FullScreenMovieControlMode",
        "FullScreenMovieScalingMode",
        "Handheld",
        "LocalNotification",
        "NotificationServices",
        "RemoteNotificationType",
        "RemoteNotification",
        "SamsungTV",
        "TextureCompressionQuality",
        "TouchScreenKeyboardType",
        "TouchScreenKeyboard",
        "MovieTexture",
        "UnityEngineInternal",
        "Terrain",
        "Tree",
        "SplatPrototype",
        "DetailPrototype",
        "DetailRenderMode",
        "MeshSubsetCombineUtility",
        "AOT",
        "Social",
        "SendMouseEvents",
        "Cursor",
        "Flash",
        "ActionScript",
        "OnRequestRebuild",
        "Ping",
        "ShaderVariantCollection",
        "SimpleJson.Reflection",
        "CoroutineTween",
        "GraphicRebuildTracker",
        "Advertisements",
        "UnityEditor",
        "WSA",
        "EventProvider",
        "Apple",
        "ClusterInput"
    };

    /// <summary>
    ///     过滤指定类型的成员信息，大多数是移动平台不支持的接口
    /// </summary>
    public static readonly Dictionary<Type, HashSet<string>> TypeMemberFilterConfig = new Dictionary
        <Type, HashSet<string>>
    {
        {typeof (StreamReader), new HashSet<string> {"CreateObjRef", "GetLifetimeService", "InitializeLifetimeService"}},
        {typeof (StreamWriter), new HashSet<string> {"CreateObjRef", "GetLifetimeService", "InitializeLifetimeService"}},
        {typeof (WWW), new HashSet<string> {"movie"}},
        {
            typeof (AnimationClip),
            new HashSet<string>
            {
                "averageDuration",
                "averageAngularSpeed",
                "averageSpeed",
                "apparentSpeed",
                "isLooping",
                "isAnimatorMotion",
                "isHumanMotion"
            }
        },
        {typeof (AnimatorOverrideController), new HashSet<string> {"PerformOverrideClipListCleanup"}},
        {typeof (Caching), new HashSet<string> {"SetNoBackupFlag", "ResetNoBackupFlag"}},
        {typeof (Light), new HashSet<string> {"areaSize"}},
        {typeof (Security), new HashSet<string> {"GetChainOfTrustValue"}},
        {typeof (Texture2D), new HashSet<string> {"alphaIsTransparency"}},
#if !UNITY_IPHONE
		{typeof (WebCamTexture), new HashSet<string> {"MarkNonReadable", "isReadable"}},
#endif
        {typeof (Application), new HashSet<string> {"ExternalEval"}},
        {typeof (GameObject), new HashSet<string> {"networkView"}},
        {typeof (Component), new HashSet<string> {"networkView"}},
        // unity5
        {typeof (UnityEngine.AnimatorControllerParameter), new HashSet<string> {"name"}},
        //{typeof (Resources), new HashSet<string> {"LoadAssetAtPath"}},
        {typeof (Input), new HashSet<string> {"IsJoystickPreconfigured"}},
#if UNITY_4_6 || UNITY_4_7
        {typeof (PointerEventData), new HashSet<string> {"lastPress"}},
        {typeof (InputField), new HashSet<string> {"onValidateInput"}},
        {typeof (Graphic), new HashSet<string> {"OnRebuildRequested"}},
        {typeof (Text), new HashSet<string> {"OnRebuildRequested"}},
        {
            typeof (Motion),
            new HashSet<string>
            {
                "ValidateIfRetargetable",
                "averageDuration",
                "averageAngularSpeed",
                "averageSpeed",
                "apparentSpeed",
                "isLooping",
                "isAnimatorMotion",
                "isHumanMotion"
            }
        },
#endif
        //{typeof(UIDrawCall), new HashSet<string>
        //    {
        //        "isActive",
        //    }
        //},
        //{typeof(UIWidget), new HashSet<string>
        //    {
        //        "showHandlesWithMoveTool",
        //        "showHandles",
        //    }
        //},
        //{typeof(UIInput), new HashSet<string>
        //    {
        //        "ProcessEvent",
        //    }
        //},
    };

    public static bool IsDiscardMemberInfo(Type type, MemberInfo memberInfo)
    {
        if (typeof(
            Delegate).IsAssignableFrom(type))
        {
            return true;
        }

        if (TypeMemberFilterConfig.ContainsKey(type))
        {
            var filterList = TypeMemberFilterConfig[type];
            if (filterList.Contains(memberInfo.Name))
                return true;
        }

        return false;
    }

    public static bool IsSupportByDotNet2SubSet(string functionName)
    {
        if (functionName == "Directory_CreateDirectory__String__DirectorySecurity" ||
            functionName == "Directory_GetAccessControl__String__AccessControlSections" ||
            functionName == "Directory_GetAccessControl__String" ||
            functionName == "Directory_SetAccessControl__String__DirectorySecurity" ||
            functionName == "DirectoryInfo_Create__DirectorySecurity" ||
            functionName == "DirectoryInfo_CreateSubdirectory__String__DirectorySecurity" ||
            functionName == "DirectoryInfo_GetAccessControl__AccessControlSections" ||
            functionName == "DirectoryInfo_GetAccessControl" ||
            functionName == "DirectoryInfo_SetAccessControl__DirectorySecurity" ||
            functionName == "File_Create__String__Int32__FileOptions__FileSecurity" ||
            functionName == "File_Create__String__Int32__FileOptions" ||
            functionName == "File_GetAccessControl__String__AccessControlSections" ||
            functionName == "File_GetAccessControl__String" ||
            functionName == "File_SetAccessControl__String__FileSecurity" ||
            functionName == "FileInfo_GetAccessControl__AccessControlSections" ||
            functionName == "FileInfo_GetAccessControl" ||
            functionName == "FileInfo_SetAccessControl__FileSecurity")
        {
            return false;
        }
        return true;
    }

    public static bool NeedGenDefaultConstructor(Type type)
    {
        if (typeof(Delegate).IsAssignableFrom(type))
            return false;

        if (type.IsInterface)
            return false;

        // don't add default constructor
        // if it has non-public constructors
        // (also check parameter count is 0?)
        if (type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Length != 0)
            return false;

        //foreach (var c in type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance))
        //{
        //    if (c.GetParameters().Length == 0)
        //        return false;
        //}

        if (type.IsClass && (type.IsAbstract || type.IsInterface))
            return false;

        if (type.IsClass)
        {
            return type.GetConstructors().Length == 0;
        }
        foreach (var c in type.GetConstructors())
        {
            if (c.GetParameters().Length == 0)
                return false;
        }
        return true;
    }
}