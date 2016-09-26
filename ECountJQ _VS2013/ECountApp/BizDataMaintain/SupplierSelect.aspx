<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierSelect.aspx.cs" Inherits="BizDataMaintain_SupplierSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 100%;">
        <div class ="divContainer" style="width:95%">
            <span>DUNS</span>
            <asp:TextBox ID="txtDUNS" runat="server" Width="80px">
            </asp:TextBox>
            <span>供应商名称</span>
            <asp:TextBox ID="txtSupplierName" runat="server">
            </asp:TextBox>
            <asp:Button ID="ButQuery" runat="server" Text="查询" OnClick="ButQuery_Click" />
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="SupplierID,DUNS" Width="600px"
                AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand">
                <Columns>
                    <%--<asp:TemplateField HeaderText="ID" >
                            <itemtemplate>
                                <%# Eval("SupplierID")%>
                            </itemtemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                    <asp:ButtonField CommandName="Select" HeaderText="选择" Text="选择">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:TemplateField HeaderText="DUNS">
                        <ItemTemplate>
                            <%# Eval("DUNS")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商名称">
                        <ItemTemplate>
                            <%# Eval("SupplierName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="width: 600px;">
            <uc1:AspPager ID="AspPager1" runat="server" />
        </div>
    </div>
    <uc2:ModalDialog ID="ModalDialog1" runat="server" />
    </form>
</body>
</html>
