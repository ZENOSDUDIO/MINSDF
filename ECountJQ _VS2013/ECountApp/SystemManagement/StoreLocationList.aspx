<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StoreLocationList.aspx.cs" Inherits="SystemManagement_StoreLocationList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    showDialog('StoreLocationMg.aspx', 800, 300, "returnOk()", "returnCancel()");
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

            showDialog('StoreLocationMg.aspx?locationid=' + id, 800, 300, "returnOk()", "returnCancel()");
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

    <%--<fieldset class="List" >
    <legend class="mainTitle">存储区域维护</legend>--%>
    <div class="divBody">
        <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
            OnClientButtonClick="toolbar_ButtonClicked" OnButtonClicked="Toolbar1_ButtonClicked"
            Width="98%">
            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                CssClassSelected="button_selected" />
            <Items>
                <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                    Text="添加" />
                <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                    Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
                <SCS:ToolbarButton CausesValidation="True" Text="查询" CommandName="search" 
                    ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png" />
                <SCS:ToolbarButton CausesValidation="True" CommandName="export" 
                    ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" Text="导出" />
                <SCS:ToolbarButton CausesValidation="True" CommandName="import" 
                    ImageUrl="~/App_Themes/Images/Toolbar/import.gif" Text="导入" />
            </Items>
        </SCS:Toolbar>
        <div class="divContainer">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="存储区域名称" Width="100px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStorelocation" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
        <div class="divContainer" style="height: 415px;">
            <asp:GridView ID="GridView1" DataKeyNames="LocationID" AutoGenerateColumns="False"
                runat="server" OnPreRender="GridView1_PreRender"
                OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="删除标记">
                        <HeaderTemplate>
                            <input type="checkbox" id="ChkAll" onclick="checkAll(this);" name="ChkAll" title="全选/全不选" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSelected" runat="server" TabIndex="-1" onclick="javascript:checkItem(this, 'ChkAll');" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="存储区域名称">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEdit_Click" CommandName='view'
                                CommandArgument='<%# Eval("LocationID")%>'><%# Eval("LocationName")%></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="区域类型">
                        <ItemTemplate>
                            <%# Eval("StoreLocationType.TypeName")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:CheckBoxField HeaderText="Available" HeaderStyle-HorizontalAlign="Center" 
                        DataField="AvailableIncluded" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:CheckBoxField>
                    <asp:CheckBoxField HeaderText="QI" HeaderStyle-HorizontalAlign="Center" DataField="QIIncluded">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:CheckBoxField>
                    <asp:CheckBoxField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" DataField="BlockIncluded">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:CheckBoxField>
                    <asp:TemplateField HeaderText="对应物流系统存储区域">
                        <ItemTemplate>
                            <%# Eval("LogisticsSysSLOC")%>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnTemp" runat="server" Width="0px" Style="display: none" OnClick="btnTemp_Click" />
        </div>
    </div>
    <%-- </fieldset>--%>
</asp:Content>
