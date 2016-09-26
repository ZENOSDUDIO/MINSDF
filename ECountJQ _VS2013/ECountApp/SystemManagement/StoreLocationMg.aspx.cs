using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class SystemManagement_StoreLocationMg : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPlants(ddlPlantID, true);
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
        if (model.StoreLocationType != null)
        {
            this.ddlTypeID.SelectedValue = model.StoreLocationType.TypeID.ToString();
        }
        if (model.Plant != null)
        {
            ddlPlantID.SelectedValue = model.Plant.PlantID.ToString();
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
                Page.RegisterStartupScript("Set", "<script>setReturnValue('ok');</script>");
                //Response.Write("<script>window.location.href='StoreLocationList.aspx';</script>");
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

        if (!string.IsNullOrEmpty(this.ddlTypeID.SelectedValue))
        {
            if (model.StoreLocationType == null)
                model.StoreLocationType = new StoreLocationType();
            model.StoreLocationType.TypeID = int.Parse(this.ddlTypeID.SelectedValue);
            model.StoreLocationType.TypeName = ddlTypeID.SelectedItem.Text;
        }
        model.AvailableIncluded = this.chkAvailableIncluded.Checked;
        model.QIIncluded = this.chkQIIncluded.Checked;
        model.BlockIncluded = this.chkBlockIncluded.Checked;

        if (!string.IsNullOrEmpty(ddlPlantID.SelectedValue))
        {
            model.Plant = new Plant
            {
                PlantID = int.Parse(ddlPlantID.SelectedValue),
                PlantCode = ddlPlantID.SelectedItem.Text
            };

            StoreLocation filter = new StoreLocation
            {
                Plant = new Plant { PlantID = model.Plant.PlantID },
                StoreLocationType = new StoreLocationType { TypeID = model.StoreLocationType.TypeID }
            };
            if (this.hidLocationID.Value.Length > 0)
            {
                filter.LocationID = int.Parse(hidLocationID.Value);
            }
            List<StoreLocation> result = Service.QueryStoreLocations(filter);
            if (result != null && result.Count > 0)
            {
                string msg = string.Format("alert('工厂【{0}】所对应【{1}】类型的存储区域已存在');",model.Plant.PlantCode,model.StoreLocationType.TypeName);
                if (filter.LocationID != DefaultValue.INT)//update
                {
                    foreach (var item in result)
                    {
                        if (item.LocationID != filter.LocationID)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LocExists", msg, true);
                            return;

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LocExists", msg, true);
                    return;
                }
            }
        }

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('该存储区域名称已存在');", true);
                return;
            }
            model.Available = true;//default value is true.
            model = Service.AddStoreLocation(model);
            this.hidLocationID.Value = model.LocationID.ToString();

        }
        CacheHelper.RemoveCache(Consts.CACHE_KEY_STORE_LOCATION);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }


}
