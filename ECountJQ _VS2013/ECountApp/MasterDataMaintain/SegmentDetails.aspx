<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SegmentDetails.aspx.cs" Inherits="MasterDataMaintain_SegmentDetails" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body text="工段">
    <form id="form1" runat="server">
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked" Width="97%">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="button_selected"
                CssClassDisabled="button_disabled"></ButtonCssClasses>
            <Items>
                <SCS:ToolbarButton CausesValidation="True" Text="保存" ImageUrl="~/App_Themes/Images/Toolbar/save.gif"
                    ValidationGroup="save" CommandName="save" />
            </Items>
        </SCS:Toolbar>
        <table width = "650px">
            <tr>
                <td style="width :200px">
                    <asp:Label ID="Label2" runat="server" Text="工厂代码" Width = "80px"></asp:Label>
                    <asp:DropDownList ID="ddlPlant" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged"
                        Width="100" AppendDataBoundItems="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="车间代码" Width = "80px"></asp:Label>
                    <asp:DropDownList ID="ddlWorkshop" runat="server" Width="100" 
                        AppendDataBoundItems="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="工段代码" Width = "80px"></asp:Label>
                    <asp:TextBox ID="txtSegmentCode" runat="server" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="工段代码必填"
                        ValidationGroup="save" ControlToValidate="txtSegmentCode" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                        runat="server" Enabled="True" HighlightCssClass="EditError" TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="工段名称" Width = "80px"></asp:Label>
                    <asp:TextBox ID="txtSegmentName" runat="server" 
                        Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="工段名称必填"
                        ValidationGroup="save" ControlToValidate="txtSegmentName" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                        runat="server" Enabled="True" HighlightCssClass="EditError" TargetControlID="RequiredFieldValidator2">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
        </table>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
    </form>
</body>
</html>
