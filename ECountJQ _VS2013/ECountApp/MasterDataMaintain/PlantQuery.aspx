<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PlantQuery.aspx.cs" Inherits="MasterDataMaintain_PlantQuery" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/jscript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            if (eventArgs.get_selectedIndex() == 0) {
                showDialog('PlantDetails.aspx?Mode=Edit', 600, 200, null, "refresh('<%= Toolbar1.Controls[2].ClientID%>')");
                eventArgs.set_cancel(true);
                //$find("ToolbarClient").raiseButtonClicked(new SCS.ToolbarClickEventArgs(-1,2));
            }
        }    
    </script>
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                    OnButtonClicked="Toolbar1_ButtonClicked" OnClientButtonClick="toolbar_ButtonClicked">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                        CssClassSelected="button_selected" />
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                            Text="添加" />
                        <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                            Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="search" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                            Text="查询" />
                    </Items>
                </SCS:Toolbar>
                <div class="divContainer">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="工厂" Width="100px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPlantCode" runat="server" AppendDataBoundItems="True" Width="150px">
                                    <asp:ListItem Text="--" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="divContainer" style="height: 330px">
                    <asp:GridView ID="gvPlants" runat="server" AutoGenerateColumns="False" DataKeyNames="PlantID"
                        OnRowDataBound="gvPlants_RowDataBound">
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged"
                                        AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="IsCheck" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="PlantID" Visible="False" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="LinkButton1_Command" CommandName="modify"
                                        CommandArgument='<%# Eval("PlantID") %>'>修改</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工厂代码">
                                <ItemTemplate>
                                    <%# Eval("PlantCode")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPlantCode" runat="server" Text='<%# Eval("PlantCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="工厂名称">
                                <ItemTemplate>
                                    <%# Eval("PlantName")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPlantName" runat="server" Text='<%# Eval("PlantName") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
 
</asp:Content>
