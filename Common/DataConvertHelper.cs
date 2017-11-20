using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class DataConvertHelper
    {
        #region DataRow转实体
        /// <summary>
        /// DataRow转实体
        /// </summary>
        /// <typeparam name="T">数据型类</typeparam>
        /// <param name="dr">DataRow</param>
        /// <returns>模式</returns>
        public static T DataRowToModel<T>(DataRow dr) where T : new()
        {
            //T t = (T)Activator.CreateInstance(typeof(T));
            T t = new T();
            if (dr == null) return default(T);
            // 获得此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            DataColumnCollection Columns = dr.Table.Columns;
            foreach (PropertyInfo p in propertys)
            {
                string columnName = p.Name;
                //columnName = ((DBColumnAttribute)p.GetCustomAttributes(typeof(DBColumnAttribute), false)[0]).ColName;

                if (Columns.Contains(columnName))
                {
                    // 判断此属性是否有Setter或columnName值是否为空
                    object value = dr[columnName];
                    if (!p.CanWrite || value is DBNull || value == DBNull.Value) continue;
                    try
                    {
                        #region SetValue
                        switch (p.PropertyType.ToString())
                        {
                            case "System.String":
                                p.SetValue(t, Convert.ToString(value), null);
                                break;
                            case "System.Int32":
                                p.SetValue(t, Convert.ToInt32(value), null);
                                break;
                            case "System.Int64":
                                p.SetValue(t, Convert.ToInt64(value), null);
                                break;
                            case "System.DateTime":
                                p.SetValue(t, Convert.ToDateTime(value), null);
                                break;
                            case "System.Boolean":
                                p.SetValue(t, Convert.ToBoolean(value), null);
                                break;
                            case "System.Double":
                                p.SetValue(t, Convert.ToDouble(value), null);
                                break;
                            case "System.Decimal":
                                p.SetValue(t, Convert.ToDecimal(value), null);
                                break;
                            case "System.Nullable`1[System.Int32]":
                                p.SetValue(t, Convert.ToInt32(value), null);
                                break;
                            default:
                                p.SetValue(t, value, null);
                                break;
                        }
                        #endregion
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return t;
        }
        #endregion

        #region DataTable转List<T>
        /// <summary>
        /// DataTable转List<T>
        /// </summary>
        /// <typeparam name="T">数据项类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>List数据集</returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> tList = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T t = DataRowToModel<T>(dr);
                    tList.Add(t);
                }
            }
            return tList;
        }
        #endregion

        #region DataReader转实体
        /// <summary>
        /// DataReader转实体
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>实体</returns>
        public static T DataReaderToModel<T>(IDataReader dr) where T : new()
        {
            T t = new T();
            if (dr == null) return default(T);
            using (dr)
            {
                if (dr.Read())
                {
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    List<string> listFieldName = new List<string>(dr.FieldCount);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        listFieldName.Add(dr.GetName(i).ToLower());
                    }

                    foreach (PropertyInfo p in propertys)
                    {
                        string columnName = p.Name;
                        if (listFieldName.Contains(columnName.ToLower()))
                        {
                            // 判断此属性是否有Setter或columnName值是否为空
                            object value = dr[columnName];
                            if (!p.CanWrite || value is DBNull || value == DBNull.Value) continue;
                            try
                            {
                                #region SetValue
                                switch (p.PropertyType.ToString())
                                {
                                    case "System.String":
                                        p.SetValue(t, Convert.ToString(value), null);
                                        break;
                                    case "System.Int32":
                                        p.SetValue(t, Convert.ToInt32(value), null);
                                        break;
                                    case "System.DateTime":
                                        p.SetValue(t, Convert.ToDateTime(value), null);
                                        break;
                                    case "System.Boolean":
                                        p.SetValue(t, Convert.ToBoolean(value), null);
                                        break;
                                    case "System.Double":
                                        p.SetValue(t, Convert.ToDouble(value), null);
                                        break;
                                    case "System.Decimal":
                                        p.SetValue(t, Convert.ToDecimal(value), null);
                                        break;
                                    default:
                                        p.SetValue(t, value, null);
                                        break;
                                }
                                #endregion
                            }
                            catch
                            {
                                //throw (new Exception(ex.Message));
                            }
                        }
                    }
                }
            }
            return t;
        }
        #endregion

        #region DataReader转List<T>
        /// <summary>
        /// DataReader转List<T>
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>List数据集</returns>
        public static List<T> DataReaderToList<T>(IDataReader dr) where T : new()
        {
            List<T> tList = new List<T>();
            if (dr == null) return tList;
            T t1 = new T();
            // 获得此模型的公共属性
            PropertyInfo[] propertys = t1.GetType().GetProperties();
            using (dr)
            {
                List<string> listFieldName = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    listFieldName.Add(dr.GetName(i).ToLower());
                }
                while (dr.Read())
                {
                    T t = new T();
                    foreach (PropertyInfo p in propertys)
                    {
                        string columnName = p.Name;
                        if (listFieldName.Contains(columnName.ToLower()))
                        {
                            // 判断此属性是否有Setter或columnName值是否为空
                            object value = dr[columnName];
                            if (!p.CanWrite || value is DBNull || value == DBNull.Value) continue;
                            try
                            {
                                #region SetValue
                                switch (p.PropertyType.ToString())
                                {
                                    case "System.String":
                                        p.SetValue(t, Convert.ToString(value), null);
                                        break;
                                    case "System.Int32":
                                        p.SetValue(t, Convert.ToInt32(value), null);
                                        break;
                                    case "System.DateTime":
                                        p.SetValue(t, Convert.ToDateTime(value), null);
                                        break;
                                    case "System.Boolean":
                                        p.SetValue(t, Convert.ToBoolean(value), null);
                                        break;
                                    case "System.Double":
                                        p.SetValue(t, Convert.ToDouble(value), null);
                                        break;
                                    case "System.Decimal":
                                        p.SetValue(t, Convert.ToDecimal(value), null);
                                        break;
                                    default:
                                        p.SetValue(t, value, null);
                                        break;
                                }
                                #endregion
                            }
                            catch
                            {
                                //throw (new Exception(ex.Message));
                            }
                        }
                    }
                    tList.Add(t);
                }
            }
            return tList;
        }
        #endregion

        #region 泛型集合转DataTable
        /// <summary>
        /// 泛型集合转DataTable
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="entityList">泛型集合</param>
        /// <returns>DataTable</returns>
        public static DataTable ListToDataTable<T>(IList<T> entityList)
        {
            if (entityList == null) return null;
            DataTable dt = CreateTable<T>();
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in entityList)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyDescriptor property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        #endregion

        #region 创建DataTable的结构
        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            //PropertyInfo[] properties = entityType.GetProperties();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            //生成DataTable的结构
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                dt.Columns.Add(prop.Name);
            }
            return dt;
        }
        #endregion


        /// <summary>
        ///  转换成整型数字
        /// </summary>
        /// <param name="strInfo"></param>
        /// <param name="nDefault"></param>
        /// <returns></returns>
        public static int ToInt(string strInfo, int nDefault)
        {
            return ToInt(strInfo, nDefault, NumberStyles.Number);
        }

        public static decimal ToDecimal(string strInfo, decimal nDefault)
        {
            return ToDecimal(strInfo, nDefault, NumberStyles.Number);
        }
        /// <summary>
        ///  转换成整型数字
        /// </summary>
        /// <param name="strInfo"></param>
        /// <param name="nDefault"></param>
        /// <returns></returns>
        public static int ToInt(string strInfo, int nDefault, NumberStyles numStyle)
        {
            if (string.IsNullOrEmpty(strInfo))
                return nDefault;
            int nResult = 0;
            if (int.TryParse(strInfo, numStyle, null, out nResult))
                return nResult;
            else
                return nDefault;
        }


        public static decimal ToDecimal(string strInfo, decimal nDefault, NumberStyles numStyle)
        {
            if (string.IsNullOrEmpty(strInfo))
                return nDefault;
            decimal nResult = 0;
            if (decimal.TryParse(strInfo, numStyle, null, out nResult))
                return nResult;
            else
                return nDefault;
        }

        /// <summary>
        /// 验证日期格式yyyy-MM-dd
        /// </summary>
        /// <param name="StrSource"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$))");
        }

        public static int GetYesOrNoValue(string content)
        {
            var result = 0;
            if (!string.IsNullOrEmpty(content))
            {
                if (content.Equals("是"))
                {
                    result = 1;
                }
                else
                {
                    var lowc = content.ToLower();
                    if (lowc.Equals("y") || lowc.Equals("yes"))
                    {
                        result = 1;
                    }
                }
            }
            return result;
        }
    }
}
