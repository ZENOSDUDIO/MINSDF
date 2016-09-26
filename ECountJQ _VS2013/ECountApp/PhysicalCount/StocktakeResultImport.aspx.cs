using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECount.ExcelTransfer;
using ECount.Infrustructure.Utilities;
using System.IO;
using System.Data;
using AjaxControlToolkit;
using SGM.ECount.DataModel;

public partial class PhysicalCount_StocktakeResultImport : ECountBasePage
{

    public string NotiID
    {
        get
        {
            return ViewState["ID"].ToString();
        }
        set
        {
            ViewState["ID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            NotiID = Request.QueryString["id"].ToString();
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/StocktakeResult.xml");
        }
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);
    }

    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtSGMStocktakeItem = ue.ContentTable;
        dtSGMStocktakeItem.Columns["序号"].ColumnName = "RowNumber";
        dtSGMStocktakeItem.Columns["零件号"].ColumnName = "PartNo";
        dtSGMStocktakeItem.Columns["工厂"].ColumnName = "Plant";
        dtSGMStocktakeItem.Columns["库位"].ColumnName = "Store";
        dtSGMStocktakeItem.Columns["线旁"].ColumnName = "Line";
        dtSGMStocktakeItem.Columns["加工区"].ColumnName = "Machining";
        dtSGMStocktakeItem.Columns["存储区域"].ColumnName = "SLOC";
        dtSGMStocktakeItem.Columns["起始CSN"].ColumnName = "StartCSN";
        dtSGMStocktakeItem.Columns["终止CSN"].ColumnName = "EndCSN";
        dtSGMStocktakeItem.DefaultView.Sort = "PartNo";
        dtSGMStocktakeItem = dtSGMStocktakeItem.DefaultView.ToTable();

