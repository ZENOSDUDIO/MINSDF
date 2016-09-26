<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlantDetails.ascx.cs"
    Inherits="MasterDataMaintain_UserControl_PlantDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
    EnableClientApi="true" Width="97%">
    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
        CssClassSelected="button_selected" />
    <Items>
        <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
            Text="保存" ValidationGroup="save" />
    </Items>
</SCS:Toolbar>
<table style="width: 100%">
    <tr>
        <td>
            工厂代码&nbsp;<asp:TextBox ID="txtPlantCode" runat="server" ValidationGroup="save"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValiPlantCode" runat="server" ControlToValidate="txtPlantCode"
                ErrorMessage="工厂代码不可为空" SetFocusOnError="True" ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="reqValiPlantCode"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            工厂名称&nbsp;<asp:TextBox ID="txtPlantName" runat="server" ValidationGroup="save"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqValiPlantName" runat="server" ControlToValidate="txtPlantName"
                ErrorMessage="工厂名称不可为空" SetFocusOnError="True" ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="reqValiPlantName_ValidatorCalloutExtender" runat="server"
                Enabled="True" HighlightCssClass="EditError" TargetControlID="reqValiPlantName">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
</table>
