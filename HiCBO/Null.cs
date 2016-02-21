/// <copyright>��־  1999-2006</copyright>
/// <version>1.0</version>
/// <author>Xumr</author>
/// <email>Xumr@DOTNET.com</email>
/// <log date="2006-03-10">����</log>

using System;
using System.Reflection;

namespace HiCBO
{
	/// <summary>
	/// ��ֵ�����ࡣ
	/// </summary>
	/// <remarks>
	/// �������ṩ�ж϶Բ�ͬ�������͵ı������ÿ�ֵ���ж��Ƿ�Ϊ�յȲ�����
	/// </remarks>
	public class Null
	{
		/// <summary>
		/// ���캯����
		/// </summary>
		private Null()
		{
		}

		/// <summary>
		/// �������Ŀ�ֵ��
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
		/// �����Ŀ�ֵ��
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
		/// �����ȸ������Ŀ�ֵ��
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
		/// �������Ŀ�ֵ��
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
		/// decimal�Ŀ�ֵ��
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
		/// DateTime�Ŀ�ֵ��
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
		/// �ַ����Ŀ�ֵ��
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
		/// �������Ŀ�ֵ��
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
		/// Guid�Ŀ�ֵ��
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
		/// ���ø����������ݵĿ�ֵ��
		/// </summary>
		/// <param name="objValue">����ֵ</param>
		/// <param name="objField">��������</param>
		/// <remarks>���objValue��Ϊ�գ��򷵻�objValue���������objField�����ͣ�������Ӧ�Ŀ�ֵ��</remarks>
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
		/// ���ø����������ݵĿ�ֵ��
		/// </summary>
		/// <param name="objPropertyInfo">������Ϣ</param>
		/// <returns>��ֵ</returns>
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
		/// ���ø����������ݵĿ�ֵ��
		/// </summary>
		/// <param name="objFieldInfo">�ֶ���Ϣ</param>
		/// <returns>��ֵ</returns>
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
		/// ȡ�ò�Ϊ�յ����ݡ�
		/// </summary>
		/// <param name="objField">���Ƚ�����</param>
		/// <param name="objDBNull">�Ƚ�����</param>
		/// <returns>��Ϊ�յ�����</returns>
		/// <remarks>
		/// ���������ݣ�objField�� objDBNull�����objFieldΪ�գ��򷵻�objDBNull��
		/// ���򣬷���objDBNull��
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
		/// �ж��Ƿ�Ϊ�ա�
		/// </summary>
		/// <param name="objField">Ҫ�жϵ�����</param>
		/// <returns>true��Ϊ�գ�false����Ϊ��</returns>
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
