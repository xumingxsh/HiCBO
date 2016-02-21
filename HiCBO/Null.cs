/// <copyright>天志  1999-2006</copyright>
/// <version>1.0</version>
/// <author>Xumr</author>
/// <email>Xumr@DOTNET.com</email>
/// <log date="2006-03-10">创建</log>

using System;
using System.Reflection;

namespace HiCBO
{
	/// <summary>
	/// 空值处理类。
	/// </summary>
	/// <remarks>
	/// 本类中提供判断对不同数据类型的变量设置空值，判断是否为空等操作。
	/// </remarks>
	public class Null
	{
		/// <summary>
		/// 构造函数。
		/// </summary>
		private Null()
		{
		}

		/// <summary>
		/// 短整数的空值。
		/// </summary>
		/// <value>-1</value>
		public static short NullShort
		{
			get
			{
				return -1;
			}
		}

		/// <summary>
		/// 整数的空值。
		/// </summary>
		/// <value>-1</value>
		public static int NullInteger
		{
			get
			{
				return -1;
			}
		}

		/// <summary>
		/// 单精度浮点数的空值。
		/// </summary>
		/// <value>Single.MinValue</value>
		public static Single NullSingle
		{
			get
			{
				return Single.MinValue;
			}
		}

		/// <summary>
		/// 浮点数的空值。
		/// </summary>
		/// <value>double.MinValue</value>
		public static double NullDouble
		{
			get
			{
				return double.MinValue;
			}
		}

		/// <summary>
		/// decimal的空值。
		/// </summary>
		/// <value>decimal.MinValue</value>
		public static decimal NullDecimal
		{
			get
			{
				return decimal.MinValue;
			}
		}

		/// <summary>
		/// DateTime的空值。
		/// </summary>
		/// <value>DateTime.MinValue</value>
		public static DateTime NullDate
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// 字符串的空值。
		/// </summary>
		/// <value>string.Empty</value>
		public static string NullString
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// 布尔数的空值。
		/// </summary>
		/// <value>false</value>
		public static bool NullBoolean
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Guid的空值。
		/// </summary>
		/// <value>Guid.Empty</value>
		public static Guid NullGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>
		/// 设置各种类型数据的空值。
		/// </summary>
		/// <param name="objValue">数据值</param>
		/// <param name="objField">数据类型</param>
		/// <remarks>如果objValue不为空，则返回objValue，否则根据objField的类型，返回相应的空值。</remarks>
		public static object SetNull(object objValue, object objField)
		{
			object ret;
			if (objValue is DBNull)
			{
				if (objField is short)
				{
					ret = NullShort;
				}
				else if (objField is int)
				{
					ret = NullInteger;
				}
				else if (objField is Single)
				{
					ret = NullSingle;
				}
				else if (objField is double)
				{
					ret = NullDouble;
				}
				else if (objField is decimal)
				{
					ret = NullDecimal;
				}
				else if (objField is DateTime)
				{
					ret = NullDate;
				}
				else if (objField is string)
				{
					ret = NullString;
				}
				else if (objField is bool)
				{
					 ret = NullBoolean;
				}
				else if (objField is Guid)
				{
					ret = NullGuid;
				}
				else
				{
					ret = null;
				}
			}
			else
			{
				ret = objValue;
			}

			return ret;
		}

		/// <summary>
		/// 设置各种类型数据的空值。
		/// </summary>
		/// <param name="objPropertyInfo">属性信息</param>
		/// <returns>空值</returns>
		public static object GetNull(PropertyInfo objPropertyInfo)
		{
            if (objPropertyInfo.PropertyType.Name.ToLower().Equals("nullable`1") ||
                objPropertyInfo.PropertyType.Name.ToLower().Equals("string"))
            {
                return null;
            }
			object ret;
			switch (objPropertyInfo.PropertyType.ToString())
			{
				case "System.Int16":
					ret = NullShort;
					break;
				case "System.Int32":
				case "System.Int64":
					ret = NullInteger;
					break;
				case "System.Single":
					ret = NullSingle;
					break;
				case "System.Double":
					ret = NullDouble;
					break;
				case "System.Decimal":
					ret = NullDecimal;
					break;
				case "System.DateTime":
					ret = NullDate;
					break;
				case "System.String":
				case "System.Char":
					ret = NullString;
					break;
				case "System.Boolean":
					ret = NullBoolean;
					break;
				case "System.Guid":
					ret = NullGuid;
					break;
				default:
					Type pType = objPropertyInfo.PropertyType;
					if (pType.BaseType.Equals(typeof(System.Enum)))
					{
						Array objEnumValues = Enum.GetValues(pType);
						Array.Sort(objEnumValues);
						ret = Enum.ToObject(pType, objEnumValues.GetValue(0));
					}
					else
					{
						ret = null;
					}
					break;
			}
			return ret;
		}

