using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class SystemManagement_UserGroupList : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //foreach (ToolbarButton tb in this.Toolbar1.Items)
            //{
            //    if (tb.CommandName == "delete")
            //    {
            //        tb.Visible = tb.Enabled = false;
            //    }
            //}

            bindGridView();
        }
    }

    private void bindGridView()
    {
        UserGroup filter = new UserGroup();
        List<UserGroup> objs = Service.GetUserGroups();
        this.GridView1.DataSource = objs;
        this.GridView1.DataBind();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("UserGroupManagement.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                CacheHelper.RemoveCache(Consts.CACHE_KEY_USER_GROUPS);
                break;
            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("UserGroupManagement.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("UserGroupManagement.aspx?groupid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }

    protected void butDelete_Click(object sender, EventArgs e)
    {
        List<string> ids = new List<string>();
        for (int i = 0; i <= this.GridView1.Rows.Count - 1; i++)
        {
            GridViewRow row = GridView1.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string sid = GridView1.DataKeys[row.RowIndex]["GroupID"].ToString();
                ids.Add(sid);
            }
        }
        if (ids.Count > 0)
        {
            Service.DeleteUserGroups(ids);
            bindGridView();
        }
        else
        {
            RegisterStartupScript("Message", "<script>alert('请标记删除.');</script>");
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<UserGroup> objs = new List<UserGroup> { new UserGroup() };
        this.BindEmptyGridView(this.GridView1, objs);
    }
}
