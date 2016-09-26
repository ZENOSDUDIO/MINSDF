<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PartCategoryEdit.aspx.cs" Inherits="SystemManagement_PartCategoryEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<fieldset class="Edit">
        <legend class="mainTitle">物料类别</legend>
        <div>
            <table cellspacing="5"  width="800px" border="0px">
                <asp:HiddenField ID="hidCategoryID" runat="server" />
                <tr>
	                <td  colspan="4">
                    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" Width="780px">
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                        <Items>
                            <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="保存" ValidationGroup="save" />
                            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="返回" CommandName="return" />
                        </Items>
                    </SCS:Toolbar>
                    </td>
                </tr>      
                
	            <tr>
	            <td>物料类别名称：</td>
	            <td>
		            <asp:TextBox id="txtCategoryName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="物料类别名称不能为空" 
                        ControlToValidate="txtCategoryName" Display="None" SetFocusOnError="True" 
                        ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
	            </td>
                </tr>
                
                
                <tr>
	            <td>物料类别描述：</td>
	            <td>
		            <asp:TextBox id="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
	            </td>
	            </tr>
	         
            </table>
         </div>
     </fieldset>

</asp:Content>

