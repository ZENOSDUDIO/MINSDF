<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DifferenceAnalvzeItemDetails.aspx.cs"
    Inherits="SystemManagement_DifferenceAnalvzeItemDetails" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
            Width="97%">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="button_selected"
                CssClassDisabled=""></ButtonCssClasses>
            <Items>
                <SCS:ToolbarButton CausesValidation="True" Text="保存" ImageUrl="~/App_Themes/Images/Toolbar/save.gif"
                    CommandName="save" ValidationGroup="save" />
            </Items>
        </SCS:Toolbar>
        <table width="60%">
            <tr>
                <td style="width: 90px">
                    <asp:Label ID="Label1" runat="server" Text="用户组" Width="90px"></asp:Label>
                </td>
                <td style="width: 150px">
                    <asp:DropDownList ID="ddlUserGroup" runat="server" AppendDataBoundItems="True" Width ="150px">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 90px">
                    <asp:Label ID="Label2" runat="server" Text="差异分析项描述" Width="90px"></asp:Label>
                </td>
                <td style="width: 150px">
                    <asp:TextBox ID="txtDescription" runat="server" Height="50px" TextMode="MultiLine"
                        CssClass="style1" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="必填" ControlToValidate="txtDescription" 
                        ValidationGroup="save">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
        </table>
    </div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    </form>
</body>
</html>
