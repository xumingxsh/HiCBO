using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace HiCBO
{
    /// <summary>
    /// 根据数据源填充对象的类。
    /// </summary>
    public partial class CBO
    {
        private CBO()
        {
        }

        /// <summary>
        /// 根据属性名称，取得对应的值。
        /// </summary>
        /// <param name="objVal">赋值对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>true:属性存在；false:属性不存在</returns>
        public delegate bool OnGetObjectHandler(ref object objVal, string propertyName);


        /// <summary>
        /// 根据DataRow的值填充数据对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <returns>出错的列索引，如果为-1，则成功;1000:列不确定</returns>
        public static bool FillObject<T>(T obj, OnGetObjectHandler handler)
        {
            // 循环遍历属性集成
            foreach (PropertyInfo it in typeof(T).GetProperties())
            {
                // 如果该属性允许写入/含有Set的属性
                if (!it.CanWrite)
                {
                    continue;
                }

                object objVal = null;
                if (!handler(ref objVal, it.Name))
                {
                    continue;
                }
                SetValue(obj, objVal, it);
            }
            return true;
        }

        /// <summary>
        /// 根据DataRow的值填充数据对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <returns>出错的列索引，如果为-1，则成功;1000:列不确定</returns>
        public static bool FillObject<T>(T obj, DataRowView dv)
        {
            DataRow dr = dv.Row;

            // 返回对象
            return FillObject(obj, dr);
        }

        /// <summary>
        /// 根据DataRow的值填充数据对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <returns>出错的列索引，如果为-1，则成功;1000:列不确定</returns>
        public static bool FillObject<T>(T obj, DataRow dr)
        {
            // 循环遍历属性集成
            foreach (PropertyInfo it in typeof(T).GetProperties())
            {
                // 如果该属性允许写入/含有Set的属性
                if (!it.CanWrite)
                {
                    continue;
                }

                if (!dr.Table.Columns.Contains(it.Name))
                {
                    continue;
                }

                object drObj = dr[it.Name];
                SetValue(obj, drObj, it);
            }
            return true;
        }

        /// <summary>
        /// 根据属性名称，设置属性值。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">赋值对象</param>
        /// <param name="val">值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>true：成功；false：属性不存在</returns>
        public static bool SetValue<T>(T t, object val, string propertyName)
        {
            // 循环遍历属性集成
            foreach (PropertyInfo it in typeof(T).GetProperties())
            {
                if (propertyName.ToLower().Equals(it.Name.ToLower()))
                {
                    SetValue(t, val, it);
                    return true;
                }
            }
            return false;
        }

        private static void SetValue(object obj, object value, PropertyInfo property)
        {
            Type type = property.PropertyType;
            if (value is DBNull || value == null ||
                (type != typeof(string) && value is string && Convert.ToString(value).Trim().Equals("")))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    property.SetValue(obj, null);
                    return;
                }
                else if (type.BaseType != typeof(System.ValueType))
                {
                    property.SetValue(obj, null);
                    return;
                }
                return;
            }

            if (type.FullName == typeof(Int16).FullName ||
                type.FullName == typeof(Int16?).FullName)
            {
                property.SetValue(obj, Convert.ToInt16(value), null);
                return;
            }

            if (type.FullName == typeof(Int64).FullName ||
                type.FullName == typeof(Int64?).FullName)
            {
                property.SetValue(obj, Convert.ToInt64(value), null);
                return;
            }

            if (type.FullName == typeof(int).FullName ||
                type.FullName == typeof(int?).FullName)
            {
                property.SetValue(obj, Convert.ToInt32(value), null);
                return;
            }

            if (type.FullName == typeof(UInt32).FullName ||
                type.FullName == typeof(UInt32?).FullName)
            {
                property.SetValue(obj, Convert.ToUInt32(value), null);
                return;
            }

            if (type.FullName == typeof(UInt16).FullName ||
                type.FullName == typeof(UInt16?).FullName)
            {
                property.SetValue(obj, Convert.ToUInt16(value), null);
                return;
            }

            if (type.FullName == typeof(UInt64).FullName ||
                type.FullName == typeof(UInt64?).FullName)
            {
                property.SetValue(obj, Convert.ToUInt64(value), null);
                return;
            }

            if (type.FullName == typeof(DateTime).FullName ||
                type.FullName == typeof(DateTime?).FullName)
            {
                property.SetValue(obj, Convert.ToDateTime(value), null);
                return;
            }

            if (type.FullName == typeof(Decimal).FullName ||
                type.FullName == typeof(Decimal?).FullName)
            {
                property.SetValue(obj, Convert.ToDecimal(value), null);
                return;
            }

            if (type.FullName == typeof(double).FullName ||
                type.FullName == typeof(double?).FullName)
            {
                property.SetValue(obj, Convert.ToDouble(value), null);
                return;
            }

            if (type.FullName == typeof(float).FullName ||
                type.FullName == typeof(float?).FullName)
            {
                property.SetValue(obj, Convert.ToSingle(value), null);
                return;
            }

            if (type.FullName == typeof(bool).FullName ||
                type.FullName == typeof(bool?).FullName)
            {
                property.SetValue(obj, Convert.ToBoolean(value), null);
                return;
            }

            if (type.FullName == typeof(string).FullName)
            {
                if (value is String)
                {
                    property.SetValue(obj, value, null);
                }
                else
                {
                    property.SetValue(obj, Convert.ToString(value), null);
                }
                return;
            }

            if (type.BaseType == typeof(System.Enum))
            {
                if (value is DBNull || value == null)
                {
                    return;
                }

                try
                {
                    int val = Convert.ToInt32(value);
                    property.SetValue(obj, Enum.ToObject(type, val), null);
                }
                catch(Exception ex)
                {
                    ex.ToString();
                    return;
                }
                return;
            }

            try
            {
                property.SetValue(obj, value, null);
            }
            catch(Exception ex)
            {
                ex.ToString();
                try
                {
                    property.SetValue(obj, value, null);
                }
                catch (Exception e)
                {
                    e.ToString();
                    property.SetValue(obj, null, null);
                }
            }
            return;
        }
    }
}