<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CycleCountLevelEdit.aspx.cs" Inherits="SystemManagement_CycleCountLevelEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<fieldset class="Edit">
        <legend class="mainTitle">�̵㼶��</legend>
        <div>
             <table width="100%" border="0" cellspacing="5" >
                <asp:HiddenField ID="hidLevelID" runat="server" />
                <tr>
	                <td  colspan="4">
                    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="780px">
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="����" ValidationGroup="save" />
                            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="����" CommandName="return" />
                        </Items>
                    </SCS:Toolbar>
                    </td>
                </tr>      
                        
                        
	            <tr>
	            <td class="width_80">�������ƣ�</td>
	            <td class="width_220">
		            <asp:TextBox id="txtLevelName" runat="server" ></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="�������Ʋ���Ϊ��" 
                        ControlToValidate="txtLevelName" Display="None" SetFocusOnError="True" 
                        ValidationGroup="save"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
	            </td>
	            <td class="width_80">
		            �̵������
	            </td>
	            <td >
		            <asp:TextBox id="txttimes" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="txttimes" EnableClientScript="False" ErrorMessage="�̵��������0" 
                        MaximumValue="5" MinimumValue="1" Type="Integer" ValidationGroup="save"></asp:RangeValidator>
                    <cc1:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="RangeValidator1">
                    </cc1:ValidatorCalloutExtender>
	            </td>
	            </tr>
            	
	            <tr>	
	            <td nowrap="nowrap">
		            �������������ʣ�������
	            </td>
	            <td >
		            <asp:TextBox id="txtMaxAmountDiffInPercent" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="�������������ʲ���Ϊ��" 
                        ControlToValidate="txtMaxAmountDiffInPercent" Display="None"
                        SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMaxAmountDiffInPercent" Display="None" ErrorMessage="��4λС��������" SetFocusOnError="True" ValidationExpression="^\d+(\.\d{1,4})?$" ValidationGroup="save"></asp:RegularExpressionValidator>
                     <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender> <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RegularExpressionValidator2" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                       
	            </td>
	            <td >
		            ��������</td>
	            <td >
		            <asp:TextBox id="txtMaxSumDifference" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="����������Ϊ��" 
                        ControlToValidate="txtMaxSumDifference" Display="None"
                        SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMaxSumDifference" Display="None" ErrorMessage="��4λС��������" SetFocusOnError="True" ValidationExpression="^\d+(\.\d{1,4})?$" ValidationGroup="save">*</asp:RegularExpressionValidator>  <%--^\-?([1-9]\d*|0)(\.\d?[0-9])?$--%>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator4" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                       <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegularExpressionValidator3" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
	            </td>
	            </tr>
            	
            </table>
        </div>
    </fieldset>
    
 
</asp:Content>

