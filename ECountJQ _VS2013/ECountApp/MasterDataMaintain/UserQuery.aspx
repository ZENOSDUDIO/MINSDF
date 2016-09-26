<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserQuery.aspx.cs" Inherits="MasterDataMaintain_UserQuery" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="button_selected"
            CssClassDisabled="button_disabled"></ButtonCssClasses>
        <Items>
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="添加" />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除" />
            <SCS:ToolbarButton CausesValidation="True" 
                ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png" Text="查询" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <div id="divSearch" class="divContainer">
            <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="所属组"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
    </div>
</asp:Content>
