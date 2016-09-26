<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartStatusMg.aspx.cs" Inherits="SystemManagement_PartStatusMg" %>

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
           <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="580px">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                       <%-- <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
                    </Items>
          </SCS:Toolbar>
                
         <table cellspacing="5"  width="580px" border="0">
            <asp:HiddenField ID="hidStatusID" runat="server" />    
        
	        <tr>
	        <td style="width:20%;">物料状态名称</td>
	        <td >
		        <asp:TextBox id="txtStatusName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="物料状态名称不能为空" 
                        ControlToValidate="txtStatusName" Display="None" SetFocusOnError="True" 
                        ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	        </td>
	        </tr>
        	
	        <tr>
	        <td>循环盘点标记</td>
	        <td>
		        <asp:CheckBox ID="chkCycleCount" Text="可否循环盘点" runat="server" Width="200px"/>
	        </td>
	        </tr>
	        
            <tr style="display:none">
	        <td>是否启用</td>
	        <td>
		        <asp:CheckBox ID="chkAvailable" Text="是否启用" runat="server" Width="200px"/>
	        </td>
	        </tr>
        	
        </table>
    </div>
    </form>
</body>
</html>
