<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsignmentPartRecordQuery.aspx.cs" Inherits="BizDataMaintain_ConsignmentPartRecordQuery" %>

<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
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

    <%--<fieldset class="List">
<legend class="mainTitle">外协记录维护</legend>--%>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
            <SCS:ToolbarButton CausesValidation="True" CommandName="export" Text="导出" ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="import" 
                ImageUrl="~/App_Themes/Images/Toolbar/import.gif" Text="导入" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <table>
            <tr>
                <td nowrap="nowrap">
                    工厂
                </td>
                <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="152px" />
                </td>
                <td nowrap="nowrap">
                    零件号
                </td>
                <td>
                    <asp:TextBox ID="txtPartCode" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    供应商DUNS
                </td>
                <td>
                    <asp:TextBox ID="txtDUNS" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    外协加工商
                </td>
                <td>
                    <asp:TextBox ID="txtDUNS1" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    外协加工商名称
                </td>
                <td>
                    <asp:TextBox ID="txtSupplierName" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContainer" style="height: 280px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RecordID"
            OnPreRender="GridView1_PreRender">
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
                <asp:TemplateField HeaderText="工厂">
                    <ItemTemplate>
                        <%#Eval("Part.Plant.PlantCode") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协零件号">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEdit_Click" CommandName='view'
                            CommandArgument='<%# Eval("RecordID")%>'><%# Eval("Part.PartCode")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="供应商DUNS">
                    <ItemTemplate>
                        <%#Eval("Part.Supplier.DUNS")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协供应商DUNS">
                    <ItemTemplate>
                        <%#Eval("Supplier.DUNS")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="外协供应商名称">
                    <ItemTemplate>
                        <%#Eval("Supplier.SupplierName")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电话">
                    <ItemTemplate>
                        <%#Eval("Supplier.PhoneNumber1")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="传真">
                    <ItemTemplate>
                        <%#Eval("Supplier.Fax")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <%#Eval("Description")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <%-- <asp:BoundField HeaderText="最后修改用户" DataField="UpdateBy"  />--%>
                <asp:BoundField HeaderText="最后修改时间" DataField="DateModified" />
            </Columns>
        </asp:GridView>
    </div>
    <uc1:AspPager ID="AspPager1" runat="server" Visible="True" />
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--</fieldset>--%>
</asp:Content>
