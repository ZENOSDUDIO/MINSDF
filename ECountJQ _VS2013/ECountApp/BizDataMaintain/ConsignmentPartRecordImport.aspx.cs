using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Data;
using SCS.Web.UI.WebControls;

public partial class BizDataMaintain_ConsignmentPartRecordImport : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UCFileUpload1.OnUpload += new EventHandler(UCFileUpload1_OnUpload);

        if (!Page.IsPostBack)
        {
        }

    }

    void UCFileUpload1_OnUpload(object sender, EventArgs e)
    {
        UploadEventArgs ue = e as UploadEventArgs;
        DataTable dtPartConsignment = ue.ContentTable;
        dtPartConsignment.Columns["序号"].ColumnName = "RowNumber";
        dtPartConsignment.Columns["工厂"].ColumnName = "PlantCode";
        dtPartConsignment.Columns["外协零件号"].ColumnName = "PartCode";
        dtPartConsignment.Columns["供应商DUNS"].ColumnName = "DUNS";
        dtPartConsignment.Columns["外协供应商DUNS"].ColumnName = "CDUNS";
        dtPartConsignment.Columns["外协供应商名称"].ColumnName = "SupplierName";
        dtPartConsignment.Columns["电话"].ColumnName = "Telephone";
        dtPartConsignment.Columns["传真"].ColumnName = "Fax";

        bool hasError = false;
        List<View_ConsignmentPart> partlist = new List<View_ConsignmentPart>();
        for (int i = 0; i < dtPartConsignment.Rows.Count; i++)
        {
            View_ConsignmentPart partCRecord = new View_ConsignmentPart();

            Supplier supplier = this.Suppliers.SingleOrDefault(s => string.Equals(s.DUNS, dtPartConsignment.Rows[i]["CDUNS"].ToString().Trim()));
            if (supplier == null)
            {
                string msg = string.Format("第{0}行，该外协供应商DUNS不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                partCRecord.ConsignmentSupplier = supplier.SupplierID;
            }

            Part part = new Part();
            List<ViewPart> list = new List<ViewPart>();
            part.PartCode = dtPartConsignment.Rows[i]["PartCode"].ToString();
            part.Plant = new Plant();
            part.Plant.PlantCode = dtPartConsignment.Rows[i]["PlantCode"] + "";
            part.Supplier = new Supplier();
            part.Supplier.DUNS = dtPartConsignment.Rows[i]["DUNS"] + "";

            list = this.Service.QueryParts(part);
            if (list.Count <= 0)
            {
                string msg = string.Format("第{0}行，该零件不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                partCRecord.PartID = list[0].PartID;
                partCRecord.PartCode = dtPartConsignment.Rows[i]["PartCode"].ToString();
                partCRecord.DUNS = dtPartConsignment.Rows[i]["DUNS"].ToString();

                partlist.Add(partCRecord);
            }
        }
        if (!hasError)
        {
            Service.ImportConsignmentRecord(partlist);
            BindDataControl(gvConsignmentPartRecord, dtPartConsignment);
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }


    protected void gvConsignmentPartRecord_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("PlantCode");
        dt.Columns.Add("PartCode");
        dt.Columns.Add("DUNS");
        dt.Columns.Add("CDUNS");
        dt.Columns.Add("SupplierName");
        dt.Columns.Add("Telephone");
        dt.Columns.Add("Fax");

        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvConsignmentPartRecord, dt);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("ConsignmentPartRecordQuery.aspx");
        }
    }
}
