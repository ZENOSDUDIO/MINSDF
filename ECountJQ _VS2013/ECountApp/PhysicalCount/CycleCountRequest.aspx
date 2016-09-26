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
                                    Text="Cycle Count ���뵥Ԥ��" />
                                <SCS:ToolbarButton CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                                    Text="Cycle Count ���뵥����" DisabledImageUrl="~/App_Themes/Images/Toolbar/Save.gif" Visible="False" />
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
                                        ���Cycle Count�ܴ�����
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTotal" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        ��������Cycle Count������
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCompleted" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        ʣ��Cycle Count������
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblRest" runat="server" ForeColor="#0066FF"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        ƽ��ÿ��Cycle Count�������
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
                                    <asp:TemplateField HeaderText="�����" SortExpression="PartCode">
                                        <ItemTemplate>
                                            <%# Eval("PartCode")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="���Ӣ������" SortExpression="PartEnglishName">
                                        <ItemTemplate>
                                            <%# Eval("PartEnglishName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="�����������" SortExpression="PartChineseName">
                                        <ItemTemplate>
                                            <%# Eval("PartChineseName")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="����">
                                        <ItemTemplate>
                                            <%# Eval("PlantCode")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="����" DataField="Workshops" />
                            <asp:BoundField HeaderText="����" DataField="Segments" />
                                    <asp:TemplateField HeaderText="��λ">
                                        <ItemTemplate>
                                            <%# Eval("WorkLocation")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ѭ���̵㼶��">
                                        <ItemTemplate>
                                            <%# Eval("LevelName")%></ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField HeaderText="��ѭ���̵����" DataField="CycleCountTimes" />                                    
                                    <asp:TemplateField HeaderText="����">
                                        <ItemTemplate>
                                            <%# Eval("Specs")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Followup">
                                        <ItemTemplate>
                                            <%# Eval("Followup")%></ItemTemplate>
                                    </asp:TemplateField>                                    
                            <asp:BoundField HeaderText="DUNS" DataField="DUNS" />
                            <asp:BoundField HeaderText="��Ӧ������" DataField="SupplierName" />
                                    <asp:TemplateField HeaderText="�������">
                                        <ItemTemplate>
                                            <%# Eval("CategoryName")%></ItemTemplate>
                                    </asp:TemplateField>
                            <asp:BoundField HeaderText="����״̬" DataField="StatusName" />
                        
                                    <asp:TemplateField HeaderText="��ע">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ɾ��">
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
                                                <asp:TemplateField HeaderText="���뵥��">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkRequestNo" runat="server" Text='<%# Eval("RequestNumber") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        ���뵥��
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="��������">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatic" runat="server" Text='<%# Convert.ToBoolean(Eval("IsStatic"))?"��̬":"��̬" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        ��������
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="����">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPlantCode" runat="server" Text='<%# Eval("Plant.PlantCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="������">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestBy.UserName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="����ʱ��">
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
