using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;

namespace ECount.Infrustructure.Data
{
    public class ExcelReader : IDisposable
    {
        static object _syncRoot = new object();
        #region Variables

        private bool _blnHeaders = true;
        private bool _blnKeepConnectionOpen;
        private bool _blnMixedData = false;
        private OleDbCommand _oleCmdSelect;
        private OleDbConnection _oleConn;
        private string _strExcelFilename;
        private string _strSheetName;
        private string _strSheetRange;

        #endregion

        public ExcelReader(string excelPath)
        {
            _strExcelFilename = excelPath;
        }

        #region properties

        public string ExcelFilename
        {
            get { return _strExcelFilename; }
            set { _strExcelFilename = value; }
        }

        public string SheetName
        {
            get { return _strSheetName; }
            set { _strSheetName = value; }
        }

        public string SheetRange
        {
            get { return _strSheetRange; }
            set
            {
                if (value.IndexOf(':') == -1)
                    throw new Exception("Invalid range length");
                _strSheetRange = value;
            }
        }

        public bool KeepConnectionOpen
        {
            get { return _blnKeepConnectionOpen; }
            set { _blnKeepConnectionOpen = value; }
        }

        public bool Headers
        {
            get { return _blnHeaders; }
            set { _blnHeaders = value; }
        }

        public bool MixedData
        {
            get { return _blnMixedData; }
            set { _blnMixedData = value; }
        }

        public string ColName(int intCol)
        {
            string sColName;
            if (intCol < 26)
                sColName = Convert.ToString(Convert.ToChar((Convert.ToByte('A') + intCol)));
            else
            {
                int intFirst = (intCol / 26);
                int intSecond = (intCol % 26);
                sColName = Convert.ToString(Convert.ToByte('A') + intFirst);
                sColName += Convert.ToString(Convert.ToByte('A') + intSecond);
            }
            return sColName;
        }

