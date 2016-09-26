using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using SGM.Common.Utility;
using SGM.Common.Cache;

public partial class PhysicalCount_StocktakeRequest : ECountBasePage
{

    protected List<View_StocktakeDetails> SelectedReqDetails
    {
        get
        {
            if (Session["Req_SelectedReqDetails"] == null)
            {
                Session["Req_SelectedReqDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Req_SelectedReqDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Req_SelectedReqDetails"] = value;
        }
    }

    protected List<View_StocktakeDetails> SelectedDetails
    {
        get
        {
            if (Session["Req_SelectedDetails"] == null)
            {
                Session["Req_SelectedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Req_SelectedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Req_SelectedDetails"] = value;
        }
    }

    protected List<View_StocktakeDetails> RemovedDetails
    {
        get
        {
            if (Session["Req_RemovedDetails"] == null)
            {
                Session["Req_RemovedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["Req_RemovedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Req_RemovedDetails"] = value;
        }
    }

    protected List<View_StocktakeDetails> RequestedDetails
    {
        get
        {
            if (Session["Req_Details"] == null)
            {
                Session["Req_Details"] = new List<View_StocktakeDetails>();
            }
            return Session["Req_Details"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["Req_Details"] = value;
        }
    }

    public long? RequestID
    {
        get
        {
            return ViewState["RequestID"] as long?;
        }
        set
        {
            ViewState["RequestID"] = value;
        }
    }
    public int CurrentDynAmount
    {
        get
        {
            int currentDynAmount = 0;
            foreach (var item in UserGroups)
            {
                currentDynAmount += item.CurrentDynamicStocktake ?? 0;
            }
            return currentDynAmount;
        }
    }

    public int CurrentStAmount
    {
        get
        {
            int currentStAmount = 0;
            foreach (var item in UserGroups)
            {
                currentStAmount += item.CurrentStaticStocktake ?? 0;
            }
            return currentStAmount;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearCache();

            if (Request.QueryString["id"] != null)
            {
                RequestID = Convert.ToInt64(Request.QueryString["id"]);
                //BindData();
                BindRequest(null);
                BindRequestDetails();
            }
            else
            {
                BindRequest(null);
            }
            BindPlant();
        }
        pagerParts.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerParts_PageNumberSelect);
        pagerRequestParts.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerRequestParts_PageNumberSelect);
    }

    private void ClearCache()
    {
        RemovedDetails = null;
        RequestedDetails = null;
        SelectedDetails = null;
        SelectedReqDetails = null;
    }
    void pagerRequestParts_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedReqDetails, gvRequestParts);
        //RefreshDetails();
        RefreshRequestedDetails();
        BindRequestDetails();
    }

    void pagerParts_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedDetails, gvParts);
        BindDetails();
        //RefreshNewRequestDetails();
        //Query();
    }


    private void BindRequest(StocktakeRequest request)
    {
        if (RequestID != null)
        {
            if (request == null)
            {
                request = Service.GetRequest(RequestID.Value);
            }
            List<StocktakeRequest> reqList = new List<StocktakeRequest>();
            if (request != null)
            {
                reqList.Add(request);
            }
            this.BindDataControl(this.dlRequest, reqList);
        }
        else
        {
            if (!pagerParts.IsPostBack)
            {
                List<StocktakeRequest> reqList = new List<StocktakeRequest>
            {
                new StocktakeRequest
                {
                    RequestBy=CurrentUser.UserInfo,
                    DateRequest=DateTime.Now,
                    IsStatic=false
                }
            };
                this.BindDataControl(this.dlRequest, reqList);
            }
        }
        multiViewRequest.ActiveViewIndex = 0;
    }
    private void BindRequestDetails()
    {
        int pageCount;
        int itemCount;
        List<View_StocktakeDetails> details;
        if (RequestID == null)//create
        {
            itemCount = RequestedDetails.Count();
            pagerRequestParts.TotalRecord = itemCount;
            int pageSize = pagerRequestParts.PageSize;
            int pageNumber = pagerRequestParts.CurrentPage;
            var qry = RequestedDetails.AsQueryable().OrderBy(d => d.PartCode);
            details = SGM.Common.Utility.Utils.GetQueryByPage(qry, pageSize, pageNumber, out pageCount, out itemCount).ToList();
        }
        else
        {
            View_StocktakeDetails condition = new View_StocktakeDetails
            {
                RequestID = RequestID.Value
            };
            string cacheKey = string.Empty;
            details = Service.QueryRequestDetailsByPage(condition, RemovedDetails, RequestedDetails, null, null, pagerRequestParts.PageSize, pagerRequestParts.CurrentPage, out pageCount, out itemCount); //Service.GetNotiDetailsByPage(new StocktakeNotification { NotificationID = NotificationID.Value }, RemovedDetails, IncludedDetails, pagerDetails.PageSize, pagerDetails.CurrentPage, out pageCount, out itemCount);
        }
        BindDataControl(gvRequestParts, details);
        pagerRequestParts.TotalRecord = itemCount;

    }

    private void BindDetails()
    {
        int pageCount;
        int itemCount;
        View_StocktakeDetails filter = new View_StocktakeDetails();
        filter.PartCode = txtPartCode.Text.Trim();
        filter.PartChineseName = txtPartName.Text.Trim();
        filter.Specs = txtBOOK.Text.Trim();
        filter.FollowUp = txtFollowUp.Text.Trim();
        if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
        {
            filter.PlantID = int.Parse(ddlPlant.SelectedValue);
        }
        if (CurrentUser.UserInfo.Workshop != null && !string.IsNullOrEmpty(CurrentUser.UserInfo.Workshop.WorkshopCode))
        {
            filter.Workshops = CurrentUser.UserInfo.Workshop.WorkshopCode;
        }
        List<ViewPart> requestedParts = new List<ViewPart>();
        if (RequestID == null)
        {
            requestedParts = (from d in RequestedDetails select new ViewPart { PartID = d.PartID.Value }).ToList();
        }
        else
        {
            List<ViewPart> partList = new List<ViewPart>();
            foreach (var item in RequestedDetails)
            {
                requestedParts.Add(new ViewPart { PartID = item.PartID.Value });

            }
            foreach (var item in RemovedDetails)
            {
                if (!requestedParts.Exists(p => p.PartID == item.PartID))
                {
                    requestedParts.Add(new ViewPart { PartID = item.PartID.Value });
                }
            }
        }
        filter.RequestID = RequestID;
        Part part = new Part { PartCode = filter.PartCode, PartChineseName = filter.PartChineseName, Specs = filter.Specs, FollowUp = filter.FollowUp };
        if (filter.PlantID != null)
        {
            part.Plant = new Plant { PlantID = filter.PlantID.Value };
        }
        if (!string.IsNullOrEmpty(filter.Workshops))
        {
            part.Workshops = filter.Workshops;
        }

        StocktakeRequest stocktakeReq = null;
        if (RequestID != null)
        {
            stocktakeReq = new StocktakeRequest { RequestID = RequestID.Value };
        }
        List<ViewPart> parts = Service.QueryUnRequestedPartByPage(stocktakeReq, part, pagerParts.PageSize, pagerParts.CurrentPage, out pageCount, out itemCount);//.QueryPartByPage(part, pagerParts.PageSize, pagerParts.CurrentPage, out pageCount, out itemCount);//.QueryFilteredPartsByPage(filter, requestedParts.ToList(), pagerParts.PageSize, pagerParts.CurrentPage, out pageCount, out itemCount);

        BindDataControl(gvParts, parts);
        pagerParts.TotalRecord = itemCount;

    }

    private void BindData()
    {
        BindRequest(null);
        BindDetails();
        BindRequestDetails();
    }

    private void RefreshSelectedDetails(List<View_StocktakeDetails> details, GridView gv)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            if (gv.DataKeys[row.RowIndex]["PartID"] == null)
            {
                continue;
            }
            CheckBox cbSelect = row.FindControl("cbSelect") as CheckBox;
            View_StocktakeDetails detail = GetDetailsByRow(row);
            detail.PartID = int.Parse(gv.DataKeys[row.RowIndex]["PartID"].ToString());
            int index = details.FindIndex(d => d.PartID == detail.PartID);
            if (index >= 0)
            {
                if (!cbSelect.Checked)
                {
                    details.RemoveAt(index);
                }
            }
            else
            {
                if (cbSelect.Checked)
                {
                    details.Add(detail);
                }
            }
        }
    }

    private View_StocktakeDetails GetDetailsByRow(GridViewRow row)
    {
        View_StocktakeDetails details = new View_StocktakeDetails();
        Label lblPartNo = row.FindControl("lblPartNo") as Label;
        Label lblPlantCode = row.FindControl("lblPlantCode") as Label;
        Label lblCName = row.FindControl("lblCName") as Label;
        Label lblEName = row.FindControl("lblEName") as Label;
        Label lblDUNS = row.FindControl("lblDUNS") as Label;

        TextBox txtComments = row.FindControl("txtComments") as TextBox;

        DropDownList ddlPriority = row.FindControl("ddlPriority") as DropDownList;
        if (ddlPriority != null && !string.IsNullOrEmpty(ddlPriority.SelectedValue))
        {
            details.Priority = int.Parse(ddlPriority.SelectedValue);
        }
        DropDownList ddlStocktakeType = row.FindControl("ddlStocktakeType") as DropDownList;
        if (ddlStocktakeType != null && !string.IsNullOrEmpty(ddlStocktakeType.SelectedValue))
        {
            details.StocktakeType = int.Parse(ddlStocktakeType.SelectedValue);
        }
        if (txtComments != null)
        {
            //details.NotifyComments = txtComments.Text.Trim();
            details.DetailsDesc = txtComments.Text.Trim();
        }
        Label lblFollowup = row.FindControl("lblFollowup") as Label;
        details.FollowUp = lblFollowup.Text;
        Label lblPartCategory = row.FindControl("lblPartCategory") as Label;
        details.CategoryName = lblPartCategory.Text;
        Label lblLevel = row.FindControl("lblLevel") as Label;
        details.LevelName = lblLevel.Text;
        Label lblPartStatus = row.FindControl("lblPartStatus") as Label;
        details.PartEnglishName = lblEName.Text;
        details.StatusName = lblPartStatus.Text;
 
        details.DUNS = lblDUNS.Text;
        details.PartChineseName = lblCName.Text;
        details.PartEnglishName = lblCName.Text;
        details.PartCode = lblPartNo.Text;
        details.PartPlantCode = lblPlantCode.Text;
        GridView gv = row.NamingContainer as GridView;
        int colCount = gv.Columns.Count;
        if (!string.IsNullOrEmpty(Server.HtmlDecode(row.Cells[colCount - 1].Text).Trim()))
        {
            details.PreDynamicNoticeCode = Server.HtmlDecode(row.Cells[colCount - 1].Text).Trim();
        }
        DateTime dynTime;
        if (DateTime.TryParse(row.Cells[colCount - 2].Text, out dynTime))
        {

            details.PreDynamicNotiTime = dynTime;
        }
        if (!string.IsNullOrEmpty(Server.HtmlDecode(row.Cells[colCount - 3].Text).Trim()))
        {
            details.PreStaticNoticeCode = Server.HtmlDecode(row.Cells[colCount - 3].Text).Trim();
        }
        DateTime stTime;
        if (DateTime.TryParse(row.Cells[colCount - 4].Text, out stTime))
        {
            details.PreStaticNotiTime = DateTime.Parse(row.Cells[colCount - 4].Text);
        }
        return details;
    }

    private void RefreshRequestedDetails()
    {
        foreach (GridViewRow row in gvRequestParts.Rows)
        {
            if (gvRequestParts.DataKeys[row.RowIndex]["PartID"] == null)
            {
                continue;
            }
            int partID = int.Parse(gvRequestParts.DataKeys[row.RowIndex]["PartID"].ToString());
            if (partID == DefaultValue.INT)
            {
                continue;
            }
            View_StocktakeDetails detail = GetDetailsByRow(row);
            detail.PartID = partID;
            int index = RequestedDetails.FindIndex(d => d.PartID == detail.PartID);
            if (index >= 0)
            {
                RequestedDetails[index].DetailsDesc = detail.DetailsDesc;
                RequestedDetails[index].StocktakeType = detail.StocktakeType;
                RequestedDetails[index].Priority = detail.Priority;
            }
            else
            {
                if (RequestID == null)
                {
                    if (!string.IsNullOrEmpty(detail.DetailsDesc))
                    {
                        RequestedDetails.Add(detail);
                    }
                }
                else
                {
                    RequestedDetails.Add(detail);
                }
            }
        }
    }

    private void BindPlant()
    {
        BindPlants(ddlPlant, true);
        if (CurrentUser.UserInfo.Workshop != null && CurrentUser.UserInfo.Workshop.Plant != null)//filter by user plant
        {
            ddlPlant.Items.FindByValue(CurrentUser.UserInfo.Workshop.Plant.PlantID.ToString()).Selected = true;
            ddlPlant.Enabled = false;
        }
        //else
        //{
        //    ddlPlant.SelectedIndex = 0;
        //}
    }

    protected void btnAddPart_Click(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedDetails, gvParts);
        RefreshRequestedDetails();
        valiCounts.Validate();
        if (!valiCounts.IsValid)
        {
            return;
        }
        for (int i = SelectedDetails.Count - 1; i >= 0; i--)
        {
            View_StocktakeDetails detail = SelectedDetails[i];
            List<Part> relatedParts = Service.GetRelatedParts(detail.PartID.ToString());
            foreach (var item in relatedParts)
            {
                View_StocktakeDetails tmpDetail = new View_StocktakeDetails();
                tmpDetail.CreateViewByPart(item);
                tmpDetail.StocktakeType = detail.StocktakeType;//stocktakeTypeID;
                tmpDetail.Priority = detail.Priority;// stocktakePriorityID;
                if (!RequestedDetails.Exists(d => d.PartID == tmpDetail.PartID))
                {
                    RequestedDetails.Add(tmpDetail);
                }
                if (RequestID != null)
                {
                    int index = RemovedDetails.FindIndex(d => d.PartID == tmpDetail.PartID);
                    if (index >= 0)
                    {
                        RemovedDetails.RemoveAt(index);
                    }
                }
            }
            SelectedDetails.RemoveAt(i);
        }
        BindData();
    }


    protected void gvParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //bind stocktake priority
            DropDownList tmpPriority = e.Row.FindControl("ddlPriority") as DropDownList;
            this.BindStocktakePriority(tmpPriority);
            tmpPriority.SelectedIndex = 2;
            DropDownList tmpStocktakeType = e.Row.FindControl("ddlStocktakeType") as DropDownList;
            this.BindStocktakeTypes(tmpStocktakeType, true);

            ViewPart part = e.Row.DataItem as ViewPart;
            CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            View_StocktakeDetails detail = SelectedDetails.Find(d => d.PartID == part.PartID);
            if (detail != null)//SelectedDetails.Exists(d => d.PartID == part.PartID))
            {
                cbSelect.Checked = true;
                if (detail.StocktakeType != null)
                {
                    tmpStocktakeType.Items.FindByValue(detail.StocktakeType.ToString()).Selected = true;
                }
                if (detail.Priority != null)
                {
                    tmpPriority.Items.FindByValue(detail.Priority.ToString()).Selected = true;
                }
            }
            else
            {
                detail = RequestedDetails.Find(d => d.PartID == part.PartID);
                if (detail != null)
                {
                    cbSelect.Enabled = false;
                    LinkButton linkAdd = e.Row.FindControl("linkAdd") as LinkButton;
                    linkAdd.Enabled = false;
                    tmpPriority.Enabled = false;
                    tmpStocktakeType.Enabled = false;
                }
                cbSelect.Checked = false;

            }

            //if (gvParts.DataKeys[e.Row.RowIndex] != null)
            //{
            //    Guid partID = (Guid)gvParts.DataKeys[e.Row.RowIndex].Value;
            //    View_StocktakeDetails details = NewDetails.FirstOrDefault(d => d.PartID == partID);
            //    if (details != null)
            //    {
            //        CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            //        cbSelect.Checked = true;
            //        if (details.StocktakeType != null)
            //        {
            //            tmpStocktakeType.Items.FindByValue(details.StocktakeType.ToString()).Selected = true;
            //        }
            //        if (details.Priority != null)
            //        {
            //            tmpPriority.Items.FindByValue(details.Priority.ToString()).Selected = true;
            //        }
            //    }

            //}
        }
    }

    protected void gvRequestParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            View_StocktakeDetails item = e.Row.DataItem as View_StocktakeDetails;
            RequiredFieldValidator reqValiPriority = e.Row.FindControl("reqValiPriority") as RequiredFieldValidator;
            RequiredFieldValidator reqValiStocktakeType = e.Row.FindControl("reqValiStocktakeType") as RequiredFieldValidator;
            if (gvRequestParts.DataKeys[e.Row.RowIndex]["PartID"] == null || item.PartID == DefaultValue.INT)//empty row
            {
                reqValiPriority.Enabled = false;
                reqValiStocktakeType.Enabled = false;
                return;
            }

            CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            if (item.NotificationID != null)
            {
                LinkButton linkbutton = e.Row.FindControl("LinkButton1") as LinkButton;
                linkbutton.Enabled = false;
                linkbutton.OnClientClick = null;
                cbSelect.Enabled = false;
            }
            else
            {
                if (SelectedReqDetails.Exists(d => d.PartID == item.PartID))
                {
                    cbSelect.Checked = true;
                }
                else
                {
                    cbSelect.Checked = false;
                }
            }

            //bind stocktake priority
            DropDownList tmpPriority = e.Row.FindControl("ddlPriority") as DropDownList;
            this.BindStocktakePriority(tmpPriority, true);
            //tmpPriority.Items.Insert(0,new ListItem("--",string.Empty));

            if (item.Priority != null)
            {
                tmpPriority.Items.FindByValue(item.Priority.ToString()).Selected = true;
                reqValiPriority.IsValid = true;
            }
            else
            {
                reqValiPriority.IsValid = false;
            }

            //bind stocktake type
            DropDownList ddlType = e.Row.FindControl("ddlStocktakeType") as DropDownList;
            this.BindStocktakeTypes(ddlType, true);
            if (item.StocktakeType != null)
            {
                ddlType.Items.FindByValue(item.StocktakeType.ToString()).Selected = true;
                reqValiStocktakeType.IsValid = true;
            }
            else
            {
                reqValiStocktakeType.IsValid = false;
            }

        }
    }


    protected void valiParts_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //RefreshNewRequestDetails();
        RefreshSelectedDetails(SelectedDetails, gvParts);
        if (SelectedDetails.Count > 0)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void valiDetails_ServerValidate(object source, ServerValidateEventArgs args)
    {
        RefreshRequestedDetails();
        int index = RequestedDetails.FindIndex(d => d.StocktakeType == null || d.Priority == null);
        if (index >= 0)
        {
            args.IsValid = false;
            index++;
            int pageIndex = (index - 1) / pagerRequestParts.PageSize + 1;
            pagerRequestParts.CurrentPage = pageIndex;

            BindRequestDetails();
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void dlRequest_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        StocktakeRequest request = e.Item.DataItem as StocktakeRequest;
        DropDownList ddlIsStatic = e.Item.FindControl("ddlIsStatic") as DropDownList;
        if (request.IsStatic.Value)
        {
            ddlIsStatic.SelectedIndex = 0;
        }
        else
        {
            ddlIsStatic.SelectedIndex = 1;
        }
    }
    protected void gvParts_PreRender(object sender, EventArgs e)
    {
        //List<View_StocktakeDetails> details = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        //if (gvParts.Rows.Count == 0)
        //{
        List<ViewPart> partList = new List<ViewPart> { new ViewPart() };
        //BindDataControl(gvParts, partList);
        //gvParts.Rows[0].Visible = false;
        BindEmptyGridView(gvParts, partList);
        //}
    }
    protected void gvRequestParts_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> detailsList = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvRequestParts, detailsList);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("StocktakeReqList.aspx");
    }
    protected void gvRequestParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        RefreshSelectedDetails(SelectedReqDetails, gvRequestParts);
        RefreshRequestedDetails();
        int partID = int.Parse(gvRequestParts.DataKeys[e.RowIndex].Value.ToString());
        View_StocktakeDetails details = GetDetailsByRow(gvRequestParts.Rows[e.RowIndex]);
        details.PartID = partID;
        if (RequestID != null)
        {
            RemovedDetails.Add(details);
        }

        //if (RequestID != null)
        //{
        int index = RequestedDetails.FindIndex(d => d.PartID == details.PartID);
        if (index >= 0)
        {
            RequestedDetails.RemoveAt(index);
        }
        index = -1;

        index = SelectedReqDetails.FindIndex(d => d.PartID == details.PartID);
        if (index >= 0)
        {
            SelectedReqDetails.RemoveAt(index);
        }
        //}
        BindData();
        //RefreshDetails();
        //Guid partID = (Guid)gvRequestParts.DataKeys[e.RowIndex].Value;
        //Details.RemoveAll(d => d.PartID == partID);

        //if (RequestID != null)
        //{
        //    NewStocktakeRequest request = new NewStocktakeRequest
        //    {
        //        Details = new List<NewStocktakeDetails> 
        //        { 
        //            new NewStocktakeDetails { PartID = partID.ToString()} 
        //        }
        //    };
        //    Service.UpdateCachedRequest(request, CacheKey, false, true);
        //}
        //BindRequestDetails();
        //BindDataControl(gvRequestParts, Details);
    }
    //protected void linkDelete_Click(object sender, EventArgs e)
    //{
    //    Details.Clear();
    //    BindDataControl(gvRequestParts, Details);
    //    pagerRequestParts.TotalRecord = 0;
    //    pagerRequestParts.CurrentPage = 1;
    //    if (RequestID != null)
    //    {
    //        NewStocktakeRequest request = new NewStocktakeRequest
    //        {
    //            Details = new List<NewStocktakeDetails>()
    //        };
    //        Service.UpdateCachedRequest(request, CacheKey, false, true);
    //    }
    //    if (gvParts.DataKeys[0].Value != null)
    //    {
    //        Query();
    //    }
    //}
    protected void valiCounts_ServerValidate(object source, ServerValidateEventArgs args)
    {
        SGM.ECount.DataModel.User user = CurrentUser.UserInfo;//Service.GetUserbyKey(CurrentUser.UserInfo);
        int maxUserCount;
        int currentCount;
        int maxCount;
        int currentAmount = 0;
        DropDownList ddlIsStatic = dlRequest.Items[0].FindControl("ddlIsStatic") as DropDownList;
        if (ddlIsStatic.SelectedIndex > 0)
        {
            BizParams param = BizParamsList.Find(p => p.ParamKey == "MaxDynamic");
            maxCount = int.Parse(param.ParamValue);
            maxUserCount = user.UserGroup.MaxDynamicStocktake ?? 0;
            currentCount = user.UserGroup.CurrentDynamicStocktake.Value;
            currentAmount = CurrentDynAmount;
            currentAmount = currentAmount - currentCount;
        }
        else
        {
            BizParams param = BizParamsList.Find(p => p.ParamKey == "MaxStatic");
            maxCount = int.Parse(param.ParamValue);
            maxUserCount = user.UserGroup.MaxStaticStocktake.Value;

            currentCount = user.UserGroup.CurrentStaticStocktake.Value;
            currentAmount = CurrentStAmount;
            currentAmount = currentAmount - currentCount;
        }

        int newPartsCount = 0;
        if (gvParts.DataKeys[0].Value != null)
        {
            //List<View_StocktakeDetails> newDetails = NewDetails;

            for (int i = 0; i < gvParts.Rows.Count; i++)
            {
                if (gvParts.DataKeys[i] != null)
                {
                    CheckBox cbSelect = gvParts.Rows[i].FindControl("cbSelect") as CheckBox;
                    if (cbSelect != null)//add selected item
                    {
                        int partID = (int)gvParts.DataKeys[i].Value;
                        if (cbSelect.Checked)
                        {
                            newPartsCount++;
                        }
                    }
                }
            }
        }

        currentCount += newPartsCount + RequestedDetails.Count ;// ((OriginDetailCount == null) ? 0 : OriginDetailCount.Value);
        if (currentCount > maxUserCount)
        {
            valiCounts.Text = "已超过当前用户盘点数量上限！";
            args.IsValid = false;
        }
        else
        {
            currentCount += currentAmount;
            if (currentCount > maxCount)
            {
                valiCounts.Text = "已超过盘点数量上限！";
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                valiDetails.Validate();
                if (!valiDetails.IsValid)
                {
                    return;
                }
                RefreshRequestedDetails();
                if (RequestID != null)//update
                {
                    DropDownList ddlIsStatic = dlRequest.Items[0].FindControl("ddlIsStatic") as DropDownList;
                    NewStocktakeRequest request = new NewStocktakeRequest { RequestID = RequestID, RequestBy = CurrentUser.UserInfo.UserID ,IsStatic = (ddlIsStatic.SelectedIndex == 0)};
                    Service.UpdateRequest(request, RemovedDetails.Select(d => d.PartID.Value).ToList(), RequestedDetails);
                }
                else
                {
                    if (dlRequest.Items.Count != 0)
                    {
                        NewStocktakeRequest request = BuildRequest();
                        StocktakeRequest req = Service.RequestStocktake(BuildRequest());
                        RequestID = req.RequestID;
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择零件');</script>");
                        return;
                    }
                }
                ClearCache();
                BindData();
                CurrentUser.RefreshUserProfile();
                CacheHelper.RemoveCache(Consts.CACHE_KEY_USER_GROUPS);
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                break;
            case "SaveAsTemplate":
                break;
            case "query":
                SelectedDetails = null;
                BindData();
                break;
            case "return":
                Response.Redirect("StocktakeReqList.aspx");
                break;
            default:
                break;
        }
    }

    private NewStocktakeRequest BuildRequest()
    {
        NewStocktakeRequest request = new NewStocktakeRequest();
        request.RequestBy = CurrentUser.UserInfo.UserID;
        DropDownList ddlIsStatic = dlRequest.Items[0].FindControl("ddlIsStatic") as DropDownList;
        request.IsStatic = (ddlIsStatic.SelectedIndex == 0);
        request.IsCycleCount = false;
        request.Details = (from d in RequestedDetails
                           select new NewStocktakeDetails
                           {
                               PartID = d.PartID.ToString(),
                               StocktakeTypeID = d.StocktakeType.Value,
                               StocktakePriority = d.Priority.Value,
                               Description = d.DetailsDesc,
                               PreDynamicNoticeCode = d.PreDynamicNoticeCode,
                               PreDynamicNotiTime = d.PreDynamicNotiTime,
                               PreStaticNoticeCode = d.PreStaticNoticeCode,
                               PreStaticNotiTime = d.PreStaticNotiTime
                           }).ToList();
        return request;
    }


    protected void linkRemove_Click(object sender, EventArgs e)
    {
        RefreshSelectedDetails(SelectedReqDetails, gvRequestParts);
        RefreshRequestedDetails();
        for (int i = SelectedReqDetails.Count - 1; i >= 0; i--)
        {
            View_StocktakeDetails detail = SelectedReqDetails[i];

            if (RequestID != null)//update
            {
                RemovedDetails.Add(detail);
            }
            int index = RequestedDetails.FindIndex(d => d.PartID == detail.PartID);
            if (index >= 0)
            {
                RequestedDetails.RemoveAt(index);
            }

            SelectedReqDetails.RemoveAt(i);
        }
        BindData();
    }
    protected void gvParts_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        RefreshSelectedDetails(SelectedReqDetails, gvRequestParts);
        RefreshRequestedDetails();
        valiCounts.Validate();
        if (!valiCounts.IsValid)
        {
            return;
        }

        View_StocktakeDetails detail = GetDetailsByRow(gvParts.Rows[e.RowIndex]);
        detail.PartID = int.Parse(gvParts.DataKeys[e.RowIndex].Value.ToString());
        List<Part> relatedParts = Service.GetRelatedParts(detail.PartID.ToString());
        foreach (var item in relatedParts)
        {
            View_StocktakeDetails tmpDetail = new View_StocktakeDetails();
            tmpDetail.CreateViewByPart(item);
            tmpDetail.StocktakeType = detail.StocktakeType;//stocktakeTypeID;
            //tmpDetail.TypeName = detail.TypeName;//ddlStocktakeType.SelectedItem.Text;
            tmpDetail.Priority = detail.Priority;// stocktakePriorityID;
            //tmpDetail.PriorityName = detail.PriorityName; //ddlStocktakePriority.SelectedItem.Text;
            if (!RequestedDetails.Exists(d => d.PartID == tmpDetail.PartID))
            {
                RequestedDetails.Add(tmpDetail);
            }
            if (RequestID != null)
            {
                int index = RemovedDetails.FindIndex(d => d.PartID == tmpDetail.PartID);
                if (index >= 0)
                {
                    RemovedDetails.RemoveAt(index);
                }
            }
            int idx = SelectedDetails.FindIndex(d => d.PartID == tmpDetail.PartID);
            if (idx >= 0)
            {
                SelectedDetails.RemoveAt(idx);
            }
        }
        //int index = RemovedDetails.FindIndex(d => d.PartID == detail.PartID);
        //if (index >= 0)
        //{
        //    RemovedDetails.RemoveAt(index);
        //}
        //RequestedDetails.Add(detail);
        BindData();
    }
}
