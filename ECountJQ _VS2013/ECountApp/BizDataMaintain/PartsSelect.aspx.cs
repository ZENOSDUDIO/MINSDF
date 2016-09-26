using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Text;
using SCS.Web.UI.WebControls;

public partial class BizDataMaintain_PartsSelect : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDDLControl();
        }
        this.AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        this.AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);

    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        bindGridView();
    }

    void AspPager1_PageSizeChange(object sender, EventArgs e)
    {
        bindGridView();
    }

    /// <summary>
    /// 绑定下拉控件
    /// </summary>
    private void bindDDLControl()
    {
        BindDropDownList(this.ddlPlantID, DropDownType.Plant);
        ddlPlantID_SelectedIndexChanged(null, null);
        BindDropDownList(this.ddlCategoryID, DropDownType.PartCategory);
        BindDropDownList(this.ddlPartStatus, DropDownType.PartStatus);
        BindDropDownList(this.ddlCycleCountLevel, DropDownType.CycleCountLevel);
    }

  
    private void bindGridView()
    {
        Part part = getPartFilter();
        int pageCount;
        int itemCount;
        List<SGM.ECount.DataModel.ViewPart> parts = Service.QueryPartByPage(part, AspPager1.PageSize, AspPager1.SelectPageNumber, out pageCount, out itemCount);
        AspPager1.TotalPage = pageCount;
        AspPager1.TotalRecord = itemCount;
        this.gvParts.DataSource = parts;
        this.gvParts.DataBind();
    }
    private Part getPartFilter()
    {
        Part model = new Part();
        if (!string.IsNullOrEmpty(this.txtPartCode.Text))
        {
            model.PartCode = this.txtPartCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtPartChineseName.Text))
        {
            model.PartChineseName = this.txtPartChineseName.Text.Trim();
        }
        //if (!string.IsNullOrEmpty(this.txtUpdateBy.Text))
        //{
        //    model.UpdateBy = int.Parse(this.txtUpdateBy.Text.Trim());//get user id by userName
        //}
        if (!string.IsNullOrEmpty(this.txtSpecs.Text))
        {
            model.Specs = this.txtSpecs.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtWorkLocation.Text))
        {
            model.WorkLocation = this.txtWorkLocation.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtFollowUp.Text))
        {
            model.FollowUp = this.txtFollowUp.Text.Trim();
        }


        if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
        {
            model.Plant = new Plant();
            model.Plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
            //model.Plant.PlantCode = this.ddlPlantID.SelectedItem.Text;
        }

        PartSegment ps = new PartSegment();
        if (this.ddlWorkshopID.SelectedValue.Length > 0)
        {
            ps.Segment = new Segment();
            ps.Segment.Workshop = new Workshop();
            ps.Segment.Workshop.WorkshopID = int.Parse(this.ddlWorkshopID.SelectedValue);
        }
        if (this.ddlSegmentID.SelectedValue.Length > 0)
        {
            if (ps.Segment == null)
                ps.Segment = new Segment();
            ps.Segment.SegmentID = int.Parse(this.ddlSegmentID.SelectedValue);
        }
        //if (this.ddlSegmentID.SelectedValue.Length > 0 || this.ddlWorkshopID.SelectedValue.Length > 0)
        //{
        //    model.PartSegments.Add(ps);
        //}


        model.PartStatus = new PartStatus();
        if (this.ddlPartStatus.SelectedValue.Length > 0)
        {
            model.PartStatus.StatusID = int.Parse(this.ddlPartStatus.SelectedValue);
            //model.PartStatus.StatusName = this.ddlPartStatus.SelectedItem.Text;
        }
        else
        {
            model.PartStatus = null;
        }
        model.Supplier = new Supplier();
        if (!string.IsNullOrEmpty(this.txtDUNS.Text))
        {
            model.Supplier.DUNS = this.txtDUNS.Text.Trim();
        }
        else
        {
            model.Supplier = null;
        }

        model.PartCategory = new PartCategory();
        if (!string.IsNullOrEmpty(this.ddlCategoryID.SelectedValue))
        {
            model.PartCategory.CategoryID = int.Parse(this.ddlCategoryID.SelectedValue.Trim());
        }
        else
        {
            model.PartCategory = null;
        }
        model.CycleCountLevel = new CycleCountLevel();
        if (!string.IsNullOrEmpty(this.ddlCycleCountLevel.SelectedValue))
        {
            model.CycleCountLevel.LevelID = int.Parse(this.ddlCycleCountLevel.SelectedValue.Trim());
        }
        else
        {
            model.CycleCountLevel = null;
        }
        return model;
    }
    //查询零件
    protected void butQuery_Click(object sender, EventArgs e)
    {
        bindGridView();
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "query":
                butQuery_Click(null, null);
                break;
            case "sure":
                ButSure_Click(null, null);
                break;
            default:
                break;
        }
    }

    public string GetSelectedRowIDS()
    {
        StringBuilder guids = new StringBuilder();
        for (int i = 0; i <= this.gvParts.Rows.Count - 1; i++)
        {
            GridViewRow row = gvParts.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                guids.Append(gvParts.DataKeys[row.RowIndex]["PartID"].ToString());
                guids.Append("∑");
            }
        }
        if (guids.Length > 0)
            guids.Remove(guids.Length - 1, 1);
        return guids.ToString();
    }

    protected void gvParts_PreRender(object sender, EventArgs e)
    {
        if (gvParts.Rows.Count == 0)
        {
            List<SGM.ECount.DataModel.ViewPart> parts = new List<ViewPart> { new ViewPart() };
            gvParts.DataSource = parts;
            gvParts.DataBind();
            gvParts.Rows[0].Visible = false;
        }
    }
    protected void ButSure_Click(object sender, EventArgs e)
    {
        string str = GetSelectedRowIDS();
        //string js = "if (window.parent.refreshData) {window.parent.refreshData();  } else  window.parent.location.href = window.parent.location.href;";
        Page.RegisterStartupScript("Set", "<script>setReturnValue('" + str + "');</script>");
    }

    //选择工厂
    protected void ddlPlantID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlPlantID.SelectedValue.Length > 0)
        {
            BindDropDownList(this.ddlWorkshopID, DropDownType.Workshop, this.ddlPlantID.SelectedValue);
        }
        else
        {
            ddlWorkshopID.Items.Clear();
        }
        ddlWorkshopID_SelectedIndexChanged(null, null);
    }

    //选择车间
    protected void ddlWorkshopID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlWorkshopID.SelectedValue.Length > 0)
        {
            BindDropDownList(this.ddlSegmentID, DropDownType.Segment, this.ddlWorkshopID.SelectedValue);
        }
        else
        {
            ddlSegmentID.Items.Clear();
        }
    }
}
