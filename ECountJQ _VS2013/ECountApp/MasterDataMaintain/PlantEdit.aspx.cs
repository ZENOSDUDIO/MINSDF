using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class MasterDataMaintain_PlantEdit : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PlantID"] != null)
            {
                bind(Request.QueryString["PlantID"].ToString());
            }
        }
    }

    protected void bind(string plantid)
    {

        Plant plant = new Plant();
        plant.PlantID = int.Parse(plantid);
        plant = Service.GetPlantByKey(plant);
        txtPlantCode.Text = plant.PlantCode;
        txtPlantName.Text = plant.PlantName;
    }


    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Plant plant = new Plant();
                plant.PlantID = int.Parse(Request.QueryString["PlantID"].ToString());
                plant.PlantCode = txtPlantCode.Text.Trim();
                plant.PlantName = txtPlantName.Text.Trim();
                Service.UpdatePlant(plant);
                string url = "PlantQuery.aspx";
                Response.Redirect(url);
                break;
            case "return":
                break;
            default:
                break;
        }

    }
}
