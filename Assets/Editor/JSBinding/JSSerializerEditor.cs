using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SharpKit.JavaScript;

public static class JSSerializerEditor
{
    public enum SerializeType
    {
        String,
        Object,
    }
    public struct AnalyzeStructInfo
    {
        public JSSerializer.AnalyzeType analyzeType;
        public string Name; // field name, it's useless when analyzeType == xxObj xxEnd or when it's element of array
        public Type type; // type. sometimes value == null
        public object value;
        public JSSerializer.UnitType unitType;// Unit type, it's useless when analyzeType == xxBegin xxObj xxEnd

        public AnalyzeStructInfo(JSSerializer.AnalyzeType at,
            string name, Type type, object v = null, 
            JSSerializer.UnitType ut = JSSerializer.UnitType.ST_Unknown)
        {
            analyzeType = at;
            Name = name;
            this.type = type;
            value = v;
            unitType = ut;

            eSerialize = SerializeType.String;
        }
        // string or object?
        public SerializeType eSerialize;
        public void Alloc(JSSerializer serializer)
        {
            eSerialize = SerializeType.String;
            switch (analyzeType)
            {
                case JSSerializer.AnalyzeType.ArrayBegin:
                    AllocString("ArrayBegin/" + this.Name + "/");
                    break;
                case JSSerializer.AnalyzeType.ArrayEnd:
                    AllocString("ArrayEnd/" + this.Name + "/");
                    break;
                case JSSerializer.AnalyzeType.StructBegin:
                    AllocString("StructBegin/" + this.Name + "/" + JSNameMgr.GetTypeFullName(this.type));
                    break;
                case JSSerializer.AnalyzeType.StructEnd:
                    AllocString("StructEnd/" + this.Name + "/" + JSNameMgr.GetTypeFullName(this.type));
                    break;
                case JSSerializer.AnalyzeType.ListBegin:
                    AllocString("ListBegin/" + this.Name + "/" + JSNameMgr.GetTypeFullName(typeof(List<>)));
                    break;
                case JSSerializer.AnalyzeType.ListEnd:
                    AllocString("ListEnd/" + this.Name + "/" + JSNameMgr.GetTypeFullName(typeof(List<>)));
                    break;
                case JSSerializer.AnalyzeType.Unit:
                    {
                        var sb = new StringBuilder();

                        // this.value could be null
                        Type declaredType = this.type;
						Type valueType = (this.value != null ? this.value.GetType() : declaredType);

                        if (this.unitType == JSSerializer.UnitType.ST_JavaScriptMonoBehaviour ||
                            this.unitType == JSSerializer.UnitType.ST_UnityEngineObject)
                        {
                            eSerialize = SerializeType.Object;

                            if (this.unitType == JSSerializer.UnitType.ST_JavaScriptMonoBehaviour)
                            {
								if (!typeof(UnityEngine.MonoBehaviour).IsAssignableFrom(valueType) ||
						    		!WillTypeBeTranslatedToJavaScript(valueType))
                                {
									Debug.LogError("unitType is ST_JavaScriptMonoBehaviour, but valueType is not MonoBehaviour or doesn't JsType attribute.");
                                }

                                // if a monobehaviour is referenced
                                // and this monobehaviour will be translated to js later
                                //  ST_MonoBehaviour
                                
                                // add game object
								// this.value can be null
								int index;
								if (this.value == null)
									index = AllocObject(null);
								else
									index = AllocObject(((MonoBehaviour)this.value).gameObject);

                                // UnitType / Name / object Index / MonoBehaviour Name
								sb.AppendFormat("{0}/{1}/{2}/{3}", (int)this.unitType, this.Name, index, JSNameMgr.GetTypeFullName(valueType));
                                AllocString(sb.ToString());
                            }
                            else
                            {
                                // UnitType / Name / object Index
                                sb.AppendFormat("{0}/{1}/{2}", (int)this.unitType, this.Name, AllocObject((UnityEngine.Object)this.value));
                                AllocString(sb.ToString());
                            }
                        }
                        else
                        {
                            sb.AppendFormat("{0}/{1}/{2}", (int)this.unitType, this.Name, 
                                ValueToString(this.value, this.type));
                            AllocString(sb.ToString());
                        }
                    }
                    break;
            }
        }
    }

    static void AllocString(string str) { 
		lstString.Add(str); 
	}
    static int AllocObject(UnityEngine.Object obj) { 
		lstObjs.Add(obj);
        return lstObjs.Count - 1; 
	}
    /// <summary>
    /// lstString lstObjs store serialized string and object list
    /// </summary>
    static List<string> lstString = new List<string>();
    static List<UnityEngine.Object> lstObjs = new List<UnityEngine.Object>();

