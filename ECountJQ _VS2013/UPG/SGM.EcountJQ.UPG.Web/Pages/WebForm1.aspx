<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SGM.ECountJQ.UPG.Web.Pages.WebForm1" %>

<%@ Register Src="GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="1000px" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 100%;">
                <div style="width: 1000px; max-height: 300px; overflow: auto; vertical-align: top;
                    text-align: left;">
                    <asp:GridView ID="gvUser" runat="server" Width="100%" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <uc1:GridViewPager ID="gvpUser" runat="server" OnPageIndexChanged="gvpUser_OnPageIndexChanged" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
