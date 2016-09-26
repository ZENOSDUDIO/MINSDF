using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.ComponentModel;

namespace SGM.ECountJQ.UPG.BLL.DBBase
{
    [Serializable]
    public class Entity<T>
    {
        [NonSerialized]
        private static Database _Database;

        private static List<FieldItem> CheckColumn(Type type, DataTable dt)
        {
            List<FieldItem> list = new List<FieldItem>();
            foreach (FieldItem item in FieldItem.GetDataObjectFields(type))
            {
                if (dt.Columns.Contains(item.ColumnName))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static T FindByIdentity(object ideneity)
        {
            Type type = typeof(T);
            FieldItem identityField = FieldItem.GetIdentityField(type);
            if (identityField == null)
            {
                return default(T);
            }

            string sql = string.Format("Select * From [{0}] WHERE {1}=@{1}", FieldItem.GetTableName(type), identityField.ColumnName);
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@" + identityField.ColumnName, ideneity);
            DataSet ds = Database.ExecuteDataSet(cmd);
            List<T> list = new List<T>();
            LoadData(list, ds);
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            else
            {
                return list[0];
            }
        }

        public static List<T> FindAll()
        {
            List<T> list = new List<T>();
            DataSet ds = Query("SELECT * FROM [" + FieldItem.GetTableName(typeof(T)) + "]");
            LoadData(list, ds);

            return list;
        }

        public static List<T> FindAllByWhere(string where)
        {
            if (string.IsNullOrEmpty(where))
            {
                where = "1=1";
            }

            List<T> list = new List<T>();
            DataSet ds = Query("SELECT * FROM [" + FieldItem.GetTableName(typeof(T)) + "] WHERE " + where);
            LoadData(list, ds);

            return list;
        }

        public static List<T> FindAllBySql(string sql)
        {
            List<T> list = new List<T>();
            DataSet ds = Query(sql);
            LoadData(list, ds);

            return list;
        }

        public static List<T> FindAllByPage(out int total, int pageSize, int pageIndex, out int pageCount, string where, PageOrder order)
        {
            if (string.IsNullOrEmpty(where))
            {
                where = " 1 = 1";
            }
            string orderStr = string.Empty;
            if (order != null && order.Name != null && !string.IsNullOrEmpty(order.Name))
            {
                orderStr = string.Format(@" ORDER BY {0} {1}", order.Name, order.Direction.ToString());
            }
            FieldItem identityField = FieldItem.GetIdentityField(typeof(T));
            if (identityField == null || string.IsNullOrEmpty(identityField.ColumnName))
            {
                throw new Exception("分页查询需要设置一个Identity列");
            }

            string sql = string.Empty;
            pageIndex = pageIndex - 1;
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageIndex == 0)
            {
                sql = string.Format(@"SELECT TOP({1})* FROM {0} WHERE {2} {3}",
                     FieldItem.GetTableName(typeof(T)), pageSize, where, orderStr);
            }
            else
            {
                sql = string.Format(@"SELECT TOP({3})* FROM {0} WHERE ({1} > (SELECT MAX({1}) FROM (SELECT TOP {4}({1}) FROM {0} ORDER BY {1})AS T)) AND {2} {5}",
                                    FieldItem.GetTableName(typeof(T)), identityField.ColumnName, where, pageSize, pageSize * pageIndex, orderStr);
            }
            total = QueryCount(where);
            pageCount = total / pageSize + (total % pageSize == 0 ? 0 : 1);

            List<T> list = new List<T>();
            DataSet ds = Query(sql);
            LoadData(list, ds);

            return list;
        }

        public static List<T> FindAllByPage(Pager pager, string where)
        {
            if (string.IsNullOrEmpty(where))
            {
                where = " 1 = 1";
            }

            FieldItem identityField = FieldItem.GetIdentityField(typeof(T));
            if (identityField == null || string.IsNullOrEmpty(identityField.ColumnName))
            {
                throw new Exception("分页查询需要设置一个Identity列");
            }

            if (pager.Order == null || string.IsNullOrEmpty(pager.Order.Name))
            {
                pager.Order = new PageOrder();
                pager.Order.Name = identityField.ColumnName;
                pager.Order.Direction = OrderDirection.Desc;
            }

            pager.TotalCount = QueryCount(where);
            int pageCount = pager.TotalCount / pager.PageSize + (pager.TotalCount % pager.PageSize == 0 ? 0 : 1);
            if (pageCount != 0 && pager.PageIndex > pageCount)
            {
                pager.PageIndex = pageCount;
            }

            string sql = string.Format(@"SELECT A.* FROM [{0}] AS A INNER JOIN(SELECT ROWNUM,[{1}] FROM(SELECT ROW_NUMBER() OVER(ORDER BY [{2}] {3}) AS ROWNUM,[{1}] FROM [{0}] WHERE {6}) AS T 
                                  WHERE ROWNUM BETWEEN {4} AND {5})AS B ON A.[{1}] = B.[{1}] ORDER BY B.ROWNUM",
                  FieldItem.GetTableName(typeof(T)), identityField.ColumnName, pager.Order.Name, pager.Order.Direction.ToString(), pager.PageSize * (pager.PageIndex - 1) + 1, pager.PageSize * pager.PageIndex, where);

            List<T> list = new List<T>();
            DataSet ds = Query(sql);
            LoadData(list, ds);

            return list;
        }

