<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PartsQuery.aspx.cs" Inherits="MasterDataMaintain_PartsQuery" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function checkAll(e) {
            var grid = document.getElementById('<%=gvParts.ClientID %>')
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
                var grid = document.getElementById('<%=gvParts.ClientID %>')
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
    <div class="divContainer"style="width:888px;">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
        OnButtonClicked="Toolbar1_ButtonClicked">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CommandName="add" ImageUrl="~/App_Themes/Images/Toolbar/Add.gif"
                Text="添加" />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="删除" ConfirmationMessage="确认删除?" CommandName="delete" />
            <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                Text="查询" />
            <SCS:ToolbarButton CausesValidation="false" Text="导出" CommandName="exportAll" ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="导入" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="batchUpdate" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="批量修改" />
        </Items>
    </SCS:Toolbar>
           </div>
    <div class="divContainer"style="width:888px;">
        <table  >
            <tr>
                <td>
                    零件号
                </td>
                <td>
                    <asp:TextBox ID="txtPartCode" runat="server" Width="108px"></asp:TextBox>
                </td>
                <td>
                    零件中文名称
                </td>
                <td>
                    <asp:TextBox ID="txtPartChineseName" runat="server" Width="115px"></asp:TextBox>
                </td>
                <td>
                    循环盘点级别
                </td>
                <td>
                    <asp:DropDownList ID="ddlCycleCountLevel" runat="server" Width="108px" AppendDataBoundItems="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    最后修改用户
                </td>
                <td>
                    <asp:TextBox ID="txtUpdateBy" runat="server" Width="108px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    车型
                </td>
                <td>
                    <asp:TextBox ID="txtSpecs" runat="server" Width="108px"></asp:TextBox>
                </td>
                <td>
                    FollowUp
                </td>
                <td>
                    <asp:TextBox ID="txtFollowUp" runat="server" Width="108px"></asp:TextBox>
                </td>
                <td>
                    物料类别
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryID" runat="server" Width="108px">
                    </asp:DropDownList>
                </td>
                <td>
                    物料状态
                </td>
                <td>
                    <asp:DropDownList ID="ddlPartStatus" runat="server" Width="108px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    工厂
                </td>
                <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlPlantID_CascadingDropDown" runat="server" Category="PlantCode"
                        Enabled="True" ServiceMethod="GetPlantPageMethod" TargetControlID="ddlPlantID"
                        LoadingText="数据载入中...">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    车间
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkshopID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlWorkshopID_CascadingDropDown" runat="server" Category="WorkshopCode"
                        Enabled="True" ParentControlID="ddlPlantID" ServiceMethod="GetWorkshopsPageMethod"
                        TargetControlID="ddlWorkshopID" LoadingText="数据载入中...">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    工段
                </td>
                <td>
                    <asp:DropDownList ID="ddlSegmentID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlSegmentID_CascadingDropDown" runat="server" Category="SegmentCode"
                        Enabled="True" LoadingText="数据载入中..." ParentControlID="ddlWorkshopID" ServiceMethod="GetSegmentsPageMethod"
                        TargetControlID="ddlSegmentID">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    工位
                </td>
                <td>
                    <asp:TextBox ID="txtWorkLocation" runat="server" Width="108px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    D U N S
                </td>
                <td>
                    <asp:TextBox ID="txtDUNS" runat="server" Width="108px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="divContainer" style="width:888px; height: 260px;">
        <asp:GridView ID="gvParts" AutoGenerateColumns="False" runat="server" DataKeyNames="PartID"
            OnPreRender="gvParts_PreRender" OnRowDataBound="gvParts_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="删除标记">
                    <HeaderTemplate>
                        <input type="checkbox" id="ChkAll" onclick="checkAll(this);" name="ChkAll" title="全选/全不选" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkSelected" runat="server" onclick="javascript:checkItem(this, 'ChkAll');" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="零件号" SortExpression="PartCode">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEditPart_Click" CommandName='<%# Eval("PartCode")%>'
                            CommandArgument='<%# Eval("PartID")%>'><%# Eval("PartCode")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="主键" Visible="False">
                    <ItemTemplate>
                        <%# Eval("PartID")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="零件英文名称" SortExpression="PartEnglishName" Visible="False">
                    <ItemTemplate>
                        <%# Eval("PartEnglishName")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="0px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="零件名称" SortExpression="PartChineseName">
                    <ItemTemplate>
                        <%# Eval("PartChineseName")%></ItemTemplate>
                    <ControlStyle />
                    <FooterStyle />
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工位" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <%# Eval("WorkLocation")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px">
                    </ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="库位">
                         <ItemTemplate>
                        <%# Eval("Dloc")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px">
                    </ItemStyle>           
                </asp:TemplateField>
                <asp:TemplateField HeaderText="循环盘点级别">
                    <ItemTemplate>
                        <%# Eval("LevelName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="已循环盘点次数">
                    <ItemTemplate>
                        <%# Eval("CycleCountTimes")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车型">
                    <ItemTemplate>
                        <%# Eval("Specs")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FollowUp">
                    <ItemTemplate>
                        <%# Eval("Followup")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工厂">
                    <ItemTemplate>
                        <%# Eval("PlantCode")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车间">
                    <ItemTemplate>
                        <%# Eval("Workshops")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工段">
                    <ItemTemplate>
                        <%# Eval("Segments")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料类别">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料状态">
                    <ItemTemplate>
                        <%# Eval("StatusName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DUNS">
                    <ItemTemplate>
                        <%# Eval("DUNS")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最后修改用户">
                    <ItemTemplate>
                        <%# Eval("UserName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div  style="width:898px;">
    <uc1:AspPager ID="AspPager1" runat="server" Visible="True" />
    </div>
</asp:Content>
