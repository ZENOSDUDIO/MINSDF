<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CycleCountRequest.aspx.cs" Inherits="PhysicalCount_CycleCountRequest" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<%@ Register Src="../UserControl/AspPager.ascx" TagName="AspPager" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                default:
//                    showWaitingModal();
                    break;
            }
        }
    </script>

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td>
                   <div style="width:888px;">
                        <SCS:Toolbar runat="server" CssClass="toolbar"  OnButtonClicked="Toolbar1_ButtonClicked"
                            ID="Toolbar1" EnableClientApi="false">
                            <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                                CssClassSelected="button_selected" />
                            <Items>
                                <SCS:ToolbarButton CommandName="preview" ImageUrl="~/App_Themes/Images/Toolbar/Preview.gif" DisabledImageUrl="~/App_Themes/Images/Toolbar/Preview.gif"
                                    Text="Cycle Count 申请单预览" />
                                <SCS:ToolbarButton CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                    Text="Cycle Count 申请单生成" DisabledImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Visible="False" />
                            </Items>
                        </SCS:Toolbar>
                      </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="divContainer" style="width:888px;">
                            <table width="100%">
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        年度Cycle Count总次数：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        年度已完成Cycle Count次数：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCompleted" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        剩余Cycle Count次数：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblRest" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        平均每次Cycle Count零件数：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblAvg" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="divContainer" style="height: 280px; width:888px;">
                            <asp:GridView ID="gvParts" runat="server" DataKeyNames="PartID" 
                                OnPreRender="gvParts_PreRender" onrowdatabound="gvParts_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="零件号" SortExpression="PartCode">
                                        <ItemTemplate>
                                            <%# Eval("PartCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="零件英文名称" SortExpression="PartEnglishName">
                                        <ItemTemplate>
                                            <%# Eval("PartEnglishName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="零件中文名称" SortExpression="PartChineseName">
                                        <ItemTemplate>
                                            <%# Eval("PartChineseName")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="工厂">
                                        <ItemTemplate>
                                            <%# Eval("PlantCode")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="车间" DataField="Workshops" />
                            <asp:BoundField HeaderText="工段" DataField="Segments" />
                                    <asp:TemplateField HeaderText="工位">
                                        <ItemTemplate>
                                            <%# Eval("WorkLocation")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="循环盘点级别">
                                        <ItemTemplate>
                                            <%# Eval("LevelName")%></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField HeaderText="已循环盘点次数" DataField="CycleCountTimes" />                                    
                                    <asp:TemplateField HeaderText="车型">
                                        <ItemTemplate>
                                            <%# Eval("Specs")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Followup">
                                        <ItemTemplate>
                                            <%# Eval("Followup")%></ItemTemplate>
                                    </asp:TemplateField>                                    
                            <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                            <asp:BoundField HeaderText="供应商名称" DataField="SupplierName" />
                                    <asp:TemplateField HeaderText="物料类别">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName")%></ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField HeaderText="物料状态" DataField="StatusName" />
                        
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDelete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="width:900px;">
                            <uc1:AspPager ID="AspPager1" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="divContainer" style="height: 100px;width:888px;">
                            <asp:GridView ID="gvRequest" runat="server" onprerender="gvRequest_PreRender" 
                                onrowdatabound="gvRequest_RowDataBound"><Columns>
                                                <asp:TemplateField HeaderText="申请单号">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        申请单号
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="动静类型">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"静态":"动态" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        动静类型
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="工厂">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPlantCode" runat="server" Text='<%# Eval("Plant.PlantCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="申请人">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="申请时间">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DateRequest", "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DateRequest") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
    <%# Eval("PartCode")%> 
</asp:Content>
