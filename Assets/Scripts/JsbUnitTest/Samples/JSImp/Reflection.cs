using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

/// <summary>
/// 
/// 
/// All classes in namespace 'jsimp' have separate implement in C# and JavaScript
/// These classes also need 'JsType' attribute.
/// 
/// 
/// </summary>
namespace jsimp
{

    public class Reflection
	{
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"// 这2个函数，如果T是C#类型，比如说 GameObject，是否仍然有效？
        	// 答案是：有点神奇，我认为是有效的！
        	// 得测试一下
			var ret = new T();
            return ret;")]
        public static T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"return new type._JsType.ctor();")]
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"if (obj != null) {
                //if (obj.hasOwnProperty(fieldName))
                {
                    obj[fieldName] = value;
                    return true;
                }
            }
            return false;")]
        public static bool SetFieldValue(object obj, string fieldName, object value)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                FieldInfo field = type.GetField(fieldName);
                if (field != null)
                {
                    field.SetValue(obj, value);
                    return true;
                }
            }
            return false;
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"if (type != null) {
                var typeStr = type._JsType.ctor.prototype[fieldName + ""$$""];
                //print(type.fullname + ""."" + fieldName + "" = "" + typeStr);
                if (typeStr != undefined) {
                    if (typeStr == ""System.Int32[]"") {
                        //return Int32Array;
                        var fieldType = Typeof(Int32Array);
                        //print(fieldType.fullname);
                        print(""[] "" + fieldType)
                        return fieldType;
                    } else {
                        var fieldType = Typeof(typeStr);
                        //print(fieldType.fullname);
                        return fieldType;
                    }
                }
            }
            return null;")]
        public static Type GetFieldType(Type type, string fieldName)
        {
            if (type != null)
            {
                FieldInfo field = type.GetField(fieldName);
                if (field != null)
                {
                    return field.FieldType;
                }
            }
            return null;
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"return this.SetFieldValue(obj, ""_"" + propertyName, value);")]
        public static bool SetPropertyValue(object obj, string propertyName, object value)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null)
                {
                    property.SetValue(obj, value, null);
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// </summary>
		[JsMethod(Code = @"return this.GetFieldType(type, propertyName);")]
        public static Type GetPropertyType(Type type, string propertyName)
        {
            if (type != null)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null)
                {
                    return property.PropertyType;
                }
            }
            return null;
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"if (type != null) {
                var typeStr = type._JsType.ctor.prototype[propertyName + ""$$""];
                return typeStr == ""System.Int32[]"";
            }
            return false;")]
        public static bool PropertyTypeIsIntArray(Type type, string propertyName)
        {
            if (type != null)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                if (property != null)
                {
                    return (property.PropertyType == typeof(int[]));
                }
            }
            return false;
		}
		
		/// <summary>
		/// </summary>
        // in JavaScript, it will be simply
        // return (a == b);
        // call this function only when it's OK for JavaScript to do (a == b)
		[JsMethod(Code = @"return (a == b);")]
        public static bool SimpleTEquals<T>(T a, T b)
        {
            return a.Equals(b);
		}
		
		/// <summary>
		/// </summary>
		[JsMethod(Code = @"return type._JsType == Int32Array;")]
        public static bool TypeIsIntArray(Type type)
        {
            return type == typeof(int[]);
		}
		
		/// <summary>
		/// </summary>
		// 调用obj的函数，这个函数没有返回值，有任意个参数。
		[JsMethod(Code = @"var args = Array.prototype.slice.apply(arguments);
			var obj = args[0];
			var methodName = args[1];
            obj[methodName].apply(obj, args.slice(2));
            return true;")]
		public static bool CallObjMethod(object obj, string methodName, params object[] parameters)
		{
			MethodInfo mi = obj.GetType().GetMethod(methodName);
			if (mi != null)
			{
				mi.Invoke(obj, parameters);
				return true;
			}
			return false;
		}
    }
}