        public int ColNumber(string strCol)
        {
            strCol = strCol.ToUpper();
            int intColNumber;
            if (strCol.Length > 1)
            {
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1]) - 65);
                intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1]) - 64) * 26;
            }
            else
                intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0]) - 65);
            return intColNumber;
        }

        public string[] GetExcelNames()
        {
            DataTable dt = null;

            try
            {
                if (_oleConn == null)
                    Open();

                // Get the data table containing the schema
                dt = _oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                    return null;

                string[] excelSheets = new string[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString();
                    excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                    i++;
                }


                return excelSheets;
            }
            catch
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
                if (dt != null)
                    dt.Dispose();
            }
        }

        public string[] GetExcelTableNames()
        {
            DataTable dt = null;
            try
            {
                if (_oleConn == null)
                    Open();

                // Get the data table containing the schema
                dt = _oleConn.GetSchema("Tables", null);

                if (dt == null)
                    return null;

                List<string> excelSheets = new List<string>();

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString().Trim('\'');
                    if (strSheetTableName.Contains("<") ||
                        strSheetTableName.Contains(">"))
                        throw new InvalidDataException("Excel文件的表名不能包含'<'和'>'。");
                    if (strSheetTableName.EndsWith("$"))
                        excelSheets.Add(strSheetTableName.TrimEnd('$'));
                }


                return excelSheets.ToArray();
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
                if (dt != null)
                    dt.Dispose();
            }
        }

        public DataTable GetTableSchema(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentException("table name can not be empty!", "tableName");
            try
            {
                if (_oleConn == null)
                    Open();
                if (!tableName.EndsWith("$"))
                    tableName += "$";
                // Get the data table containing the schema
                return _oleConn.GetSchema("Columns", new string[]
				                                     	{
				                                     		null,
				                                     		null, tableName, null
				                                     	});
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (!KeepConnectionOpen)
                    Close();
            }
        }

        #endregion

        #region Excel Connection

        private string ExcelConnectionOptions()
        {
            string strOpts = "";
            if (MixedData)
                strOpts += "Imex=2;";
            else
            {
                strOpts += "Imex=1;";
            }
            if (Headers)
                strOpts += "HDR=Yes;";
            else
                strOpts += "HDR=No;";
            return strOpts;
        }


        private string ExcelConnection()
        {
            return string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties={1}Excel 8.0;{2}{1}",
                                 _strExcelFilename,
                                 Convert.ToChar(34),
                                 ExcelConnectionOptions());
        }

        #endregion

        #region Open / Close

        public void Open()
        {
            Close();

            if (!File.Exists(_strExcelFilename))
                throw new FileNotFoundException("", _strExcelFilename);
            _oleConn = new OleDbConnection(ExcelConnection());
            _oleConn.Open();
        }

        public void Close()
        {
            if (_oleConn != null)
            {
                if (_oleConn.State != ConnectionState.Closed)
                    _oleConn.Close();
                _oleConn.Dispose();
                _oleConn = null;
            }
        }

        #endregion

        #region Command Select

        private bool SetSheetQuerySelect()
        {
            if (_oleConn == null)
                throw new Exception("Connection is unassigned or closed.");

            if (_strSheetName.Length == 0)
                throw new Exception("Sheetname was not assigned.");

            _oleCmdSelect = new OleDbCommand(string.Format("SELECT * FROM [{0}${1}]", _strSheetName, _strSheetRange), _oleConn);

            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_oleConn != null)
            {
                _oleConn.Dispose();
                _oleConn = null;
            }
            if (_oleCmdSelect != null)
            {
                _oleCmdSelect.Dispose();
                _oleCmdSelect = null;
            }
        }

        #endregion

        public DataTable GetTable()
        {
            return GetTable("Sheet1");
        }

        public DataTable GetTable(string strTableName)
        {
            if (strTableName.EndsWith("$"))
                strTableName.TrimEnd('$');
            _strSheetName = strTableName;
            //Open and query

            DataTable dt = new DataTable(strTableName);
            if (_oleConn == null)
                Open();
            if (_oleConn.State != ConnectionState.Open)
                throw new Exception("Connection cannot open error.");
            if (!SetSheetQuerySelect())
                return null;

            //Fill table
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
            oleAdapter.SelectCommand = _oleCmdSelect;
            oleAdapter.FillSchema(dt, SchemaType.Source);
            oleAdapter.Fill(dt);
            if (!Headers)
                if (_strSheetRange.IndexOf(":") > 0)
                {
                    string FirstCol = _strSheetRange.Substring(0, _strSheetRange.IndexOf(":") - 1);
                    int intCol = ColNumber(FirstCol);
                    for (int intI = 0; intI < dt.Columns.Count; intI++)
                    {
                        dt.Columns[intI].Caption = ColName(intCol + intI);
                    }
                }
            // remove the empty column
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(dt.Columns[i].ColumnName))
                {
                    dt.Columns.RemoveAt(i);
                    i--;
                }
            }

            // remove the empty rows
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool hasValue = false;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null && dt.Rows[i][j] != DBNull.Value)
                    {
                        dt.Rows[i][j] = dt.Rows[i][j].ToString().Replace("\t", "");//add by chenyh
                        hasValue = true;
                    }
                }
                if (!hasValue)
                    dt.Rows[i].Delete();
            }
            dt.AcceptChanges();
            //Cannot delete rows in Excel workbook
            dt.DefaultView.AllowDelete = false;

            //Clean up
            _oleCmdSelect.Dispose();
            oleAdapter.Dispose();
            if (!KeepConnectionOpen) Close();
            return dt;
        }


        public static bool SaveData(string directory, DataTable dt, out string file, out string message)
        {
            file = string.Empty;
            if (!Directory.Exists(directory))
            {
                message = "can not found directory: " + directory;
                return false;
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                message = "can not found any data";
                return false;
            }

            message = string.Empty;

            lock (_syncRoot)
            {
                file = string.Concat(dt.TableName, "_", DateTime.Now.ToString("yyyyMMddHHmmssfff"), ".xml");
            }
            string filename = Path.Combine(directory, file);

            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.Write(
                    @"<?xml version=""1.0""?>
<?mso-application progid=""Excel.Sheet""?>
<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:o=""urn:schemas-microsoft-com:office:office""
 xmlns:x=""urn:schemas-microsoft-com:office:excel""
 xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:html=""http://www.w3.org/TR/REC-html40"">
 <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">
  <WindowHeight>9480</WindowHeight>
  <WindowWidth>15135</WindowWidth>
  <WindowTopX>120</WindowTopX>
  <WindowTopY>60</WindowTopY>
  <ProtectStructure>False</ProtectStructure>
  <ProtectWindows>False</ProtectWindows>
 </ExcelWorkbook>
 <Styles>
  <Style ss:ID=""Default"" ss:Name=""Normal"">
   <Alignment ss:Vertical=""Center""/>
   <Borders/>
   <Font ss:FontName=""宋体"" x:CharSet=""134"" ss:Size=""11"" ss:Color=""#000000""/>
   <Interior/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID=""s62"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#000000""
    ss:Bold=""1""/>
   <Interior/>
  </Style>
  <Style ss:ID=""s64"">
   <Alignment ss:Vertical=""Bottom"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""12"" ss:Color=""#000000""/>
   <Interior/>
  </Style>
 </Styles>\n");

                sw.Write(" <Worksheet ss:Name=\"{0}\">", dt.TableName);

                int fieldCount = dt.Columns.Count;
                sw.Write(
                    "  <Table ss:ExpandedColumnCount=\"{0}\" ss:ExpandedRowCount=\"{1}\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"15.75\">\n",
                    fieldCount, dt.Rows.Count + 1);

                bool isStart = true;

                foreach (DataRow dr in dt.Rows)
                {
                    if (isStart)
                    {
                        sw.Write("   <Row>\n");
                        // write column name
                        for (int i = 0; i < fieldCount; i++)
                        {
                            sw.Write("    <Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">{0}</Data></Cell>\n",
                                     HttpUtility.HtmlEncode(dt.Columns[i].ColumnName));
                        }
                        sw.Write("   </Row>\n");

                        isStart = false;
                    }

                    sw.Write("   <Row>\n");
                    for (int i = 0; i < fieldCount; i++)
                    {
                        sw.Write("    <Cell ss:StyleID=\"s64\"><Data ss:Type=\"{0}\">{1}</Data></Cell>\n",
                                 GetExcelType(dt.Columns[i].DataType),
                                 (dr[i] == null || dr[i] == DBNull.Value) ? string.Empty : HttpUtility.HtmlEncode(dr[i].ToString()));
                    }
                    sw.Write("   </Row>\n");
                }
                sw.Write(
                    @"  </Table>
  <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
   <PageSetup>
    <Header x:Margin=""0.3""/>
    <Footer x:Margin=""0.3""/>
    <PageMargins x:Bottom=""0.75"" x:Left=""0.7"" x:Right=""0.7"" x:Top=""0.75""/>
   </PageSetup>
   <Selected/>
   <Panes />
   <ProtectObjects>False</ProtectObjects>
   <ProtectScenarios>False</ProtectScenarios>
  </WorksheetOptions>
 </Worksheet>
</Workbook>");
                sw.Close();
            }
            return true;
        }

        private static string GetExcelType(Type dataType)
        {
            if (dataType == typeof(int) ||
                dataType == typeof(float) ||
                dataType == typeof(decimal))
                return "Number";
            return "String";
        }
    }
}