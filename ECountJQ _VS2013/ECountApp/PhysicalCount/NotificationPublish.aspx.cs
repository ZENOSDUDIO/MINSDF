using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Utility;

public partial class PhysicalCount_NotificationPublish : ECountBasePage
{
    protected long NotificationID
    {
        get
        {
            return Convert.ToInt64(ViewState["id"]);
        }
        set
        {
            ViewState["id"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            NotificationID = int.Parse(Request.QueryString["id"]);
        }
        if (!IsPostBack)
        {
            if (Mode == PageMode.View)
            {
                Toolbar1.Visible = false;
            }
            BindData();
        }
        pager.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pager_PageNumberSelect);
        //pager.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(pager_PageSizeChange);
    }

    void pager_PageNumberSelect(object sender, EventArgs e)
    {
        BindDetails();
    }

    //void pager_PageSizeChange(object sender, EventArgs e)
    //{
    //    BindDetails();
    //}

    protected void BindData()
    {
        BindNotification();
        BindDetails();

        rpSLOC.DataSource = this.StoreLocationTypes;// Service.QueryStoreLocations(new StoreLocation());
        rpSLOC.DataBind();
    }

    protected List<string> SelectedLocationTypes
    {
        get
        {
            if (ViewState["SelectedLocationTypes"] == null)
            {
                ViewState["SelectedLocationTypes"] = new List<string>();
            }
            List<string> list = ViewState["SelectedLocationTypes"] as List<string>;
            //if (list.Count == 0)
            //{
            //    list.Add("RDC");
            //    ViewState["SelectedLocationTypes"] = list;
            //}
            return ViewState["SelectedLocationTypes"] as List<string>;
        }
        set
        {
            ViewState["SelectedLocationTypes"] = value;
        }
    }
    bool _existsCSMTPart = false;
    bool _existsRepairPart = false;
    private void BindNotification()
    {

        StocktakeNotification notification = Service.GetNotification(new StocktakeNotification { NotificationID = NotificationID });
        _existsCSMTPart = Service.NotiExistsCSMTPart(notification);
        _existsRepairPart = Service.NotiExistsRepairPart(notification);

        SelectedLocationTypes = notification.StocktakeLocations.Select(l => l.StoreLocationType.TypeName).ToList();
        Toolbar1.Visible = (notification.StocktakeStatus.StatusID == Consts.STOCKTAKE_NEW_NOTIFICATION);
        //Toolbar1.Items[1].Visible = (notification.StocktakeStatus.StatusID == Consts.STOCKTAKE_PUBLISHED);

        List<StocktakeNotification> list = new List<StocktakeNotification> { notification };
        BindDataControl(fvNotification, list);

        RadioButtonList rblEmergency = fvNotification.FindControl("rblEmergency") as RadioButtonList;
        rblEmergency.Enabled = (Mode != PageMode.View);

        TextBox txtPlanDate = fvNotification.FindControl("txtPlanDate") as TextBox;
        txtPlanDate.ReadOnly = (Mode == PageMode.View);

        ImageButton btnCalendar = fvNotification.FindControl("btnCalendar") as ImageButton;
        btnCalendar.Enabled = (Mode != PageMode.View);

        if (notification.StocktakeStatus.StatusID != Consts.STOCKTAKE_NEW_NOTIFICATION)
        {
            //Toolbar1.Controls[0].Visible = false;
        }
        if (notification.StocktakeStatus.StatusID != Consts.STOCKTAKE_PUBLISHED)
        {
            //Toolbar1.Controls[1].Visible = false;
        }
    }

    private void BindDetails()
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeDetails> details = Service.QueryNotifyDetailsByPage(new View_StocktakeDetails { NotificationID = NotificationID }, null, null, null, null, null, pager.PageSize, pager.CurrentPage, out pageCount, out itemCount);

        BindDataControl(gvDetails, details);
        //details.Select(d=>d.
        pager.TotalRecord = itemCount;
    }

    protected void rpSLOC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            CheckBox cbSLOC = e.Item.FindControl("cbSLOC") as CheckBox;
            StoreLocationType type = this.StoreLocationTypes.Find(t => string.Equals(t.TypeName, cbSLOC.Text));
            //if (type.RequiredToCount.Value)
            ////if (Toolbar1.Visible&&cbSLOC.Text=="RDC")
            //{
            //    cbSLOC.Checked = true;
            //}
            //if (SelectedLocationTypes.IndexOf(cbSLOC.Text) >= 0)
            //{
            //    cbSLOC.Checked = true;
            //}
            if (type.RequiredToCount.Value || SelectedLocationTypes.IndexOf(cbSLOC.Text) >= 0||(SelectedLocationTypes == null || SelectedLocationTypes.Count == 0) && (type.TypeID == 3 && _existsRepairPart || type.TypeID == 4 && _existsCSMTPart))
            {
                cbSLOC.Checked = true;
            }
            StoreLocation location = e.Item.DataItem as StoreLocation;
            cbSLOC.Enabled = (Mode != PageMode.View);
        }
        else
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox cbSLOC = e.Item.FindControl("cbSLOC") as CheckBox;
                StoreLocationType type = this.StoreLocationTypes.Find(t => string.Equals(t.TypeName, cbSLOC.Text));
                if (type.RequiredToCount.Value || SelectedLocationTypes.IndexOf(cbSLOC.Text) >= 0 || (SelectedLocationTypes == null || SelectedLocationTypes.Count == 0) && (type.TypeID == 3 && _existsRepairPart || type.TypeID == 4 && _existsCSMTPart))
                //if (SelectedLocationTypes.IndexOf(cbSLOC.Text) >= 0)
                {
                    cbSLOC.Checked = true;
                }
                cbSLOC.Enabled = (Mode != PageMode.View);
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Publish":
                List<StoreLocationType> locationTypes = new List<StoreLocationType>();
                for (int i = 0; i < rpSLOC.Items.Count; i++)
                {
                    CheckBox cbSLOC = rpSLOC.Items[i].FindControl("cbSLOC") as CheckBox;
                    if (cbSLOC.Checked)
                    {
                        string sloc = cbSLOC.Text;
                        StoreLocationType location = this.StoreLocationTypes.Find(s => s.TypeName == sloc);
                        if (location != null)
                        {
                            locationTypes.Add(location);
                        }
                    }
                }
                if (locationTypes.Count <= 0)
                {
                    return;
                }
                TextBox txtPlanDate = fvNotification.FindControl("txtPlanDate") as TextBox;
                DateTime planDate = DateTime.Parse(txtPlanDate.Text);
                RadioButtonList rblEmergency = fvNotification.FindControl("rblEmergency") as RadioButtonList;
                bool isEmergent = (rblEmergency.SelectedIndex == 0);
                Service.PublishNotification(new StocktakeNotification { NotificationID = NotificationID, Publisher = CurrentUser.UserInfo, PlanDate = planDate, IsEmergent = isEmergent }, locationTypes);
                Toolbar1.Visible = false;
                //Toolbar1.Items[1].Visible = true;
                Response.Write("<script>alert('发布成功！');</script>");
                Response.Write("<script>window.close();</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnPublish();", true);
                break;
            default:
                break;
        }
    }

    protected void gv_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> details = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvDetails, details);
    }
}
