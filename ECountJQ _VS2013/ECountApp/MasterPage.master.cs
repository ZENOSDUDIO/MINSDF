using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Configuration;
using System.ServiceModel;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    //protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    //{
    //    if (e.Exception.Data["ExtraInfo"] != null)
    //    {
    //        ScriptManager.AsyncPostBackErrorMessage =
    //            e.Exception.Message +
    //            e.Exception.Data["ExtraInfo"].ToString();
    //    }
    //    else
    //    {
    //        ScriptManager.AsyncPostBackErrorMessage =
    //            "系统异常，请联系管理员或稍后再试";
    //    }
    //    if (!Page.ClientScript.IsStartupScriptRegistered("closewating"))
    //    {
    //        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "closewating", "closeWaitingModal();", true);
    //    }

    //}

    protected void treeViewMain_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        e.Node.SelectAction = TreeNodeSelectAction.Expand;
    }
    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Utils.Logout();
    }
}
