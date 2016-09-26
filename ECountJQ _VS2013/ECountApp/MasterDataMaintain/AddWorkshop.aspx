<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddWorkshop.aspx.cs" Inherits="MasterDataMaintain_AddWorkshop" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lbPlantCode" runat="server" Text="工厂代码"></asp:Label>
                <asp:DropDownList ID="ddlPlant" runat="server" >
                </asp:DropDownList><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="必选" ControlToValidate="ddlPlant" 
                    Display="Static" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td>
                <asp:Label ID="lbWrokshopCode" runat="server" Text="车间代码"></asp:Label>
                <asp:TextBox ID="tbWorkshopCode" runat="server" ValidationGroup="save"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="必填" ControlToValidate="tbWorkshopCode" 
                    Display="Static" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td>
                <asp:Label ID="lbWrokshopName" runat="server" Text="车间名称"></asp:Label>
                <asp:TextBox ID="tbWorkshopName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="必填" ControlToValidate="tbWorkshopName" 
                    Display="Static" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                        ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3">
                    </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
                <asp:HiddenField ID="hidWorkshopID" runat="server" Value="0" />
            </td>
            <td>
                <asp:Button ID="ButBack" runat="server" Text="返回" onclick="ButBack_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

