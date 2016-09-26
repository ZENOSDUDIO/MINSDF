<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

        1,用户在第一次登录系统时，或者管理员重置用户密码后，系统强制要求用户更改密码。<br />
    2,系统会自动锁定超过90天未登录过系统的账号。<br />
    3,用户连续6次登录不成功，用户ID会被自动锁定。<br />
    4,用户密码符合以下规则，并要求定期更改。用户密码要求： 
    <br />
    &nbsp;&nbsp;a,密码长度至少为8个字符。 
    <br />
    &nbsp;&nbsp;b,不允许在密码中包含用户账号。 
    <br />
    &nbsp;&nbsp;c,用户必须至少每90天修改一次密码。 
    <br />
    &nbsp;&nbsp;d,密码采用强认证,必须至少包含一位大写英文字符、一位小写英文字符以及一位数字。<br /><table border="0" cellpadding="4" cellspacing="0" 
            style="border-collapse:collapse;">
            <tr>
                <td>
                    <table border="0" cellpadding="0">
                        <tr>
                            <td align="center" colspan="2" 
                                style="color:White;background-color:#507CD1;font-size:0.9em;font-weight:bold;">
                                更改密码</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="CurrentPasswordLabel" runat="server" 
                                    AssociatedControlID="CurrentPassword">密码:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="CurrentPassword" runat="server" Font-Size="0.8em" 
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                                    ControlToValidate="CurrentPassword" ErrorMessage="必须填写“密码”。" 
                                    ToolTip="必须填写“密码”。" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="NewPasswordLabel" runat="server" 
                                    AssociatedControlID="NewPassword">新密码:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="NewPassword" runat="server" Font-Size="0.8em" 
                                    TextMode="Password"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmNewPassword">确认新密码:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmNewPassword" runat="server" Font-Size="0.8em" 
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmNewPassword" ErrorMessage="必须填写“确认新密码”。" 
                                    ToolTip="必须填写“确认新密码”。" ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ControlToValidate="NewPassword" ErrorMessage="密码至少8位" 
                                    ValidationExpression="^\w{8,}$"></asp:RegularExpressionValidator>
                                
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="NewPassword" ErrorMessage="密码至少包含一位大写英文字符、一位小写英文字符以及一位数字" 
                                    
                                    ValidationExpression=".{0,}[A-Z]{1,}.{0,}[a-z]{1,}.{0,}[0-9]{1,}.{0,}|.{0,}[A-Z]{1,}.{0,}[0-9]{1,}.{0,}[a-z]{1,}.{0,}|.{0,}[a-z]{1,}.{0,}[A-Z]{1,}.{0,}[0-9]{1,}.{0,}|.{0,}[a-z]{1,}.{0,}[0-9]{1,}.{0,}[A-Z]{1,}.{0,}|.{0,}[0-9]{1,}.{0,}[A-Z]{1,}.{0,}[a-z]{1,}.{0,}|.{0,}[0-9]{1,}.{0,}[a-z]{1,}.{0,}[A-Z]{1,}.{0,}"></asp:RegularExpressionValidator>
                                
                                <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                    ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                    Display="Dynamic" ErrorMessage="“确认新密码”与“新密码”项必须匹配。" 
                                    ValidationGroup="ChangePassword1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="ChangePasswordPushButton" runat="server" BackColor="White" 
                                    BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" 
                                     Font-Names="Verdana" Font-Size="0.8em" 
                                    ForeColor="#284E98" Text="更改密码" ValidationGroup="ChangePassword1" 
                                    onclick="ChangePasswordPushButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

</asp:Content>

