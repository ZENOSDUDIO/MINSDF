using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCS.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Globalization;
using SGM.Common.Cache;
using SGM.Common.Utility;

public partial class PhysicalCount_CycleCountRequest : ECountBasePage
{
    protected override bool ShowWaitingModal
    {
        get
        {
            return false;
        }
    }

    public List<View_StocktakeDetails> UpdatedDetails
    {
        get
        {
            if (Session["CCReq_UpdatedDetails"] == null)
            {
                Session["CCReq_UpdatedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["CCReq_UpdatedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["CCReq_UpdatedDetails"] = value;
        }
    }

    public List<View_StocktakeDetails> DeletedDetails
    {
        get
        {
            if (Session["CCReq_DeletedDetails"] == null)
            {
                Session["CCReq_DeletedDetails"] = new List<View_StocktakeDetails>();
            }
            return Session["CCReq_DeletedDetails"] as List<View_StocktakeDetails>;
        }
        set
        {
            Session["CCReq_DeletedDetails"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BizParams param = BizParamsList.Find(p=>string.Equals(p.ParamKey,Consts.BIZ_PARAMS_CYCLECOUNTED));
            bool cycleCounted=false;
            Toolbar1.Items[1].Visible = false;

            Toolbar1.Visible= (param != null && Boolean.TryParse(param.ParamValue, out cycleCounted) && !cycleCounted);
            
            UpdatedDetails = null;
            DeletedDetails = null;

        }
        AspPager1.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(AspPager1_PageNumberSelect);
    }

    private void RefreshDetails()
    {
        for (int i = 0; i < gvParts.Rows.Count; i++)
        {
            GridViewRow row = gvParts.Rows[i];
            TextBox txtDesc = row.FindControl("txtDesc") as TextBox;
            CheckBox cbDelete = row.FindControl("cbDelete") as CheckBox;
            int partID = (int)gvParts.DataKeys[i].Value;
            if (cbDelete != null)
            {
                View_StocktakeDetails details = DeletedDetails.Find(d => d.PartID == partID);
                if (cbDelete.Checked && details == null)
                {
                    details = new View_StocktakeDetails { PartID = partID };
                    DeletedDetails.Add(details);
                    return;
                }
                else
                {
                    if (!cbDelete.Checked && details != null)
                    {
                        DeletedDetails.Remove(details);
                    }
                }
            }
            if (txtDesc != null && !string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                View_StocktakeDetails details = UpdatedDetails.Find(d => d.PartID == partID);
                if (details != null)
                {
                    details.DetailsDesc = txtDesc.Text;
                }
                else
                {
                    details = new View_StocktakeDetails { PartID = partID, DetailsDesc = txtDesc.Text };
                    UpdatedDetails.Add(details);
                }
            }
        }
    }

    void AspPager1_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshDetails();
        Query();
    }
    private void Query()
    {
        int pageCount;
        int itemCount;
        int cycledTimes = 0;
        List<ViewPart> partsToCycleCount = Service.GetPartsToCycleCount(out cycledTimes, AspPager1.PageSize, AspPager1.CurrentPage, out pageCount, out itemCount);
        this.BindDataControl(gvParts, partsToCycleCount);
        AspPager1.TotalRecord = itemCount;
        lblCompleted.Text = cycledTimes.ToString();
        lblAvg.Text = itemCount.ToString();

        DateTime tmpDate = new DateTime(DateTime.Now.Year, 12, 31);
        int totalWeeksInYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(tmpDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        lblTotal.Text = totalWeeksInYear.ToString();
        int rest = totalWeeksInYear - cycledTimes;
        rest = (rest < 0) ? 0 : rest;
        lblRest.Text = rest.ToString();

    }
    protected void Toolbar1_ButtonClicked(object sender, ButtonEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "save":
                    RefreshDetails();
                    List<StocktakeRequest> newRequestList = new List<StocktakeRequest> { Service.CreateCycleCount(CurrentUser.UserInfo, DeletedDetails, UpdatedDetails) };
                    CacheHelper.RemoveCache(Consts.CACHE_KEY_BIZ_PARAMS);
                    lblAvg.Text = string.Empty;
                    lblCompleted.Text = string.Empty;
                    lblRest.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                    UpdatedDetails = null;
                    DeletedDetails = null;
                    gvParts.DataBind();
                    AspPager1.TotalRecord = 0;
                    AspPager1.CurrentPage = 1;
                    BindDataControl(gvRequest, newRequestList);
                    Toolbar1.Visible = false;
                    break;
                case "preview":
                    Query();
                    Toolbar1.Items[1].Visible = true;
                    gvRequest.DataBind();
                    break;
                default:
                    break;
            }
        }
        finally
        {
            //if (!Page.ClientScript.IsClientScriptBlockRegistered("closeWatingModal"))
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "closeWatingModal", "closeWaitingModal();", true);
            //}
        }
    }
    protected void gvParts_PreRender(object sender, EventArgs e)
    {
        List<ViewPart> partList = new List<ViewPart> { new ViewPart() };
        BindEmptyGridView(gvParts, partList);
    }
    protected void gvRequest_PreRender(object sender, EventArgs e)
    {
        List<StocktakeRequest> reqList = new List<StocktakeRequest> { new StocktakeRequest() };
        BindEmptyGridView(gvRequest, reqList);
    }
    protected void gvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            LinkButton linkRequestNo = e.Row.Cells[0].FindControl("linkRequestNo") as LinkButton;
            StocktakeRequest request = e.Row.DataItem as StocktakeRequest;
            string script = string.Format("showDialog('StocktakeRequest.aspx?Mode=View&id={0}',1080,550);return false;", request.RequestID);
            linkRequestNo.OnClientClick = script;
        }
    }
    protected void gvParts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int partID = (int)gvParts.DataKeys[e.Row.RowIndex].Value;
            View_StocktakeDetails delItem = DeletedDetails.Find(d => d.PartID == partID);
            View_StocktakeDetails updItem = UpdatedDetails.Find(d => d.PartID == partID);
            CheckBox cbDelete = e.Row.FindControl("cbDelete") as CheckBox;
            if (cbDelete != null)
            {
                cbDelete.Checked = (delItem != null);
            }
            TextBox txtDesc = e.Row.FindControl("txtDesc") as TextBox;
            if (txtDesc != null && updItem != null)
            {
                txtDesc.Text = updItem.DetailsDesc;
            }
        }
    }
}
