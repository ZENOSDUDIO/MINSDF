using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using Microsoft.Data.Extensions;
using System.Data.Metadata.Edm;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace SGM.ECount.BLL
{
    public class PartRepairRecordBLL : BaseGenericBLL<PartRepairRecord>
    {
        public PartRepairRecordBLL()
            : base("PartRepairRecord")
        {

        }

        public IQueryable<PartRepairRecord> QueryPartRepairRecords(PartRepairRecord filter)
        {
            IQueryable<PartRepairRecord> query = Context.PartRepairRecord.Include("Part.Plant").Include("Part.Supplier").Include("Supplier");
            if (filter != null)
            {
                if (filter.Part != null)
                {
                    if (filter.Part.PartID!=DefaultValue.INT)
                    {
                        query = query.Where(p => p.Part.PartID == filter.Part.PartID);
                    }
                    if (filter.Part.Plant != null && filter.Part.Plant.PlantID != DefaultValue.INT)
                    {
                        int plantID = filter.Part.Plant.PlantID;
                        query = query.Where(r => r.Part.Plant.PlantID == plantID);
                    }
                    if (!string.IsNullOrEmpty(filter.Part.PartCode))
                    {
                        string partCode = filter.Part.PartCode;
                        query = query.Where(r => r.Part.PartCode == partCode);
                    }
                }
                if (filter.Part != null && filter.Part.Supplier != null && !string.IsNullOrEmpty(filter.Part.Supplier.DUNS))
                {
                    string duns = filter.Part.Supplier.DUNS;
                    query = query.Where(r => r.Part.Supplier.DUNS == duns);
                }
                if (filter.Supplier != null)
                {
                    if (!string.IsNullOrEmpty(filter.Supplier.DUNS))
                    {
                        string duns = filter.Supplier.DUNS;
                        query = query.Where(r => r.Supplier.DUNS == duns);
                    }
                    if (!string.IsNullOrEmpty(filter.Supplier.SupplierName))
                    {
                        string supplierName = filter.Supplier.SupplierName;
                        query = query.Where(r => r.Supplier.SupplierName == supplierName);
                    }
                }
                query = query.Where(r => r.Available == true);
            }
            return query.OrderBy(r => r.Part.Plant.PlantCode).ThenBy(r => r.Part.PartCode);
        }

        public List<PartRepairRecord> GetPartRepairRecordsByPage(PartRepairRecord filter, int pageSize, int pageNumber, out int pageCount, out int itemCount)
        {
            return GetQueryByPage(QueryPartRepairRecords(filter), pageSize, pageNumber, out pageCount,out itemCount).ToList();
        }

        public PartRepairRecord GetPartRepairRecordbykey(PartRepairRecord record)
        {
            record = GetObjectByKey(record);
            if (record.Part != null)
            {
                record.Part.PlantReference.Load();
                record.Part.SupplierReference.Load();
            }

            return record;
        }

        public void UpdatePartRepairRecord(PartRepairRecord record)
        {
            record.DateModified = DateTime.Now;
            //record.PartReference.EntityKey = Context.CreateEntityKey(_context.DefaultContainerName + "." + "Part", record.Part);
            //record.SupplierReference.EntityKey = Context.CreateEntityKey(_context.DefaultContainerName + "." + "Supplier", record.Supplier);

            UpdateObject(record);
        }


        public PartRepairRecord AddPartRepairRecord(PartRepairRecord record)
        {
            record =AddObject(record);
            return record;
        }

        public void DeletePartRepairRecord(PartRepairRecord record)
        {
            PartRepairRecord tmpRecord = GetPartRepairRecordbykey(record);
            tmpRecord.Available = false;
            UpdatePartRepairRecord(tmpRecord);
            //DeletePartRepairRecordSql(record);
        }

        public void DeletePartRepairRecords(List<string> guids)
        {
            if (guids.Count>0)
            {
                StringBuilder sbSql = new StringBuilder();
                foreach (string id in guids)
                {
                    sbSql.Append(string.Format("Update PartRepairRecord set Available=0 where RecordID='{0}';", id));
                }
                Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text); 
            }
        }

        public void ImportPartRepairRecord(List<View_PartRepairRecord> list)
        {
            Type[] types = new Type[] { typeof(View_PartRepairRecord) };
            XmlSerializer xs = new XmlSerializer(typeof(List<View_PartRepairRecord>), types);
            string itemStr = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                xs.Serialize(sw, list);
                itemStr = sw.ToString();
            }
            DbParameter paramItems = Context.CreateDbParameter("@partItems", System.Data.DbType.Xml, itemStr, System.Data.ParameterDirection.Input);

            Context.ExecuteNonQuery("sp_ImportPartRepairRecord", CommandType.StoredProcedure, paramItems);
        }

        #region temporary Sql Method ...
        private void DeletePartRepairRecordSql(PartRepairRecord record)
        {
            SqlParameter para1 = new SqlParameter("@RecordID", System.Data.DbType.Int32);
            para1.Value = record.RecordID;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(string.Format("DELETE PartRepairRecord FROM PartRepairRecord WHERE RecordID={0};", "@RecordID"));

            Context.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, para1);
            //using (SqlConnection conn = ((System.Data.EntityClient.EntityConnection)(Context.Connection)).StoreConnection as SqlConnection)
            //{
            //    conn.Open();
            //    ExecuteNonQuery(conn, CommandType.Text, sbSql.ToString(), para1);
            //}
        }
        private int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) 
                throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);
            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();
            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) 
                throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) 
                throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;
            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;
            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) 
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;
            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) 
                throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&(p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        #endregion temporary Sql Method ...
    }
}
