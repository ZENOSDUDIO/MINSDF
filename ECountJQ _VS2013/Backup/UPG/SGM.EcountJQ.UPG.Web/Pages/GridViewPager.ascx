<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewPager.ascx.cs"
    Inherits="SGM.ECountJQ.UPG.Web.Pages.GridViewPager" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="Pager">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        共&nbsp;<font class="Number"><%= Pager.TotalCount %></font>&nbsp;条记录，每页&nbsp<asp:DropDownList
                            ID="ddlPageSize" AutoPostBack="True" runat="server" Width="50px" CausesValidation="False"
                            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;条，当前第&nbsp;<asp:TextBox ID="txtPageIndex" runat="server" AutoPostBack="True"
                            OnTextChanged="txtPageIndex_TextChanged" Width="40px" CssClass="CurrentIndex"></asp:TextBox>&nbsp;/&nbsp;<font
                                class="Number"><%= Pager.PageCount %></font>&nbsp;页
                        <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">第一页</asp:LinkButton>&nbsp;<asp:LinkButton
                            ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">上一页</asp:LinkButton>&nbsp;<asp:LinkButton
                                ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">下一页</asp:LinkButton>&nbsp;<asp:LinkButton
                                    ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">最后一页</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
