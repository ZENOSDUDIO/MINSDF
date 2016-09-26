<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantEdit.aspx.cs" Inherits="MasterDataMaintain_PlantEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2" >
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="100%">
<ButtonCssClasses 
                CssClass="button" 
                CssClassEnabled="button_enabled" 
                CssClassDisabled="button_disabled" 
                CssClassSelected="button_selected"/> 
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="save" 
                            ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" 
                            ValidationGroup="save" />
                        <SCS:ToolbarButton CausesValidation="False" 
                            ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />
                    </Items>
                </SCS:Toolbar>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="工厂代码"></asp:Label>
                <asp:TextBox ID="txtPlantCode" runat="server" ValidationGroup="save"></asp:TextBox>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                    TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                </cc1:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPlantCode" ErrorMessage="必填" SetFocusOnError="True" 
                    ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="工厂名称"></asp:Label>
                <asp:TextBox ID="txtPlantName" runat="server" ValidationGroup="save"></asp:TextBox>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                    TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError">
                </cc1:ValidatorCalloutExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtPlantName" ErrorMessage="必填" SetFocusOnError="True" 
                    ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
</asp:Content>

