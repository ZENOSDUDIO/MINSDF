<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="WorkshopQuery.aspx.cs" Inherits="MasterDataMaintain_Workshop" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/jscript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            if (eventArgs.get_selectedIndex() == 0) {
                showDialog('WorkshopDetails.aspx?Mode=Edit', 700, 300, null, "refresh('<%= Toolbar1.Controls[2].ClientID%>')");
                eventArgs.set_cancel(true);
            }
        }    
    </script>
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
        OnButtonClicked="Toolbar1_ButtonClicked" OnClientButtonClick="toolbar_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="True" CommandName="Add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="添加" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="Delete" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除" ConfirmationMessage="确认删除?" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="search" 
                ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png" Text="查询" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="工厂" Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPlantCode" runat="server" AppendDataBoundItems="True" 
                        Width="150px" onselectedindexchanged="ddlPlantCode_SelectedIndexChanged" 
                        AutoPostBack="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="车间"  Width="100px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlwokshopcode" runat="server" 
                        AppendDataBoundItems="True" Width ="150px" AutoPostBack="True">
                        <asp:ListItem Text="--" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContainer" style="height:400px">
        <asp:GridView ID="gvWorkshops" runat="server" AutoGenerateColumns="False"  OnRowDataBound="gvWorkshops_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged"
                            AutoPostBack="true" />
                    </HeaderTemplate>
                    <HeaderStyle Width="30px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="IsCheck" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="WorkshopID" Visible="False" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"   CommandArgument='<%# Eval("WorkshopID") %>'
                            CommandName="modify">修改</asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工厂代码">
                    <ItemTemplate>
                        <%#Eval("Plant.PlantCode") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车间代码">
                    <ItemTemplate>
                        <%# Eval("WorkshopCode") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车间名称">
                    <ItemTemplate>
                        <%# Eval("WorshopName") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
