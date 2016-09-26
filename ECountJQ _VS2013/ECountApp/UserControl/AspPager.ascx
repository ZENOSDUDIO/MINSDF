<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspPager.ascx.cs" Inherits="BizDataMaintain_AspPager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="pagerTxtFilter" %>
<div class="divContainer" >
<asp:Table ID="Table1" runat="server" >
    <asp:TableRow>
   
        <asp:TableCell Wrap="False" Font-Size="13px">
            <asp:Label ID="lbRecordCount" runat="server">
            共&nbsp;0&nbsp;条记录</asp:Label>
            ，每页&nbsp;
            <asp:DropDownList ID="ddlPageSize" Style="vertical-align: middle" AutoPostBack="True"
                runat="server" OnSelectedIndexChanged="OnPageSizeChange" Width="50px" CausesValidation="False">
                <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="50">50</asp:ListItem>
                <asp:ListItem Value="100">100</asp:ListItem>
                <asp:ListItem Value="200">200</asp:ListItem>
            </asp:DropDownList>
            &nbsp;条，当前第
            <asp:TextBox ID="txtDestinationPage" ValidationGroup="0" Style="vertical-align: middle;
                 border-top-width: 0px; border-left-width: 0px; border-right-width: 0px;text-align: center;" 
                 runat="server" Width="20px" OnTextChanged="DestinationPageChange_Click"
                CausesValidation="false" AutoPostBack="true" Text="1" MaxLength="4">
             </asp:TextBox>
            <pagerTxtFilter:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                FilterType="Numbers" TargetControlID="txtDestinationPage">
            </pagerTxtFilter:FilteredTextBoxExtender>
            /
            <asp:Label ID="lbTotalPage" runat="server">1
            </asp:Label>
            页&nbsp;&nbsp;
            <asp:LinkButton ID="bnFirstPage" runat="server" OnClick="FirstPage_Click" CausesValidation="False"><font face='webdings' >9</font></asp:LinkButton>
            &nbsp;&nbsp;
            <asp:LinkButton ID="bnPreviousPage" runat="server" OnClick="PreviousPage_Click" CausesValidation="False"><font face='webdings' color="#5D7B9D">3</font></asp:LinkButton>
            &nbsp;
        </asp:TableCell>
        <asp:TableCell ID="PageSelector" EnableViewState="True"  Font-Size="13px">
        </asp:TableCell>
        <asp:TableCell>
            &nbsp;<asp:LinkButton ID="bnNextPage" runat="server" OnClick="NextPage_Click" CausesValidation="False">
            <font face='webdings' color="#5D7B9D">4</font></asp:LinkButton>
            &nbsp;&nbsp;<asp:LinkButton ID="bnLastPage" runat="server" OnClick="LastPage_Click"
                CausesValidation="False"><font face='webdings' color="#5D7B9D">:</font></asp:LinkButton>
        </asp:TableCell>
        <asp:TableCell>
        &nbsp;
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</div>