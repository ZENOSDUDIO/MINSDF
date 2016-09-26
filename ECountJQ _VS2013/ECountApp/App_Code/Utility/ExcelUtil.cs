using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Reflection;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;


/// <summary>
/// Summary description for ExcelUtil
/// </summary>
public static class ExcelUtil
{
    private static Page currentPage = HttpContext.Current.Handler as Page;
    private static Object sycObj = new Object();
    private static int incremental = 10;
    
    
    /// <summary>
    /// 按照时间生成excel名称 防止生成相同名的excel造成文件覆盖
    /// </summary>
    /// <returns></returns>
    private static string CreateExcelName()
    {
        lock (sycObj)
        {
            incremental = incremental + 1;
            if (incremental > 99)
                incremental = 10;
            return Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff") + incremental).ToString();
        }
    }

    //public static void ExportExcel(string filePath,IList<string> listColumns, IList<string> listProperty,DataTable dt)
    //{
    //    if (listColumns.Count == 0)
    //    {
    //        throw new IndexOutOfRangeException("没有有效列名!");
    //    }
    //    if (listColumns.Count != listProperty.Count)
    //    {
    //        throw new ArgumentException("列名称和数据表字段要一一对应.");
    //    }
    //    if (dt.Rows.Count == 0)
    //    {
    //        return;
    //    }
    //    string fileName = CreateExcelName() + ".xls";
    //    filePath += fileName;
    //    object objOpt = System.Reflection.Missing.Value;
    //    Excel.Application objExcel = null;
    //    Excel.Workbooks objBooks = null;
    //    Excel.Workbook objBook = null;
    //    try
    //    {
    //        objExcel = new Excel.Application();
    //        objBooks = (Excel.Workbooks)objExcel.Workbooks;
    //        objBook = (Excel.Workbook)(objBooks.Add(objOpt));
    //        // Add data to cells of the first worksheet in the new workbook.
    //        Excel.Sheets objSheets = (Excel.Sheets)objBook.Worksheets;
    //        Excel.Worksheet objSheet = (Excel.Worksheet)(objSheets.get_Item(1));
    //        objSheet.Name = "SheetName";
    //        for(int i=1;i<=listColumns.Count;i++)
    //        {
    //            objSheet.Cells[1, i] = listColumns[i-1];
    //        }
            
    //        Excel.Range objRange = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, listColumns.Count]);
    //        //objRange.Font.Bold = true;
    //        objRange.Font.Size = 14;
    //        objRange.Interior.ColorIndex = 37;
            
    //        //10 threads
    //        //int loopCount = dt.Rows.Count/ 10 + 1;
    //        //int completedCount = 0;
    //        //object syncRoot = new object();

    //        for (int i = 1; i <= listProperty.Count; i++)
    //        {
    //            objRange = objSheet.get_Range(objSheet.Cells[2, i], objSheet.Cells[2 + dt.Rows.Count, i]);
    //            objRange.NumberFormatLocal = "@";
    //            for (int j = 0; j < dt.Rows.Count; j++)
    //            {
    //                objSheet.Cells[2 + j, i] = dt.Rows[j][listProperty[i - 1]].ToString();
    //            }
    //            //for (int j = 0; j < 10; j++)
    //            //{
    //            //    ThreadPool.QueueUserWorkItem(new WaitCallback(
    //            //    delegate(object state)
    //            //    {
    //            //        int startIndex = Convert.ToInt32(state);
    //            //        int endIndex = startIndex + loopCount;
    //            //        endIndex = (endIndex > dt.Rows.Count) ? dt.Rows.Count : endIndex;
    //            //        for (int k = startIndex ; k < endIndex; j++)
    //            //        {
    //            //            objSheet.Cells[2 + k, i] = dt.Rows[k][listProperty[i - 1]].ToString();
    //            //        }
    //            //        lock (syncRoot)
    //            //        {
    //            //            completedCount++;
    //            //        }
    //            //    }), j * loopCount);
    //            //}

