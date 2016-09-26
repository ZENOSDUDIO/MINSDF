<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NotificationQuery.ascx.cs" Inherits="PhysicalCount_UserControl_NotificationQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%">
                        <tr>
                            <td nowrap="nowrap">
                                通知单号
                            </td>
                            <td>
                                <asp:TextBox ID="txtNotificationNO" runat="server" Width="126px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                零件号
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartNO" runat="server" Width="126px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                盘点状态
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="92px" 
                                    AppendDataBoundItems="True" DataTextField="StatusName" 
                                    DataValueField="StatusID">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                申请单号
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequestNO" runat="server" Width="126px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                零件中文名
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartCName" runat="server" Width="126px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                创建时间 从
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateStart" runat="server" Width="106px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateStart_CalendarExtender" runat="server" Enabled="True"
                                    PopupButtonID="btnCalendarStart" TargetControlID="txtDateStart">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="btnCalendarStart" runat="server" CausesValidation="False" EnableViewState="False"
                                    ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                            </td>
                            <td nowrap="nowrap">
                                到
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateEnd" runat="server" Width="106px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateEnd_CalendarExtender" runat="server" Enabled="True"
                                    PopupButtonID="btnCalendarEnd" TargetControlID="txtDateEnd">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="btnCalendarEnd" runat="server" CausesValidation="False" EnableViewState="False"
                                    ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                            </td>
                            <td colspan="1" nowrap="nowrap">
                                <span lang="zh-cn">申请</span>人
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtCreateBy" runat="server" Width="102px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">
                                计划盘点时间 从
                            </td>
                            <td>
                                <asp:TextBox ID="txtPlanDateStart" runat="server" Width="106px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtPlanDateStart_CalendarExtender" runat="server" Enabled="True"
                                    PopupButtonID="btnCalendarPlanStart" TargetControlID="txtPlanDateStart">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="btnCalendarPlanStart" runat="server" CausesValidation="False"
                                    EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                            </td>
                            <td nowrap="nowrap">
                                到
                            </td>
                            <td>
                                <asp:TextBox ID="txtPlanDateEnd" runat="server" Width="106px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtPlanDateEnd_CalendarExtender" runat="server" Enabled="True"
                                    PopupButtonID="btnCalendarPlanEnd" TargetControlID="txtPlanDateEnd">
                                </cc1:CalendarExtender>
                                <asp:ImageButton ID="btnCalendarPlanEnd" runat="server" CausesValidation="False"
                                    EnableViewState="False" ImageUrl="~/App_Themes/Images/icon-calendar.gif" ImageAlign="Middle" />
                            </td>
                            <td colspan="2" nowrap="nowrap">
                                <asp:CheckBoxList ID="cblSearchOption" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">通知单</asp:ListItem>
                                    <asp:ListItem>通知单明细</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td nowrap="nowrap">申请人所属组</td>
                            <td>
                                <asp:TextBox ID="txtUserGroup" runat="server" Width="126px"></asp:TextBox>
                            </td>
                            <td nowrap="nowrap">
                                <span lang="zh-cn">盘点区域</span></td>
                            <td>
                                <asp:DropDownList ID="ddlStoreLocation" runat="server" 
                                    AppendDataBoundItems="True">
                                    <asp:ListItem>SGM现场</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="1" nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;
                            </td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                            <td nowrap="nowrap">
                                &nbsp;</td>
                        </tr>
                    </table>