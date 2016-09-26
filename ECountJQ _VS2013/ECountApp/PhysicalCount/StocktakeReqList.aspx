<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StocktakeReqList.aspx.cs" Inherits="PhysicalCount_StocktakeReqList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    showDialog('StocktakeRequest.aspx?Mode=Edit', 1080, 550, null, "refresh('<%= Toolbar1.Controls[3].ClientID%>')");
                    eventArgs.set_cancel(true);
                    break;
                default:
                    break;
            }
        }

    </script>

    <%--
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
<div class="divContainer" style="width:888px">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar"  EnableClientApi="true"
        OnButtonClicked="Toolbar1_ButtonClicked" OnClientButtonClick="toolbar_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="�������뵥" />
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="ɾ�����뵥" ConfirmationMessage="ȷ��ɾ�����뵥��" CommandName="delete" />
            <SCS:ToolbarButton CausesValidation="False" Text="�������뵥" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif" />
            <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                Text="��ѯ" />
        </Items>
    </SCS:Toolbar>
  </div>
<div class="divContainer" style="width:888px">
                    <table>
                        <tr>
                            <td nowrap="nowrap">
                                ���뵥��
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequestNo" Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                ������
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequestBy" runat="server" Width="96px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                ����
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="True" Width="96px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                �̵�״̬
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlStatus" runat="server"  Width="96px" AppendDataBoundItems="True">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                �����
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCode" Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                ����ʱ�� ��
                            </td>
                            <td nowrap="nowrap" valign="middle">
                                <asp:TextBox ID="txtTimeStart" runat="server" Width="96px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTimeStart_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtTimeStart" PopupButtonID="btnCalendarStart" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="btnCalendarStart" runat="server" CausesValidation="False" EnableViewState="False"
                                    ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                            </td>
                            <td nowrap="nowrap">
                                ��
                            </td>
                            <td>
                                <asp:TextBox ID="txtTimeEnd" runat="server" Width="96px"></asp:TextBox><asp:ImageButton
                                    ID="btnCalendarEnd" runat="server" BorderStyle="None" CausesValidation="False"
                                    EnableViewState="False" ImageAlign="Middle" ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                <cc1:CalendarExtender ID="txtTimeEnd_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtTimeEnd" PopupButtonID="btnCalendarEnd" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                </cc1:CalendarExtender>
                            </td>
                            <td colspan="2" nowrap="nowrap">
                                <asp:CheckBoxList ID="cblSearchOption" Width="100%" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">���뵥</asp:ListItem>
                                    <asp:ListItem>���뵥��ϸ</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                ���������
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCName"   Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="left">
                                �̵�����
                            </td>
                            <td nowrap="nowrap" valign="middle">
                                <asp:DropDownList ID="ddlType"  runat="server" AppendDataBoundItems="True" Width="96px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                ��������
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIsStatic"  Width="96px" runat="server" >
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="true" Text="��̬">��̬</asp:ListItem>
                                    <asp:ListItem Value="false" Text="��̬">��̬</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td align="left" nowrap="nowrap">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
</div>

    <cc1:TabContainer ID="tabContainerRequest" runat="server" ActiveTabIndex="0" Font-Size="Large" Width = "906px">
                    <cc1:TabPanel runat="server" HeaderText="���뵥" ID="TabPanel1">
                        <HeaderTemplate>
                            ���뵥
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="divContainer" style="height: 220px;width:880px;">
                                <asp:GridView ID="gvRequest" runat="server" AutoGenerateColumns="False" DataKeyNames="RequestID"
                                    OnPreRender="gvRequest_PreRender" OnRowDataBound="gvRequest_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="���뵥��">
                                            <HeaderTemplate>
                                                ���뵥��</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��������">
                                            <HeaderTemplate>
                                                ��������</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"��̬":"��̬" %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������������">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsergroup" runat="server" Text='<%# Eval("RequestBy.UserGroup.GroupName") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����ʱ��">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DateRequest") %>'></asp:TextBox></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateRequest", "{0:G}") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ѡ��">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbSelectAll" OnCheckedChanged="cbSelect_CheckedChanged" runat="server"
                                                    AutoPostBack="True" /></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelect" runat="server" /></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Wrap="False" />
                                </asp:GridView>
                            </div>
                            <div style="width:896px;">
                            <uc2:AspPager ID="pagerRequest" runat="server" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="���뵥��ϸ">
                        <ContentTemplate>
                            <div class="divContainer" style="height: 220px;width:880px;">
                                <asp:GridView ID="gvDetails" runat="server" OnPreRender="gvDetails_PreRender" AutoGenerateColumns="False"
                                    DataKeyNames="RequestID" OnRowDataBound="gvDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="���뵥��">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��������">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Convert.ToBoolean(Eval("RequestIsStatic"))?"��̬":"��̬" %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="�����" DataField="PartCode" />
                                        <%--<asp:TemplateField HeaderText="�����">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="" Text='<%# Eval("PartCode") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%><asp:BoundField HeaderText="���������" DataField="PartChineseName" />
                                        <asp:BoundField HeaderText="����" DataField="PartPlantCode" />
                                        <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                                        <asp:BoundField HeaderText="������" DataField="RequestUser" />
                                        <%--<asp:TemplateField HeaderText="������������">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%><asp:BoundField HeaderText="����ʱ��" DataField="DateRequest"
                                                    DataFormatString="{0:G}" />
                                        <asp:BoundField HeaderText="�̵����" DataField="TypeName" />
                                        <asp:BoundField HeaderText="�����̶�" DataField="PriorityName" />
                                        <asp:TemplateField HeaderText="�̵�״̬">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" Text='<%# (Eval("StocktakeStatusName")==null)?"������":Eval("StocktakeStatusName") %>'
                                                    runat="server"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="����״̬" DataField="StatusName" />
                                        <asp:BoundField HeaderText="�̵�ԭ�򼰱�ע" DataField="DetailsDesc" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style ="width:896px;">
                            <uc2:AspPager ID="AspPager1" runat="server" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