		/// <summary>
		/// 设置各种类型数据的空值。
		/// </summary>
		/// <param name="objFieldInfo">字段信息</param>
		/// <returns>空值</returns>
		public static object SetNull(FieldInfo objFieldInfo)
		{
			object ret;
			switch (objFieldInfo.FieldType.ToString())
			{
				case "System.Int16":
					ret = NullShort;
					break;
				case "System.Int32":
				case "System.Int64":
					ret = NullInteger;
					break;
				case "System.Single":
					ret = NullSingle;
					break;
				case "System.Double":
					ret = NullDouble;
					break;
				case "System.Decimal":
					ret = NullDecimal;
					break;
				case "System.DateTime":
					ret = NullDate;
					break;
				case "System.String":
				case "System.Char":
					ret = NullString;
					break;
				case "System.Boolean":
					ret = NullBoolean;
					break;
				case "System.Guid":
					ret = NullGuid;
					break;
				default:
					Type pType = objFieldInfo.FieldType;
					if (pType.BaseType.Equals(typeof(System.Enum)))
					{
						Array objEnumValues = Enum.GetValues(pType);
						Array.Sort(objEnumValues);
						ret = Enum.ToObject(pType, objEnumValues.GetValue(0));
					}
					else
					{
						ret = null;
					}
					break;
			}
			return ret;
		}

		/// <summary>
		/// 取得不为空的数据。
		/// </summary>
		/// <param name="objField">被比较数据</param>
		/// <param name="objDBNull">比较数据</param>
		/// <returns>不为空的数据</returns>
		/// <remarks>
		/// 有两个数据：objField， objDBNull；如果objField为空，则返回objDBNull，
		/// 否则，返回objDBNull。
		/// </remarks>
		public static object GetNull(object objField, object objDBNull)
		{
			object ret = objField;
			if (objField == null)
			{
				ret = objDBNull;
			}
			else if (objField is short)
			{
				if (Convert.ToInt16(objField) == NullShort)
				{
					ret = objDBNull;
				}
			}
			else if (objField is int)
			{
				if (Convert.ToInt32(objField) == NullInteger)
				{
					ret = objDBNull;
				}
			}
			else if (objField is Single)
			{
				if (Convert.ToSingle(objField) == NullSingle)
				{
					ret = objDBNull;
				}
			}
			else if (objField is double)
			{
				if (Convert.ToDouble(objField) == NullDouble)
				{
					ret = objDBNull;
				}
			}
			else if (objField is decimal)
			{
				if (Convert.ToDecimal(objField) == NullDecimal)
				{
					ret = objDBNull;
				}
			}
			else if (objField is DateTime)
			{
				if (Convert.ToDateTime(objField) == NullDate.Date)
				{
					ret = objDBNull;
				}
			}
			else if (objField is string)
			{
				if (objField == null)
				{
					ret = objDBNull;
				}
			}
			else if (objField is bool)
			{
				if (Convert.ToBoolean(objField) == NullBoolean)
				{
					ret = objDBNull;
				}
			}
			else if (objField is Guid)
			{
				if (((Guid)objField).Equals(NullGuid))
				{
					ret = objDBNull;
				}
			}

			return ret;
		}

		/// <summary>
		/// 判断是否为空。
		/// </summary>
		/// <param name="objField">要判断的数据</param>
		/// <returns>true：为空；false：不为空</returns>
		public static bool IsNull(object objField)
		{
			bool ret;
			if (objField != null)
			{
				if (objField is int)
				{
					ret = objField.Equals(NullInteger);
				}
				else if (objField is Single)
				{
					ret = objField.Equals(NullSingle);
				}
				else if (objField is double)
				{
					ret = objField.Equals(NullDouble);
				}
				else if (objField is decimal)
				{
					ret = objField.Equals(NullDecimal);
				}
				else if (objField is DateTime)
				{
					DateTime objDate = Convert.ToDateTime(objField);
					ret = objDate.Equals(NullInteger);
				}
				else if (objField is string)
				{
					ret = objField.Equals(NullString);
				}
				else if (objField is bool)
				{
					ret = objField.Equals(NullBoolean);
				}
				else if (objField is Guid)
				{
					ret = objField.Equals(NullGuid);
				}
				else
				{
					ret = false;
				}
			}
			else
			{
				ret = true;
			}
			return ret;
		}
	}
}
