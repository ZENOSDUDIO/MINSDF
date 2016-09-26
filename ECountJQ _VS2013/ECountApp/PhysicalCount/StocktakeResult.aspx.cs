using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGM.ECount.DataModel;
using System.Drawing;

public partial class PhysicalCount_StocktakeResult : ECountBasePage
{
    public string NotificationID
    {
        get
        {
            return ViewState["NotificationID"].ToString();// Request.QueryString["id"].ToString();
        }
        set
        {
            ViewState["NotificationID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                NotificationID = Request.QueryString["id"];
                BindData(Request.QueryString["id"]);
                if (Mode == PageMode.View)
                {
                    Toolbar1.Visible = false;
                }
            }
            else
            {
                Response.End();
            }
        }
    }

    public string NotificationNo
    {
        get { return ViewState["NotificationNo"] + ""; }
        set
        {
            ViewState["NotificationNo"] = value;

        }
    }


    private void BindData(string notificationID)
    {
        if (Mode == PageMode.View)
        {
            mvResult.ActiveViewIndex = 2;
            View_StocktakeResult filter = new View_StocktakeResult { NotificationID = long.Parse(notificationID), CSMTDUNS = CurrentUser.UserInfo.ConsignmentDUNS };

            if (cbUnfulfilled.Checked)
            {
                filter.Fulfilled = false;
            }
            if (CurrentUser.UserInfo.Plant != null)
            {
                filter.PlantID = CurrentUser.UserInfo.Plant.PlantID;
            }
            if (CurrentUser.UserInfo.Workshop != null)
            {
                if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Workshop.WorkshopCode))
                {
                    filter.Workshops = CurrentUser.UserInfo.Workshop.WorkshopCode;
                }
            }
            if (CurrentUser.UserInfo.Segment != null)
            {
                if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Segment.SegmentCode))
                {
                    filter.Segments = CurrentUser.UserInfo.Workshop.WorkshopCode;
                }
            }
            List<View_StocktakeResult> list = Service.GetStocktakeResult(filter, true);
            if (list.Count > 0)
            {
                NotificationNo = list[0].NotificationCode;
            }

            for (int i = 1; i <= list.Count; i++)
            {
                list[i - 1].RowNumber = i;
            }
            
            BindDataControl(gvResult, list);
            //}
        }
        else
        {
            if (!string.IsNullOrEmpty(CurrentUser.UserInfo.ConsignmentDUNS))
            {
                View_ResultCSMT filter = new View_ResultCSMT { NotificationID = long.Parse(notificationID), CSMTDUNS = CurrentUser.UserInfo.ConsignmentDUNS };
                if (cbUnfulfilled.Checked)
                {
                    filter.CSMTFulfilled = false;
                }
                if (CurrentUser.UserInfo.Plant != null)
                {
                    filter.PlantID = CurrentUser.UserInfo.Plant.PlantID;
                }
                if (CurrentUser.UserInfo.Workshop != null)
                {
                    if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Workshop.WorkshopCode))
                    {
                        filter.Workshops = CurrentUser.UserInfo.Workshop.WorkshopCode;
                    }
                }
                if (CurrentUser.UserInfo.Segment != null)
                {
                    if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Segment.SegmentCode))
                    {
                        filter.Segments = CurrentUser.UserInfo.Workshop.WorkshopCode;
                    }
                }
                mvResult.ActiveViewIndex = 1;
                List<View_ResultCSMT> list = Service.GetCSMTStocktakeResult(filter);
                if (list.Count > 0)
                {
                    NotificationNo = list[0].NotificationCode;
                }

                for (int i = 1; i <= list.Count; i++)
                {
                    list[i - 1].RowNumber = i;
                }

                BindDataControl(gvCSMTItems, list);

            }
            else
            {
                View_ResultNoneCSMT filter = new View_ResultNoneCSMT { NotificationID = long.Parse(notificationID) };
                if (cbUnfulfilled.Checked)
                {
                    filter.NoneCSMTFulfilled = false;
                }
                if (CurrentUser.UserInfo.Plant != null)
                {
                    filter.PlantID = CurrentUser.UserInfo.Plant.PlantID;
                }
                if (CurrentUser.UserInfo.Workshop != null)
                {
                    if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Workshop.WorkshopCode))
                    {
                        filter.Workshops = CurrentUser.UserInfo.Workshop.WorkshopCode;
                    }
                }
                if (CurrentUser.UserInfo.Segment != null)
                {
                    if (!string.IsNullOrEmpty(CurrentUser.UserInfo.Segment.SegmentCode))
                    {
                        filter.Segments = CurrentUser.UserInfo.Workshop.WorkshopCode;
                    }
                }
                if (!string.IsNullOrEmpty(CurrentUser.UserInfo.RepairDUNS))
                {
                    filter.RepairDUNS = CurrentUser.UserInfo.RepairDUNS;
                }
                mvResult.ActiveViewIndex = 0;

                //List<View_StocktakeResult> list = Service.GetStocktakeResult(filter);
                List<View_ResultNoneCSMT> list = Service.GetNoneCSMTStocktakeResult(filter);
                if (list.Count > 0)
                {
                    NotificationNo = list[0].NotificationCode;
                }

                for (int i = 1; i <= list.Count; i++)
                {
                    list[i - 1].RowNumber = i;
                }

                BindDataControl(gvItems, list);
            }
        }

    }

    protected void gvItems_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            rowHeader.CssClass = "gridViewHeaderStyle2";
            #region header
            Literal newCells = new Literal();
            newCells.Text = @"
                序号</th>
                <th rowspan='4'>
                零件号</th>
                <th rowspan='4'>
                    工厂</th>
                <th rowspan='4'>
                    零件名称</th>
                <th rowspan='4'>
                    DUNS</th>
                <th rowspan='4'>
                    物料类别</th>
                <th rowspan='4'>
                    循环级别</th>
                <th rowspan='4'>
                    申请单号</th>
                <th rowspan='4'>
                    申请人</th>
                <th rowspan='4'>
                    盘点类别</th>
                <th colspan='19'>
                    盘点结果</th>
                <th rowspan='4'>
                    总数</th>
                <th rowspan='4'>
                    备注</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th colspan='9'>
                    SGM现场</th>
                <th colspan='3'>
                    RDC</th>
                <th colspan='4'>
                    返修</th>
                <th colspan='3'>
                    港口</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th colspan='3'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    起点CSN</th>
                <th rowspan='2'>
                    终点CSN</th>
                <th rowspan='2'>
                    车间</th>
                <th rowspan='2'>
                    工段</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    DUNS</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th>
                    库位</th>
                <th>
                    线旁</th>
                <th>
                    加工区";
            #endregion

            TableCellCollection cells = e.Row.Cells;
            TableHeaderCell headerCell = new TableHeaderCell();
            //headerCell.CssClass = "gridViewHeaderStyle";
            headerCell.RowSpan = 4;
            headerCell.Controls.Add(newCells);
            rowHeader.Cells.Add(headerCell);

            rowHeader.Cells.Add(headerCell);
            rowHeader.Visible = true;

            gvItems.Controls[0].Controls.AddAt(0, rowHeader);

        }
    }

    protected bool LocationAccessDenied(StoreLocation location, bool aiqIncluded)
    {
        return (!aiqIncluded
            || CurrentUser.UserInfo.UserGroup.StoreLocationType != null
                && CurrentUser.UserInfo.UserGroup.StoreLocationType.TypeID != location.StoreLocationType.TypeID
            || CurrentUser.UserInfo.UserGroup.StoreLocationType == null
                && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null
                    || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value));
    }

    protected void HideUnauthorisedLocation(StoreLocation location, TextBox txtAvailable, TextBox txtQI, TextBox txtBlock)
    {
        if (LocationAccessDenied(location, location.AvailableIncluded.Value))
        {
            txtAvailable.Visible = false;
        }
        if (LocationAccessDenied(location, location.QIIncluded.Value))
        {
            txtQI.Visible = false;
        }
        if (LocationAccessDenied(location, location.BlockIncluded.Value))
        {
            txtBlock.Visible = false;
        }
    }

    protected void HideUnauthorisedLocation(StoreLocation location, TableCell cellAvailable, TableCell cellQI, TableCell cellBlock)
    {
        if (LocationAccessDenied(location, location.AvailableIncluded.Value))
        {
            cellAvailable.Text = string.Empty;
        }
        if (LocationAccessDenied(location, location.QIIncluded.Value))
        {
            cellQI.Text = string.Empty;
        }
        if (LocationAccessDenied(location, location.BlockIncluded.Value))
        {
            cellBlock.Text = string.Empty;
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            View_ResultNoneCSMT item = e.Row.DataItem as View_ResultNoneCSMT;
            StoreLocation location = new StoreLocation();
            if (item.SGMItemID != null)
            {
                List<WorkshopStocktakeDetail> details = item.WorkshopDetails;
                if (CurrentUser.UserInfo.Workshop != null)
                {
                    details = new List<WorkshopStocktakeDetail>();
                    WorkshopStocktakeDetail detail = item.WorkshopDetails.Find(d => d.WorkshopCode == CurrentUser.UserInfo.Workshop.WorkshopCode);
                    if (detail != null)
                    {
                        details.Add(detail);
                    }
                }               

                GridView gvStore = e.Row.FindControl("gvStore") as GridView;
                BindDataControl(gvStore, details);
                GridView gvLine = e.Row.FindControl("gvLine") as GridView;
                BindDataControl(gvLine, details);
                GridView gvMachining = e.Row.FindControl("gvMachining") as GridView;
                BindDataControl(gvMachining, details);
                GridView gvQI = e.Row.FindControl("gvQI") as GridView;
                BindDataControl(gvQI, details);
                GridView gvBlock = e.Row.FindControl("gvBlock") as GridView;
                BindDataControl(gvBlock, details);
                GridView gvStartCSN = e.Row.FindControl("gvStartCSN") as GridView;
                BindDataControl(gvStartCSN, details);
                GridView gvEndCSN = e.Row.FindControl("gvEndCSN") as GridView;
                BindDataControl(gvEndCSN, details);
                GridView gvWorkshop = e.Row.FindControl("gvWorkshop") as GridView;
                BindDataControl(gvWorkshop, details);

                location = this.StoreLocations.Find(l => l.LocationID == item.SGMLocationID);

                if (location != null)
                {
                    bool availableExcluded = LocationAccessDenied(location, location.AvailableIncluded.Value);//(!location.AvailableIncluded.Value || CurrentUser.UserInfo.UserGroup.StoreLocationType != null && CurrentUser.UserInfo.UserGroup.StoreLocationType.TypeID != location.StoreLocationType.TypeID ||
                    //CurrentUser.UserInfo.UserGroup.StoreLocation == null && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value));
                    bool qiExcluded = LocationAccessDenied(location, location.QIIncluded.Value);//(!location.QIIncluded.Value || CurrentUser.UserInfo.UserGroup.StoreLocation != null && CurrentUser.UserInfo.UserGroup.StoreLocation.LocationID != item.SGMLocationID || CurrentUser.UserInfo.UserGroup.StoreLocation == null && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value));
                    bool blockExcluded = LocationAccessDenied(location, location.BlockIncluded.Value);//(!location.BlockIncluded.Value || CurrentUser.UserInfo.UserGroup.StoreLocation != null && CurrentUser.UserInfo.UserGroup.StoreLocation.LocationID != item.SGMLocationID ||  CurrentUser.UserInfo.UserGroup.StoreLocation == null && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value));
                    for (int i = 0; i < details.Count; i++)
                    {
                        GridViewRow rowMachining = gvMachining.Rows[i];
                        GridViewRow rowLine = gvLine.Rows[i];
                        GridViewRow rowStore = gvStore.Rows[i];
                        GridViewRow rowStartCSN = gvStartCSN.Rows[i];
                        GridViewRow rowEndCSN = gvEndCSN.Rows[i];
                        GridViewRow rowQI = gvQI.Rows[i];
                        GridViewRow rowBlock = gvBlock.Rows[i];
                        GridViewRow rowWorkshop = gvWorkshop.Rows[i];
                        TextBox txtEndCSN = rowEndCSN.FindControl("txtEndCSN") as TextBox;
                        TextBox txtStartCSN = rowStartCSN.FindControl("txtStartCSN") as TextBox;
                        TextBox txtSGMBlock = rowBlock.FindControl("txtSGMBlock") as TextBox;
                        TextBox txtSGMQI = rowQI.FindControl("txtSGMQI") as TextBox;
                        TextBox txtMachining = rowMachining.FindControl("txtMachining") as TextBox;
                        TextBox txtLine = rowLine.FindControl("txtLine") as TextBox;
                        TextBox txtStore = rowStore.FindControl("txtStore") as TextBox;

                        if (availableExcluded)
                        {
                            txtStore.Visible = false;
                            txtMachining.Visible = false;
                            txtLine.Visible = false;

                        }
                        if (qiExcluded)
                        {
                            txtSGMQI.Visible = false;
                        }
                        if (blockExcluded)
                        {
                            txtSGMBlock.Visible = false;
                        }
                        if (availableExcluded && blockExcluded && qiExcluded)
                        {

                            txtStartCSN.Visible = false;
                            txtEndCSN.Visible = false;
                        }
                    }

                }
            }
            //for(int i=0;i<e.Row.Cells.Count;i++)
            //{
            //DataControlFielde.Row.Cells[i] e.Row.Cells[i] = e.Row.e.Row.Cells[i]s[i] as DataControlFielde.Row.Cells[i];
            #region set textbox status

            if (Mode == PageMode.View)
            {

                IEnumerable<TextBox> txtBoxes = e.Row.Controls.OfType<TextBox>();
                foreach (var txtBox in txtBoxes)
                {
                    txtBox.ReadOnly = true;
                }
            }
            TextBox txtGenBlock = e.Row.FindControl("txtGenBlock") as TextBox;
            TextBox txtGenAvailable = e.Row.FindControl("txtGenAvailable") as TextBox;
            TextBox txtGenQI = e.Row.FindControl("txtGenQI") as TextBox;
            if (item.GeneralItemID == null || item.GeneralItemID == DefaultValue.LONG)
            {
                if (txtGenBlock != null)
                {
                    txtGenBlock.Visible = false;

                }
                if (txtGenQI != null)
                {
                    txtGenQI.Visible = false;

                }
                if (txtGenAvailable != null)
                {
                    txtGenAvailable.Visible = false;

                }
            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.GenerLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, txtGenAvailable, txtGenQI, txtGenBlock);

                }
            }

            TextBox txtRepairBlock = e.Row.FindControl("txtRepairBlock") as TextBox;
            TextBox txtRepairQI = e.Row.FindControl("txtRepairQI") as TextBox;
            TextBox txtRepairAvailable = e.Row.FindControl("txtRepairAvailable") as TextBox;
            if (item.RepairItemID == null || item.RepairItemID == DefaultValue.LONG)
            {

                if (txtRepairBlock != null)
                {
                    txtRepairBlock.Visible = false;

                }

                if (txtRepairQI != null)
                {
                    txtRepairQI.Visible = false;

                }

                if (txtRepairAvailable != null)
                {
                    txtRepairAvailable.Visible = false;

                }
            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.RepairLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, txtRepairAvailable, txtRepairQI, txtRepairBlock);
                }
            }


            TextBox txtRDCBlock = e.Row.FindControl("txtRDCBlock") as TextBox;
            TextBox txtRDCQI = e.Row.FindControl("txtRDCQI") as TextBox;
            TextBox txtRDCAvailable = e.Row.FindControl("txtRDCAvailable") as TextBox;
            if (item.RDCItemID == DefaultValue.LONG)
            {
                if (txtRDCBlock != null)
                {
                    txtRDCBlock.Visible = false;

                }

                if (txtRDCQI != null)
                {
                    txtRDCQI.Visible = false;

                }

                if (txtRDCAvailable != null)
                {
                    txtRDCAvailable.Visible = false;

                }

            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.RDCLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, txtRDCAvailable, txtRDCQI, txtRDCBlock);

                }
            }


            if (CurrentUser.UserInfo.UserGroup.StoreLocationType == null && (CurrentUser.UserInfo.UserGroup.FillinAllLocation == null || !CurrentUser.UserInfo.UserGroup.FillinAllLocation.Value))
            {

                txtRDCAvailable.ReadOnly = true;
                txtRDCQI.ReadOnly = true;
                txtRDCBlock.ReadOnly = true;
                txtRepairAvailable.ReadOnly = true;
                txtRepairBlock.ReadOnly = true;
                txtRepairQI.ReadOnly = true;
                txtGenAvailable.ReadOnly = true;
                txtGenBlock.ReadOnly = true;
                txtGenQI.ReadOnly = true;
            }
            #endregion
            //} 
            if (Mode == PageMode.View)
            {
                IEnumerable<TextBox> txtCollection = e.Row.Controls.OfType<TextBox>();
                foreach (var txt in txtCollection)
                {
                    txt.ReadOnly = true;
                }
            }
        }
    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "save":
                List<View_StocktakeResult> list = new List<View_StocktakeResult>();
                if (mvResult.ActiveViewIndex == 0)
                {
                    #region get items
                    for (int i = 0; i < gvItems.Rows.Count; i++)
                    {
                        GridViewRow row = gvItems.Rows[i];
                        View_StocktakeResult result = new View_StocktakeResult();
                        //for (int j = 0; j < gvItems.Rows[i].Cells.Count; j++)
                        //{
                        //TableCell cell = gvItems.Rows[i].Cells[j];


                        result.GenFillinBy = CurrentUser.UserInfo.UserID;
                        TextBox txtGenBlock = gvItems.Rows[i].FindControl("txtGenBlock") as TextBox;
                        if (txtGenBlock != null && !string.IsNullOrEmpty(txtGenBlock.Text.Trim()))
                        {
                            result.GeneralBlock = decimal.Parse(txtGenBlock.Text);

                        }

                        TextBox txtGenQI = gvItems.Rows[i].FindControl("txtGenQI") as TextBox;
                        if (txtGenQI != null && !string.IsNullOrEmpty(txtGenQI.Text.Trim()))
                        {
                            result.GeneralQI = decimal.Parse(txtGenQI.Text);

                        }
                        TextBox txtGenAvailable = gvItems.Rows[i].FindControl("txtGenAvailable") as TextBox;
                        if (txtGenAvailable != null && !string.IsNullOrEmpty(txtGenAvailable.Text.Trim()))
                        {
                            result.GeneralAvailable = decimal.Parse(txtGenAvailable.Text);

                        }




                        result.RepairFillinBy = CurrentUser.UserInfo.UserID;
                        TextBox txtRepairBlock = gvItems.Rows[i].FindControl("txtRepairBlock") as TextBox;
                        if (txtRepairBlock != null && !string.IsNullOrEmpty(txtRepairBlock.Text.Trim()))
                        {
                            result.RepairBlock = decimal.Parse(txtRepairBlock.Text);

                        }
                        TextBox txtRepairQI = gvItems.Rows[i].FindControl("txtRepairQI") as TextBox;
                        if (txtRepairQI != null && !string.IsNullOrEmpty(txtRepairQI.Text.Trim()))
                        {
                            result.RepairQI = decimal.Parse(txtRepairQI.Text);

                        }
                        TextBox txtRepairAvailable = gvItems.Rows[i].FindControl("txtRepairAvailable") as TextBox;
                        if (txtRepairAvailable != null && !string.IsNullOrEmpty(txtRepairAvailable.Text.Trim()))
                        {
                            result.RepairAvailable = decimal.Parse(txtRepairAvailable.Text);

                        }



                        result.RDCFillinBy = CurrentUser.UserInfo.UserID;
                        TextBox txtRDCBlock = gvItems.Rows[i].FindControl("txtRDCBlock") as TextBox;
                        if (txtRDCBlock != null && !string.IsNullOrEmpty(txtRDCBlock.Text.Trim()))
                        {
                            result.RDCBlock = decimal.Parse(txtRDCBlock.Text);

                        }

                        TextBox txtRDCQI = gvItems.Rows[i].FindControl("txtRDCQI") as TextBox;
                        if (txtRDCQI != null && !string.IsNullOrEmpty(txtRDCQI.Text.Trim()))
                        {
                            result.RDCQI = decimal.Parse(txtRDCQI.Text);

                        }

                        TextBox txtRDCAvailable = gvItems.Rows[i].FindControl("txtRDCAvailable") as TextBox;
                        if (txtRDCAvailable != null && !string.IsNullOrEmpty(txtRDCAvailable.Text.Trim()))
                        {
                            result.RDCAvailable = decimal.Parse(txtRDCAvailable.Text);

                        }



                        result.WorkshopDetails = new List<WorkshopStocktakeDetail>();
                        result.SGMFillinBy = CurrentUser.UserInfo.UserID;

                        GetSGMDetailsFromRow(row, result, "gvStore", "txtStore");
                        GetSGMDetailsFromRow(row, result, "gvLine", "txtLine");
                        GetSGMDetailsFromRow(row, result, "gvMachining", "txtMachining");
                        GetSGMDetailsFromRow(row, result, "gvStartCSN", "txtStartCSN");
                        GetSGMDetailsFromRow(row, result, "gvEndCSN", "txtEndCSN");
                        GetSGMDetailsFromRow(row, result, "gvQI", "txtSGMQI");
                        GetSGMDetailsFromRow(row, result, "gvBlock", "txtSGMBlock");
                        //TextBox txtEndCSN = gvItems.Rows[i].FindControl("txtEndCSN") as TextBox;
                        //if (txtEndCSN != null && !string.IsNullOrEmpty(txtEndCSN.Text.Trim()))
                        //{
                        //    result.EndCSN = txtEndCSN.Text;

                        //}

                        //TextBox txtStartCSN = gvItems.Rows[i].FindControl("txtStartCSN") as TextBox;
                        //if (txtStartCSN != null && !string.IsNullOrEmpty(txtStartCSN.Text.Trim()))
                        //{
                        //    result.StartCSN = txtStartCSN.Text;

                        //}

                        //TextBox txtSGMBlock = gvItems.Rows[i].FindControl("txtSGMBlock") as TextBox;
                        //if (txtSGMBlock != null && !string.IsNullOrEmpty(txtSGMBlock.Text.Trim()))
                        //{
                        //    result.SGMBlock = decimal.Parse(txtSGMBlock.Text);

                        //}

                        //TextBox txtSGMQI = gvItems.Rows[i].FindControl("txtSGMQI") as TextBox;
                        //if (txtSGMQI != null && !string.IsNullOrEmpty(txtSGMQI.Text.Trim()))
                        //{
                        //    result.SGMQI = decimal.Parse(txtSGMQI.Text);

                        //}

                        //TextBox txtMachining = gvItems.Rows[i].FindControl("txtMachining") as TextBox;
                        //if (txtMachining != null && !string.IsNullOrEmpty(txtMachining.Text.Trim()))
                        //{
                        //    result.Machining = decimal.Parse(txtMachining.Text);

                        //}

                        //TextBox txtLine = gvItems.Rows[i].FindControl("txtLine") as TextBox;
                        //if (txtLine != null && !string.IsNullOrEmpty(txtLine.Text.Trim()))
                        //{
                        //    result.Line = decimal.Parse(txtLine.Text);

                        //}

                        //TextBox txtStore = gvItems.Rows[i].FindControl("txtStore") as TextBox;
                        //if (txtStore != null && !string.IsNullOrEmpty(txtStore.Text.Trim()))
                        //{
                        //    result.Store = decimal.Parse(txtStore.Text);

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
                        if (!string.IsNullOrEmpty(gvItems.DataKeys[i]["RepairItemID"] + ""))
                        {
                            result.RepairItemID = long.Parse(gvItems.DataKeys[i]["RepairItemID"] + "");
                        }
                        result.CSMTFillinTime = result.RDCFillinTime = result.RepairFillinTime = result.GenFillinTime = result.SGMFillinTime = DateTime.Now;
                        list.Add(result);
                    }
                    #endregion
                }
                else
                {
                    for (int i = 0; i < gvCSMTItems.Rows.Count; i++)
                    {
                        View_StocktakeResult result = new View_StocktakeResult();
                        TextBox txtCSMTBlock = gvCSMTItems.Rows[i].FindControl("txtCSMTBlock") as TextBox;
                        if (txtCSMTBlock != null && !string.IsNullOrEmpty(txtCSMTBlock.Text.Trim()))
                        {
                            result.CSMTBlock = decimal.Parse(txtCSMTBlock.Text);

                        }

                        result.CSMTFillinBy = CurrentUser.UserInfo.UserID;
                        result.CSMTFillinTime = DateTime.Now;
                        TextBox txtCSMTQI = gvCSMTItems.Rows[i].FindControl("txtCSMTQI") as TextBox;
                        if (txtCSMTQI != null && !string.IsNullOrEmpty(txtCSMTQI.Text.Trim()))
                        {
                            result.CSMTQI = decimal.Parse(txtCSMTQI.Text);

                        }
                        TextBox txtCSMTAvailable = gvCSMTItems.Rows[i].FindControl("txtCSMTAvailable") as TextBox;
                        if (txtCSMTAvailable != null && !string.IsNullOrEmpty(txtCSMTAvailable.Text.Trim()))
                        {
                            result.CSMTAvailable = decimal.Parse(txtCSMTAvailable.Text);

                        }
                        if (!string.IsNullOrEmpty(gvCSMTItems.DataKeys[i]["CSMTItemID"] + ""))
                        {
                            result.CSMTItemID = long.Parse(gvCSMTItems.DataKeys[i]["CSMTItemID"].ToString());
                        }
                        list.Add(result);
                    }
                }
                Service.FillStocktakeResult(new StocktakeNotification { NotificationID = long.Parse(this.NotificationID) }, list);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeScript", "closeDialogOnSave();", true);
                break;
            case "export":
                byte[] content = Service.ExportStocktakeResult(new StocktakeNotification { NotificationID = long.Parse(this.NotificationID) },CurrentUser.UserInfo);
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

    private void GetSGMDetailsFromRow(GridViewRow row, View_StocktakeResult result, string innerGVID, string txtID)
    {
        GridView innerGV = row.FindControl(innerGVID) as GridView;
        string gvID = innerGV.ID;
        foreach (GridViewRow item in innerGV.Rows)
        {

            long detailID = Convert.ToInt64(innerGV.DataKeys[item.RowIndex].Value);
            WorkshopStocktakeDetail detail = new WorkshopStocktakeDetail { ItemDetailID = detailID, SGMFillinBy = CurrentUser.UserInfo.UserID, SGMFillinTime = DateTime.Now };
            //item.DataItem as WorkshopStocktakeDetail;
            WorkshopStocktakeDetail resultDetail = result.WorkshopDetails.Find(d => d.ItemDetailID == detail.ItemDetailID);
            TextBox txt = item.FindControl(txtID) as TextBox;
            decimal fieldValue;
            if (resultDetail == null)
            {
                if (txt != null)
                {
                    switch (gvID)
                    {
                        case "gvMachining":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                detail.Machining = fieldValue;
                            }
                            break;
                        case "gvLine":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                detail.Line = fieldValue;
                            }
                            break;
                        case "gvStore":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                detail.Store = fieldValue;
                            }
                            break;
                        case "gvQI":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                detail.SGMQI = fieldValue;
                            }
                            break;
                        case "gvBlock":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                detail.SGMBlock = fieldValue;
                            }
                            break;
                        case "gvStartCSN":
                            detail.StartCSN = txt.Text.Trim();
                            break;
                        case "gvEndCSN":
                            detail.EndCSN = txt.Text.Trim();
                            break;
                        default:
                            break;
                    }

                }
                result.WorkshopDetails.Add(detail);
            }
            else
            {
                resultDetail.SGMFillinBy = CurrentUser.UserInfo.UserID;

                resultDetail.SGMFillinTime = DateTime.Now;
                if (txt != null)
                {
                    switch (gvID)
                    {
                        case "gvMachining":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                resultDetail.Machining = fieldValue;
                            }
                            break;
                        case "gvLine":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                resultDetail.Line = fieldValue;
                            }
                            break;
                        case "gvStore":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                resultDetail.Store = fieldValue;
                            }
                            break;
                        case "gvQI":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                resultDetail.SGMQI = fieldValue;
                            }
                            break;
                        case "gvBlock":
                            if (decimal.TryParse(txt.Text.Trim(), out fieldValue))
                            {
                                resultDetail.SGMBlock = fieldValue;
                            }
                            break;
                        case "gvStartCSN":
                            resultDetail.StartCSN = txt.Text.Trim();
                            break;
                        case "gvEndCSN":
                            resultDetail.EndCSN = txt.Text.Trim();
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }
    protected void gvItems_PreRender(object sender, EventArgs e)
    {
        List<View_ResultNoneCSMT> list = new List<View_ResultNoneCSMT> { new View_ResultNoneCSMT() };
        BindEmptyGridView(gvItems, list);
    }
    protected void gvCSMTItems_PreRender(object sender, EventArgs e)
    {
        List<View_ResultCSMT> list = new List<View_ResultCSMT> { new View_ResultCSMT() };
        BindEmptyGridView(gvCSMTItems, list);
    }

    protected void gvCSMTItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            View_ResultCSMT item = e.Row.DataItem as View_ResultCSMT;

            #region set textbox status

            TextBox txtCSMTAvailable = e.Row.FindControl("txtCSMTAvailable") as TextBox;
            TextBox txtCSMTBlock = e.Row.FindControl("txtCSMTBlock") as TextBox;
            TextBox txtCSMTQI = e.Row.FindControl("txtCSMTQI") as TextBox;
            StoreLocation location = new StoreLocation();

            location = this.StoreLocations.Find(l => l.LocationID == item.CSMTLocationID);

            if (location != null)
            {


                if (!location.AvailableIncluded.Value || string.IsNullOrEmpty(CurrentUser.UserInfo.ConsignmentDUNS) && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value))
                {
                    txtCSMTAvailable.Visible = false;
                }

                if (!location.QIIncluded.Value || string.IsNullOrEmpty(CurrentUser.UserInfo.ConsignmentDUNS) && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value))
                {
                    txtCSMTQI.Visible = false;
                }

                if (!location.BlockIncluded.Value || string.IsNullOrEmpty(CurrentUser.UserInfo.ConsignmentDUNS) && (CurrentUser.UserInfo.UserGroup.ShowAllLocation == null || !CurrentUser.UserInfo.UserGroup.ShowAllLocation.Value))
                {
                    txtCSMTBlock.Visible = false;
                }

            }

            if (string.IsNullOrEmpty(CurrentUser.UserInfo.ConsignmentDUNS) && (CurrentUser.UserInfo.UserGroup.FillinAllLocation == null || !CurrentUser.UserInfo.UserGroup.FillinAllLocation.Value))
            {

                txtCSMTBlock.ReadOnly = true;
                txtCSMTQI.ReadOnly = true;
                txtCSMTAvailable.ReadOnly = true;
            }
            #endregion
            //} 
            if (Mode == PageMode.View)
            {
                IEnumerable<TextBox> txtCollection = e.Row.Controls.OfType<TextBox>();
                foreach (var txt in txtCollection)
                {
                    txt.ReadOnly = true;
                }
            }
        }
    }
    protected void cbUnfulfilled_CheckedChanged(object sender, EventArgs e)
    {
        BindData(NotificationID);
    }
    protected void gvResult_PreRender(object sender, EventArgs e)
    {

        List<View_StocktakeResult> list = new List<View_StocktakeResult> { new View_StocktakeResult() };
        BindEmptyGridView(gvResult, list);
    }
    protected void gvResult_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow rowHeader = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            rowHeader.CssClass = "gridViewHeaderStyle2";
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
            newCells.Text = @"
                序号</th>
                <th rowspan='4'>
                零件号</th>
                <th rowspan='4'>
                    工厂</th>
                <th rowspan='4'>
                    零件名称</th>
                <th rowspan='4'>
                    DUNS</th>
                <th rowspan='4'>
                    物料类别</th>
                <th rowspan='4'>
                    循环级别</th>
                <th rowspan='4'>
                    申请单号</th>
                <th rowspan='4'>
                    申请人</th>
                <th rowspan='4'>
                    盘点类别</th>
                <th colspan='23'>
                    盘点结果</th>
                <th rowspan='4'>
                    总数</th>
                <th rowspan='4'>
                    备注</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th colspan='9'>
                    SGM现场</th>
                <th colspan='3'>
                    RDC</th>
                <th colspan='4'>
                    返修</th>
                <th colspan='3'>
                    港口</th>
                <th colspan='4'>
                    外协</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th colspan='3'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    起点CSN</th>
                <th rowspan='2'>
                    终点CSN</th>
                <th rowspan='2'>
                    车间</th>
                <th rowspan='2'>
                    工段</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    DUNS</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    Available</th>
                <th rowspan='2'>
                    QI</th>
                <th rowspan='2'>
                    Block</th>
                <th rowspan='2'>
                    DUNS</th>
            </tr>
            <tr class='gridViewHeaderStyle2'>
                <th>
                    库位</th>
                <th>
                    线旁</th>
                <th>
                    加工区";
            #endregion

            TableCellCollection cells = e.Row.Cells;
            TableHeaderCell headerCell = new TableHeaderCell();
            //headerCell.CssClass = "gridViewHeaderStyle";
            headerCell.RowSpan = 4;
            headerCell.Controls.Add(newCells);
            rowHeader.Cells.Add(headerCell);

            rowHeader.Cells.Add(headerCell);
            rowHeader.Visible = true;

            gvResult.Controls[0].Controls.AddAt(0, rowHeader);

        }
    }
    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            View_StocktakeResult item = e.Row.DataItem as View_StocktakeResult;
            StoreLocation location = new StoreLocation();
            if (item.SGMItemID != null)
            {
                List<WorkshopStocktakeDetail> details = item.WorkshopDetails;
                if (CurrentUser.UserInfo.Workshop != null)
                {
                    details = new List<WorkshopStocktakeDetail>();
                    WorkshopStocktakeDetail detail = item.WorkshopDetails.Find(d => d.WorkshopCode == CurrentUser.UserInfo.Workshop.WorkshopCode);
                    if (detail != null)
                    {
                        details.Add(detail);
                    }
                }

                GridView gvStore = e.Row.FindControl("gvStore") as GridView;
                
                BindDataControl(gvStore, details);   
                GridView gvLine = e.Row.FindControl("gvLine") as GridView;
                BindDataControl(gvLine, details);
                GridView gvMachining = e.Row.FindControl("gvMachining") as GridView;
                BindDataControl(gvMachining, details);
                GridView gvQI = e.Row.FindControl("gvQI") as GridView;
                BindDataControl(gvQI, details);
                GridView gvBlock = e.Row.FindControl("gvBlock") as GridView;
                BindDataControl(gvBlock, details);
                GridView gvStartCSN = e.Row.FindControl("gvStartCSN") as GridView;
                BindDataControl(gvStartCSN, details);
                GridView gvEndCSN = e.Row.FindControl("gvEndCSN") as GridView;
                BindDataControl(gvEndCSN, details);
                GridView gvWorkshop = e.Row.FindControl("gvWorkshop") as GridView;
                BindDataControl(gvWorkshop, details);

                location = this.StoreLocations.Find(l => l.LocationID == item.SGMLocationID);

                if (location != null)
                {
                    bool availableExcluded = LocationAccessDenied(location, location.AvailableIncluded.Value);
                    bool qiExcluded = LocationAccessDenied(location, location.QIIncluded.Value);
                    bool blockExcluded = LocationAccessDenied(location, location.BlockIncluded.Value);
                    for (int i = 0; i < details.Count; i++)
                    {
                        GridViewRow rowMachining = gvMachining.Rows[i];
                        GridViewRow rowLine = gvLine.Rows[i];
                        GridViewRow rowStore = gvStore.Rows[i];
                        GridViewRow rowStartCSN = gvStartCSN.Rows[i];
                        GridViewRow rowEndCSN = gvEndCSN.Rows[i];
                        GridViewRow rowQI = gvQI.Rows[i];
                        GridViewRow rowBlock = gvBlock.Rows[i];
                        GridViewRow rowWorkshop = gvWorkshop.Rows[i];

                        if (availableExcluded)
                        {
                            rowStore.Cells[0].Text = string.Empty;
                            rowMachining.Cells[0].Text = string.Empty;
                            rowLine.Cells[0].Text = string.Empty;
                            

                        }
                        if (qiExcluded)
                        {
                            rowQI.Cells[0].Text = string.Empty;
                        }
                        if (blockExcluded)
                        {
                            rowBlock.Cells[0].Text = string.Empty;
                        }
                        if (availableExcluded && blockExcluded && qiExcluded)
                        {
                            rowStartCSN.Cells[0].Text = string.Empty;
                            rowEndCSN.Cells[0].Text = string.Empty;                        }
                    }

                }
            }

            #region set textbox status

            //if (Mode == PageMode.View)
            //{

            //    IEnumerable<TextBox> txtBoxes = e.Row.Controls.OfType<TextBox>();
            //    foreach (var txtBox in txtBoxes)
            //    {
            //        txtBox.ReadOnly = true;
            //    }
            //}
            //TextBox txtGenBlock = e.Row.FindControl("txtGenBlock") as TextBox;
            //TextBox txtGenAvailable = e.Row.FindControl("txtGenAvailable") as TextBox;
            //TextBox txtGenQI = e.Row.FindControl("txtGenQI") as TextBox;
            if (item.GeneralItemID == null || item.GeneralItemID == DefaultValue.LONG)
            {
                e.Row.Cells[26].Text = string.Empty;
                e.Row.Cells[27].Text = string.Empty;
                e.Row.Cells[28].Text = string.Empty;
                //if (txtGenBlock != null)
                //{
                //    txtGenBlock.Visible = false;

                //}
                //if (txtGenQI != null)
                //{
                //    txtGenQI.Visible = false;

                //}
                //if (txtGenAvailable != null)
                //{
                //    txtGenAvailable.Visible = false;

                //}
            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.GenerLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, e.Row.Cells[26], e.Row.Cells[27], e.Row.Cells[28]);

                }
            }

            //TextBox txtRepairBlock = e.Row.FindControl("txtRepairBlock") as TextBox;
            //TextBox txtRepairQI = e.Row.FindControl("txtRepairQI") as TextBox;
            //TextBox txtRepairAvailable = e.Row.FindControl("txtRepairAvailable") as TextBox;
            if (item.RepairItemID == null || item.RepairItemID == DefaultValue.LONG)
            {


                e.Row.Cells[22].Text = string.Empty;
                e.Row.Cells[23].Text = string.Empty;
                e.Row.Cells[24].Text = string.Empty;
            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.RepairLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, e.Row.Cells[22], e.Row.Cells[23], e.Row.Cells[24]);
                }
            }


            //TextBox txtRDCBlock = e.Row.FindControl("txtRDCBlock") as TextBox;
            //TextBox txtRDCQI = e.Row.FindControl("txtRDCQI") as TextBox;
            //TextBox txtRDCAvailable = e.Row.FindControl("txtRDCAvailable") as TextBox;
            if (item.RDCItemID == DefaultValue.LONG)
            {
                e.Row.Cells[19].Text = string.Empty;
                e.Row.Cells[20].Text = string.Empty;
                e.Row.Cells[21].Text = string.Empty;

            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.RDCLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, e.Row.Cells[19], e.Row.Cells[20], e.Row.Cells[21]);

                }
            }

            if (item.CSMTItemID == null || item.CSMTItemID == DefaultValue.LONG)
            {
                e.Row.Cells[29].Text = string.Empty;
                e.Row.Cells[30].Text = string.Empty;
                e.Row.Cells[31].Text = string.Empty;
                
            }
            else
            {
                location = this.StoreLocations.Find(l => l.LocationID == item.CSMTLocationID);

                if (location != null)
                {
                    HideUnauthorisedLocation(location, e.Row.Cells[29], e.Row.Cells[30], e.Row.Cells[31]);

                }
            }

            #endregion
        }
    }
    protected void gvResult_DataBound(object sender, EventArgs e)
    {
        List<int> colList = new List<int>();
        for (int i = 0; i < 28; i++)
        {
            colList.Add(i);
        }
        MergeCell(gvResult, 0, colList, gvResult.RowStyle.ForeColor, gvResult.AlternatingRowStyle.ForeColor, gvResult.RowStyle.BackColor, gvResult.AlternatingRowStyle.BackColor);// Color.FromName("#333333"), Color.FromName("#284775"), Color.FromName("#F7F6F3"), Color.FromName("White"));
    }
}
