using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SGM.ECount.DataModel;
using SGM.Common.Utility;
using SGM.Common.Cache;

public partial class SystemManagement_StoreLocationImport : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);
        if (!Page.IsPostBack)
        {
            UCFileUpload1.ValidationSchemaFile = Server.MapPath(@"~/ImportSchema/StorelocationImport.xml");
        }
    }

    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtStorelocaiton = ue.ContentTable;
        dtStorelocaiton.Columns["序号"].ColumnName = "No";
        dtStorelocaiton.Columns["存储区域名称"].ColumnName = "LocationName";
        dtStorelocaiton.Columns["区域类型"].ColumnName = "TypeID";
        dtStorelocaiton.Columns["Available"].ColumnName = "Available";
        dtStorelocaiton.Columns["QI"].ColumnName = "QI";
        dtStorelocaiton.Columns["Block"].ColumnName = "Block";
        dtStorelocaiton.Columns["物流系统存储区域"].ColumnName = "LogisticsSysSLOC";

        bool hasError = false;
        List<S_StoreLocation> recordList = new List<S_StoreLocation>();
        for (int i = 0; i < dtStorelocaiton.Rows.Count; i++)
        {
            S_StoreLocation storeloc = new S_StoreLocation();
            if (dtStorelocaiton.Rows[i]["LocationName"] != DBNull.Value)
            {
                storeloc.LocationName = dtStorelocaiton.Rows[i]["LocationName"].ToString();
            }

            if (dtStorelocaiton.Rows[i]["TypeID"] != DBNull.Value)
            {
                storeloc.TypeID = Convert.ToInt32(dtStorelocaiton.Rows[i]["TypeID"]);
            }

            if (dtStorelocaiton.Rows[i]["Available"] != DBNull.Value)
            {
                storeloc.AvailableIncluded = Convert.ToInt32(dtStorelocaiton.Rows[i]["Available"]);        
            }

            if (dtStorelocaiton.Rows[i]["QI"] != DBNull.Value)
            {
                storeloc.QIIncluded = Convert.ToInt32(dtStorelocaiton.Rows[i]["QI"]);
            }

            if (dtStorelocaiton.Rows[i]["Block"] != DBNull.Value)
            {
                storeloc.BlockIncluded = Convert.ToInt32(dtStorelocaiton.Rows[i]["Block"]);
            }

            if (dtStorelocaiton.Rows[i]["LogisticsSysSLOC"] != DBNull.Value)
            {
                storeloc.LogisticsSysSLOC = dtStorelocaiton.Rows[i]["LogisticsSysSLOC"].ToString();
                StoreLocation store = this.StoreLocations.SingleOrDefault(s => string.Equals(s.LogisticsSysSLOC, storeloc.LogisticsSysSLOC));
                CacheHelper.RemoveCache(Consts.CACHE_KEY_STORE_LOCATION);
                if (store != null)
                {
                    string msg = string.Format("第{0}行，该物流系统存储区域已存在", i + 2);
                    UCFileUpload1.AddErrorInfo(msg);
                    hasError = true;
                }
            }
            if (!recordList.Exists(r => r.LocationName == storeloc.LocationName && r.LogisticsSysSLOC == storeloc.LogisticsSysSLOC ))
            {
                recordList.Add(storeloc);
            }
            else
            {
                string msg = string.Format("存储区域名称{0}，物流系统存储区域{1}重复", storeloc.LocationName, storeloc.LogisticsSysSLOC);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
        }

        if (!hasError)
        {
            Service.ImportStoreLocation(recordList);
            BindDataControl(gvStorelocation, dtStorelocaiton);

            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
 
    }

    protected void gvStorelocation_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("No");
        dt.Columns.Add("LocationName");
        dt.Columns.Add("TypeID");
        dt.Columns.Add("Available");
        dt.Columns.Add("QI");
        dt.Columns.Add("Block");
        dt.Columns.Add("LogisticsSysSLOC");
        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvStorelocation, dt);
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("StoreLocationList.aspx");
        }
    }
}
