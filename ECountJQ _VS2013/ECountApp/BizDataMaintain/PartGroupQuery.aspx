<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PartGroupQuery.aspx.cs" Inherits="BizDataMaintain_PartGroupQuery" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
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

            <div class="divBody">
                <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                    OnButtonClicked="Toolbar1_ButtonClicked">
                    <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                        CssClassSelected="button_selected" />
                    <Items>
                        <SCS:ToolbarButton CausesValidation="True" CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                            Text="添加" />
                        <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                            Text="删除" ConfirmationMessage="确认删除？" CommandName="delete" />
                        <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                            Text="查询" />
                        <SCS:ToolbarButton CausesValidation="True" CommandName="export" 
                            ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" Text="导出" />
                    </Items>
                </SCS:Toolbar>
                <div class="divContainer">
                    分组名称<asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="divContainer" style="height: 380px">
                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
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
                                    <asp:TemplateField HeaderText="分组名称">
                                        <ItemTemplate>
                                            <a href='PartGroupEdit.aspx?GroupID=<%# Eval("GroupID") %>' target="_self">
                                                <%# Eval("GroupName") %></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="描述">
                                        <ItemTemplate>
                                            <%# Eval("Description") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:HiddenField ID="hidGroupID" runat="server" Value="0"></asp:HiddenField>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
</asp:Content>
