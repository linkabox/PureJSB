//using SharpKit.JavaScript;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Xml;
//using Lavie;

//namespace Lavie
//{


//    public static class XmlUtils
//    {
//        public static void AssignObjectValueFromXml(this XmlNode mNode, object mData, Type mDataType)
//        {
//            foreach (XmlAttribute xmlAttribute in mNode.Attributes)
//            {
//                var fieldName = xmlAttribute.Name;
//                string value = mNode.Attributes.GetNamedItem(fieldName).Value.ToString();

//                object fieldValue = null;
//                if (jsimp.Reflection.PropertyTypeIsIntArray(mDataType, fieldName))
//                {
//                    fieldValue = ConvertString2IntArray(value);
//                }
//                else
//                {
//                    Type fieldType = jsimp.Reflection.GetPropertyType(mDataType, fieldName);
//                    if (fieldType == null)
//                    {
//                        continue;
//                    }
//                    fieldValue = ConvertString2ActualType(fieldType, value);
//                }
//                jsimp.Reflection.SetPropertyValue(mData, fieldName, fieldValue);
//            }
//        }
//        public static object CreateObjectFromXml(this XmlNode mNode, Type type)
//        {
//            object mData = jsimp.Reflection.CreateInstance(type);
//            AssignObjectValueFromXml(mNode, mData, type);
//            return mData;
//        }
//        public static T CreateObjectFromXml<T>(this XmlNode mNode)
//        {
//            T mData = jsimp.Reflection.CreateInstance<T>();
//            AssignObjectValueFromXml(mNode, mData, typeof(T));
//            return mData;
//        }
//        public static List<T> CreateObjectFromXml<T>(this XmlNodeList nodeList, string subType = "")
//        {
//            List<T> list = new List<T>();
//            foreach (XmlNode mNode in nodeList)
//            {
//                T mData = jsimp.Reflection.CreateInstance<T>();

//                // 1) basic type members
//                AssignObjectValueFromXml(mNode, mData, typeof(T));

//                // 2) object type members
//                if (subType.Length > 0 && mNode.HasChildNodes)
//                {
//                    foreach (XmlNode childNode in mNode.ChildNodes)
//                    {
//                        string fieldName = childNode.NodeValue<string>(subType);
//                        Type fieldType = jsimp.Reflection.GetPropertyType(typeof(T), fieldName);

//                        if (fieldType != null)
//                        {
//                            object fieldValue = childNode.CreateObjectFromXml(fieldType);
//                            jsimp.Reflection.SetPropertyValue(mData, fieldName, fieldValue);
//                        }
//                    }
//                }
//                list.Add(mData);
//            }
//            return list;
//        }
//        private static object ConvertString2IntArray(string value)
//        {
//            string[] arr = (value.Split(','));

//            int[] ret = new int[arr.Length];
//            for (int i = 0; i < arr.Length; i++)
//            {
//                ret[i] = int.Parse(arr[i]);
//            }

//            return ret;
//        }
//        private static object ConvertString2ActualType(Type type, string value)
//        {
//            object ret = null;
//            if (type == typeof(int))
//            {
//                ret = int.Parse(value);
//            }
//            else if (type == typeof(float))
//            {
//                ret = float.Parse(value);
//            }
//            else if (type == typeof(bool))
//            {
//                ret = (bool)(value == "1");
//            }
//            else if (type.IsEnum)
//            {
//                ret = int.Parse(value);
//            }
//            else if (type == typeof(string))
//            {
//                ret = value;
//            }
////             else if (jsimp.Reflection.TypeIsIntArray(type))
////             {
////                 string[] arr = (value.Split(','));
//// 
////                 int[] value1 = new int[arr.Length];
////                 for (int i = 0; i < arr.Length; i++)
////                 {
////                     value1[i] = int.Parse(arr[i]);
////                 }
//// 
////                 ret = value1;
////             }
//            else
//            {
//                //throw new Exception(String.Format("value{0} is not defined!", lastValue));
//            }
//            return ret;
//        }
//        public static T NodeValue<T>(this XmlNode node, string nodeName)
//        {
//            XmlAttributeCollection collection = node.Attributes;
//            if (collection.Count == 0)
//            {
//                // throw  new Exception(String.Format("node {0} have no atrributes {1}",node.Name,nodeName));
//                return default(T);
//            }

//            XmlNode namedItem = collection.GetNamedItem(nodeName);
//            if (namedItem == null)
//            {
//                return default(T);
//            }

//            Type typeT = typeof(T);
//            string value = namedItem.Value;
//            return (T) ConvertString2ActualType(typeT, value);
//        }
//        public static T Select<T>(this XmlNodeList xmlNodeList, string nodeName, string value, string attribute)
//        {
//            foreach (XmlNode node in xmlNodeList)
//            {
//                if (node.NodeValue<T>(nodeName).ToString() == value)
//                {
//                    return node.NodeValue<T>(attribute);
//                }
//            }
//            return default(T);
//        }
//        public static XmlNode Select<T>(this XmlNodeList xmlNodeList, string nodeName, T value)
//        {
//            foreach (XmlNode node in xmlNodeList)
//            {
//                T nodeValue = node.NodeValue<T>(nodeName);
//                if (jsimp.Reflection.SimpleTEquals(nodeValue, value))
//                {
//                    return node;
//                }
//            }
//            return null;
//        }
//    }
//}