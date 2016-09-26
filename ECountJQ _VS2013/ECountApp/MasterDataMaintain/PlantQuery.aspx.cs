using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class MasterDataMaintain_PlantQuery : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!Page.IsPostBack)
        {
            BindPlants(ddlPlantCode);
            bind();
        }
    }

    protected void bind()
    {
        List<Plant> list = new List<Plant>();
        list = Service.GetPlants();
        gvPlants.DataKeyNames = new string[] { "PlantID" };
        gvPlants.DataSource = list;
        gvPlants.DataBind();
    }

    protected void DeletePlants_Click(object sender, EventArgs e)
    {
        Delete();
    }

    private void Delete()
    {
        List<int> checkedList = new List<int>();
        for (int i = 0; i < gvPlants.Rows.Count; i++)
        {
            CheckBox ch = new CheckBox();
            GridViewRow row = gvPlants.Rows[i];
            ch = row.Cells[0].FindControl("IsCheck") as CheckBox;

            if (ch.Checked)
            {
                checkedList.Add(int.Parse(gvPlants.DataKeys[row.RowIndex].Value.ToString()));
            }
        }
        foreach (var item in checkedList)
        {
            Service.DeletePlant(item);
        }
        bind();

        RebindPlant();
    }

    protected void RebindPlant()
    {
        ddlPlantCode.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "--";
        li.Value = "";
        ddlPlantCode.Items.Insert(0, li);

        List<Plant> list = new List<Plant>();
        list = Service.GetPlants();
        for (int i = 0; i < list.Count; i++)
        {
            ListItem li1 = new ListItem();
            li1.Text = list[i].PlantCode;
            li1.Value = list[i].PlantID.ToString();
            ddlPlantCode.Items.Add(li1);
        }
    }

    //change Item selected State，all selected or all not selected
    protected void ChkAll_CheckedChanged(object sender, System.EventArgs e)
    {
        CheckBox cbCheckAll = (CheckBox)sender;
        CheckBox chkExport;
        if (cbCheckAll.Checked)
        {
            foreach (GridViewRow dr in gvPlants.Rows)
            {
                chkExport = (CheckBox)dr.FindControl("IsCheck");
                chkExport.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow dr in gvPlants.Rows)
            {
                chkExport = (CheckBox)dr.FindControl("IsCheck");
                chkExport.Checked = false;
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        int temp = 0;
        CheckBox ch;
        for (int i = 0; i < gvPlants.Rows.Count; i++)
        {
            ch = (CheckBox)(gvPlants.Rows[i].FindControl("IsCheck"));
            if (!ch.Checked)
            {
                break;
            }
            else
            {
                temp++;
            }
        }

        if (gvPlants.Rows.Count == temp)
        {
            CheckBox ch1 = new CheckBox();
            ch1 = (CheckBox)gvPlants.HeaderRow.FindControl("ChkAll");
            ch1.Checked = true;
        }
        else
        {
            CheckBox ch2 = new CheckBox();
            ch2 = (CheckBox)gvPlants.HeaderRow.FindControl("ChkAll");
            ch2.Checked = false;
        }
    }

    // redirect to edit page
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        string opName = linkButton.CommandName;
        string url;
        if (opName == "modify")
        {
            //string script = string.Format("showDialog('PlantDetails.aspx?Mode=Edit&PlantID={0}',800,600,null,null);",linkButton.CommandArgument);
            //if (!Page.ClientScript.IsClientScriptBlockRegistered("dialogModal"))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dialogModal", script, true);
            //}
            url = string.Format("PlantEdit.aspx?PlantID={0}", linkButton.CommandArgument);
            Response.Redirect(url);
        }
    }
    protected void gvPlants_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            LinkButton btnModify = e.Row.Cells[2].FindControl("LinkButton1") as LinkButton;
            Plant plant = e.Row.DataItem as Plant;
            string script = string.Format("showDialog('PlantDetails.aspx?Mode=Edit&PlantID={0}',600,200,null, \"refresh('{1}')\");return false;", plant.PlantID, Toolbar1.Controls[2].ClientID);

            btnModify.OnClientClick = script;
        }
    }

    //search plant
    protected void QueryPlant_Click()
    {
        string selectValue = this.ddlPlantCode.SelectedValue;
        if (string.IsNullOrEmpty( this.ddlPlantCode.SelectedValue) )
        {
            bind();
        }
        else
        {
            Plant plant = new Plant();
            plant.PlantID = int.Parse(this.ddlPlantCode.SelectedValue);        

            List<Plant> list = new List<Plant>();
            list.Add(Service.GetPlantByKey(plant));
            gvPlants.DataSource = list;
            gvPlants.DataBind();
        }
        RebindPlant();
        if (!string.IsNullOrEmpty(selectValue))
        {
            this.ddlPlantCode.SelectedValue = selectValue;
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "delete":
                Delete();
                break;

            case "search":
                QueryPlant_Click();
                break;
            default:
                break;
        }
    }

}