        public static int QueryCount()
        {
            return QueryCount(FieldItem.GetTableName(typeof(T)), string.Empty);
        }

        public static int QueryCount(string where)
        {
            return QueryCount(FieldItem.GetTableName(typeof(T)), where);
        }

        public static int QueryCount(string tableName, string where)
        {
            int count = 0;

            if (!string.IsNullOrEmpty(tableName))
            {
                if (string.IsNullOrEmpty(where))
                {
                    where = " 1=1";
                }
                object val = Database.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(*) FROM [{0}] WHERE {1}", tableName, where));
                if (val != null)
                {
                    count = Convert.ToInt32(val);
                }
            }

            return count;
        }

        protected static void LoadData(List<T> list, DataSet ds)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            if (ds != null && ds.Tables != null && ds.Tables.Count != 0)
            {
                LoadData(list, ds.Tables[0]);
            }
        }

        protected static void LoadData(List<T> list, DataTable dt)
        {
            if (list == null)
            {
                list = new List<T>();
            }

            if (dt.Rows != null && dt.Rows.Count != 0)
            {
                List<FieldItem> fs = CheckColumn(typeof(T), dt);
                foreach (DataRow row in dt.Rows)
                {
                    T obj = Activator.CreateInstance<T>();
                    LoadData(obj, row, fs);
                    if (obj != null)
                    {
                        list.Add(obj);
                    }
                }
            }
        }

        private static void LoadData(T obj, DataRow dr, List<FieldItem> fs)
        {
            foreach (FieldItem item in fs)
            {
                object val = dr[item.ColumnName];
                typeof(T).GetProperty(item.ColumnName).SetValue(obj, (val == DBNull.Value) ? null : val, null);
            }
        }

        private static DataSet Query(string sql)
        {
            return Database.ExecuteDataSet(CommandType.Text, sql);
        }

        public virtual int Insert()
        {
            Type type = base.GetType();
            IList<FieldItem> dataObjectFields = FieldItem.GetDataObjectFields(type);
            string identityName = string.Empty;

            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (FieldItem item in dataObjectFields)
            {
                if ((item.DataObjectField != null) && item.DataObjectField.IsIdentity)
                {
                    identityName = item.Name;
                }
                else
                {
                    if (sb1.Length > 0)
                    {
                        sb1.Append(",");
                    }
                    if (sb2.Length > 0)
                    {
                        sb2.Append(",");
                    }
                    sb1.AppendFormat("[{0}]", item.ColumnName);
                    sb2.Append("@" + item.ColumnName);
                    cmd.Parameters.AddWithValue("@" + item.ColumnName, type.GetProperty(item.ColumnName).GetValue(this, null));
                }
            }

            string sql = string.Format("INSERT INTO {0}({1}) VALUES({2}) SELECT @@IDENTITY AS ReturnValue",
FieldItem.GetTableName(type), sb1.ToString(), sb2.ToString());
            cmd.CommandText = sql;

            object identity = Database.ExecuteScalar(cmd);
            int identityValue = Convert.ToInt32(identity);
            if (!string.IsNullOrEmpty(identityName) && identityValue != 0)
            {
                type.GetProperty(identityName).SetValue(this, identityValue, null);
            }

            return identityValue;
        }

