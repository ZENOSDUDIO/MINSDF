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
                            Text="����" />
                        <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/return.gif"
                            Text="����" CommandName="return" />
                        <SCS:ToolbarButton CausesValidation="False" Text="��һ��" CommandName="previous" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                            DisabledImageUrl="~/App_Themes/Images/Toolbar/left.gif" />
                        <SCS:ToolbarButton CommandName="next" ImageUrl="~/App_Themes/Images/Toolbar/right.gif"
                            Text="��һ��" DisabledImageUrl="~/App_Themes/Images/Toolbar/right.gif" />
                    </Items>
                </SCS:Toolbar>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer">
                    <asp:Image ID="Image2" runat="server" ImageAlign="TextTop" ImageUrl="~/App_Themes/Images/attention.gif" />
                    &nbsp; 
                    <span style="font-weight: bold; font-size:14px; white-space: normal;">ע�����������ܿ����ÿ��������з�����ȷ�ϣ������ڽӵ��������������������
                        5 ������������ɲ�����������������̵�Э����
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
                                        ���챨����
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReportNo" runat="server" Text='<%# Bind("DiffAnalyseReport.ReportCode") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        ֪ͨ����
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNotiNo" runat="server" Text='<%# Bind("DiffAnalyseReport.StocktakeNotification.NotificationCode") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        ֪ͨ����ʱ��
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPublishTime" runat="server" Text='<%# Bind("DiffAnalyseReport.StocktakeNotification.DatePublished") %>'
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        �̵�������ʱ��
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
                            <asp:CheckBox ID="cbAutoSave" runat="server" Text="���&quot;��һ��&quot;��&quot;��һ��&quot;�Զ�������Ϣ " />
                        </td>
                        <td>
                            <asp:Label ID="lblSummary" runat="server" Text="��{0}����������������{1}��"></asp:Label>
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
                                            ����
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            �����
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPart" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            ��������
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPartCName" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            ��Ӧ��DUNS
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDUNS" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                        </td>
                                        <td bgcolor="#5D7B9D" style="color: #FFFFFF">
                                            �̵�ԭ��
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
                            <asp:BoundField HeaderText="�����" DataField="PartCode" />
                            <asp:BoundField HeaderText="��������" DataField="PartChineseName" />
                            <asp:BoundField HeaderText="����" DataField="PartPlantCode" />
                            <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                            <asp:BoundField HeaderText="�̵�ԭ��" DataField="TypeName" />
                            <asp:BoundField HeaderText="״̬" DataField="StatusName" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="SysAvailable" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="Available" />
                            <asp:BoundField HeaderText="����ֵ" DataField="AvailableDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataFormatString="{0:c}" DataField="AvailableDiffSum" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="SysQI" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="QI" />
                            <asp:BoundField HeaderText="����ֵ" DataField="QIDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataFormatString="{0:c}" DataField="QIDiffSum" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="SysBlock" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="Block" />
                            <asp:BoundField HeaderText="����ֵ" DataField="BlockDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataFormatString="{0:c}" DataField="BlockDiffSum" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_Available_SGM" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="SGMAvailable" />
                            <asp:BoundField HeaderText="����ֵ" DataField="SGMAvailableDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="SGMAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_QI_SGM" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="SGMQI" />
                            <asp:BoundField HeaderText="����ֵ" DataField="SGMQIDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="SGMQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_Block_SGM" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="SGMBlock" />
                            <asp:BoundField HeaderText="����ֵ" DataField="SGMBlockDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="SGMBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_Available_RDC" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RDCAvailable" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RDCAvailableDiff" />
                            <%-- <asp:BoundField HeaderText="ӯ�����" DataField="RDCAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_QI_RDC" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RDCQI" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RDCQIDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="RDCQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RDCBlock" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RDCBlockDiff" />
                            <%--   <asp:BoundField HeaderText="ӯ�����" DataField="RDCBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_Available_Repair" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RepairAvailable" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RepairAvailableDiff" />
                            <%-- <asp:BoundField HeaderText="ӯ�����" DataField="RepairAvailableDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_QI_Repair" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RepairQI" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RepairQIDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="RepairQIDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="ϵͳֵ" DataField="Sys_Block_Repair" />
                            <asp:BoundField HeaderText="ʵ��ֵ" DataField="RepairBlock" />
                            <asp:BoundField HeaderText="����ֵ" DataField="RepairBlockDiff" />
                            <%--<asp:BoundField HeaderText="ӯ�����" DataField="RepairBlockDiffSum" DataFormatString="{0:c}" />--%>
                            <asp:BoundField HeaderText="����ֵ" />
                            <asp:BoundField HeaderText="���챨������ʱ��" DataField="TimeCreated" />
                            <asp:BoundField HeaderText="������" DataField="AnalysedByUser" />
                            <asp:BoundField HeaderText="���淴��ʱ��" DataField="TimeFeedback" />
                            <asp:BoundField HeaderText="����ʱ��" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <span class="style1">����̵������������ԭ��</span>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="height: 380px">
                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="DetailsID"
                        OnRowDataBound="gvDetails_RowDataBound" OnDataBound="gvDetails_DataBound">
                        <Columns>
                            <asp:BoundField DataField="GroupName" HeaderText="�������ܿ�" />
                            <asp:BoundField DataField="Description" HeaderText="������Ŀ����" ItemStyle-Wrap="True" />
                            <asp:TemplateField HeaderText="�������">
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
