using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class BizDataMaintain_Segment : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindPlants(this.ddlPlantCode);
            bind();
        }
    }



    protected void bind()
    {
        BindSegments(gvSegments);
    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chCheckALL = (CheckBox)sender;

        if (chCheckALL.Checked)
        {
            foreach (GridViewRow dr in gvSegments.Rows)
            {
                (dr.FindControl("IsCheck") as CheckBox).Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in gvSegments.Rows)
            {
                (dr.FindControl("IsCheck") as CheckBox).Checked = false;
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = gvSegments.HeaderRow.FindControl("ChkAll") as CheckBox;
        int iCount = 0;

        foreach (GridViewRow dr in gvSegments.Rows)
        {
            CheckBox cb = new CheckBox();
            cb = dr.FindControl("IsCheck") as CheckBox;

            if (!cb.Checked)
            {
                break;
            }
            else
            {
                iCount++;
            }
        }

        if (gvSegments.Rows.Count == iCount)
        {
            chkAll.Checked = true;
        }
        else
        {
            chkAll.Checked = false;
        }
    }

    protected void DeleteSegment()
    {
        List<Segment> checkedList = new List<Segment>();
        CheckBox chk;

        for (int i = 0; i < gvSegments.Rows.Count; i++)
        {
            chk = gvSegments.Rows[i].Cells[0].FindControl("IsCheck") as CheckBox;

            if (chk.Checked)
            {
                Segment segment = new Segment();
                segment.SegmentID = int.Parse(gvSegments.DataKeys[i].Value.ToString());
                checkedList.Add(segment);
            }
        }

        foreach (var item in checkedList)
        {
            Service.DeleteSegment(item);
        }

        bind();
    }

    protected void QuerySegment()
    {
        Segment segment = new Segment();
        List<Segment> list = new List<Segment>();

        if (!string.IsNullOrEmpty(this.txtSegmentCode.Text))
        {
            segment.SegmentCode = this.txtSegmentCode.Text.ToString();          
        }

        if ("--" != this.ddlwokshopcode.Text.ToString())
        {
            Workshop workshop = new Workshop();
            segment.Workshop = workshop;
            segment.Workshop.WorkshopCode = this.ddlwokshopcode.SelectedItem.ToString();

            Plant plant = new Plant();
            segment.Workshop.Plant = plant;
            segment.Workshop.Plant.PlantCode = ddlPlantCode.SelectedItem.ToString();
        }
        else
        {
            if ("--" != this.ddlPlantCode.Text.ToString())
            {
                Workshop workshop = new Workshop();
                Plant plant = new Plant();
                segment.Workshop = workshop;
                segment.Workshop.Plant = plant;
                segment.Workshop.Plant.PlantCode = ddlPlantCode.SelectedItem.ToString();
            }
        }

        list = Service.QuerySegmentByPage(segment);
        gvSegments.DataSource = list;
        gvSegments.DataBind();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                DeleteSegment();
                break;
            case "Search":
                QuerySegment();
                break;
            default:
                break;
        }
    }

    protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlPlantCode.SelectedItem.ToString() != "--")
        {
            this.ddlwokshopcode.Items.Clear();
            ddlwokshopcode.Items.Insert(0, "--");
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantCode.SelectedValue);
            BindWorkshops(this.ddlwokshopcode, plant);
        }
        else
        {
            this.ddlwokshopcode.Items.Clear();
            ddlwokshopcode.Items.Insert(0, "--");
        }
    }

    protected void gvSegments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            LinkButton btnModify = e.Row.Cells[2].FindControl("LinkButton1") as LinkButton;
            Segment segment = e.Row.DataItem as Segment;

            string script = string.Format("showDialog('SegmentDetails.aspx?Mode=Edit&SegmentID={0}',700,300,null,\"refresh('{1}')\");return false;", segment.SegmentID, Toolbar1.Controls[2].ClientID);

            btnModify.OnClientClick = script;
        }
    }
}
