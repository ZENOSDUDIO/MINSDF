using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_StocktakeTypeMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindDropDownList(this.ddlDefaultPriority, DropDownType.StocktakePriority);
            if (Request.QueryString["typeid"] != null)
            {
                bindBaseData(Request.QueryString["typeid"].ToString());
            }
        }
    }

    private void bindBaseData(string typeID)
    {
        StocktakeType model = Service.GetStocktakeTypeByKey(new StocktakeType { TypeID = int.Parse(typeID) });
        this.hidTypeID.Value = model.TypeID.ToString();
        this.txtTypeName.Text = model.TypeName;
        this.txtLogisticCode.Text = model.LogisticCode;
        this.txtDefaultPriority.Text = model.DefaultPriority.ToString();
        //if (model.DefaultPriority != null)
        //{
        //    this.ddlDefaultPriority.SelectedValue = model.DefaultPriority.ToString();
        //}
        this.chkManualEnabled.Checked = (bool)model.ManualEnabled;
        this.chkActAsCycleCount.Checked = (bool)model.ActAsCycleCount;
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
                Page.RegisterStartupScript("Set", "<script>setReturnValue('ok');</script>");
                //Response.Write("<script>window.location.href='StocktakeTypeList.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StocktakeType model = new StocktakeType();
        model.TypeName = this.txtTypeName.Text.Trim();
        model.LogisticCode = this.txtLogisticCode.Text.Trim();
        model.DefaultPriority = int.Parse(this.txtDefaultPriority.Text);
        //if (!string.IsNullOrEmpty(this.ddlDefaultPriority.SelectedValue))
        //{
        //    model.DefaultPriority = int.Parse(this.ddlDefaultPriority.SelectedValue);
        //}
        model.ManualEnabled = this.chkManualEnabled.Checked;
        model.ActAsCycleCount = this.chkActAsCycleCount.Checked;
        model.Description = this.txtDescription.Text.Trim();

        if (this.hidTypeID.Value.Length > 0)
        {
            model.TypeID = int.Parse(this.hidTypeID.Value);
            Service.UpdateStocktakeType(model);
        }
        else
        {
            StocktakeType temp = new StocktakeType();
            temp.TypeName = model.TypeName;
            if (Service.ExistStocktakeType(temp))
            {
                RegisterStartupScript("Message", "<script>alert('该盘点类别名称已存在');</script>");
                return;
            }
            else
            {
                model.Available = true;
                model = Service.AddStocktakeType(model);
                this.hidTypeID.Value = model.TypeID.ToString();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

}
