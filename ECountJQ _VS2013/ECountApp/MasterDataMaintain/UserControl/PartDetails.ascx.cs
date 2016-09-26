using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Text;
using System.Web.Script;
using SGM.Common.Utility;

public partial class BizDataMaintain_PartDetails : ECountBaseUserControl
{
    public Part PartInfo { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            PartGroup info = new PartGroup();
            List<PartGroup> pgs = Service.QueryPartGroups(info);
            BindDataControl(gvGroup, pgs);
            BindData(PartInfo);
        }
    }

    public void BindData(Part part)
    {
        if (this.Page != null)
        {
            ECountBasePage pagebase = this.Page as ECountBasePage;
            pagebase.BindDropDownList(this.ddlPlantID, DropDownType.Plant);
            pagebase.BindDropDownList(this.ddlCategoryID, DropDownType.PartCategory);
            pagebase.BindDropDownList(this.ddlPartStatus, DropDownType.PartStatus);
            pagebase.BindDropDownList(this.ddlCycleCountLevel, DropDownType.CycleCountLevel);

            if (part != null)
            {
                ddlPlantID.SelectedValue = part.Plant != null ? part.Plant.PlantID.ToString() : "";

                if (part.PartCategory != null && part.PartCategory.CategoryID != DefaultValue.INT)
                {
                    ddlCategoryID.SelectedValue = part.PartCategory.CategoryID.ToString();
                }
                ddlPartStatus.SelectedValue = part.PartStatus != null ? part.PartStatus.StatusID.ToString() : "";
                ddlCycleCountLevel.SelectedValue = part.CycleCountLevel != null ? part.CycleCountLevel.LevelID.ToString() : "";
                //ddlPartGroup.SelectedValue = part.PartGroup != null ? part.PartGroup.GroupID.ToString() : "";

                this.hidPartID.Value = part.PartID.ToString();
                this.txtPartCode.Text = part.PartCode;//.ToString();
                this.txtPartEnglishName.Text = part.PartEnglishName;//.ToString();
                this.txtPartChineseName.Text = part.PartChineseName;//.ToString();
                this.hidSupplierID.Value = part.Supplier.SupplierID.ToString();
                this.txtSupplier.Text = part.Supplier.DUNS;
                txtWorkshops.Text = part.Workshops;
                txtSegments.Text = part.Segments;

                //显示当前所选车间的所有工段
                if (part.Workshops != null)
                {
                    List<String> workshopcode = new List<String>();
                    List<Segment> segment = new List<Segment>();
                    string[] split = part.Workshops.Split(',');
                    foreach (string s in split)
                    {
                        if (!string.IsNullOrEmpty(s.Trim()))
                        {
                            workshopcode.Add(s);
                        }
                    }
                    segment = Service.GetSegmentsByWorkshopCodes(workshopcode);

                }

                if (!string.IsNullOrEmpty(part.WorkLocation))
                {
                    string[] strs = part.WorkLocation.Split(',');
                    for (int i = 1; i <= strs.Length; i++)
                    {
                        (FindControl("txtWorkLocation" + i) as TextBox).Text = strs[i - 1];
                    }
                }
                if (!string.IsNullOrEmpty(part.Dloc))
                {
                    string[] strs = part.Dloc.Split(',');
                    for (int i = 1; i <= strs.Length; i++)
                    {
                        (FindControl("txtDloc" + i) as TextBox).Text = strs[i - 1];
                    }
                }
                this.txtFollowUp.Text = part.FollowUp == null ? "" : part.FollowUp.ToString();
                this.txtCycleCountTimes.Text = part.CycleCountTimes.ToString();
                this.txtSpecs.Text = part.Specs == null ? "" : part.Specs.ToString();
                this.txtDescription.Text = part.Description == null ? "" : part.Description;
                if (part.Groups.Count > 0)
                {
                     List<PartGroup> groups  = Service.GetGroupsByPart(part);

                    foreach (GridViewRow row in gvGroup.Rows)
                    {
                        if (gvGroup.DataKeys[row.RowIndex]["GroupID"] == null)
                        {
                            continue;
                        }
                        int groupID = (int)gvGroup.DataKeys[row.RowIndex]["GroupID"];
                        if (groups.Exists(g => g.GroupID == groupID))
                        {
                            CheckBox cbSelect = row.FindControl("cbSelect") as CheckBox;
                            if (cbSelect != null)
                            {
                                cbSelect.Checked = true;
                            }
                        }
                    }
                }
            }

        }
    }




    protected void ddlPlantID_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtWorkshops.Text = string.Empty;
        txtSegments.Text = string.Empty;
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                Save();
                break;
            case "return":
                Response.Write("<script>window.location.href='PartsQuery.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void Save()
    {
        Part model = new Part();
        if (this.hidPartID.Value.Length > 0)
        {
            model.PartID = int.Parse(this.hidPartID.Value);
        }
        List<PartGroup> groups = model.Groups.Select(g => g.PartGroup).ToList();
        foreach (GridViewRow row in gvGroup.Rows)
        {
            if (gvGroup.DataKeys[row.RowIndex]["GroupID"] == null)
            {
                continue;
            }
            CheckBox cbSelect = row.FindControl("cbSelect") as CheckBox;
            if (cbSelect.Checked)
            {
                int groupID = (int)gvGroup.DataKeys[row.RowIndex]["GroupID"];
                if (model.Groups.FirstOrDefault(g => g.PartGroup.GroupID == groupID) == null)
                {
                    GroupPartRelation relation = new GroupPartRelation
                    {
                        PartGroup = new PartGroup
                        {
                            GroupID = groupID
                        }
                    };
                    if (this.hidPartID.Value.Length > 0)
                    {
                        relation.Part = new Part
                        {
                            PartID = int.Parse(this.hidPartID.Value)
                        };
                    }
                    model.Groups.Add(relation);
                }
            }
        }
        model.PartCode = (this.FindControl("txtPartCode") as TextBox).Text;
        model.PartEnglishName = (this.FindControl("txtPartEnglishName") as TextBox).Text;
        model.PartChineseName = (this.FindControl("txtPartChineseName") as TextBox).Text;
        model.WorkLocation = getWorkLocation();
        model.Dloc = getDloc();
        model.FollowUp = (this.FindControl("txtFollowUp") as TextBox).Text;
        model.CycleCountTimes = short.Parse((this.FindControl("txtCycleCountTimes") as TextBox).Text);

        if (model.PartCategory == null)
            model.PartCategory = new PartCategory();
        if (ddlCategoryID.SelectedItem.ToString() != "--")
        {
            model.PartCategory.CategoryID = int.Parse(ddlCategoryID.SelectedValue);
        }
        else
        {
            model.PartCategory = null;
        }

        if (model.CycleCountLevel == null)
            model.CycleCountLevel = new CycleCountLevel();
        if (ddlCycleCountLevel.SelectedValue.Length > 0)
        {
            model.CycleCountLevel.LevelID = int.Parse(ddlCycleCountLevel.SelectedValue);
        }
        //model.CycleCountTimes = Convert.ToInt16((this.FindControl("txtCycleCountTimes") as TextBox).Text);
        if (model.PartStatus == null)
            model.PartStatus = new PartStatus();
        if (ddlPartStatus.SelectedValue.Length > 0)
        {
            model.PartStatus.StatusID = int.Parse(ddlPartStatus.SelectedValue);
        }
        //if (this.ddlPartGroup.SelectedValue.Length > 0)
        //{
        //    if (model.PartGroup == null)
        //        model.PartGroup = new PartGroup();
        //    model.PartGroup.GroupID = int.Parse(this.ddlPartGroup.SelectedValue);
        //}
        model.Specs = (this.FindControl("txtSpecs") as TextBox).Text;
        model.Description = (this.FindControl("txtDescription") as TextBox).Text;
        model.DateModified = DateTime.Now;
        if (model.Plant == null)
            model.Plant = new Plant();
        if (this.ddlPlantID.SelectedValue.Length > 0)
        {
            model.Plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue);
        }


        model.Workshops = txtWorkshops.Text;
        model.Segments = txtSegments.Text;

        if (this.hidSupplierID.Value.Length > 0)
        {
            if (model.Supplier == null)
                model.Supplier = new Supplier();
            model.Supplier.SupplierID = int.Parse(this.hidSupplierID.Value);
        }
        if (this.hidPartID.Value.Length > 0)
        {

            List<ViewPart> list = new List<ViewPart>();
            Part part = new Part();
            part.PartCode = model.PartCode;
            part.Plant = model.Plant;
            part.Supplier = model.Supplier;
            list = this.Service.QueryParts(part);
            if (list.Count > 0 && list[0].PartID != model.PartID)
            {
                Response.Write("<script>alert('该零件已存在');</script>");
                return;

            }
            model.UpdateBy = new User();
            model.UpdateBy.UserID = CurrentUser.UserInfo.UserID;
            this.Container.Service.UpdatePart(model);

        }
        else
        {
            Part temp = new Part();
            temp.PartCode = model.PartCode;
            temp.Supplier = model.Supplier;
            temp.Plant = model.Plant;
            List<ViewPart> list = this.Container.Service.QueryParts(temp);
            if (list != null && list.Count > 0)
            {
                Response.Write("<script>alert('该零件已存在');</script>");
                return;
            }
            else
            {
                model.UpdateBy = new User();
                model.UpdateBy.UserID = CurrentUser.UserInfo.UserID;
                model = this.Container.Service.AddPart(model);
                this.hidPartID.Value = model.PartID.ToString();
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

    private string getWorkLocation()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= 15; i++)
        {
            TextBox txt = FindControl("txtWorkLocation" + i) as TextBox;
            if (txt.Text.Trim().Length > 0)
            {
                sb.Append(txt.Text.Trim());
                sb.Append(",");
            }
        }
        if (sb.Length > 0)
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    private string getDloc()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= 15; i++)
        {
            TextBox txt = FindControl("txtDloc" + i) as TextBox;
            if (txt.Text.Trim().Length > 0)
            {
                sb.Append(txt.Text.Trim());
                sb.Append(",");
            }
        }
        if (sb.Length > 0)
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    protected void gvGroup_PreRender(object sender, EventArgs e)
    {
        List<PartGroup> groups = new List<PartGroup> { new PartGroup() };
        BindEmptyGridView(gvGroup, groups);
    }
}
