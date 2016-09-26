<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsignmentPartRecordImport.aspx.cs" Inherits="BizDataMaintain_ConsignmentPartRecordImport" %>

<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<scs:toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true" >
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />
        </Items>
    </scs:toolbar>
    <div class="divContainer">
       <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/PartConsignmentRecordImport.xls"
                        Target="_self">导入模板下载</asp:HyperLink>
    </div>                    
    <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
    <div class="divContainer" style="height: 260px">
        <asp:GridView ID="gvConsignmentPartRecord" runat="server" OnPreRender="gvConsignmentPartRecord_PreRender">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <%# Eval("RowNumber")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工厂">
                    <ItemTemplate>
                        <%# Eval("PlantCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协零件号">
                    <ItemTemplate>
                        <%# Eval("PartCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商DUNS">
                    <ItemTemplate>
                        <%# Eval("DUNS")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协供应商DUNS">
                    <ItemTemplate>
                        <%# Eval("CDUNS")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协供应商名称">
                    <ItemTemplate>
                        <%# Eval("SupplierName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电话">
                    <ItemTemplate>
                        <%# Eval("Telephone")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="传真">
                    <ItemTemplate>
                        <%# Eval("Fax")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

