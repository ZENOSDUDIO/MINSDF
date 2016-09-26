using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class MasterDataMaintain_SegmentDetails : ECountBasePage
{
    public int? SegmentID
    {
        get
        {
            return ViewState["SegmentID"] as int?;
        }
        set
        {
            ViewState["SegmentID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindPlants(ddlPlant);

            if (Request.QueryString["SegmentID"] != null)
            {
                Segment segment = new Segment();
                segment.SegmentID = int.Parse(Request.QueryString["SegmentID"]);
                SegmentID = segment.SegmentID;
                segment = Service.GetSegmentbykey(segment);
                this.ddlPlant.SelectedValue = segment.Workshop.Plant.PlantID.ToString();

                BindWorkshops(this.ddlWorkshop, segment.Workshop.Plant);
                this.ddlWorkshop.SelectedValue = segment.Workshop.WorkshopID.ToString();
                this.txtSegmentCode.Text = segment.SegmentCode;
                this.txtSegmentName.Text = segment.SegmentName;
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
                Segment segment = new Segment();

                workshop.WorkshopID = int.Parse(ddlWorkshop.SelectedValue);
                segment.Workshop = workshop;
                plant.PlantID = int.Parse(ddlPlant.SelectedValue);
                segment.Workshop.Plant = plant;
                segment.SegmentCode = txtSegmentCode.Text;
                segment.SegmentName = txtSegmentName.Text;
                if (SegmentID != null)
                {
                    segment.SegmentID = SegmentID.Value;
                }

                bool IsExistSeg = true;

                List<Segment> list = new List<Segment>();
                list = Service.GetSegmentbyWorkshopID(workshop.WorkshopID);
                IsExistSeg = list.Exists(s => string.Compare(s.SegmentCode, segment.SegmentCode, true) == 0 && s.Available.Value && s.SegmentID != segment.SegmentID);

                if (IsExistSeg)
                {
                    Response.Write("<script>alert('该工段已经存在')</script>");
                    return;
                }

                if (SegmentID == null)
                {
                    //new segment
                    Service.AddSegment(segment);
                    SegmentID = segment.SegmentID;
                }
                else
                { //updatesegment
                    segment.SegmentID = SegmentID.Value;
                    Service.UpdateSegment(segment);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                break;

            default:
                break;
        }
    }


    //选择工厂
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlPlant.SelectedItem.ToString() != "--")
        {
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlant.SelectedValue);
            plant = Service.GetPlantByKey(plant);
            BindWorkshops(this.ddlWorkshop, plant);
        }
        else
        {
            ddlWorkshop.Items.Clear();
            ddlWorkshop.Items.Insert(0, "--");
        }

    }

}
