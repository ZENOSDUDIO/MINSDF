using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class ChangePwd : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        ECountMembershipProvider ECountMembership = new ECountMembershipProvider();
        if (ECountMembership.ChangePassword(User.Identity.Name, this.CurrentPassword.Text, this.NewPassword.Text))
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            Response.Write("<script>alert('密码修成成功，点击确定重新登录！');</script>");
            Response.Flush();
            Response.Write("<script>window.location.href='" + FormsAuthentication.LoginUrl + "';</script>");
        }
        else
            Response.Write("<script>alert('密码修改失败！');</script>");
    }
}
