using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SGM.ECount.DataModel;

public partial class PhysicalCount_AdjustmentImport : ECountBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/Adjustment.xml");
        }
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);
    }

    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtAdjustment = ue.ContentTable;

        dtAdjustment.Columns["通知单号"].ColumnName = "NotificationNo";
        dtAdjustment.Columns["零件号"].ColumnName = "PartNo";
        dtAdjustment.Columns["工厂代码"].ColumnName = "PlantNo";
        dtAdjustment.Columns["供应商DUNS"].ColumnName = "DUNS";
        dtAdjustment.Columns["SAP存储区域代码"].ColumnName = "SLOCID";
        dtAdjustment.Columns["Available调整值"].ColumnName = "AvailableAdjust";
        dtAdjustment.Columns["QI调整值"].ColumnName = "QIAdjust";
        dtAdjustment.Columns["Block调整值"].ColumnName = "BlockAdjust";

        //StocktakeNotification notification = Service.GetNotification(new StocktakeNotification { NotificationCode = long.Parse(NotiID) });
        //List<View_StocktakeResult> items = Service.GetStocktakeResult(new View_StocktakeResult { NotificationID = notification.NotificationID });

        List<string> errorMsg = new List<string>();
        //List<View_StocktakeResult> itemList = new List<View_StocktakeResult>();

        bool hasError = false;
        List<View_StocktakeItem> list = new List<View_StocktakeItem>();
        for (int i = 0; i < dtAdjustment.Rows.Count; i++)
        {
            DataRow row = dtAdjustment.Rows[i];
            string partNo = row["PartNo"].ToString();
            string plantNo = row["PlantNo"].ToString();
            string duns = row["DUNS"].ToString();
            string notificationNo = row["NotificationNo"].ToString();
            string sloc = row["SLOCID"].ToString();

            StoreLocation location = this.StoreLocations.Find(s => string.Equals(s.LogisticsSysSLOC, sloc));
            if (location == null)
            {

                string msg = string.Format("第{0}行存储区域不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                List<View_StocktakeItem> items = Service.QueryStocktakeItem(
                    new View_StocktakeItem
                    {
                        PartCode = partNo,
                        PartPlantCode = plantNo,
                        DUNS = duns,
                        LogisticsSysSLOC = sloc,
                        NotificationCode=notificationNo
                    });

                if (items == null || items.Count == 0)
                {
                    string msg = string.Format("第{0}行零件不在盘点通知单中", i + 2);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
                else
                {
                    items[0].AvailableAdjust = (row["AvailableAdjust"] == DBNull.Value) ? 0 : Convert.ToInt32(row["AvailableAdjust"]);
                    items[0].QIAdjust = (row["QIAdjust"] == DBNull.Value) ? 0 : Convert.ToInt32(row["QIAdjust"]);
                    items[0].BlockAdjust = (row["BlockAdjust"] == DBNull.Value) ? 0 : Convert.ToInt32(row["BlockAdjust"]);
                    list.Add(items[0]);
                }
            }
        }
        if (!hasError)
        {
            Service.ImportAdjustment(list);
            BindDataControl(gvItems, dtAdjustment);
            //show information
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }


    protected void gvItems_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NotificationNo");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("PlantNo");
        dt.Columns.Add("DUNS");
        dt.Columns.Add("AdjustFlag");
        dt.Columns.Add("SLOCID");
        dt.Columns.Add("AvailableAdjust");
        dt.Columns.Add("QIAdjust");
        dt.Columns.Add("BlockAdjust");
        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvItems, dt);
    }
}
