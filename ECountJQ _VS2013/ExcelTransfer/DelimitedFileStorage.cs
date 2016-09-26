using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ECount.Infrustructure.Utilities;

namespace ECount.Infrustructure.Data.Integration
{
    /// <summary>
    /// 1. 如有分隔符或换行, 将数据用双引号包含
    /// 2. 如有双引号,前缀一个双引号以转义
    /// </summary>
    internal class DelimitedFileStorage : StorageProviderBase
    {
        static readonly object _syncRoot = new object();
        protected Encoding encoding = Encoding.Default;

        public DelimitedFileStorage(string providername)
            : base(providername)
        {
        }

        internal override void CreateDataSchema(IntegrationContext context)
        {
            string sourcePath = context.State[ContextState.SourcePath] as string;
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.SourcePath));

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException(string.Format("File [{0}] not found!", sourcePath));

            string delimiterString = context.State[ContextState.FlatFileDelimiter] as string;
            if (string.IsNullOrEmpty(delimiterString))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.FlatFileDelimiter));
            char delimiter = delimiterString[0];

            string transfermode = context.State[ContextState.TransferMode] as string;
            if (string.IsNullOrEmpty(transfermode))
                transfermode = DataFixMode.Fix;

            DataSchema dataSchema = new DataSchema();
            dataSchema.SetAnyAttributeValue("delimiter", delimiter.ToString());
            dataSchema.TransferMode = transfermode;

            encoding = StringUtil.GetFileEncoding(sourcePath);
            using (StreamReader reader = new StreamReader(sourcePath, encoding))
            {
                // maybe this line is included by quote, thus need seek the closed quote
                string line = GetLine(reader);

                string[] columns = GetLineColumns(line, delimiter);

                List<DataSchemaField> fields = new List<DataSchemaField>();
                for (int i = 0; i < columns.Length; i++)
                {
                    // skip the empty column
                    if (columns[i].Trim().Length == 0)
                        continue;
                    DataSchemaField field = new DataSchemaField();
                    field.Name = field.Source = field.Destination = columns[i];
                    field.Type = typeof(string).ToString();
                    field.Index = i;

                    fields.Add(field);
                }

                dataSchema.Fields = fields.ToArray();

                reader.Close();
            }
            context.Schema = dataSchema;
        }

        internal override void GetData(IntegrationContext context)
        {
            string sourcePath = context.State[ContextState.SourcePath] as string;
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", ContextState.SourcePath));
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException(string.Format("File [{0}] not found!", sourcePath));

            bool skipfirstline = (bool)context.State[ContextState.FlatFileSkipFirstLine];
            bool skiplastline = (bool)context.State[ContextState.FlatFileSkipLastLine];

            encoding = StringUtil.GetFileEncoding(sourcePath);
            using (
                StreamReader reader =
                    new StreamReader(new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), encoding))
            {
                if (skipfirstline)
                    GetLine(reader);
                string row = null;
                while (!reader.EndOfStream)
                {
                    if (skiplastline)
                    {
                        if (row == null)
                        {
                            row = GetLine(reader);
                            continue;
                        }
                    }
                    else
                        row = GetLine(reader);
                    if (row.Trim().Length == 0)
                        continue;
                    DataItem item = GetDataItem(row, context.Schema, true);
                    if (item != null)
                        if (item.IsValid)
                            context.AddData(item);
                        else
                            context.AddSkippedData(item);
                    if (skiplastline)
                        row = GetLine(reader);
                }
            }
        }

        internal override void TransferData(IntegrationContext context)
        {
            string desDirectory = context.State["desDirectory"] as string;
            if (string.IsNullOrEmpty(desDirectory))
                throw new ArgumentNullException(string.Format("argument [{0}] missed!", desDirectory));
            if (!Directory.Exists(desDirectory))
                throw new FileNotFoundException(string.Format("Directory [{0}] does not exist!", desDirectory));
            string desPrefix = context.State["desPrefix"] as string;


            if (null == context.Schema)
                throw new ArgumentException("没有找到可用的数据结构!");
            //if (context.Data.Count == 0)
            //    throw new ArgumentException("没有找到可用的数据!");


            string file;
            lock (_syncRoot)
            {
                file = string.Concat(desPrefix, "_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".csv");
            }
            string filepath = Path.Combine(desDirectory, file);

            using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create), Encoding.Default))
            {
                // write column header
                WriteHeaderLine(context.Schema, writer);

                // write content
                foreach (DataItem item in context.Data)
                {
                    WriteItemLine(item, context.Schema, writer);
                }

                writer.Close();
            }

            context.State["desFile"] = file;
        }

        public virtual DataItem GetDataItem(string row, DataSchema dataSchema, bool isSource)
        {
            string[] items = GetLineColumns(row, dataSchema.GetAnyAttributeValue("delimiter")[0]);

            DataItem dataItem = new DataItem(dataSchema);
            for (int i = 0; i < dataSchema.Fields.Length; i++)
            {
                DataSchemaField schemaField = dataSchema.Fields[i];
                DataItemField itemfield;
                if (isSource)
                    itemfield = dataItem.GetSourceField(schemaField.Source);
                else
                    itemfield = dataItem.GetDestinationField(schemaField.Destination);

                string replaced = schemaField.GetAnyAttributeValue("replaced");
                string defaultvalue = schemaField.GetAnyAttributeValue("default");
                if (schemaField.Index >= items.Length)
                    // bad row
                    if (defaultvalue == null)
                        switch (dataSchema.TransferMode)
                        {
                            case DataFixMode.Skip:
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine("skipped transfering data row:");
                                Array.ForEach(items, delegate(string item) { sb.AppendFormat("{0}\t", item); });
                                // TODO:
                                //LogEntry log = new LogEntry();
                                //log.Message = sb.ToString();
                                //log.Categories.Add(LogCategory.Trace);
                                //log.Priority = LogPriority.Normal;

                                Logger.Instance.Info(this, sb.ToString());
                                return null;
                            case DataFixMode.Fix:
                                itemfield.Value = string.Empty;
                                break;
                            case DataFixMode.RaiseError:
                                throw new ArgumentException(
                                    string.Format("row has [{0}] columns while schema require [{1}] fields.{2}row:[{3}]", items.Length,
                                                  dataSchema.Fields.Length, Environment.NewLine, row));
                            default:
                                throw new ArgumentOutOfRangeException(
                                    string.Format("Invalid transfer mode [{0}] in schema, requires one of 'skip,fix,raiseerror'",
                                                  dataSchema.TransferMode));
                        }
                    else
                        itemfield.Value = defaultvalue;
                else
                {
                    itemfield.Value = items[schemaField.Index].Trim();
                }
                // for the value start with 0, in excel which will be escaped with ' starting.
                if (itemfield.Value != null && itemfield.Value.ToString().StartsWith("'0"))
                    itemfield.Value = itemfield.Value.ToString().TrimStart('\'');
                // 如果存在被替换值和默认值,则将被替换值替换为默认值
                if (!string.IsNullOrEmpty(itemfield.Value as string) && !string.IsNullOrEmpty(replaced) && replaced.IndexOf(itemfield.Value.ToString()) != -1 &&
                    defaultvalue != null)
                    itemfield.Value = defaultvalue;
                // 如果该项验证失败, 且为DataFixMode.Fix, 且有默认值, 则以默认值代替
                if (!string.IsNullOrEmpty(dataSchema.TransferMode) && dataSchema.TransferMode.Equals("fix", StringComparison.InvariantCultureIgnoreCase) && defaultvalue != null)
                {
                    ValidationResults vr = new ValidationResults();
                    itemfield.DoValidate(vr);
                    if (!vr.IsValid && itemfield.Value as string != defaultvalue)
                        itemfield.Value = defaultvalue;
                }

                try
                {
                    Utilities.AdjustDataItemFieldValue(itemfield);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(this, ex);
                    return null;
                }
            }

            return dataItem;
        }

        internal static string GetLine(StreamReader reader)
        {
            string line = string.Empty;
            int count = 0;
            do
            {
                if (reader.EndOfStream)
                    break;
                string line1 = reader.ReadLine();
                count += Array.FindAll(line1.ToCharArray(), delegate(char c) { return c.Equals('"'); }).Length;
                line += line1 + "\\n";
            } while (count != 0 && 0 != count % 2);

            // if this line is comment line, then move next
            if (line.StartsWith("#"))
                return GetLine(reader);

            // remove the last "\\n"
            return line.Substring(0, line.Length - 2);
        }

        internal static string[] GetLineColumns(string line, char delimiter)
        {
            List<string> columns = new List<string>();
            int startIndex = 0;
            bool quotestart = false;
            // maybe the delimiter is escaped by quote
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    if (quotestart)
                    {
                        // encount quote escape here
                        int escape = i + 1;
                        if (escape > line.Length - 1)
                            break;
                        if (line[escape] == '"')
                        {
                            i++;
                            continue;
                        }
                        if (line[escape] == delimiter)
                        {
                            // encount column seek completed
                            quotestart = false;
                            string c = line.Substring(startIndex, escape - startIndex);
                            if (c.StartsWith("\"") && c.EndsWith("\""))
                                c = c.Substring(1, c.Length - 2);
                            columns.Add(c.Replace("\"\"", "\"").Replace("\\n", "|").Trim());
                            // start new column seek
                            startIndex = ++i;

                            if (startIndex > line.Length - 1 || ++i > line.Length - 1)
                                break;

                            startIndex = i;
                            // empty content
                            bool shouldbreak = false;
                            while (line[i] == delimiter)
                            {
                                columns.Add(string.Empty);
                                startIndex = ++i;
                                if (i > line.Length - 1)
                                {
                                    shouldbreak = true;
                                    break;
                                }
                            }
                            if (shouldbreak)
                                break;

                            if (line[i] == '"')
                                quotestart = true;
                        }
                        else
                            throw new ArgumentException("双引号匹配出错,双引号只允许这三种情况下存在:1)为列的起始位置;2)转义另一个双引号;3)为列的截止位置且紧跟列分割符");
                    }
                    else
                        quotestart = true;
                else if (line[i] == delimiter)
                    if (!quotestart)
                    {
                        // not included in quote, so must be column seek completed
                        columns.Add(line.Substring(startIndex, i - startIndex).Trim(',').Trim());
                        // start new column seek

                        // empty content
                        if (i < (line.Length - 1) && line[++i] == delimiter)
                        {
                            columns.Add(string.Empty);
                            startIndex = i + 1;
                        }
                        else
                        {
                            if (line[i] == '"')
                                quotestart = true;
                            startIndex = i;
                        }
                    }
            }

            // add the last column
            string c2 = line.Substring(startIndex);
            if (c2.StartsWith("\"") && c2.EndsWith("\""))
                c2 = c2.Substring(1, c2.Length - 2);
            columns.Add(c2.Replace("\"\"", "\"").Replace("\\n", "|").Trim(',').Trim());

            return columns.ToArray();
        }

        private static void WriteHeaderLine(DataSchema schema, TextWriter writer)
        {
            bool isstart = true;
            char delimiter = GetDelimiterFromSchema(schema);
            foreach (DataSchemaField field in schema.Fields)
            {
                if (isstart)
                    isstart = false;
                else
                    writer.Write(delimiter);
                writer.Write(StringUtil.GetDelimitedReadyString(field.Destination, delimiter).TrimEnd(delimiter));
            }
            writer.WriteLine();
        }

        private static void WriteItemLine(DataItem item, DataSchema schema, TextWriter writer)
        {
            bool isstart = true;
            char delimiter = GetDelimiterFromSchema(schema);
            foreach (DataSchemaField field in schema.Fields)
            {
                object v = item[field.Destination].Value ?? string.Empty;

                if (isstart)
                    isstart = false;
                else
                    writer.Write(delimiter);

                writer.Write(StringUtil.GetDelimitedReadyString(v.ToString(), delimiter).TrimEnd(delimiter));
            }
            writer.WriteLine();
        }

        private static char GetDelimiterFromSchema(DataSchema schema)
        {
            string delimiter = schema.GetAnyAttributeValue("delimiter");
            return string.IsNullOrEmpty(delimiter) ? ',' : delimiter[0];
        }
    }
}