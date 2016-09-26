<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserManagement.aspx.cs" Inherits="SystemManagement_UserManagement" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>

        function bindReturnValue2() {
            var v = getReturnValue();
            var s = v.split('∑');
            document.getElementById('<%= hidcSupplierID.ClientID%>').value = unescape(s[0]);
            document.getElementById('<%= txtConsignmentDUNS.ClientID%>').value = unescape(s[1]);
        }
        function bindReturnValue3() {
            var v = getReturnValue();
            var s = v.split('∑');
            document.getElementById('<%= hidrSupplierID.ClientID%>').value = unescape(s[0]);
            document.getElementById('<%= txtRepairDUNS.ClientID%>').value = unescape(s[1]);
        }

        function popupWindow2() {
            showDialog('../BizDataMaintain/SupplierSelect.aspx', 625, 365, 'bindReturnValue2();');
        }
        function popupWindow3() {
            showDialog('../BizDataMaintain/SupplierSelect.aspx', 625, 365, 'bindReturnValue3();');
        }
    </script>

    <fieldset style="width: 800px">
        <legend class="mainTitle">用户编辑</legend>
        <div>
            <table cellspacing="5" width="800px" border="0">
                <asp:HiddenField ID="hidUserID" runat="server" />
                <tr>
                    <td colspan="4">
                        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
                            EnableClientApi="true" Width="780px">
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                                CssClassSelected="button_selected" />
                            <Items>
                                <SCS:ToolbarButton CausesValidation="False" CommandName="unlock" ImageUrl="~/App_Themes/Images/Toolbar/unlock.gif"
                                    Text="解锁"  Enabled="False" DisabledImageUrl="~/App_Themes/Images/Toolbar/unlock.gif" />
                                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                    Text="保存" ValidationGroup="save" DisabledImageUrl="~/App_Themes/Images/Toolbar/Save.gif" />
                                <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                                    Text="返回" CommandName="return" />
                            </Items>
                        </SCS:Toolbar>
                    </td>
                </tr>
                <tr>
                    <td width="160">
                        用户名
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqUserName" runat="server" ControlToValidate="txtUserName"
                            ErrorMessage="必填" ValidationGroup="save">*</asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ReqUserName_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="ReqUserName">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td width="160">
                        初始密码
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" Width="200px" 
                            MaxLength="8"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqPassword" runat="server" 
                            ErrorMessage="必填" ControlToValidate="txtPassword" 
                            ValidationGroup="save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
<%--                    <td>
                        供应商DUNS
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:HiddenField ID="hidSupplierID" runat="server" />
                        <asp:TextBox ID="txtSupplier" runat="server" Width="120px" 
                            onpaste="return false;" onkeypress="return false;"></asp:TextBox>
                        <input id="Button1" onclick="popupWindow1(); return false;" type="button" value="选择DUNS"
                            style="width: 80px" />
                    </td>--%>
                    <td>
                        返修供应商DUNS
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:HiddenField ID="hidrSupplierID" runat="server" />
                        <asp:TextBox ID="txtRepairDUNS" runat="server" Width="120px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox>
                        <input id="Button1" onclick="popupWindow3(); return false;" type="button" value="选择DUNS"
                            style="width: 80px" />
                    </td>
                    <td>
                        外协供应商DUNS
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:HiddenField ID="hidcSupplierID" runat="server" />
                        <asp:TextBox ID="txtConsignmentDUNS" runat="server" Width="120px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox>
                        <input id="Button2" onclick="popupWindow2(); return false;" type="button" value="选择DUNS"
                            style="width: 80px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        工 厂
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlantID" runat="server" Width="180px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlPlantID_SelectedIndexChanged" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value ="">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        所属用户组
                     </td>
                    <td>
                        <asp:DropDownList ID="ddlUserGroupID" runat="server" Width="180px" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value = "">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        工 段
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSegmentID" runat="server" Width="180px" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        车 间
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWorkShopID" runat="server" Width="180px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlWorkShopID_SelectedIndexChanged" 
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
