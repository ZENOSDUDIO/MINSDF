using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_PartStatusMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["statusid"] != null)
            {
                bindBaseData(Request.QueryString["statusid"].ToString());
            }
        }
    }

    private void bindBaseData(string statusID)
    {
        PartStatus model = Service.GetPartStatusByKey(new PartStatus { StatusID = int.Parse(statusID) });
        //bind PartStatus infomation
        this.hidStatusID.Value = model.StatusID.ToString();
        this.txtStatusName.Text = model.StatusName;
        this.chkCycleCount.Checked = (bool)model.CycleCount;
        this.chkAvailable.Checked = (bool)model.Available;
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);

                break;
            case "return":
                Page.RegisterStartupScript("Set", "<script>setReturnValue('ok');</script>");
                //Response.Write("<script>window.location.href='PartStatusList.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //save new PartStatus
        PartStatus model = new PartStatus();
        model.StatusName = this.txtStatusName.Text.Trim();
        model.CycleCount = this.chkCycleCount.Checked;
        model.Available = this.chkAvailable.Checked;
        if (this.hidStatusID.Value.Length > 0)
        {
            model.StatusID = int.Parse(this.hidStatusID.Value);
            Service.UpdatePartStatus(model);
        }
        else
        {
            PartStatus temp = new PartStatus();
            temp.StatusName = model.StatusName;
            if (Service.ExistPartStatus(temp))
            {
                RegisterStartupScript("Message", "<script>alert('该物料状态名称已存在');</script>");
                return;
            }
            else
            {
                model.Available = true;
                model = Service.AddPartStatus(model);
                this.hidStatusID.Value = model.StatusID.ToString();
                //this.RegisterStartupScript("back", "<script>window.location.href='PartStatusList.aspx';</script>");
            }

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

}
