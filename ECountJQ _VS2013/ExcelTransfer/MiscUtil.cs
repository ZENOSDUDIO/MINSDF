using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using ECount.Infrustructure.Data.Integration;
using System.ComponentModel;
using System.Globalization;
using System.Collections;

namespace ECount.Infrustructure.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// TODO: refactoring with ECount.Infrustructure.Data.Integration.ValidationUtils
    /// </remarks>
    public static class MiscUtil
    {
        public static string ResolveFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // 绝对路径
            if (File.Exists(path))
                return path;

            // 可能是相对路径

            // website
            if (System.Web.HttpContext.Current != null)
            {
                string path1 = System.Web.HttpContext.Current.Server.MapPath(path);
                if (File.Exists(path1))
                    return path1;
            }

            // winform
            string rootlocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rootlocation = rootlocation.Substring(0, rootlocation.LastIndexOf('\\') + 1);
            string path2 = Path.Combine(rootlocation, path.TrimStart('\\'));
            if (File.Exists(path2))
                return path2;

            return string.Empty;
        }

        public static string ResolveFolderPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // 绝对路径
            if (Directory.Exists(path))
                return path;

            // 可能是相对路径

            // website
            if (System.Web.HttpContext.Current != null)
            {
                string path1 = System.Web.HttpContext.Current.Server.MapPath(path);
                if (Directory.Exists(path1))
                    return path1;
            }

            // winform
            string rootlocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            rootlocation = rootlocation.Substring(0, rootlocation.LastIndexOf('\\') + 1);
            string path2 = Path.Combine(rootlocation, path.TrimStart('\\'));
            if (Directory.Exists(path2))
                return path2;

            return string.Empty;
        }

        public static T Clone<T>(T oldObj) where T : class
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                bFormatter.Serialize(stream, oldObj);
                stream.Seek(0, SeekOrigin.Begin);
                T clone = (T)bFormatter.Deserialize(stream);
                return clone;
            }
        }

        public static void EnsureColumn(DataTable dt, string columnname)
        {
            if (!dt.Columns.Contains(columnname))
                throw new Exception(string.Format("必须包含列名为'{0}'的数据列", columnname));
        }

        public static void EnsureColumns(DataTable dt, params string[] columnnames)
        {
            string names = string.Empty;
            foreach (string name in columnnames)
            {
                if (!dt.Columns.Contains(name))
                {
                    names += name + ",";
                }
            }
            if (!string.IsNullOrEmpty(names))
                throw new Exception(string.Format("必须包含以下数据列:({0})", names.TrimEnd(',')));
        }

        public static void EnsureColumnLength(DataRow dr, int rowindex, string columnname, int length)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length != length)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值[{2}]的长度必须为{3}个字符", rowindex, columnname, colvalue, length));
        }

        public static void EnsureColumnMaxLength(DataRow dr, int rowindex, string columnname, int maxlength)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length > maxlength)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值[{2}]的长度不能超过{3}个字符", rowindex, columnname, colvalue, maxlength));
        }

        public static void EnsureColumnNotEmpty(DataRow dr, int rowindex, string columnname)
        {
            string colvalue = dr[columnname].ToString();
            if (colvalue.Length == 0)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值不能为空", rowindex, columnname));
        }

        public static void EnsureColumnType<T>(DataRow dr, int rowindex, string columnname)
        {
            string colvalue = dr[columnname].ToString();
            Type targetType = typeof(T);
            if (colvalue.Length == 0 && ValidationUtils.IsTheTargetTypeAValueTypeDifferentFromString(targetType))
            {
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值'{2}'不能转换为{3}", rowindex, columnname, colvalue, targetType));
            }

            object convertedValue = null;
            try
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
                convertedValue = typeConverter.ConvertFromString(null, CultureInfo.CurrentCulture, colvalue);
            }
            catch
            {
            }

            string typename;

            if (targetType == typeof(int))
                typename = "整数";
            else if (targetType == typeof(bool))
                typename = "真假值";
            else if (targetType == typeof(decimal))
                typename = "小数";
            else if (targetType == typeof(DateTime))
                typename = "时间";
            else
                typename = targetType.ToString();

            if (convertedValue == null)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值'{2}'不能转换为{3}", rowindex, columnname, colvalue, typename));
        }

        public static void EnsureColumnDomain<T>(DataRow dr, int rowindex, string columnname, params T[] domains)
        {
            EnsureColumnType<T>(dr, rowindex, columnname);
            bool match = false;
            T colvalue = (T)dr[columnname];
            string info = string.Empty;
            foreach (T element in domains)
            {
                if (element.Equals(colvalue))
                {
                    match = true;
                }
                info += string.Format("{0},", typeof(T));
            }
            if (!match)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值'{2}'必须位于({3})", rowindex, columnname, colvalue, info.TrimEnd(',')));
        }

        public static void EnsureColumnMinValue<T>(DataRow dr, int rowindex, string columnname, T minvalue) where T : IComparable
        {
            EnsureColumnType<T>(dr, rowindex, columnname);

            T colvalue = (T)dr[columnname];
            if (colvalue.CompareTo(minvalue) < 0)
                throw new Exception(string.Format("第[{0}]行的数据列'{1}'的值'{2}'必须不小于({3})", rowindex, columnname, colvalue, minvalue));
        }

        public static string EnsureDataTableQualify(DataTable dt, string schemapath)
        {
            return EnsureDataTableQualify(dt, schemapath, new Hashtable());
        }

        public static string EnsureDataTableQualify(DataTable dt, string schemapath, Hashtable param)
        {
            if (param == null)
                param = new Hashtable();
            try
            {
                IntegrationContext context = new IntegrationContext(param);
                context.Schema = ValidationUtils.GetDataSchemaFromFile(schemapath);
                if (context.Schema == null)
                    return string.Format("无法解析出[{0}]的schema", schemapath);
                DataTableStorage.GetDataFromSourceDataTable(dt, context);
                return context.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
