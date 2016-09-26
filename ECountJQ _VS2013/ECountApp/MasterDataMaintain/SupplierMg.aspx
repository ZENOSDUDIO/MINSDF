<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierMg.aspx.cs" Inherits="MasterDataMaintain_SupplierMg" %>
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
          <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="790px">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                <%--<SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />--%>
            </Items>
         </SCS:Toolbar>
         
        <table width="100%" cellspacing="3" cellpadding="0">
        <asp:HiddenField ID="hidSupplierID" runat="server" />
            <tr>
            <td class="width_80">供应商名称</td>
            <td class="width_220">
                <asp:TextBox ID="txtSupplierName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtSupplierName" Display="None" 
                    ErrorMessage="供应商名称必填" SetFocusOnError="True" 
                    ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                        ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
            </td>

            <td class="width_80">DUNS</td>
            <td>
                <asp:TextBox ID="txtDUNS" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtDUNS" Display="None" 
                    ErrorMessage="DUNS必填" SetFocusOnError="True" 
                    ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                        ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
            </td>
            </tr>
        	
            <tr>
            <td class="width_80">电话1</td>
            <td>
                <asp:TextBox ID="txtPhoneNumber1" runat="server" Width="200px"></asp:TextBox>
            </td>
             <td class="width_80">电话2</td>
            <td>
                <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="200px"></asp:TextBox>
            </td>
            </tr>                                        	
        	
            <tr>
            <td class="width_80">传真</td>
            <td>
                <asp:TextBox ID="txtFax" runat="server" Width="200px"></asp:TextBox></td>
            <td>
                对应存储区域</td>
            <td>
                <asp:DropDownList ID="ddlStoreLocation" runat="server" 
                    DataTextField="LogisticsSysSLOC" DataValueField="LocationID" Width="194px">
                </asp:DropDownList>
                </td>
            </tr>
             
             <tr>
            <td class="width_80">描述</td>		
            <td colspan="3">
                <asp:TextBox id="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>                                        	
       </table>
    </div>
    </form>
</body>
</html>
