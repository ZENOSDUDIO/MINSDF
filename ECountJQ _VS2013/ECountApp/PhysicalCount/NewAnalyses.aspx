<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NewAnalyses.aspx.cs" Inherits="PhysicalCount_NewAnalyses" EnableEventValidation="false" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <script type="text/javascript">
        var gvAnalysis = '<%= gvAnalysis.ClientID%>';
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 6:
                    showDialog('AdjustmentImport.aspx', 900, 500);
                    eventArgs.set_cancel(true);
                    break;
                case 7:
                    showDialog('AnalyseMgr.aspx?Mode=Edit', 900, 500);
                    eventArgs.set_cancel(true);
                    break;
                default:
                    break;
            }
        }

        function getSelectedItems() {

           // alert($get('<%= hdnDeleteItems.ClientID %>').value);
            var inputs = $get(gvAnalysis).getElementsByTagName("input");
            var selectAll;
            var selectedItems='';
            
            for(var i=0;i<inputs.length;i++)
            {
                if (inputs[i].id == 'cbSelectAll')
                {
                    deleteAll=inputs[i];
                }
                else
                {
                    if (inputs[i].type=='checkbox' && inputs[i].checked)
                    {
                        selectedItems += ','+inputs[i].parentElement.itemID+',';
                    }
                }
            }
            $get('<%= hdnDeleteItems.ClientID %>').value = selectedItems;
            //alert($get('<%= hdnDeleteItems.ClientID %>').value);
        }
    </script>

    <table width="100%">
        <tr>
            <td>
             <div style="width:902px;">
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                    OnButtonClicked="Toolbar1_ButtonClicked" OnClientButtonClick="toolbar_ButtonClicked">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                        CssClassSelected="button_selected" />
                    <Items>
                        <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                            Text="查询" CausesValidation="True" ValidationGroup="1" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                            Text="生成差异分析报告" ValidationGroup="1" />
                        <SCS:ToolbarButton CausesValidation="True" Text="导出分析结果" CommandName="export" ImageUrl="~/App_Themes/Images/Toolbar/export.gif" />
                        <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                            Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
                        <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/validate.gif"
                            Text="差异分析" CommandName="analyse" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="AddAll" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                            Text="全部生成差异分析报告" ValidationGroup="1" />
                        <%--<SCS:ToolbarButton CommandName="Adjuest" ImageUrl="~/App_Themes/Images/Toolbar/Import.gif"
                            Text="批量调整" />
                        <SCS:ToolbarButton CommandName="Adjuest" ImageUrl="~/App_Themes/Images/Toolbar/Edit.gif"
                            Text="调整" />--%>
                    </Items>
                </SCS:Toolbar>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="width:888px;" >
                    <asp:MultiView ID="mvSearch" runat="server" ActiveViewIndex="0">
                        <asp:View ID="viewNewAnalyse" runat="server">
                            <table width="100%">
                                <tr>
                                    <td nowrap="nowrap">
                                        通知单号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoticeNo" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqValiNotiNo" runat="server" ControlToValidate="txtNoticeNo"
                                            Display="None" ErrorMessage="请输入要查询的通知单号" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="reqValiNotiNo_ValidatorCalloutExtender" runat="server"
                                            Enabled="True" HighlightCssClass="EditError" TargetControlID="reqValiNotiNo"
                                            PopupPosition="Right">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPartNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件中文名
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPartCName" runat="server"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:RadioButtonList ID="rblOption" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblOption_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Selected="True">新增</asp:ListItem>
                                            <asp:ListItem>浏览</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        F/U
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFollowUp" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        分析人
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnalyzer" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        车型
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSpecs" runat="server" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        报告状态
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="StatusName" DataValueField="StatusID"
                                            Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="viewAnalyse" runat="server">
                            <table width="100%" style="white-space: nowrap">
                                <tr>
                                    <td nowrap="nowrap">
                                        通知单号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptNoticeNo" runat="server" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRptNoticeNo"
                                            Display="None" ErrorMessage="请输入要查询的通知单号" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                            HighlightCssClass="EditError" TargetControlID="RequiredFieldValidator1">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptPartNo" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件中文名
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptPartCName" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        报告生成时间 从
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptTimeFrom" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtRptTimeFrom_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="btnCalendarStart" TargetControlID="txtRptTimeFrom" 
                                            Format="yyyy-MM-dd HH:mm:ss" CssClass ="Calendar">
                                        </cc1:CalendarExtender>
                                        <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" UserTimeFormat="TwentyFourHour" UserDateFormat="YearMonthDay" MaskType="DateTime" TargetControlID="txtRptTimeFrom" Mask="9999-99-99 99:99:99">
                                        </cc1:MaskedEditExtender>--%>
                                        <asp:ImageButton ID="btnCalendarStart" runat="server" CausesValidation="False" EnableViewState="False"
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        F/U
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptFU" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        车型
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptSpecs" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        报告状态
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptStatus" runat="server" DataTextField="StatusName" DataValueField="StatusID"
                                            Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        到
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRptTimeTo" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtRptTimeTo_CalendarExtender" runat="server" Enabled="True"
                                            PopupButtonID="btnCalendarEnd" TargetControlID="txtRptTimeTo" 
                                            Format="yyyy-MM-dd HH:mm:ss">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarEnd" runat="server" CausesValidation="False" EnableViewState="False"
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        工厂
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptPlant" runat="server" Width="100px">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="ddlRptPlant_CascadingDropDown" runat="server" Category="PlantCode"
                                            Enabled="True" ServiceMethod="GetPlantPageMethod" TargetControlID="ddlRptPlant"
                                            LoadingText="数据载入中...">
                                        </cc1:CascadingDropDown>
                                    </td>
                                    <td>
                                        车间
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptWorkshop" runat="server" Width="100px">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="ddlRptWorkshop_CascadingDropDown" runat="server" Category="WorkshopCode"
                                            Enabled="True" ParentControlID="ddlRptPlant" ServiceMethod="GetWorkshopsPageMethod"
                                            TargetControlID="ddlRptWorkshop" LoadingText="数据载入中...">
                                        </cc1:CascadingDropDown>
                                    </td>
                                    <td>
                                        工段
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptSegment" runat="server" Width="100px">
                                        </asp:DropDownList>
                                        <cc1:CascadingDropDown ID="ddlRptSegment_CascadingDropDown" runat="server" Category="SegmentCode"
                                            Enabled="True" LoadingText="数据载入中..." ParentControlID="ddlRptWorkshop" ServiceMethod="GetSegmentsPageMethod"
                                            TargetControlID="ddlRptSegment">
                                        </cc1:CascadingDropDown>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="viewAnalyseMgr" runat="server">
                            <table width="100%">
                                <tr>
                                    <td nowrap="nowrap">
                                        通知单号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrNoticeNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrPartNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件中文名
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrPartCName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        报告状态 &nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMgrStatus" runat="server" DataTextField="StatusName" DataValueField="StatusID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        F/U
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrFollowUp" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        分析人
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrAnalyzer" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        车型
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMgrSpecs" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        盈亏
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDiff" runat="server">
                                            <asp:ListItem Text="--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="盘盈" Value="+"></asp:ListItem>
                                            <asp:ListItem Text="盘亏" Value="-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        盈亏次数
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDiffTimesOp" Style="vertical-align: middle" 
                                            runat="server">
                                            <asp:ListItem Value="=" Text="="></asp:ListItem>
                                            <asp:ListItem Value="<" Text="<"></asp:ListItem>
                                            <asp:ListItem Value=">" Text=">"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtDiffTimes" runat="server" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        盈亏数量
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddiDiffAmountOp" Style="vertical-align: middle" 
                                            runat="server">
                                            <asp:ListItem Value="=" Text="="></asp:ListItem>
                                            <asp:ListItem Value=">" Text="<"></asp:ListItem>
                                            <asp:ListItem Value="<" Text=">"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtDiffAmount" runat="server" Width="90px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtDiffAmount_FilteredTextBoxExtender" runat="server"
                                    ValidChars="."        Enabled="True" FilterType="Numbers,Custom" TargetControlID="txtDiffAmount">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td>盈亏金额
                                    </td>
                                    <td><asp:DropDownList ID="ddiDiffSumOp" Style="vertical-align: middle" 
                                            runat="server">
                                            <asp:ListItem Value="=" Text="="></asp:ListItem>
                                            <asp:ListItem Value=">" Text="<"></asp:ListItem>
                                            <asp:ListItem Value="<" Text=">"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtDiffSum" runat="server" Width="111px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            Enabled="True" FilterType="Numbers,Custom" TargetControlID="txtDiffSum" ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td nowrap="nowrap">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:MultiView ID="mvNewReport" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewNewReport" runat="server">
                        <div class="divContainer" style="height: 320px; width:888px;">
                            <asp:GridView ID="gvNew" runat="server" AutoGenerateColumns="False" OnPreRender="gvNew_PreRender"
                                DataKeyNames="NotificationID,DetailsID" OnRowDataBound="gvNew_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="选择">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NotificationCode" HeaderText="通知单号" />
                                    <asp:BoundField DataField="PartCode" HeaderText="零件号" />
                                    <asp:BoundField DataField="PartChineseName" HeaderText="中文名称" />
                                    <asp:BoundField DataField="PartPlantCode" HeaderText="工厂" />
                                    <asp:BoundField DataField="DUNS" HeaderText="DUNS" />
                                    <asp:BoundField DataField="TypeName" HeaderText="盘点原因" />
                                    <asp:BoundField DataField="StocktakeStatusName" HeaderText="状态" />
                                </Columns>
                            </asp:GridView>
                        </div>
                      <div  style="width:900px;">
                        <uc1:AspPager ID="pagerNew" runat="server" />
                        </div>
                    </asp:View>
                    <asp:View ID="viewBrowseReport" runat="server">
                        <div class="divContainer" style="height: 320px;width:888px">
                            <asp:GridView ID="gvAnalysis" runat="server" AutoGenerateColumns="False" OnRowCreated="gvAnalysis_RowCreated"
                                OnPreRender="gvAnalysis_PreRender" ShowHeader="False" DataKeyNames="ItemID"
                                EnableViewState="False" OnRowDataBound="gvAnalysis_RowDataBound" 
                                AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="cbDeleteAll" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDelete" runat="server"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="差异报告编号" DataField="ReportCode" />
                                    <asp:BoundField HeaderText="通知单号" DataField="NotificationCode" />
                                    <asp:BoundField HeaderText="零件号" DataField="PartCode" />
                                    <asp:BoundField HeaderText="中文名称" DataField="PartChineseName" />
                                    <asp:BoundField HeaderText="工厂" DataField="PartPlantCode" />
                                    <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                                    <asp:BoundField HeaderText="盘点原因" DataField="TypeName" />
                                    <asp:BoundField HeaderText="状态" DataField="StatusName" />
                                    <asp:BoundField DataField="Wip" HeaderText="WIP"  DataFormatString="{0:0.####}"/>                                      
                                    <asp:BoundField HeaderText="分析" DataField="UnRecorded" />
                                    <asp:BoundField HeaderText="M080" DataField="M080" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="系统值" DataField="SysAvailable" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="Available" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="AvailableDiff" DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="AvailableDiffSum" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="SysQI" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="QI"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值" DataField="QIDiff" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="QIDiffSum" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="SysBlock"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="Block" DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="BlockDiff"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="BlockDiffSum" />
                                   <%-- <asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Available_SGM"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="SGMAvailable"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="SGMAvailableDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="SGMAvailableDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_QI_SGM"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="SGMQI"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="SGMQIDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="SGMQIDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Block_SGM"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="SGMBlock"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="SGMBlockDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="SGMBlockDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Available_RDC"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="RDCAvailable"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值" DataField="RDCAvailableDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RDCAvailableDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_QI_RDC"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="RDCQI"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值" DataField="RDCQIDiff"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RDCQIDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Block_RDC"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="RDCBlock"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="RDCBlockDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RDCBlockDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Available_Repair"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="RepairAvailable"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值" DataField="RepairAvailableDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RepairAvailableDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_QI_Repair"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值" DataField="RepairQI"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="RepairQIDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RepairQIDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值" DataField="Sys_Block_Repair"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="实际值" DataField="RepairBlock"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值" DataField="RepairBlockDiff"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataField="RepairBlockDiffSum" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%><asp:BoundField HeaderText="系统值"/>
                                    <asp:BoundField HeaderText="实际值"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值"  DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="差异值"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="系统值"   DataFormatString="{0:0.####}"/>
                                    <asp:BoundField HeaderText="实际值"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="差异值"  DataFormatString="{0:0.####}" />
                                    <asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" />
                                    <%--<asp:BoundField HeaderText="调整值" />--%>
                                    <asp:BoundField HeaderText="差异报告生成时间" DataField="TimeCreated" />
                                    <asp:BoundField HeaderText="分析人" DataField="AnalysedByUser" />
                                    <asp:BoundField HeaderText="报告反馈时间" DataField="TimeFeedback" />
                                    <asp:BoundField HeaderText="分析原因" />
                                    <asp:BoundField HeaderText="车型" DataField="Specs" />
                                    <asp:BoundField HeaderText="供应商名称" DataField="SupplierName" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="width:900px;">
                        <uc1:AspPager ID="pagerReport" runat="server" />
                        </div>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <input id="hdnDeleteItems" runat="server" type="hidden" />
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
