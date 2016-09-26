<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RequestImport.aspx.cs" Inherits="PhysicalCount_RequestImport" %>

<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="����" CommandName="return" />
        </Items>
    </SCS:Toolbar>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/RequestImport.xls"
                    Target="_blank">ģ������</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblIsStatic" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">��̬</asp:ListItem>
                    <asp:ListItem>��̬</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" valign="middle">
                <%--<asp:Button ID="btnUpload" Style="display: none" runat="server" Text="�ϴ�" OnClick="btnUpload_Click" />--%>
                &nbsp;
                <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="height: 230px">
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" EnableViewState="False"
                        OnPreRender="gvResult_PreRender" Width="98%">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="���" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PartCode" HeaderText="�����" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PartPlantCode" HeaderText="����" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DUNS" HeaderText="��Ӧ��DUNS" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TypeName" HeaderText="�������" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PriorityName" HeaderText="�����̶�" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="��ע" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
