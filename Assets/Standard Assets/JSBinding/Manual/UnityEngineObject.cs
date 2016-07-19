using UnityEngine;

public partial class UnityEngineManual
{
    public static bool Object_op_Equality__UEObject__UEObject(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        var arg1 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        JSApi.setBooleanS((int) JSApi.SetType.Rval, arg0 as Object == arg1 as Object);
//#if UNITY_EDITOR
//        if (arg0 is CSRepresentedObject || arg1 is CSRepresentedObject)
//        {
//            throw new Exception("Error! UnityEngine.Object arg0 == arg1 has CSRepresentedObject");
//        }
//#endif
        return true;
    }

    public static bool Object_op_Implicit__UEObject_to_Boolean(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        JSApi.setBooleanS((int) JSApi.SetType.Rval, arg0 as Object);
//#if UNITY_EDITOR
//        if (arg0 is CSRepresentedObject)
//        {
//            throw new Exception("Error! UnityEngine.Object arg0 op_Implicit bool has CSRepresentedObject");
//        }
//#endif
        return true;
    }

    public static bool Object_op_Inequality__UEObject__UEObject(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        var arg1 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        JSApi.setBooleanS((int) JSApi.SetType.Rval, arg0 as Object != arg1 as Object);
//#if UNITY_EDITOR
//        if (arg0 is CSRepresentedObject || arg1 is CSRepresentedObject)
//        {
//            throw new Exception("Error! UnityEngine.Object arg0 != arg1 has CSRepresentedObject");
//        }
//#endif
        return true;
    }

    public static bool Object_Destroy__UEObject(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        var unityObj = arg0 as Object;
        if (unityObj != null)
            Object.Destroy(unityObj);

        return true;
    }

    public static bool Object_Destroy__UEObject__Single(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        float arg1 = JSApi.getSingle((int) JSApi.GetType.Arg);
        var unityObj = arg0 as Object;
        if (unityObj != null)
            Object.Destroy(unityObj, arg1);

        return true;
    }

    public static bool Object_DestroyImmediate__UEObject__Boolean(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        bool arg1 = JSApi.getBooleanS((int) JSApi.GetType.Arg);
        var unityObj = arg0 as Object;
        if (unityObj != null)
            Object.DestroyImmediate(unityObj, arg1);

        return true;
    }

    public static bool Object_DestroyImmediate__UEObject(JSVCall vc, int argc)
    {
        var arg0 = JSMgr.datax.getObject((int) JSApi.GetType.Arg);
        var unityObj = arg0 as Object;
        if (unityObj != null)
            Object.DestroyImmediate(unityObj);

        return true;
    }
}