using System;
using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;

//public static class GameObjectExt
//{
//    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
//    {
//        T t = go.GetComponent<T>();
//        if (t == null)
//        {
//            t = go.AddComponent<T>();
//        }
//        return t;
//    }
//}

public class ComponentTest : MonoBehaviour {

    // Use this for initialization
    void Start () {
        //通过js代码AddComponent的组件必须导出，否则无法添加
        //这种调用会报异常，暂时忽略  this.gameObject.AddComponent(typeof(BoxCollider));
        GameObject go = new GameObject("NewGo",new Type[] {typeof(SphereCollider),typeof(BoxCollider),typeof(Animation)});
        this.gameObject.AddComponent(typeof(SphereCollider));
        this.gameObject.AddComponent<BoxCollider>();
        this.gameObject.AddComponent("Animation");

        //this.gameObject.GetOrAddComponent<BoxCollider>();
        //this.gameObject.GetOrAddComponent<Animation>();

        var objs = Resources.FindObjectsOfTypeAll<Animation>();
        Debug.LogError("Find Objects: "+objs.Length);
        //GetOrAddComponent这类扩展方法只能用于添加C#的组件，否则Unity会Crash掉
        //this.gameObject.GetOrAddComponent<AwakeC>();

        // 
        //  GetComponent<>()
        //  TEnemy TEnemyBase
        TEnemyBase eb = GetComponent<TEnemyBase>();
        if (eb != null)
        {
            eb.enemyName = "BULL";
            Debug.Log("enemyName = " + eb.enemyName);
        }
        else
        {
            Debug.Log("GetComponent<TEnemyBase>() returns null!");
        }

        //gameObject.AddComponent<MentosKXT>();
        gameObject.GetOrAddComponent<MentosKXT>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
