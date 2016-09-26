using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using SGM.ECount.DataModel;
using SGM.Common.Utility;
using System.Data;
using System.Text;
using System.Globalization;

public partial class PhysicalCount_NewAnalyses : ECountBasePage
{

    public string View
    {
        get
        {
            if (Request.QueryString["view"] == null)
            {
                return "viewNewAnalyse";
            }
            return Request.QueryString["view"];
        }
    }
    public List<string> ReportColumnsList
    {
        get
        {
            if (Session["NewAnalyses_ColumnsList"] == null)
            {
                Session["NewAnalyses_ColumnsList"] = new List<string>();
            }
            return Session["NewAnalyses_ColumnsList"] as List<string>;
        }
        set
        {
            Session["NewAnalyses_ColumnsList"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportColumnsList = null;
            NotiToAnalyse = null;
            for (int i = 0; i < mvSearch.Views.Count; i++)
            {
                if (string.Equals(mvSearch.Views[i].ID, View))
                {
                    mvSearch.ActiveViewIndex = i;
                    break;
                }
            }
            if (!viewNewAnalyse.Visible)
            {
                mvNewReport.ActiveViewIndex = 1;
            }

            BindData();
        }
        InitToolbar();
        if (!viewNewAnalyse.Visible)//(View != "viewNewAnalyse")
        {
            Toolbar1.Items[0].CausesValidation = false;
        }

        gvAnalysis.Columns[0].Visible = viewNewAnalyse.Visible;// (View == "viewNewAnalyse");
        pagerNew.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerNew_PageNumberSelect);
        pagerReport.PageNumberSelect += new BizDataMaintain_AspPager.PageNumberSelectEventHandler(pagerReport_PageNumberSelect);

    }

    void pagerReport_PageNumberSelect(object sender, EventArgs e)
    {
        Query();
    }

    private void InitToolbar()
    {
        Toolbar1.Items[1].Visible = (viewNewAnalyse.Visible);//
        Toolbar1.Items[2].Visible = (viewAnalyseMgr.Visible);//false;
        Toolbar1.Items[3].Visible = (viewNewAnalyse.Visible);//false;
        Toolbar1.Items[4].Visible = (viewAnalyse.Visible);//false;
        Toolbar1.Items[5].Visible = (viewNewAnalyse.Visible);//false; 
    }


    void pagerNew_PageNumberSelect(object sender, EventArgs e)
    {
        RefreshNotiToAnalyse();
        Query();
    }

    private void RefreshNotiToAnalyse()
    {
        if (gvNew.DataKeys[0].Value != null)
        {
            StocktakeNotification noti = NotiToAnalyse;
            noti.NotificationID = Convert.ToInt64(gvNew.DataKeys[0]["NotificationID"]);

            for (int i = 0; i < gvNew.Rows.Count; i++)
            {
                if (gvNew.DataKeys[i] != null)
                {
                    CheckBox cbSelect = gvNew.Rows[i].Cells[0].FindControl("cbSelect") as CheckBox;
                    if (cbSelect != null)//add selected item
                    {
                        string detailsID = gvNew.DataKeys[i]["DetailsID"].ToString();
                        if (cbSelect.Checked)
                        {
                            View_StocktakeDetails details = new View_StocktakeDetails { DetailsID = long.Parse(detailsID) };
                            if (!noti.DetailsView.Exists(d => d.DetailsID == details.DetailsID))
                            {
                                noti.DetailsView.Add(details);
                            }
                        }
                        else//removed unselected item
                        {
                            int findIndex = noti.DetailsView.FindIndex(d => d.DetailsID.ToString() == detailsID);
                            if (findIndex >= 0)
                            {
                                noti.DetailsView.RemoveAt(findIndex);
                            }
                        }
                    }
                }
            }
            NotiToAnalyse = noti;
        }
    }

