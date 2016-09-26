<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ImportAnalyseRef.aspx.cs" Inherits="PhysicalCount_ImportAnalyseRef" %>

<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc2" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        var gvNotificationID = '<%= gvNotification.ClientID%>';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
 <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true" >
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
     <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                            Text="查询" />
        </Items>
    </SCS:Toolbar> 
    <table style="width: 100%">
        <tr>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/AnalyseRefImport.xls"
                    Target="_blank">模板下载</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                    <table width="100%" >
                                <tr >
                                    <td nowrap="nowrap" >
                                        通知单号</td>
                                    <td nowrap="nowrap" >
                                        <asp:TextBox ID="txtNotificationNO" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" >
                                        &nbsp;申请单号</td>
                                    <td >
                                        <asp:TextBox ID="txtRequestNO" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" >
                                        零件中文名
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPartCName" runat="server" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" >
                                        零件号</td>
                                    <td nowrap="nowrap" >
                                        
                                        <asp:TextBox ID="txtPartNO" runat="server" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="white-space:nowrap" >申请人所属组</td>
                                    <td >
                                        <asp:TextBox ID="txtUserGroup" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td >
                                        &nbsp;创建时间 从</td>
                                    <td style="white-space:nowrap" >
                                        <asp:TextBox ID="txtDateStart" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateStart_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarStart" 
                                            TargetControlID="txtDateStart" Format="yyyy-MM-dd HH:mm:ss" CssClass ="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarStart" runat="server" CausesValidation="False" 
                                            EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap" >
                                        &nbsp;到</td>
                                    <td style="white-space:nowrap" >
                                        <asp:TextBox ID="txtDateEnd" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateEnd_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarEnd" TargetControlID="txtDateEnd" 
                                            Format="yyyy-MM-dd HH:mm:ss"  CssClass ="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarEnd" runat="server" CausesValidation="False" 
                                            EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap" >
                                        盘点状态</td>
                                    <td nowrap="nowrap" >
                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" 
                                            DataTextField="StatusName" DataValueField="StatusID" Width="84px">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr >
                                    <td nowrap="nowrap" >
                                        申请人</td>
                                    <td nowrap="nowrap" >
                                        <asp:TextBox ID="txtCreateBy" runat="server" Width="102px"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" >
                                        &nbsp;计划盘点时间 从</td>
                                    <td style="white-space:nowrap" >
                                        <asp:TextBox ID="txtPlanDateStart" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPlanDateStart_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarPlanStart" 
                                            TargetControlID="txtPlanDateStart" Format="yyyy-MM-dd HH:mm:ss" CssClass ="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarPlanStart" runat="server" 
                                            CausesValidation="False" EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap" >
                                        &nbsp;到
                                    </td>
                                    <td style="white-space:nowrap" >
                                        <asp:TextBox ID="txtPlanDateEnd" runat="server" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtPlanDateEnd_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="btnCalendarPlanEnd" 
                                            TargetControlID="txtPlanDateEnd" Format="yyyy-MM-dd HH:mm:ss" CssClass ="Calendar">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="btnCalendarPlanEnd" runat="server" 
                                            CausesValidation="False" EnableViewState="False" ImageAlign="Middle" 
                                            ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                    </td>
                                    <td nowrap="nowrap" colspan="2" > 
                                      <%--  &nbsp;空单？
                                    </td>
                                    <td nowrap="nowrap">--%>
                                        <%--<asp:DropDownList ID="DropDownList1" runat="server" Width="84px">
                                            <asp:ListItem>空单</asp:ListItem>
                                            <asp:ListItem>非空单</asp:ListItem>
                                        </asp:DropDownList>--%>
                                    </td>
                                </tr>
                            </table>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:GridView runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="NotificationID" ID="gvNotification" 
                                    OnPreRender="gvNotification_PreRender"><Columns>
<asp:TemplateField><ItemTemplate>
                                                <asp:CheckBox ID="cbNotiSelect" runat="server" onclick="selectItem('cbNotiSelect', 'cbNotiSelectAll', gvNotificationID)" />
                                            
</ItemTemplate>
<HeaderTemplate>
                   <%--                             <asp:CheckBox ID="cbNotiSelectAll" runat="server" Text="选择" onclick="javascript:selectAll('<%= gvNotification.ClientID%>','cbNotiSelect')" TextAlign="Left" />--%>
                   <input type="checkbox" id="cbNotiSelectAll" onclick="selectAll(gvNotificationID,'cbNotiSelect')" />
                                            
</HeaderTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="通知单号" DataField="NotificationCode"></asp:BoundField>
<asp:TemplateField HeaderText="动静类型"><ItemTemplate>
                                                <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"静态":"动态" %>'></asp:Label>
                                            
</ItemTemplate>
<HeaderTemplate>
                                                动静类型
                                            
</HeaderTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="计划盘点时间" DataField="PlanDate"></asp:BoundField>
<asp:BoundField HeaderText="创建人" DataField="CreatorName"></asp:BoundField>
<asp:BoundField HeaderText="创建时间" DataField="DateCreated"></asp:BoundField>
<asp:TemplateField HeaderText="发布状态"><ItemTemplate>
                                                <asp:Label ID="lblPublished" runat="server" Text='<%# Convert.ToBoolean(Eval("Published"))?"已发布":"未发布" %>'></asp:Label>
                                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="发布人" DataField="PublisherName"></asp:BoundField>
<asp:BoundField HeaderText="发布时间" DataField="DatePublished"></asp:BoundField>
</Columns>
</asp:GridView>
<uc2:asppager ID="pagerNotiList" runat="server" />
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" valign="middle">
                <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div class="divContainer" style="height: 230px">
                    <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" EnableViewState="False"
                        OnPreRender="gvResult_PreRender" Width="98%">
                        <Columns>
                            <asp:BoundField DataField="PartCode" HeaderText="零件号" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PartPlantCode" HeaderText="工厂" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DUNS" HeaderText="供应商DUNS" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Wip" HeaderText="WIP" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnRecorded" HeaderText="未入账" >
                             <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="M080" HeaderText="M080" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

