<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StoreLocationEdit.aspx.cs" Inherits="SystemManagement_StoreLocationEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 800px">
        <legend class="mainTitle">存储区域编辑</legend>
        <div>
        <table cellspacing="5"  width="800px" border="0">
        <asp:HiddenField ID="hidLocationID" runat="server" />
            <tr>
                <td  colspan="2">
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
	            <td>存储 区域 名称：</td>
	            <td>
		            <asp:TextBox id="txtLocationName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="存储区域名称不能为空" ControlToValidate="txtLocationName"  Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError"></cc1:ValidatorCalloutExtender>
	            </td>
	             <td>工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 厂：</td>
                <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="180px">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="工厂必选" ControlToValidate="ddlPlantID"  Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError"></cc1:ValidatorCalloutExtender>
                </td>
	        </tr>
	        
            <tr>
                <td>对应物流系统存储区域：</td>
                <td>
	                <asp:TextBox id="txtLogisticsSysSLOC" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="对应物流系统存储区域不能为空" ControlToValidate="txtLogisticsSysSLOC"  Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3" HighlightCssClass="EditError"></cc1:ValidatorCalloutExtender>
                </td>
                <td>区域类型：</td>
                <td>
                    <asp:DropDownList ID="ddlTypeID" runat="server" Width="180px">
                    </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="区域类型必选" ControlToValidate="ddlTypeID"  Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4" HighlightCssClass="EditError"></cc1:ValidatorCalloutExtender>               
                </td>
              
            </tr>
            
            <tr>
                <td>Available：</td>
                <td>
	                <asp:CheckBox ID="chkAvailableIncluded" Text="Available" runat="server" Checked="False" />
                </td>
                <td>QI：</td>
                <td>
	                <asp:CheckBox ID="chkQIIncluded" Text="QI" runat="server" Checked="False" />
                </td>
               
            </tr>
            
            <tr>
                 <td>Block：</td>
                <td>
	                <asp:CheckBox ID="chkBlockIncluded" Text="Block" runat="server" Checked="False" />
                </td>
            </tr>

    	

	        
        </table>
        </div>
</fieldset>
</asp:Content>