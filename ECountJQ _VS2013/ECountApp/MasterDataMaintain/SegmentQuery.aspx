<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SegmentQuery.aspx.cs" Inherits="BizDataMaintain_Segment" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/jscript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            if (eventArgs.get_selectedIndex() == 0) {
                showDialog('SegmentDetails.aspx?Mode=Edit', 700, 300, null, "refresh('<%= Toolbar1.Controls[2].ClientID%>')");
                eventArgs.set_cancel(true);
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
                Text="删除" ConfirmationMessage="确认删除？" CommandName="Delete" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="Search" 
                ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png" Text="查询" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <table width="100%">
            <tr>
                <td>
                    工厂</td>
                <td>
                    <asp:DropDownList ID="ddlPlantCode" runat="server" AppendDataBoundItems="True" Width="150px"
                        OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    车间</td>
                <td>
                    <asp:DropDownList ID="ddlwokshopcode" runat="server" AppendDataBoundItems="True"
                        Width="150px" AutoPostBack="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    工段代码</td>
                <td>
                    <asp:TextBox ID="txtSegmentCode" runat="server" Width = "150px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContainer" style="height: 330px;">
        <asp:GridView ID="gvSegments" runat="server" AutoGenerateColumns="False" onrowdatabound="gvSegments_RowDataBound" 
            DataKeyNames="SegmentID"  >
            
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
                    <ItemStyle Width="30px" HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server">修改</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工厂代码">
                    <ItemTemplate>
                        <%#Eval("Workshop.Plant.PlantCode")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车间代码">
                    <ItemTemplate>
                        <%#Eval("Workshop.WorkshopCode")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工段代码">
                    <ItemTemplate>
                        <%#Eval("SegmentCode")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工段名称">
                    <ItemTemplate>
                        <%#Eval("SegmentName")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
            </Columns>
           
        </asp:GridView>
    </div>
</asp:Content>
