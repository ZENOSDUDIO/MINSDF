using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGM.ECount.DataModel;
using System.Data;
using SGM.Common.Utility;
using System.Data.Common;

namespace SGM.ECount.BLL
{
    public class DiffAnalyseReportBLL : BaseGenericBLL<DiffAnalyseReport>
    {
        public DiffAnalyseReportBLL()
            : base("DiffAnalyseReport")
        {
        }

        public DiffAnalyseReport GetAnalyseReport(long reportID)
        {
            return Context.DiffAnalyseReport.Include("DiffAnalyseReportItem").FirstOrDefault(r => r.ReportID == reportID);
        }

        public void ImportRefData(List<View_StocktakeDetails> list, List<long> notiList)
        {
            string[] notiArrary=notiList.Select(n=>n.ToString()).ToArray();
            string notiStr = string.Join(",",notiArrary);
            string refStr = Utils.SerializeToString(list);
            Context.ImportAnalyseRefData(refStr, notiStr);
        }

        public List<DiffAnalyseReport> GetReportsByNotification(StocktakeNotification noti)
        {
            return Context.DiffAnalyseReport.Include("StocktakeNotification").Include("DiffAnalyseReportItem").Where(r => r.StocktakeNotification.NotificationCode == noti.NotificationCode).ToList();
        }
        public List<DiffAnalyseReport> GetReportsList()
        {
            return Context.DiffAnalyseReport.Include("StocktakeNotification").Include("DiffAnalyseReportItem").ToList();
        }

        public DataSet GetReports(List<int> details)
        {
            PlantBLL bll = new PlantBLL();
            List<Plant> plants = bll.GetPlants();
            DataSet ds = new DataSet();
            DataTable dt = GetAnalyseResult(details);
            dt.Columns["PartCode"].ColumnName = "PN";
            dt.Columns["PartChineseName"].ColumnName = "零件名称";
            dt.Columns["PartPlantCode"].ColumnName = "工厂";
            dt.Columns["Workshops"].ColumnName = "车间";
            dt.Columns["Segments"].ColumnName = "工段";
            dt.Columns["RequestUser"].ColumnName = "申请人";
            dt.Columns["TypeName"].ColumnName = "申请类别";
            dt.Columns["RepairAmount"].ColumnName = "返修";
            dt.Columns["CSMTAmount"].ColumnName = "外协";
            dt.Columns["RDCAmount"].ColumnName = "RDC";
            dt.Columns["SGMAmount"].ColumnName = "SGM";
            dt.Columns["Price"].ColumnName = "UNIT PRICE";
            dt.Columns["LevelName"].ColumnName = "盘点级别";
            //dt.Columns["CSMTDUNS"].ColumnName = "外协duns";
            dt.Columns["PlanDate"].ColumnName = "盘点日期";
            /* 2011/04/09 //MRP Controller */
            dt.Columns["WorkLocation"].ColumnName = "PA"; 
            dt.Columns["CategoryName"].ColumnName = "JIT";
            dt.Columns["HOLD"].ColumnName = "其中HOLD";
            dt.Columns["SysHOLD"].ColumnName = "HOLD";
            dt.Columns["UnRecorded"].ColumnName = "分析";
            dt.Columns["Wip"].ColumnName = "WIP";
            dt.Columns["M080"].ColumnName = "M080";
            dt.Columns["Sys_Block_CSMT"].ColumnName = "M60(block)";
            dt.Columns["Specs"].ColumnName = "车型";
            dt.Columns["SupplierName"].ColumnName = "供应商名称";
            foreach (var plant in plants)
            {
                //List<View_DifferenceAnalyse> analyses = Context.View_DifferenceAnalyse.Where(a => a.PartPlantCode == plant.PlantCode && a.Status == Consts.STOCKTAKE_ANALYZING).ToList();
                dt.DefaultView.RowFilter = "[工厂]='" + plant.PlantCode + "'";
                dt.DefaultView.Sort = "PA asc";
                //if (plant.PlantCode == "SH11")
                //{
                //    dt.Columns["M080"].ColumnName = "M180";
                //}
                DataTable filteredTable = dt.DefaultView.ToTable();
                filteredTable.TableName = plant.PlantName + "(" + plant.PlantCode + ")";

                List<string> numericCols=new List<string>();
                for (int j = 0; j < filteredTable.Columns.Count; j++)
                {
                    DataColumn col = filteredTable.Columns[j];
                    if (col.DataType == typeof(Int16) || col.DataType == typeof(Int32) || col.DataType == typeof(Int64) || col.DataType == typeof(Single) || col.DataType == typeof(Double) || col.DataType == typeof(Decimal))
                    {
                        col.ReadOnly = false;
                        numericCols.Add(col.ColumnName);
                    }
                }
                ConsignmentPartBLL cpBll = new ConsignmentPartBLL();
                for (int i = 0; i < filteredTable.Rows.Count; i++)
                {
                    DataRow row = filteredTable.Rows[i];

                    //item.PartID;
                    List<ConsignmentPartRecord> records = cpBll.QueryRecords(new ConsignmentPartRecord { Part = new Part { PartID = Convert.ToInt32(row["PartID"]) } }).ToList();
                    if (records.Count > 0)
                    {
                        foreach (var record in records)
                        {
                            row["车间"] += "/" + record.Supplier.SupplierName;
                        }
                    }
                    foreach (var colName in numericCols)
                    {
                        if (row[colName] == DBNull.Value)
                        {
                            row[colName] = 0;
                        }
                    }
                }

                if (filteredTable != null && filteredTable.Rows.Count > 0)
                {
                    ds.Tables.Add(filteredTable);
                }
            }
            return ds;
        }

        public DataTable GetAnalyseResult(List<int> details)
        {
            string detailsStr = null;
            if (details != null && details.Count > 0)
            {
                string[] detailArray = details.Select(d => d.ToString()).ToArray();
                detailsStr = string.Join(",", detailArray);
            }
            DbParameter paramDetails = Context.CreateDbParameter("@details", DbType.String, detailsStr, ParameterDirection.Input);
            DataTable dt = Context.LoadDataTable("sp_GetDiffAnalyseResult", CommandType.StoredProcedure,paramDetails);
            return dt;
        }
    }
}