    private void BindData()
    {
        List<StocktakeStatus> list = StocktakeStatuses.Where(s => s.StatusID >= Consts.STOCKTAKE_COMPLETE).ToList();
        if (viewNewAnalyse.Visible)//(View == "viewNewAnalyse")
        {
            BindDataControl(ddlStatus, list, true);
        }
        else
        {
            if (viewAnalyse.Visible)
            {
                BindDataControl(ddlRptStatus, list, true);
            }
            else
            {
                BindDataControl(ddlMgrStatus, list, true);
            }
        }
    }

    protected void gvAnalysis_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
  
            rowHeader.CssClass = "gridViewHeaderStyle2";
            #region header template
          
            #endregion

            #region header
            Literal newCells = new Literal();
            if (View == "viewNewAnalyse")
            {

                newCells.Text = @"<input type='checkbox' id='cbSelectAll' onclick=""selectAll(gvAnalysis,'cbDelete');getSelectedItems();"" /></th>
                <th rowspan='{4}'>";
            }

            newCells.Text += @"差异报告编号</th>
                <th rowspan='{4}'>
                    通知单号</th>
                <th rowspan='{4}'>
                    零件号</th>
                <th rowspan='{4}'>
                    零件名称</th>
                <th rowspan='{4}'>
                    工厂</th>
                <th rowspan='{4}'>
                    DUNS</th>
                <th rowspan='{4}'>
                    盘点原因</th>
                <th rowspan='{4}'>
                    状态</th>
                <th rowspan='{4}'>
                    WIP</th>
                <th rowspan='{4}'>
                    分析</th>
                <th rowspan='{4}'>
                    M080</th>
                <th colspan='12'>
                    总计</th>
                <th colspan='12'>
                    SGM现场</th>
                <th colspan='12'>
                    RDC</th>
                <th colspan='12'>
                    返修</th>
                <th colspan='12'>
                    港口</th>{0}
                <th rowspan='{4}'>
                    差异报告生成时间</th>
                <th rowspan='{4}'>
                    分析人</th>
                <th rowspan='{4}'>
                    报告反馈时间</th>
                <th rowspan='{4}'>
                    分析原因</th>
                <th rowspan='{4}'>
                    车型</th>
                <th rowspan='{4}'>
                    供应商名称</th>
            </tr><tr class='gridViewHeaderStyle2'>";
            for (int i = 0; i < 5; i++)
            {
                newCells.Text += @"<th colspan='4'>
                    Available</th>
                <th colspan='4'>
                    QI</th>
                <th colspan='4'>
                    Block</th>";
            }
            newCells.Text += @"{1}</tr><tr class='gridViewHeaderStyle2'>";
            for (int i = 0; i < 15; i++)
            {
                newCells.Text += @"<th rowspan={5}>
                    系统值</th>
                <th rowspan='{5}'>
                    实际值</th>
                <th rowspan='{5}'>
                    差异值</th>
                <th rowspan='{5}'>
                    盈亏金额</th>";
                //<th rowspan='{5}'>
                //    调整值</th>";
            }
            newCells.Text += @"{2}</tr>{3}";
            #endregion

            string headerCSMT = string.Empty; // header of Consignment column
            string headerCSMTDUNS = string.Empty; //header of duns columns
            string headerCSMTAQB = string.Empty; //header of available/qi/block columns
            string headerCSMTValue = string.Empty;

            int startIndex = ReportColumnsList.IndexOf("AvailableItem");
            int endIndex = ReportColumnsList.IndexOf("QIItem");

