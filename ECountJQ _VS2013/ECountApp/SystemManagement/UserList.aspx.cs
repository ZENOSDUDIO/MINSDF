using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class SystemManagement_UserList : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserGroup(this.ddlUserGroup);
            bindGridView();
        }
    }

    private void bindGridView()
    {
        List<User> objs = Service.GetUsers();
        this.gvUser.DataSource = objs;
        this.gvUser.DataBind();
    }

    protected void QueryUsers()
    {
        User user = new User();
        List<User> list = new List<User>();

        if (!string.IsNullOrEmpty(this.txtUserName.Text))
        {
            user.UserName = this.txtUserName.Text.Trim();
        }

        if ( !string.IsNullOrEmpty(this.ddlUserGroup.SelectedValue))
        {
            UserGroup usergroup = new UserGroup();
            user.UserGroup = usergroup;
            user.UserGroup.GroupID = int.Parse(this.ddlUserGroup.SelectedValue.ToString());
        }
        list = Service.QueryUsersByPage(user);
        gvUser.DataSource = list;
        gvUser.DataBind();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "add":
                Response.Redirect("UserManagement.aspx");
                break;
            case "delete":
                butDelete_Click(null, null);
                break;

            case "search":
                QueryUsers();
                break;

            default:
                break;
        }
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("UserManagement.aspx"));
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        //string opName = linkButton.CommandName;
        string url;
        url = string.Format("UserManagement.aspx?userid={0}", linkButton.CommandArgument);
        Response.Redirect(url);
    }

    protected void butDelete_Click(object sender, EventArgs e)
    {
        List<string> ids = new List<string>();
        for (int i = 0; i <= this.gvUser.Rows.Count - 1; i++)
        {
            GridViewRow row = gvUser.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                string sid = gvUser.DataKeys[row.RowIndex]["UserID"].ToString();
                ids.Add(sid);
            }
        }
        if (ids.Count > 0)
        {
            Service.DeleteUsers(ids);
            bindGridView();
        }
        else
        {
            RegisterStartupScript("Message", "<script>alert('请标记删除.');</script>");
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        List<User> objs = new List<User> { new User() };
        this.BindEmptyGridView(this.gvUser, objs);
    }
}