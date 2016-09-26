<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="UserGroupManagement.aspx.cs" Inherits="SystemManagement_UserGroupManagement" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

     
    </script>

    <fieldset style="width: 800px">
        <legend class="mainTitle">�û���༭</legend>
        <div>
            <table cellspacing="5" width="800px" border="0">
                <asp:HiddenField ID="hidGroupID" runat="server" />
                <tr>
                    <td colspan="4">
                        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
                            EnableClientApi="true" Width="780px">
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                                CssClassSelected="button_selected" />
                            <Items>
                                <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                    Text="����" ValidationGroup="save" />
                                <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                                    Text="����" CommandName="return" />
                            </Items>
                        </SCS:Toolbar>
                    </td>
                </tr>
                <tr>
                    <td width="170px">
                        �û�������
                    </td>
                    <td>
                        <asp:TextBox ID="txtGroupName" runat="server" Width="200px"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="�û������Ʋ���Ϊ��" ControlToValidate="txtGroupName"
                            Display="None" SetFocusOnError="True" ValidationGroup="save"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                                HighlightCssClass="EditError">
                            </cc1:ValidatorCalloutExtender>
                    </td>
                    <td width="160px">
                        <%--                    <td height="25" width="*" align="left">
                        <asp:HiddenField ID="hidSupplierID" runat="server" />
                        <asp:TextBox ID="txtSupplier" runat="server" Width="120px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox>
                        <input id="Button1" onclick="returnWindow('<%= hidSupplierID.ClientID %>','<%= txtSupplier.ClientID %>');"
                            type="button" value="ѡ��DUNS" style="width: 80px" />
                    </td>--%>
                    </td>
<%--                    <td height="25" width="*" align="left">
                        <asp:HiddenField ID="hidSupplierID" runat="server" />
                        <asp:TextBox ID="txtSupplier" runat="server" Width="120px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox>
                        <input id="Button1" onclick="returnWindow('<%= hidSupplierID.ClientID %>','<%= txtSupplier.ClientID %>');"
                            type="button" value="ѡ��DUNS" style="width: 80px" />
                    </td>--%>
                </tr>
                <tr>
                    <td style="display: none">
                        ����/����
                    </td>
                    <td style="display: none">
                        <asp:DropDownList ID="ddlPlantID" runat="server" Width="90px" >
                        </asp:DropDownList><cc1:CascadingDropDown ID="ddlPlantID_CascadingDropDown" runat="server" Category="PlantCode"
                                            Enabled="True" ServiceMethod="GetPlantPageMethod" TargetControlID="ddlPlantID"
                                            LoadingText="����������...">
                                        </cc1:CascadingDropDown>
                        /<asp:DropDownList ID="ddlWorkshopID" runat="server" Width="90px">
                        </asp:DropDownList><cc1:CascadingDropDown ID="ddlWorkshopID_CascadingDropDown" runat="server" Category="WorkshopCode"
                                            Enabled="True" ParentControlID="ddlPlantID" ServiceMethod="GetWorkshopsPageMethod"
                                            TargetControlID="ddlWorkshopID" LoadingText="����������...">
                                        </cc1:CascadingDropDown>
                    </td>
                    <td>
                        �洢��������
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStoreLocationID" runat="server" Width="180px" 
                            Visible="False">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlStoreLocationType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        �鿴����
                    </td>
                    <td>
                        <asp:CheckBox ID="chkShowAllLocation" Text="�Ƿ���Բ鿴���������̵���" runat="server" Checked="False" />
                    </td>
                    <td>
                        ��������
                    </td>
                    <td>
                        <asp:CheckBox ID="chkFillinAllLocation" Text="�Ƿ�����������������̵���" runat="server" Checked="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ϵͳ����
                    </td>
                    <td>
                        <asp:CheckBox ID="chkSysAdmin" Text="�Ƿ�ϵͳ����" runat="server" Checked="False" />
                    </td>
                    <td>
                        �������ȫ��
                    </td>
                    <td>
                        <asp:CheckBox ID="chkAnalyzeAll" Text="�Ƿ���Բ������ȫ��" runat="server" Checked="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ��̬�̵�������
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaxStaticStocktake" runat="server" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMaxStaticStocktake"
                            Display="None" ErrorMessage="����������" SetFocusOnError="True" ValidationExpression="^\d+$"
                            ValidationGroup="save"></asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RegularExpressionValidator1"
                            HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        ��̬�̵�������
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaxDynamicStocktake" runat="server" Width="200px"></asp:TextBox><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMaxDynamicStocktake"
                            Display="None" ErrorMessage="����������" SetFocusOnError="True" ValidationExpression="^\d+$"
                            ValidationGroup="save"></asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RegularExpressionValidator2"
                            HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        ��ǰ��̬�̵���
                    </td>
                    <td>
                        <asp:TextBox ID="txtCurrentStaticStocktake" runat="server" Width="200px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                runat="server" ControlToValidate="txtCurrentStaticStocktake" Display="None" ErrorMessage="����������"
                                SetFocusOnError="True" ValidationExpression="^\d+$" ValidationGroup="save"></asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RegularExpressionValidator3"
                            HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td>
                        ��ǰ��̬�̵���
                    </td>
                    <td>
                        <asp:TextBox ID="txtCurrentDynamicStocktake" runat="server" Width="200px" onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                runat="server" ControlToValidate="txtCurrentDynamicStocktake" Display="None"
                                ErrorMessage="����������" SetFocusOnError="True" ValidationExpression="^\d+$" ValidationGroup="save"></asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RegularExpressionValidator4"
                            HighlightCssClass="EditError">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divContainer" style="table-layout: fixed; min-height: 145px; width: 780px;">
            <asp:GridView ID="GridView1" DataKeyNames="OperationId" AutoGenerateColumns="False"
                runat="server" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="ѡ��">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelected" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelected_CheckedChanged" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="�˵�" DataField="OperationName" />
                    <asp:BoundField HeaderText="��������" DataField="CommandName" Visible="false" />
                    <asp:BoundField HeaderText="��ע" DataField="Description" />
                    <asp:TemplateField HeaderText="��ť">
                        <ItemTemplate>
                            <asp:GridView ID="gvOperation" DataKeyNames="OperationId" AutoGenerateColumns="False"
                                runat="server" OnRowDataBound="GridView1_RowDataBound" ShowHeader="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="ѡ��">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelected" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="��ť" DataField="OperationName" />
                                    <asp:BoundField HeaderText="��������" DataField="CommandName" Visible="false" />
                                    <asp:BoundField HeaderText="��ע" DataField="Description" />
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
</asp:Content>
