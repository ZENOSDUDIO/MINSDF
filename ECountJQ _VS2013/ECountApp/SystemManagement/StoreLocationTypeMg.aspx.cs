using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_StoreLocationTypeMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["typeid"] != null)
            {
                bindBaseData(Request.QueryString["typeid"].ToString());
            }
        }
    }

    private void bindBaseData(string typeID)
    {
        StoreLocationType model = Service.GetStoreLocationTypeByKey(new StoreLocationType { TypeID = int.Parse(typeID) });
        this.hidTypeID.Value = model.TypeID.ToString();
        this.txtTypeName.Text = model.TypeName;
        cbRequired.Checked = model.RequiredToCount.Value;
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
                //Response.Write("<script>window.location.href='StoreLocationTypeList.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StoreLocationType model = new StoreLocationType();
        model.TypeName = this.txtTypeName.Text.Trim();
        model.RequiredToCount = cbRequired.Checked;
        if (this.hidTypeID.Value.Length > 0)
        {
            model.TypeID = int.Parse(this.hidTypeID.Value);
            Service.UpdateStoreLocationType(model);
        }
        else
        {
            StoreLocationType temp = new StoreLocationType();
            temp.TypeName = model.TypeName;
            if (Service.ExistStoreLocationType(temp))
            {
                RegisterStartupScript("Message", "<script>alert('该区域类型名称已存在');</script>");
                return;
            }
            else
            {
                model = Service.AddStoreLocationType(model);
                this.hidTypeID.Value = model.TypeID.ToString();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

}
