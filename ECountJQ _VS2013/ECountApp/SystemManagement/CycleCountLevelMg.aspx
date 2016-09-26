<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CycleCountLevelMg.aspx.cs" Inherits="SystemManagement_CycleCountLevelMg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<%@ Register src="../Common/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />     
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
     <uc1:ModalDialog ID="ModalDialog1" runat="server" />
     <div>
            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="780px">
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                    <%--<SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
                </Items>
            </SCS:Toolbar>
             <table width="600px" border="0" cellspacing="0" >
                <asp:HiddenField ID="hidLevelID" runat="server" />     
	            <tr>
	                <td style="width:20%">级别名称</td>
	                <td style="width:32%">
		                <asp:TextBox id="txtLevelName" runat="server" Width="180px" ></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="级别名称不能为空" 
                            ControlToValidate="txtLevelName" Display="None" SetFocusOnError="True" 
                            ValidationGroup="save"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	                </td>
	                <td style="width:15%">
		                盘点次数
	                </td>
	                <td >
		                <asp:TextBox id="txttimes" runat="server" Width="180px"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="txttimes" ErrorMessage="盘点次数必须大于1" MaximumValue="100" 
                            MinimumValue="1" Type="Integer" ValidationGroup="save"></asp:RangeValidator>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="盘点次数不能为空" 
                            ControlToValidate="txttimes" Display="None" SetFocusOnError="True"
                            ValidationGroup="save"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttimes" Display="None" ErrorMessage="输入整数" SetFocusOnError="True" ValidationExpression="^\d+$"  ValidationGroup="save"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                         <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RegularExpressionValidator1" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	                </td>
	            </tr>
            	
	            <tr>	
	            <td >
		            允许数量差异率(%)
	            </td>
	            <td >
		            <asp:TextBox id="txtMaxAmountDiffInPercent" runat="server" Width="180px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="允许数量差异率不能为空" 
                        ControlToValidate="txtMaxAmountDiffInPercent" Display="None"
                        SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMaxAmountDiffInPercent" Display="None" ErrorMessage="限4位小数的数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d{1,4})?$" ValidationGroup="save"></asp:RegularExpressionValidator>
                     <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender> <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RegularExpressionValidator2" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                       
	            </td>
	            <td >
		            允许差异金额</td>
	            <td >
		            <asp:TextBox id="txtMaxSumDifference" runat="server" Width="180px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator4" runat="server" ErrorMessage="允许差异金额不能为空" 
                        ControlToValidate="txtMaxSumDifference" Display="None"
                        SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMaxSumDifference" Display="None" ErrorMessage="限4位小数的数字" SetFocusOnError="True" ValidationExpression="^\d+(\.\d{1,4})?$" ValidationGroup="save">*</asp:RegularExpressionValidator>  <%--^\-?([1-9]\d*|0)(\.\d?[0-9])?$--%>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator4" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                       <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegularExpressionValidator3" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
	            </td>
	            </tr>
            	
            </table>
        </div>
    </form>
</body>
</html>
