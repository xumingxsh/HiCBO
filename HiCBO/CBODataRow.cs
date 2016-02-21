﻿using System;
using System.Data;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace HiCBO
{
    public partial class CBO
    {


        /// <summary>
        /// 根据字段名成取得字段列的索引号。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dcl"></param>
        /// <returns></returns>
        public static int GetIndexForDataTable(string name, DataColumnCollection dcl)
        {
            int count = dcl.Count;
            for (int i = 0; i < count; i++)
            {
                if (string.Compare(dcl[i].ColumnName, name, true) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 返回dr属性字段索引的数组。
        /// </summary>
        /// <param name="objProperties">属性数组[存储着dr的列字段名称的属性]</param>
        /// <param name="dr"></param>
        /// <returns>字段索引的数组</returns>
        public static int[] GetOrdinals(List<PropertyInfo> lst, DataRow dr)
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

            DataColumnCollection dcl = dr.Table.Columns;
            for (int i = 0; i < lst.Count; i++)
            {
                arrOrdinals[i] = -1;
                try
                {
                    // GetOrdinal：返回命名字段的索引。 
                    // propertyInfo.Name：获取此成员的名称。 
                    // 该行试图返回字段名称为propertyInfo.Name的DataReader的列索引
                    arrOrdinals[i] = GetIndexForDataTable(lst[i].Name, dcl);
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
        /// <param name="dr">存储记录的DataRow</param>
        /// <param name="objProperties">属性集合</param>
        /// <param name="arrOrdinals">索引集合</param>
        /// <returns>objType类型对象</returns>
        private static object CreateObject(Type objType, DataRow dr, List<PropertyInfo> lst, int[] arrOrdinals)
        {
            // 创建objType类型的对象
            object objObject = Activator.CreateInstance(objType);

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
				int typeIndex = arrOrdinals[i];	// 类型索引
				if (typeIndex == -1)
				{
					continue;
				}
				
				object drObj = dr[typeIndex];

				// 判断dr的第arrOrdinals[intProperty]格元素是空
				if (Information.IsDBNull(drObj))
				{
					// 将给定对象的属性值设置为给定值[即相应的空值]
					lst[i].SetValue(objObject, nullValue, null);
					continue;
				}

				// 如果无错误，赋值，设置下一个属性
				try
				{
					// 将给定对象的属性值设置为给定值
					lst[i].SetValue(objObject, drObj, null);
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
						lst[i].SetValue(objObject, Convert.ChangeType(drObj, objPropertyType), null);
						continue;
					}

					// BaseType：获取当前 System.Type 直接从中继承的类型
					// 如果类型是枚举
					//  判断dr的第arrOrdinals[intProperty]格元素是不是数字
					if (Information.IsNumeric(drObj))
					{
						// 将给定对象的属性值设置为给定值 即第Convert.ToInt32(dr[arrOrdinals[intProperty]]个枚举值
						lst[i].SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(drObj)), null);
					}
					else
					{
						// 将给定对象的属性值设置为给定值
						lst[i].SetValue(objObject, System.Enum.ToObject(objPropertyType, drObj), null);
					}
				}
				catch
				{
					// 将给定对象的属性值设置为给定值
					lst[i].SetValue(objObject, Convert.ChangeType(drObj, objPropertyType), null);
				}
            }

            // 返回objObject对象
            return objObject;
        }

        /// <summary>
        /// 用dr填充一个objType对象，并返回。
        /// </summary>
        /// <param name="dr">存储对象数据的DataRow</param>
        /// <param name="objType">对象类型</param>
        /// <returns>objType对象</returns>
        public static object FillObject(DataRow dr, Type objType)
        {
            object objFillObject;

            // GetPropertyInfo：返回存储某类型的所有属性的集合。
            // 取得属性集合
            List<PropertyInfo> objProperties = GetPropertyInfo(objType);

            // GetOrdinals：返回dr属性字段索引的数组。
            // 返回索引数组
            int[] arrOrdinals = GetOrdinals(objProperties, dr);

            bool Continue = true;


            if (Continue)
            {
                // CreateObject：给objType类型的对象逐个赋值并返回。
                objFillObject = CreateObject(objType, dr, objProperties, arrOrdinals);
            }
            else
            {
                objFillObject = null;
            }

            // 返回对象
            return objFillObject;
        }

    }
}