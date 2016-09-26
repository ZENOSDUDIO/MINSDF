<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PartImport.aspx.cs" Inherits="MasterDataMaintain_PartImport" %>

<%@ Register Src="../Common/UCFileUpload.ascx" TagName="UCFileUpload" TagPrefix="uc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
        EnableClientApi="true">
        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
            CssClassSelected="button_selected" />
        <Items>
            <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                Text="返回" CommandName="return" />
        </Items>
    </SCS:Toolbar>
    <div class="divContainer">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ImportTemplate/PartImport.xls"
            Target="_self">模板下载</asp:HyperLink>
    </div>
    <asp:MultiView ID="mvFields" runat="server" ActiveViewIndex="1">
        <asp:View ID="View1" runat="server">
            <div class="divContainer">
                <asp:RadioButtonList ID="rblFields" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">修改〖F/U〗字段 </asp:ListItem>
                    <asp:ListItem>修改〖车型〗字段</asp:ListItem>
                    <asp:ListItem>修改〖工段|工位|物料类别〗字段</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
    <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
    <div class="divContainer" style="height: 260px;">
        <asp:GridView ID="gvParts" AutoGenerateColumns="False" runat="server" OnPreRender="gvParts_PreRender">
            <Columns>
                <%--<asp:TemplateField HeaderText="最后修改用户">
                    <ItemTemplate>
                        <%# Eval("UserName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="序号" SortExpression="RowNumber">
                    <ItemTemplate>
                        <%# Eval("RowNumber")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="零件号" SortExpression="PartCode">
                    <ItemTemplate>
                        <%# Eval("PartCode")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工厂">
                    <ItemTemplate>
                        <%# Eval("PlantCode")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DUNS">
                    <ItemTemplate>
                        <%# Eval("DUNS")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工段">
                    <ItemTemplate>
                        <%# Eval("Segments")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工位" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <%# Eval("WorkLocation")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="库位" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <%# Eval("Dloc")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="零件名称" SortExpression="PartChineseName">
                    <ItemTemplate>
                        <%# Eval("PartChineseName")%></ItemTemplate>
                    <ControlStyle Width="100px" />
                    <FooterStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="center" Width="100px" />
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FollowUp">
                    <ItemTemplate>
                        <%# Eval("Followup")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车型">
                    <ItemTemplate>
                        <%# Eval("Specs")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料类别">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="车间">
                    <ItemTemplate>
                        <%# Eval("Workshops")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料状态">
                    <ItemTemplate>
                        <%# Eval("StatusName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="70px" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
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
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
