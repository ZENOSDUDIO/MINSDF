<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserGroupList.aspx.cs" Inherits="SystemManagement_UserGroupList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divBody">
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
            OnButtonClicked="Toolbar1_ButtonClicked" >
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                    Text="���" />
                <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                    Text="ɾ��" ConfirmationMessage="ȷ��ɾ����" CommandName="delete" />
            </Items>
        </SCS:Toolbar>
      <table width="100%" style="table-layout:fixed;overflow:auto">
      <tr>
      <td>
        <div class="divContainer" style="table-layout:fixed;height: 400px;">
            <asp:GridView ID="GridView1" DataKeyNames="GroupID" AutoGenerateColumns="False" runat="server" OnPreRender="GridView1_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="ɾ�����">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSelected" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�û�������">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEdit_Click" CommandName='view'
                                CommandArgument='<%# Eval("GroupID")%>'><%# Eval("GroupName")%></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
     <%--               <asp:TemplateField HeaderText="����">
                        <ItemTemplate>
                            <%# Eval("Workshop.WorshopName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="�洢����">
                        <ItemTemplate>
                            <%# Eval("StoreLocationType.TypeName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
   <%--                 <asp:TemplateField HeaderText="DUNS">
                        <ItemTemplate>
                            <%# Eval("DUNS")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                    <asp:CheckBoxField HeaderText="�鿴���������̵���" HeaderStyle-HorizontalAlign="Left" DataField="ShowAllLocation">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:CheckBoxField>
                    <asp:CheckBoxField HeaderText="�������������̵���" HeaderStyle-HorizontalAlign="Left" DataField="FillinAllLocation">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:CheckBoxField>
                    <asp:CheckBoxField HeaderText="�������ȫ�� " HeaderStyle-HorizontalAlign="Left" DataField="AnalyzeAll">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:CheckBoxField>
                    <asp:TemplateField HeaderText="��̬�̵�������">
                        <ItemTemplate>
                            <%# Eval("MaxStaticStocktake")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��̬�̵�������">
                        <ItemTemplate>
                            <%# Eval("MaxDynamicStocktake")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ǰ��̬�̵���">
                        <ItemTemplate>
                            <%# Eval("CurrentStaticStocktake")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ǰ��̬�̵���">
                        <ItemTemplate>
                            <%# Eval("CurrentDynamicStocktake")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <%# Eval("CreateDate")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </td>
        </tr>
        </table>
    </div>
</asp:Content>
