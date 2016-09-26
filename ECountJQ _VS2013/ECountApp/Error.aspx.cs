using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Request.QueryString["ErrorMsg"] != null)
            {

            }
            else
            {
                //Exception ex = Server.GetLastError();

            Server.ClearError();
            }
        }
    }
}
