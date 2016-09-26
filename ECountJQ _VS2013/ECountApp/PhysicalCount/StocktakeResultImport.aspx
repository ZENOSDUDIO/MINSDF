<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="StocktakeResultImport.aspx.cs" Inherits="PhysicalCount_StocktakeResultImport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true" >
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/ImportTemplate/ResultImport.xls" runat="server">导入模板下载</asp:HyperLink>
         </div>       
                    <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
       <div class="divContainer" style="height:273px"><asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" OnPreRender="gvItems_PreRender">
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="序号" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PartNo" HeaderText="零件号" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Plant" HeaderText="工厂" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DUNS" HeaderText="DUNS" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                     <asp:BoundField DataField="SLOC" HeaderText="存储区域" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Store" HeaderText="库位" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Line" HeaderText="线旁" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Machining" HeaderText="加工区" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Available" HeaderText="Available" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QI" HeaderText="QI" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Block" HeaderText="Block" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StartCSN" HeaderText="起始CSN" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndCSN" HeaderText="结束CSN" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
</div>                         
</asp:Content>
