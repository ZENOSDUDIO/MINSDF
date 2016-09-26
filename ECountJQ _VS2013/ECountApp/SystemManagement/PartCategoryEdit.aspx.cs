using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_PartCategoryEdit :ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["categoryid"] != null)
            {
                bindBaseData(Request.QueryString["categoryid"].ToString());
            }
        }
    }

    private void bindBaseData(string categoryID)
    {
        PartCategory model = Service.GetPartCategoryByKey(new PartCategory { CategoryID = int.Parse(categoryID) });
        //bind PartCategory infomation
        this.hidCategoryID.Value = model.CategoryID.ToString();
        this.txtCategoryName.Text = model.CategoryName;
        this.txtDescription.Text = model.Description;
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);
                break;
            case "return":
                Response.Write("<script>window.location.href='PartCategoryList.aspx';</script>");
                break;
            default:
                break;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //save new PartCategory
        PartCategory model = new PartCategory();
        model.CategoryName = this.txtCategoryName.Text.Trim();
        model.Description = this.txtDescription.Text.Trim();
        if (this.hidCategoryID.Value.Length > 0)
        {
            model.CategoryID = int.Parse(this.hidCategoryID.Value);
            Service.UpdatePartCategory(model);
        }
        else
        {
            PartCategory temp = new PartCategory();
            temp.CategoryName = model.CategoryName;
            List<PartCategory> list = Service.QueryPartCategorys(temp);
            if (list != null && list.Count > 0)
            {
                RegisterStartupScript("Message", "<script>alert('该物料类别名称已存在');</script>");
            }
            else
            {
                model = Service.AddPartCategory(model);
                this.hidCategoryID.Value = model.CategoryID.ToString();
                //this.RegisterStartupScript("back", "<script>window.location.href='PartCategoryList.aspx';</script>");
            }
        }
    }
}