    static List<AnalyzeStructInfo> lstAnalyze = new List<AnalyzeStructInfo>();
    public static int AddAnalyze(Type type, string name, object value, int index = -1)
    {
        if (index == -1) index = lstAnalyze.Count;
        JSSerializer.UnitType unitType = GetUnitType(type, value);
        if (unitType != JSSerializer.UnitType.ST_Unknown)
        {
            lstAnalyze.Insert(index, new AnalyzeStructInfo(JSSerializer.AnalyzeType.Unit, name, type, value, unitType));
            return 1;
        }
        else
        {
            if (type.IsArray)
            {
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ArrayBegin, name, type));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ArrayObj, name, type, value));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ArrayEnd, name, type));
                return 3;
            }
            else if (type.IsClass || type.IsValueType)
            {
				if (value == null) return 0;
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.StructBegin, name, type));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.StructObj, name, type, value));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.StructEnd, name, type));
                return 3;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ListBegin, name, type));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ListObj, name, type, value));
                lstAnalyze.Insert(index++, new AnalyzeStructInfo(JSSerializer.AnalyzeType.ListEnd, name, type));
                return 3;
            }
        }
        return 0;
    }

    /// <summary>
    /// type to UnitType
    /// </summary>
    static Dictionary<Type, JSSerializer.UnitType> sDict;
    static JSSerializer.UnitType GetUnitType(Type type, object value)
    {
        if (sDict == null)
        {
            sDict = new Dictionary<Type, JSSerializer.UnitType>();

            sDict.Add(typeof(Boolean), JSSerializer.UnitType.ST_Boolean);

            sDict.Add(typeof(Byte), JSSerializer.UnitType.ST_Byte);
            sDict.Add(typeof(SByte), JSSerializer.UnitType.ST_SByte);
            sDict.Add(typeof(Char), JSSerializer.UnitType.ST_Char);
            sDict.Add(typeof(Int16), JSSerializer.UnitType.ST_Int16);
            sDict.Add(typeof(UInt16), JSSerializer.UnitType.ST_UInt16);
            sDict.Add(typeof(Int32), JSSerializer.UnitType.ST_Int32);
            sDict.Add(typeof(UInt32), JSSerializer.UnitType.ST_UInt32);
            sDict.Add(typeof(Int64), JSSerializer.UnitType.ST_Int64);
            sDict.Add(typeof(UInt64), JSSerializer.UnitType.ST_UInt64);

            sDict.Add(typeof(Single), JSSerializer.UnitType.ST_Single);
            sDict.Add(typeof(Double), JSSerializer.UnitType.ST_Double);


            sDict.Add(typeof(String), JSSerializer.UnitType.ST_String);
        }

        if (type.IsEnum)
        {
            return JSSerializer.UnitType.ST_Enum;
        }

		Type valueType = (value != null ? value.GetType () : type);

        if (
			((typeof(UnityEngine.MonoBehaviour).IsAssignableFrom(type)) && WillTypeBeTranslatedToJavaScript(type)) ||
			// 2015/10/23
			// 其实这个函数应该总是取 valueType 来判断
			// 怕出 bug，这里先特殊支持，当 type == MonoBehaviour 时，才检查 valueType
			((typeof(UnityEngine.MonoBehaviour) == type) && WillTypeBeTranslatedToJavaScript(valueType))
		    )
        {
            return JSSerializer.UnitType.ST_JavaScriptMonoBehaviour;
        }
        if ((typeof(UnityEngine.Object).IsAssignableFrom(type)))
        {
            return JSSerializer.UnitType.ST_UnityEngineObject;
        }

        JSSerializer.UnitType ret = JSSerializer.UnitType.ST_Unknown;
        if (!sDict.TryGetValue(type, out ret))
        {
            // Debug.LogError("GetIndex: Unknown type: " + type.Name);
            return JSSerializer.UnitType.ST_Unknown;
        }
        return ret;
    }
    /// <summary>
    /// C# values to string format.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    static string ValueToString(object value, Type type)
    {
        //
        // value
        //
        StringBuilder sb = new StringBuilder();
        if (type.IsPrimitive)
        {
            sb.AppendFormat("{0}", value.ToString());
        }
        else if (type.IsEnum)
        {
            sb.AppendFormat("{0}", (int)Enum.Parse(type, value.ToString()));
        }
        else if (type == typeof(string))
        {
            sb.AppendFormat("{0}", value == null ? "" : value.ToString());
        }
        else if (type == typeof(Vector2))
        {
            Vector2 v2 = (Vector2)value;
            sb.AppendFormat("{0}/{1}", v2.x, v2.y);
        }
        else if (type == typeof(Vector3))
        {
            Vector3 v3 = (Vector3)value;
            sb.AppendFormat("{0}/{1}/{2}", v3.x, v3.y, v3.z);
        }
        return sb.ToString();
    }
    /// <summary>
    /// Gets the mono behaviour serialized fields.
    /// Returns all PUBLIC fields who has no NonSerialized attribute.
    /// </summary>
    /// <param name="behaviour">The behaviour.</param>
    /// <returns></returns>
    public static FieldInfo[] GetMonoBehaviourSerializedFields(MonoBehaviour behaviour)
    {
        Type type = behaviour.GetType();
        return GetTypeSerializedFields(type);
    }
    public static FieldInfo[] GetTypeSerializedFields(Type type)
    {
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance /* | BindingFlags.Static */ );
        List<FieldInfo> lst = new List<FieldInfo>();
        for (var i = 0; i < fields.Length; i++)
        {
            bool bNonSerialized = false;
			// Skip generic type
			if (fields[i].FieldType.IsGenericType)
				continue;

			if (fields[i].FieldType.IsSubclassOf(typeof(System.Delegate)))
			    continue;

            object[] attrs = fields[i].GetCustomAttributes(false);
            for (var j = 0; j < attrs.Length; j++)
            {
                if (attrs[j].GetType() == typeof(NonSerializedAttribute))
                {
                    bNonSerialized = true;
                    break;
                }
            }
            if (!bNonSerialized)
            {
                lst.Add(fields[i]);
            }
        }
        return lst.ToArray();
	}
	public static PropertyInfo[] GetTypeSerializedProperties(Type type)
	{
		return JSBindingSettings.GetTypeSerializedProperties(type);
	}
    static void TraverseAnalyze()
    {
        bool bContinueTraverse = false;
        for (var i = 0; i < lstAnalyze.Count; i++)
        {
            AnalyzeStructInfo info = lstAnalyze[i];
            bool bBreakFor = true;
            int Pos = i + 1;
            switch (info.analyzeType)
            {
                case JSSerializer.AnalyzeType.ArrayObj:
                    {
                        bContinueTraverse = true;
                        if (info.value != null)
                        {
                            Array arr = (Array)info.value;
                            Type arrayElementType = info.value.GetType().GetElementType();
                            for (var j = 0; j < arr.Length; j++)
                            {
                                object value = arr.GetValue(j);
                                Pos += AddAnalyze(arrayElementType, "["+j.ToString()+"]", value, Pos);
                            }
                        }
                        lstAnalyze.RemoveAt(i);
                    }
                    break;
                case JSSerializer.AnalyzeType.StructObj:
                    {
                        bContinueTraverse = true;
                        var structure = info.value;

						// 1) Fields
                        FieldInfo[] fields = GetTypeSerializedFields(structure.GetType());
                        foreach (FieldInfo field in fields)
                        {
                            Pos += AddAnalyze(field.FieldType, field.Name, field.GetValue(structure), Pos);
                        }
						
						// 2) Properties
						PropertyInfo[] properties = GetTypeSerializedProperties (structure.GetType());
						foreach (PropertyInfo pro in properties)
						{
							Pos += AddAnalyze(pro.PropertyType, "#" + pro.Name, pro.GetValue(structure, null), Pos);
						}

                        lstAnalyze.RemoveAt(i);
                    }
                    break;
                case JSSerializer.AnalyzeType.ListObj:
                    {
                        bContinueTraverse = true;
                        if (info.value != null)
                        {
                            var list = info.value;
                            var listType = list.GetType();
                            var listElementType = listType.GetGenericArguments()[0];

                            int Count = (int)listType.GetProperty("Count").GetValue(list, new object[] { });
                            PropertyInfo pro = listType.GetProperty("Item");

                            for (var j = 0; j < Count; j++)
                            {
                                var value = pro.GetValue(list, new object[] { j });
                                Pos += AddAnalyze(listElementType, j.ToString(), value, Pos);
                            }
                        }
                        lstAnalyze.RemoveAt(i);
                    }
                    break;
                default:
                    {
                        bBreakFor = false;
                    }
                    break;
            }
            if (bBreakFor)
                break;
        }
        if (bContinueTraverse)
            TraverseAnalyze();
    }
    static void CopyBehaviour(MonoBehaviour behaviour, JSSerializer serizlizer)
    {
        lstAnalyze.Clear();
        lstString.Clear();
        lstObjs.Clear();

        // GameObject go = behaviour.gameObject;
        //Type type = behaviour.GetType();

		// 1) Fields
        FieldInfo[] fields = GetMonoBehaviourSerializedFields(behaviour);
        foreach (FieldInfo field in fields)
        {
            AddAnalyze(field.FieldType, field.Name, field.GetValue(behaviour));
        }

		// 2) Properties
		PropertyInfo[] properties = GetTypeSerializedProperties (behaviour.GetType());
		foreach (PropertyInfo pro in properties)
		{
			AddAnalyze(pro.PropertyType, "#" + pro.Name, pro.GetValue(behaviour, null));
		}

        TraverseAnalyze();

        for (var i = 0; i < lstAnalyze.Count; i++)
        {
            lstAnalyze[i].Alloc(serizlizer);
        }
        serizlizer.jsClassName = JSNameMgr.GetTypeFullName(behaviour.GetType());
        serizlizer.arrString = lstString.ToArray();
        serizlizer.arrObject = lstObjs.ToArray();
    }
    /// <summary>
    /// Wills the type be available in javascript.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static bool WillTypeBeAvailableInJavaScript(Type type)
    {
        return WillTypeBeTranslatedToJavaScript(type) || WillTypeBeExportedToJavaScript(type);
    }
    /// <summary>
    /// Wills the type be translated to javascript.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static bool WillTypeBeTranslatedToJavaScript(Type type)
    {
        if (type.IsDefined(typeof(JsTypeAttribute), false))
            return true;

		if (JsExternalTools.JsTypeNameSet.Contains (type.FullName))
			return true;

        return false;
    }
    /// <summary>
    /// Wills the type be exported to java script.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static bool WillTypeBeExportedToJavaScript(Type type)
    {
        return CSGenerator.ExportTypeSet.Contains(type);
    }
    /// <summary>
    /// Replace MonoBehaviour with JSComponent, only when this MonoBehaviour has JsType attribute
    /// Will copy serialized data if needed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go">GameObject to replace MonoBehaviours.</param>
    /// <returns>return true if any MonoBehaviour of go has been replaced.</returns>
    public static bool CopyGameObject(GameObject go)
    {
        bool bReplaced = false;
        var behaviours = go.GetComponents<MonoBehaviour>();
        for (var i = 0; i < behaviours.Length; i++)
        {
            var behav = behaviours[i];
            // ignore JSSerializer here
            if (behav is JSSerializer)
            {
                continue;
            }
            if (behav == null)
            {
                // Debug.Log("00000000000");
                Debug.LogWarning("There is null behaviour in gameObject \"" + go.name + "\"");
                continue;
            }

            if (WillTypeBeTranslatedToJavaScript(behav.GetType()))
            {   // if this MonoBehaviour is going to be translated to JavaScript
                // replace this behaviour with JSComponent
                // copy the serialized data if needed
                //JSSerializer helper = (JSSerializer)go.AddComponent<T>();
                JSSerializer helper = JSComponentGenerator.CreateJSComponentInstance(go, behav);
                if (helper == null)
                {
                    continue;
                }
                CopyBehaviour(behav, helper);
                bReplaced = true;
            }
        }
        return bReplaced;
    }
    /// <summary>
    /// Remove MonoBehaviour with JSComponent, only when this MonoBehaviour has JsType attribute
    /// </summary>
    /// <param name="go">GameObject to remove MonoBehaviours.</param>
    public static void RemoveOtherMonoBehaviours(GameObject go)
    {
        var coms = go.GetComponents<MonoBehaviour>();
//         var dict = new Dictionary<Type, int>(); // type -> Count
//         foreach (var com in coms)
//         {
//             object[] attrs = com.GetType().GetCustomAttributes(true);
//             foreach (var attr in attrs)
//             {
//                 if (attr is RequireComponent)
//                 {
//                     var rc = (RequireComponent)attr;
//                     var ts = new Type[] { rc.m_Type0, rc.m_Type1, rc.m_Type2 };
//                     foreach (var t in ts)
//                     {
//                         if (dict.ContainsKey(t)) dict[t]++;
//                         else dict[t] = 1;
//                     }
//                 }
//             }
//         }

        List<MonoBehaviour> lst = new List<MonoBehaviour>();
        for (var i = 0; i < coms.Length; i++)
        {
            var com = coms[i];
            if (com != null)
            {
                // ignore JSSerializer here
                if (com is JSSerializer)
                    continue;

                if (!WillTypeBeTranslatedToJavaScript(com.GetType()))
                    continue;

                bool requireOther = false;
                object[] attrs = com.GetType().GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is RequireComponent) { requireOther = true; break; }
                }
                if (requireOther)
                {
                    // delete components with [RequireComponent()] first
                    UnityEngine.Object.DestroyImmediate(com, true);
                }
                else
                {
                    lst.Add(com);
                }
            }
        }
        for (var i = 0; i < lst.Count; i++)
        {
            UnityEngine.Object.DestroyImmediate(lst[i], true);
        }
    }
}