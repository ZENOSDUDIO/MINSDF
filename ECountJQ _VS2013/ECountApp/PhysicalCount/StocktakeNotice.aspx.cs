using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Utility;

public partial class PhysicalCount_StocktakeNotice : ECountBasePage
{
    //protected override bool ShowWaitingModal
    //{
    //    get
    //    {
    //        return true;
    //    }
    //}


    protected long? NotificationID
    {
        get
        {
            return ViewState["NotificationID"] as long?;
            //if (!string.IsNullOrEmpty(Request.QueryString["NotificationID"]))
            //{
            //    long notificationID;
            //    if (long.TryParse(Request.QueryString["NotificationID"], out notificationID))
            //    {
            //        return notificationID;
            //    }
            //}
            //return null;
        }
        set
        {
            ViewState["NotificationID"] = value;
        }
    }



    protected List<View_StocktakeDetails> SelectedDetails
    {
        get
        {
            if (Session["Notice_SelectedDetails"] == null)
            {
                Session["Notice_SelectedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Notice_SelectedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Notice_SelectedDetails"] = value;
        }
    }

    protected List<View_StocktakeDetails> SelectedRemovedDetails
    {
        get
        {
            if (Session["Notice_SelectedRemDtls"] == null)
            {
                Session["Notice_SelectedRemDtls"] = new List<View_StocktakeDetails>();
            }
            return Session["Notice_SelectedRemDtls"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Notice_SelectedRemDtls"] = value;
        }
    }

    protected List<View_StocktakeDetails> RemovedDetails
    {
        get
        {
            if (Session["Notice_RemovedDetails"] == null)
            {
                Session["Notice_RemovedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Notice_RemovedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Notice_RemovedDetails"] = value;
        }
    }

    protected List<View_StocktakeDetails> IncludedDetails
    {
        get
        {
            if (Session["Notice_IncludedDetails"] == null)
            {
                Session["Notice_IncludedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Notice_IncludedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Notice_IncludedDetails"] = value;
        }
    }
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearCache();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))//edit
            {
                NotificationID = long.Parse(Request.QueryString["id"]);

            }
            if (Mode == PageMode.View)
            {
                Toolbar1.Visible = false;
            }
            BindData();
        }
        
        pagerDetails.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerDetails_PageNumberSelect);
        pagerRemovedDetails.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerRemovedDetails_PageNumberSelect);
       
    }

    private void ClearCache()
    {
        RemovedDetails = null;
        IncludedDetails = null;
        SelectedDetails = null;
        SelectedRemovedDetails = null;
    }


    void pagerRemovedDetails_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedRemovedDetails, gvRemovedDetails);
        BindRemovedDetails();
    }

    void pagerDetails_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedDetails, gvDetails);
        RefreshComments();
        BindDetails();
        //UncheckRemovedItems(RemovedStaticDetails, gvDetails);
    }


    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.FindControl("lblComment").Visible = Mode == PageMode.View;

            e.Row.FindControl("txtComment").Visible = Mode == PageMode.Edit;
            View_StocktakeDetails detail = e.Row.DataItem as View_StocktakeDetails;
            CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            if (SelectedDetails.Exists(d => d.DetailsID == detail.DetailsID))
            {
                cbSelect.Checked = true;
            }
            else
            {
                cbSelect.Checked = false;
            }
        }
    }
    protected void gvDetails_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void Remove(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedDetails, gvDetails);
        for (int i = SelectedDetails.Count - 1; i >= 0; i--)
        {
            View_StocktakeDetails detail = SelectedDetails[i];
            if (NotificationID != null)
            {
                int index = IncludedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
                if (index >= 0)
                {
                    IncludedDetails.RemoveAt(index);
                }
            }
            RemovedDetails.Add(detail);
            SelectedDetails.RemoveAt(i);
        }
        BindData();
    }
    protected void Add(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedRemovedDetails, gvRemovedDetails);
        for (int i = SelectedRemovedDetails.Count - 1; i >= 0; i--)
        {
            View_StocktakeDetails detail = SelectedRemovedDetails[i];
            int index = RemovedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
            if (index >= 0)
            {
                RemovedDetails.RemoveAt(index);
            }

            if (NotificationID != null)
            {
                IncludedDetails.Add(detail);
            }
            SelectedRemovedDetails.RemoveAt(i);
        }
        BindData();
    }
    protected void AddAll(object sender, EventArgs e)
    {
        IncludedDetails.AddRange(RemovedDetails);
        RemovedDetails.Clear();
        BindData();
    }
    protected void RemoveAll(object sender, EventArgs e)
    {
        //if (NotificationID!=null)
        //{
        //    for (int i = IncludedDetails.Count - 1; i >= 0; i--)
        //    {
        //        RemovedDetails.Add(IncludedDetails[i]);
        //        IncludedDetails.RemoveAt(i);
        //    }
        //}
        //BindData();
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long detailsID = long.Parse(gvDetails.DataKeys[e.RowIndex].Value.ToString());
        View_StocktakeDetails details = GetDetailsByRow(gvDetails.Rows[e.RowIndex]);
        details.DetailsID = detailsID;


        RefreshSelectedDetails(SelectedDetails, gvDetails);
        int idx = SelectedDetails.FindIndex(d => d.DetailsID == details.DetailsID);
        if (idx >= 0)
        {
            SelectedDetails.RemoveAt(idx);
        }

        RemovedDetails.Add(details);

        if (NotificationID != null)
        {
            int index = IncludedDetails.FindIndex(d => d.DetailsID == details.DetailsID);
            if (index >= 0)
            {
                IncludedDetails.RemoveAt(index);
            }
        }
        BindData();
    }

    private View_StocktakeDetails GetDetailsByRow(GridViewRow row)
    {
        View_StocktakeDetails details = new View_StocktakeDetails();
        Label lblPartCode = row.FindControl("lblPartCode") as Label;
        Label lblPlantCode = row.FindControl("lblPlantCode") as Label;
        Label lblWorkshops = row.FindControl("lblWorkshops") as Label;
        Label lblSegments = row.FindControl("lblSegments") as Label;
        Label lblWorklocation = row.FindControl("lblWorklocation") as Label;
        Label lblCName = row.FindControl("lblCName") as Label;
        Label lblDUNS = row.FindControl("lblDUNS") as Label;
        Label lblUserName = row.FindControl("lblUserName") as Label;
        Label lblTypeName = row.FindControl("lblTypeName") as Label;
        Label lblPriority = row.FindControl("lblPriority") as Label;
        TextBox txtComment = row.FindControl("txtComment") as TextBox;
        if (txtComment!=null)
        {
            details.NotifyComments = txtComment.Text.Trim();
        }
        details.PriorityName = lblPriority.Text;
        details.TypeName = lblTypeName.Text;
        details.CreateByUserName = lblUserName.Text;
        details.DUNS = lblDUNS.Text;
        details.PartChineseName = lblCName.Text;
        details.Segments = lblSegments.Text;
        details.WorkLocation = lblWorklocation.Text;
        details.Workshops = lblWorkshops.Text;
        details.PartCode = lblPartCode.Text;
        details.PartPlantCode = lblPlantCode.Text;
        return details;
    }

    protected void gvRemovedDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        View_StocktakeDetails detail = GetDetailsByRow(gvRemovedDetails.Rows[e.RowIndex]);
        detail.DetailsID = long.Parse(gvRemovedDetails.DataKeys[e.RowIndex].Value.ToString());

        RefreshSelectedDetails(SelectedRemovedDetails, gvRemovedDetails);
        int idx = SelectedRemovedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
        if (idx >= 0)
        {
            SelectedRemovedDetails.RemoveAt(idx);
        }

        int index = RemovedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
        if (index >= 0)
        {
            RemovedDetails.RemoveAt(index);
        }
        if (NotificationID != null)
        {
            IncludedDetails.Add(detail);
        }
        BindData();
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Save":
                RefreshComments();
                RadioButtonList rblIsStatic = dlNotification.Items[0].FindControl("rblIsStatic") as RadioButtonList;
                StocktakeNotification notifciation = new StocktakeNotification { Creator = CurrentUser.UserInfo, IsStatic = (rblIsStatic.SelectedIndex == 1) };
                DropDownList ddlPlant = dlNotification.Items[0].FindControl("ddlPlant") as DropDownList;
                notifciation.Plant = new Plant { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
                if (NotificationID != null)//update
                {
                    notifciation.NotificationID = NotificationID.Value;
                    Service.UpdateNotification(notifciation, RemovedDetails.Select(d => d.DetailsID).ToList(), IncludedDetails, false);
                }
                else
                {
                    List<long> removed = RemovedDetails.Select(d => d.DetailsID).ToList();
                    notifciation.DetailsView = IncludedDetails;
                    notifciation = Service.CreateNotification(notifciation, removed);
                    NotificationID = notifciation.NotificationID;
                }
                RemovedDetails.Clear();
                IncludedDetails.Clear();
                BindData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);

                break;
            case "SaveDynamic":
                //StocktakeNotification dynamicNotifciation = new StocktakeNotification { Creator = CurrentUser.UserInfo };
                //if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
                //{
                //    dynamicNotifciation.Plant = new Plant { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
                //}
                //if (NotificationID != null)
                //{
                //    dynamicNotifciation.NotificationID = NotificationID.Value;
                //}
                //else
                //{
                //    dynamicNotifciation.IsStatic = false;
                //    dynamicNotifciation = Service.CreateNotification(dynamicNotifciation, RemovedDynamicDetails);
                //    NotificationID = dynamicNotifciation.NotificationID;
                //    rblGenerateBy.Enabled = false;
                //}
                //List<StocktakeNotification> dynamicNotiList = new List<StocktakeNotification> { dynamicNotifciation };
                //BindDataControl(dlDynamic, dynamicNotiList);

                //Toolbar1.Items[3].Enabled = true;
                break;
            case "query":
                BindData();
                break;
            default:
                break;
        }
    }

    private void RefreshSelectedDetails(List<View_StocktakeDetails> selectedDetails, GridView gv)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox cbSelect = row.FindControl("cbSelect") as CheckBox;
            View_StocktakeDetails detail = GetDetailsByRow(row);
            detail.DetailsID = long.Parse(gv.DataKeys[row.RowIndex].Value.ToString());
            int index = selectedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
            if (index >= 0)
            {
                if (!cbSelect.Checked)
                {
                    selectedDetails.RemoveAt(index);
                }
            }
            else
            {
                if (cbSelect.Checked)
                {
                    selectedDetails.Add(detail);
                }
            }
        }
    }

    private void RefreshComments()
    {
        foreach (GridViewRow row in gvDetails.Rows)
        {
            long detailsID=long.Parse(gvDetails.DataKeys[row.RowIndex].Value.ToString());
            if (detailsID==DefaultValue.LONG)
            {
                continue;
            }
            View_StocktakeDetails detail = GetDetailsByRow(row);
            detail.DetailsID = detailsID;
            int index = IncludedDetails.FindIndex(d => d.DetailsID == detail.DetailsID);
            if (index >= 0)
            {
                IncludedDetails[index].NotifyComments = detail.NotifyComments;
            }
            else
            {
                if (NotificationID==null)
                {
                    if (!string.IsNullOrEmpty(detail.NotifyComments))
                    {
                        IncludedDetails.Add(detail);
                    }
                }
                else
                {
                    IncludedDetails.Add(detail);
                }
            }
        }
    }

    //private View_StocktakeDetails GetFilter()
    //{
    //    View_StocktakeDetails details = new View_StocktakeDetails();
    //    if (NotificationID != null)
    //    {
    //        details.NotificationID = NotificationID;
    //    }
    //    if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
    //    {
    //        details.PartPlantID = Convert.ToInt32(ddlPlant.SelectedValue);
    //    }
    //    if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
    //    {
    //        details.Workshops = ddlWorkshop.SelectedItem.Text;
    //    }
    //    if (!string.IsNullOrEmpty(ddlSegment.SelectedValue))
    //    {
    //        details.Segments = ddlSegment.SelectedItem.Text;
    //    }
    //    return details;
    //}

    private void BindData()
    {
        //BindPlants(ddlPlant);
        BindNotification();
        BindDetails();
        BindRemovedDetails();

    }

    private void BindNotification()
    {
        StocktakeNotification noti;
        if (NotificationID != null)
        {
            noti = Service.GetNotification(new StocktakeNotification { NotificationID = NotificationID.Value });
            //if (noti.Plant!=null)
            //{
            //    ddlPlant.SelectedValue = noti.Plant.PlantID.ToString();
            //    int index = ddlPlant.SelectedIndex;
            //    for (int i = ddlPlant.Items.Count - 1; i >= 0; i--)
            //    {
            //        if (i!=index)
            //        {
            //            ddlPlant.Items.RemoveAt(i); 
            //        }
            //    }
            //}
            //RadioButtonList rblIsStatic = dlNotification.Items[0].FindControl("rblIsStatic") as RadioButtonList;
            //ListItem item = rblIsStatic.Items.FindByValue((!noti.IsStatic).ToString());
            //if (item != null)
            //{
            //    rblIsStatic.Items.Remove(item);
            //}
            //if (noti.IsStatic.Value)
            //{
            //    rblIsStatic.Items.RemoveAt(0);
            //}
            //else
            //{
            //    rblIsStatic.Items.RemoveAt(1);
            //}
        }
        else
        {
            noti = new StocktakeNotification { Creator = CurrentUser.UserInfo, DateCreated = DateTime.Now };
            if (dlNotification.Items.Count > 0)
            {
                DropDownList ddlPlant = dlNotification.Items[0].FindControl("ddlPlant") as DropDownList;
                RadioButtonList rblIsStatic = dlNotification.Items[0].FindControl("rblIsStatic") as RadioButtonList;
                if (rblIsStatic != null)
                {
                    noti.IsStatic = bool.Parse(rblIsStatic.SelectedValue);
                }
                if (ddlPlant != null && !string.IsNullOrEmpty(ddlPlant.SelectedValue))
                {
                    noti.Plant = new Plant { PlantID = int.Parse(ddlPlant.SelectedValue), PlantCode = ddlPlant.SelectedItem.Text };
                }
            }
        }
        List<StocktakeNotification> notiList = new List<StocktakeNotification> { noti };
        BindDataControl(dlNotification, notiList);
        //ddlPlant_CascadingDropDown.SelectedValue = noti.Plant.PlantID.ToString();
    }

    private void BindRemovedDetails()
    {
        int itemCount;
        int pageCount;
        DropDownList ddlPlant = dlNotification.Items[0].FindControl("ddlPlant") as DropDownList;
        if (NotificationID != null && !string.IsNullOrEmpty(ddlPlant.SelectedValue))
        {
            RadioButtonList rblIsStatic = dlNotification.Items[0].FindControl("rblIsStatic") as RadioButtonList;
            List<View_StocktakeDetails> details = Service.GetNewRequestDetailsByPlant(IncludedDetails, RemovedDetails, rblIsStatic.SelectedIndex == 1, new Plant { PlantID = int.Parse(ddlPlant.SelectedValue) }, pagerRemovedDetails.PageSize, pagerRemovedDetails.CurrentPage, out pageCount, out itemCount);
            BindDataControl(gvRemovedDetails, details);
            pagerRemovedDetails.TotalRecord = itemCount;
        }
        else
        {
            if (NotificationID == null)
            {
                itemCount = RemovedDetails.Count();
                pagerRemovedDetails.TotalRecord = itemCount;
                int pageSize = pagerRemovedDetails.PageSize;
                int pageNumber = pagerRemovedDetails.CurrentPage;
                var qry = RemovedDetails.AsQueryable().OrderBy(d => d.PartCode);
                List<View_StocktakeDetails> list = SGM.Common.Utility.Utils.GetQueryByPage(qry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
                pagerRemovedDetails.TotalRecord = itemCount;
                BindDataControl(gvRemovedDetails, list);
            }
        }
    }

    private void BindDetails()
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeDetails> details;
        if (NotificationID == null)//create
        {
            RadioButtonList rblIsStatic = dlNotification.Items[0].FindControl("rblIsStatic") as RadioButtonList;
            bool isStatic = rblIsStatic.SelectedIndex == 1;
            DropDownList ddlPlant = dlNotification.Items[0].FindControl("ddlPlant") as DropDownList;
            if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
            {
                Plant plant = new Plant { PlantID = int.Parse(ddlPlant.SelectedItem.Value) };
                details = Service.GetNewRequestDetailsByPlant(RemovedDetails, null, isStatic, plant, pagerDetails.PageSize, pagerDetails.CurrentPage, out pageCount, out itemCount);
            }
            else
            {
                details = new List<View_StocktakeDetails>();
                itemCount = 0;
            }
        }
        else
        {
            details = Service.GetNotiDetailsByPage(new StocktakeNotification { NotificationID = NotificationID.Value }, RemovedDetails, IncludedDetails, pagerDetails.PageSize, pagerDetails.CurrentPage, out pageCount, out itemCount);

        }

        BindDataControl(gvDetails, details);
        pagerDetails.TotalRecord = itemCount;

    }


    protected void gvRemovedDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            View_StocktakeDetails detail = e.Row.DataItem as View_StocktakeDetails;
            CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            if (SelectedRemovedDetails.Exists(d => d.DetailsID == detail.DetailsID))
            {
                cbSelect.Checked = true;
            }
            else
            {
                cbSelect.Checked = false;

            }
        }
    }
    protected void gv_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> details = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView((GridView)sender, details);
    }


    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearCache();
        BindData();
        //if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
        //{
        //    Plant plant = new Plant { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
        //    BindWorkshops(ddlWorkshop, plant, true);
        //    ddlWorkshop.SelectedIndex = 0;
        //}
        //else
        //{
        //    ddlWorkshop.Items.Clear();
        //    ddlSegment.Items.Clear();
        //}

    }

    protected void rblIsStatic_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearCache();
        BindData();
    }


    //protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(ddlWorkshop.SelectedValue))
    //    {
    //        Workshop workshop = new Workshop { WorkshopID = Convert.ToInt32(ddlWorkshop.SelectedValue) };
    //        BindSegments(ddlSegment, workshop, true);
    //        ddlSegment.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //        ddlSegment.Items.Clear();
    //    }
    //}   

    protected void dlNotification_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
            DropDownList ddlPlant = e.Item.FindControl("ddlPlant") as DropDownList;
            BindPlants(ddlPlant);
            StocktakeNotification noti = e.Item.DataItem as StocktakeNotification;

            if (noti != null)
            {
                if (noti.IsStatic != null)
                {
                    RadioButtonList rblIsStatic = e.Item.FindControl("rblIsStatic") as RadioButtonList;
                    rblIsStatic.SelectedIndex = (noti.IsStatic.Value) ? 1 : 0;
                    if (NotificationID != null)
                    {
                        for (int i = rblIsStatic.Items.Count - 1; i >= 0; i--)
                        {
                            if (rblIsStatic.Items[i].Selected == false)
                            {
                                rblIsStatic.Items.RemoveAt(i);
                            }

                        }
                    }
                }
                if (noti.Plant != null)
                {
                    ddlPlant.SelectedValue = noti.Plant.PlantID.ToString();
                    if (NotificationID != null)
                    {
                        int index = ddlPlant.SelectedIndex;
                        for (int i = ddlPlant.Items.Count - 1; i >= 0; i--)
                        {
                            if (i != index)
                            {
                                ddlPlant.Items.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }
    }
}
