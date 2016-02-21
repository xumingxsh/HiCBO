using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace HiCBO
{
    public partial class CBO
    {
        private static bool SetValue(object obj, PropertyInfo objProperty, string value)
        {
            if (!objProperty.CanWrite)
            {
                return true;
            }
            Type type = objProperty.PropertyType;
            string name = type.Name.ToLower();

            // 如果值为空，且属性允许为空，则设置为空，否则，就是赋值失败
            if (value == "")
            {
                if (name == "nullable`1" || name == "string")
                {
                    if (objProperty.GetValue(obj, null) != null)
                    {
                        objProperty.SetValue(obj, null, null);
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }

            if (type.FullName == typeof(Int16).FullName ||
                type.FullName == typeof(Int16?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToInt32(value), null);
                return true;
            }

            if (type.FullName == typeof(UInt16).FullName ||
                type.FullName == typeof(UInt16?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToUInt32(value), null);
                return true;
            }

            if (type.FullName == typeof(int).FullName ||
                type.FullName == typeof(int?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToInt32(value), null);
                return true;
            }

            if (type.FullName == typeof(UInt32).FullName ||
                type.FullName == typeof(UInt32?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToUInt32(value), null);
                return true;
            }

            if (type.FullName == typeof(Int64).FullName ||
                type.FullName == typeof(Int64?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToInt64(value), null);
                return true;
            }

            if (type.FullName == typeof(UInt64).FullName ||
                type.FullName == typeof(UInt64?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToUInt64(value), null);
                return true;
            }

            if (type.FullName == typeof(DateTime).FullName ||
                type.FullName == typeof(DateTime?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToDateTime(value), null);
                return true;
            }

            if (type.FullName == typeof(Decimal).FullName ||
                type.FullName == typeof(Decimal?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToDecimal(value), null);
                return true;
            }

            if (type.FullName == typeof(double).FullName ||
                type.FullName == typeof(double?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToDouble(value), null);
                return true;
            }

            if (type.FullName == typeof(float).FullName ||
                type.FullName == typeof(float?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToSingle(value), null);
                return true;
            }

            if (type.FullName == typeof(bool).FullName ||
                type.FullName == typeof(bool?).FullName)
            {
                objProperty.SetValue(obj, Convert.ToBoolean(value), null);
                return true;
            }

            if (type.FullName == typeof(string).FullName)
            {
                objProperty.SetValue(obj, value, null);
                return true;
            }
            return true;
        }

        /// <summary>
        /// 返回存储某类型[objType]的所有属性（property）的集合。
        /// </summary>
        /// <param name="objType">类型（类、接口、枚举等）</param>
        /// <returns>属性集合</returns>
        private static List<PropertyInfo> GetPropertyInfo(Type objType)
        {
            List<PropertyInfo> objProperties = new List<PropertyInfo>();

            // PropertyInfo：发现属性的特性并提供对属性元数据的访问。 
            // GetProperties：返回当前Type的所有公共属性。
            foreach (PropertyInfo objProperty in objType.GetProperties())
            {
                // 用属性填充集合
                objProperties.Add(objProperty);
            }

            // 返回类型集合
            return objProperties;
        }
    }
}
