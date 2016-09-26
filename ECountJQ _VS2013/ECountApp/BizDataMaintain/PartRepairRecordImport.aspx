<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PartRepairRecordImport.aspx.cs" Inherits="BizDataMaintain_PartRepairRecordImport" %>

<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true" >
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/PartRepairRecordImport.xls"
                        Target="_self">模板下载</asp:HyperLink>
    </div>                    
    <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
    <div class="divContainer" style="height: 260px">
        <asp:GridView ID="gvPartRepairRecord" runat="server" OnPreRender="gvPartRepairRecord_PreRender">
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
                <asp:TemplateField HeaderText="返修零件号">
                    <ItemTemplate>
                        <%# Eval("PartCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商DUNS">
                    <ItemTemplate>
                        <%# Eval("DUNS")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返修供应商DUNS">
                    <ItemTemplate>
                        <%# Eval("RepairDUNS")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返修供应商名称">
                    <ItemTemplate>
                        <%# Eval("RepairSupplierName")%>
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
