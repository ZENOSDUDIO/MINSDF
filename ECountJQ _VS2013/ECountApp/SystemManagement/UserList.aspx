<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="SystemManagement_UserList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
            OnButtonClicked="Toolbar1_ButtonClicked">
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
                        <asp:Label ID="Label1" runat="server" Text="用户名" Width="50px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="用户组" Width="50px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUserGroup" runat="server" Width="100px" AppendDataBoundItems="True">
                            <asp:ListItem Value="">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divContainer" style="height: 395px;">
            <asp:GridView ID="gvUser" AutoGenerateColumns="False" runat="server" CellPadding="4"
                ForeColor="#333333" AllowPaging="false" OnPreRender="GridView1_PreRender" DataKeyNames="UserID">
                <Columns>
                    <asp:TemplateField HeaderText="删除标记">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSelected" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户名">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEdit_Click" CommandName='view'
                                CommandArgument='<%# Eval("UserID")%>'><%# Eval("UserName")%></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属用户组">
                        <ItemTemplate>
                            <%# Eval("UserGroup.GroupName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Password">
		                <itemtemplate>
			                <%# Eval("Password")%>
		                </itemtemplate>
		                <HeaderStyle HorizontalAlign="Left" />
	                </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="供应商DUNS">
		                <itemtemplate>
			                <%# Eval("Supplier.DUNS")%>
		                </itemtemplate>
		                <HeaderStyle HorizontalAlign="Left" />
	                </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="外协供应商DUNS">
		                <itemtemplate>
			                <%# Eval("ConsignmentDUNS")%>
		                </itemtemplate>
		                <HeaderStyle HorizontalAlign="Left" />
	                </asp:TemplateField>
	                <asp:TemplateField HeaderText="返修供应商DUNS">
		                <itemtemplate>
			                <%# Eval("RepairDUNS")%>
		                </itemtemplate>
		                <HeaderStyle HorizontalAlign="Left" />
	                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="工厂">
                        <ItemTemplate>
                            <%# Eval("Plant.PlantCode")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="车间">
                        <ItemTemplate>
                            <%# Eval("Workshop.WorkshopCode")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工段">
                        <ItemTemplate>
                            <%# Eval("Segment.SegmentName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建日期">
                        <ItemTemplate>
                            <%# Eval("CreateDate")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Available">
		                <itemtemplate>
			                <%# Eval("Available")%>
		                </itemtemplate>
		                <HeaderStyle HorizontalAlign="Left" />
	                </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
