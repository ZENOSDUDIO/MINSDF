using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECountJQ.UPG.BLL.DBBase;
using SGM.ECountJQ.UPG.BLL;

namespace SGM.ECountJQ.UPG.Web.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            Pager pg = gvpUser.Pager;
            gvUser.DataSource = UserTest.FindAllByPage(pg, "");
            gvUser.DataBind();
            gvpUser.Pager = pg;
        }

        protected void gvpUser_OnPageIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}