<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PartRepairRecordEdit.aspx.cs" Inherits="BizDataMaintain_PartRepairRecordEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script>
    function bindReturnValue() {
        //string str = PartID+"��"+PartCode + "��" + PartChineseName + "��" + PlantName + "��" + DUNS;
        var v = getReturnValue();
        var s = v.split('��');
        document.getElementById('<%= hidCurrentPartID.ClientID%>').value = unescape(s[0]);
        document.getElementById('<%= labPartCode.ClientID%>').value = unescape(s[1]);
        document.getElementById('<%= labPartName.ClientID%>').value = unescape(s[2]);
        document.getElementById('<%= labPlantName.ClientID%>').value = unescape(s[3]);
        document.getElementById('<%= labDUNS.ClientID%>').value = unescape(s[4]);
    }

    function popupWindow() {
        showDialog('PartSelect.aspx', 900, 500, 'bindReturnValue();');
    }
    function popupWindow1() {
        showDialog('SupplierSelect.aspx', 625, 365, 'bindReturnValue1();');
    }
    function bindReturnValue1() {
        var v = getReturnValue();
        var s = v.split('��');
        document.getElementById('<%= hidSupplierID.ClientID%>').value = unescape(s[0]);
        document.getElementById('<%= hidSupplierDUNS.ClientID%>').value = unescape(s[1]);
        document.getElementById('<%= txtSupplier.ClientID%>').value = unescape(s[1]);
        document.getElementById('<%= txtTelephone.ClientID%>').value = unescape(s[2]);
        document.getElementById('<%= txtFax.ClientID%>').value = unescape(s[3]);
    }
</script>
    
<fieldset class="Edit">
    <legend class="mainTitle">���޵�</legend>
 
        <asp:HiddenField ID="hidRecordID" runat="server" />
        <asp:HiddenField ID="hidCurrentPartID" runat="server" />

         <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" >
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="����" ValidationGroup="save" />
                    <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="����" CommandName="return" />
                </Items>
          </SCS:Toolbar>
  <div class="divContainer" style="height:400px">
        <table>
            <tr>
                <td colspan="4"><input id="butSelectPart" type="button" value="ѡ�������" onclick="popupWindow();return false;" style="width:180px" /></td>
            </tr>
            
            
            <tr>
            <td >��������ţ�</td>
            <td style="width:200px"><%--<asp:Label ID="labPartCode" runat="server" Text=""  Width="200px"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:Label>--%><asp:TextBox ID="labPartCode" runat="server"  onpaste="return false;"
                            onkeypress="return false;" Width="200px"></asp:TextBox></td>
            <td >��&nbsp;&nbsp;&nbsp;&nbsp; ����</td>
            <td style="width:200px"><%--<asp:Label ID="labPlantName" runat="server" Text=""  Width="200px"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:Label>--%><asp:TextBox ID="labPlantName" runat="server"  onpaste="return false;"
                            onkeypress="return false;" Width="200px"></asp:TextBox></td>
            </tr>
        	
            <tr>
            <td >��Ӧ��DUNS��</td>
            <td style="width:200px">
                <%--<asp:Label ID="labDUNS" runat="server" Text=""  Width="200px"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:Label>--%><asp:TextBox ID="labDUNS" runat="server"  onpaste="return false;"
                            onkeypress="return false;" Width="200px"></asp:TextBox></td>
            <td >����������ƣ�</td>
            <td style="width:200px">
                <%--<asp:Label ID="labPartName" runat="server" Text="" Width="200px"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></asp:Label>--%><asp:TextBox ID="labPartName" runat="server"  onpaste="return false;"
                            onkeypress="return false;" Width="200px"></asp:TextBox></td>
            </tr>
        	
        	<tr>
                <td colspan="4">
                <input id="Button1" type="button" value="ѡ���޼ӹ���DUNS" onclick="popupWindow1();return false;" style="width:180px"/>
                </td>
            </tr>
            
            <tr>
            <td >���޼ӹ���DUNS��</td>
            <td colspan="3">
                <asp:HiddenField ID="hidSupplierID" runat="server" />
                <asp:HiddenField ID="hidSupplierDUNS" runat="server" />
                <asp:TextBox ID="txtSupplier" runat="server" ReadOnly="true" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="���޼ӹ���DUNS��ѡ" ControlToValidate="txtSupplier" Display="None" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                        ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator2" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
            </td>
            </tr>
            
            
            <tr>
            <td>��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
            <td colspan="3">
                <asp:TextBox ID="txtTelephone" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
        	
            <tr>
            <td>��&nbsp;&nbsp;&nbsp;&nbsp;�棺</td>
            <td colspan="3">
                <asp:TextBox ID="txtFax" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            
            
            <tr>
            <td>��&nbsp;&nbsp;&nbsp;&nbsp;����</td>		
            <td colspan="3">
                <asp:TextBox id="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
            
        </table>                 
        </div>
</fieldset>
</asp:Content>

