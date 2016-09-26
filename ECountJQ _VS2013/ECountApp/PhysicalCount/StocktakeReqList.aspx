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
                Text="创建申请单" />
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除申请单" ConfirmationMessage="确认删除申请单？" CommandName="delete" />
            <SCS:ToolbarButton CausesValidation="False" Text="导入申请单" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif" />
            <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                Text="查询" />
        </Items>
    </SCS:Toolbar>
  </div>
<div class="divContainer" style="width:888px">
                    <table>
                        <tr>
                            <td nowrap="nowrap">
                                申请单号
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequestNo" Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                申请人
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequestBy" runat="server" Width="96px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                工厂
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="True" Width="96px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                盘点状态
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlStatus" runat="server"  Width="96px" AppendDataBoundItems="True">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                零件号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCode" Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                申请时间 从
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
                                到
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
                                    <asp:ListItem Selected="True">申请单</asp:ListItem>
                                    <asp:ListItem>申请单明细</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                零件中文名
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCName"   Width="96px" runat="server"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap" align="left">
                                盘点类型
                            </td>
                            <td nowrap="nowrap" valign="middle">
                                <asp:DropDownList ID="ddlType"  runat="server" AppendDataBoundItems="True" Width="96px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                动静类型
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlIsStatic"  Width="96px" runat="server" >
                                    <asp:ListItem Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="true" Text="静态">静态</asp:ListItem>
                                    <asp:ListItem Value="false" Text="动态">动态</asp:ListItem>
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
                    <cc1:TabPanel runat="server" HeaderText="申请单" ID="TabPanel1">
                        <HeaderTemplate>
                            申请单
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="divContainer" style="height: 220px;width:880px;">
                                <asp:GridView ID="gvRequest" runat="server" AutoGenerateColumns="False" DataKeyNames="RequestID"
                                    OnPreRender="gvRequest_PreRender" OnRowDataBound="gvRequest_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="申请单号">
                                            <HeaderTemplate>
                                                申请单号</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="动静类型">
                                            <HeaderTemplate>
                                                动静类型</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"静态":"动态" %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请人所属组">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsergroup" runat="server" Text='<%# Eval("RequestBy.UserGroup.GroupName") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请时间">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DateRequest") %>'></asp:TextBox></EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateRequest", "{0:G}") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="选择">
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
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="申请单明细">
                        <ContentTemplate>
                            <div class="divContainer" style="height: 220px;width:880px;">
                                <asp:GridView ID="gvDetails" runat="server" OnPreRender="gvDetails_PreRender" AutoGenerateColumns="False"
                                    DataKeyNames="RequestID" OnRowDataBound="gvDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="申请单号">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="动静类型">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Convert.ToBoolean(Eval("RequestIsStatic"))?"静态":"动态" %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="零件号" DataField="PartCode" />
                                        <%--<asp:TemplateField HeaderText="零件号">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="" Text='<%# Eval("PartCode") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%><asp:BoundField HeaderText="零件中文名" DataField="PartChineseName" />
                                        <asp:BoundField HeaderText="工厂" DataField="PartPlantCode" />
                                        <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                                        <asp:BoundField HeaderText="申请人" DataField="RequestUser" />
                                        <%--<asp:TemplateField HeaderText="申请人所属组">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%><asp:BoundField HeaderText="申请时间" DataField="DateRequest"
                                                    DataFormatString="{0:G}" />
                                        <asp:BoundField HeaderText="盘点类别" DataField="TypeName" />
                                        <asp:BoundField HeaderText="紧急程度" DataField="PriorityName" />
                                        <asp:TemplateField HeaderText="盘点状态">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" Text='<%# (Eval("StocktakeStatusName")==null)?"新申请":Eval("StocktakeStatusName") %>'
                                                    runat="server"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="物料状态" DataField="StatusName" />
                                        <asp:BoundField HeaderText="盘点原因及备注" DataField="DetailsDesc" />
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
