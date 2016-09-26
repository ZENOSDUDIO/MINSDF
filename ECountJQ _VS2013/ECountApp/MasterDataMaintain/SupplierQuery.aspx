<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SupplierQuery.aspx.cs" Inherits="BizDataMaintain_SupplierQuery" %>

<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc2" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    showDialog('SupplierMg.aspx', 800, 300, "returnOk()", "returnCancel()");
                    eventArgs.set_cancel(true);
                    break;
                default:
                    break;
            }
        }
        function returnOk() {
            var v = getReturnValue();
            if (v == 'ok') {
                var objbtn = document.getElementById('<%= btnTemp.ClientID%>');
                objbtn.click();
            }
        }
        function returnCancel() {
            var objbtn = document.getElementById('<%= btnTemp.ClientID%>');
            objbtn.click();
        }
        function showdata(id) {

            showDialog('SupplierMg.aspx?supplierid=' + id, 800, 300, "returnOk()", "returnCancel()");
            return false;
        }


        function checkAll(e) {
            var grid = document.getElementById('<%=GridView1.ClientID %>')
            var items = grid.getElementsByTagName("input");

            for (var i = 0; i < items.length; i++) {
                var idx = items[i].id.indexOf("ChkSelected");
                if (items[i].type = "checkbox") {
                    if (idx > 0)
                        items[i].checked = e.checked;
                }
            }

        }

        function checkItem(e, allName) {
            var all = document.getElementsByName(allName)[0];
            if (!e.checked)
                all.checked = false;
            else {
                var grid = document.getElementById('<%=GridView1.ClientID %>')
                var items = grid.getElementsByTagName("input");
                var count = 0;
                var scount = 0;
                for (var i = 0; i < items.length; i++) {
                    var idx = items[i].id.indexOf("ChkSelected");
                    if (items[i].type = "checkbox") {
                        if (idx > 0) {
                            ++count;
                            if (items[i].checked)
                                ++scount;
                        }
                    }
                }
                if (scount == count) {
                    all.checked = true;
                }
                else
                    all.checked = false;
            }
        }
    </script>

    <%--<fieldset style="width:800px">
<legend class="mainTitle">供应商维护</legend>--%>
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
        OnClientButtonClick="toolbar_ButtonClicked" OnButtonClicked="Toolbar1_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="添加" />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
            <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                Text="查询" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="export" ImageUrl="~/App_Themes/Images/Toolbar/Export.gif"
                Text="导出"  />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="导入"  />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="DUNS" Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDUNS" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="供应商名称" Width="80px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplierName" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 320px;" class="divContainer">
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" DataKeyNames="SupplierID,DUNS"
            runat="server"  OnPreRender="GridView1_PreRender"
            OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="删除标记">
                    <HeaderTemplate>
                        <input type="checkbox" id="ChkAll" onclick="checkAll(this);" name="ChkAll" title="全选/全不选" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkSelected" runat="server" onclick="javascript:checkItem(this, 'ChkAll');" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DUNS" SortExpression="DUNS">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="LinkButton1" CommandName='editex' CommandArgument='<%# Eval("SupplierID")%>'><%# Eval("DUNS")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商名称" SortExpression="SupplierName">
                    <ItemTemplate>
                        <%# Eval("SupplierName")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电话1">
                    <ItemTemplate>
                        <%# Eval("PhoneNumber1")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电话2">
                    <ItemTemplate>
                        <%# Eval("PhoneNumber2")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="传真">
                    <ItemTemplate>
                        <%# Eval("Fax")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <uc2:AspPager ID="AspPager1" runat="server" Visible="True" />
    </div>
    <asp:Button ID="btnTemp" runat="server" Width="0px" Style="display: none" OnClick="btnTemp_Click" />
    <%--</fieldset>--%>
</asp:Content>
