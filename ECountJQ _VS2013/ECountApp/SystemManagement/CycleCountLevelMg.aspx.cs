using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_CycleCountLevelMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Levelid"] != null)
            {
                bindBaseData(Request.QueryString["levelid"].ToString());
            }
        }
    }

    private void bindBaseData(string levelID)
    {
        CycleCountLevel model = Service.GetCycleCountLevelByKey(new CycleCountLevel { LevelID = int.Parse(levelID) });
        //bind CycleCountLevel infomation
        this.hidLevelID.Value = model.LevelID.ToString();
        this.txtLevelName.Text = model.LevelName;
        this.txtMaxAmountDiffInPercent.Text = model.MaxAmountDiffInPercent.ToString();
        this.txtMaxSumDifference.Text = model.MaxSumDifference.ToString();
        this.txttimes.Text = model.times.ToString();
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);
                break;
            case "return":
                //Page.RegisterStartupScript("Set", "<script>setReturnValue('ok');</script>");
                //Response.Write("<script>window.location.href='CycleCountLevelList.aspx';</script>");
                break;
            default:
                break;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //save new CycleCountLevel
        CycleCountLevel model = new CycleCountLevel();
        model.LevelName = this.txtLevelName.Text.Trim();
        model.MaxAmountDiffInPercent = decimal.Parse(this.txtMaxAmountDiffInPercent.Text.Trim());
        model.MaxSumDifference = decimal.Parse(this.txtMaxSumDifference.Text.Trim());
        model.times = Int16.Parse(this.txttimes.Text.Trim());
        if (this.hidLevelID.Value.Length > 0)
        {
            model.LevelID = int.Parse(this.hidLevelID.Value);
            Service.UpdateCycleCountLevel(model);
        }
        else
        {
            CycleCountLevel temp = new CycleCountLevel();
            temp.LevelName = model.LevelName;
            List<CycleCountLevel> list = Service.QueryCycleCountLevels(temp);
            if (list != null && list.Count > 0)
            {
                RegisterStartupScript("Message", "<script>alert('该盘点级别名称已存在');</script>");
                return;
            }
            else
            {

                try
                {
                    model = Service.AddCycleCountLevel(model);
                    //RegisterStartupScript("Message", "<script>alert('数据保存成功');</script>");
                }
                catch
                {
                    RegisterStartupScript("Message", "<script>alert('数据保存失败');</script>");
                }
                this.hidLevelID.Value = model.LevelID.ToString();
                //this.RegisterStartupScript("back", "<script>window.location.href='CycleCountLevelList.aspx';</script>");
            }

     
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true); 
    }

}
