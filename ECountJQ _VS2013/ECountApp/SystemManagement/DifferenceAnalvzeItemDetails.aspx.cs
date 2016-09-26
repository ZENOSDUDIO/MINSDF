using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class SystemManagement_DifferenceAnalvzeItemDetails : ECountBasePage
{
    public int? DetailsID
    {
        get
        {
            return ViewState["DetailsID"] as int?;
        }
        set
        {
            ViewState["DetailsID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindUserGroup();
            if (Request.QueryString["DetailsID"] != null)
            {
                DifferenceAnalyseDetails diffAnalyseDetails = new DifferenceAnalyseDetails();
                diffAnalyseDetails.DetailsID = int.Parse(Request.QueryString["DetailsID"]);
                diffAnalyseDetails = Service.GetDiffAnalyseDetailstbyID(diffAnalyseDetails.DetailsID);
                DetailsID = diffAnalyseDetails.DetailsID;
                this.ddlUserGroup.SelectedValue = diffAnalyseDetails.UserGroup.GroupID.ToString();
                this.txtDescription.Text = diffAnalyseDetails.Description;
            }
        }
    }

    void bindUserGroup()
    {
        List<UserGroup> list = new List<UserGroup>();
        ddlUserGroup.DataValueField = "GroupID";
        ddlUserGroup.DataTextField = "GroupName";
        list = Service.GetUserGroups();
        ddlUserGroup.DataSource = list;
        ddlUserGroup.DataBind();
    }

    void updateDiffAnalyseDetails()
    {
        if (ddlUserGroup.SelectedValue != "--")
        {
            DifferenceAnalyseDetails diffAnalyseDetails = new DifferenceAnalyseDetails();
            UserGroup ugroup = new UserGroup();
            diffAnalyseDetails.Description = txtDescription.Text;
            diffAnalyseDetails.DetailsID = DetailsID.Value;
            diffAnalyseDetails.UserGroup = ugroup;
            diffAnalyseDetails.UserGroup.GroupID = int.Parse(ddlUserGroup.SelectedValue);
            if (Service.ExistDifferenceAnalyse(diffAnalyseDetails))
            {
                RegisterStartupScript("Message", "<script>alert('该用户组的差异分析项已存在');</script>");
                return;
            }
            else
            {
                Service.UpdateDiffAnalyseDetail(diffAnalyseDetails);
            }

        }
        else
        {
            Response.Write("<script>alert('请选择用户组');</script>");
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                if (DetailsID != null)
                {/* Update differenceAnalyseDetail */
                    updateDiffAnalyseDetails();

                }
                else
                { /* add differenceAnalyseDetail */
                    if (ddlUserGroup.SelectedValue != "--")
                    {
                        DifferenceAnalyseDetails diffAnalyseDetails = new DifferenceAnalyseDetails();
                        UserGroup ugroup = new UserGroup();
                        diffAnalyseDetails.Description = txtDescription.Text;
                        diffAnalyseDetails.UserGroup = ugroup;
                        diffAnalyseDetails.UserGroup.GroupID = int.Parse(ddlUserGroup.SelectedValue);
                        if (Service.ExistDifferenceAnalyse(diffAnalyseDetails))
                        {
                            RegisterStartupScript("Message", "<script>alert('该用户组的差异分析项已存在');</script>");
                            return;
                        }
                        else
                        {
                            Service.AddDiffAnalyseDetail(diffAnalyseDetails);
                        }
     
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择用户组');</script>");
                    }
                }

                break;

            default:
                break;
        }
    }
}
