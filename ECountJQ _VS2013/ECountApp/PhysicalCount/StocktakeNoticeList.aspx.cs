using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SGM.Common.Utility;
using System.Text;

public partial class PhysicalCount_StocktakeNoticeList : ECountBasePage
{
    public string View
    {
        get
        {
            if (Request.QueryString["view"] == null)
            {
                return "notification";
            }
            return Request.QueryString["view"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        pagerDetails.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerDetails_PageNumberSelect);
        pagerNotiList.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerNotiList_PageNumberSelect);
        if (!IsPostBack)
        {
            if (View == "Result")
            {
                gvNotification.Columns[0].Visible = false;
                gvDetails.Columns[0].Visible = false;
            }
            BindData();

            gvDetails.Columns[1].Visible = (View == "notification");
            gvDetails.Columns[2].Visible = !gvDetails.Columns[1].Visible;

            gvNotification.Columns[1].Visible = gvDetails.Columns[1].Visible;
            gvNotification.Columns[2].Visible = gvDetails.Columns[2].Visible;

            Toolbar1.Items[0].Visible = Toolbar1.Items[1].Visible = Toolbar1.Items[2].Visible  = (View == "notification");
            mvQuery.ActiveViewIndex = (View == "notification") ? 0 : 1;
        }
    }

    private void BindData()
    {
        List<StocktakeStatus> statusList = Service.GetStocktakeStatus();
        statusList = statusList.Where(s => s.StatusID >= Consts.STOCKTAKE_NEW_NOTIFICATION).ToList();
        if (View == "notification")
        {
            BindDataControl(ddlStatus, statusList);
        }
        else
        {
            BindDataControl(ddlStatus_Result, statusList);
            ddlStatus_Result.Items.Insert(0, new ListItem(Consts.DROPDOWN_UNSELECTED_TEXT,string.Empty));
            BindStoreLocation(ddlStoreLocation);
            ddlStoreLocation.Items.Insert(0, new ListItem(Consts.DROPDOWN_UNSELECTED_TEXT,string.Empty));
        }
    }