    //            //while (completedCount < 10)
    //            //{
    //            //    Thread.Sleep(2000);
    //            //}
    //        }
    //        objSheet.Columns.AutoFit();
    //        if (System.IO.File.Exists(filePath))
    //            System.IO.File.Delete(filePath);
    //        // Save the file.
    //        objBook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookNormal, objOpt, objOpt,
    //                  objOpt, objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
    //                 objOpt, objOpt, objOpt, objOpt, objOpt);
    //        objBook.Close(false, objOpt, objOpt);
    //        //objExcel.Quit();
    //        //objExcel = null;
    //    }
    //    finally
    //    {
    //        if (objExcel != null)
    //        {
    //            try
    //            {
    //                foreach (Excel.Workbook wb in objExcel.Workbooks)
    //                {
    //                    if (wb != null)
    //                    {
    //                        try
    //                        {
    //                            wb.Saved = true;
    //                        }
    //                        catch { }
    //                    }
    //                }
    //                objExcel.Workbooks.Close();
    //                objExcel.Quit();
    //                Marshal.ReleaseComObject(objExcel);
    //                objExcel = null;
    //            }
    //            catch (Exception exx) { }
    //        }
    //    }

    //        // Response the file to client.
    //        HttpResponse response = System.Web.HttpContext.Current.Response;
    //        response.Clear();
    //        response.Buffer = true;
    //        response.ContentType = "application/vnd.ms-excel";
    //        response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
    //        response.ContentEncoding = Encoding.GetEncoding("utf-8");
    //        response.WriteFile(filePath);
    //        response.Flush();

    //        if (System.IO.File.Exists(filePath))
    //            System.IO.File.Delete(filePath);
    //        response.End();
    //}

    public static Dictionary<string, string> GetExportListColumnInfo(string typeName)
    {

        Dictionary<string, string> columns = new Dictionary<string, string>();
        if (string.Equals(typeName, "ViewPart"))
        {
            columns.Add("PartCode", "零件号");
            columns.Add("PartEnglishName", "零件英文名称");
            columns.Add("PartChineseName", "零件中文名称");
            columns.Add("WorkLocation", "工位");
            columns.Add("FollowUp", "FollowUp");
            columns.Add("Specs", "Specs");
            columns.Add("CategoryName", "零件类别");
            columns.Add("PlantCode", "工厂代码");
            columns.Add("PlantName", "工厂名称");
            columns.Add("StatusName", "零件状态");
            columns.Add("LevelName", "盘点级别");
            columns.Add("SupplierName", "供应商名称");
            columns.Add("DUNS", "供应商DUNS");
            columns.Add("GroupName", "零件分组名称");
        }
        return columns;
    }

    public static DataTable ListToDataTable<T>(List<T> entitys)
    {
        //检查实体集合不能为空
        if (entitys == null || entitys.Count < 1)
        {
            throw new Exception("需转换的集合为空");
        }
        //取出第一个实体的所有Propertie
        Type entityType = entitys[0].GetType();
        System.Reflection.PropertyInfo[] entityProperties = entityType.GetProperties();

        //生成DataTable的Structure
        System.Data.DataTable dt = new System.Data.DataTable();
        for (int i = 0; i < entityProperties.Length; i++)
        {
            dt.Columns.Add(entityProperties[i].Name);
        }

        //将所有entity添加到DataTable中
        foreach (object entity in entitys)
        {
            //检查所有的的实体都为同一类型
            if (entity.GetType() != entityType)
            {
                throw new Exception("要转换的集合元素类型不一致");
            }
            object[] entityValues = new object[entityProperties.Length];
            for (int i = 0; i < entityProperties.Length; i++)
            {
                entityValues[i] = entityProperties[i].GetValue(entity, null);
            }
            dt.Rows.Add(entityValues);
        }
        return dt;
    }
}