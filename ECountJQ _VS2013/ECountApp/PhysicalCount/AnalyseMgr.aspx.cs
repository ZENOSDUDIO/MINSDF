using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using SGM.ECount.DataModel;
using SGM.Common.Utility;

public partial class PhysicalCount_AnalyseMgr : ECountBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindData();
            if (Mode == PageMode.View)
            {
                Toolbar1.Visible = false;
            }
        }
    }


    private void BindData()
    {
        View_StocktakeResult filter = new View_StocktakeResult { Status = Consts.STOCKTAKE_ANALYZING };
        if (!string.IsNullOrEmpty(CurrentUser.UserInfo.UserGroup.DUNS))
        {
            string duns = CurrentUser.UserInfo.UserGroup.DUNS;
            filter.CSMTDUNS = duns;
            filter.RepairDUNS = duns;
            filter.DUNS = duns;
        }
        if (CurrentUser.UserInfo.Workshop != null)
        {
            if (CurrentUser.UserInfo.Workshop.Plant != null)
            {
                filter.PlantID = CurrentUser.UserInfo.Workshop.Plant.PlantID;
            }
            if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Workshop.WorkshopCode))
            {
                filter.Workshops = CurrentUser.UserInfo.Workshop.WorkshopCode;
            }
        }

        List<View_StocktakeResult> list = Service.GetStocktakeResult(filter,true);
        if (list.Count > 0)
        {
            //NotificationNo = list[0].NotificationCode;
            BindDataControl(gvItems, list);
        }
    }

    protected void gvItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            string HeaderBackColor = "#5D7B9D";
            rowHeader.BackColor = ColorTranslator.FromHtml(HeaderBackColor);
            rowHeader.ForeColor = Color.White;

            #region header template
            //<tr><th rowspan='4'>
            //        零件号</th>
            //    <th rowspan='4'>
            //        工厂</th>
            //    <th rowspan='4'>
            //        零件名称</th>
            //    <th rowspan='4'>
            //        DUNS</th>
            //    <th rowspan='4'>
            //        物料类别</th>
            //    <th rowspan='4'>
            //        循环级别</th>
            //    <th rowspan='4'>
            //        申请单号</th>
            //    <th rowspan='4'>
            //        申请人</th>
            //    <th rowspan='4'>
            //        盘点类别</th>
            //    <th colspan='23'>
            //        盘点结果</th>
            //    <th rowspan='4'>
            //        总数</th>
            //    <th rowspan='4'>
            //        备注</th>
            //</tr>
            //<tr>
            //    <th colspan='9'>
            //        SGM现场</th>
            //    <th colspan='3'>
            //        RDC</th>
            //    <th colspan='4'>
            //        返修</th>
            //    <th colspan='4'>
            //        外协</th>
            //    <th colspan='3'>
            //        港口</th>
            //</tr>
            //<tr>
            //    <th colspan='3'>
            //        Available</th>
            //    <th rowspan='2'>
            //        QI</th>
            //    <th rowspan='2'>
            //        Block</th>
            //    <th rowspan='2'>
            //        起点CSN</th>
            //    <th rowspan='2'>
            //        终点CSN</th>
            //    <th rowspan='2'>
            //        车间</th>
            //    <th rowspan='2'>
            //        工段</th>
            //    <th rowspan='2'>
            //        Available</th>
            //    <th rowspan='2'>
            //        QI</th>
            //    <th rowspan='2'>
            //        Block</th>
            //    <th rowspan='2'>
            //        Available</th>
            //    <th rowspan='2'>
            //        QI</th>
            //    <th rowspan='2'>
            //        Block</th>
            //    <th rowspan='2'>
            //        DUNS</th>
            //    <th rowspan='2'>
            //        Available</th>
            //    <th rowspan='2'>
            //        QI</th>
            //    <th rowspan='2'>
            //        Block</th>
            //    <th rowspan='2'>
            //        DUNS</th>
            //    <th rowspan='2'>
            //        Available</th>
            //    <th rowspan='2'>
            //        QI</th>
            //    <th rowspan='2'>
            //        Block</th>
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
                <th rowspan='3'>
                    工厂</th>
                <th rowspan='3'>
                    零件名称</th>
                <th rowspan='3'>
                    DUNS</th>
                <th rowspan='3'>
                    物料类别</th>
                <th rowspan='3'>
                    循环级别</th>
                <th rowspan='3'>
                    申请单号</th>
                <th rowspan='3'>
                    申请人</th>
                <th rowspan='3'>
                    盘点类别</th>
                <th colspan='21'>
                    盘点结果</th>
                <th rowspan='3'>
                    总数</th>
                <th rowspan='3'>
                    备注</th>
            </tr>
            <tr>
                <th colspan='7'>
                    SGM现场调整值</th>
                <th colspan='3'>
                    RDC调整值</th>
                <th colspan='4'>
                    返修调整值</th>
                <th colspan='4'>
                    外协调整值</th>
                <th colspan='3'>
                    港口调整值</th>
            </tr>
            <tr>
                <th>
                    Available</th>
                <th >
                    QI</th>
                <th >
                    Block</th>
                <th>
                    起点CSN</th>
                <th>
                    终点CSN</th>
                <th>
                    车间</th>
                <th>
                    工段</th>
                <th>
                    Available</th>
                <th>
                    QI</th>
                <th>
                    Block</th>
                <th>
                    Available</th>
                <th>
                    QI</th>
                <th>
                    Block</th>
                <th>
                    DUNS</th>
                <th>
                    Available</th>
                <th>
                    QI</th>
                <th>
                    Block</th>
                <th>
                    DUNS</th>
                <th>
                    Available</th>
                <th>
                    QI</th>
                <th>
                    Block";
            #endregion

            TableCellCollection cells = e.Row.Cells;
            TableHeaderCell headerCell = new TableHeaderCell();

            headerCell.RowSpan = 3;
            headerCell.Controls.Add(newCells);
            rowHeader.Cells.Add(headerCell);

            rowHeader.Cells.Add(headerCell);
            rowHeader.Visible = true;

            gvItems.Controls[0].Controls.AddAt(0, rowHeader);

        }
    }
    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            View_StocktakeResult item = e.Row.DataItem as View_StocktakeResult;

            #region set textbox status

            //if (Mode == PageMode.View)
            //{

            //    IEnumerable<TextBox> txtBoxes = e.Row.Controls.OfType<TextBox>();
            //    foreach (var txtBox in txtBoxes)
            //    {
            //        txtBox.ReadOnly = true;
            //    }
            //}
            if (item.GeneralItemID == null || item.CSMTItemID.ToString() == DefaultValue.GUID)
            {
                TextBox txtGenBlock = e.Row.FindControl("txtGenBlock") as TextBox;
                if (txtGenBlock != null)
                {
                    txtGenBlock.Visible = false;

                }
                TextBox txtGenQI = e.Row.FindControl("txtGenQI") as TextBox;
                if (txtGenQI != null)
                {
                    txtGenQI.Visible = false;

                }
                TextBox txtGenAvailable = e.Row.FindControl("txtGenAvailable") as TextBox;
                if (txtGenAvailable != null)
                {
                    txtGenAvailable.Visible = false;

                }
            }
            if (item.CSMTItemID == null || item.CSMTItemID == DefaultValue.LONG)
            {

                TextBox txtCSMTBlock = e.Row.FindControl("txtCSMTBlock") as TextBox;
                if (txtCSMTBlock != null)
                {
                    txtCSMTBlock.Visible = false;

                }

                TextBox txtCSMTQI = e.Row.FindControl("txtCSMTQI") as TextBox;
                if (txtCSMTQI != null)
                {
                    txtCSMTQI.Visible = false;

                }
                TextBox txtCSMTAvailable = e.Row.FindControl("txtCSMTAvailable") as TextBox;
                if (txtCSMTAvailable != null)
                {
                    txtCSMTAvailable.Visible = false;

                }
            }


            if (item.RepairItemID == null || item.RepairItemID == DefaultValue.LONG)
            {
                //TextBox txtRepairDUNS = e.Row.FindControl("txtRepairDUNS") as TextBox;
                //if (txtRepairDUNS != null)
                //{
                //    txtRepairDUNS.Visible = false;
                //    
                //}

                TextBox txtRepairBlock = e.Row.FindControl("txtRepairBlock") as TextBox;
                if (txtRepairBlock != null)
                {
                    txtRepairBlock.Visible = false;

                }

                TextBox txtRepairQI = e.Row.FindControl("txtRepairQI") as TextBox;
                if (txtRepairQI != null)
                {
                    txtRepairQI.Visible = false;

                }

                TextBox txtRepairAvailable = e.Row.FindControl("txtRepairAvailable") as TextBox;
                if (txtRepairAvailable != null)
                {
                    txtRepairAvailable.Visible = false;

                }
            }


            if (item.RDCItemID == null || item.RDCItemID == DefaultValue.LONG)
            {
                TextBox txtRDCBlock = e.Row.FindControl("txtRDCBlock") as TextBox;
                if (txtRDCBlock != null)
                {
                    txtRDCBlock.Visible = false;

                }

                TextBox txtRDCQI = e.Row.FindControl("txtRDCQI") as TextBox;
                if (txtRDCQI != null)
                {
                    txtRDCQI.Visible = false;

                }

                TextBox txtRDCAvailable = e.Row.FindControl("txtRDCAvailable") as TextBox;
                if (txtRDCAvailable != null)
                {
                    txtRDCAvailable.Visible = false;

                }

            }

            if (item.SGMItemID == null || item.SGMItemID == DefaultValue.LONG || string.IsNullOrEmpty(item.WorkLocation) && string.IsNullOrEmpty(item.Segments) && string.Equals((item.Workshops+"").TrimEnd(','), "GA"))
            {
                //TextBox txtSegments = e.Row.FindControl("txtSegments") as TextBox;
                //if (txtSegments != null)
                //{
                //    txtSegments.Visible = false;
                //    
                //}

                //TextBox txtWorkshops = e.Row.FindControl("txtWorkshops") as TextBox;
                //if (txtWorkshops != null)
                //{
                //    txtWorkshops.Visible = false;
                //    
                //}

                Label lblEndCSN = e.Row.FindControl("lblEndCSN") as Label;
                if (lblEndCSN != null)
                {
                    lblEndCSN.Visible = false;

                }

                Label lblStartCSN = e.Row.FindControl("lblStartCSN") as Label;
                if (lblStartCSN != null)
                {
                    lblStartCSN.Visible = false;
                }

                TextBox txtSGMBlock = e.Row.FindControl("txtSGMBlock") as TextBox;
                if (txtSGMBlock != null)
                {
                    txtSGMBlock.Visible = false;
                    txtSGMBlock.Text = "0";
                }

                TextBox txtSGMQI = e.Row.FindControl("txtSGMQI") as TextBox;
                if (txtSGMQI != null)
                {
                    txtSGMQI.Visible = false;
                    txtSGMQI.Text = "0";
                }

                TextBox txtSGMAvailable = e.Row.FindControl("txtSGMAvailable") as TextBox;
                if (txtSGMAvailable != null)
                {
                    txtSGMAvailable.Visible = false;
                    txtSGMAvailable.Text = "0";
                }
            }
            #endregion
            //} 
            //if (Mode == PageMode.View)
            //{
            //    IEnumerable<TextBox> txtCollection = e.Row.Controls.OfType<TextBox>();
            //    foreach (var txt in txtCollection)
            //    {
            //        txt.ReadOnly = true;
            //    }
            //}
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                List<View_StocktakeResult> list = new List<View_StocktakeResult>();
                #region get items
                for (int i = 0; i < gvItems.Rows.Count; i++)
                {
                    View_StocktakeResult result = new View_StocktakeResult();
                    //for (int j = 0; j < gvItems.Rows[i].Cells.Count; j++)
                    //{
                    //TableCell cell = gvItems.Rows[i].Cells[j];


                    TextBox txtGenBlock = gvItems.Rows[i].FindControl("txtGenBlock") as TextBox;
                    if (txtGenBlock != null && !string.IsNullOrEmpty(txtGenBlock.Text.Trim()))
                    {
                        result.GenBlockAdjust = int.Parse(txtGenBlock.Text);

                    }

                    TextBox txtGenQI = gvItems.Rows[i].FindControl("txtGenQI") as TextBox;
                    if (txtGenQI != null && !string.IsNullOrEmpty(txtGenQI.Text.Trim()))
                    {
                        result.GenQIAdjust = int.Parse(txtGenQI.Text);

                    }
                    TextBox txtGenAvailable = gvItems.Rows[i].FindControl("txtGenAvailable") as TextBox;
                    if (txtGenAvailable != null && !string.IsNullOrEmpty(txtGenAvailable.Text.Trim()))
                    {
                        result.GenAvailableAdjust = int.Parse(txtGenAvailable.Text);

                    }


                    TextBox txtCSMTBlock = gvItems.Rows[i].FindControl("txtCSMTBlock") as TextBox;
                    if (txtCSMTBlock != null && !string.IsNullOrEmpty(txtCSMTBlock.Text.Trim()))
                    {
                        result.CSMTBlockAdjust = int.Parse(txtCSMTBlock.Text);

                    }

                    TextBox txtCSMTQI = gvItems.Rows[i].FindControl("txtCSMTQI") as TextBox;
                    if (txtCSMTQI != null && !string.IsNullOrEmpty(txtCSMTQI.Text.Trim()))
                    {
                        result.CSMTQIAdjust = int.Parse(txtCSMTQI.Text);

                    }
                    TextBox txtCSMTAvailable = gvItems.Rows[i].FindControl("txtCSMTAvailable") as TextBox;
                    if (txtCSMTAvailable != null && !string.IsNullOrEmpty(txtCSMTAvailable.Text.Trim()))
                    {
                        result.CSMTAvailableAdjust = int.Parse(txtCSMTAvailable.Text);

                    }


                    TextBox txtRepairBlock = gvItems.Rows[i].FindControl("txtRepairBlock") as TextBox;
                    if (txtRepairBlock != null && !string.IsNullOrEmpty(txtRepairBlock.Text.Trim()))
                    {
                        result.RepairBlockAdjust = int.Parse(txtRepairBlock.Text);

                    }
                    TextBox txtRepairQI = gvItems.Rows[i].FindControl("txtRepairQI") as TextBox;
                    if (txtRepairQI != null && !string.IsNullOrEmpty(txtRepairQI.Text.Trim()))
                    {
                        result.RepairQIAdjust = int.Parse(txtRepairQI.Text);

                    }
                    TextBox txtRepairAvailable = gvItems.Rows[i].FindControl("txtRepairAvailable") as TextBox;
                    if (txtRepairAvailable != null && !string.IsNullOrEmpty(txtRepairAvailable.Text.Trim()))
                    {
                        result.RepairAvailableAdjust = int.Parse(txtRepairAvailable.Text);

                    }

                    TextBox txtRDCBlock = gvItems.Rows[i].FindControl("txtRDCBlock") as TextBox;
                    if (txtRDCBlock != null && !string.IsNullOrEmpty(txtRDCBlock.Text.Trim()))
                    {
                        result.RDCBlockAdjust = int.Parse(txtRDCBlock.Text);

                    }

                    TextBox txtRDCQI = gvItems.Rows[i].FindControl("txtRDCQI") as TextBox;
                    if (txtRDCQI != null && !string.IsNullOrEmpty(txtRDCQI.Text.Trim()))
                    {
                        result.RDCQIAdjust = int.Parse(txtRDCQI.Text);

                    }

                    TextBox txtRDCAvailable = gvItems.Rows[i].FindControl("txtRDCAvailable") as TextBox;
                    if (txtRDCAvailable != null && !string.IsNullOrEmpty(txtRDCAvailable.Text.Trim()))
                    {
                        result.RDCAvailableAdjust = int.Parse(txtRDCAvailable.Text);

                    }

                    TextBox txtSGMAvailable = gvItems.Rows[i].FindControl("txtSGMAvailable") as TextBox;
                    if (txtSGMAvailable != null && !string.IsNullOrEmpty(txtSGMAvailable.Text.Trim()))
                    {
                        result.SGMAvailableAdjust = int.Parse(txtSGMAvailable.Text);

                    }

                    TextBox txtSGMBlock = gvItems.Rows[i].FindControl("txtSGMBlock") as TextBox;
                    if (txtSGMBlock != null && !string.IsNullOrEmpty(txtSGMBlock.Text.Trim()))
                    {
                        result.SGMBlockAdjust = int.Parse(txtSGMBlock.Text);

                    }

                    TextBox txtSGMQI = gvItems.Rows[i].FindControl("txtSGMQI") as TextBox;
                    if (txtSGMQI != null && !string.IsNullOrEmpty(txtSGMQI.Text.Trim()))
                    {
                        result.SGMQIAdjust = int.Parse(txtSGMQI.Text);

                    }

                    //TextBox txtMachining = gvItems.Rows[i].FindControl("txtMachining") as TextBox;
                    //if (txtMachining != null && !string.IsNullOrEmpty(txtMachining.Text.Trim()))
                    //{
                    //    result.Machining = int.Parse(txtMachining.Text);

                    //}

                    //TextBox txtLine = gvItems.Rows[i].FindControl("txtLine") as TextBox;
                    //if (txtLine != null && !string.IsNullOrEmpty(txtLine.Text.Trim()))
                    //{
                    //    result.Line = int.Parse(txtLine.Text);

                    //}

                    //TextBox txtStore = gvItems.Rows[i].FindControl("txtStore") as TextBox;
                    //if (txtStore != null && !string.IsNullOrEmpty(txtStore.Text.Trim()))
                    //{
                    //    result.Store = int.Parse(txtStore.Text);

                    //}
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["DetailsID"] + ""))
                    {
                        result.DetailsID = long.Parse(gvItems.DataKeys[i]["DetailsID"] + "");
                    }
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["SGMItemID"] + ""))
                    {
                        result.SGMItemID = long.Parse(gvItems.DataKeys[i]["SGMItemID"] + "");
                    }
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["RDCItemID"] + ""))
                    {
                        result.RDCItemID = long.Parse(gvItems.DataKeys[i]["RDCItemID"] + "");
                    }
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["GeneralItemID"] + ""))
                    {
                        result.GeneralItemID = long.Parse(gvItems.DataKeys[i]["GeneralItemID"] + "");
                    }
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["CSMTItemID"] + ""))
                    {
                        result.CSMTItemID = long.Parse(gvItems.DataKeys[i]["CSMTItemID"] + "");
                    }
                    if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["RepairItemID"] + ""))
                    {
                        result.RepairItemID = long.Parse(gvItems.DataKeys[i]["RepairItemID"] + "");
                    }
                    list.Add(result);
                }
                #endregion
                Service.FillStocktakeAdjustment(list);
                break;
            case "export":
                byte[] content = Service.ExportStocktakeResult(new StocktakeNotification { NotificationID = long.Parse(this.ID) },CurrentUser.UserInfo);
                if (content != null)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    string fileName = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyyMMdd") + "盘点结果(" + DateTime.Now.DayOfWeek + ").xls");
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";filetype=excel");
                    Response.OutputStream.Write(content, 0, content.Length);
                    Response.Flush();
                }
                break;
            case "import":
                break;
            default:
                break;
        }
    }
}
