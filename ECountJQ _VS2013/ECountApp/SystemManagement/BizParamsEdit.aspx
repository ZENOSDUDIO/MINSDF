<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"  CodeFile="BizParamsEdit.aspx.cs" Inherits="SystemManagement_BizParamsEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="Edit">
        <div>
            <asp:HiddenField ID="hidLevelID" runat="server" />
            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" >
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                    <SCS:ToolbarButton CausesValidation="False" CommandName="clear" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="年终循环盘点清零" ConfirmationMessage="确定清零？" />
                </Items>
            </SCS:Toolbar>
        </div>
        <div class="divContainer" style="height: 530px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="false"
                 OnRowDataBound="GridView1_OnRowDataBound">
                <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label id="bpGroup" class="mainTitle" runat="server" Text='<%# Container.DataItem %>' />
                        <asp:GridView ID="gvBizParams" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ParamID"  OnPreRender="gvBizParams_PreRender" OnRowDataBound="BizParam_RowDataBound" >
                            <Columns>
                                <asp:BoundField HeaderText="参数描述" DataField="ParamDesc" ItemStyle-Width="280px" />
                                <asp:TemplateField HeaderText="参数值">
                                    <ItemTemplate>
                                        <asp:TextBox ID="paramValue" MaxLength="10" runat="server" Text='<%# Bind("ParamValue") %>'></asp:TextBox>
                                        <asp:CheckBox ID="checkStatus" Enabled="true" runat="server" />
                                        <asp:DropDownList ID="ddlList" runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblError" runat="server" Text="" style="color: Red;"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="参数键" DataField="ParamKey" Visible="false" />
                                <asp:BoundField HeaderText="参数组" DataField="GroupName" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
</asp:Content>


