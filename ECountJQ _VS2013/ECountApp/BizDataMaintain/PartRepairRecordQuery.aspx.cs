using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class BizDataMaintain_PartRepairRecordQuery :ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
        if (!IsPostBack)
        {
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
        AspPager1.CurrentPage = 1;
        bindGridView();
    }

    private void bindGridView()
    {
        PartRepairRecord filter = new PartRepairRecord();
        if (ddlPlantID.SelectedValue.Length > 0)
        {
            filter.Part = new Part();
            filter.Part.Plant = new Plant();
            filter.Part.Plant.PlantID = int.Parse(ddlPlantID.SelectedValue);
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
            filter.Part.PartCode = this.txtPartCode.Text.Trim();
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
        List<PartRepairRecord> prrs = Service.QueryPartRepairRecordsByPage(filter, this.AspPager1.PageSize, this.AspPager1.SelectPageNumber, out pageCount, out itemCount);

        this.AspPager1.TotalPage = pageCount;
        this.AspPager1.TotalRecord = itemCount;
        this.GridView1.DataSource = prrs;
        this.GridView1.DataBind();
    }

   
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("PartRepairRecordEdit.aspx?recordid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("PartRepairRecordEdit.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;
            case "query":
                butQuery_Click(null, null);
                break;
            case "import":
                string url;
                url = string.Format("PartRepairRecordImport.aspx");
                Response.Redirect(url);
                break;
            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("PartRepairRecordEdit.aspx"));
    }

    //delete PartRepairRecord object by primary key RecordID
    protected void butDelete_Click(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count == 1 && GridView1.Rows[0].Visible == false)
        {
            return;
        }
        List<string> guids = new List<string>();
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            if (GridView1.DataKeys[i].Value==null)
            {
                continue;
            }
            GridViewRow row = GridView1.Rows[i];

            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string recordID = GridView1.DataKeys[row.RowIndex]["RecordID"].ToString();
                guids.Add(recordID);
                //PartRepairRecord prr = new PartRepairRecord();
                //prr.RecordID = new Guid(recordID);
                //Service.DeletePartRepairRecord(prr);
            }
        }
        Service.DeletePartRepairRecords(guids);
        bindGridView();
    }

    protected void butQuery_Click(object sender, EventArgs e)
    {
        bindGridView();
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<PartRepairRecord> prrs = new List<PartRepairRecord> { new PartRepairRecord() };
        this.BindEmptyGridView(this.GridView1, prrs);
        //foreach (GridViewRow gr in this.GridView1.Rows)
        //{
        //    //gr.Height = 10;
        //    gr.Attributes["Height"] = "100px";
        //}
    }

    //定义GridView行高，让宽度根据内容自动拉长
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            e.Row.Height = Unit.Pixel(10);
        }
    }
}
