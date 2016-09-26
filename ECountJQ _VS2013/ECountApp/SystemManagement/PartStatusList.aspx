<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PartStatusList.aspx.cs" Inherits="SystemManagement_PartStatusList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    showDialog('PartStatusMg.aspx', 600, 200, "returnOk()", "returnCancel()");
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

            showDialog('PartStatusMg.aspx?statusid=' + id, 600, 200, "returnOk()", "returnCancel()");
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

    <%--<fieldset class="List">
    <legend class="mainTitle">零件状态维护</legend>--%>
    <div class="divBody">
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
            OnClientButtonClick="toolbar_ButtonClicked" OnButtonClicked="Toolbar1_ButtonClicked">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                    Text="添加" />
                <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                    Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
            </Items>
        </SCS:Toolbar>
        <div class="divContainer" style="height: 395px;">
            <asp:GridView ID="GridView1" DataKeyNames="StatusID" AutoGenerateColumns="False"
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
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料状态名称">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEdit_Click" CommandName='view'
                                CommandArgument='<%# Eval("StatusID")%>'><%# Eval("StatusName")%></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CheckBoxField HeaderText="是否循环盘点" HeaderStyle-HorizontalAlign="Left" DataField="CycleCount" />
                    <%-- <asp:CheckBoxField HeaderText="是否启用" HeaderStyle-HorizontalAlign="Left" DataField="Available" />--%>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnTemp" runat="server" Width="0px" Style="display: none" OnClick="btnTemp_Click" />
        </div>
    </div>
    <%-- </fieldset>--%>
</asp:Content>
