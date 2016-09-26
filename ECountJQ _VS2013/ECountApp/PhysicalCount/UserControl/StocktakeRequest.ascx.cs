using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class PhysicalCount_UserControl_StocktakeRequest : ECountBaseUserControl
{
    protected List<View_StocktakeDetails> Details
    {
        get
        {
            return Session["StocktakeDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["StocktakeDetails"] = value;
        }
    }
    protected List<ViewPart> QueryParts
    {
        get
        {
            return Session["Request_QueryParts"] as List<ViewPart>;
        }
        set
        {
            Session["Request_QueryParts"] = value;
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

    protected int? OriginDetailCount
    {
        get
        {
            return ViewState["OriginDetailCount"] as int?;
        }
        set
        {
            ViewState["OriginDetailCount"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Details = null;
            QueryParts = null;
            if (Request.QueryString["id"] != null)
            {
                RequestID = Convert.ToInt64(Request.QueryString["id"]);
            }
            BindData();
        }
        //AspPager1.PageSizeChange += new BizDataMaintain_AspPager.PageSizeChangeEventHandler(AspPager1_PageSizeChange);
        AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
        pagerParts.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerParts_PageNumberSelect);
    }

    void pagerParts_PageNumberSelect(object sender, EventArgs e)
    {
        BindRequestDetails();
    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        Query();
    }

    void AspPager1_PageSizeChange(object sender, EventArgs e)
    {
        AspPager1.CurrentPage = 1;
        Query();
    }

    private void BindRequestDetails()
    {
        List<View_StocktakeDetails> details;
        if (RequestID != null)
        {
            View_StocktakeDetails condition = new View_StocktakeDetails();
            condition.RequestID = RequestID.Value;
            int pageCount;
            int itemCount;
            //View_StocktakeDetails condition=new View_StocktakeDetails()
            //{
            //    RequestID=this.RequestID.Value
            //};
            string key=string.Empty;
            details = Service.QueryRequestDetailsByPage(condition, null, null,null,null, pagerParts.PageSize, pagerParts.CurrentPage, out pageCount, out itemCount);//.QueryStocktakeReqDetails(condition, null, null);
            pagerParts.TotalRecord = itemCount;
            OriginDetailCount = details.Count;
        }
        else
        {
            details = new List<View_StocktakeDetails>();
        }
        this.Details = details;
        this.BindDataControl(this.gvRequestParts, this.Details);
        //gvRequestParts.DataSource = details;
        //gvRequestParts.DataBind();
    }

    private void BindData()
    {
        BindPlant();
        //BindParts();
        BindRequest(null);
        BindRequestDetails();

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
            multiViewRequest.ActiveViewIndex = 0;
        }
        else
        {
            multiViewRequest.ActiveViewIndex = 1;
        }
    }
    private void BindPlant()
    {
        BindPlants(ddlPlant);
        ddlPlant.SelectedIndex = 0;
    }

    protected void btnAddPart_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && gvParts.Rows.Count > 0)
        {
            //load stocktake details from session
            List<View_StocktakeDetails> details = this.Details;

            //parts to be added to stocktake request
            List<View_StocktakeDetails> newDetails = new List<View_StocktakeDetails>();
            List<ViewPart> parts = this.QueryParts;
            for (int i = gvParts.Rows.Count - 1; i >= 0; i--)
            {
                CheckBox ckSelect = gvParts.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
                //move part from parts list into stocktake request
                if (ckSelect.Checked)
                {
                    // gvParts.Rows[i].Cells[11].Text;
                    string partID = gvParts.DataKeys[i].Value.ToString();
                    bool exists = details.Exists(d => d.PartID.ToString() == partID);
                    if (!exists)//part not in request list
                    {
                        DropDownList ddlStocktakeType = gvParts.Rows[i].Cells[1].FindControl("ddlStocktakeType") as DropDownList;
                        int stocktakeTypeID = Convert.ToInt32(ddlStocktakeType.SelectedValue);
                        DropDownList ddlStocktakePriority = gvParts.Rows[i].Cells[2].FindControl("ddlPriority") as DropDownList;
                        int stocktakePriorityID = Convert.ToInt32(ddlStocktakePriority.SelectedValue);
                        //ViewStockTakeRequest newDetail = new ViewStockTakeRequest();
                        //newDetail.CreateViewByPart(this.QueryParts[i]);

                        //newDetail.PartID = new Guid(partID);
                        //newDetail.PartCode = ((HyperLink)gvParts.Rows[i].Cells[3].FindControl("linkPartCode")).Text;
                        //newDetail.PartChineseName = this.QueryParts[i].PartChineseName;// gvParts.Rows[i].Cells[4].Text;
                        //newDetail.PlantName = this.QueryParts[i].Plant.PlantName;// ((Label)gvParts.Rows[i].Cells[5].FindControl("lblPlant")).Text;
                        //newDetail.DUNS = this.QueryParts[i].Supplier.DUNS;// ((Label)gvParts.Rows[i].Cells[6].FindControl("lblDUNS")).Text;
                        //newDetail.FollowUp = this.QueryParts[i].FollowUp;// gvParts.Rows[i].Cells[7].Text;
                        //newDetail.CategoryName = ((Label)gvParts.Rows[i].Cells[8].FindControl("lblPartCategory")).Text;
                        //newDetail.LevelName = ((Label)gvParts.Rows[i].Cells[9].FindControl("lblCycleCountLevel")).Text;
                        //newDetail.StatusName = ((Label)gvParts.Rows[i].Cells[10].FindControl("lblPartStatus")).Text;

                        //newDetail.TypeName = ddlStocktakeType.SelectedItem.Text;
                        //newDetail.StocktakeType = stocktakeTypeID;
                        //newDetail.Priority = stocktakePriorityID;
                        //newDetail.PriorityName = ddlStocktakePriority.SelectedItem.Text;
                        //newDetails.Add(newDetail);

                        List<Part> relatedParts = Service.GetRelatedParts(partID);
                        foreach (var item in relatedParts)
                        {
                            View_StocktakeDetails tmpDetail = new View_StocktakeDetails();
                            tmpDetail.CreateViewByPart(item);
                            tmpDetail.StocktakeType = stocktakeTypeID;
                            tmpDetail.TypeName = ddlStocktakeType.SelectedItem.Text;
                            tmpDetail.Priority = stocktakePriorityID;
                            tmpDetail.PriorityName = ddlStocktakePriority.SelectedItem.Text;
                            newDetails.Add(tmpDetail);
                        }
                    }
                    parts.RemoveAt(i);
                }
            }
            BindDataControl(gvParts, parts);

            details.AddRange(newDetails);
            Details = details;
            BindDataControl(gvRequestParts, details);
        }

    }

    protected void ddlStocktakeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        GridViewRow row = ddl.Parent.Parent as GridViewRow;
        DropDownList tmpPriority = row.Cells[2].FindControl("ddlPriority") as DropDownList;
        int selectedIndex = ddl.SelectedIndex;
        if (selectedIndex > 0)
        {
            int priorityID = this.StocktakeTypes[selectedIndex - 1].DefaultPriority.Value;
            //set default priority
            tmpPriority.SelectedValue = priorityID.ToString();
        }
        else
        {
            tmpPriority.SelectedIndex = 0;
            RequiredFieldValidator reqVali = row.Cells[1].FindControl("reqValiStocktakeType") as RequiredFieldValidator;
            reqVali.IsValid = false;
        }
    }
    protected void gvParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            //bind stocktake priority
            DropDownList tmpPriority = e.Row.Cells[2].FindControl("ddlPriority") as DropDownList;
            this.BindStocktakePriority(tmpPriority);
            DropDownList tmpStocktakeType = e.Row.Cells[1].FindControl("ddlStocktakeType") as DropDownList;
            this.BindStocktakeTypes(tmpStocktakeType);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Part condition = new Part();
        condition.PartCode = txtPartCode.Text.Trim();
        condition.PartChineseName = txtPartName.Text.Trim();
        condition.Specs = txtBOOK.Text.Trim();
        condition.FollowUp = txtFollowUp.Text.Trim();
        if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
        {
            condition.Plant = new Plant();
            condition.Plant.PlantID = int.Parse(ddlPlant.SelectedValue);
        }
        int pageCount;
        int itemCount;
        List<ViewPart> parts = Service.QueryPartByPage(condition, 10, 1, out pageCount, out itemCount);
        this.QueryParts = parts;
        this.BindDataControl(gvParts, parts);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //CurrentUser.UserGroup.MaxStaticStocktake
        NewStocktakeRequest request = new NewStocktakeRequest();
        request.IsStatic = (ddlIsStatic.SelectedIndex == 0);
        request.Details = new List<NewStocktakeDetails>();
        List<View_StocktakeDetails> detailsList = this.Details;
        for (int i = 0; i < detailsList.Count; i++)
        {
            View_StocktakeDetails item = detailsList[i];
            item.Description = ((TextBox)gvRequestParts.Rows[i].Cells[0].FindControl("txtComments")).Text;
            //item.StocktakeType = Convert.ToInt32(((DropDownList)gvRequestParts.Rows[i].Cells[5].FindControl("ddlType")).SelectedValue);
            //item.Priority = Convert.ToInt32(((DropDownList)gvRequestParts.Rows[i].Cells[6].FindControl("ddlDetailPriority")).SelectedValue);

            NewStocktakeDetails details = new NewStocktakeDetails();
            details.PartID = item.PartID.ToString();
            details.StocktakeTypeID = item.StocktakeType.Value;
            details.StocktakePriority = item.Priority.Value;
            request.Details.Add(details);
        }
        this.Details = detailsList;
        StocktakeRequest newRequest = Service.RequestStocktake(request);
        RequestID = newRequest.RequestID;
        this.BindRequest(newRequest);
        this.BindRequestDetails();
    }

    protected void gvRequestParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            View_StocktakeDetails item = e.Row.DataItem as View_StocktakeDetails;

            if (item.Priority != null)
            {
                //bind stocktake priority
                DropDownList tmpPriority = e.Row.Cells[6].FindControl("ddlDetailPriority") as DropDownList;
                this.BindStocktakePriority(tmpPriority);
                tmpPriority.Items.FindByValue(item.Priority.ToString()).Selected = true;
            }

            if (item.StocktakeType != null)
            {
                //bind stocktake type
                DropDownList tmpStocktakeType = e.Row.Cells[5].FindControl("ddlType") as DropDownList;
                this.BindStocktakeTypes(tmpStocktakeType);
                tmpStocktakeType.Items.FindByValue(item.StocktakeType.ToString()).Selected = true;
            }
        }
    }

    protected void requestItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        GridViewRow row = ddl.Parent.Parent as GridViewRow;

        switch (ddl.ID)
        {
            case "ddlDetailPriority":
                Details[row.RowIndex].Priority = Convert.ToInt32(ddl.SelectedValue);
                Details[row.RowIndex].PriorityName = ddl.SelectedItem.Text;
                break;
            case "ddlType":
                Details[row.RowIndex].StocktakeType = Convert.ToInt32(ddl.SelectedValue);
                Details[row.RowIndex].TypeName = ddl.SelectedItem.Text;
                break;
            default:
                break;
        }
    }
    protected void partItem_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        GridViewRow currentRow = checkBox.Parent.Parent as GridViewRow;
        if (currentRow == gvParts.HeaderRow)
        {
            if (checkBox.Checked)
            {
                foreach (GridViewRow row in gvParts.Rows)
                {
                    CheckBox cbSelect = row.Cells[0].FindControl("cbSelect") as CheckBox;
                    cbSelect.Checked = true;
                }
            }
        }
        else
        {
            if (!checkBox.Checked)
            {
                CheckBox cbSelectAll = gvParts.HeaderRow.Cells[0].FindControl("cbSelectAll") as CheckBox;
                cbSelectAll.Checked = false;
            }
        }
    }
    protected void valiParts_ServerValidate(object source, ServerValidateEventArgs args)
    {
        for (int i = gvParts.Rows.Count - 1; i >= 0; i--)
        {
            CheckBox ckSelect = gvParts.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
            if (ckSelect.Checked)
            {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
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
        Details.RemoveAt(e.RowIndex);
        BindDataControl(gvRequestParts, Details);
    }
    protected void linkDelete_Click(object sender, EventArgs e)
    {
        Details.Clear();
        BindDataControl(gvRequestParts, Details);
    }
    protected void valiCounts_ServerValidate(object source, ServerValidateEventArgs args)
    {
        SGM.ECount.DataModel.User user = Service.GetUserbyKey(CurrentUser.UserInfo);
        int maxCount;
        int currentCount;

        if (ddlIsStatic.SelectedIndex > 0)
        {
            maxCount = user.UserGroup.MaxDynamicStocktake.Value;
            currentCount = user.UserGroup.CurrentDynamicStocktake.Value;
        }
        else
        {
            maxCount = user.UserGroup.MaxStaticStocktake.Value;
            currentCount = user.UserGroup.CurrentStaticStocktake.Value;
        }
        if (Details.Count + currentCount - OriginDetailCount > maxCount)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
        //Details.Count
        //if (user.UserGroup.MaxStaticStocktake)
        //{

        //}
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                try
                {
                    Save();
                }
                finally
                {
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("closeWatingModal"))
                    {
                        ToolkitScriptManager.RegisterClientScriptBlock(this.Container, this.Container.GetType(), "closeWatingModal", "closeWaitingModal();", true);
                    }
                }
                break;
            case "query":
                try
                {
                    Query();
                }
                finally
                {
                    if (!Page.ClientScript.IsClientScriptBlockRegistered("closeWatingModal"))
                    {
                        ToolkitScriptManager.RegisterClientScriptBlock(this.Container, this.Container.GetType(), "closeWatingModal", "closeWaitingModal();", true);
                    }
                }
                break;
            case "return":
                Response.Redirect("StocktakeReqList.aspx");
                break;
            default:
                break;
        }
    }

    private void Query()
    {
        Part condition = new Part();
        condition.PartCode = txtPartCode.Text.Trim();
        condition.PartChineseName = txtPartName.Text.Trim();
        condition.Specs = txtBOOK.Text.Trim();
        condition.FollowUp = txtFollowUp.Text.Trim();
        if (!string.IsNullOrEmpty(ddlPlant.SelectedValue))
        {
            condition.Plant = new Plant();
            condition.Plant.PlantID = int.Parse(ddlPlant.SelectedValue);
        }
        int pageCount;
        int itemCount;
        List<ViewPart> parts = Service.QueryPartByPage(condition, AspPager1.PageSize, AspPager1.CurrentPage, out pageCount, out itemCount);
        AspPager1.TotalRecord = itemCount;
        AspPager1.TotalPage = pageCount;
        this.QueryParts = parts;
        this.BindDataControl(gvParts, parts);
    }

    private void Save()
    {
        //CurrentUser.UserGroup.MaxStaticStocktake
        NewStocktakeRequest request = new NewStocktakeRequest();
        request.RequestBy = CurrentUser.UserInfo.UserID;
        request.IsStatic = (ddlIsStatic.SelectedIndex == 0);
        request.IsCycleCount = false;
        request.Details = new List<NewStocktakeDetails>();
        List<View_StocktakeDetails> detailsList = this.Details;
        for (int i = 0; i < detailsList.Count; i++)
        {
            View_StocktakeDetails item = detailsList[i];
            item.Description = ((TextBox)gvRequestParts.Rows[i].Cells[0].FindControl("txtComments")).Text;
            //item.StocktakeType = Convert.ToInt32(((DropDownList)gvRequestParts.Rows[i].Cells[5].FindControl("ddlType")).SelectedValue);
            //item.Priority = Convert.ToInt32(((DropDownList)gvRequestParts.Rows[i].Cells[6].FindControl("ddlDetailPriority")).SelectedValue);

            NewStocktakeDetails details = new NewStocktakeDetails();
            details.PartID = item.PartID.ToString();
            details.StocktakeTypeID = item.StocktakeType.Value;
            details.StocktakePriority = item.Priority.Value;
            details.Description = item.Description;
            request.Details.Add(details);
        }
        this.Details = detailsList;
        if (RequestID==null)//create
        {
            StocktakeRequest newRequest = Service.RequestStocktake(request);
            RequestID = newRequest.RequestID;
            this.BindRequest(newRequest);
            this.BindRequestDetails(); 
        }
        else//update
        {
            request.RequestID = this.RequestID;
            Service.UpdateStocktakeRequest(request);
        }
    }
}
