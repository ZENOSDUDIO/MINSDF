<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StocktakeTypeMg.aspx.cs" Inherits="SystemManagement_StocktakeTypeMg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<%@ Register src="../Common/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
          <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="550px">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                        <%--<SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
                    </Items>
                </SCS:Toolbar>
         <table cellspacing="5"  width="550px" border="0">
            <asp:HiddenField ID="hidTypeID" runat="server" />
     
        
	        <tr>
	        <td>盘点类别名称：</td>
	        <td >
		        <asp:TextBox id="txtTypeName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="盘点类别名称不能为空" 
                        ControlToValidate="txtTypeName" Display="None" SetFocusOnError="True" 
                        ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	        </td>
	        </tr>
        	
	        <tr>
	        <td>物流系统代码：</td>
	        <td>
	            <asp:TextBox id="txtLogisticCode" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ErrorMessage="物流系统代码不能为空" 
                        ControlToValidate="txtLogisticCode" Display="None" SetFocusOnError="True" 
                        ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	        </td>
	        </tr>
	        
	        <tr>
	        <td>缺省优先级：</td>
	        <td>
<%--                <asp:DropDownList ID="ddlDefaultPriority" runat="server" Width="180px">
                </asp:DropDownList>--%>
                <asp:TextBox ID="txtDefaultPriority" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="缺省优先级必填" ControlToValidate="txtDefaultPriority"  Display="None" 
                    SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3" HighlightCssClass="EditError"></cc1:ValidatorCalloutExtender> 
	        </td>
	        </tr>
	        
	        <tr>
	        <td>手工创建盘点申请：</td>
	        <td>
		        <asp:CheckBox ID="chkManualEnabled" Text="是否可以手工创建盘点申请" runat="server" Width="200px"/>
	        </td>
	        </tr>
	        
	        <tr>
	        <td>计入循环盘点计数：</td>
	        <td>
		        <asp:CheckBox ID="chkActAsCycleCount" Text="是否计入循环盘点计数" runat="server" Width="200px"/>
	        </td>
	        </tr>
	        <tr>
	        <td>描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 述：</td>
	        <td>
		        <asp:TextBox id="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
	        </td>
	        </tr>
	        
        </table>
    </div>
    </form>
</body>
</html>
