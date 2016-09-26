using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;

public partial class BizDataMaintain_SupplierSelect : ECountBasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);

        if (!IsPostBack)
        {
            bindGridView();
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
            List<Supplier> ms = pagebase.Service.QuerySuppliersByPage(model, this.AspPager1.PageSize, this.AspPager1.SelectPageNumber, out pageCount, out itemCount);
            this.AspPager1.TotalPage = pageCount;
            this.AspPager1.TotalRecord = itemCount;
            this.GridView1.DataSource = ms;
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
        //    lb.oncl += new EventHandler(ButQuery_Click);
        //    if (lb != null)
        //        lb.CommandArgument = e.Row.RowIndex.ToString();
        //}
    }

    //Query Supplier 
    protected void ButQuery_Click(object sender, EventArgs e)
    {
        bindGridView();
    }

    //protected void lnkSelect_Click(object sender, EventArgs e)
    //{
    //    string str;
    //    int rowIndex = 0;
    //    Supplier supp = new Supplier();
    //    rowIndex = int.Parse((sender as LinkButton).CommandArgument);
    //    supp.SupplierID = int.Parse(this.GridView1.DataKeys[rowIndex]["SupplierID"].ToString());
    //    supp = Service.GetSupplierbykey(supp);
    //    str = supp.SupplierID + "∑" + supp.DUNS + "∑" + supp.PhoneNumber1 + "∑" + supp.Fax;
    
    //    Page.RegisterStartupScript("Set", "<script>setReturnValue('" + str + "');</script>");
    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="Select")
        {
            string str;
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = ((Control)sender).NamingContainer as GridViewRow;
            Supplier supp = new Supplier();
            supp.SupplierID = int.Parse(this.GridView1.DataKeys[rowIndex]["SupplierID"].ToString());
            supp = Service.GetSupplierbykey(supp);
            str = supp.SupplierID + "∑" + supp.DUNS + "∑" + supp.PhoneNumber1 + "∑" + supp.Fax;

            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "Set", "setReturnValue('" + str + "');", true); 
        }
    }
}