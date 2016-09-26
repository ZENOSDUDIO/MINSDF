using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class SystemManagement_PartCategoryList : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGridView();
        }
    }
    protected void btnTemp_Click(object sender, EventArgs e)
    {
        this.bindGridView();
    }

    private void bindGridView()
    {
        PartCategory filter = new PartCategory();

        List<PartCategory> objs = Service.QueryPartCategorys(filter);
        this.GridView1.DataSource = objs;
        this.GridView1.DataBind();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("PartCategoryEdit.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;
            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("PartCategoryEdit.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("PartCategoryEdit.aspx?categoryid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("onclick", "javascript:return showdata('" + GridView1.DataKeys[e.Row.RowIndex]["CategoryID"].ToString() + "');");
        }
    }

    protected void butDelete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string sid = GridView1.DataKeys[row.RowIndex]["CategoryID"].ToString();
                PartCategory obj = new PartCategory();
                obj.CategoryID = int.Parse(sid);
                Service.DeletePartCategory(obj);
            }
        }
        bindGridView();
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<PartCategory> objs = new List<PartCategory> { new PartCategory() };
        this.BindEmptyGridView(this.GridView1, objs);
    }
}
