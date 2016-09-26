using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using SCS.Web.UI.WebControls;

public partial class PhysicalCount_StocktakeReqList : ECountBasePage//System.Web.UI.Page
{
    //protected override bool ShowWaitingModal
    //{
    //    get
    //    {
    //        return true;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            BindData();
        }
        AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
        pagerRequest.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerRequest_PageNumberSelect);

    }

    void pagerRequest_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshSelectedRequests();
        int? plantId;
        int? stocktakeType;
        DateTime? dateStart;
        DateTime? dateEnd;
        BuildCondition(out plantId, out stocktakeType, out dateStart, out dateEnd);
        QueryRequest(plantId, stocktakeType, dateStart, dateEnd);
    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        //RefreshSelectedRequests();
        //Query();
        int? plantId;
        int? stocktakeType;
        DateTime? dateStart;
        DateTime? dateEnd;
        BuildCondition(out plantId, out stocktakeType, out dateStart, out dateEnd);
        QueryDetails(plantId, stocktakeType, dateStart, dateEnd);
    }

    private void BindData()
    {
        this.BindStocktakeStatus(this.ddlStatus);
        ddlStatus.SelectedIndex = 0;
        this.BindStocktakeTypes(this.ddlType);
        ddlType.SelectedIndex = 0;
        this.BindPlants(this.ddlPlant);
        ddlPlant.SelectedIndex = 0;
    }

    //protected void btnCreate_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("StocktakeRequest.aspx");
    //}
    //protected void gvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //}
    //protected void btnQuery_Click(object sender, EventArgs e)
    //{
    //    Query();
    //}

    private void Query()
    {
        int? plantId;
        int? stocktakeType;
        DateTime? dateStart;
        DateTime? dateEnd;
        BuildCondition(out plantId, out stocktakeType, out dateStart, out dateEnd);
        if (cblSearchOption.Items[0].Selected)
        {
            QueryRequest(plantId, stocktakeType, dateStart, dateEnd);
        }
        else
        {
            gvRequest.DataSource = new List<StocktakeRequest>();
            gvRequest.DataBind();
            //BindEmptyGridView(gvRequest, requests);
        }
        if (cblSearchOption.Items[1].Selected)
        {
            QueryDetails(plantId, stocktakeType, dateStart, dateEnd);
        }
        else
        {
            //List<View_StocktakeDetails> details = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
            //BindEmptyGridView(gvDetails, details);
            gvDetails.DataBind();
            AspPager1.TotalRecord = 0;
        }
        if (!cblSearchOption.Items[tabContainerRequest.ActiveTabIndex].Selected)
        {
            tabContainerRequest.ActiveTabIndex = (cblSearchOption.SelectedIndex <= 0) ? 0 : 1;
        }
    }

    private void BuildCondition(out int? plantId, out int? stocktakeType, out DateTime? dateStart, out DateTime? dateEnd)
    {

        if (string.IsNullOrEmpty(ddlPlant.SelectedValue))
        {
            plantId = null;
        }
        else
        {
            plantId = Convert.ToInt32(ddlPlant.SelectedValue);
        }

        if (string.IsNullOrEmpty(ddlType.SelectedValue))
        {
            stocktakeType = null;
        }
        else
        {
            stocktakeType = Convert.ToInt32(ddlType.SelectedValue);
        }
        dateStart = null;
        dateEnd = null;
        if (!string.IsNullOrEmpty(txtTimeStart.Text))
        {
            DateTime tmpDate;

            if (DateTime.TryParse(txtTimeStart.Text, out tmpDate))
            {
                dateStart = tmpDate;
            }
        }

        if (!string.IsNullOrEmpty(txtTimeEnd.Text))
        {
            DateTime tmpDate;
            if (DateTime.TryParse(txtTimeEnd.Text, out tmpDate))
            {
                dateEnd = tmpDate;
            }
        }
    }

    private void QueryDetails(int? plantId, int? stocktakeType, DateTime? dateStart, DateTime? dateEnd)
    {
        int pageCount;
        int itemCount;

        View_StocktakeDetails detailsCondition = new View_StocktakeDetails
        {
            RequestNumber = txtRequestNo.Text,
            RequestUser = txtRequestBy.Text,
            PartPlantID = plantId,
            PartPlantCode = txtPartCode.Text,
            StocktakeType = stocktakeType,
            PartChineseName = txtPartCName.Text,
            PartCode=txtPartCode.Text
        };
        if (!string.IsNullOrEmpty(ddlIsStatic.SelectedValue))
        {
            detailsCondition.RequestIsStatic = Convert.ToBoolean(ddlIsStatic.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
        {
            detailsCondition.RequestStatus = int.Parse(ddlStatus.SelectedValue);
        }
        string tmp=string.Empty;
        List<View_StocktakeDetails> details = Service.QueryRequestDetailsByPage(detailsCondition,null,null, dateStart, dateEnd, AspPager1.PageSize, AspPager1.CurrentPage, out pageCount, out itemCount);
        BindDataControl(gvDetails, details);
        AspPager1.TotalRecord = itemCount;
    }

    private void QueryRequest(int? plantId, int? stocktakeType, DateTime? dateStart, DateTime? dateEnd)
    {
        ViewStockTakeRequest condition = new ViewStockTakeRequest
        {
            RequestNumber = txtRequestNo.Text,
            UserName = txtRequestBy.Text,
            PlantID = plantId,
            PartCode = txtPartCode.Text,
            StocktakeType = stocktakeType,
            PartChineseName = txtPartCName.Text
        };

        if (!string.IsNullOrEmpty(ddlIsStatic.SelectedValue))
        {
            condition.IsStatic = Convert.ToBoolean(ddlIsStatic.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
        {
            condition.Status = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        int pageCount;
        int itemCount;
        List<StocktakeRequest> requests = Service.QueryStocktakeRequestByPage(condition, dateStart, dateEnd, pagerRequest.PageSize, pagerRequest.CurrentPage, out pageCount, out itemCount);
        pagerRequest.TotalRecord = itemCount;
        BindDataControl(gvRequest, requests);
    }
    protected void gvRequest_PreRender(object sender, EventArgs e)
    {
        //if (gvRequest.Rows.Count==0)
        //{
        List<StocktakeRequest> requestList = new List<StocktakeRequest> { new StocktakeRequest() };
        BindEmptyGridView(gvRequest, requestList);
        //}
    }

    protected void gvDetails_PreRender(object sender, EventArgs e)
    {
        //if (gvDetails.Rows.Count==0)
        //{
        List<View_StocktakeDetails> detailsList = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvDetails, detailsList);
        //}
    }

    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "delete":
                Delete();
                SelectedRequests = null;
                Query();
                break;
            case "import":
                Response.Redirect("RequestImport.aspx");
                break;
            case "query":
  
                SelectedRequests = null;
                AspPager1.CurrentPage = 1;
                pagerRequest.CurrentPage = 1;
                Query();
                break;
            default:
                break;
        }
    }

    private void Delete()
    {
        RefreshSelectedRequests();
        List<StocktakeRequest> reqList = new List<StocktakeRequest>();
        if (SelectedRequests.Count > 0)
        {
            reqList =
                (from r in SelectedRequests
                 select new StocktakeRequest
                 {
                     RequestID = r
                 }
                 ).ToList();
        }
        Service.DeleteRequestBatch(reqList);
    }

    public List<long> SelectedRequests
    {
        get
        {
            if (Session["StocktakeReqList_SelectedRequests"] == null)
            {
                Session["StocktakeReqList_SelectedRequests"] = new List<long>();
            }
            return Session["StocktakeReqList_SelectedRequests"] as List<long>;
        }
        set
        {
            Session["StocktakeReqList_SelectedRequests"] = value;
        }
    }
    protected void RefreshSelectedRequests()
    {
        for (int i = 0; i < gvRequest.Rows.Count; i++)
        {
            CheckBox ChkSelected = gvRequest.Rows[i].FindControl("cbSelect") as CheckBox;
            long reqID = Convert.ToInt64(gvRequest.DataKeys[i].Value);
            if (ChkSelected.Checked)
            {
                if (!SelectedRequests.Contains(reqID))
                {
                    SelectedRequests.Add(reqID);
                }
            }
            else
            {
                if (SelectedRequests.Contains(reqID))
                {
                    SelectedRequests.Remove(reqID);
                }
            }
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            LinkButton linkRequestNo = e.Row.Cells[0].FindControl("linkRequestNo") as LinkButton;
            View_StocktakeDetails request = e.Row.DataItem as View_StocktakeDetails;
            string script = string.Format("showDialog('StocktakeRequest.aspx?Mode=Edit&id={0}',1080,550, null, \"refresh('{1}')\");return false;", request.RequestID, Toolbar1.Controls[3].ClientID);
            linkRequestNo.OnClientClick = script;
        }
    }
    protected void gvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex >= 0)
        {
            LinkButton linkRequestNo = e.Row.Cells[0].FindControl("linkRequestNo") as LinkButton;
            StocktakeRequest request = e.Row.DataItem as StocktakeRequest;
            string script = string.Format("showDialog('StocktakeRequest.aspx?Mode=Edit&id={0}',1080,550,null, \"refresh('{1}')\");return false;", request.RequestID, Toolbar1.Controls[3].ClientID);
            linkRequestNo.OnClientClick = script;

            CheckBox cbSelect = e.Row.FindControl("cbSelect") as CheckBox;
            long requestID = (long)gvRequest.DataKeys[e.Row.RowIndex].Value;
            if (SelectedRequests.Contains(requestID))
            {
                cbSelect.Checked = true;
            }
        }
    }
    protected void cbSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSelect = sender as CheckBox;
        if (string.Compare(cbSelect.ID, "cbSelect") == 0)//cbSelect changed
        {
            CheckBox cbSelectAll = gvRequest.HeaderRow.Cells[5].FindControl("cbSelectAll") as CheckBox;
            if (cbSelect.Checked)
            {
                foreach (GridViewRow row in gvRequest.Rows)
                {
                    CheckBox cb = row.Cells[5].FindControl("cbSelect") as CheckBox;
                    if (cb != null && !cb.Checked)
                    {
                        if (cbSelectAll != null)
                        {
                            cbSelectAll.Checked = false;
                        }
                        return;
                    }
                }
                if (cbSelectAll != null)
                {
                    cbSelectAll.Checked = true;
                }
            }
            else
            {
                cbSelectAll.Checked = false;
            }
        }
        else//cbSelectAll changed
        {
            foreach (GridViewRow row in gvRequest.Rows)
            {
                CheckBox cb = row.Cells[5].FindControl("cbSelect") as CheckBox;
                if (cb != null)
                {
                    cb.Checked = cbSelect.Checked;
                }
            }
            //if (cbSelect.Checked)
            //{
            //    foreach (GridViewRow row in gvRequest.Rows)
            //    {
            //        CheckBox cb = row.Cells[5].FindControl("cbSelect") as CheckBox;
            //        if (cb!=null)
            //        {
            //            cb.Checked = true;
            //        }
            //    }
            //}
            //else
            //{

            //}
        }
    }
}
