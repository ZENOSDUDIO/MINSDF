using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCS.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Data;

public partial class BizDataMaintain_PartRepairRecordImport : ECountBasePage
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
        DataTable dtPartRepair = ue.ContentTable;
        dtPartRepair.Columns["序号"].ColumnName = "RowNumber";
        dtPartRepair.Columns["工厂"].ColumnName = "PlantCode";
        dtPartRepair.Columns["返修零件号"].ColumnName = "PartCode";
        dtPartRepair.Columns["供应商DUNS"].ColumnName = "DUNS";
        dtPartRepair.Columns["返修供应商DUNS"].ColumnName = "RepairDUNS";
        dtPartRepair.Columns["返修供应商名称"].ColumnName = "RepairSupplierName";
        dtPartRepair.Columns["电话"].ColumnName = "Telephone";
        dtPartRepair.Columns["传真"].ColumnName = "Fax";

        bool hasError = false;
        List<View_PartRepairRecord> partlist = new List<View_PartRepairRecord>();
        for (int i = 0; i < dtPartRepair.Rows.Count; i++)
        {
            View_PartRepairRecord partRepairRecord = new View_PartRepairRecord();

            Supplier supplier = this.Suppliers.SingleOrDefault(s => string.Equals(s.DUNS, dtPartRepair.Rows[i]["RepairDUNS"].ToString().Trim()));
            if (supplier == null)
            {
                string msg = string.Format("第{0}行，该返修供应商DUNS不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                partRepairRecord.RepairSupplierID = supplier.SupplierID;
            }

            Part part = new Part();
            List<ViewPart> list = new List<ViewPart>();
            part.PartCode = dtPartRepair.Rows[i]["PartCode"].ToString();
            part.Plant = new Plant ();
            part.Plant.PlantCode = dtPartRepair.Rows[i]["PlantCode"] + "";
            part.Supplier = new Supplier();
            part.Supplier.DUNS = dtPartRepair.Rows[i]["DUNS"] + "";

            list = this.Service.QueryParts(part);
            if (list.Count <= 0)
            {
                string msg = string.Format("第{0}行，该零件不存在", i + 2);
                UCFileUpload1.AddErrorInfo(msg);
                hasError = true;
            }
            else
            {
                partRepairRecord.PartID = list[0].PartID;

                partRepairRecord.PartCode = dtPartRepair.Rows[i]["PartCode"].ToString();
                partRepairRecord.DUNS = dtPartRepair.Rows[i]["DUNS"].ToString();
                partRepairRecord.PlantID = list[0].PlantID;
                partlist.Add(partRepairRecord);
            }
        }
        if (!hasError)
        {
            Service.ImportPartRepairRecord(partlist);
            BindDataControl(gvPartRepairRecord, dtPartRepair);
            this.UCFileUpload1.AddSuccessInfo("上传文件成功", string.Empty, string.Empty);
        }
    }

    protected void gvPartRepairRecord_PreRender(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("PlantCode");
        dt.Columns.Add("PartCode");
        dt.Columns.Add("DUNS");
        dt.Columns.Add("RepairDUNS");
        dt.Columns.Add("RepairSupplierName");
        dt.Columns.Add("Telephone");
        dt.Columns.Add("Fax");

        dt.Rows.Add(dt.NewRow());
        BindEmptyGridView(gvPartRepairRecord, dt);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        if (e.CommandName == "return")
        {
            Response.Redirect("PartRepairRecordQuery.aspx");
        }
    }
}
