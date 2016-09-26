using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using ECount.Infrustructure.Data;
using ECount.Infrustructure.Data.Integration;
using ECount.Infrustructure.Utilities;
using System.Web;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace ECount.ExcelTransfer
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 将按字符串长度隔开的文本数据导入到数据库，约定：
        /// 1. 文本文件包含第一行头和最后一行尾，且不必导入
        /// 2. 文本文件不含列名，第二行起即为数据
        /// 3. 数据库连接取config文件中第一个连接串
        /// 4. 待导入表名即schema文件名
        /// 5. 导入前会清空表数据
        /// </summary>
        /// <param name="sourcePath">源数据文件的绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        /*
       public bool TransferFixLengthFileToDatabase(string sourcePath, string schemaPath, out string message)
       {
           
           message = string.Empty;
           if (string.IsNullOrEmpty(sourcePath))
           {
               message = "数据文件路径为空";
               return false;
           }
           if (!File.Exists(sourcePath))
           {
               message = string.Format("数据文件路径[{0}]不存在", sourcePath);
               return false;
           }

           if (string.IsNullOrEmpty(schemaPath))
           {
               message = "schema文件路径为空";
               return false;
           }
           if (!File.Exists(schemaPath))
           {
               message = string.Format("schema文件路径[{0}]不存在", schemaPath);
               return false;
           }

           bool result;

           using (IntegrationEngine target = new IntegrationEngine())
           {
               FixedLengthFileStorage sourceProvider = new FixedLengthFileStorage("sourceProvider");
               target.Providers.Add(sourceProvider);
               DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
               target.Providers.Add(destinationProvider);

               IDictionary state3 = new Hashtable();
               state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
               state3["destinationProvider"] = IntegrationMode.TransferData;
               state3[ContextState.SourcePath] = sourcePath;
               state3[ContextState.SchemaFilePath] = schemaPath;
               state3[ContextState.FlatFileSkipFirstLine] = true;
               state3[ContextState.FlatFileSkipLastLine] = true;
               state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[0].ConnectionString;
               state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
               state3[ContextState.DatabaseTruncateTable] = true;

               try
               {
                   result = target.Run(state3);
               }
               catch (Exception ex)
               {
                   result = false;
                   message = ex.Message;
               }
           }
           return result;
            
       }
        */
        /// <summary>
        /// 将按','隔开的文本数据导入到数据库，约定：
        /// 1. 文本文件包含第一行列名，且不必导入
        /// 2. 文本文件第二行起即为数据
        /// 3. 数据库连接取config文件中第一个连接串
        /// 4. 待导入表名即schema文件名
        /// 5. 导入前会清空表数据
        /// 6. 分隔符','在schema文件中定义
        /// </summary>
        /// <param name="sourcePath">源数据文件的绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        /*
       public bool TransferCSVFileToDatabase(string sourcePath, string schemaPath, out string message)
       {
           
           message = string.Empty;
           if (string.IsNullOrEmpty(sourcePath))
           {
               message = "数据文件路径为空";
               return false;
           }
           if (!File.Exists(sourcePath))
           {
               message = string.Format("数据文件路径[{0}]不存在", sourcePath);
               return false;
           }

           if (string.IsNullOrEmpty(schemaPath))
           {
               message = "schema文件路径为空";
               return false;
           }
           if (!File.Exists(schemaPath))
           {
               message = string.Format("schema文件路径[{0}]不存在", schemaPath);
               return false;
           }

           bool result;

           using (IntegrationEngine target = new IntegrationEngine())
           {
               DelimitedFileStorage sourceProvider = new DelimitedFileStorage("sourceProvider");
               target.Providers.Add(sourceProvider);
               DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
               target.Providers.Add(destinationProvider);

               IDictionary state3 = new Hashtable();
               state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
               state3["destinationProvider"] = IntegrationMode.TransferData;
               state3[ContextState.SourcePath] = sourcePath;
               state3[ContextState.SchemaFilePath] = schemaPath;
               state3[ContextState.FlatFileSkipFirstLine] = true;
               state3[ContextState.FlatFileSkipLastLine] = false;
               state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[0].ConnectionString;
               state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
               state3[ContextState.DatabaseTruncateTable] = true;

               try
               {
                   result = target.Run(state3);
               }
               catch (Exception ex)
               {
                   result = false;
                   message = ex.Message;
               }
           }
           return result;
            
       }
       */
        /// <summary>
        /// 将Excel数据导入数据库, 约定:
        /// 1. Excel文件包含第一行列名，且不必导入
        /// 2. 文本文件第二行起即为数据
        /// 3. 数据库连接取config文件中第一个连接串
        /// 4. 待导入表名即schema文件名
        /// 5. 先尝试读取Excel名为schema文件名的worksheet,失败则读取第一个worksheet的数据.
        /// 6. 导入前会清空表数据
        /// </summary>
        /// <param name="sourcePath">源数据文件的绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        /*
        public bool TransferExcelToDatabase(string sourcePath, string schemaPath, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(sourcePath))
            {
                message = "数据文件路径为空";
                return false;
            }
            if (!File.Exists(sourcePath))
            {
                message = string.Format("数据文件路径[{0}]不存在", sourcePath);
                return false;
            }

            if (string.IsNullOrEmpty(schemaPath))
            {
                message = "schema文件路径为空";
                return false;
            }
            if (!File.Exists(schemaPath))
            {
                message = string.Format("schema文件路径[{0}]不存在", schemaPath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                ExcelStorage sourceProvider = new ExcelStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                DatabaseStorage destinationProvider = new DatabaseStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = new Hashtable();
                state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3[ContextState.SourcePath] = sourcePath;
                state3[ContextState.SchemaFilePath] = schemaPath;
                state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                state3["tableName"] = state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
                state3[ContextState.DatabaseTruncateTable] = true;
                try
                {
                    result = target.Run(state3);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }
            return result;
        }
        */

        /// <summary>
        /// 将Routine数据的Excel文件导入数据库, 约定:
        /// 1. Excel文件包含第一行列名，且不必导入
        /// 2. 文本文件第二行起即为数据
        /// 3. 数据库连接取config文件中第一个连接串
        /// 4. 待导入表名即schema文件名
        /// 5. 先尝试读取Excel名为schema文件名的worksheet,失败则读取第一个worksheet的数据.
        /// 6. 导入前会清空表数据 
        /// </summary>
        /// <param name="sourcePath">源数据文件的绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="plant">用户选择的待导入的工厂</param>
        /// <param name="workshop">用户选择的待导入的车间</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        /*public bool TransferRoutineExcelToDatabase(string sourcePath, string schemaPath, string plant, string workshop,
                                                   out string message)
        {
            
            message = string.Empty;
            if (string.IsNullOrEmpty(plant))
            {
                message = "所选工厂为空";
                return false;
            }
            if (string.IsNullOrEmpty(workshop))
            {
                message = "所选车间为空";
                return false;
            }
            if (string.IsNullOrEmpty(sourcePath))
            {
                message = "数据文件路径为空";
                return false;
            }
            if (!File.Exists(sourcePath))
            {
                message = string.Format("数据文件路径[{0}]不存在", sourcePath);
                return false;
            }

            if (string.IsNullOrEmpty(schemaPath))
            {
                message = "schema文件路径为空";
                return false;
            }
            if (!File.Exists(schemaPath))
            {
                message = string.Format("schema文件路径[{0}]不存在", schemaPath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                ExcelStorage sourceProvider = new ExcelStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                RoutineStorage destinationProvider = new RoutineStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = new Hashtable();
                state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3[ContextState.SourcePath] = sourcePath;
                state3[ContextState.SchemaFilePath] = schemaPath;
                state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                state3["tableName"] = "D-JIT";
                state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
                state3[ContextState.DatabaseTruncateTable] = true;

                using (RoutineValidator validator = new RoutineValidator(plant, workshop))
                {
                    try
                    {
                        sourceProvider.OnValidating += validator.VerifyDPlant;
                        sourceProvider.OnValidating += validator.VerifyDWorkshop;
                        sourceProvider.OnValidating += validator.VerifySupplierExist;
                        sourceProvider.OnValidating += validator.VerifyPOSupplierExist;
                        sourceProvider.OnValidating += validator.VerifyPlantExist;
                        sourceProvider.OnValidating += validator.VerifyDPlantExist;
                        sourceProvider.OnValidating += validator.VerifyEPSPlantExist;
                        sourceProvider.OnValidating += validator.VerifyWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyDWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyEPSWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyDockExist;
                        sourceProvider.OnValidating += validator.VerifyDlocExist;
                        sourceProvider.OnValidating += validator.VerifyUlocExist;
                        sourceProvider.OnValidating += validator.VerifyElocExist;
                        sourceProvider.OnValidating += validator.VerifyEPSDlocExist;
                        sourceProvider.OnValidating += validator.VerifyEPSRouteExist;
                        sourceProvider.OnValidating += validator.VerifyRouteExist;
                        sourceProvider.OnValidating += validator.VerifyTwiceRouteExist;
                        sourceProvider.OnValidating += validator.VerifyDollyEPSRouteExist;
                        sourceProvider.OnValidating += validator.VerifyDataIntegrity;
                        sourceProvider.OnValidating += validator.VerifyBreakPointPart;
                        result = target.Run(state3);
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return false;
                    }
                }
            }
            return result;
            
        }
        */

        /// <summary>
        /// 将Routine数据的Excel文件导入数据库, 约定:
        /// 1. Excel文件包含第一行列名，且不必导入
        /// 2. 文本文件第二行起即为数据
        /// 3. 数据库连接取config文件中第一个连接串
        /// 4. 待导入表名即schema文件名
        /// 5. 先尝试读取Excel名为schema文件名的worksheet,失败则读取第一个worksheet的数据.
        /// 6. 导入前会清空表数据 
        /// </summary>
        /// <param name="sourcePath">源数据文件的绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="plant">用户选择的待导入的工厂</param>
        /// <param name="workshop">用户选择的待导入的车间</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        /*public bool TransferRoutineCSVToDatabase(string sourcePath, string schemaPath, string plant, string workshop,
                                                   out string message)
        {
            
            message = string.Empty;
            if (string.IsNullOrEmpty(plant))
            {
                message = "所选工厂为空";
                return false;
            }
            if (string.IsNullOrEmpty(workshop))
            {
                message = "所选车间为空";
                return false;
            }
            if (string.IsNullOrEmpty(sourcePath))
            {
                message = "数据文件路径为空";
                return false;
            }
            if (!File.Exists(sourcePath))
            {
                message = string.Format("数据文件路径[{0}]不存在", sourcePath);
                return false;
            }

            if (string.IsNullOrEmpty(schemaPath))
            {
                message = "schema文件路径为空";
                return false;
            }
            if (!File.Exists(schemaPath))
            {
                message = string.Format("schema文件路径[{0}]不存在", schemaPath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                DelimitedFileStorage sourceProvider = new DelimitedFileStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                RoutineStorage destinationProvider = new RoutineStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = new Hashtable();
                state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3[ContextState.SourcePath] = sourcePath;
                state3[ContextState.SchemaFilePath] = schemaPath;
                state3[ContextState.DatabaseConnectionString] = ConfigurationManager.ConnectionStrings[1].ConnectionString;
                state3["tableName"] = "D-JIT";
                state3[ContextState.DatabaseTableName] = GetTableNameFromSchemaFileName(schemaPath);
                state3[ContextState.DatabaseTruncateTable] = true;

                using (RoutineValidator validator = new RoutineValidator(plant, workshop))
                {
                    try
                    {
                        sourceProvider.OnValidating += validator.VerifyDPlant;
                        sourceProvider.OnValidating += validator.VerifyDWorkshop;
                        sourceProvider.OnValidating += validator.VerifySupplierExist;
                        sourceProvider.OnValidating += validator.VerifyPOSupplierExist;
                        sourceProvider.OnValidating += validator.VerifyPlantExist;
                        sourceProvider.OnValidating += validator.VerifyDPlantExist;
                        sourceProvider.OnValidating += validator.VerifyEPSPlantExist;
                        sourceProvider.OnValidating += validator.VerifyWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyDWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyEPSWorkshopExist;
                        sourceProvider.OnValidating += validator.VerifyDockExist;
                        sourceProvider.OnValidating += validator.VerifyDlocExist;
                        sourceProvider.OnValidating += validator.VerifyUlocExist;
                        sourceProvider.OnValidating += validator.VerifyElocExist;
                        sourceProvider.OnValidating += validator.VerifyEPSDlocExist;
                        sourceProvider.OnValidating += validator.VerifyEPSRouteExist;
                        sourceProvider.OnValidating += validator.VerifyRouteExist;
                        sourceProvider.OnValidating += validator.VerifyTwiceRouteExist;
                        sourceProvider.OnValidating += validator.VerifyDollyEPSRouteExist;
                        sourceProvider.OnValidating += validator.VerifyDataIntegrity;
                        sourceProvider.OnValidating += validator.VerifyBreakPointPart;
                        result = target.Run(state3);
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return false;
                    }
                }
            }
            return result;
            
        }
        */

        /// <summary>
        /// Write data into excel file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool WriteExcel(string fileName, DataSet ds, out string message)
        {
            message = string.Empty;
            if (ds == null || ds.Tables.Count == 0)
                message = "无可用的数据用来导出";
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                message = string.Format("schema文件路径[{0}]不存在", fileName);
                return false;
            }
            try
            {
                Dictionary<string, List<string>> workSheets = new Dictionary<string, List<string>>();
                List<string> tableNames = new List<string>();
                foreach (DataTable dt in ds.Tables)
                {
                    tableNames.Add(dt.TableName);
                }
                using (ExcelReader reader = new ExcelReader(fileName))
                {
                    reader.Headers = true;
                    List<string> tableNameList = reader.GetExcelNames().ToList();
                    foreach (DataTable table in ds.Tables)
                    {
                        if (!tableNameList.Contains("'" + table.TableName + "$'"))
                        {
                            continue;
                        }
                        DataTable dt = reader.GetTable(table.TableName);
                        List<string> colNames = new List<string>();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                if (string.Equals(dt.Columns[j].ColumnName, table.Columns[i].ColumnName))
                                {
                                    colNames.Add(dt.Columns[j].ColumnName);
                                    break;
                                }
                            }
                        }
                        workSheets.Add(table.TableName, colNames);
                        //Excel.Worksheet workSheet1 = new Excel.Worksheet();

                        //workSheet1["table.TableName"].WrapText = True;
                    }
                }

                string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties={1}Excel 8.0;HDR=Yes;{1}", fileName, Convert.ToChar(34)); //string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended   Properties=""Excel 8.0;HDR=YES;""", fileName);
                StringBuilder sb = new StringBuilder();

                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;

                    using (DbCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            DataTable dt = ds.Tables[i];
                            if (!workSheets.ContainsKey(dt.TableName))
                            {
                                continue;
                            }
                            List<string> cols = workSheets[dt.TableName];
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {

                                DataRow row = dt.Rows[j];
                                sb.Append(string.Format("UPDATE [{0}$] SET ", dt.TableName));
                                for (int k = 0; k < cols.Count; k++)
                                {
                                    string colName = cols[k];
                                    if (row[colName] != DBNull.Value)
                                    {
                                        sb.Append(string.Format("[{0}]=", colName));
                                        if (dt.Columns[colName].DataType == typeof(string) || dt.Columns[colName].DataType == typeof(DateTime))
                                        {
                                            sb.Append(string.Format("\"{0}\"", row[colName]));
                                        }
                                        else
                                        {
                                            sb.Append(string.Format("\"{0:0.####}\"", row[colName]));
                                            //sb.Append((row[colName].ToString() + "+0").ToString());
                                        }
                                        sb.Append(",");
                                    }
                                }
                                command.CommandText = sb.ToString().TrimEnd(',') + " WHERE [ID]=" + (j + 1);
                                command.ExecuteNonQuery();
                                sb = new StringBuilder();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }


        /// <summary>
        /// Write data into excel file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool WriteExcel(string fileName, DataSet ds, out string message, Dictionary<string, string> dictColumnsMapping)
        {
            message = string.Empty;
            if (ds == null || ds.Tables.Count == 0)
                message = "无可用的数据用来导出";
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                message = string.Format("schema文件路径[{0}]不存在", fileName);
                return false;
            }
            try
            {

                string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties={1}Excel 8.0;HDR=NO;{1}", fileName, Convert.ToChar(34));

                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                string[] fieldArray = new string[dictColumnsMapping.Keys.Count];
                dictColumnsMapping.Keys.CopyTo(fieldArray, 0);
                string fields = string.Join(",", fieldArray);
                //for (int i = 0; i < fieldArray.Length; i++)
                //{
                //    fields += fieldArray[i];
                //    if (i<fieldArray.Length-1)
                //    {
                //        fields += ",";
                //    }
                //}
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;

                    using (DbCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            DataTable dt = ds.Tables[i];

                            //List<string> cols = workSheets[dt.TableName];
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {

                                DataRow row = dt.Rows[j];
                                StringBuilder sb = new StringBuilder();

                                sb.Append(string.Format("INSERT INTO [{0}$]({1}) VALUES", dt.TableName, fields));
                                //sb.Append(string.Format("UPDATE [{0}$] SET ", dt.TableName));
                                StringBuilder sbValues = new StringBuilder();
                                foreach (string fieldName in dictColumnsMapping.Keys)
                                {
                                    string colName = dictColumnsMapping[fieldName];
                                    if (dt.Columns[colName].DataType != typeof(int) && dt.Columns[colName].DataType != typeof(long) && dt.Columns[colName].DataType != typeof(double) && dt.Columns[colName].DataType != typeof(float) && dt.Columns[colName].DataType != typeof(decimal) && dt.Columns[colName].DataType != typeof(short))
                                    {
                                        sbValues.Append(string.Format("'{0}',", row[colName]));
                                    }
                                    else
                                    {
                                        if (row[colName]!=DBNull.Value)
                                        {

                                        sbValues.Append(string.Format("{0},", row[colName]));
                                        }
                                        else
                                        {

                                            sbValues.Append(string.Format("{0},", "NULL"));
                                        }
                                    }
                                }
                                command.CommandText = sb.ToString() + "(" + sbValues.ToString().TrimEnd(',') + ")";//.TrimEnd(',') + " WHERE [" + keyField + "]=" + (j + 1);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        public static bool UpdateExcel(string fileName, string sheetName, Dictionary<string, KeyValuePair<string, string>> ranges, out string message)
        {

            message = string.Empty;
            //if (ds == null || ds.Tables.Count == 0)
            //    message = "无可用的数据用来导出";
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                message = string.Format("schema文件路径[{0}]不存在", fileName);
                return false;
            }
            try
            {

                string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Mode=ReadWrite;Extended Properties={1}Excel 8.0;HDR=NO;{1}", fileName, Convert.ToChar(34));

                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb"); using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;

                    using (DbCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        foreach (var item in ranges)
                        {

                            command.CommandText = string.Format("UPDATE [{0}${1}] SET [{2}]='{3}'", sheetName, item.Key, item.Value.Key, item.Value.Value);
                            command.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将内存数据导出到XML Speadsheet格式的Excel文件
        /// </summary>
        /// <param name="dt">待导出的数据</param>
        /// <param name="directory">待导出文件所在的目录绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="file">导出文件相对于<paramref name="directory"/>的相对路径</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        public static bool TransferDataTableToExcel(DataTable dt, string directory, string schemaPath, out string file,
                                             out string message)
        {
            file = string.Empty;
            message = string.Empty;
            if (dt == null || dt.Rows.Count == 0)
                message = "无可用的数据用来导出";
            if (string.IsNullOrEmpty(directory))
            {
                message = "目录路径为空";
                return false;
            }
            if (!Directory.Exists(directory))
            {
                message = string.Format("目录路径[{0}]不存在", directory);
                return false;
            }

            if (!string.IsNullOrEmpty(schemaPath) && !File.Exists(schemaPath))
            {
                message = string.Format("schema文件路径[{0}]不存在", schemaPath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                DataTableStorage sourceProvider = new DataTableStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                ExcelStorage destinationProvider = new ExcelStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = new Hashtable();
                if (string.IsNullOrEmpty(schemaPath))
                    state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
                else
                    state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3["datatable"] = dt;
                state3[ContextState.SchemaFilePath] = schemaPath;
                state3["desDirectory"] = directory;
                try
                {
                    result = target.Run(state3);
                    if (result)
                        file = state3["desFile"] as string ?? string.Empty;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
            }
            return result;
        }

        /// <summary>
        /// 将内存数据导出到CSV文件
        /// </summary>
        /// <param name="dt">待导出的数据</param>
        /// <param name="directory">待导出文件所在的目录绝对物理路径</param>
        /// <param name="schemaPath">schema文件的绝对物理路径</param>
        /// <param name="file">导出文件相对于<paramref name="directory"/>的相对路径</param>
        /// <param name="message">如返回值为false, 则包含失败信息, 否则可忽略</param>
        /// <returns></returns>
        public static bool TransferDataTableToCSVFile(DataTable dt, string directory, string schemaPath, out string file,
                                               out string message)
        {
            file = string.Empty;
            message = string.Empty;
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    message = "无可用的数据用来导出";
            //    return false;
            //}
            if (string.IsNullOrEmpty(directory))
            {
                message = "目录路径为空";
                return false;
            }
            if (!Directory.Exists(directory))
            {
                message = string.Format("目录路径[{0}]不存在", directory);
                return false;
            }

            if (!string.IsNullOrEmpty(schemaPath) && !File.Exists(schemaPath))
            {
                message = string.Format("schema文件路径[{0}]不存在", schemaPath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                DataTableStorage sourceProvider = new DataTableStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                DelimitedFileStorage destinationProvider = new DelimitedFileStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = new Hashtable();
                if (string.IsNullOrEmpty(schemaPath))
                    state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
                else
                    state3["sourceProvider"] = IntegrationMode.GetSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3["datatable"] = dt;
                state3[ContextState.SchemaFilePath] = schemaPath;
                state3["desDirectory"] = directory;
                if (string.IsNullOrEmpty(schemaPath))
                    state3["desPrefix"] = dt.TableName;
                else
                    state3["desPrefix"] = GetTableNameFromSchemaFileName(schemaPath);
                try
                {
                    result = target.Run(state3);
                    if (result)
                        file = state3["desFile"] as string ?? string.Empty;
                    state3.Clear();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
                
            }
            return result;
        }

        public static bool ImportExcelData(string sourcePath, string tableName, out DataTable dt, out string message)
        {
            return ImportExcelData(sourcePath, tableName, out dt, out message, new Hashtable());
        }

        public static bool ImportExcelData(string sourcePath, string tableName, out DataTable dt, out string message, Hashtable param)
        {
            dt = new DataTable();
            message = string.Empty;
            if (string.IsNullOrEmpty(sourcePath))
            {
                message = "数据文件路径为空";
                return false;
            }
            if (!File.Exists(sourcePath))
            {
                message = string.Format("数据文件路径[{0}]不存在", sourcePath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                ExcelStorage sourceProvider = new ExcelStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                DataTableStorage destinationProvider = new DataTableStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = param;
                state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3["tableName"] = tableName;
                state3[ContextState.SourcePath] = sourcePath;

                try
                {
                    result = target.Run(state3);
                    dt = state3["datatable"] as DataTable;
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result = false;
                        message = "没有读取到数据。";
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    message = ex.Message;
                }
            }
            return result;
        }

        public static bool ImportCSVData(string sourcePath, out DataTable dt, out string message)
        {
            return ImportCSVData(sourcePath, out dt, out message, new Hashtable());
        }

        public static bool ImportCSVData(string sourcePath, out DataTable dt, out string message, Hashtable param)
        {
            dt = new DataTable();
            message = string.Empty;
            if (string.IsNullOrEmpty(sourcePath))
            {
                message = "数据文件路径为空";
                return false;
            }
            if (!File.Exists(sourcePath))
            {
                message = string.Format("数据文件路径[{0}]不存在", sourcePath);
                return false;
            }

            bool result;

            using (IntegrationEngine target = new IntegrationEngine())
            {
                DelimitedFileStorage sourceProvider = new DelimitedFileStorage("sourceProvider");
                target.Providers.Add(sourceProvider);
                DataTableStorage destinationProvider = new DataTableStorage("destinationProvider");
                target.Providers.Add(destinationProvider);

                IDictionary state3 = param;
                state3["sourceProvider"] = IntegrationMode.CreateSchema | IntegrationMode.GetData;
                state3["destinationProvider"] = IntegrationMode.TransferData;
                state3[ContextState.SourcePath] = sourcePath;
                state3[ContextState.FlatFileDelimiter] = ",";
                state3[ContextState.FlatFileSkipFirstLine] = true;
                state3[ContextState.FlatFileSkipLastLine] = false;

                try
                {
                    result = target.Run(state3);
                    dt = state3["datatable"] as DataTable;
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        result = false;
                        message = "没有读取到数据。";
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    message = ex.Message;
                }
            }
            return result;
        }

        public static bool ExportData(DataTable dt, string directory, out string file,
                                               out string message)
        {
            // TODO: left the choice to be CSV or EXCEL
            return TransferDataTableToCSVFile(dt, directory, null, out file, out message);
            //return TransferDataTableToExcel(dt, directory, null, out file, out message);
        }

        public static bool ExportData<T>(BusinessObjectCollection<T> bocollection, string directory, out string file,
                                               out string message) where T : BusinessObject
        {
            return TransferDataTableToCSVFile(bocollection.ToDataTable(), directory, null, out file, out message);
        }

        private static string GetTableNameFromSchemaFileName(string schemaPath)
        {
            int directoryIndex = schemaPath.LastIndexOf('\\') + 1;
            int extensionIndex = schemaPath.LastIndexOf('.');
            return schemaPath.Substring(directoryIndex, extensionIndex - directoryIndex);
        }
        public static string EnsureDataTableQualify(DataTable dt, string schemapath, Hashtable param)
        {
            return MiscUtil.EnsureDataTableQualify(dt, schemapath, param);
        }


        //public static bool GetImportedDataTable(HttpPostedFile fileUploaded, string uploadFolder, out string message,
        //                                            out DataTable dt)
        //{
        //    return GetImportedDataTable(fileUploaded, uploadFolder, out message, out dt, new Hashtable());
        //}

        public static bool GetImportedDataTable(HttpPostedFile fileUploaded, string uploadFolder, out string message,
                                               out DataTable dt, Hashtable param, string configFileName)
        {
            dt = new DataTable();
            if (fileUploaded == null || string.IsNullOrEmpty(fileUploaded.FileName))
            {
                message = "请选择上传文件";
                return false;
            }

            const int maxLength = 1024 * 1024 * 8; //最大4M，否则内存占用太严重,需要让用户自己拆分文件
            if (fileUploaded.ContentLength <= 0 || fileUploaded.ContentLength > maxLength)
            {
                message = "上传文件大小应在8M内";
                return false;
            }

            string mime = fileUploaded.ContentType;
            //text/csv
            if (!mime.Equals("text/csv", StringComparison.InvariantCultureIgnoreCase) &&
                !mime.Equals("application/vnd.ms-excel", StringComparison.InvariantCultureIgnoreCase) &&
                !fileUploaded.FileName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                message = string.Format("上传文件的格式'{0}'无法识别, 或者文件正在被使用.", mime);
                return false;
            }

            string filename = fileUploaded.FileName;
            filename = filename.Substring(filename.LastIndexOf('\\') + 1);


            //将上传的文件保存到服务器磁盘
            string tempFileName = Path.Combine(uploadFolder, "temp_" + DateTime.Now.ToFileTime() + filename);
            fileUploaded.SaveAs(tempFileName);
            bool result = false;
            try
            {
                //return new ExcelHelper().ImportExcelData(tempFileName, "", out dt, out message, param);
                result = ImportExcelData(tempFileName, "", out dt, out message, param);
            }
            catch (InvalidDataException)
            {
                dt = null;
                message = "ERROR";
                result = false;
            }

            if (result)
            {
                if (File.Exists(configFileName))
                {
                    string qualifyResult = EnsureDataTableQualify(dt, configFileName, param);
                    if (!string.IsNullOrEmpty(qualifyResult))
                    {
                        message += qualifyResult;
                        result = false;
                    }
                }
            }
            File.Delete(tempFileName);
            return result;
        }
    }


}