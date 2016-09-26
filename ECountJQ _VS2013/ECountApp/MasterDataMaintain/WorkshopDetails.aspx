<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="WorkshopDetails.aspx.cs"
    Inherits="MasterDataMaintain_WorkshopDetails" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" class="style1">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true" Width="97%">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected"></ButtonCssClasses>
        <Items>
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/save.gif"
                Text="保存" ValidationGroup="save" CommandName="save" />
        </Items>
    </SCS:Toolbar>
    <table width  ="700px">
        <tr>
            <td style="width :150px">
                <asp:Label ID="Label1" runat="server" Text="工厂代码"></asp:Label>
                <asp:DropDownList ID="ddlPlant" runat="server" Width="90px">
                </asp:DropDownList>
            </td>
            <td style= "width:170px">
                <asp:Label ID="Label2" runat="server" Text="车间代码"></asp:Label>
                <asp:TextBox ID="txtWorkshopCode" runat="server" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWorkshopCode"
                    Display="None" ErrorMessage="车间代码为必填" ValidationGroup="save"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" HighlightCssClass="EditError" TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td style ="width: 200px">
                <asp:Label ID="Label3" runat="server" Text="车间名称"></asp:Label>
                <asp:TextBox ID="txtWorkshopName" runat="server" OnTextChanged="txtWorkshopName_TextChanged"
                    Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWorkshopName"
                    Display="None" ErrorMessage="车间名称为必填" ValidationGroup="save"></asp:RequiredFieldValidator>
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
