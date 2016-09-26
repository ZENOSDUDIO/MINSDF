<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DifferenceAnalyzeItemList.aspx.cs" Inherits="SystemManagement_DifferenceAnalyzeItemList" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/jscript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            if (eventArgs.get_selectedIndex() == 0) {
                showDialog('DifferenceAnalvzeItemDetails.aspx?Mode=Edit', 600, 300, "returnOk()", "returnCancel()");
                eventArgs.set_cancel(true);
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
    </script>

    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
        OnClientButtonClick="toolbar_ButtonClicked" OnButtonClicked="Toolbar1_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassSelected="button_selected"
            CssClassDisabled="button_disabled"></ButtonCssClasses>
        <Items>
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="添加" CommandName="add" />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除" CommandName="delete" ConfirmationMessage="确认删除?" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer" style="height: 395px">
        <asp:GridView ID="gvDifferenceAnalyseDetail" runat="server" AutoGenerateColumns="False"
            OnRowDataBound="gvDifferenceAnalyseDetail_RowDataBound" DataKeyNames="DetailsID">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ChkALL" runat="server" OnCheckedChanged="ChkAll_CheckedChanged"
                            AutoPostBack="true" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="IsCheck" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" Visible="False">
                    <ItemTemplate>
                        <%# Eval("DetailsID")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="modify" CommandArgument='<%#Eval("DetailsID") %>'>修改</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户组">
                    <ItemTemplate>
                        <%#Eval("UserGroup.GroupName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="差异分析项目详细">
                    <ItemTemplate>
                        <%#Eval("Description") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnTemp" runat="server" Width="0px" Style="display: none" OnClick="btnTemp_Click" />
    </div>
</asp:Content>
