<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StocktakeTypeEdit.aspx.cs" Inherits="SystemManagement_StocktakeTypeEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 800px">
        <legend class="mainTitle">盘点类别</legend>
        <div>
        <table cellspacing="5"  width="800px" border="0">
            <asp:HiddenField ID="hidTypeID" runat="server" />
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
                <asp:TextBox ID="txtDefaultPriority" runat="server"></asp:TextBox>
   <%--             <asp:DropDownList ID="ddlDefaultPriority" runat="server" Width="180px">
                </asp:DropDownList>--%>
                
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
</fieldset>
</asp:Content>