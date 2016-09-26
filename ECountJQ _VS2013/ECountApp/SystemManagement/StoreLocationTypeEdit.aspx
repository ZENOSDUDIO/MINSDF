<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StoreLocationTypeEdit.aspx.cs" Inherits="SystemManagement_StoreLocationTypeEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 800px">
        <legend class="mainTitle">存储区域类型</legend>
        <div>
            <table cellspacing="5" width="800px" border="0">
                <asp:HiddenField ID="hidTypeID" runat="server" />
                <tr>
                    <td colspan="2">
                        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
                            EnableClientApi="true" Width="780px">
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                                CssClassSelected="button_selected" />
                            <Items>
                                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                    Text="保存" ValidationGroup="save" />
                                <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                                    Text="返回" CommandName="return" />
                            </Items>
                        </SCS:Toolbar>
                    </td>
                </tr>
                <tr>
                    <td>
                        区域类型名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtTypeName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="区域类型名称不能为空" ControlToValidate="txtTypeName"
                            Display="None" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                                HighlightCssClass="EditError">
                            </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
