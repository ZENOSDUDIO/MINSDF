<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StocktakeNoticeList.aspx.cs" Inherits="PhysicalCount_StocktakeNoticeList"
    Theme="" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    showDialog('StocktakeNotice.aspx?mode=Edit', 950, 550, null, "refresh('<%= Toolbar1.Controls[3].ClientID%>')");
                    eventArgs.set_cancel(true);
                    break;
                //                case 2: 
                ////                    showDialog('NoticeExport.aspx', 600, 500); 
                //                    eventArgs.set_cancel(true); 
                //                    break; 
                case 3:
                    //                    showWaitingModal();
                    break;
                default:
                    break;
            }
        }
        var gvNotificationID = '<%= gvNotification.ClientID%>';
        var gvDetailsID = '<%= gvDetails.ClientID%>';
    </script>
    <div style="width:906px;">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar"   EnableClientApi="true"
                    OnButtonClicked="Toolbar1_ButtonClicked" 
                    OnClientButtonClick="toolbar_ButtonClicked" EnableTheming="True">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                        CssClassSelected="button_selected" />
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                            Text="创建通知单" />
                        <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                            Text="删除通知单" ConfirmationMessage="确认删除通知单？" CommandName="delete" />
     
                        <SCS:ToolbarButton CausesValidation="True" CommandName="export" Text="导出通知单" 
                            ImageUrl="~/App_Themes/Images/Toolbar/import.gif" />
     
                        <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                            Text="查询" />
                    </Items>
                </SCS:Toolbar>
     </div>
     <asp:MultiView ID="mvQuery" runat="server" ActiveViewIndex="0" >
                    <asp:View ID="viewNotiQuery" runat="server">
                        <div class="divContainer"   style="width:888px;">
                            <table width="100%">
                                <tr>
                                    <td nowrap="nowrap">
                                        通知单号</td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="txtNotificationNO" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        &nbsp;申请单号</td>
                                    <td>
                                        <asp:TextBox ID="txtRequestNO" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件中文名
                                    </td>
                                    <td><asp:TextBox ID="txtPartCName" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件号</td>
                                    <td nowrap="nowrap">
                                        
                                        <asp:TextBox ID="txtPartNO" runat="server" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap">申请人所属组</td>
                                    <td>
                                        <asp:TextBox ID="txtUserGroup" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;创建时间 从</td>
                                    <td style="white-space:nowrap">
                                        <asp:TextBox ID="txtDateStart" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateStart_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarStart" 
                                            TargetControlID="txtDateStart" Format="yyyy-MM-dd HH:mm:ss" CssClass="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarStart" runat="server" CausesValidation="False" 
                                            EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap">
                                        &nbsp;到</td>
                                    <td style="white-space:nowrap">
                                        <asp:TextBox ID="txtDateEnd" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateEnd_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarEnd" TargetControlID="txtDateEnd" 
                                            Format="yyyy-MM-dd HH:mm:ss"  CssClass="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarEnd" runat="server" CausesValidation="False" 
                                            EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap">
                                        盘点状态</td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" 
                                            DataTextField="StatusName" DataValueField="StatusID" Width="84px">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        申请人</td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="txtCreateBy" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        &nbsp;计划盘点时间 从</td>
                                    <td style="white-space:nowrap">
                                        <asp:TextBox ID="txtPlanDateStart" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPlanDateStart_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarPlanStart" 
                                            TargetControlID="txtPlanDateStart" Format="yyyy-MM-dd HH:mm:ss" CssClass="Calendar">
                                        </cc1:CalendarExtender><asp:ImageButton ID="btnCalendarPlanStart" runat="server" 
                                            CausesValidation="False" EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap">
                                        &nbsp;到
                                    </td>
                                    <td style="white-space:nowrap">
                                        <asp:TextBox ID="txtPlanDateEnd" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPlanDateEnd_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarPlanEnd" 
                                            TargetControlID="txtPlanDateEnd" Format="yyyy-MM-dd HH:mm:ss" CssClass="Calendar">
                                        </cc1:CalendarExtender><asp:ImageButton ID="btnCalendarPlanEnd" runat="server" 
                                            CausesValidation="False" EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap" colspan="2"> 
  <asp:CheckBoxList ID="cblSearchOption" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">通知单</asp:ListItem>
                                            <asp:ListItem>明细</asp:ListItem>
                                        </asp:CheckBoxList>
                                     </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="viewResultQuery" runat="server">
                        <div class="divContainer"  style="width:888px;" >
                            <table width="100%">
                                <tr>
                                    <td nowrap="nowrap">
                                        通知单号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotiNo_Result" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPartNo_Result" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        创建时间 从
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="txtDateStart_Result" runat="server" Width="106px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="btnCalendarStart_Result"
                                            TargetControlID="txtDateStart_Result" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarStart_Result" runat="server" CausesValidation="False"
                                            EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                    <td>
                                        到
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd_Result" runat="server" Width="106px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="btnCalendarEnd_Result"
                                            TargetControlID="txtDateEnd_Result" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarEnd_Result" runat="server" CausesValidation="False"
                                            EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        申请单号
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReqNo_Result" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        零件中文名
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCName_Result" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        计划盘点时间 从
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="txtPlanDateStart_Result" runat="server" Width="106px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="btnCalendarPlanStart_Result"
                                            TargetControlID="txtPlanDateStart_Result" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarPlanStart_Result" runat="server" CausesValidation="False"
                                            EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                    <td>
                                        到
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanDateEnd_Result" runat="server" Width="106px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" PopupButtonID="btnCalendarPlanEnd_Result"
                                            TargetControlID="txtPlanDateEnd_Result" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarPlanEnd_Result" runat="server" CausesValidation="False"
                                            EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">
                                        申请人
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRequestBy_Result" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap">
                                        盘点状态
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus_Result" runat="server" Width="106px" DataTextField="StatusName"
                                            DataValueField="StatusID">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="1" nowrap="nowrap">
                                        盘点区域
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="ddlStoreLocation" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBoxList ID="cbOption_Result" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">通知单</asp:ListItem>
                                            <asp:ListItem>通知单明细</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>

     <cc1:TabContainer ID="tcNotification" runat="server" ActiveTabIndex="0" 
                    Font-Size="Small"  Width = "906px">
                    <cc1:TabPanel runat="server" HeaderText="通知单列表" ID="tabList">
                        <ContentTemplate>
                            <div class="divContainer" style="height: 255px; width:888px;">
                                <asp:GridView ID="gvNotification" runat="server" OnPreRender="gvNotification_PreRender"
                                    AutoGenerateColumns="False" OnRowDataBound="gvNotification_RowDataBound" DataKeyNames="NotificationID">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbNotiSelect" runat="server" onclick="selectItem('cbNotiSelect', 'cbNotiSelectAll', gvNotificationID)" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                   <%--                             <asp:CheckBox ID="cbNotiSelectAll" runat="server" Text="选择" onclick="javascript:selectAll('<%= gvNotification.ClientID%>','cbNotiSelect')" TextAlign="Left" />--%>
                   <input type="checkbox" id="cbNotiSelectAll" onclick="selectAll(gvNotificationID,'cbNotiSelect')" />
                                            </HeaderTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Images/Toolbar/edit.gif" />
                                                <asp:LinkButton ID="linkModify" runat="server">修改</asp:LinkButton>
                                                <asp:Image ID="imgPublish" runat="server" ImageUrl="~/App_Themes/Images/icon_publish.png" /><asp:LinkButton
                                                    ID="linkPublish" runat="server" OnClick="linkDetailPublish_Click">
                                                发布</asp:LinkButton>
           <asp:Image ID="Image1" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExportNoti" runat="server" 
                                                    onclick="linkExportNoti_Click">导出通知单</asp:LinkButton>
                                                <asp:Image ID="imgPublish0" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExportNotiDetails" runat="server" 
                                                    onclick="linkExportNotiDetails_Click">导出通知单明细</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Images/Toolbar/edit.gif" />
                                                <asp:LinkButton ID="linkFill" runat="server" onclick="linkFill_Click">录入</asp:LinkButton>
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Images/icon_import.gif" />
                                                <asp:LinkButton ID="linkImport" runat="server" onclick="linkImport_Click">导入</asp:LinkButton>
                                                <asp:Image ID="Image7" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExport" runat="server" onclick="linkExport_Click">导出</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="通知单号">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkNotificationNo" runat="server" Text='<%# Bind("NotificationCode") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="动静类型">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"静态":"动态" %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                动静类型
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PlanDate" HeaderText="计划盘点时间" />
                                        <asp:TemplateField HeaderText="创建人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("CreatorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="创建时间" DataField="DateCreated" />
                                        <asp:TemplateField HeaderText="发布状态">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPublished" runat="server" Text='<%# Convert.ToBoolean(Eval("Published"))?"已发布":"未发布" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="发布人" DataField="PublisherName" />
                                        <asp:BoundField HeaderText="发布时间" DataField="DatePublished" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <uc1:AspPager ID="pagerNotiList" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabDetails" runat="server" HeaderText="通知单明细">
                        <ContentTemplate>
                            <div class="divContainer" style="height: 255px; width:888px;">
                                <asp:GridView ID="gvDetails" DataKeyNames="NotificationID" runat="server" OnRowDataBound="gvDetails_RowDataBound"
                                    OnPreRender="gvDetails_PreRender" OnRowCommand="gvDetails_RowCommand" AutoGenerateColumns="False"
                                   >
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input type="checkbox" id="cbDetailSelectAll" onclick="selectAll(gvDetailsID,'cbDetailsSelect')" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDetailsSelect" onclick="selectItem('cbDetailsSelect', 'cbDetailSelectAll', gvDetailsID)" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Images/Toolbar/edit.gif" />
                                                <asp:LinkButton ID="linkModify" runat="server">修改</asp:LinkButton>
                                                <asp:Image ID="imgPublish" runat="server" ImageUrl="~/App_Themes/Images/icon_publish.png" />
                                                <asp:LinkButton ID="linkPublish" runat="server" OnClick="linkPublish_Click" Text="发布"></asp:LinkButton>
                                                <asp:Image ID="imgPublish0" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExportNoti" runat="server" 
                                                    onclick="linkExportNoti_Click">导出通知单</asp:LinkButton>
                                             <asp:Image ID="Image2" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExportNotiDetails" runat="server" 
                                                    onclick="linkExportNotiDetails_Click">导出通知单明细</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Images/Toolbar/edit.gif" />
                                                <asp:LinkButton ID="linkFill" runat="server">录入</asp:LinkButton>
                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Images/icon_import.gif" />
                                                <asp:LinkButton ID="linkImport" runat="server">导入</asp:LinkButton><asp:Image ID="Image7" runat="server" 
                                                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
                                                <asp:LinkButton ID="linkExport" runat="server" onclick="linkExport_Click">导出</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="通知单号">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkNotificationNo" runat="server" Text='<%# Bind("NotificationCode") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件号">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工厂">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="车间">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Workshops") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="工段">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Segments") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="零件名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DUNS">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="创建人">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("CreateByUserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="盘点类别">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="紧急程度">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("PriorityName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PreStaticNoticeCode" HeaderText="上次静态盘点通知单" />
                                        <asp:BoundField DataField="PreStaticNotiTime" HeaderText="上次静态盘点时间" DataFormatString="{0:G}" />
    <asp:BoundField DataField="PreDynamicNoticeCode" HeaderText="上次动态盘点通知单" />
    <asp:BoundField DataField="PreDynamicNotiTime" HeaderText="上次动态盘点时间" DataFormatString="{0:G}" />
                                        <asp:BoundField DataField="NotificationID" HeaderText="NotificationID" Visible="False" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div  style="width:888px;">
                            <uc1:AspPager ID="pagerDetails" runat="server" />
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

</asp:Content>
