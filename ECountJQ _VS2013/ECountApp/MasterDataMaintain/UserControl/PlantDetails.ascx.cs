using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class MasterDataMaintain_UserControl_PlantDetails : ECountBaseUserControl
{
    public int? PlantID
    {
        get
        {
            return ViewState["PlantID"] as int?;
        }
        set
        {
            ViewState["PlantID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PlantID"] != null)
            {
                PlantID = int.Parse(Request.QueryString["PlantID"]);
                bind(PlantID.Value);
            }
            if (Mode == PageMode.View)
            {
                Toolbar1.Visible = false;
            }
        }
    }
    protected void bind(int plantid)
    {

        Plant plant = new Plant();
        plant.PlantID = plantid;
        plant = Container.Service.GetPlantByKey(plant);
        txtPlantCode.Text = plant.PlantCode;
        txtPlantName.Text = plant.PlantName;
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Plant plant = new Plant();
                bool IsPlantExist = true;
                plant.PlantCode = txtPlantCode.Text.Trim();
                plant.PlantName = txtPlantName.Text.Trim();
                if (PlantID != null)
                {
                    plant.PlantID = PlantID.Value;
                }

                IsPlantExist = this.Plants.Exists(p => string.Compare(p.PlantCode, plant.PlantCode, true) == 0 && p.Available.Value == true && p.PlantID != plant.PlantID);
                if (IsPlantExist)
                {
                    Response.Write("<script>alert('该工厂已存在');</script>");
                    return;
                }

                if (PlantID != null)
                {
                    plant.PlantID = PlantID.Value;
                    Container.Service.UpdatePlant(plant);
                }
                else
                { /*NEW PLANT*/
                    plant = Container.Service.AddPlant(plant);
                    PlantID = plant.PlantID;
                }
                CacheHelper.RemoveCache(Consts.CACHE_KEY_PLANT);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                break;
            default:
                break;
        }

    }
}
