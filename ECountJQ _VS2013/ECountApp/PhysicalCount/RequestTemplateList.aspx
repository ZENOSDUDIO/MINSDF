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
                                    Text="�������뵥ģ��" />
                                <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                                    Text="ɾ�����뵥ģ��" ConfirmationMessage="ȷ��ɾ�����뵥��" CommandName="delete" />
                                <SCS:ToolbarButton CausesValidation="False" Text="�������뵥" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif" />
                                <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                                    Text="��ѯ" />
                            </Items>
                        </SCS:Toolbar>
    <asp:GridView ID="GridView1" runat="server">
        <Columns>
            <asp:BoundField DataField="TemplateNo" HeaderText="ģ����" />
            <asp:BoundField DataField="TemplateName" HeaderText="ģ������" />
            <asp:BoundField DataField="IsStatic" HeaderText="��������" />
            <asp:BoundField DataField="CreateBy" HeaderText="������" />
            <asp:BoundField DataField="Description" HeaderText="˵��" />
        </Columns>
    </asp:GridView>
</asp:Content>

