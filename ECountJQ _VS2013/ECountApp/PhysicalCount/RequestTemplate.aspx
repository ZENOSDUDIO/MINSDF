<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestTemplate.aspx.cs" Inherits="PhysicalCount_RequestTemplate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="asppager" TagPrefix="uc2" %>
<%@ Register src="../Common/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <script type="text/javascript" language="javascript">
    function ClearQuery() {
        $get('<%= txtPartCode.ClientID %>').value = '';
        $get('<%= txtPartName.ClientID %>').value = '';
        $get('<%= txtFollowUp.ClientID %>').value = '';
        $get('<%= txtBOOK.ClientID %>').value = '';
        $get('<%= ddlPlant.ClientID %>').value = '';
        return false;
    }
    function toolbar_ButtonClicked(sender, eventArgs) {
        if (eventArgs.get_selectedIndex() == 1 || eventArgs.get_selectedIndex() == 0) {
            //                $get('<%= panelSearchHeader.ClientID %>').click();
//            showWaitingModal();
//            eventArgs.set_cancel(false);
        }
        else {
            if (eventArgs.get_selectedIndex() == 2) {
                ClearQuery();
                eventArgs.set_cancel(true);
            }
        }
    }    
</script>

<uc1:ModalDialog ID="ModalDialog1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="toolBarDiv">
            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
                OnClientButtonClick="toolbar_ButtonClicked" EnableClientApi="true">
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                    CssClassSelected="button_selected" />
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                        Text="保存" ValidationGroup="0" />
                    <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                        Text="查询" ValidationGroup="0" />
                    <SCS:ToolbarButton CommandName="cancel" ImageUrl="~/App_Themes/Images/Toolbar/undo1.gif"
                        Text="取消" ValidationGroup="0" />
                </Items>
            </SCS:Toolbar>
        </div>
        <div id="headerDiv">
                                <table style="width: 100%">
                                <tr><td align="right">模板编号：</td>
                                    <td>
                                        <asp:Label ID="lblTemplateNo" runat="server"></asp:Label></td>
                                    <td align="right" valign="middle">
                                        模板名称：</td>
                                    <td valign="middle">
                                        
                                        <asp:TextBox ID="txtTemplateName" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td rowspan="2" align="right" valign="top">
                                        说明：</td>
                                    <td rowspan="2">
                                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="320px"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle">
                                            <asp:Label ID="Label10" runat="server" EnableViewState="False" Text="动静类型："></asp:Label></td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlIsStatic" runat="server">
                                                <asp:ListItem Value="True">静态</asp:ListItem>
                                                <asp:ListItem Value="False">动态</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="right" valign="middle">
                                            申请人：</td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
        </div>
        <asp:Panel ID="panelSearchHeader" runat="server" EnableViewState="False">
            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left; font-weight: bold;">
                    查询零件</div>
                <div style="float: left; margin-left: 20px;">
                    <asp:Label ID="Label6" runat="server" EnableViewState="False" Font-Bold="True"></asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:Image ID="Image1" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/App_Themes/Images/Toolbar/icon-expand.png" />
                </div>
            </div>
        </asp:Panel>
        <div id="divStocktakeRequest" style="height: 360px; width: 100%; overflow: auto">
            <asp:Panel ID="panelSearch" runat="server" Height="320px">
                <div id="divSearch" class="divContainer">
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
                                <asp:DropDownList ID="ddlPlant" runat="server" Width="109px" AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                F/U
                            </td>
                            <td>
                                <asp:TextBox ID="txtFollowUp" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                BOOK
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtBOOK" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="panelParts" CssClass="divContainer" Style="height: 200px; overflow: auto;"
                    runat="server">
                    <asp:GridView ID="gvParts" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvParts_RowDataBound"
                        DataKeyNames="PartID" OnPreRender="gvParts_PreRender" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelect" runat="server" OnCheckedChanged="partItem_CheckedChanged"
                                        AutoPostBack="True" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" OnCheckedChanged="partItem_CheckedChanged"
                                        AutoPostBack="True" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请类别">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlStocktakeType" OnSelectedIndexChanged="ddlStocktakeType_SelectedIndexChanged"
                                        ValidationGroup="2" AppendDataBoundItems="True" >
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="紧急程度">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPriority" runat="server" AppendDataBoundItems="True" ValidationGroup="2">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="零件号">
                                <ItemTemplate>
                                    <asp:HyperLink ID="linkPartCode" runat="server" NavigateUrl='<%# Eval("PartID", "../BizDataMaintain/PartEdit.aspx?readOnly=1&id={0}") %>'
                                        Text='<%# Eval("PartCode") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="中文名称" DataField="PartChineseName">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="工厂">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("PlantName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DUNS">
                                <ItemTemplate>
                                    <asp:Label ID="lblDUNS" runat="server" Text='<%# Eval("DUNS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="F/U" DataField="FollowUp"></asp:BoundField>
                            <asp:TemplateField HeaderText="物料类别">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="循环盘点级别">
                                <ItemTemplate>
                                    <asp:Label ID="lblCycleCountLevel" runat="server" Text='<%# Eval("LevelName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料状态">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartStatus" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="PartID" DataField="PartID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <div id="divPager">
                    <uc2:asppager ID="AspPager1" runat="server" PageSize="20" />
                </div>
                <div id="divOperation">
                    <asp:Button ID="btnAddPart" runat="server" OnClick="btnAddPart_Click" Text="加入申请单" style="cursor:hand"
                        ValidationGroup="2" BackColor="#EFF2F8" BorderColor="#CCCCCC" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                        ForeColor="#1B3A7A" />
                    <asp:CustomValidator ID="valiParts" runat="server" ErrorMessage="请选择需盘点的零件！" ValidationGroup="2"
                        OnServerValidate="valiParts_ServerValidate"></asp:CustomValidator>
                </div>
            </asp:Panel>
            <div class="divContainer" id="divParts"><asp:GridView ID="gvRequestParts" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvRequestParts_RowDataBound"
                    OnPreRender="gvRequestParts_PreRender" OnRowDeleting="gvRequestParts_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="盘点原因及备注">
                            <%--<EditItemTemplate>
                                <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("DetailsDesc") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("DetailsDesc") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="零件号">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PartCode") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文名称">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("PartChineseName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工厂">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PartPlantName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DUNS">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("DUNS") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请类别">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="requestItem_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="紧急程度">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlDetailPriority" runat="server" OnSelectedIndexChanged="requestItem_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F/U">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Followup") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料类别">
                            <ItemTemplate>
                                <asp:Label ID="lblPartCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="循环盘点级别">
                            <ItemTemplate>
                                <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("LevelName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料状态">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="移除" ShowHeader="False">
                            <HeaderTemplate>
                                <asp:LinkButton ID="linkDelete" runat="server" Font-Underline="True" ForeColor="White"
                                    >移除</asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="移除" ToolTip="全部移除"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="divContainer"><uc2:asppager ID="pagerParts" runat="server" PageSize="10" /></div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="2"
                ShowMessageBox="True" ShowSummary="False" Height="0px" />
            <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" runat="server"
                Enabled="True" TargetControlID="panelSearch" CollapseControlID="panelSearchHeader"
                ImageControlID="Image1" CollapsedText="(点击展开...)" ExpandedText="(点击折叠...)" CollapsedSize="0"
                ExpandControlID="panelSearchHeader" ExpandedSize="320" ScrollContents="False"
                TextLabelID="Label6" CollapsedImage="~/App_Themes/Images/collapse_blue.jpg" ExpandedImage="~/App_Themes/Images/Expand_blue.jpg">
            </cc1:CollapsiblePanelExtender>
            <asp:CustomValidator ID="valiCounts" runat="server" OnServerValidate="valiCounts_ServerValidate"></asp:CustomValidator>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
