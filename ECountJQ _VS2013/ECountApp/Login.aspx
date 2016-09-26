<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ECount 系统登录</title>
    <link type="text/css" href="App_Themes/Default.css" />
</head>
<body style="margin-left: 0px; margin-top: 0px; height: 100%; filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr='#6cbdfe', endColorStr='#ffffff', gradientType='0')">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="height: 30%;">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Login ID="Login1" runat="server" BackColor="#E3EAEB" BorderColor="#E6E2D8" BorderPadding="4"
            BorderStyle="Solid" BorderWidth="1px" DestinationPageUrl="~/Default.aspx" 
            TextLayout="TextOnTop" FailureText="用户名或密码错误，请重试！" >
            <TextBoxStyle Font-Size="0.8em" />
            <LoginButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
            <LayoutTemplate>
                <table border="0" cellpadding="1" cellspacing="0" style="width: 409px; height: 219px;
                    text-align: center; background-repeat: repeat-x; background-image: url(/ECountApp/App_Themes/Images/ecount_log.gif)">
                    <tr>
                        <td>
                        </td>
                        <td valign="bottom">
                            <table border="0" cellpadding="0" style="width: 80%">
                                <tr>
                                    <td style="width: 56px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 228px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 56px; height: 30px; color: White; font-size: 9pt;
                                        vertical-align: middle">
                                        用户名：
                                    </td>
                                    <td style="width: 228px; height: 35px;">
                                        <asp:TextBox ID="UserName" runat="server" Width="180px" Height="20px" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="请输入用户名" ToolTip="请输入用户名" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 56px; height: 30px; color: White; font-size: 9pt;
                                        vertical-align: middle">
                                        密码：
                                    </td>
                                    <td style="width: 228px; height: 35px;">
                                        <asp:TextBox ID="Password" runat="server" MaxLength="12"
                                            Width="180px" Height="20px" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="请输入密码" ToolTip="请输入密码" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: Red; height: 17px; color: Red; font-size: 9pt;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False" Text=""></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" style="height: 24px; text-align: center">
                                        &nbsp;<asp:ImageButton ID="LoginButton" runat="server" ImageUrl="~/App_Themes/Images/loginBg.gif"
                                            OnClick="LoginButton_Click" CommandName="Login" />
                                        &nbsp;&nbsp;<asp:ImageButton ID="cancelButton" runat="server" ImageUrl="~/App_Themes/Images/cancelBg.gif"
                                            OnClick="LoginButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px">
                        </td>
                        <td style="height: 40px">
                        </td>
                        <td style="height: 22px">
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <TitleTextStyle BackColor="#1C5E55" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
