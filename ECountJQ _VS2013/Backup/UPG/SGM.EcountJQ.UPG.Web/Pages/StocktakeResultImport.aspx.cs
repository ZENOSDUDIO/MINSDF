using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECountJQ.UPG.BLL;
using System.IO;
using ECount.ExcelTransfer;
using System.Data;
using System.Collections;
using System.Text;

namespace SGM.ECountJQ.UPG.Web.Pages
{
    public partial class StocktakeResultImport : System.Web.UI.Page
    {
        protected Int64 NoficicationID = 0;

        protected StocktakeNotification Noficication;

        private string SchemaFilePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SchemaFilePath = Server.MapPath(@"~/ImportSchema/StocktakeResult.xml");
            InitPageData();
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitPageData()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                this.NoficicationID = Int64.Parse(Request["id"]);
            }

            if (this.NoficicationID == 0)
            {
                ShowPageError("获取通知单ID失败！");
                return;
            }

            if (this.NoficicationID != 0)
            {
                this.Noficication = StocktakeNotification.Find(this.NoficicationID);
            }

            if (this.Noficication == null)
            {
                ShowPageError("获取通知单信息失败！");
                return;
            }
        }

        /// <summary>
        /// 显示页面错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        private void ShowPageError(string msg)
        {
            this.divError.Visible = true;
            this.divMain.Visible = false;

            this.divError.InnerHtml = string.Format(@"<font color=""red"">{0}</font>", msg);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ValidImportData())
            {
                string filename = this.fuImportFile.PostedFile.FileName;
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);

                string tempFilePath = Path.Combine(Server.MapPath("/"), "ImportTemp/StocktakeResult_" + DateTime.Now.ToFileTime() + filename);

                try
                {
                    this.fuImportFile.PostedFile.SaveAs(tempFilePath);
                    DataTable dt = new DataTable();
                    string readDataMsg = string.Empty;
                    if (!ExcelHelper.GetImportedDataTable(this.fuImportFile.PostedFile, Server.MapPath("/"), out readDataMsg, out dt, new Hashtable(), this.SchemaFilePath))
                    {
                        ShowImportErrorInfo(readDataMsg);
                        return;
                    }

                    if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                    {
                        ShowImportErrorInfo("导入数据为空！");
                        return;
                    }

                    List<StocktakeResultImportRow> list = StocktakeResultImportRow.FindAll(dt);
                    if (list == null || list.Count == 0)
                    {
                        ShowImportErrorInfo("导入数据集合为空！");
                        return;
                    }

                    #region 基础数据

                    List<StoreLocation> AllStoreLocation = StoreLocation.FindAll();
                    List<StocktakeResultSimple> AllStocktakeResultSimple = StocktakeResultSimple.FindAll(this.Noficication.NotificationID, Int64.MinValue, Int64.MaxValue);

                    #endregion

                    //SGM导入数据
                    List<StocktakeItemSimple> StocktakeItems = new List<StocktakeItemSimple>();
                    //外协供应商导入数据
                    List<SupplierStocktakeItemSimple> SupplierStocktakeItems = new List<SupplierStocktakeItemSimple>();

                    List<string> errorList = new List<string>();
                    foreach (StocktakeResultImportRow stocktakeResult in list)
                    {
                        #region 验证数据

                        //存储区域
                        StoreLocation sloc = FindStoreLocation(AllStoreLocation, stocktakeResult.StoreLocation);
                        if (sloc == null)
                        {
                            errorList.Add(string.Format(@"序号【{0}】：存储区域【{1}】不存在", stocktakeResult.SerialNumber, stocktakeResult.StoreLocation));
                            continue;
                        }

                        StocktakeResultSimple StocktakeResult = FindStocktakeResult(AllStocktakeResultSimple, stocktakeResult.PartNumber, stocktakeResult.Plant, stocktakeResult.DUNS, sloc.LocationID);
                        if (StocktakeResult == null)
                        {
                            errorList.Add(string.Format(@"序号【{0}】：工厂【{1}】，DUNS【{2}】，存储区域为【{3}】的零件【{4}】不在盘点通知单中",
                                stocktakeResult.SerialNumber, stocktakeResult.Plant, stocktakeResult.DUNS, stocktakeResult.StoreLocation, stocktakeResult.PartNumber));
                            continue;
                        }

                        #endregion

                        #region 数据验证通过，将数据装箱

                        if (StocktakeResult.TypeID == 1)
                        {
                            StocktakeItemSimple item = new StocktakeItemSimple();
                            item.ItemID = StocktakeResult.ItemID;
                            item.Line = string.IsNullOrEmpty(stocktakeResult.Line) ? 0 : decimal.Parse(stocktakeResult.Line);
                            item.Machining = string.IsNullOrEmpty(stocktakeResult.Machining) ? 0 : decimal.Parse(stocktakeResult.Machining);
                            item.Store = string.IsNullOrEmpty(stocktakeResult.Store) ? 0 : decimal.Parse(stocktakeResult.Store);
                            item.StartCSN = stocktakeResult.CSNStart;
                            item.EndCSN = stocktakeResult.CSNEnd;
                            item.Block = string.IsNullOrEmpty(stocktakeResult.Block) ? 0 : decimal.Parse(stocktakeResult.Block);
                            item.Available = string.IsNullOrEmpty(stocktakeResult.Available) ? 0 : decimal.Parse(stocktakeResult.Available);
                            item.QI = string.IsNullOrEmpty(stocktakeResult.QI) ? 0 : decimal.Parse(stocktakeResult.QI);
                            //item.BlockAdjust = stocktakeResult.Block
                        }

                        #endregion
                    }

                    #region 显示导出错误信息
                    if (errorList != null && errorList.Count != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(string.Format(@"共【{0}】条错误数据</br>", errorList.Count));

                        foreach (string error in errorList)
                        {
                            sb.AppendFormat(@"{0}</br>", error);
                        }

                        ShowImportErrorInfo(sb.ToString());
                    }
                    #endregion
                    else
                    {
                        ShowImportErrorInfo("导入成功！");
                    }
                }
                catch (Exception er)
                {
                    ShowImportErrorInfo(er.Message);
                }
            }
        }

        /// <summary>
        /// 验证导入的数据
        /// </summary>
        private bool ValidImportData()
        {
            tdImportError.Visible = false;
            tdImportError.InnerText = "&nbsp;";

            if (this.fuImportFile.PostedFile == null)
            {
                ShowImportErrorInfo("请选择需要上传的文件！");
                return false;
            }

            string mineType = this.fuImportFile.PostedFile.ContentType;
            if (!mineType.Equals("text/csv", StringComparison.InvariantCultureIgnoreCase) && !mineType.Equals("application/vnd.ms-excel", StringComparison.InvariantCultureIgnoreCase) && !this.fuImportFile.FileName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                ShowImportErrorInfo(string.Format("上传文件的格式'{0}'无法识别, 或者文件正在被使用.", mineType));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 显示导入错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        private void ShowImportErrorInfo(string msg)
        {
            tdImportError.Visible = true;
            tdImportError.InnerHtml = string.Format(@"<font color=""red"">{0}</font>", msg);
        }

        #region 基础数据校验

        /// <summary>
        /// 从集合中查找存储区域
        /// </summary>
        /// <param name="list">所有存储区域集合</param>
        /// <param name="sloc">LogisticsSysSLOC</param>
        /// <returns>存储区域</returns>
        public StoreLocation FindStoreLocation(List<StoreLocation> list, string sloc)
        {
            if (list == null || list.Count == 0 || string.IsNullOrEmpty(sloc))
            {
                return null;
            }

            return list.Find(delegate(StoreLocation obj) { return obj.LogisticsSysSLOC == sloc; });
        }

        public StocktakeResultSimple FindStocktakeResult(List<StocktakeResultSimple> list, string partNumber, string plantCode, string duns, int locationId)
        {
            if (list == null || list.Count == 0 || string.IsNullOrEmpty(partNumber))
            {
                return null;
            }

            return list.Find(delegate(StocktakeResultSimple obj) { return obj.PartCode == partNumber && obj.PlantCode == plantCode && obj.DUNS == duns && obj.LocationID == locationId; });
        }

        #endregion
    }

    public class StocktakeResultImportRow
    {
        #region Properties

        /// <summary>
        /// 序号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string Plant { get; set; }

        /// <summary>
        /// DUNS
        /// </summary>
        public string DUNS { get; set; }

        /// <summary>
        /// 存储区域
        /// </summary>
        public string StoreLocation { get; set; }

        /// <summary>
        /// 库位
        /// </summary>
        public string Store { get; set; }

        /// <summary>
        /// 线旁
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// 加工区
        /// </summary>
        public string Machining { get; set; }

        /// <summary>
        /// Available
        /// </summary>
        public string Available { get; set; }

        /// <summary>
        /// Block
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// QI
        /// </summary>
        public string QI { get; set; }

        /// <summary>
        /// 起始CSN
        /// </summary>
        public string CSNStart { get; set; }

        /// <summary>
        /// 终止CSN
        /// </summary>
        public string CSNEnd { get; set; }

        #endregion

        public StocktakeResultImportRow()
        {
        }

        public StocktakeResultImportRow(DataRow dr)
        {
            if (dr != null)
            {
                this.SerialNumber = dr.Table.Columns.Contains("序号") && dr["序号"] != null ? dr["序号"].ToString() : string.Empty;
                this.PartNumber = dr.Table.Columns.Contains("零件号") && dr["零件号"] != null ? dr["零件号"].ToString() : string.Empty;
                this.Plant = dr.Table.Columns.Contains("工厂") && dr["工厂"] != null ? dr["工厂"].ToString() : string.Empty;
                this.DUNS = dr.Table.Columns.Contains("DUNS") && dr["DUNS"] != null ? dr["DUNS"].ToString() : string.Empty;
                this.StoreLocation = dr.Table.Columns.Contains("存储区域") && dr["存储区域"] != null ? dr["存储区域"].ToString() : string.Empty;
                this.Store = dr.Table.Columns.Contains("库位") && dr["库位"] != null ? dr["库位"].ToString() : string.Empty;
                this.Line = dr.Table.Columns.Contains("线旁") && dr["线旁"] != null ? dr["线旁"].ToString() : string.Empty;
                this.Machining = dr.Table.Columns.Contains("加工区") && dr["加工区"] != null ? dr["加工区"].ToString() : string.Empty;
                this.Available = dr.Table.Columns.Contains("Available") && dr["Available"] != null ? dr["Available"].ToString() : string.Empty;
                this.Block = dr.Table.Columns.Contains("Block") && dr["Block"] != null ? dr["Block"].ToString() : string.Empty;
                this.QI = dr.Table.Columns.Contains("QI") && dr["QI"] != null ? dr["QI"].ToString() : string.Empty;
                this.CSNStart = dr.Table.Columns.Contains("CSNStart") && dr["CSNStart"] != null ? dr["CSNStart"].ToString() : string.Empty;
                this.CSNEnd = dr.Table.Columns.Contains("CSNEnd") && dr["CSNEnd"] != null ? dr["CSNEnd"].ToString() : string.Empty;
            }
        }

        public static List<StocktakeResultImportRow> FindAll(DataTable dt)
        {
            List<StocktakeResultImportRow> list = new List<StocktakeResultImportRow>();

            if (dt != null && dt.Rows != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StocktakeResultImportRow entity = new StocktakeResultImportRow(dr);
                    list.Add(entity);
                }
            }

            if (list != null)
            {
                list.Sort(delegate(StocktakeResultImportRow a, StocktakeResultImportRow b) { return a.PartNumber.CompareTo(b.PartNumber); });
            }

            return list;
        }
    }
}