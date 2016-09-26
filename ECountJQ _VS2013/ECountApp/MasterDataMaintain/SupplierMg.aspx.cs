using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class MasterDataMaintain_SupplierMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStoreLocation(ddlStoreLocation, true);
            if (Request.QueryString["supplierid"] != null)
            {
                bindBaseData(Request.QueryString["supplierid"].ToString());
            }
        }
    }

    private void bindBaseData(string supplierID)
    {
        Supplier model = Service.GetSupplierbykey(new Supplier { SupplierID = int.Parse(supplierID) });
        //bind supplier infomation
        this.txtDUNS.Text = model.DUNS;
        this.txtSupplierName.Text = model.SupplierName;
        this.hidSupplierID.Value = model.SupplierID.ToString();
        this.txtPhoneNumber1.Text = model.PhoneNumber1;
        this.txtPhoneNumber2.Text = model.PhoneNumber2;
        this.txtFax.Text = model.Fax;
        this.txtDescription.Text = model.Description;
        if (model.StoreLocation != null)
        {
            ddlStoreLocation.SelectedValue = model.StoreLocation.LocationID.ToString();
        }
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
                //Response.Write("<script>window.location.href='SupplierQuery.aspx';</script>");
                break;
            default:
                break;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //save new Supplier
        Supplier model = new Supplier();
        model.DUNS = this.txtDUNS.Text.Trim();
        model.SupplierName = this.txtSupplierName.Text.Trim();
        model.PhoneNumber1 = this.txtPhoneNumber1.Text.Trim();
        model.PhoneNumber2 = this.txtPhoneNumber2.Text.Trim();
        model.Fax = this.txtFax.Text.Trim();
        model.Description = this.txtDescription.Text.Trim();

        if (this.hidSupplierID.Value.Length > 0)
        {
            model.SupplierID = int.Parse(this.hidSupplierID.Value);
        }

        Supplier supplier = this.Suppliers.SingleOrDefault(s => string.Equals(s.SupplierName, model.SupplierName.ToString())&&  s.SupplierID != model.SupplierID);
        if (supplier != null)
        {
            Response.Write("<script>alert('该供应商名称已存在');</script>");
            return;
        }

        supplier = this.Suppliers.SingleOrDefault(s => string.Equals(s.DUNS, model.DUNS) && s.SupplierID != model.SupplierID);
        //Supplier temp = new Supplier();
        //    temp.DUNS = this.txtDUNS.Text.Trim();
        //    int count = Service.GetSuppliersCount(temp);
            if (supplier != null) //if (count > 0)
            {
                Response.Write("<script>alert('该供应商DUNS已存在');</script>");
                return;
            }

        if (!string.IsNullOrEmpty(ddlStoreLocation.SelectedValue))
        {
            model.StoreLocation = new StoreLocation { LocationID = int.Parse(ddlStoreLocation.SelectedValue) };
        }

        if (this.hidSupplierID.Value.Length > 0)
        {
            Service.UpdateSupplier(model);
        }
        else
        {
                model = Service.AddSupplier(model);
                this.hidSupplierID.Value = model.SupplierID.ToString();
        }
        CacheHelper.RemoveCache(Consts.CACHE_KEY_SUPPLIER);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

}
