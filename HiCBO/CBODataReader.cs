using System;
using System.Data;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace HiCBO
{
    public partial class CBO
    {
        /// <summary>
        /// 用dr填充一个objType对象，并返回。
        /// </summary>
        /// <param name="dr">存储对象数据的DataReader</param>
        /// <param name="objType">对象类型</param>
        /// <param name="ManageDataReader"></param>
        /// <returns>objType对象</returns>
        /// <example>
        /// <code>
        /// IDataReader dr = ...;
        /// Type type = typeof(BookDT);
        /// BookDT detal = (BookDT)FillObject(dr, type);
        /// dr.Close();
        /// </code>
        /// </example>
        public static T FillObject<T>(IDataReader dr)
        {
            // GetPropertyInfo：返回存储某类型的所有属性的集合。
            // 取得属性集合
            List<PropertyInfo> objProperties = GetPropertyInfo(typeof(T));

            // GetOrdinals：返回dr属性字段索引的数组。
            // 返回索引数组
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            // CreateObject：给objType类型的对象逐个赋值并返回。
           return CreateObject<T>(dr, objProperties, arrOrdinals);
        }

        /// <summary>
        /// 返回dr属性字段索引的数组。
        /// </summary>
        /// <param name="objProperties">属性数组[存储着dr的列字段名称的属性]</param>
        /// <param name="dr"></param>
        /// <returns>字段索引的数组</returns>
        private static int[] GetOrdinals(List<PropertyInfo> lst, IDataReader dr)
        {
            // 形成对应属性集合的整合数组
            int[] arrOrdinals = new int[lst.Count];

            for (int i = 0; i < lst.Count; i++)
            {
                arrOrdinals[i] = -1;
            }

            if (dr == null)
            {
                return arrOrdinals;
            }

            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    // GetOrdinal：返回命名字段的索引。 
                    // propertyInfo.Name：获取此成员的名称。 
                    // 该行试图返回字段名称为propertyInfo.Name的DataReader的列索引
                    arrOrdinals[i] = dr.GetOrdinal(lst[i].Name);
                }
                catch
                {
                }
            }

            // 返回命名字段索引的数组
            return arrOrdinals;
        }


        /// <summary>
        /// 给objType类型的对象逐个赋属性值并返回该对象。
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="dr">存储记录的DataReader</param>
        /// <param name="objProperties">属性集合</param>
        /// <param name="arrOrdinals">索引集合</param>
        /// <returns>objType类型对象</returns>
        private static T CreateObject<T>(IDataReader dr, List<PropertyInfo> lst, int[] arrOrdinals)
        {
            // 创建objType类型的对象
            T objObject = Activator.CreateInstance<T>();

            for (int i = 0; i < lst.Count; i++)
            {
                // 如果该属性允许写入/含有Set的属性
                if (!lst[i].CanWrite)
                {
                    continue;
                }
                // 将objValue设置为空  根据objPropertyInfo.PropertyType值
                object nullValue = Null.GetNull(lst[i]);

                // 如果索引不存在
                if (arrOrdinals[i] == -1)
                {
                    continue;
                }

                object drObjVal = dr.GetValue(arrOrdinals[i]);

                // 判断dr的第arrOrdinals[intProperty]格元素是空
                if (Information.IsDBNull(drObjVal))
                {
                    // 将给定对象的属性值设置为给定值[即相应的空值]
                    lst[i].SetValue(objObject, nullValue, null);
                    continue;
                }

                // 如果无错误，赋值，设置下一个属性
                try
                {
                    // 将给定对象的属性值设置为给定值
                    lst[i].SetValue(objObject, drObjVal, null);
                    continue;
                }
                catch
                {
                }

                // 取得相应数据类型
                Type objPropertyType = lst[i].PropertyType;

                // 如果设置不成功
                try
                {

                    // BaseType：获取当前 System.Type 直接从中继承的类型
                    // 如果类型不是枚举
                    if (!objPropertyType.BaseType.Equals(typeof(System.Enum)))
                    {
                        lst[i].SetValue(objObject, Convert.ChangeType(drObjVal, objPropertyType), null);
                        continue;
                    }

                    // BaseType：获取当前 System.Type 直接从中继承的类型
                    // 如果类型是枚举
                    //  判断dr的第arrOrdinals[intProperty]格元素是不是数字
                    if (Information.IsNumeric(drObjVal))
                    {
                        // 将给定对象的属性值设置为给定值 即第Convert.ToInt32(dr.GetValue(arrOrdinals[intProperty])个枚举值
                        ((PropertyInfo)lst[i]).SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(drObjVal)), null);
                    }
                    else
                    {
                        // 将给定对象的属性值设置为给定值
                        ((PropertyInfo)lst[i]).SetValue(objObject, System.Enum.ToObject(objPropertyType, drObjVal), null);
                    }
                }
                catch
                {
                    // 将给定对象的属性值设置为给定值
                    lst[i].SetValue(objObject, Convert.ChangeType(drObjVal, objPropertyType), null);
                }
            }

            // 返回objObject对象
            return objObject;
        }
    }
}
