<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeNotice.aspx.cs"
    Inherits="PhysicalCount_StocktakeNotice" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />
</head>
<body>
    <form id="form1" runat="server">

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        var gvRemovedDetailsID = '<%= gvRemovedDetails.ClientID%>';
        var gvDetailsID = '<%= gvDetails.ClientID%>';</script>

    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td>
                    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                        OnButtonClicked="Toolbar1_ButtonClicked">
                        <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled"
                            CssClassSelected="button_selected" />
                        <Items>
                            <SCS:ToolbarButton CommandName="Save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                Text="生成盘点通知单" />
                            <%--<SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                                Text="查询" />
                            <SCS:ToolbarButton CommandName="RemoveAll" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                                ConfirmationMessage="确定移除通知单中全部零件？" Text="移除全部待盘零件" />--%>
                        </Items>
                    </SCS:Toolbar>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divContainer" style="width: 98%">
                        <asp:DataList ID="dlNotification" runat="server" Width="98%" DataKeyField="NotificationID"
                            OnItemDataBound="dlNotification_ItemDataBound">
                            <ItemTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblIsStatic" runat="server" OnSelectedIndexChanged="rblIsStatic_SelectedIndexChanged"
                                                onclick="showWaitingModal()" AutoPostBack="True" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="False">动态</asp:ListItem>
                                                <asp:ListItem Value="True">静态</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="Label9" runat="server" EnableViewState="False" Text="通知单号："></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblRequestNo" runat="server" Font-Bold="True" Text='<%# Eval("NotificationCode") %>'></asp:Label>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="Label10" runat="server" EnableViewState="False" Text="工厂："></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlPlant" runat="server" Width="80px" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged"
                                                onchange="showWaitingModal()" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <%--<asp:Label ID="Label13" runat="server" Font-Bold="True" Text='<%# Eval("Plant.PlantName") %>'></asp:Label>--%>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label11" runat="server" EnableViewState="False" Text="创建人："></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblCreator" runat="server" Text='<%# Eval("Creator.UserName") %>'></asp:Label>
                                        </td>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="Label12" runat="server" EnableViewState="False" Text="创建时间："></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("DateCreated") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="916px">
                        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                            <HeaderTemplate>
                                <asp:Label ID="Label2" runat="server" Text="通知单明细" Font-Size="10" EnableViewState="False"></asp:Label></HeaderTemplate>
                            <ContentTemplate>
                  <div class="divContainerFixed" style="width:880px;height:180px">             
                  <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="DetailsID"
                                    ID="gvDetails" OnPreRender="gv_PreRender" OnRowDataBound="gvDetails_RowDataBound"
                                    OnRowDeleting="gvDetails_RowDeleting" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelect" runat="server" onclick="selectItem('cbSelect', 'cbSelectAll', gvDetailsID)" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <%-- <asp:CheckBox ID="cbSelectAll" runat="server" Text="选择" TextAlign="Left" />--%>
                                                <input type="checkbox" id="cbSelectAll" onclick="selectAll(gvDetailsID,'cbSelect')" />
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="移除" OnClientClick="showWaitingModal()"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件号">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartCode" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="申请人注释" DataField="DetailsDesc" />
                                                                                <asp:TemplateField HeaderText="盘点类别">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="紧急程度">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("PriorityName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工厂">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlantCode" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="车间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkshops" runat="server" Text='<%# Bind("Workshops") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工段">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSegments" runat="server" Text='<%# Bind("Segments") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工位">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorklocation" runat="server" Text='<%# Bind("WorkLocation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件名称">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCName" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DUNS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDUNS" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            <asp:BoundField HeaderText="循环盘点级别" DataField="LevelName" /><asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtComment" runat="server" Text='<%# Bind("NotifyComments") %>'></asp:TextBox>
                                                <asp:Label ID="lblComment" runat="server" Text='<%# Bind("NotifyComments") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                       <%-- <asp:BoundField HeaderText="上次静态盘点日期" DataField="PreStaticNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次静态盘点通知单" DataField="PreStaticNoticeCode" />
                        <asp:BoundField HeaderText="上次动态盘点日期" DataField="PreDynamicNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次动态盘点通知单" DataField="PreDynamicNoticeCode" />--%>
                                        
                                        <asp:BoundField HeaderText="申请单号" DataField="RequestNumber" />
                        <asp:BoundField HeaderText="物料状态" DataField="StatusName" />
                                        <asp:TemplateField HeaderText="申请人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("RequestUser") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请时间">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldateRequest" runat="server" Text='<%# Bind("DateRequest") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        

                                    </Columns>
                                </asp:GridView></div> 
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                    <uc1:AspPager ID="pagerDetails" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:LinkButton ID="linkRemove" runat="server" OnClick="Remove" Style="background-image: url(../App_Themes/Images/Toolbar/Down.gif);
                        background-position: left; background-repeat: no-repeat" CssClass="LinkButton"
                        OnClientClick="showWaitingModal()">移出通知单</asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="linkAdd" runat="server" OnClick="Add" Style="background-image: url(../App_Themes/Images/Toolbar/Up.gif);
                        background-position: left; background-repeat: no-repeat" CssClass="LinkButton"
                        OnClientClick="showWaitingModal()">移入通知单</asp:LinkButton>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0"
                        Width="916px">
                        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel2">
                            <HeaderTemplate>
                                <asp:Label ID="Label1" runat="server" Text="待移除零件" EnableViewState="False" Font-Size="10"></asp:Label></HeaderTemplate>
                            <ContentTemplate>
                  <div class="divContainerFixed" style="width:880px;height:180px">
                                <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="DetailsID"
                                    ID="gvRemovedDetails" OnPreRender="gv_PreRender" OnRowDeleting="gvRemovedDetails_RowDeleting"
                                    OnRowDataBound="gvRemovedDetails_RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelect" runat="server" onclick="selectItem('cbSelect', 'cbSelectAll', gvRemovedDetailsID)" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <%--<asp:CheckBox ID="cbSelectAll" runat="server"  onclick="selectAll(gvDetailsID,'cbSelect')"  Text="选择" TextAlign="Left" />--%>
                                                <input type="checkbox" id="cbSelectAll" onclick="selectAll(gvRemovedDetailsID,'cbSelect')" /></HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="移入通知单" OnClientClick="showWaitingModal()"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件号">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartCode" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="申请人注释" DataField="DetailsDesc" />
                                        <asp:TemplateField HeaderText="工厂">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlantCode" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="车间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkshops" runat="server" Text='<%# Bind("Workshops") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工段">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSegments" runat="server" Text='<%# Bind("Segments") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工位">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorklocation" runat="server" Text='<%# Bind("WorkLocation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件名称">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCName" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DUNS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDUNS" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            <asp:BoundField HeaderText="循环盘点级别" DataField="LevelName" /><%--<asp:BoundField HeaderText="上次静态盘点日期" DataField="PreStaticNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次静态盘点通知单" DataField="PreStaticNoticeCode" />
                        <asp:BoundField HeaderText="上次动态盘点日期" DataField="PreDynamicNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次动态盘点通知单" DataField="PreDynamicNoticeCode" />--%>
                                        <asp:BoundField HeaderText="申请单号" DataField="RequestNumber" />
                                        <asp:TemplateField HeaderText="申请人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("CreateByUserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="盘点类别">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="紧急程度">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("PriorityName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
 </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer><uc1:AspPager ID="pagerRemovedDetails" runat="server" />
                </td>
            </tr>
            <%--<SCS:ToolbarButton CommandName="MoveIn" ImageUrl="~/App_Themes/Images/Toolbar/Up.gif"
                                        Text="移入待盘区" />
                                    <SCS:ToolbarButton CommandName="MoveOut" ImageUrl="~/App_Themes/Images/Toolbar/Down.gif"
                                        Text="移出待盘区" />--%>
        </table>
        <uc2:ModalDialog ID="ModalDialog1" runat="server" />
    </div>
    </form>
</body>
</html>
