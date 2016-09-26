<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotificationPublish.aspx.cs"
    Inherits="PhysicalCount_NotificationPublish" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="asppager" TagPrefix="uc2" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    
    <div>
        <table width="100%">
            <tr>
                <td><%--<SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                        OnButtonClicked="Toolbar1_ButtonClicked">
                        <ButtonCssClasses CssClass="button" CssClassDisabled="button_disabled" CssClassEnabled="button_enabled"
                            CssClassSelected="button_selected" />
                        <Items>
                            <SCS:ToolbarButton CommandName="Publish" ImageUrl="~/App_Themes/Images/Toolbar/publish.gif"
                                Text="确认发布" />
                            <SCS:ToolbarButton CommandName="Cancel" ImageUrl="~/App_Themes/Images/Toolbar/Undo1.gif"
                                Text="取消发布" />
                        </Items>
                    </SCS:Toolbar>--%>
                    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
            EnableClientApi="true">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="Publish" ImageUrl="~/App_Themes/Images/Toolbar/Publish.gif"
                    Text="发布" ValidationGroup="1" />
                <%--<SCS:ToolbarButton CommandName="Cancel" ImageUrl="~/App_Themes/Images/Toolbar/undo1.gif"
                    Text="取消发布" />--%>
                <%--<SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                        Text="返回" CommandName="return" ValidationGroup="0" />--%>
            </Items>
        </SCS:Toolbar>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divContainer">
                        <asp:FormView ID="fvNotification" runat="server" Width="100%">
                            <ItemTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span lang="zh-cn">通知单号：</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Label ID="lblNoticeNo" runat="server" Text='<%# Bind("NotificationCode") %>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <span lang="zh-cn">动静类型：</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblIsStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"静态":"动态" %>'></asp:Label>
                                        </td>
                                        <td align="right" valign="middle">
                                            &nbsp;
                                            <asp:RadioButtonList ID="rblEmergency" runat="server" RepeatDirection="Horizontal"
                                                SelectedIndex='<%# Convert.ToBoolean(Eval("IsEmergent"))?0:1 %>'>
                                                <asp:ListItem Selected="True" Value="0">紧急</asp:ListItem>
                                                <asp:ListItem Value="1">正常</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span lang="zh-cn">创建用户：</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblCreateBy" runat="server" Text='<%# Bind("Creator.UserName") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <span lang="zh-cn">创建时间：</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Label ID="lblDateCreate" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <span lang="zh-cn">计划盘点时间：</span>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:TextBox ID="txtPlanDate" runat="server"  Text='<%# Bind("PlanDate") %>'></asp:TextBox>
                                            <asp:ImageButton ID="btnCalendar" runat="server" CausesValidation="False" 
                                                EnableViewState="False" ImageAlign="Middle" 
                                                ImageUrl="~/App_Themes/Images/icon-calendar.gif" />
                                            <cc1:CalendarExtender ID="txtPlanDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtPlanDate" PopupButtonID="btnCalendar" Format="yyyy-MM-dd HH:mm:ss" CssClass = "Calendar" >
                                            </cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqValiPlanTime" runat="server" 
                                                ControlToValidate="txtPlanDate" ErrorMessage="请选择计划盘点时间" 
                                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="reqValiPlanTime_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="reqValiPlanTime" 
                                                PopupPosition="BottomLeft">
                                            </cc1:ValidatorCalloutExtender>
                                            
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divContainer">
                        <asp:Repeater ID="rpSLOC" runat="server" OnItemDataBound="rpSLOC_ItemDataBound">
                            <HeaderTemplate>
                                <table width="100%">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td>
                                    <asp:CheckBox ID="cbSLOC" Text='<%# Eval("TypeName") %>' runat="server" />
                                </td>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <td>
                                    <asp:CheckBox ID="cbSLOC" runat="server" Text='<%# Eval("TypeName")%>' />
                                </td>
                                </tr><tr>
                            </AlternatingItemTemplate>    
                            <FooterTemplate>
                                </tr></table></FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divContainer">
                        <asp:GridView ID="gvDetails" runat="server" OnPreRender="gv_PreRender" AutoGenerateColumns="False"
                            DataKeyNames="DetailsID">
                            <Columns>
                                <asp:TemplateField HeaderText="零件号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartCode" runat="server" Text='<%# Bind("PartCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="工厂">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlantCode" runat="server" Text='<%# Bind("PartPlantCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="车间">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkshops" runat="server" Text='<%# Bind("Workshops") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="工段">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSegments" runat="server" Text='<%# Bind("Segments") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="零件名称">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartCName" runat="server" Text='<%# Bind("PartChineseName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DUNS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDUNS" runat="server" Text='<%# Bind("DUNS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        <asp:BoundField HeaderText="申请单号" DataField="RequestNumber" />
                                <asp:TemplateField HeaderText="申请人">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateBy" runat="server" Text='<%# Bind("CreateByUserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="盘点类别">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStocktakeType" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="紧急程度">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("PriorityName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <div class="divContainer">
            <uc2:AspPager ID="pager" runat="server" />
        </div>
        <uc1:ModalDialog ID="ModalDialog1" runat="server" />
    </div>
    </form>
</body>
</html>
