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
        /// 根据DataRow的值填充数据对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <returns>出错的列索引，如果为-1，则成功;1000:列不确定</returns>
        public static bool UpdateObj<T>(T obj, DataRow dr)
        {
            Type objType = typeof(T);
            // 循环遍历属性集成
            foreach (PropertyInfo objProperty in objType.GetProperties())
            {
                int index = dr.Table.Columns.IndexOf(objProperty.Name);
                if (index < 0)
                {
                    continue;
                }
                string value = Convert.ToString(dr[index]);

                // 如果不是日期和字符串，而值为空，则跳过
                if ((value == null || value.Trim() == "") &&
                    objProperty.GetType() != typeof(System.String) &&
                    objProperty.GetType() != typeof(System.DateTime))
                {
                    continue;
                }

                if (!SetValue(obj, objProperty, value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