        StocktakeNotification notification = Service.GetNotification(new StocktakeNotification { NotificationID = long.Parse(NotiID) });
        List<View_StocktakeResult> items = new List<View_StocktakeResult>();
        int userID = CurrentUser.UserInfo.UserID;
        List<string> errorMsg = new List<string>();
        List<View_StocktakeResult> itemList = new List<View_StocktakeResult>();
        bool hasError = false;
        for (int i = 0; i < dtSGMStocktakeItem.Rows.Count; i++)
        {
            DataRow row = dtSGMStocktakeItem.Rows[i];
            string partNo = row["PartNo"].ToString();
            string plantCode = row["Plant"].ToString();
            string duns = row["DUNS"].ToString();
            string sloc = row["SLOC"].ToString();
            StoreLocation location = this.StoreLocations.Find(l => l.LogisticsSysSLOC == sloc);
            if (location == null)
            {
                string msg = string.Format("存储区域【{0}】不存在", sloc);// i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
                continue;
            }


            if (i % 1000 == 0)
            {
                string startCode = partNo;
                int end = i + 999;
                if (end >= dtSGMStocktakeItem.Rows.Count)
                {
                    end = dtSGMStocktakeItem.Rows.Count - 1;
                }
                string endCode = dtSGMStocktakeItem.Rows[end]["PartNo"].ToString();
                items = Service.GetStocktakeResultOfScope(new View_StocktakeResult { NotificationID = notification.NotificationID }, startCode, endCode);
            }

            View_StocktakeResult result = null;
            if (result == null)
            {
                result = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.SGMLocationID != null && r.SGMLocationID == location.LocationID);// && r.SGMItemID != null);
            }
            if (result == null)
            {
                result = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.RepairLocationID != null && r.RepairLocationID == location.LocationID);//&& r.RepairItemID != null);
            }
            if (result == null)
            {
                result = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.CSMTLocationID != null && r.CSMTLocationID == location.LocationID);//&& r.CSMTItemID != null);
            }
            if (result == null)
            {
                result = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.GeneralItemID != null && r.GeneralItemID == location.LocationID);//&& r.CSMTItemID != null);
            }
            if (result == null)
            {
                result = items.FirstOrDefault(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.RDCLocationID != null && r.RDCLocationID == location.LocationID);//&& r.RDCItemID != null);
            }
            if (result == null)
            {
                string msg = string.Format("工厂【{0}】，DUNS【{1}】，存储区域为【{2}】的零件【{3}】不在盘点通知单中", plantCode, duns, sloc, partNo);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {

                string csmtDUNS = CurrentUser.UserInfo.ConsignmentDUNS;
                View_StocktakeResult result1 = null;
                result1 = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.CSMTLocationID != null && r.CSMTLocationID == location.LocationID);
                if ( string.IsNullOrEmpty(csmtDUNS) && (result1 != null) )
                {
                    string msg = string.Format("当前用户无权导入工厂【{0}】，DUNS【{1}】，存储区域为【{2}】的零件【{3}】", plantCode, duns, sloc, partNo);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
                if (!string.IsNullOrEmpty(csmtDUNS) && !items.Exists(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && string.Equals(r.CSMTDUNS, csmtDUNS)))
                {
                    string msg = string.Format("当前用户无权导入工厂【{0}】，DUNS【{1}】，存储区域为【{2}】的零件【{3}】", plantCode, duns, sloc, partNo);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
                View_StocktakeResult result2 = null;
                result2 = items.Find(r => r.PartCode == partNo && r.PartPlantCode == plantCode && r.DUNS == duns && r.SGMLocationID != null && r.SGMLocationID == location.LocationID);
               if ((CurrentUser.UserInfo.Workshop== null) && (result2 != null))
                {
                    string msg = string.Format("当前用户无权导入当前车间，工厂【{0}】，DUNS【{1}】，存储区域为【{2}】的零件【{3}】的实盘结果", plantCode, duns, sloc, partNo);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
                if (!hasError)
                {
                    decimal? available= SGM.Common.Utility.Utils.GetDecimalDbCell(row["Available"]);
                    decimal? qi = SGM.Common.Utility.Utils.GetDecimalDbCell(row["QI"]);
                    decimal? block = SGM.Common.Utility.Utils.GetDecimalDbCell(row["Block"]);

                    WorkshopStocktakeDetail detail = null;

                    if (CurrentUser.UserInfo.Workshop != null)
                    {
                        detail =
result.WorkshopDetails.FirstOrDefault(d => d.WorkshopCode == CurrentUser.UserInfo.Workshop.WorkshopCode);
                    }
                    
                    if (result.SGMItemID != null && result.SGMLocationID == location.LocationID && CurrentUser.UserInfo.Workshop != null && detail != null)
                    {
                        for (int j = result.WorkshopDetails.Count - 1; j >= 0; j--)
                        {
                            if (result.WorkshopDetails[j].ItemDetailID!=detail.ItemDetailID)
                            {
                                result.WorkshopDetails.RemoveAt(j);
                            }
                        }
                        detail.SGMFillinBy = userID;
                        detail.SGMBlock = block;
                        detail.SGMQI = qi;
                        detail.Line = SGM.Common.Utility.Utils.GetDecimalDbCell(row["Line"]);
                        detail.Machining = SGM.Common.Utility.Utils.GetDecimalDbCell(row["Machining"]);
                        detail.Store = SGM.Common.Utility.Utils.GetDecimalDbCell(row["Store"]);
                        if (row["StartCSN"] != DBNull.Value && row["StartCSN"] + "" != string.Empty)
                        {
                            detail.StartCSN = row["StartCSN"].ToString();
                        }
                        if (row["EndCSN"] != DBNull.Value && row["EndCSN"] + "" != string.Empty)
                        {
                            detail.EndCSN = row["EndCSN"].ToString();
                        }
                    }
                    else
                    {

                        if (result.CSMTItemID != null && result.CSMTLocationID == location.LocationID)
                        {
                            result.CSMTFillinBy = userID;
                            result.CSMTAvailable = available;
                            result.CSMTBlock = block;
                            result.CSMTQI = qi;
                        }
                        else
                        {

                            if (result.RepairItemID != null && result.RepairLocationID == location.LocationID)
                            {
                                result.RepairFillinBy = userID;
                                result.RepairAvailable = available;
                                result.RepairBlock = block;
                                result.RepairQI = qi;
                            }
                            else
                            {
                                if (result.GeneralItemID != null && result.GenerLocationID == location.LocationID)
                                {
                                    result.GenFillinBy = userID;
                                    result.GeneralAvailable = available;
                                    result.GeneralBlock = block;
                                    result.GeneralQI = qi;
                                }
                                else
                                {
                                    if (result.RDCItemID != DefaultValue.LONG && result.RDCLocationID == location.LocationID)
                                    {
                                        result.RDCFillinBy = userID;
                                        result.RDCAvailable = available;
                                        result.RDCBlock = block;
                                        result.RDCQI = qi;
                                    }
                                }
                            }
                        }
                    }
                    //result.SGMBlock = int.Parse(dtSGMStocktakeItem.Rows[i]["Block"].ToString());

                    itemList.Add(result);
                }
            }
        }
        if (!hasError)
        {

            //List<View_StocktakeResult> tmpList = new List<View_StocktakeResult>();
            //string cacheKey = string.Empty;
            //while (itemList.Count > 0)
            //{
            //    tmpList.Clear();
            //    tmpList.AddRange(itemList.Take((itemList.Count < 20) ? itemList.Count : 20));
            //    itemList.RemoveRange(0, (itemList.Count < 20) ? itemList.Count : 20);
            //    if (itemList.Count > 0)
            //    {
            //        cacheKey = Service.ImportResult(notification, tmpList, cacheKey, false);
            //    }
            //    else
            //    {
            //        Service.ImportResult(notification, tmpList, cacheKey, true);
            //    }
            //    //fill in items
            //}
            Service.FillStocktakeResult(notification, itemList);
            BindDataControl(gvItems, dtSGMStocktakeItem);
            //show information
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }


    protected void gvItems_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("Plant");
        dt.Columns.Add("DUNS");
        dt.Columns.Add("SLOC");
        dt.Columns.Add("Line");
        dt.Columns.Add("Store");
        dt.Columns.Add("Machining");
        dt.Columns.Add("Block");
        dt.Columns.Add("Available");
        dt.Columns.Add("QI");
        dt.Columns.Add("StartCSN");
        dt.Columns.Add("EndCSN");
        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvItems, dt);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("StocktakeNoticeList.aspx?View=Result");
        }
    }
}
