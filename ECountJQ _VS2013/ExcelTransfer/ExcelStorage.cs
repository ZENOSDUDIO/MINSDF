using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ECount.Infrustructure.Data.Integration
{
    internal class ExcelStorage : StorageProviderBase
    {
        public ExcelStorage(string providername)
            : base(providername)
        {
        }

        internal override void CreateDataSchema(IntegrationContext context)
        {
            string sourcePath = context.State[ContextState.SourcePath] as string;
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(string.Format("缺少参数[{0}]!", ContextState.SourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException(string.Format("文件[{0}]不存在!", sourcePath));
            string transfermode = context.State[ContextState.TransferMode] as string;
            if (string.IsNullOrEmpty(transfermode))
                transfermode = DataFixMode.Skip;

            string tableName = context.State["tableName"] as string;

            if (tableName != null && !tableName.EndsWith("$"))
                tableName += "$";

            using (ExcelReader reader = new ExcelReader(sourcePath))
            {
                string[] tables = reader.GetExcelTableNames();
                // it is vary possibly that csv data but filed end with xls. we need parse in csv format
                if (tables == null)
                    throw new InvalidDataException(string.Format("[{0}]不是有效的excel文件", sourcePath));
                if (tables.Length == 0)
                    throw new ArgumentException("无法找到excel页", sourcePath);
                if (tableName == null || !Array.Exists(tables, delegate(string t)
                {
                    if (
                        t.Equals(tableName, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    else if (
                        t.Equals(tableName + "'",
                                 StringComparison.InvariantCultureIgnoreCase))
                    {
                        // incase the escape char '\''
                        tableName = t;
                        return true;
                    }
                    else
                        return false;
                }))
                    tableName = tables[0];

                DataTable dt = reader.GetTableSchema(tableName);
                DataSchema dataSchema = new DataSchema();
                dataSchema.TransferMode = transfermode;
                List<DataSchemaField> fields = new List<DataSchemaField>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataSchemaField field = new DataSchemaField();
                    field.Name = field.Source = field.Destination = dt.Rows[i][3].ToString(); // COLUMN_NAME
                    // TODO: by alex hu	11 DATA_TYPE
                    field.Type = typeof(string).ToString();
                    field.Index = i;

                    fields.Add(field);
                }

                dataSchema.Fields = fields.ToArray();

                context.Schema = dataSchema;
            }
        }

        internal override void GetData(IntegrationContext context)
        {
            string sourcePath = context.State[ContextState.SourcePath] as string;
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.SourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException(string.Format("File [{0}] not found!", sourcePath));

            if (null == context.Schema)
                throw new ArgumentException("data schema missed!");
            string tableName = context.State["tableName"] as string;

            using (ExcelReader reader = new ExcelReader(sourcePath))
            {
                string[] tables = reader.GetExcelTableNames();
                // it is vary possibly that csv data but filed end with xls. we need parse in csv format
                if (tables == null)
                    throw new InvalidDataException(string.Format("[{0}]不是有效的excel文件", sourcePath));
                if (tables.Length == 0)
                    throw new ArgumentException("无法找到excel页", sourcePath);
                if (tableName == null ||
                    !Array.Exists(tables,
                                  delegate(string t) { return t.Equals(tableName, StringComparison.InvariantCultureIgnoreCase); }))
                    tableName = tables[0];

                DataTable dt = reader.GetTable(tableName);
                DataTableStorage.GetDataFromSourceDataTable(dt, context);
            }
        }

        internal override void TransferData(IntegrationContext context)
        {
            string desDirectory = context.State["desDirectory"] as string;
            if (string.IsNullOrEmpty(desDirectory))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", desDirectory));
            if (!Directory.Exists(desDirectory))
                throw new FileNotFoundException(string.Format("Directory [{0}] does not exist!", desDirectory));

            if (null == context.Schema)
                throw new ArgumentException("没有找到可用的数据结构!");
            if (context.Data.Count == 0)
                throw new ArgumentException("没有找到可用的数据!");

            DataTable dt = DataTableStorage.GetEmptyDataTable(context.Schema);

            foreach (DataItem item in context.Data)
            {
                DataTableStorage.PrepareDataForDestination(item, dt);
            }

            string file, message;
            if (!ExcelReader.SaveData(desDirectory, dt, out file, out message))
                // TODO: fails handle here
                context.Status = IntegrationStatus.Failure;
            else
                context.State["desFile"] = file;
        }
    }
}