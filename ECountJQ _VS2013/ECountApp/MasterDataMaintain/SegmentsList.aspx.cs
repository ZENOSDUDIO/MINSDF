using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.MobileControls;
using SGM.ECount.DataModel;

public partial class MasterDataMaintain_SegmentsList : ECountBasePage
{

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


    public List<string> SelectedSegments
    {
        get
        {
            return ViewState["SelectedSegments"] as List<string>;
        }
        set
        {
            ViewState["SelectedSegments"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string workshops = Request.QueryString["SelectedWorkshops"];
            if (!string.IsNullOrEmpty(workshops))
            {
                this.SelectedWorkshops = workshops.Split(',').ToList();
            }
            string segments = Request.QueryString["SelectedSegments"];
            if (!string.IsNullOrEmpty(segments))
            {
                this.SelectedSegments = segments.Split(',').ToList();
            }
            BindData();
        }
    }

    private void BindData()
    {

        List<Segment> segments = Service.GetSegmentsByWorkshopCodes(SelectedWorkshops);
        BindDataControl(gvSegments, segments);
    }
    protected void gvSegments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Segment segment = e.Row.DataItem as Segment;
            CheckBox cbSelect = e.Row.Cells[0].FindControl("cbSelect") as CheckBox;
            if (SelectedSegments != null && SelectedSegments.Contains(segment.SegmentCode))
            {
                cbSelect.Checked = true;
            }
            else
            {
                cbSelect.Checked = false;
            }
        }
    }
    protected void gvSegments_PreRender(object sender, EventArgs e)
    {
        List<Segment> list = new List<Segment> { new Segment() };
        BindEmptyGridView(gvSegments,list);
    }
}
