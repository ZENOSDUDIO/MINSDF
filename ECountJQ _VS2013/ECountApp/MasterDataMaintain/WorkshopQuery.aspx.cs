using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class MasterDataMaintain_Workshop : ECountBasePage
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
        List<Workshop> list = new List<Workshop>();
        gvWorkshops.DataKeyNames = new string[] { "WorkShopID" };
        list = Service.Getworkshop();
        gvWorkshops.DataSource = list;
        gvWorkshops.DataBind();
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        int temp = 0;
        CheckBox chk;

        CheckBox cbCheckAll;
        cbCheckAll = (gvWorkshops.HeaderRow.FindControl("ChkAll") as CheckBox);
        foreach (GridViewRow dr in gvWorkshops.Rows)
        {
            chk = dr.FindControl("IsCheck") as CheckBox;
            if (!chk.Checked)
            {
                break;
            }
            else
            {
                temp++;
            }

        }

        if (gvWorkshops.Rows.Count == temp)
        {
            cbCheckAll.Checked = true;
        }
        else
        {
            cbCheckAll.Checked = false;
        }
    }

    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chCheckALL = (CheckBox)sender;
        if (chCheckALL.Checked)
        {
            foreach (GridViewRow dr in gvWorkshops.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = dr.FindControl("IsCheck") as CheckBox;
                chk.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in gvWorkshops.Rows)
            {
                CheckBox chk = new CheckBox();
                chk = dr.FindControl("IsCheck") as CheckBox;
                chk.Checked = false;
            }
        }
    }

    protected void RebindWorkshop()
    {
        ddlwokshopcode.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "--";
        li.Value = "";
        ddlwokshopcode.Items.Insert(0, li);

        if (this.ddlPlantCode.SelectedValue != "--")
        {
            List<Workshop> list = new List<Workshop>();
            list = Service.GetWorkshopbyPlantID(int.Parse(this.ddlPlantCode.SelectedValue));
            for (int i = 0; i < list.Count; i++)
            {
                ListItem li1 = new ListItem();
                li1.Text = list[i].WorkshopCode;
                li1.Value = list[i].WorkshopID.ToString();
                ddlwokshopcode.Items.Add(li1);
            }
        }
    }

    protected void DeleteWorkshop()
    {
        List<Workshop> checkedList = new List<Workshop>();
        CheckBox chk = new CheckBox();

        for (int i = 0; i < gvWorkshops.Rows.Count; i++)
        {

            chk = gvWorkshops.Rows[i].Cells[0].FindControl("IsCheck") as CheckBox;

            if (chk.Checked)
            {
                Workshop workshop = new Workshop();
                workshop.WorkshopID = int.Parse(gvWorkshops.DataKeys[i].Value.ToString());
                checkedList.Add(workshop);
            }
        }

        foreach (var item in checkedList)
        {
            Service.DeleteWorkshop(item);
        }
        bind();

        RebindWorkshop();
    }

    //search workshop
    protected void QueryWorkshop()
    {
        string selectValue = this.ddlwokshopcode.SelectedValue;

        if (("--" == this.ddlPlantCode.Text.ToString())
            && ("--" == this.ddlwokshopcode.SelectedItem.Text.ToString()))
        {
            bind();
        }
        else if ("--" == this.ddlwokshopcode.SelectedItem.Text.ToString())
        {
            Workshop workshop = new Workshop();
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantCode.SelectedValue);
            plant.PlantCode = this.ddlPlantCode.SelectedItem.ToString();
            workshop.Plant = plant;

            List<Workshop> list = new List<Workshop>();
            list = Service.GetWorkshopbyPlantID(plant.PlantID);
            gvWorkshops.DataSource = list;
            gvWorkshops.DataBind();
        }
        else
        {
            Workshop workshop = new Workshop();
            workshop.WorkshopID = int.Parse(this.ddlwokshopcode.SelectedValue);

            List<Workshop> list = new List<Workshop>();
            list.Add(Service.GetWorkshopbykey(workshop));
            gvWorkshops.DataSource = list;
            gvWorkshops.DataBind();
        }

        RebindWorkshop();
        if (!string.IsNullOrEmpty(selectValue))
        {
            this.ddlwokshopcode.SelectedValue = selectValue;
        }

    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                DeleteWorkshop();
                break;
            case "search":
                QueryWorkshop();
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
            ddlwokshopcode.Items.Insert(0, new ListItem("--",""));
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantCode.SelectedValue);
            BindWorkshops(this.ddlwokshopcode, plant);
        }
        else
        {
            this.ddlwokshopcode.Items.Clear();
            ddlwokshopcode.Items.Insert(0, new ListItem("--", ""));
        }
    }

    protected void gvWorkshops_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            LinkButton btnModify = e.Row.Cells[2].FindControl("LinkButton1") as LinkButton;
            Workshop workshop = e.Row.DataItem as Workshop;
            string script = string.Format("showDialog('WorkshopDetails.aspx?Mode=Edit&WorkshopID={0}',700,300,null, \"refresh('{1}')\");return false;", workshop.WorkshopID, Toolbar1.Controls[2].ClientID);
            btnModify.OnClientClick = script;
        }
    }
}
