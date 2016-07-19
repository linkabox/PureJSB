using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;

// 这个是跑在C#的
[JsType(Export = false)]
public class CallJSTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //1---------------------------------------------------------------
        // 调用全局函数，使用 id 0
        JSMgr.vCall.CallJSFunctionName(0, "CreateJSBindingInfo");

        object obj = JSMgr.datax.getObject((int)JSApi.GetType.JSFunRet);
        if (obj != null)
        {
            CSRepresentedObject csObj = (CSRepresentedObject)obj;
            PrintJSBindingInfo(csObj.jsObjID);
        }
        else
        {
            Debug.Log("obj is null");
        }
	}

    void PrintJSBindingInfo(int objID)
    {
        // 获得字符串属性
        JSApi.getProperty(objID, "Version");
        string s = JSApi.getStringS((int)JSApi.GetType.SaveAndRemove);
        print(s);

        // 获得整数属性
        JSApi.getProperty(objID, "QQGroup");
        int i = JSApi.getInt32((int)JSApi.GetType.SaveAndRemove);
        print(i);

        // 调用这个obj的函数
        JSMgr.vCall.CallJSFunctionName(objID, "getDocumentUrl");
        s = JSApi.getStringS((int)JSApi.GetType.JSFunRet);
        print(s);
    }

	void Update () {
	
	}
}
