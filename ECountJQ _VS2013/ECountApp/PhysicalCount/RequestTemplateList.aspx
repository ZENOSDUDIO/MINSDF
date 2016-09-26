<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequestTemplateList.aspx.cs" Inherits="PhysicalCount_RequestTemplateList" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                            OnButtonClicked="Toolbar1_ButtonClicked" OnClientButtonClick="toolbar_ButtonClicked">
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                                CssClassSelected="button_selected" />
                            <Items>
                                <SCS:ToolbarButton CausesValidation="False" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                                    Text="创建申请单模板" />
                                <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                                    Text="删除申请单模板" ConfirmationMessage="确认删除申请单？" CommandName="delete" />
                                <SCS:ToolbarButton CausesValidation="False" Text="导入申请单" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif" />
                                <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                                    Text="查询" />
                            </Items>
                        </SCS:Toolbar>
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:BoundField DataField="TemplateNo" HeaderText="模板编号" />
            <asp:BoundField DataField="TemplateName" HeaderText="模板名称" />
            <asp:BoundField DataField="IsStatic" HeaderText="动静类型" />
            <asp:BoundField DataField="CreateBy" HeaderText="制作人" />
            <asp:BoundField DataField="Description" HeaderText="说明" />
        </Columns>
    </asp:GridView>
</asp:Content>

