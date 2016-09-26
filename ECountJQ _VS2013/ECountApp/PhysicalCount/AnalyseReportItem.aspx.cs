using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Data;
using System.Drawing;
using SGM.Common.Utility;

public partial class PhysicalCount_AnalyseReportItem : ECountBasePage
{
    public int CurrentItemIndex
    {
        get
        {
            if (ViewState["CurrentItemIndex"] == null)
            {
                ViewState["CurrentItemIndex"] = 0;
            }
            return (int)ViewState["CurrentItemIndex"];
        }
        set
        { ViewState["CurrentItemIndex"] = value; }
    }

    //public long? ItemID
    //{
    //    get
    //    {
    //        if (Session["AnalyseReportItem_ItemID"] == null)
    //        {
    //            return null;
    //        }
    //        return (long?)Session["AnalyseReportItem_ItemID"];
    //    }
    //    set
    //    {
    //        Session["AnalyseReportItem_ItemID"] = value;
    //    }
    //}

    public long? ReportID
    {
        get
        {
            long result;
            if (long.TryParse(Request.QueryString["ReportID"] + "", out result))
            {
                long? reportId = result;
                return reportId;
            }
            return null;
        }
    }


    public string NoticeNo
    {
        get
        {

            return Request.QueryString["NoticeNo"];
        }
    }

    public List<string> ReportColumnsList
    {
        get
        {
            if (Session["AnalyseReportItem_ColumnsList"] == null)
            {
                Session["AnalyseReportItem_ColumnsList"] = new List<string>();
            }
            return Session["AnalyseReportItem_ColumnsList"] as List<string>;
        }
        set
        {
            Session["AnalyseReportItem_ColumnsList"] = value;
        }
    }

    public List<DiffAnalyseReportItem> ReportItems
    {
        get
        {
            if (Session["AnalyseReportItem_Items"] == null)
            {
                Session["AnalyseReportItem_Items"] = new List<DiffAnalyseReportItem>();
            }
            return Session["AnalyseReportItem_Items"] as List<DiffAnalyseReportItem>;
        }
        set
        {
            Session["AnalyseReportItem_Items"] = value;
        }
    }

