<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AnalyseReportItem.aspx.cs" Inherits="PhysicalCount_AnalyseReportItem" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                    OnButtonClicked="Toolbar1_ButtonClicked">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                        CssClassSelected="button_selected" />
                    <Items>
                        <SCS:ToolbarButton CausesValidation="False" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                            Text="保存" />
                        <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/return.gif"
                            Text="返回" CommandName="return" />
                        <SCS:ToolbarButton CausesValidation="False" Text="上一条" CommandName="previous" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                            DisabledImageUrl="~/App_Themes/Images/Toolbar/left.gif" />
                        <SCS:ToolbarButton CommandName="next" ImageUrl="~/App_Themes/Images/Toolbar/right.gif"
                            Text="下一条" DisabledImageUrl="~/App_Themes/Images/Toolbar/right.gif" />
                    </Items>
                </SCS:Toolbar>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer">
                    <asp:Image ID="Image2" runat="server" ImageAlign="TextTop" ImageUrl="~/App_Themes/Images/attention.gif" />
                    &nbsp; 
                    <span style="font-weight: bold; font-size:14px; white-space: normal;">注：各分析功能块须对每项条款进行分析并确认，且须在接到库存差异分析报告后必须在
                        5 个工作日内完成差异分析并反馈给总盘点协调人
                        </span>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer">
                    <asp:FormView ID="fvTitle" runat="server" AllowPaging="True" Width="100%">
                        <PagerSettings Visible="False" />
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        差异报告编号
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReportNo" runat="server" Text='<%# Bind("DiffAnalyseReport.ReportCode") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        通知单号
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNotiNo" runat="server" Text='<%# Bind("DiffAnalyseReport.StocktakeNotification.NotificationCode") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        通知发出时间
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPublishTime" runat="server" Text='<%# Bind("DiffAnalyseReport.StocktakeNotification.DatePublished") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        盘点结果反馈时间
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFeedbackTime" runat="server" Text='<%# Bind("DiffAnalyseReport.StocktakeNotification.DateCounted") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbAutoSave" runat="server" Text="点击&quot;上一条&quot;，&quot;下一条&quot;自动保存信息 " />
                        </td>
                        <td>
                            <asp:Label ID="lblSummary" runat="server" Text="第{0}个差异分析零件，共{1}个"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:FormView ID="fvItem" runat="server" Width="100%"  ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <ItemTemplate>
                        <div class="divContainer">
                            <table cellspacing="0">
                                <tr>
                                    <tr>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            工厂
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            零件号
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPart" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            中文名称
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPartCName" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            供应商DUNS
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDUNS" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            盘点原因
                                        </td>
                                        <td>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                </asp:FormView>
                <div class="divContainer" style="width: 1036px; height: 113px">
                    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" OnRowCreated="gvItem_RowCreated"
                        ShowHeader="False" DataKeyNames="ItemID" EnableViewState="False">
                        <Columns>
                            <asp:BoundField HeaderText="零件号" DataField="PartCode" />
                            <asp:BoundField HeaderText="中文名称" DataField="PartChineseName" />
                            <asp:BoundField HeaderText="工厂" DataField="PartPlantCode" />
                            <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                            <asp:BoundField HeaderText="盘点原因" DataField="TypeName" />
                            <asp:BoundField HeaderText="状态" DataField="StatusName" />
                            <asp:BoundField HeaderText="系统值" DataField="SysAvailable" />
                            <asp:BoundField HeaderText="实际值" DataField="Available" />
                            <asp:BoundField HeaderText="差异值" DataField="AvailableDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="AvailableDiffSum" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="SysQI" />
                            <asp:BoundField HeaderText="实际值" DataField="QI" />
                            <asp:BoundField HeaderText="差异值" DataField="QIDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="QIDiffSum" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="SysBlock" />
                            <asp:BoundField HeaderText="实际值" DataField="Block" />
                            <asp:BoundField HeaderText="差异值" DataField="BlockDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataFormatString="{0:c}" DataField="BlockDiffSum" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_Available_SGM" />
                            <asp:BoundField HeaderText="实际值" DataField="SGMAvailable" />
                            <asp:BoundField HeaderText="差异值" DataField="SGMAvailableDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="SGMAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_QI_SGM" />
                            <asp:BoundField HeaderText="实际值" DataField="SGMQI" />
                            <asp:BoundField HeaderText="差异值" DataField="SGMQIDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="SGMQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_Block_SGM" />
                            <asp:BoundField HeaderText="实际值" DataField="SGMBlock" />
                            <asp:BoundField HeaderText="差异值" DataField="SGMBlockDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="SGMBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_Available_RDC" />
                            <asp:BoundField HeaderText="实际值" DataField="RDCAvailable" />
                            <asp:BoundField HeaderText="差异值" DataField="RDCAvailableDiff" />
                            <%-- <asp:BoundField HeaderText="盈亏金额" DataField="RDCAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_QI_RDC" />
                            <asp:BoundField HeaderText="实际值" DataField="RDCQI" />
                            <asp:BoundField HeaderText="差异值" DataField="RDCQIDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="RDCQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" />
                            <asp:BoundField HeaderText="实际值" DataField="RDCBlock" />
                            <asp:BoundField HeaderText="差异值" DataField="RDCBlockDiff" />
                            <%--   <asp:BoundField HeaderText="盈亏金额" DataField="RDCBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_Available_Repair" />
                            <asp:BoundField HeaderText="实际值" DataField="RepairAvailable" />
                            <asp:BoundField HeaderText="差异值" DataField="RepairAvailableDiff" />
                            <%-- <asp:BoundField HeaderText="盈亏金额" DataField="RepairAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_QI_Repair" />
                            <asp:BoundField HeaderText="实际值" DataField="RepairQI" />
                            <asp:BoundField HeaderText="差异值" DataField="RepairQIDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="RepairQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="系统值" DataField="Sys_Block_Repair" />
                            <asp:BoundField HeaderText="实际值" DataField="RepairBlock" />
                            <asp:BoundField HeaderText="差异值" DataField="RepairBlockDiff" />
                            <%--<asp:BoundField HeaderText="盈亏金额" DataField="RepairBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="调整值" />
                            <asp:BoundField HeaderText="差异报告生成时间" DataField="TimeCreated" />
                            <asp:BoundField HeaderText="分析人" DataField="AnalysedByUser" />
                            <asp:BoundField HeaderText="报告反馈时间" DataField="TimeFeedback" />
                            <asp:BoundField HeaderText="分析时间" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <span class="style1">造成盘点数量差异可能原因</span>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="height: 380px">
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="DetailsID"
                        OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                        <Columns>
                            <asp:BoundField DataField="GroupName" HeaderText="分析功能块" />
                            <asp:BoundField DataField="Description" HeaderText="分析项目条款" ItemStyle-Wrap="True" />
                            <asp:TemplateField HeaderText="分析结果">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsOK" runat="server" Text="IsOK?" Checked='<%# (Eval("Passed")==null)?false:Eval("Passed") %>' />
                                    &nbsp;<asp:TextBox ID="txtComments" runat="server" Width="256px" Text='<%# Bind("Comment") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="9">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
