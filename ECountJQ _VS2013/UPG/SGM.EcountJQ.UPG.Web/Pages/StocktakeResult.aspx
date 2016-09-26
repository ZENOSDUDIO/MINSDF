<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StocktakeResult.aspx.cs"
    Inherits="SGM.ECountJQ.UPG.Web.Pages.StocktakeResult" %>

<%@ Register Src="GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="style/Default.css" />
    <script type="text/javascript" language="javascript" src="Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" language="javascript">
        function ShowNotificationList() {
            $("#trNotificationList").show();
            $("#trNotificationDetil").hide();
            $("#<%= hdCurrentTab.ClientID %>").val("list");
        }

        function ShowNotificationDetail() {
            $("#trNotificationList").hide();
            $("#trNotificationDetil").show();
            $("#<%= hdCurrentTab.ClientID %>").val("detail");
        }

        $(document).ready(function () {
            if ($("#<%= hdCurrentTab.ClientID %>").val() == "list") {
                ShowNotificationList();
            }
            else {
                ShowNotificationDetail();
            }
        });

        function OpenFillDialog() {
            return false;
        }
        function OpenExportDialog() {

            return false;
        }
        function OpenImportDialog(id) {
            ShowIframe("实盘结果导入", "StocktakeResultImport.aspx?id=" + id + "&rdm=" + Math.random(), 800, 450);
            return false;
        }
    </script>
    <asp:HiddenField ID="hdCurrentTab" runat="server" Value="list" />
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 80px;">
                            通知单号
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotificationCode" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckNotificationList" runat="server" Checked="true" Text="通知单列表" />
                        </td>
                        <td>
                            <asp:CheckBox ID="ckNotificationDetil" runat="server" Text="通知单明细" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 80px;">
                            <a href="javascript:void(0);" onclick="ShowNotificationList();">通知单列表</a>
                        </td>
                        <td style="width: 80px;">
                            <a href="javascript:void(0);" onclick="ShowNotificationDetail();">通知单明细</a>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trNotificationList">
            <td style="width: 100%;">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 100%;">
                            <div style="width: 100%; max-height: 300px; overflow: auto; text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="false" CssClass="Grid">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                操作
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "block":"none"%>;'>
                                                            <a href="javascript:void(0);" onclick="OpenFillDialog(<%# Eval(Fields.NotificationID) %>)">
                                                                录入</a>
                                                        </td>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "none":"block"%>;'>
                                                            <a disabled="disabled" href="javascript:void(0);">录入</a>
                                                        </td>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "block":"none"%>;'>
                                                            <a href="javascript:void(0);" onclick="OpenImportDialog(<%# Eval(Fields.NotificationID) %>)">
                                                                导入</a>
                                                        </td>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "none":"block"%>;'>
                                                            <a disabled="disabled" href="javascript:void(0);">导入</a>
                                                        </td>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "block":"none"%>;'>
                                                            <a href="javascript:void(0);" onclick="OpenExportDialog(<%# Eval(Fields.NotificationID) %>)">
                                                                导出</a>
                                                        </td>
                                                        <td style='border: 0px; background-color: White; padding: 0px; display: <%# ((int)Eval(Fields.Status) >= 3 && (int)Eval(Fields.Status) <= 5) ? "none":"block"%>;'>
                                                            <a disabled="disabled" href="javascript:void(0);">导出</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                通知单号
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("NotificationCode")%></ItemTemplate>
                                            <HeaderStyle Width="120px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="120px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                动静类型
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# (bool)Eval("IsStatic") ? "静态" : "动态" %></ItemTemplate>
                                            <HeaderStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                计划盘点时间
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("PlanDate") == null ? string.Empty : ((DateTime)Eval("PlanDate")).ToString("yyyy-MM-dd HH:mm:ss") %></ItemTemplate>
                                            <HeaderStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                创建人
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetUserName(Eval("CreatedBy"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                创建时间
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DateCreated") == null ? string.Empty : ((DateTime)Eval("DateCreated")).ToString("yyyy-MM-dd HH:mm:ss")%></ItemTemplate>
                                            <HeaderStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                发布状态
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Published") != null && (bool)Eval("Published") == true ? "已发布":"未发布"%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                发布人
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# GetUserName(Eval("PublishedBy"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="80px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                发布时间
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("DatePublished") == null ? string.Empty : ((DateTime)Eval("DatePublished")).ToString("yyyy-MM-dd HH:mm:ss")%></ItemTemplate>
                                            <HeaderStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemStyle Width="140px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <uc1:GridViewPager ID="gvpNotification" runat="server" OnPageIndexChanged="gvpNotification_PageIndexChanged" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trNotificationDetil" style="display: none;">
            <td style="width: 100%;">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 100%;">
                            <asp:GridView ID="gvDetails" runat="server" Width="100%" AutoGenerateColumns="true"
                                CssClass="Grid">
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <uc1:GridViewPager ID="gvpDetails" runat="server" OnPageIndexChanged="gvpDetails_PageIndexChanged" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
