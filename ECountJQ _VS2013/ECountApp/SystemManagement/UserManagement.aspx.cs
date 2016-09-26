using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class SystemManagement_UserManagement : ECountBasePage
{
    public int? UserID
    {
        get
        {
            return ViewState["UserID"] as int?;
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPlants(this.ddlPlantID);
            BindUserGroup(this.ddlUserGroupID);
            if (Request.QueryString["userid"] != null)
            {
                UserID = int.Parse(Request.QueryString["userid"]);
                bindBaseData();
            }
        }
    }

    private void bindBaseData()
    {
        User user = new User();
        user.UserID = UserID.Value;
        user = Service.GetUserbyKey(user);
        this.hidUserID.Value = user.UserID.ToString();
        this.txtUserName.Text = user.UserName;
        this.txtPassword.Text = user.Password;
        //this.txtSupplier.Text = user.DUNS;
        this.txtRepairDUNS.Text = user.RepairDUNS;
        this.txtConsignmentDUNS.Text = user.ConsignmentDUNS;
        if (user.UserGroup != null)
        {
            this.ddlUserGroupID.SelectedValue = user.UserGroup.GroupID.ToString();
        }
        if (user.Plant != null && user.Plant.PlantID != DefaultValue.INT)
        {
            this.ddlPlantID.SelectedValue = user.Plant.PlantID.ToString();
        }

        if (user.Workshop != null)
        {
            if (user.Workshop.WorkshopID != DefaultValue.INT)
            {
                BindWorkshops(this.ddlWorkShopID, user.Workshop.Plant);
                this.ddlWorkShopID.Items.FindByValue(user.Workshop.WorkshopID.ToString()).Selected = true;
            }

            if ((user.Segment != null) && (user.Segment.SegmentID != DefaultValue.INT))
            {
                BindSegments(this.ddlSegmentID, user.Workshop);
                this.ddlSegmentID.Items.FindByValue(user.Segment.SegmentID.ToString()).Selected = true;
            }
        }
        foreach (ToolbarButton item in Toolbar1.Items)
        {
            string commandName = item.CommandName;
            if (!user.Available&&user.RetryTimes>=5)
            {
                if (commandName == "unlock")
                {
                    item.Enabled = true;
                }
                if (commandName == "save")
                {
                    item.Enabled = false;
                }
            }
            else
            {
                if (commandName == "unlock")
                {
                    item.Enabled = false;
                }
                if (commandName == "save")
                {
                    item.Enabled = true;
                }
            }
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
                Response.Write("<script>window.location.href='UserList.aspx';</script>");
                break;
            case "unlock":
                User user = Service.GetUserbyKey(new User { UserID = UserID.Value });
                user.RetryTimes = 0;
                user.Available = true;
                Service.UpdateUser(user);
                bindBaseData();
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        User user = new User();
        user.UserName = this.txtUserName.Text.Trim();
        user.Password = this.txtPassword.Text.Trim();

        if (!string.IsNullOrEmpty(this.ddlUserGroupID.SelectedValue))
        {
            UserGroup usergroup = new UserGroup();
            user.UserGroup = usergroup;
            user.UserGroup.GroupID = int.Parse(this.ddlUserGroupID.SelectedValue);
        }
        else
        {
            Response.Write("<script>alert('请选择用户组');</script>");
            return;
        } 

        if (!string.IsNullOrEmpty(this.txtRepairDUNS.Text))
        {
            user.RepairDUNS = this.txtRepairDUNS.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.txtConsignmentDUNS.Text))
        {
            user.ConsignmentDUNS = this.txtConsignmentDUNS.Text.Trim();
        }

        if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
        {
            user.Plant = new Plant { PlantID = int.Parse(this.ddlPlantID.SelectedValue) };
        }
        if (!string.IsNullOrEmpty(this.ddlWorkShopID.SelectedValue))
        {
            user.Workshop = new Workshop { WorkshopID = int.Parse(ddlWorkShopID.SelectedValue) };
        }

        if (!string.IsNullOrEmpty(this.ddlSegmentID.SelectedValue))
        {
            Segment segment = new Segment();
            user.Segment = segment;
            user.Segment.SegmentID = int.Parse(this.ddlSegmentID.SelectedValue);
        }
        user.CreateDate = DateTime.Now;
        user.LastModified = null;

        if (UserID != null)
        {
            user.UserID = UserID.Value;
        }
        if (Service.ExistUser(user))
        {
            ClientScript.RegisterStartupScript(this.GetType(),"Message", "<script>alert('该用户已经存在');</script>");
            return;
        }

        if (UserID != null)
        {//update user
            user.UserID = UserID.Value;
            Service.UpdateUser(user);
        }
        else
        {//add user
            user = Service.AddUser(user);
            this.hidUserID.Value = user.UserID.ToString();
            UserID = user.UserID;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

    protected void ddlPlantID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ddlPlantID.SelectedValue))
        {
            this.ddlWorkShopID.Items.Clear();
            this.ddlWorkShopID.Items.Insert(0, "--");
            this.ddlWorkShopID.Items[0].Value = "";
            this.ddlSegmentID.Items.Clear();
            this.ddlSegmentID.Items.Insert(0, "--");
            this.ddlSegmentID.Items[0].Value = "";
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantID.SelectedValue.ToString());
            BindWorkshops(this.ddlWorkShopID, plant);
        }
        else
        {
            this.ddlWorkShopID.Items.Clear();
            this.ddlWorkShopID.Items.Insert(0, "--");
            this.ddlWorkShopID.Items[0].Value = "";
            this.ddlSegmentID.Items.Clear();
            this.ddlSegmentID.Items.Insert(0, "--"); ;
            this.ddlSegmentID.Items[0].Value = "";
        }
    }

    protected void ddlWorkShopID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ddlWorkShopID.SelectedValue))
        {
            this.ddlSegmentID.Items.Clear();
            this.ddlSegmentID.Items.Insert(0, "--");
            this.ddlSegmentID.Items[0].Value = "";
            Workshop workshop = new Workshop();
            workshop.WorkshopID = int.Parse(this.ddlWorkShopID.SelectedValue.ToString());
            BindSegments(this.ddlSegmentID, workshop);
        }
        else
        {
            this.ddlSegmentID.Items.Clear();
            this.ddlSegmentID.Items.Insert(0, "--");
            this.ddlSegmentID.Items[0].Value = "";
        }
    }

}
