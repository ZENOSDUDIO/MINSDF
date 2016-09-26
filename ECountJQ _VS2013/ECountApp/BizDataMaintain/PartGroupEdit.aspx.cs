using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class BizDataMaintain_PartGroupEdit : ECountBasePage
{
    public List<ViewPart> GroupParts
    {
        get
        {
            if (Session["PartGroupEdit_GroupParts"] == null)
            {
                Session["PartGroupEdit_GroupParts"] = new List<ViewPart>();
            }
            return Session["PartGroupEdit_GroupParts"] as List<ViewPart>;
        }
        set
        {
            Session["PartGroupEdit_GroupParts"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "零件组编辑";
        if (!IsPostBack)
        {
            GroupParts = null;
            if (Request.QueryString["groupid"] != null)
            {
                PartGroup model = Service.GetPartGroupByKey(int.Parse(Request.QueryString["groupid"]));
                this.txtGroupName.Text = model.GroupName;
                this.txtDescription.Text = model.Description;
                hidGroupID.Value = model.GroupID.ToString();
                PartGroup group = new PartGroup();
                group.GroupID = int.Parse(hidGroupID.Value);
                bindGridViewPartResult(group);
            }
        }
        bindGridViewTempPartResult();
    }

    private void bindGridViewPartResult(PartGroup model)
    {
        if (model != null)
        {
            List<ViewPart> parts = this.Service.QueryPartsByGroup(model);
            this.GridViewPartResult.DataSource = parts;
            this.GridViewPartResult.DataBind();
            GroupParts = parts;
        }
    }

    private void bindGridViewTempPartResult()
    {
            List<SGM.ECount.DataModel.ViewPart> parts = GroupParts;
            this.GridViewPartResult.DataSource = parts;
            this.GridViewPartResult.DataBind();
    }


    protected void bindSelectedParts()
    {
        if (this.hidPartIDs.Value.Length == 0)
            return;
        string guids = this.hidPartIDs.Value;
        string[] gids = guids.Split('∑');

        //if (ViewState["Parts"] == null)
        //{
        //    ViewState["Parts"] = new List<ViewPart>();
        //}
        List<ViewPart> parts = GroupParts;
        foreach (object obj in gids)
        {
            ViewPart vp = Service.GetViewPartByKey(obj.ToString());
            int partid = int.Parse(obj.ToString());
            if (parts.Find(p => p.PartID == partid) == null)
            {
                parts.Add(vp);
            }
        }
        GroupParts = parts;
        bindGridViewTempPartResult();
    }

    //remove ViewPart object from current PartGroup
    protected void lnkRemoveViewPart_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        int partid = int.Parse(linkButton.CommandArgument);
        if (GroupParts.Count>0)
        {
            List<ViewPart> parts = GroupParts;
            parts.Remove(parts.Find(vp => vp.PartID == partid));
            GroupParts = parts;
            bindGridViewTempPartResult();
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                btnSave_Click(null, null);
                break;
            case "return":
                Response.Write("<script>window.location.href='PartGroupQuery.aspx';</script>");
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.txtGroupName.Text.Trim().Length > 0)
        {
            PartGroup group = new PartGroup();
            group.GroupName = this.txtGroupName.Text.Trim();
            group.Description = this.txtDescription.Text.Trim();
            group.GroupID = int.Parse(hidGroupID.Value);

            List<Part> parts = new List<Part>();
            if (GroupParts.Count>0)
            {
                List<ViewPart> vps = GroupParts;
                foreach (ViewPart vp in vps)
                {
                    group.GroupParts.Add(new GroupPartRelation
                    {
                        Part = new Part { PartID = vp.PartID }
                    });
                    parts.Add(new Part { PartID = vp.PartID });
                }
            }
            if (group.GroupID > 0)
            {
                Service.UpdatePartGroup(group);
            }
            else
            {
                PartGroup temp = new PartGroup();
                temp.GroupName = group.GroupName;
                List<PartGroup> list = Service.QueryPartGroups(temp);
                if (list != null && list.Count > 0)
                {
                    RegisterStartupScript("Message", "<script>alert('该分组名称已存在');</script>");
                    return;
                }
                else
                {
                    group = Service.AddPartGroup(group);
                    hidGroupID.Value = group.GroupID.ToString();
                }
            }
        }
        else
        {
            this.txtGroupName.Focus();
            Response.Write("<script>alert('分组名称不能为空。');</script>");
        }
    }

    protected void GridViewPartResult_PreRender(object sender, EventArgs e)
    {
        if (GridViewPartResult.Rows.Count == 0)
        {
            List<SGM.ECount.DataModel.ViewPart> parts = new List<ViewPart> { new ViewPart() };
            GridViewPartResult.DataSource = parts;
            GridViewPartResult.DataBind();
            GridViewPartResult.Rows[0].Visible = false;
        }
    }

    protected void btnTemp_Click(object sender, EventArgs e)
    {
        this.bindSelectedParts();
    }
}
