using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_StoreLocationEdit : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList(this.ddlPlantID, DropDownType.Plant);
            BindDropDownList(this.ddlTypeID, DropDownType.StoreLocationType);
            if (Request.QueryString["locationid"] != null)
            {
                bindBaseData(Request.QueryString["locationid"].ToString());
            }
        }
    }

    private void bindBaseData(string locationID)
    {
        StoreLocation model = Service.GetStoreLocationByKey(new StoreLocation { LocationID = int.Parse(locationID) });
        this.hidLocationID.Value = model.LocationID.ToString();
        this.txtLocationName.Text = model.LocationName;
        this.txtLogisticsSysSLOC.Text = model.LogisticsSysSLOC;
        if (model.Plant != null)
        {
            this.ddlPlantID.SelectedValue = model.Plant.PlantID.ToString();
        }
        if (model.StoreLocationType != null)
        {
            this.ddlTypeID.SelectedValue = model.StoreLocationType.TypeID.ToString();
        }
        this.chkAvailableIncluded.Checked = (bool)model.AvailableIncluded;
        this.chkQIIncluded.Checked = (bool)model.QIIncluded;
        this.chkBlockIncluded.Checked = (bool)model.BlockIncluded;
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);
                break;
            case "return":
                Response.Write("<script>window.location.href='StoreLocationList.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        StoreLocation model = new StoreLocation();
        model.LocationName = this.txtLocationName.Text.Trim();
        model.LogisticsSysSLOC = this.txtLogisticsSysSLOC.Text.Trim();
        if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
        {
            if (model.Plant == null)
                model.Plant = new Plant();
            model.Plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.ddlTypeID.SelectedValue))
        {
            if (model.StoreLocationType == null)
                model.StoreLocationType = new StoreLocationType();
            model.StoreLocationType.TypeID = int.Parse(this.ddlTypeID.SelectedValue);
        }
        model.AvailableIncluded = this.chkAvailableIncluded.Checked;
        model.QIIncluded = this.chkQIIncluded.Checked;
        model.BlockIncluded = this.chkBlockIncluded.Checked;



        if (this.hidLocationID.Value.Length > 0)
        {
            model.LocationID = int.Parse(this.hidLocationID.Value);
            Service.UpdateStoreLocation(model);
        }
        else
        {
            StoreLocation temp = new StoreLocation();
            temp.LocationName = model.LocationName;
            if (Service.ExistStoreLocation(temp))
            {
                RegisterStartupScript("Message", "<script>alert('该存储区域名称已存在');</script>");
                return;
            }
            else
            {
                model.Available = true;//default value is true.
                model = Service.AddStoreLocation(model);
                this.hidLocationID.Value = model.LocationID.ToString();
            }
        }
    }

}
