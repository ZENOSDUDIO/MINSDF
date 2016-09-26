<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreLocationTypeMg.aspx.cs"
    Inherits="SystemManagement_StoreLocationTypeMg" %>

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
            EnableClientApi="true" Width="570px">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                    Text="保存" ValidationGroup="save" />
                <%--<SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
            </Items>
        </SCS:Toolbar>
        <table cellspacing="5" border="0">
            <asp:HiddenField ID="hidTypeID" runat="server" />
            <tr>
                <td style="width: 15%;">
                    区域类型名称：
                </td>
                <td>
                    <asp:TextBox ID="txtTypeName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="区域类型名称不能为空" ControlToValidate="txtTypeName"
                        Display="None" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                            HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:CheckBox ID="cbRequired" runat="server" Text="必盘" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
