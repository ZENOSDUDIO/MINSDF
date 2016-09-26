<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartsSelect.aspx.cs" Inherits="BizDataMaintain_PartsSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
            OnButtonClicked="Toolbar1_ButtonClicked" Width="880px">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                    Text="查询" />
                <SCS:ToolbarButton CommandName="sure" ImageUrl="~/App_Themes/Images/Toolbar/add.gif"
                    Text="确定" />
            </Items>
        </SCS:Toolbar>
        <div class="divContainer">
            <table>
                <tr>
                    <td>
                        零件号
                    </td>
                    <td>
                        <asp:TextBox ID="txtPartCode" runat="server" Width="115px"></asp:TextBox>
                    </td>
                    <td>
                        零件名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtPartChineseName" runat="server" Width="115px"></asp:TextBox>
                    </td>
                    <td>
                        循环盘点级别
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCycleCountLevel" runat="server" Width="115px" AppendDataBoundItems="True">
                            <asp:ListItem>--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        最后修改用户
                    </td>
                    <td>
                        <asp:TextBox ID="txtUpdateBy" runat="server" Width="115px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        车型
                    </td>
                    <td>
                        <asp:TextBox ID="txtSpecs" runat="server" Width="115px"></asp:TextBox>
                    </td>
                    <td>
                        FollowUp
                    </td>
                    <td>
                        <asp:TextBox ID="txtFollowUp" runat="server" Width="115px"></asp:TextBox>
                    </td>
                    <td>
                        物料类别
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoryID" runat="server" Width="115px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        物料状态
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPartStatus" runat="server" Width="115px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        工厂
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlantID" runat="server" OnSelectedIndexChanged="ddlPlantID_SelectedIndexChanged"
                            AutoPostBack="true" Width="115px" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        车间
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWorkshopID" runat="server" OnSelectedIndexChanged="ddlWorkshopID_SelectedIndexChanged"
                            AutoPostBack="true" Width="115px" AppendDataBoundItems="True">
                            <asp:ListItem>--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        工段
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSegmentID" runat="server" Width="115px" AppendDataBoundItems="True">
                            <asp:ListItem>--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        工位
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkLocation" runat="server" Width="115px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        DUNS
                    </td>
                    <td>
                        <asp:TextBox ID="txtDUNS" runat="server" Width="115px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divContainer" style="height: 270px">
            <input id="Hidden1" type="hidden" runat="server" />
            <asp:GridView ID="gvParts" AutoGenerateColumns="False" Width="97%" runat="server"
                AllowPaging="false" OnPreRender="gvParts_PreRender" DataKeyNames="PartID">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSelected" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="零件号" SortExpression="PartCode">
                        <ItemTemplate>
                            <%# Eval("PartCode")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="主键" Visible="False">
                        <ItemTemplate>
                            <%# Eval("PartID")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="零件中文名称" SortExpression="PartChineseName">
                        <ItemTemplate>
                            <%# Eval("PartChineseName")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工位">
                        <ItemTemplate>
                            <%# Eval("WorkLocation")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="循环盘点级别">
                        <ItemTemplate>
                            <%# Eval("LevelName")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="车型">
                        <ItemTemplate>
                            <%# Eval("Specs")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Followup">
                        <ItemTemplate>
                            <%# Eval("Followup")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工厂">
                        <ItemTemplate>
                            <%# Eval("PlantName")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料类别">
                        <ItemTemplate>
                            <%# Eval("CategoryName")%></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <uc1:AspPager ID="AspPager1" runat="server" Visible="True" />
        <uc2:ModalDialog ID="ModalDialog1" runat="server" />
    </div>
    </form>
</body>
</html>
