using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class MasterDataMaintain_WorkshopsList : ECountBasePage
{
    public int PlantID
    {
        get
        {
            return int.Parse(ViewState["PlantID"].ToString());
        }
        set
        {
            ViewState["PlantID"] = value;
        }
    }

    public List<string> SelectedWorkshops
    {
        get
        {
            return ViewState["SelectedWorkshops"] as List<string>;
        }
        set
        {
            ViewState["SelectedWorkshops"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PlantID = int.Parse(Request.QueryString["PlantID"]);
            string workshops = Request.QueryString["SelectedWorkshops"];
            if (!string.IsNullOrEmpty(workshops))
            {
                this.SelectedWorkshops = workshops.Split(',').ToList(); 
            }       
            BindData();
        }
    }

    private void BindData()
    {
        List<Workshop> workshops = Service.GetWorkshopbyPlant(new Plant { PlantID = PlantID });
        BindDataControl(gvWorkshops, workshops);
       
    }
    protected void gvWorkshops_PreRender(object sender, EventArgs e)
    {
        List<Workshop> workshops = new List<Workshop> { new Workshop() };
        BindEmptyGridView(gvWorkshops, workshops);
    }
    protected void gvWorkshops_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType== DataControlRowType.DataRow)
        {
            Workshop workshop = e.Row.DataItem as Workshop;
            CheckBox cbSelect = e.Row.Cells[0].FindControl("cbSelect") as CheckBox;
            if (SelectedWorkshops!= null&& SelectedWorkshops.Contains(workshop.WorkshopCode))
            {
                cbSelect.Checked = true;
            }
            else
            {
                cbSelect.Checked = false;
            }
        }
    }
}
