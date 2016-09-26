using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using SGM.Common.Cache;
using SGM.Common.Utility;


public partial class MasterDataMaintain_WorkshopDetails : ECountBasePage
{
    public int? WorkshopID
    {
        get
        {
            return ViewState["WorkshopID"] as int?;
        }
        set
        {
            ViewState["WorkshopID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDownList(this.ddlPlant, DropDownType.Plant);

            if (Request.QueryString["WorkshopID"] != null)
            {
                Workshop workshop = new Workshop();
                workshop.WorkshopID = int.Parse(Request.QueryString["WorkshopID"]);
                WorkshopID = workshop.WorkshopID;
                workshop = Service.GetWorkshopbykey(workshop);

                this.txtWorkshopCode.Text = workshop.WorkshopCode;
                this.txtWorkshopName.Text = workshop.WorshopName;
                this.ddlPlant.SelectedValue = workshop.Plant.PlantID.ToString();
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Plant plant = new Plant();
                Workshop workshop = new Workshop();

                plant.PlantID = int.Parse(this.ddlPlant.SelectedValue.Trim());
                workshop.Plant = plant;
                workshop.WorkshopCode = txtWorkshopCode.Text;
                workshop.WorshopName = txtWorkshopName.Text;
                if (WorkshopID!= null)
                {
                    workshop.WorkshopID = WorkshopID.Value; 
                }

                bool IsWorkshopExist = true;
                List<Workshop> workshoplist = new List<Workshop>();
                workshoplist = Service.GetWorkshopbyPlantID(plant.PlantID);
                IsWorkshopExist = workshoplist.Exists(w => string.Compare(w.WorkshopCode, workshop.WorkshopCode, true) == 0 && w.Available.Value&&w.WorkshopID != workshop.WorkshopID);

                if (IsWorkshopExist)
                {
                    Response.Write("<script>alert('该车间已存在');</script>");
                    return;
                }

                if (null == WorkshopID)
                {
                    /*NEW WORKSHOP*/
                    Service.AddWorkshop(workshop);
                    WorkshopID = workshop.WorkshopID;
                }
                else
                {
                    workshop.WorkshopID = WorkshopID.Value;
                    Service.UpdateWorkshop(workshop);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                break;

            default:
                break;
        }
    }
    protected void txtWorkshopName_TextChanged(object sender, EventArgs e)
    {

    }
}
