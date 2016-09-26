<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreLocationMg.aspx.cs"
    Inherits="SystemManagement_StoreLocationMg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <uc1:ModalDialog ID="ModalDialog1" runat="server" />
    <div>
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
            EnableClientApi="true" Width="780px">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                    Text="保存" ValidationGroup="save" />
                <%-- <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
            </Items>
        </SCS:Toolbar>
        <table cellspacing="1px" width="800px" border="0">
            <asp:HiddenField ID="hidLocationID" runat="server" />
            <tr>
                <td style="width: 15%;">
                    存储区域名称
                </td>
                <td style="width: 25%;">
                    <asp:TextBox ID="txtLocationName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="存储区域名称不能为空" ControlToValidate="txtLocationName"
                        Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                        HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td style="width: 10%;">
                    区域类型
                </td>
                <td>
                    <asp:DropDownList ID="ddlTypeID" runat="server" Width="180px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="区域类型必选"
                        ControlToValidate="ddlTypeID" Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4"
                        HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                            <td>
                    物流系统存储区域
                </td>
                <td>
                    <asp:TextBox ID="txtLogisticsSysSLOC" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="对应物流系统存储区域不能为空" ControlToValidate="txtLogisticsSysSLOC"
                        Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3"
                        HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    工厂
                </td>
                <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="180px">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>

                <td colspan="2">
                    <asp:CheckBox ID="chkAvailableIncluded" Text="Available" runat="server" Checked="False" />
                    &nbsp;
                    <asp:CheckBox ID="chkQIIncluded" Text="QI" runat="server" Checked="False" />
                    &nbsp;
                    <asp:CheckBox ID="chkBlockIncluded" Text="Block" runat="server" Checked="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
