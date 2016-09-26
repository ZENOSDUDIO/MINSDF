<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PartGroupEdit.aspx.cs" Inherits="BizDataMaintain_PartGroupEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="SCS.Web.UI.WebControls.Toolbar" namespace="SCS.Web.UI.WebControls" tagprefix="SCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function bindReturnValue() {
        //string str = PartID+"��"+PartID
        var v = getReturnValue();
        document.getElementById('<%= hidPartIDs.ClientID%>').value = unescape(v);
        var objbtn = document.getElementById('<%= btnTemp.ClientID%>');
        objbtn.click();
    }

    function popupWindow() {
        showDialog('PartsSelect.aspx', 900, 500, 'bindReturnValue();');
    }
</script>

<fieldset class="Edit">
    <asp:HiddenField ID="hidGroupID" runat="server" Value="0" />
    <legend class="mainTitle">�������</legend>

             <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" onbuttonclicked="Toolbar1_ButtonClicked"  EnableClientApi="true" >
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"  CssClassSelected="button_selected"/> 
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Text="����" ValidationGroup="save" />
                        <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif" Text="����" CommandName="return" />
                    </Items>
             </SCS:Toolbar>

       <div class="divContainer" >
            <table cellspacing="5" >
                <input id="hidPartIDs" type="hidden" runat="server"/>
                <tr>
                    <td style="width:80px">�������ƣ�</td>
                    <td><asp:TextBox ID="txtGroupName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="�������Ʋ���Ϊ��" ControlToValidate="txtGroupName" Display="None" ValidationGroup="save">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1" HighlightCssClass="EditError">
                    </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>                                    
                    <td style="width:80px">��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
                    <td><asp:TextBox ID="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                
               <tr>
                    <td colspan="2"><input id="butSelectParts" type="button" value="ѡ�����" onclick="popupWindow();return false;" style="width:180px" /> <asp:Button ID="btnTemp" runat="server" Width="0px" style="display:none" onclick="btnTemp_Click" /> </td>
               </tr>
            </table>
       </div>     
            <div class="divContainer" style="height:280px">
                <asp:GridView ID="GridViewPartResult" AutoGenerateColumns="false" Width="100%" runat="server" OnPreRender="GridViewPartResult_PreRender" >
                         <Columns>
                         <asp:TemplateField HeaderText="����" >
                                <itemtemplate>
                                     <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkRemoveViewPart_Click" CommandArgument='<%# Eval("PartID")%>'>�Ƴ�</asp:LinkButton> 
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="�����"  SortExpression="PartCode">
                                <itemtemplate>
                                    <%# Eval("PartCode")%>
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����" Visible="False">
                                <itemtemplate>
                                     <%# Eval("PartID")%>                        
                                </itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="�����������" SortExpression="PartChineseName">
                                <itemtemplate>
                                    <%# Eval("PartChineseName")%></itemtemplate>          
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��λ" >
                                <itemtemplate>
                                     <%# Eval("WorkLocation")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ѭ���̵㼶��" >
                                <itemtemplate>
                                 <%# Eval("LevelName")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����">
                                <itemtemplate>
                                 <%# Eval("Specs")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Followup" >
                                <itemtemplate>
                                 <%# Eval("Followup")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="����">
                                <itemtemplate>
                                 <%# Eval("PlantCode")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="�������">
                                <itemtemplate>
                                 <%# Eval("CategoryName")%></itemtemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>                
                </asp:GridView>
            </div>  

                       
</fieldset>  

</asp:Content>