        public virtual int Update()
        {
            Type type = base.GetType();

            FieldItem identityField = FieldItem.GetIdentityField(type);
            if (identityField == null)
            {
                throw new Exception("使用该方法需要配置Identity字段");
            }

            IList<FieldItem> dataObjectFields = FieldItem.GetDataObjectFields(type);

            SqlCommand cmd = new SqlCommand();
            StringBuilder sb = new StringBuilder();

            foreach (FieldItem item in dataObjectFields)
            {
                if ((item.DataObjectField != null) && !item.DataObjectField.IsIdentity)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }
                    sb.AppendFormat("[{0}]=@{0}", item.ColumnName);

                    cmd.Parameters.AddWithValue("@" + item.ColumnName, type.GetProperty(item.ColumnName).GetValue(this, null));
                }
            }

            string sql = string.Format(@"UPDATE {0} SET {1} WHERE [{2}]=@{2}", FieldItem.GetTableName(type), sb.ToString(), identityField.ColumnName);
            cmd.Parameters.AddWithValue("@" + identityField.ColumnName, type.GetProperty(identityField.ColumnName).GetValue(this, null));

            cmd.CommandText = sql;

            return Database.ExecuteNonQuery(cmd);
        }

        public virtual int Delete()
        {
            Type type = base.GetType();
            IList<FieldItem> dataObjectFields = FieldItem.GetDataObjectFields(type);

            string sql = string.Empty;
            SqlCommand cmd = new SqlCommand();
            foreach (FieldItem item in dataObjectFields)
            {
                if ((item.DataObjectField != null) && item.DataObjectField.IsIdentity)
                {
                    sql = string.Format(@"DELETE FROM {0} WHERE {1} = @{1}", FieldItem.GetTableName(type), item.ColumnName);
                    cmd.Parameters.AddWithValue("@" + item.ColumnName, type.GetProperty(item.Name).GetValue(this, null));
                }
            }

            if (!string.IsNullOrEmpty(sql))
            {
                cmd.CommandText = sql;
                return Database.ExecuteNonQuery(cmd);
            }
            else
            {
                return 0;
            }
        }

        public static int Delete(object identity)
        {
            Type type = typeof(T);
            IList<FieldItem> dataObjectFields = FieldItem.GetDataObjectFields(type);

            string sql = string.Empty;
            SqlCommand cmd = new SqlCommand();
            foreach (FieldItem item in dataObjectFields)
            {
                if ((item.DataObjectField != null) && item.DataObjectField.IsIdentity)
                {
                    sql = string.Format(@"DELETE FROM {0} WHERE {1} = @{1}", FieldItem.GetTableName(type), item.ColumnName);
                    cmd.Parameters.AddWithValue("@" + item.ColumnName, identity);
                }
            }

            if (!string.IsNullOrEmpty(sql))
            {
                cmd.CommandText = sql;
                return Database.ExecuteNonQuery(cmd);
            }
            else
            {
                return 0;
            }
        }

        protected static int Execute(string sql)
        {
            return Database.ExecuteNonQuery(CommandType.Text, sql);
        }

        protected static string SqlDataFormat(object obj, Type type, Type entityType)
        {
            string fullName = type.FullName;
            if (fullName.Contains("String"))
            {
                if (obj == null)
                {
                    return "null";
                }
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    return "''";
                }
                return ("'" + obj.ToString().Replace("'", "''") + "'");
            }
            if (fullName.Contains("DateTime"))
            {
                if (obj == null)
                {
                    return "''";
                }
                try
                {
                    if ((Convert.ToDateTime(obj) < new DateTime(0x6d9, 1, 1)) || (Convert.ToDateTime(obj) > new DateTime(0x270f, 1, 1)))
                    {
                        return "''";
                    }
                }
                catch
                {
                }
                DateTime time = (DateTime)obj;
                return ("'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (fullName.Contains("Boolean"))
            {
                if (obj == null)
                {
                    return "";
                }
                if (!Convert.ToBoolean(obj))
                {
                    return "0";
                }
                return "1";
            }
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        protected static DataSet ExecuteDataSet(string procedureName, params object[] parameters)
        {
            return Database.ExecuteDataSet(procedureName, parameters);
        }

        [XmlIgnore]
        private static Database Database
        {
            get
            {
                if (_Database == null)
                {
                    MapTableAttribute table = FieldItem.GetTable(typeof(T));
                    if ((table != null) && !string.IsNullOrEmpty(table.ConnName))
                    {
                        _Database = DatabaseFactory.CreateDatabase(table.ConnName);
                    }
                }
                return _Database;
            }
            set
            {
                _Database = value;
            }
        }
    }
}