            int dunsCount = endIndex - startIndex - 1;
            if (dunsCount > 0)
            {
                headerCSMT = @"<th colspan='" + dunsCount * 12 + @"'>外协</th>";
                headerCSMTValue += @"<tr class='gridViewHeaderStyle2'>";
                for (int i = 0; i < dunsCount; i++)
                {
                    headerCSMTAQB += @"
                <th colspan='4'>
                    Available</th>
                <th colspan='4'>
                    QI</th>
                <th colspan='4'>
                    Block</th>";
                    for (int j = 0; j < 3; j++)
                    {
                        headerCSMTValue += @"
                        <th >
                            系统值</th>
                        <th>
                            实际值</th>
                        <th>
                            差异值</th>
                        <th>
                            盈亏金额</th>";
                        //<th>
                        //    调整值</th>
                    }
                }
                headerCSMTValue = headerCSMTValue.Substring(0, headerCSMTValue.LastIndexOf("</th>"));
                for (int i = startIndex + 1; i < endIndex; i++)
                {
                    string colName = ReportColumnsList[i];

                    colName = colName.Substring(0, colName.IndexOf("_Available"));
                    headerCSMTDUNS += "<th colspan='12'>" + colName + "</th>";
                }
            }
            string headerRowspan = string.Empty;
            string subHeaderRowspan = string.Empty;
            if (dunsCount > 0)
            {
                headerRowspan = "4";
                subHeaderRowspan = "2";
            }
            else
            {

                headerRowspan = "3";
                subHeaderRowspan = "1";
            }
            newCells.Text = string.Format(newCells.Text, headerCSMT, headerCSMTDUNS, headerCSMTAQB, headerCSMTValue, headerRowspan, subHeaderRowspan);
            if (dunsCount <= 0)
            {
                newCells.Text = newCells.Text.Substring(0, newCells.Text.LastIndexOf("</th></tr>"));
            }


            TableCellCollection cells = e.Row.Cells;
            TableHeaderCell headerCell = new TableHeaderCell();

            if (dunsCount > 0)
            {
                headerCell.RowSpan = 4;
            }
            else
            {
                headerCell.RowSpan = 3;
            }
            headerCell.Controls.Add(newCells);
            rowHeader.Cells.Add(headerCell);

            rowHeader.Cells.Add(headerCell);
            rowHeader.Visible = true;