    void pagerNotiList_PageNumberSelect(object sender, EventArgs e)
    {
        View_StocktakeDetails condition;
        DateTime? dateStart;
        DateTime? dateEnd;
        DateTime? planDateStart;
        DateTime? planDateEnd;
        int? locationID;
        BuildQueryCondition(out condition, out dateStart, out dateEnd, out planDateStart, out planDateEnd, out locationID);
        QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd, locationID);
    }

    void pagerDetails_PageNumberSelect(object sender, EventArgs e)
    {
        View_StocktakeDetails condition;
        DateTime? dateStart;
        DateTime? dateEnd;
        DateTime? planDateStart;
        DateTime? planDateEnd;
        int? locationID;
        BuildQueryCondition(out condition, out dateStart, out dateEnd, out planDateStart, out planDateEnd, out locationID);
        QueryDetails(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd);
    }

    private void Export(List<string> expNotificationList)
    {
        if (expNotificationList==null||expNotificationList.Count==0)
        {
            return;
        }
        string errorMessage;
        foreach (var item in expNotificationList)
        {
            string notificationID = item;
        }
        string notiCode;
        byte[] buffer = Service.ExportNotification(expNotificationList, out errorMessage,out notiCode);

        if (string.IsNullOrEmpty(errorMessage) && buffer.Length > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";

            //string fileName = DateTime.Now.ToString("yyyyMMdd") + HttpUtility.UrlEncode("PDSQ_(周五)") + ".xls";
            string fileName = notiCode + HttpUtility.UrlEncode("盘点通知单明细.xls");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
            Response.OutputStream.Write(buffer, 0, buffer.Length);
            Response.Flush();
            Response.End();
        }

        //Service.ExportNotification(delNotificationList);
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "query":
                pagerNotiList.CurrentPage = 1;
                pagerDetails.CurrentPage = 1;
                Query();
                break;
            case "delete":
                Delete();                
                Query();
                break;
            case "export":
                Export(GetSelectedItems());
                break;
            default:
                break;
        }
    }

    private void CancelPublish()
    {
        List<string> delNotificationList = GetSelectedItems();
        Service.CancelPublish(delNotificationList);
    }

    private void Delete()
    {
        List<string> delNotificationList = GetSelectedItems();

        Service.DeleteNotification(delNotificationList);
    }

    private List<string> GetSelectedItems()
    {
        List<string> delNotificationList = new List<string>();

        for (int i = 0; i < gvNotification.Rows.Count; i++)
        {
            if (gvNotification.Rows[i].Visible)
            {
                CheckBox cbNotiSelect = gvNotification.Rows[i].Cells[0].FindControl("cbNotiSelect") as CheckBox;
                if (cbNotiSelect.Checked)
                {
                    string notificationID = gvNotification.DataKeys[i].Value.ToString();
                    if (!delNotificationList.Contains(notificationID))
                    {
                        delNotificationList.Add(notificationID);
                    }
                }
            }
        }
        for (int i = 0; i < gvDetails.Rows.Count; i++)
        {

            if (gvDetails.Rows[i].Visible)
            {
                CheckBox cbDetailsSelect = gvDetails.Rows[i].Cells[0].FindControl("cbDetailsSelect") as CheckBox;
                if (cbDetailsSelect.Checked)
                {
                    string notificationID = gvDetails.Rows[i].Cells[12].Text;
                    if (!delNotificationList.Contains(notificationID))
                    {
                        delNotificationList.Add(notificationID);
                    }
                }
            }
        }
        return delNotificationList;
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            View_StocktakeDetails details = e.Row.DataItem as View_StocktakeDetails;
            if (details.DetailsID == DefaultValue.LONG)
            {
                return;
            }
            LinkButton linkNotificationNo = e.Row.Cells[3].FindControl("linkNotificationNo") as LinkButton;

            if (View == "notification")
            {
                LinkButton linkModify = e.Row.Cells[1].FindControl("linkModify") as LinkButton;
                LinkButton linkPublish = e.Row.Cells[1].FindControl("linkPublish") as LinkButton;
                LinkButton linkExportNoti = e.Row.Cells[1].FindControl("linkExportNoti") as LinkButton;
                LinkButton linkExportNotiDetails = e.Row.Cells[1].FindControl("linkExportNotiDetails") as LinkButton;
                
                SetButtonByStatus(linkPublish, linkModify, linkNotificationNo,linkExportNoti,linkExportNotiDetails, details);
            }
            else
            {
                LinkButton linkFill = e.Row.FindControl("linkFill") as LinkButton;
                LinkButton linkImport = e.Row.FindControl("linkImport") as LinkButton;
                LinkButton linkExport = e.Row.FindControl("linkExport") as LinkButton;
                SetResultByStatus(linkFill, linkImport, linkNotificationNo, linkExport,details.Status.Value, details.NotificationID.Value);
            }
        }
    }

    void SetButtonByStatus(LinkButton linkPublish, LinkButton linkModify, LinkButton linkView, LinkButton linkExportNoti, LinkButton linkExportNotiDetails, View_StocktakeNotification noti)// bool published, int status, long notificatioID)
    {
        if (!noti.Published.Value)//not published yet
        {
            string viewScript = string.Format("showDialog('StocktakeNotice.aspx?Mode=View&id={0}',950,550, null, \"refresh('{1}')\");return false;", noti.NotificationID, Toolbar1.Controls[3].ClientID);
        linkView.OnClientClick = viewScript;
        linkView.Style[HtmlTextWriterStyle.Cursor] = "hand";

            linkModify.Enabled = true;
            linkPublish.Enabled = true;
            string modifyScript = string.Format("showDialog('StocktakeNotice.aspx?Mode=Edit&id={0}',950,550, null, \"refresh('{1}')\");return false;", noti.NotificationID, Toolbar1.Controls[3].ClientID);
            linkModify.OnClientClick = modifyScript;
            linkModify.Style[HtmlTextWriterStyle.Cursor] = "hand";

            linkPublish.Text = "发布";
            linkPublish.CommandName = "publish";
            linkPublish.CommandArgument = noti.NotificationID.ToString();
            string deployScript = string.Format("showDialog('NotificationPublish.aspx?id={0}&Mode=Edit',950,550, null, \"refresh('{1}')\");return false;", noti.NotificationID,Toolbar1.Controls[3].ClientID);
            linkPublish.OnClientClick = deployScript;
            linkPublish.Style[HtmlTextWriterStyle.Cursor] = "hand";
            linkExportNoti.Enabled = false; 
            linkExportNotiDetails.Enabled = true;
        }
        else//published
        {

            string viewScript = string.Format("showDialog('NotificationPublish.aspx?Mode=View&id={0}',950,550, null, \"refresh('{1}')\");return false;", noti.NotificationID, Toolbar1.Controls[3].ClientID);
            linkView.OnClientClick = viewScript;
            linkView.Style[HtmlTextWriterStyle.Cursor] = "hand";

            linkModify.Enabled = false;
            if (noti.Status.Value == Consts.STOCKTAKE_PUBLISHED)//just published
            {
                linkPublish.Enabled = false;                

            }
            else// notification has been filled
            {
                linkPublish.Enabled = false;
            }
            linkExportNoti.Enabled = true;
            linkExportNotiDetails.Enabled = true;
        }
    }


    void SetButtonByStatus(LinkButton linkPublish, LinkButton linkModify, LinkButton linkView, LinkButton linkExportNoti,LinkButton linkExportNotiDetails, View_StocktakeDetails noti)
    {
        SetButtonByStatus(linkPublish, linkModify, linkView,linkExportNoti,linkExportNotiDetails, new View_StocktakeNotification { NotificationID = noti.NotificationID.Value, Status = noti.Status, Published = noti.Published });        
    }

    void SetResultByStatus(LinkButton linkFill, LinkButton linkImport, LinkButton linkView,LinkButton linkExport, int status, long notificatioID)
    {
        string viewScript = "return false;";
        if (status >= Consts.STOCKTAKE_PUBLISHED)
        {
            viewScript = string.Format("showDialog('StocktakeResult.aspx?Mode=View&id={0}',1080,500, null, \"refresh('{1}')\");return false;", notificatioID, Toolbar1.Controls[3].ClientID);
            
        }
        linkView.OnClientClick = viewScript;
        linkView.Style[HtmlTextWriterStyle.Cursor] = "hand";

        if (status >= Consts.STOCKTAKE_PUBLISHED &&status<=Consts.STOCKTAKE_COMPLETE)// not analyzing yet
        {
            linkFill.Enabled = true;
            string fillScript = string.Format("showDialog('StocktakeResult.aspx?Mode=Edit&id={0}',1080,500, null, \"refresh('{1}')\");return false;", notificatioID, Toolbar1.Controls[3].ClientID);
            linkFill.OnClientClick = fillScript;
            linkFill.Style[HtmlTextWriterStyle.Cursor] = "hand";

            linkImport.Enabled = true;
            //string importScript = string.Format("showDialog('StocktakeResultImport.aspx?id={0}',800,550, null, \"refresh('{1}')\");return false;", notificatioID, Toolbar1.Controls[3].ClientID);
            //linkImport.OnClientClick = importScript;
            linkImport.Style[HtmlTextWriterStyle.Cursor] = "hand";

            linkExport.Enabled = linkFill.Enabled = linkImport.Enabled = true;
            linkFill.Style[HtmlTextWriterStyle.Cursor]=linkImport.Style[HtmlTextWriterStyle.Cursor]=
            linkFill.Style[HtmlTextWriterStyle.Cursor] = linkExport.Style[HtmlTextWriterStyle.Cursor] = "hand";
        }
        else
        {
            linkImport.Enabled = linkExport.Enabled = linkFill.Enabled = false;
        }
    }

    protected void gvNotification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            View_StocktakeNotification noti = e.Row.DataItem as View_StocktakeNotification;
            if (noti.NotificationID == 0)
            {
                return;
            }
            LinkButton linkNotificationNo = e.Row.Cells[3].FindControl("linkNotificationNo") as LinkButton;
            if (View == "notification")
            {
                LinkButton linkPublish = e.Row.Cells[1].FindControl("linkPublish") as LinkButton;
                LinkButton linkModify = e.Row.Cells[1].FindControl("linkModify") as LinkButton;
                LinkButton linkExportNoti = e.Row.Cells[1].FindControl("linkExportNoti") as LinkButton;
                LinkButton linkExportNotiDetails = e.Row.Cells[1].FindControl("linkExportNotiDetails") as LinkButton;

                SetButtonByStatus(linkPublish, linkModify, linkNotificationNo, linkExportNoti,linkExportNotiDetails, noti);
            }
            else
            {
                LinkButton linkFill = e.Row.FindControl("linkFill") as LinkButton;
                LinkButton linkImport = e.Row.FindControl("linkImport") as LinkButton;

                LinkButton linkExport = e.Row.FindControl("linkExport") as LinkButton;
                SetResultByStatus(linkFill, linkImport, linkNotificationNo, linkExport,noti.Status.Value, noti.NotificationID);
            }
            //if (!noti.Published.Value)//notification is not published yet
            //{
            //    string modifyScript = string.Format("showDialog('StocktakeNotice.aspx?Mode=Edit&id={0}',950,550);return false;", noti.NotificationID);
            //    linkModify.OnClientClick = modifyScript;
            //    linkModify.Style[HtmlTextWriterStyle.Cursor] = "hand";
            //    string publishScript = string.Format("showDialog('NotificationPublish.aspx?id={0}',950,550);return false;", noti.NotificationID);
            //    linkPublish.OnClientClick = publishScript;
            //    linkPublish.Style[HtmlTextWriterStyle.Cursor] = "hand";
            //}
            //else
            //{
            //    linkPublish.Enabled = false;
            //    linkModify.Enabled = false;
            //}

        }

    }

    protected void gvDetails_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> list = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvDetails, list);
    }

    protected void gvNotification_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeNotification> list = new List<View_StocktakeNotification> { new View_StocktakeNotification() };
        BindEmptyGridView(gvNotification, list);
    }

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":

                break;
            case "Publish":
                break;
            default:
                break;
        }
    }


    protected void cbSelect_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void Query()
    {
        View_StocktakeDetails condition;
        DateTime? dateStart;
        DateTime? dateEnd;
        DateTime? planDateStart;
        DateTime? planDateEnd;
        int? locationID;
        BuildQueryCondition(out condition, out dateStart, out dateEnd, out planDateStart, out planDateEnd, out locationID);
        if (View == "notification" && cblSearchOption.Items[0].Selected || View == "Result" && cbOption_Result.Items[0].Selected)
        {
            QueryNotification(condition, dateStart, dateEnd, planDateStart, planDateEnd, locationID);
        }
        else
        {
            gvNotification.DataBind();
            pagerNotiList.TotalRecord = 0;
        }
        if (View == "notification" && cblSearchOption.Items[1].Selected || View == "Result" && cbOption_Result.Items[1].Selected)
        {
            QueryDetails(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd);
        }
        else
        {
            gvDetails.DataBind();
            pagerDetails.TotalRecord = 0;
        }

        if (View == "notification" && !cblSearchOption.Items[tcNotification.ActiveTabIndex].Selected)
        {
            tcNotification.ActiveTabIndex = (cblSearchOption.SelectedIndex <= 0) ? 0 : 1;
        }

        if (View == "Result" && !cbOption_Result.Items[tcNotification.ActiveTabIndex].Selected)
        {
            tcNotification.ActiveTabIndex = (cbOption_Result.SelectedIndex <= 0) ? 0 : 1;
        }
    }

    private void QueryDetails(View_StocktakeDetails condition, int? locationID, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd)
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeDetails> detailsList = Service.QueryNotifyDetailsByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pagerDetails.PageSize, pagerDetails.CurrentPage, out pageCount, out itemCount);
        gvDetails.DataSource = detailsList;
        gvDetails.DataBind();
        pagerDetails.TotalRecord = itemCount;
    }

    private void QueryNotification(View_StocktakeDetails condition, DateTime? dateStart, DateTime? dateEnd, DateTime? planDateStart, DateTime? planDateEnd, int? locationID)
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeNotification> notificationList = Service.QueryNotiByPage(condition, locationID, dateStart, dateEnd, planDateStart, planDateEnd, pagerNotiList.PageSize, pagerNotiList.CurrentPage, out pageCount, out itemCount);
        gvNotification.DataSource = notificationList;
        gvNotification.DataBind();
        pagerNotiList.TotalRecord = itemCount;
    }

    private void BuildQueryCondition(out View_StocktakeDetails condition, out DateTime? dateStart, out DateTime? dateEnd, out DateTime? planDateStart, out DateTime? planDateEnd, out int? locationID)
    {
        condition = new View_StocktakeDetails();
        locationID = null;

        if (View == "notification")
        {
            if (!string.IsNullOrEmpty(txtCreateBy.Text.Trim()))
            {
                condition.RequestUser = txtCreateBy.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPartCName.Text.Trim()))
            {
                condition.PartChineseName = txtPartCName.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtRequestNO.Text.Trim()))
            {
                condition.RequestNumber = txtRequestNO.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtNotificationNO.Text.Trim()))
            {
                condition.NotificationCode = txtNotificationNO.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtPartNO.Text.Trim()))
            {
                condition.PartCode = txtPartNO.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtUserGroup.Text.Trim()))
            {
                condition.RequestUserGroup = txtUserGroup.Text.Trim();
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                condition.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            }

            dateStart = null;
            DateTime tmpDate;
            if (DateTime.TryParse(txtDateStart.Text.Trim(), out tmpDate))
            {
                dateStart = tmpDate;
            }
            dateEnd = null;
            if (DateTime.TryParse(txtDateEnd.Text.Trim(), out tmpDate))
            {
                dateEnd = tmpDate;
            }

            planDateStart = null;
            if (DateTime.TryParse(txtPlanDateStart.Text.Trim(), out tmpDate))
            {
                planDateStart = tmpDate;
            }
            planDateEnd = null;
            if (DateTime.TryParse(txtPlanDateEnd.Text.Trim(), out tmpDate))
            {
                planDateEnd = tmpDate;
            }
        }
        else
        {
            condition.Published = true;
            if (!string.IsNullOrEmpty(txtNotiNo_Result.Text))
            {
                condition.NotificationCode = txtNotiNo_Result.Text;
            }
            if (!string.IsNullOrEmpty(txtPartNo_Result.Text))
            {
                condition.PartCode = txtPartNo_Result.Text;
            }
            if (!string.IsNullOrEmpty(txtReqNo_Result.Text))
            {
                condition.RequestNumber = txtReqNo_Result.Text;
            }

            if (ddlStatus_Result.SelectedIndex > 0)
            {
                condition.Status = Convert.ToInt32(ddlStatus_Result.SelectedValue);
            }

            if (!string.IsNullOrEmpty(txtPartCName.Text))
            {
                condition.PartChineseName = txtPartCName.Text;
            }

            if (!string.IsNullOrEmpty(txtRequestBy_Result.Text))
            {
                condition.RequestUser = txtRequestBy_Result.Text;
            }
            if (!string.IsNullOrEmpty(ddlStoreLocation.SelectedValue))
            {
                locationID = Convert.ToInt32(ddlStoreLocation.SelectedValue);
            }
            //if (ddlStoreLocation.SelectedIndex > 0)
            //{
            //    condition.LocationID = Convert.ToInt32(ddlStoreLocation.SelectedValue);
            //}
            dateStart = null;
            DateTime tmpDate;
            if (DateTime.TryParse(txtDateStart_Result.Text.Trim(), out tmpDate))
            {
                dateStart = tmpDate;
            }
            dateEnd = null;
            if (DateTime.TryParse(txtDateEnd_Result.Text.Trim(), out tmpDate))
            {
                dateEnd = tmpDate;
            }

            planDateStart = null;
            if (DateTime.TryParse(txtPlanDateStart_Result.Text.Trim(), out tmpDate))
            {
                planDateStart = tmpDate;
            }
            planDateEnd = null;
            if (DateTime.TryParse(txtPlanDateEnd_Result.Text.Trim(), out tmpDate))
            {
                planDateEnd = tmpDate;
            }

        }
    }
    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void linkPublish_Click(object sender, EventArgs e)
    {
        LinkButton linkPublish = sender as LinkButton;
        //if (linkPublish.CommandName == "unPublish")
        //{
        //    string notificatioID = linkPublish.CommandArgument;
        //    Service.CancelPublish(new List<string> { notificatioID });
        //    Query();
        //}
    }

    protected void linkDetailPublish_Click(object sender, EventArgs e)
    {

        LinkButton linkPublish = sender as LinkButton;
        //if (linkPublish.CommandName == "unPublish")
        //{
        //    string notificatioID = linkPublish.CommandArgument;
        //    Service.CancelPublish(new List<string> { notificatioID });
        //    Query();
        //}
    }
    protected void linkFill_Click(object sender, EventArgs e)
    {
        //LinkButton linkFill = sender as LinkButton;
        //GridViewRow row = linkFill.NamingContainer as GridViewRow;
        //GridView gv = row.NamingContainer as GridView;
        //string notiID = gv.DataKeys[row.RowIndex].Value.ToString();
        //Response.Redirect(string.Format("StocktakeResult.aspx?Mode=Edit&id={0}", notiID));

    }
    protected void linkImport_Click(object sender, EventArgs e)
    {
        LinkButton linkImport = sender as LinkButton;
        GridViewRow row = linkImport.NamingContainer as GridViewRow;
        GridView gv = row.NamingContainer as GridView;
        string notiID = gv.DataKeys[row.RowIndex].Value.ToString();
        Response.Redirect(string.Format("StocktakeResultImport.aspx?id={0}", notiID));
    }
    protected void linkExport_Click(object sender, EventArgs e)
    {
        LinkButton linkExport = sender as LinkButton;
        GridViewRow row = linkExport.NamingContainer as GridViewRow;
        GridView gv = row.NamingContainer as GridView;
        string notiID = gv.DataKeys[row.RowIndex].Value.ToString();
        byte[] content = Service.ExportStocktakeResult(new StocktakeNotification { NotificationID = long.Parse(notiID) },CurrentUser.UserInfo);
        if (content!=null)
        {
            
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            
            string fileName =DateTime.Now.ToString("yyyyMMdd")+ HttpUtility.UrlEncode("实盘结果(")+DateTime.Now.DayOfWeek+").xls";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
            Response.OutputStream.Write(content, 0, content.Length);
            Response.Flush();  
        }
    }
    protected void linkExportNoti_Click(object sender, EventArgs e)
    {
        LinkButton linkExport = sender as LinkButton;
        GridViewRow row = linkExport.NamingContainer as GridViewRow;
        CheckBox cbNotiSelect = row.FindControl("cbNotiSelect") as CheckBox;
        if (cbNotiSelect!=null)
        {
            cbNotiSelect.Checked = true;
        }
        ExportNoti(sender);
        
    }


    protected void linkExportNotiDetails_Click(object sender, EventArgs e)
    {
        LinkButton linkExport = sender as LinkButton;
        GridViewRow row = linkExport.NamingContainer as GridViewRow;
        CheckBox cbNotiSelect = row.FindControl("cbNotiSelect") as CheckBox;
        if (cbNotiSelect != null)
        {
            cbNotiSelect.Checked = true;
        }
        Export(GetSelectedItems());

    }

    private void ExportNoti(object sender)
    {
        LinkButton linkExport = sender as LinkButton;
        GridViewRow row = linkExport.NamingContainer as GridViewRow;
        GridView gv = row.NamingContainer as GridView;
        string notiID = gv.DataKeys[row.RowIndex].Value.ToString();
        string notiCode;
        byte[] content = Service.ExportStocktakeNotice(new StocktakeNotification { NotificationID = long.Parse(notiID) }, CurrentUser.UserInfo, out  notiCode);
        if (content != null)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";

            string fileName = notiCode + HttpUtility.UrlEncode("盘点通知单.xls");
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
            Response.OutputStream.Write(content, 0, content.Length);
            Response.Flush();
        }
    }
}
