using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;

public partial class MasterDataMaintain_AddWorkshop : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDownList(this.ddlPlant, DropDownType.Plant);
            this.ddlPlant.Items.RemoveAt(0);
            if (Request.QueryString["WorkshopID"] != null)
            {
                bindBaseData(Request.QueryString["WorkshopID"].ToString());
            }
        }
    }

    private void bindBaseData(string workshopID)
    {
        Workshop workshop = new Workshop();//Service
        workshop.WorkshopID = int.Parse(workshopID);
        Service.GetWorkshopbykey(workshop);

        //this.hidWorkshopID.Value = workshop.WorkshopID.ToString();
        this.ddlPlant.SelectedValue = workshop.Plant.PlantID.ToString();
        this.tbWorkshopCode.Text = workshop.WorkshopCode;
        this.tbWorkshopName.Text = workshop.WorshopName;

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        Workshop workshop = new Workshop();
        workshop.WorkshopCode = tbWorkshopCode.Text;
        workshop.WorshopName = tbWorkshopName.Text;
        Plant plant = new Plant();
        plant.PlantID = int.Parse(ddlPlant.SelectedItem.Value);
        workshop.Plant = plant;

        Service.AddWorkshop(workshop);

        /*提示添加成功，同时关闭当前窗口*/
        Response.Write("<script>alert('保存成功！');</script>");
    }
    protected void ButBack_Click(object sender, EventArgs e)
    {
        string url = "WorkShopQuery.aspx";
        Response.Redirect(url);
    }
}
