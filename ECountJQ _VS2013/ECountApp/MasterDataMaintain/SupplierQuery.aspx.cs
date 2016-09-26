using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using System.Text;

public partial class BizDataMaintain_SupplierQuery : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "供应商管理";
        this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);

        if (!IsPostBack)
        {
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

    protected void btnTemp_Click(object sender, EventArgs e)
    {
        this.bindGridView();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "javascript:return showdata('" + GridView1.DataKeys[e.Row.RowIndex]["SupplierID"].ToString() + "');");
        }
    }

    /// <summary>
    /// bind data to GridView
    /// </summary>
    /// <param name="part"></param>
    private void bindGridView()
    {
        Supplier model = new Supplier();
        if (!string.IsNullOrEmpty(this.txtDUNS.Text))
        {
            model.DUNS = this.txtDUNS.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtSupplierName.Text))
        {
            model.SupplierName = this.txtSupplierName.Text.Trim();
        }
        if (this.Page != null)
        {
            int pageCount;
            int itemCount;
            ECountBasePage pagebase = this.Page as ECountBasePage;
            List<Supplier> ms = pagebase.Service.QuerySuppliersByPage(model, AspPager1.PageSize, AspPager1.SelectPageNumber, out pageCount, out itemCount);
            this.AspPager1.TotalPage = pageCount;
            this.AspPager1.TotalRecord = itemCount;
            this.GridView1.DataSource = ms;
            this.GridView1.DataBind();
        }
    }

    //Query Supplier 
    protected void butQuery_Click(object sender, EventArgs e)
    {       
        bindGridView();
    }



    private void ExportSupplier()
    {
        Supplier record = new Supplier();
        string errorMessage;

        if (this.txtDUNS.Text.Trim().Length > 0)
        {
            record.DUNS = this.txtDUNS.Text.Trim();
        }
        if (this.txtSupplierName.Text.Trim().Length > 0)
        {
            record.SupplierName = this.txtSupplierName.Text.Trim();
        }

        byte[] buffer = Service.ExportSupplier(record, out errorMessage);
        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=supplier.csv");
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
            case "delete":
                butDelete_Click(null, null);
                break;
            case "query":
                AspPager1.CurrentPage = 1;
                butQuery_Click(null, null);
                break;

            case "export":
                ExportSupplier();
                break;

            default:
                break;
        }
    }


    protected void butDelete_Click(object sender, EventArgs e)
    {
        List<int> list = new List<int>();
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string supplierID = GridView1.DataKeys[row.RowIndex]["SupplierID"].ToString();
                //string duns = GridView1.DataKeys[row.RowIndex]["DUNS"].ToString();
                //Supplier sp = new Supplier();
                //sp.SupplierID = int.Parse(supplierID);
                list.Add(int.Parse(supplierID));
            }
        }
        if (list.Count > 0)
        {
            Service.DeleteSuppliers(list);
        }
        bindGridView();
    }


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<Supplier> objs = new List<Supplier> { new Supplier() };
        this.BindEmptyGridView(this.GridView1, objs);
    }
}