    //public List<View_DiffAnalyseReportDetails> Details
    //{
    //    get
    //    {
    //        if (Session["AnalyseReportItem_Details"]==null)
    //        {
    //            Session["AnalyseReportItem_Details"] = new List<View_DiffAnalyseReportDetails>();
    //        }
    //        return Session["AnalyseReportItem_Details"] as List<View_DiffAnalyseReportDetails>;
    //    }
    //    set
    //    {
    //        Session["AnalyseReportItem_Details"] = value;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ReportItems = null;
            ReportColumnsList = null;
            List<DiffAnalyseReport> reports = new List<DiffAnalyseReport>();
            if (!string.IsNullOrEmpty(NoticeNo))//(ReportID != null)
            {
                reports = Service.GetDiffAnalyseReportsByNoti(new StocktakeNotification { NotificationCode = NoticeNo });//.GetAnalyseReport(ReportID.Value);
            }
            else
            {
                reports = Service.GetDiffAnalyseReports();
            }
            ReportItems = reports.SelectMany(r => r.DiffAnalyseReportItem).ToList();
            if (reports.Count!=0)
            {
                BindData();
                SetNaviButtonsStatus();
            }
        }
    }

    public void BindData()
    {
        BindReport();
        BindItem();
        BindDetails();
    }

    public void BindReport()
    {
        //List<DiffAnalyseReportItem> listReport = new List<DiffAnalyseReportItem> { Items[CurrentItemIndex].DiffAnalyseReport };
        fvTitle.PageIndex = CurrentItemIndex;
        BindDataControl(fvTitle, ReportItems);
    }
    private bool _nodifference = false;
    private void BindItem()
    {
        View_DifferenceAnalyse filter = new View_DifferenceAnalyse { ItemID = ReportItems[CurrentItemIndex].ItemID };
        DataTable dt = Service.QueryAnalyseReport(filter, null, null).Tables[0];

        BindDataControl(fvItem, dt);
        if (dt.Rows.Count > 0 && ((dt.Rows[0]["AvailableDiff"] == DBNull.Value || (decimal)dt.Rows[0]["AvailableDiff"] == 0) && (dt.Rows[0]["QIDiff"] == DBNull.Value || (decimal)dt.Rows[0]["QIDiff"] == 0) && (dt.Rows[0]["BlockDiff"] == DBNull.Value || (decimal)dt.Rows[0]["BlockDiff"] == 0)))
        {
            _nodifference = true;
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

                BoundField fieldAvailable_Value = new BoundField();
                fieldAvailable_Value.DataField = ReportColumnsList[i];

                //BoundField fieldAvailable_DiffSum = new BoundField();
                //fieldAvailable_DiffSum.DataField = ReportColumnsList[i] + "_DiffSum";
                //fieldAvailable_DiffSum.DataFormatString = "c";
                BoundField fieldAvailable_Diff = new BoundField();
                fieldAvailable_Diff.DataField = ReportColumnsList[i] + "_Diff";

                BoundField fieldAvailable_Adjust = new BoundField();
                fieldAvailable_Adjust.DataField = ReportColumnsList[i] + "_Adjust";

                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldAvailable_Sys);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldAvailable_Value);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldAvailable_Diff);
                //gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldAvailable_DiffSum);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldAvailable_Adjust);

                //qi
                BoundField fieldQI_Sys = new BoundField();
                fieldQI_Sys.DataField = ReportColumnsList[i] + "_Sys";

                BoundField fieldQI_Value = new BoundField();
                fieldQI_Value.DataField = ReportColumnsList[i + dunsCount + 1];

                //BoundField fieldQI_DiffSum = new BoundField();
                //fieldQI_DiffSum.DataField = ReportColumnsList[i + dunsCount + 1] + "_DiffSum";
                //fieldQI_DiffSum.DataFormatString = "C";

                BoundField fieldQI_Diff = new BoundField();
                fieldQI_Diff.DataField = ReportColumnsList[i + dunsCount + 1] + "_Diff";

                BoundField fieldQI_Adjust = new BoundField();
                fieldQI_Adjust.DataField = ReportColumnsList[i + dunsCount + 1] + "_Adjust";

                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldQI_Sys);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldQI_Value);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldQI_Diff);
                //gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldQI_DiffSum);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldQI_Adjust);


                //block
                BoundField fieldBlk_Sys = new BoundField();
                fieldBlk_Sys.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Sys";

                BoundField fieldBlk_Value = new BoundField();
                fieldBlk_Value.DataField = ReportColumnsList[i + 2 * dunsCount + 2];

                //BoundField fieldBlk_DiffSum = new BoundField();
                //fieldBlk_DiffSum.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_DiffSum";
                //fieldBlk_DiffSum.DataFormatString = "C";

                BoundField fieldBlk_Diff = new BoundField();
                fieldBlk_Diff.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Diff";

                BoundField fieldBlk_Adjust = new BoundField();
                fieldBlk_Adjust.DataField = ReportColumnsList[i + 2 * dunsCount + 2] + "_Adjust";

                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldBlk_Sys);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldBlk_Value);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldBlk_Diff);
                //gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldBlk_DiffSum);
                gvItem.Columns.Insert(gvItem.Columns.Count - 4, fieldBlk_Adjust);
            }
        }
        #endregion

        BindDataControl(gvItem, dt);
    }

    private void BindDetails()
    {
        List<View_DiffAnalyseReportDetails> details = new List<View_DiffAnalyseReportDetails>();
        if (ReportItems[CurrentItemIndex].DetailsView == null)
        {
            UserGroup group=null;
            if (CurrentUser.UserInfo.UserGroup.AnalyzeAll == null || !CurrentUser.UserInfo.UserGroup.AnalyzeAll.Value)            
            {
                group = CurrentUser.UserInfo.UserGroup;
            }
            details = Service.GetDiffAnalyseRptDetails(ReportItems[CurrentItemIndex],group);
            ReportItems[CurrentItemIndex].DetailsView = details;
        }
        else
        {
            details = ReportItems[CurrentItemIndex].DetailsView;
        }
        BindDataControl(gvDetails, details);
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                CacheDetails();
                SaveAll();
                BindItem();
                BindDetails();
                break;
            case "return":
                Response.Redirect("NewAnalyses.aspx?view=viewAnalyse");
                break;
            case "previous":
                NavigateToPrevoius();
                break;
            case "next":
                NavigateToNext();
                break;
            default:
                break;
        }
    }

    private void SaveAll()
    {
        List<View_DiffAnalyseReportDetails> detailsList = ReportItems.Where(i => i.DetailsView != null).SelectMany(i => i.DetailsView).ToList();
        Service.SaveAnalyseReport(detailsList);
    }


    private void Save()
    {
        List<View_DiffAnalyseReportDetails> detailsList = ReportItems[CurrentItemIndex].DetailsView;
        Service.SaveAnalyseReport(detailsList);

    }

    private void NavigateToPrevoius()
    {
        //CheckBox cbAutoSave = fvTitle.FindControl("cbAutoSave") as CheckBox;
        //CacheDetails();
        //if (cbAutoSave.Checked)
        //{
        //    Save();
        //}

        //CurrentItemIndex--;
        //BindData();
        //SetNaviButtonsStatus();

        Navigate(CurrentItemIndex - 1);
    }


    private void NavigateToNext()
    {
        //CheckBox cbAutoSave = fvTitle.FindControl("cbAutoSave") as CheckBox;
        //CacheDetails();
        //if (cbAutoSave.Checked)
        //{
        //    Save();
        //}
        //CurrentItemIndex++;
        //BindData();

        //SetNaviButtonsStatus();
        Navigate(CurrentItemIndex + 1);
    }
    private void Navigate(int index)
    {
        CacheDetails();
        if (cbAutoSave.Checked && ReportItems[CurrentItemIndex].DiffAnalyseReport.Status >= Consts.STOCKTAKE_NEW_ANALYSIS && ReportItems[CurrentItemIndex].DiffAnalyseReport.Status < Consts.STOCKTAKE_COMPLETE)
        {
            Save();
        }

        CurrentItemIndex = (index >= ReportItems.Count || index < 0) ? CurrentItemIndex : index;
        BindData();

        SetNaviButtonsStatus();
    }
    private void CacheDetails()
    {
        if (ReportItems[CurrentItemIndex].DiffAnalyseReport.Status > Consts.STOCKTAKE_ANALYZING)//completed item shouldn't be updated
        {
            return;
        }
        List<View_DiffAnalyseReportDetails> detailsList = ReportItems[CurrentItemIndex].DetailsView;
        for (int i = 0; i < gvDetails.Rows.Count; i++)
        {
            long detailsID = (long)gvDetails.DataKeys[i].Value;
            CheckBox cbIsOK = gvDetails.Rows[i].FindControl("cbIsOK") as CheckBox;
            TextBox txtComments = gvDetails.Rows[i].FindControl("txtComments") as TextBox;
            int index = ReportItems[CurrentItemIndex].DetailsView.FindIndex(d => d.DetailsID == detailsID);
            ReportItems[CurrentItemIndex].DetailsView[index].Comment = (string.IsNullOrEmpty(txtComments.Text.Trim())) ? null : txtComments.Text.Trim();
            ReportItems[CurrentItemIndex].DetailsView[index].Passed = cbIsOK.Checked;
        }
    }

    private void SetNaviButtonsStatus()
    {
        Toolbar1.Items[2].Enabled = (CurrentItemIndex > 0);
        Toolbar1.Items[3].Enabled = (CurrentItemIndex < ReportItems.Count - 1);
        //Label lblSummary = fvTitle.FindControl("lblSummary") as Label;
        lblSummary.Text = string.Format("第{0}个差异分析零件，共{1}个", CurrentItemIndex + 1, ReportItems.Count);
    }

    protected void gvItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            string HeaderBackColor = "#5D7B9D";
            rowHeader.BackColor = ColorTranslator.FromHtml(HeaderBackColor);
            rowHeader.ForeColor = Color.White;

            #region header template
            //<tr><th rowspan='4'>
            //        删除</th>
            //    <th rowspan='4'>
            //        差异报告编号</th>
            //    <th rowspan='4'>
            //        通知单号</th>
            //    <th rowspan='4'>
            //        零件号</th>
            //    <th rowspan='4'>
            //        零件名称</th>
            //    <th rowspan='4'>
            //        工厂</th>
            //    <th rowspan='4'>
            //        DUNS</th>
            //    <th rowspan='4'>
            //        盘点原因</th>
            //    <th rowspan='4'>
            //        状态</th>
            //    <th colspan='15'>
            //        总计</th>
            //    <th rowspan='4'>
            //        差异报告生成时间</th>
            //    <th rowspan='4'>
            //        分析人</th>
            //    <th rowspan='4'>
            //        报告反馈时间</th>
            //    <th rowspan='4'>
            //        分析时间</th>
            //</tr>
            //<tr>
            //    <th colspan='5'>
            //        Available</th>
            //    <th colspan='5'>
            //        QI</th>
            //    <th colspan='5'>
            //        Block</th>
            //</tr>
            //<tr>
            //    <th rowspan='2'>
            //        系统值</th>
            //    <th rowspan='2'>
            //        实际值</th>
            //    <th rowspan='2'>
            //        差异值</th>
            //    <th rowspan='2'>
            //        盈亏金额</th>
            //    <th rowspan='2'>
            //        调整值</th>
            //    <th rowspan='2'>
            //        系统值</th>
            //    <th rowspan='2'>
            //        实际值</th>
            //    <th rowspan='2'>
            //        差异值</th>
            //    <th rowspan='2'>
            //        盈亏金额</th>
            //    <th rowspan='2'>
            //        调整值</th>
            //    <th rowspan='2'>
            //        系统值</th>
            //    <th rowspan='2'>
            //        实际值</th>
            //    <th rowspan='2'>
            //        差异值</th>
            //    <th rowspan='2'>
            //        盈亏金额</th>
            //    <th rowspan='2'>
            //        调整值</th>
            //</tr>
            //<tr>
            //    <th>
            //        库位</th>
            //    <th>
            //        线旁</th>
            //    <th>
            //        加工区</th>
            //</tr>
            #endregion

            #region header
            Literal newCells = new Literal();
            newCells.Text = @"零件号</th>
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
                <th colspan='12'>
                    总计</th>
                <th colspan='12'>
                    SGM现场</th>
                <th colspan='12'>
                    RDC</th>
                <th colspan='12'>
                    返修</th>{0}
                <th rowspan='{4}'>
                    差异报告生成时间</th>
                <th rowspan='{4}'>
                    分析人</th>
                <th rowspan='{4}'>
                    报告反馈时间</th>
                <th rowspan='{4}'>
                    分析时间</th>
            </tr><tr>";
            for (int i = 0; i < 4; i++)
            {
                newCells.Text += @"<th colspan='4'>
                    Available</th>
                <th colspan='4'>
                    QI</th>
                <th colspan='4'>
                    Block</th>";
            }
            newCells.Text += @"{1}</tr><tr>";
            for (int i = 0; i < 12; i++)
            {
                newCells.Text += @"<th rowspan={5}>
                    系统值</th>
                <th rowspan='{5}'>
                    实际值</th>
                <th rowspan='{5}'>
                    差异值</th>
                <th rowspan='{5}'>
                    调整值</th>";
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
                headerCSMTValue += @"<tr>";
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
                            调整值</th>";
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

            gvItem.Controls[0].Controls.AddAt(0, rowHeader);
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cbIsOK = e.Row.FindControl("cbIsOK") as CheckBox;
            TextBox txtComments = e.Row.FindControl("txtComments") as TextBox;

            if (ReportItems[CurrentItemIndex].DiffAnalyseReport.Status > Consts.STOCKTAKE_ANALYZING)
            {
                cbIsOK.Enabled = false;
                txtComments.Enabled = false;
            }
            else
            {
                cbIsOK.Enabled = true;
                txtComments.Enabled = true;
            }
            if (_nodifference)//available/QI/BLOCK no difference, set as isOK
            {
                //no comments && nodifference, set as isOK
                if (string.IsNullOrEmpty(txtComments.Text.Trim()))
                {
                    cbIsOK.Checked = true;
                }
            }

            if (cbIsOK.Checked == true)
            {
                txtComments.Style[HtmlTextWriterStyle.Visibility] = "hidden";
            }
            cbIsOK.Attributes["onclick"] = "if(document.getElementById('" + cbIsOK.ClientID + "').checked) {document.getElementById('" + txtComments.ClientID + "').style.visibility='hidden';document.getElementById('" + txtComments.ClientID + "').value='';}else{document.getElementById('" + txtComments.ClientID + "').style.visibility='visible';}";
        }
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        MergeCell(gvDetails, 0, new List<int> { 0 }, Color.FromName("#333333"), Color.FromName("#284775"), Color.FromName("#F7F6F3"), Color.FromName("White"));
    }

}