            gvAnalysis.Controls[0].Controls.AddAt(0, rowHeader);
        }
    }
    protected StocktakeNotification NotiToAnalyse
    {
        get
        {
            if (Session["NewAnalyses_Noti"] == null)
            {
                Session["NewAnalyses_Noti"] = new StocktakeNotification { DetailsView = new List<View_StocktakeDetails>() };
            }
            return Session["NewAnalyses_Noti"] as StocktakeNotification;
        }
        set
        {
            Session["NewAnalyses_Noti"] = value;
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "query":
                pagerNew.CurrentPage = 1;
                //pagerAnalysis.CurrentPage = 1;
                NotiToAnalyse = null;
                hdnDeleteItems.Value = string.Empty;
                Query();
                break;
            case "add":

                RefreshNotiToAnalyse();

                if (NotiToAnalyse.DetailsView.Count > 0)
                {
                    long reportID;
                    string reportCode;

                    Service.CreateAnalyseReport(NotiToAnalyse, CurrentUser.UserInfo, out reportCode, out reportID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CreateAnaSuccess", "alert('生成差异分析报告成功！')", true);
                }
                break;
            case "AddAll":
                long reportIDAll;
                string reportCodeAll;
                View_StocktakeDetails filter = BuildNewAnalyseCondition();

                Service.CreateAnalyseRptByCondition(filter, CurrentUser.UserInfo, out reportCodeAll, out reportIDAll);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CreateAnaSuccess", "alert('生成差异分析报告成功！')", true);
                Query();
                break;
            case "export":
                string notiCode = null;
                Query();
                if (!string.IsNullOrEmpty(txtMgrNoticeNo.Text.Trim()))
                {
                    notiCode = txtMgrNoticeNo.Text.Trim();
                }
                byte[] report = Service.ExportAnalyseReport(FilteredDetails);
                if (report != null)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    //string fileName = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMdd") + "差异分析结果(" + DateTime.Now.DayOfWeek + ").xls");
                    string fileName = notiCode + HttpUtility.UrlEncode("差异分析结果.xls");
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
                    Response.OutputStream.Write(report, 0, report.Length);
                    Response.Flush();
                }
                break;
            case "delete":

                try
                {
                    if (!string.IsNullOrEmpty(hdnDeleteItems.Value))
                    {
                        hdnDeleteItems.Value = hdnDeleteItems.Value.Substring(1, hdnDeleteItems.Value.Length - 2);
                        hdnDeleteItems.Value = hdnDeleteItems.Value.Replace(",,", ",");
                        List<string> idList = hdnDeleteItems.Value.Split(',').ToList();
                        List<DiffAnalyseReportItem> list = new List<DiffAnalyseReportItem>();
                        foreach (var id in idList)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                DiffAnalyseReportItem item = new DiffAnalyseReportItem { ItemID = Convert.ToInt64(id) };
                                list.Add(item);
                            }
                        }

                        Service.DeleteAnalyseItems(list);
                        hdnDeleteItems.Value = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    hdnDeleteItems.Value = string.Empty;
                    throw;
                }
                Query();
                break;
            case "analyse":

                //pagerNew.CurrentPage = 1;
                ////pagerAnalysis.CurrentPage = 1;
                //NotiToAnalyse = null;
                //hdnDeleteItems.Value = string.Empty;
                //Query();
                //if (gvAnalysis.Rows.Count > 0)
                //{
                Response.Redirect("AnalyseReportItem.aspx?NoticeNo=" + txtRptNoticeNo.Text.Trim());
                //}
                break;
            default:
                break;
        }
    }

    public View_StocktakeDetails BuildNewAnalyseCondition()
    {
        View_StocktakeDetails details = new View_StocktakeDetails();
        if (!string.IsNullOrEmpty(txtNoticeNo.Text.Trim()))
        {
            details.NotificationCode = txtNoticeNo.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtPartCName.Text.Trim()))
        {
            details.PartChineseName = txtPartCName.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txtPartNo.Text.Trim()))
        {
            details.PartCode = txtPartNo.Text.Trim();
        }
        return details;
    }

    public List<int> FilteredDetails
    {
        get
        {
            return Session["Rpt_FilteredDetails"] as List<int>;
        }
        set
        {
            Session["Rpt_FilteredDetails"] = value;
        }
    }

    public void Query()
    {
        if (viewNewAnalyse.Visible && rblOption.Items[0].Selected)//View == "viewNewAnalyse"
        {

            if (string.IsNullOrEmpty(txtNoticeNo.Text.Trim()))
            {
                return;
            }
            View_StocktakeDetails filter = BuildNewAnalyseCondition();
            int itemCount;
            int pageCount;
            List<View_StocktakeDetails> detailsList = Service.QueryFullFilledNotiDetailsByPage(filter, pagerNew.PageSize, pagerNew.CurrentPage, out pageCount, out itemCount);
            pagerNew.TotalRecord = itemCount;
            BindDataControl(gvNew, detailsList);
        }
        else
        {
            View_DifferenceAnalyse filter = new View_DifferenceAnalyse();
            DateTime? timeStart = null;
            DateTime? timeEnd = null;
            if (viewAnalyse.Visible)//(View == "Analyse")
            {
                if (!string.IsNullOrEmpty(txtRptNoticeNo.Text.Trim()))
                {
                    filter.NotificationCode = txtRptNoticeNo.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtRptFU.Text.Trim()))
                {
                    filter.FollowUp = txtRptFU.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtRptPartCName.Text.Trim()))
                {
                    filter.PartChineseName = txtRptPartCName.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtRptPartNo.Text.Trim()))
                {
                    filter.PartCode = txtRptPartNo.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtRptSpecs.Text.Trim()))
                {
                    filter.Specs = txtRptSpecs.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtRptTimeFrom.Text.Trim()))
                {
                    timeStart = Convert.ToDateTime(txtRptTimeFrom.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtRptTimeTo.Text.Trim()))
                {
                    timeEnd = Convert.ToDateTime(txtRptTimeTo.Text.Trim());
                }
                if (!string.IsNullOrEmpty(ddlRptStatus.SelectedValue))
                {
                    filter.Status = int.Parse(ddlRptStatus.SelectedValue);
                }
                if (!string.IsNullOrEmpty(ddlRptPlant.SelectedValue))
                {
                    filter.PartPlantCode = ddlRptPlant.SelectedItem.Text;
                }
                if (!string.IsNullOrEmpty(ddlRptWorkshop.SelectedValue))
                {
                    filter.Workshops = ddlRptWorkshop.SelectedItem.Text;
                }
                if (!string.IsNullOrEmpty(ddlRptSegment.SelectedValue))
                {
                    filter.Segments = ddlRptSegment.SelectedItem.Text;
                }
            }
            else
            {
                if (viewNewAnalyse.Visible)
                {
                    if (!string.IsNullOrEmpty(txtNoticeNo.Text))
                    {
                        filter.NotificationCode = txtNoticeNo.Text;
                    }
                    else
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtPartCName.Text))
                    {
                        filter.PartChineseName = txtPartCName.Text;
                    }
                    if (!string.IsNullOrEmpty(txtPartNo.Text))
                    {
                        filter.PartCode = txtPartNo.Text;
                    }
                    if (!string.IsNullOrEmpty(txtSpecs.Text))
                    {
                        filter.Specs = txtSpecs.Text;
                    }
                    if (!string.IsNullOrEmpty(txtFollowUp.Text))
                    {
                        filter.FollowUp = txtFollowUp.Text;
                    }
                    if (!string.IsNullOrEmpty(txtAnalyzer.Text))
                    {
                        filter.AnalysedByUser = txtAnalyzer.Text;
                    }
                    if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
                    {
                        filter.Status = int.Parse(ddlStatus.SelectedValue);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtMgrNoticeNo.Text))
                    {
                        filter.NotificationCode = txtMgrNoticeNo.Text;
                    }
                    if (!string.IsNullOrEmpty(txtMgrPartCName.Text))
                    {
                        filter.PartChineseName = txtMgrPartCName.Text;
                    }
                    if (!string.IsNullOrEmpty(txtMgrPartNo.Text))
                    {
                        filter.PartCode = txtMgrPartNo.Text;
                    }
                    if (!string.IsNullOrEmpty(txtMgrSpecs.Text))
                    {
                        filter.Specs = txtMgrSpecs.Text;
                    }
                    if (!string.IsNullOrEmpty(txtMgrFollowUp.Text))
                    {
                        filter.FollowUp = txtMgrFollowUp.Text;
                    }
                    if (!string.IsNullOrEmpty(txtMgrAnalyzer.Text))
                    {
                        filter.AnalysedByUser = txtMgrAnalyzer.Text;
                    }
                    if (!string.IsNullOrEmpty(ddlMgrStatus.SelectedValue))
                    {
                        filter.Status = int.Parse(ddlMgrStatus.SelectedValue);
                    }
                    if (ddlDiff.SelectedIndex > 0)
                    {
                        int amount;
                        if (!string.IsNullOrEmpty(txtDiffAmount.Text.Trim()))
                        {

                            amount = int.Parse(ddlDiff.SelectedItem.Value + txtDiffAmount.Text.Trim(), NumberStyles.AllowLeadingSign);
                            filter.DiffAmount = amount;
                            if (amount >= 0)
                            {
                                filter.DiffFilter = ddiDiffAmountOp.SelectedItem.Text + amount;
                            }
                            else
                            {
                                filter.DiffFilter = ddiDiffAmountOp.SelectedValue + amount;
                            }
                        }

                        decimal sum;
                        if (!string.IsNullOrEmpty(txtDiffSum.Text.Trim()))
                        {

                            sum = decimal.Parse(ddlDiff.SelectedItem.Value + txtDiffSum.Text.Trim(), NumberStyles.AllowLeadingSign);
                            filter.DiffSumFilter = sum + "";
                            if (sum >= 0)
                            {
                                filter.DiffSumFilter = ddiDiffSumOp.SelectedItem.Text + sum;
                            }
                            else
                            {
                                filter.DiffFilter = ddiDiffSumOp.SelectedValue + sum;
                            }
                        }

                        filter.ProfitLossFilter = (ddlDiff.SelectedValue == "+") ? ">0" : "<0";

                        if (!string.IsNullOrEmpty(txtDiffTimes.Text.Trim()))
                        {
                            int times = int.Parse(txtDiffTimes.Text.Trim());
                            filter.TimesFilter = ddlDiffTimesOp.SelectedValue + times;
                        }
                    }
                }
            }
            
            DataTable dt = Service.QueryAnalyseReport(filter, timeStart, timeEnd).Tables[0];
            FilteredDetails = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                FilteredDetails.Add(int.Parse(row["DetailsID"].ToString()));
            }

            ReportColumnsList = new List<string>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ReportColumnsList.Add(dt.Columns[i].ColumnName);
            }
            int startIndex = ReportColumnsList.IndexOf("AvailableItem");
            int endIndex = ReportColumnsList.IndexOf("QIItem");
            int dunsCount = endIndex - startIndex - 1;

            #region add dynamic columns
            if (dunsCount > 0)
            {
                for (int i = startIndex + 1; i < endIndex; i++)
                {
                    //available
                    BoundField fieldAvailable_Sys = new BoundField();
                    fieldAvailable_Sys.DataField = ReportColumnsList[i] + "_Sys";
                    fieldAvailable_Sys.DataFormatString = "{0:#.####}";

                    BoundField fieldAvailable_Value = new BoundField();
                    fieldAvailable_Value.DataField = ReportColumnsList[i];
                    fieldAvailable_Value.DataFormatString = "{0:#.####}";

                    BoundField fieldAvailable_DiffSum = new BoundField();
                    fieldAvailable_DiffSum.DataField = ReportColumnsList[i] + "_DiffSum";
                    fieldAvailable_DiffSum.DataFormatString = "{0:c}";

                    BoundField fieldAvailable_Diff = new BoundField();
                    fieldAvailable_Diff.DataField = ReportColumnsList[i] + "_Diff";
                    fieldAvailable_Diff.DataFormatString = "{0:#.####}";

                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldAvailable_Sys);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldAvailable_Value);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldAvailable_Diff);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldAvailable_DiffSum);
                    //gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 4, fieldAvailable_Adjust);

                    //qi
                    BoundField fieldQI_Sys = new BoundField();
                    fieldQI_Sys.DataField = ReportColumnsList[i + dunsCount + 1] + "_Sys";
                    fieldQI_Sys.DataFormatString = "{0:#.####}";

                    BoundField fieldQI_Value = new BoundField();
                    fieldQI_Value.DataField = ReportColumnsList[i + dunsCount + 1];
                    fieldQI_Value.DataFormatString = "{0:#.####}";

                    BoundField fieldQI_DiffSum = new BoundField();
                    fieldQI_DiffSum.DataField = ReportColumnsList[i + dunsCount + 1] + "_DiffSum";
                    fieldQI_DiffSum.DataFormatString = "{0:c}";

                    BoundField fieldQI_Diff = new BoundField();
                    fieldQI_Diff.DataField = ReportColumnsList[i + dunsCount + 1] + "_Diff";
                    fieldQI_Diff.DataFormatString = "{0:#.####}";

                    //BoundField fieldQI_Adjust = new BoundField();
                    //fieldQI_Adjust.DataField = ReportColumnsList[i + dunsCount + 1] + "_Adjust";

                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldQI_Sys);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldQI_Value);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldQI_Diff);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldQI_DiffSum);
                    //gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 4, fieldQI_Adjust);


                    //block
                    BoundField fieldBlk_Sys = new BoundField();
                    fieldBlk_Sys.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Sys";
                    fieldBlk_Sys.DataFormatString = "{0:#.####}";

                    BoundField fieldBlk_Value = new BoundField();
                    fieldBlk_Value.DataField = ReportColumnsList[i + 2 * dunsCount + 2];
                    fieldBlk_Value.DataFormatString = "{0:#.####}";

                    BoundField fieldBlk_DiffSum = new BoundField();
                    fieldBlk_DiffSum.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_DiffSum";
                    fieldBlk_DiffSum.DataFormatString = "{0:c}";

                    BoundField fieldBlk_Diff = new BoundField();
                    fieldBlk_Diff.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Diff";
                    fieldBlk_Diff.DataFormatString = "{0:#.####}";
                    //BoundField fieldBlk_Adjust = new BoundField();
                    //fieldBlk_Adjust.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Adjust";

                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldBlk_Sys);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldBlk_Value);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldBlk_Diff);
                    gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 6, fieldBlk_DiffSum);
                    //gvAnalysis.Columns.Insert(gvAnalysis.Columns.Count - 4, fieldBlk_Adjust);
                }
            }
            #endregion

            PagedDataSource ds = new PagedDataSource();
            ds.AllowPaging = true;
            ds.DataSource = dt.DefaultView;
            ds.PageSize = pagerReport.PageSize;
            ds.CurrentPageIndex = pagerReport.CurrentPage - 1;
            pagerReport.TotalRecord = dt.DefaultView.Count;
            BindDataControl(gvAnalysis, ds);//dt);

        }
    }
    protected void rblOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnDeleteItems.Value = string.Empty;
        NotiToAnalyse = null;
        if (rblOption.SelectedIndex == 0)
        {
            txtSpecs.Enabled = false;
            txtFollowUp.Enabled = false;
            txtAnalyzer.Enabled = false;
            ddlStatus.Enabled = false;
            ddlStatus.SelectedIndex = 0;

            Toolbar1.Items[1].Enabled = true;
            Toolbar1.Items[2].Enabled = false;
            Toolbar1.Items[3].Enabled = false;
            Toolbar1.Items[5].Enabled = true;
            gvNew.DataBind();
        }
        else
        {
            txtSpecs.Enabled = true;
            txtFollowUp.Enabled = true;
            txtAnalyzer.Enabled = true;
            ddlStatus.Enabled = true;


            Toolbar1.Items[1].Enabled = false;
            Toolbar1.Items[2].Enabled = true;
            Toolbar1.Items[3].Enabled = true;
            Toolbar1.Items[5].Enabled = false;
        }
        mvNewReport.ActiveViewIndex = rblOption.SelectedIndex;

    }
    protected void gvNew_PreRender(object sender, EventArgs e)
    {
        List<View_StocktakeDetails> list = new List<View_StocktakeDetails> { new View_StocktakeDetails() };
        BindEmptyGridView(gvNew, list);
    }
    protected void gvAnalysis_PreRender(object sender, EventArgs e)
    {

    }

    protected void gvNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            View_StocktakeDetails details = e.Row.DataItem as View_StocktakeDetails;
            if (details != null && details.NotificationID != null)
            {
                CheckBox cbSelect = e.Row.Cells[0].FindControl("cbSelect") as CheckBox;
                if (cbSelect != null)
                {
                    if (NotiToAnalyse.DetailsView.Exists(d => d.DetailsID == details.DetailsID))
                    {
                        cbSelect.Checked = true;
                    }
                }
            }
        }
    }
    protected void gvAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cbDelete = e.Row.Cells[0].FindControl("cbDelete") as CheckBox;
            if (cbDelete != null)
            {
                string itemID = gvAnalysis.DataKeys[e.Row.RowIndex].Value.ToString();
                cbDelete.Attributes["itemID"] = itemID;
                cbDelete.Attributes["onclick"] =
                    "selectItem('cbDelete', 'cbSelectAll', gvAnalysis);if($get('" + cbDelete.ClientID + "').checked){ $get('" + hdnDeleteItems.ClientID + "').value+=','+$get('" + cbDelete.ClientID + "').parentElement.itemID+',';}"
                    + " else {$get('" + hdnDeleteItems.ClientID + "').value=$get('" + hdnDeleteItems.ClientID + "').value.replace(','+$get('" + cbDelete.ClientID + "').parentElement.itemID+',','');}";
            }
        }
    }
    protected void ddlRptPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlRptPlant.SelectedValue))
        {
            BindWorkshops(ddlRptWorkshop, new Plant { PlantID = Convert.ToInt32(ddlRptPlant.SelectedValue) }, true);
            ddlRptWorkshop.SelectedIndex = 0;
        }
    }
    protected void ddlRptWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlRptWorkshop.SelectedValue))
        {
            BindSegments(ddlRptSegment, new Workshop { WorkshopID = Convert.ToInt32(ddlRptWorkshop.SelectedValue) });
            ddlRptSegment.SelectedIndex = 0;
        }
    }

}
