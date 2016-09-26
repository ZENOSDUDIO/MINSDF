using System;
using System.Collections.Generic;
using System.Data;

namespace ECount.Infrustructure.Data.Integration
{
    internal class DataTableStorage : StorageProviderBase
    {
        public DataTableStorage(string providername)
            : base(providername)
        {
        }

        internal override void CreateDataSchema(IntegrationContext context)
        {
            DataTable dt = context.State["datatable"] as DataTable;
            if (dt == null)
                throw new ArgumentException("context.State[\"datatable\"]");
            //if (dt.Rows.Count == 0)
            //    throw new ArgumentException("rows cannot be empty.");

            DataSchema dataSchema = new DataSchema();
            dataSchema.TransferMode = DataFixMode.Skip;
            List<DataSchemaField> fields = new List<DataSchemaField>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataSchemaField field = new DataSchemaField();
                field.Name = field.Source = field.Destination = dt.Columns[i].ColumnName;
                field.Type = dt.Columns[i].DataType.ToString();
                if (!dt.Columns[i].AllowDBNull)
                    field.SetAnyAttributeValue("required", "true");
                field.Index = dt.Columns[i].Ordinal;

                fields.Add(field);
            }
            dataSchema.Fields = fields.ToArray();
            context.Schema = dataSchema;
        }

        internal override void GetData(IntegrationContext context)
        {
            DataTable dt = context.State["datatable"] as DataTable;
            if (dt == null)
                throw new ArgumentException("context.State[\"datatable\"]");
            //if (dt.Rows.Count == 0)
            //    throw new ArgumentException("rows cannot be empty.");

            if (null == context.Schema)
                throw new ArgumentException("没有找到可用的数据结构!");

            GetDataFromSourceDataTable(dt, context);

            context.State["datatable"] = dt;
        }

        internal override void TransferData(IntegrationContext context)
        {
            if (null == context.Schema)
                throw new ArgumentException("没有找到可用的数据结构!");
            if (context.Data.Count == 0)
                throw new ArgumentException("没有找到可用的数据!");

            DataTable dt = GetEmptyDataTable(context.Schema);
            FillDataTable(dt, context.Data);
            context.State["datatable"] = dt;
        }

        internal static void GetDataFromSourceDataTable(DataTable dt, IntegrationContext context)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataItem item = GetDataItemFromSource(dr, context.Schema);
                if (item != null)
                    if (item.IsValid)
                        context.AddData(item);
                    else
                        context.AddSkippedData(item);
            }
        }

        internal static DataItem GetDataItemFromSource(DataRow dr, DataSchema schema)
        {
            DataItem item = new DataItem(schema);
            for (int i = 0; i < schema.Fields.Length; i++)
            {
                if (!schema.Fields[i].Skip)
                {
                    object v;
                    string replaced = schema.Fields[i].GetAnyAttributeValue("replaced");
                    string defaultvalue = schema.Fields[i].GetAnyAttributeValue("default");
                    try
                    {
                        v = dr[schema.Fields[i].Source];
                    }
                    catch (ArgumentException)
                    {
                        // 该列不存在
                        if (defaultvalue != null)
                            v = defaultvalue;
                        else
                            throw new Exception(string.Format("没有找到数据列'{0}'.", schema.Fields[i].Source));
                    }
                    // 如果存在被替换值和默认值,则将被替换值替换为默认值
                    if (!string.IsNullOrEmpty(v as string) && v != DBNull.Value && !string.IsNullOrEmpty(replaced) && replaced.IndexOf(v.ToString()) != -1 &&
                        defaultvalue != null)
                        v = defaultvalue;
                    DataItemField f = item.GetSourceField(schema.Fields[i].Source);
                    f.Value = v;

                    // 如果该项验证失败, 且为DataFixMode.Fix, 且有默认值, 则以默认值代替
                    if (!string.IsNullOrEmpty(schema.TransferMode) && schema.TransferMode.Equals("fix", StringComparison.InvariantCultureIgnoreCase) && defaultvalue != null)
                    {
                        ValidationResults vr = new ValidationResults();
                        f.DoValidate(vr);
                        if (!vr.IsValid && f.Value as string != v)
                            f.Value = v;
                    }
                }
            }

            return item;
        }

        internal static DataTable GetEmptyDataTable(DataSchema schema)
        {
            DataTable dt = new DataTable("Sheet1");
            foreach (DataSchemaField field in schema.Fields)
            {
                Type t;
                string tstr = field.Type;
                if (tstr.StartsWith("System.Nullable`1"))
                {
                    // nullable type
                    tstr = tstr.Substring(18, tstr.Length - 19);
                }
                t = Type.GetType(tstr);
                dt.Columns.Add(new DataColumn(field.Name, t));
            }
            return dt;
        }

        internal static void PrepareDataForDestination(DataItem item, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            foreach (DataItemField field in item.Items)
            {
                if (!field.Skip)
                    if (string.IsNullOrEmpty(field.Destination))
                        dr[field.Name] = InCaseNullValue(field.Value);
                    else
                        dr[field.Destination] = InCaseNullValue(field.Value);
            }
            dt.Rows.Add(dr);
        }

        private static object InCaseNullValue(object value)
        {
            return value ?? DBNull.Value;
        }

        internal static DataSchema GetDataSchema(DataTable dt)
        {
            DataSchema schema = new DataSchema();
            List<DataSchemaField> fields = new List<DataSchemaField>();
            int i = 0;
            foreach (DataColumn column in dt.Columns)
            {
                // TODO: skip the identity column
                //if (column.AutoIncrement)
                //    continue;

                DataSchemaField field = new DataSchemaField();
                field.Name = field.Source = field.Destination = column.ColumnName;
                field.Type = column.DataType.ToString();
                field.Index = ++i;
                // TODO: add validation info
                //if (!column.AllowDBNull)
                //    field.SetAnyAttributeValue("required", "True");

                fields.Add(field);
            }

            schema.Fields = fields.ToArray();

            return schema;
        }

        internal static void FillDestinationDataItems(DataTable dt, DataItemCollection data)
        {
            foreach (DataItem item in data)
            {
                PrepareDataForDestination(item, dt);
            }
        }

        internal static void FillDataTable(DataTable dt, DataItemCollection data)
        {
            if (dt == null || dt.Columns.Count == 0)
            {
                dt = GetEmptyDataTable(data[0].Schema);
            }

            foreach (DataItem item in data)
            {
                PrepareDataForDestination(item, dt);
            }
        }
    }
}