using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using ECount.ExcelTransfer;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml.Xsl;

public partial class BizDataMaintain_ConsignmentPartRecordQuery : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
        if (!IsPostBack)
        {
            Filter = null;
            BindDropDownList(this.ddlPlantID, DropDownType.Plant);
            //bindGridView();
        }
    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        bindGridView();
    }

    void AspPager1_PageSizeChange(object sender, EventArgs e)
    {
        bindGridView();
    }

    public ConsignmentPartRecord Filter
    {
        get
        {
            if (Session["ConsignmentRecord_Filter"] == null)
            {
                Session["ConsignmentRecord_Filter"] = new ConsignmentPartRecord();
            }
            return Session["ConsignmentRecord_Filter"] as ConsignmentPartRecord;
        }
        set
        {
            Session["ConsignmentRecord_Filter"] = value;
        }
    }

    private void bindGridView()
    {
        ConsignmentPartRecord filter = new ConsignmentPartRecord();
        if (ddlPlantID.SelectedValue.Length > 0)
        {
            filter.Part = new Part();
            filter.Part.Plant = new Plant();
            filter.Part.Plant.PlantID = int.Parse(ddlPlantID.SelectedValue);
            filter.Part.Plant.PlantCode = ddlPlantID.SelectedItem.Text;
        }
        if (this.txtPartCode.Text.Trim().Length > 0)
        {
            if (filter.Part == null)
                filter.Part = new Part();
            filter.Part.PartCode = this.txtPartCode.Text.Trim();
        }
        if (this.txtDUNS.Text.Trim().Length > 0)
        {
            if (filter.Part == null)
                filter.Part = new Part();
            
            filter.Part.Supplier = new Supplier();
            filter.Part.Supplier.DUNS = this.txtDUNS.Text.Trim();
        }

        if (this.txtDUNS1.Text.Trim().Length > 0)
        {
            if (filter.Supplier == null)
                filter.Supplier = new Supplier();
            filter.Supplier.DUNS = this.txtDUNS1.Text.Trim();
        }
        if (this.txtSupplierName.Text.Trim().Length > 0)
        {
            if (filter.Supplier == null)
                filter.Supplier = new Supplier();
            filter.Supplier.SupplierName = this.txtSupplierName.Text.Trim();
        }

        int pageCount;
        int itemCount;
        List<ConsignmentPartRecord> cprs = Service.QueryConsignmentPartRecordsByPage(filter, this.AspPager1.PageSize, this.AspPager1.SelectPageNumber, out pageCount, out itemCount);
        Filter = filter;
        this.AspPager1.TotalPage = pageCount;
        this.AspPager1.TotalRecord = itemCount;
        this.GridView1.DataSource = cprs;
        this.GridView1.DataBind();
    }


    private void ExportConsignmentParts()
    {
        View_ConsignmentPart record = new View_ConsignmentPart();
        string errorMessage;

        if (Filter.Part != null)
        {
            if (!string.IsNullOrEmpty(Filter.Part.PartCode))//(!string.IsNullOrEmpty(this.txtPartCode.Text))
            {
                Part part = new Part();
                record.PartCode = Filter.Part.PartCode;//this.txtPartCode.Text.Trim();
            }

            if (Filter.Part.Plant != null)//(this.ddlPlantID.SelectedItem.ToString() != "--")
            {
                record.PlantCode = Filter.Part.Plant.PlantCode;//this.ddlPlantID.SelectedItem.ToString();
            }
            if (Filter.Part.Supplier != null && !string.IsNullOrEmpty(Filter.Part.Supplier.DUNS))
            {
                record.DUNS = Filter.Part.Supplier.DUNS;
            }
        }

        if (Filter.Supplier != null)//(!string.IsNullOrEmpty(txtDUNS.Text))
        {
            if (!string.IsNullOrEmpty(Filter.Supplier.DUNS))
            {
                record.CDUNS = Filter.Supplier.DUNS;//;this.txtDUNS.Text.ToString();
            }
            if (!string.IsNullOrEmpty(Filter.Supplier.SupplierName))
            {
                record.SupplierName = Filter.Supplier.SupplierName;
            }
        }




        byte[] buffer = Service.ExportConsignmentParts(record, out errorMessage);
        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=consignmentparts.csv");
            Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.Flush();
            Response.End();
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("ConsignmentPartRecordEdit.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;
            case "query":
                AspPager1.CurrentPage = 1;
                Query();
                break;

            case "export":
                ExportConsignmentParts();
                break;

            case "import":
                string url;
                url = string.Format("ConsignmentPartRecordImport.aspx");
                Response.Redirect(url);
                break;

            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("ConsignmentPartRecordEdit.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("ConsignmentPartRecordEdit.aspx?recordid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }

    protected void butDelete_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count==1 && GridView1.Rows[0].Visible==false)
            {
                return;
            }
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {

            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string recordID = GridView1.DataKeys[row.RowIndex]["RecordID"].ToString();
                ConsignmentPartRecord cpr = new ConsignmentPartRecord();
                cpr.RecordID = int.Parse(recordID);
                Service.DeleteConsignmentPartRecord(cpr);
            }
        }
        bindGridView();
    }
    protected void Query()
    {
        bindGridView();
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<ConsignmentPartRecord> cprs = new List<ConsignmentPartRecord> { new ConsignmentPartRecord() };
        this.BindEmptyGridView(this.GridView1, cprs);
    }
}
