<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StoreLocationImport.aspx.cs" Inherits="SystemManagement_StoreLocationImport" %>
<%@ Register src="../Common/UCFileUpload.ascx" tagname="UCFileUpload" tagprefix="uc1" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
    
<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<scs:toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                Text="返回" CommandName="return" />
        </Items>
    </scs:toolbar>
 
<table style="width: 100%">
        <tr>
        <td>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/StorelocationImport.xls"
              Target="_blank">导入模板下载
            </asp:HyperLink>
   
            </td>
            </tr>
        <tr>
            <td>
        <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            <div  class="divContainer" style="height:300px">
                <asp:GridView ID="gvStorelocation" runat="server" AutoGenerateColumns="False" 
                    onprerender="gvStorelocation_PreRender"  >
                    <Columns>
                        <asp:BoundField DataField="No" HeaderText="序号" >                 
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LocationName" HeaderText=" 存储区域名称" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TypeID" HeaderText="区域类型" >
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
                        <asp:BoundField DataField="LogisticsSysSLOC" HeaderText="物流系统存储区域" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

