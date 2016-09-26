<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeRequest.aspx.cs"
    Inherits="PhysicalCount_StocktakeRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="asppager" TagPrefix="uc2" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<%--    <div>--%>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <script type="text/javascript" language="javascript">
            function ClearQuery() {

                return false;
            }
            function toolbar_ButtonClicked(sender, eventArgs) {
                if (eventArgs.get_selectedIndex() == 2) {
                    ClearQuery();
                    eventArgs.set_cancel(true);
                }
            }
            var gvPartsID = '<%= gvParts.ClientID%>';
            var gvRequestPartsID = '<%= gvRequestParts.ClientID%>';
        </script>
        <uc1:ModalDialog ID="ModalDialog1" runat="server" />
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
            OnClientButtonClick="toolbar_ButtonClicked" EnableClientApi="true">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                    Text="保存" ValidationGroup="2" />
                <%--<SCS:ToolbarButton CausesValidation="True" CommandName="SaveAsTemplate" ImageUrl="~/App_Themes/Images/Toolbar/Template.gif"
                        Text="保存为模板" ValidationGroup="0" />--%>
                <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                    Text="查询" ValidationGroup="0" />
            </Items>
        </SCS:Toolbar>
        <asp:MultiView ID="multiViewRequest" runat="server" ActiveViewIndex="1" EnableTheming="False">
            <asp:View ID="viewRequest" runat="server">
                <div id="divReqHeaderContainer" class="divContainer">
                    <asp:DataList ID="dlRequest" runat="server" Width="98%" DataKeyField="RequestID"
                        OnItemDataBound="dlRequest_ItemDataBound">
                        <ItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td align="right" valign="middle">
                                        <asp:Label ID="Label9" runat="server" EnableViewState="False" Text="申请单号："></asp:Label>
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>' Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="right" valign="middle">
                                        <asp:Label ID="Label10" runat="server" EnableViewState="False" Text="动静类型："></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlIsStatic" runat="server">
                                            <asp:ListItem Value="True">静态</asp:ListItem>
                                            <asp:ListItem Value="False">动态</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label11" runat="server" EnableViewState="False" Text="申请人："></asp:Label>
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label>
                                    </td>
                                    <td align="right" valign="middle">
                                        <asp:Label ID="Label12" runat="server" Text="申请时间：" EnableViewState="False"></asp:Label>
                                    </td>
                                    <td align="left" valign="middle">
                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("DateRequest") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList></div>
            </asp:View>
            <asp:View ID="viewEmpty" runat="server">
            </asp:View>
        </asp:MultiView>
        <asp:Panel ID="panelSearchHeader" runat="server" EnableViewState="False" CssClass="divContainer" >
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; font-weight: bold;">
                    查询零件
                </div>
                <div style="float: left; margin-left: 20px;">
                    <asp:Label ID="Label6" runat="server" EnableViewState="False" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:Image ID="Image1" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/App_Themes/Images/Toolbar/icon-expand.png" />
                </div>
            </div>
        </asp:Panel>
        <div id="divStocktakeRequest" style="height: 420px; width: 100%; overflow: auto">
            <asp:Panel ID="panelSearch" runat="server" CssClass="divContainer">
                <div id="divSearch">
                    <table style="width: 100%" id="tblPartsQry">
                        <tr>
                            <td>
                                零件号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCode" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                零件名
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                工厂
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPlant" runat="server" Width="109px">
                                </asp:DropDownList>
                            </td>                       
                            <td>
                                F/U
                            </td>
                            <td>
                                <asp:TextBox ID="txtFollowUp" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td>
                                BOOK
                            </td>
                            <td>
                                <asp:TextBox ID="txtBOOK" runat="server" Width="120px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>               
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="1060px">
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1"><HeaderTemplate><label style="font-size: small">查询零件列表</label></HeaderTemplate><ContentTemplate>
                <asp:Panel ID="panelParts" CssClass="divContainer" Style="height: 200px;width:98%" runat="server"> <asp:GridView ID="gvParts" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvParts_RowDataBound"
                        DataKeyNames="PartID" OnPreRender="gvParts_PreRender" 
                        onrowdeleted="gvParts_RowDeleted" onrowdeleting="gvParts_RowDeleting" >
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelect" runat="server" onclick="selectItem('cbSelect', 'cbSelectAll', gvPartsID)"/>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input type="checkbox" id="cbSelectAll"  onclick="selectAll(gvPartsID,'cbSelect')" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="False" 
                                        CommandName="Delete" Text="加入申请单" OnClientClick="showWaitingModal()"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请类别">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStocktakeType" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="紧急程度">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPriority" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="零件号">
                            <ItemTemplate>
                                <asp:Label ID="lblPartNo" runat="server" Text='<%# Eval("PartCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="中文名称">
                            <ItemTemplate>
                                <asp:Label ID="lblCName" runat="server" Text='<%# Eval("PartChineseName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="英文名称">
                                <ItemTemplate>
                                    <asp:Label ID="lblEName" runat="server" Text='<%# Eval("PartEnglishName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工厂">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlantCode" runat="server" Text='<%# Eval("PlantCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="车间" DataField="Workshops" />
                            <asp:BoundField HeaderText="工段" DataField="Segments" />
                            <asp:BoundField HeaderText="工位" DataField="WorkLocation" />
                        <asp:TemplateField HeaderText="DUNS">
                            <ItemTemplate>
                                <asp:Label ID="lblDUNS" runat="server" Text='<%# Eval("DUNS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="F/U">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowup" runat="server" Text='<%# Eval("Followup") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料类别">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartCategory" runat="server" 
                                        Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="循环盘点级别">
                                <ItemTemplate>
                                    <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("LevelName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料状态">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartStatus" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="PartID" DataField="PartID" Visible="False" />
                            <asp:BoundField HeaderText="上次静态盘点日期" DataField="PreStaticNotiTime" DataFormatString="{0:G}" />
                            <asp:BoundField HeaderText="上次静态盘点通知单" DataField="PreStaticNotiCode" />
                            <asp:BoundField HeaderText="上次动态盘点日期" DataField="PreDynamicNotiTime" DataFormatString="{0:G}" />
                            <asp:BoundField HeaderText="上次动态盘点通知单" DataField="PreDynamicNotiCode" />
                        </Columns>
                    </asp:GridView>
                  
                </asp:Panel></ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                <uc2:asppager ID="pagerParts" runat="server" PageSize="20" /></asp:Panel>
                <div id="divOperation" class="divContainer">
                    &nbsp;
                    <asp:LinkButton ID="linkAdd" runat="server" onclick="btnAddPart_Click" style="background-image: url(../App_Themes/Images/Toolbar/Down.gif); background-position:left;background-repeat:no-repeat"
                        ValidationGroup="3" CssClass="LinkButton" OnClientClick="showWaitingModal()">加入申请单</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="linkRemove" runat="server" CausesValidation="False" style="background-image: url(../App_Themes/Images/Toolbar/Up.gif); background-position:left;background-repeat:no-repeat"
                        onclick="linkRemove_Click" CssClass="LinkButton" 
                        onclientclick="showWaitingModal()">移出申请单</asp:LinkButton>
                    <asp:ValidationSummary ID="validSumDetails" runat="server"
                            DisplayMode="SingleParagraph" HeaderText="申请类别和紧急程度不可为空！" ValidationGroup="2" ShowMessageBox="True" />
                    <asp:CustomValidator ID="valiParts" runat="server" ErrorMessage="请选择需盘点的零件！" ValidationGroup="3"
                        OnServerValidate="valiParts_ServerValidate" Display="Dynamic" Text="请选择需盘点的零件！">
                        <asp:CustomValidator ID="valiDetails" runat="server" ValidationGroup="2" OnServerValidate="valiDetails_ServerValidate"
                            Display="Dynamic"></asp:CustomValidator></asp:CustomValidator>
                    <asp:CustomValidator ID="valiCounts" runat="server" 
                        OnServerValidate="valiCounts_ServerValidate" ValidationGroup="3">已超过当前用户盘点数量上限！</asp:CustomValidator>
                </div>
            <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" 
                Width="1060px">
            <cc1:TabPanel ID="TabPanel2" runat="server" ><HeaderTemplate><label style="font-size: small">待盘零件列表</label></HeaderTemplate><ContentTemplate>
                <div id="divDetails" style="height: 300px;" class="divContainer">
                <asp:GridView ID="gvRequestParts" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvRequestParts_RowDataBound"
                    OnPreRender="gvRequestParts_PreRender" OnRowDeleting="gvRequestParts_RowDeleting"
                    DataKeyNames="PartID,DetailsID">
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate><input id="cbSelectAll" onclick="selectAll(gvRequestPartsID,'cbSelect')" type="checkbox" /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" onclick="selectItem('cbSelect', 'cbSelectAll', gvRequestPartsID)" runat="server" />
                            </ItemTemplate>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="移除" OnClientClick="showWaitingModal()"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="盘点原因及备注">
                            <ItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("DetailsDesc") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="零件号">
                            <ItemTemplate>
                                <asp:Label ID="lblPartNo" runat="server" Text='<%# Eval("PartCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文名称">
                            <ItemTemplate>
                                <asp:Label ID="lblCName" runat="server" Text='<%# Eval("PartChineseName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="英文名称">
                            <ItemTemplate>
                                <asp:Label ID="lblEName" runat="server" Text='<%# Eval("PartEnglishName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工厂">
                            <ItemTemplate>
                                <asp:Label ID="lblPlantCode" runat="server" Text='<%# Eval("PartPlantCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField HeaderText="车间" DataField="Workshops" />
                            <asp:BoundField HeaderText="工段" DataField="Segments" />
                            <asp:BoundField HeaderText="工位" DataField="WorkLocation" />
                        <asp:TemplateField HeaderText="DUNS">
                            <ItemTemplate>
                                <asp:Label ID="lblDUNS" runat="server" Text='<%# Eval("DUNS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请类别">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStocktakeType" runat="server" ValidationGroup="2" CausesValidation="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValiStocktakeType" runat="server" ControlToValidate="ddlStocktakeType"
                                    ValidationGroup="2" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="紧急程度">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlPriority" runat="server" ValidationGroup="2" CausesValidation="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValiPriority" runat="server" ControlToValidate="ddlPriority"
                                    ValidationGroup="2" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F/U">
                            <ItemTemplate>
                                <asp:Label ID="lblFollowup" runat="server" Text='<%# Eval("Followup") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料类别">
                            <ItemTemplate>
                                <asp:Label ID="lblPartCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="循环盘点级别">
                            <ItemTemplate>
                                <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("LevelName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料状态">
                            <ItemTemplate>
                                <asp:Label ID="lblPartStatus" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="上次静态盘点日期" DataField="PreStaticNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次静态盘点通知单" DataField="PreStaticNoticeCode" />
                        <asp:BoundField HeaderText="上次动态盘点日期" DataField="PreDynamicNotiTime" DataFormatString="{0:G}" />
                        <asp:BoundField HeaderText="上次动态盘点通知单" DataField="PreDynamicNoticeCode" />
                    </Columns>
                </asp:GridView>
            </div></ContentTemplate></cc1:TabPanel>
            </cc1:TabContainer>
            <uc2:asppager ID="pagerRequestParts" runat="server" PageSize="10" />
            <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" runat="server"
                Enabled="True" TargetControlID="panelSearch" CollapseControlID="panelSearchHeader"
                ImageControlID="Image1" CollapsedText="(点击展开...)" ExpandedText="(点击折叠...)" CollapsedSize="0"
                ExpandControlID="panelSearchHeader" ExpandedSize="320" ScrollContents="False"
                TextLabelID="Label6" CollapsedImage="~/App_Themes/Images/collapse_blue.jpg" ExpandedImage="~/App_Themes/Images/Expand_blue.jpg">
            </cc1:CollapsiblePanelExtender>
            
        </div>
<%--    </div>--%>
    </form>
</body>
</html>
