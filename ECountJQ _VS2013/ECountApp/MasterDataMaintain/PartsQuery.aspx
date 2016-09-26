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
                Text="���" />
            <SCS:ToolbarButton CausesValidation="True" ImageUrl="~/App_Themes/Images/Toolbar/delete.gif"
                Text="ɾ��" ConfirmationMessage="ȷ��ɾ��?" CommandName="delete" />
            <SCS:ToolbarButton CommandName="query" ImageUrl="~/App_Themes/Images/Toolbar/Icon-Search.png"
                Text="��ѯ" />
            <SCS:ToolbarButton CausesValidation="false" Text="����" CommandName="exportAll" ImageUrl="~/App_Themes/Images/Toolbar/Export.gif" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="import" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="����" />
            <SCS:ToolbarButton CausesValidation="True" CommandName="batchUpdate" ImageUrl="~/App_Themes/Images/Toolbar/import.gif"
                Text="�����޸�" />
        </Items>
    </SCS:Toolbar>
           </div>
    <div class="divContainer"style="width:888px;">
        <table  >
            <tr>
                <td>
                    �����
                </td>
                <td>
                    <asp:TextBox ID="txtPartCode" runat="server" Width="108px"></asp:TextBox>
                </td>
                <td>
                    �����������
                </td>
                <td>
                    <asp:TextBox ID="txtPartChineseName" runat="server" Width="115px"></asp:TextBox>
                </td>
                <td>
                    ѭ���̵㼶��
                </td>
                <td>
                    <asp:DropDownList ID="ddlCycleCountLevel" runat="server" Width="108px" AppendDataBoundItems="True">
                        <asp:ListItem>--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    ����޸��û�
                </td>
                <td>
                    <asp:TextBox ID="txtUpdateBy" runat="server" Width="108px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ����
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
                    �������
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryID" runat="server" Width="108px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����״̬
                </td>
                <td>
                    <asp:DropDownList ID="ddlPartStatus" runat="server" Width="108px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    ����
                </td>
                <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlPlantID_CascadingDropDown" runat="server" Category="PlantCode"
                        Enabled="True" ServiceMethod="GetPlantPageMethod" TargetControlID="ddlPlantID"
                        LoadingText="����������...">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    ����
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkshopID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlWorkshopID_CascadingDropDown" runat="server" Category="WorkshopCode"
                        Enabled="True" ParentControlID="ddlPlantID" ServiceMethod="GetWorkshopsPageMethod"
                        TargetControlID="ddlWorkshopID" LoadingText="����������...">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    ����
                </td>
                <td>
                    <asp:DropDownList ID="ddlSegmentID" runat="server" Width="108px">
                    </asp:DropDownList>
                    <cc1:CascadingDropDown ID="ddlSegmentID_CascadingDropDown" runat="server" Category="SegmentCode"
                        Enabled="True" LoadingText="����������..." ParentControlID="ddlWorkshopID" ServiceMethod="GetSegmentsPageMethod"
                        TargetControlID="ddlSegmentID">
                    </cc1:CascadingDropDown>
                </td>
                <td>
                    ��λ
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
                <asp:TemplateField HeaderText="ɾ�����">
                    <HeaderTemplate>
                        <input type="checkbox" id="ChkAll" onclick="checkAll(this);" name="ChkAll" title="ȫѡ/ȫ��ѡ" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ChkSelected" runat="server" onclick="javascript:checkItem(this, 'ChkAll');" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�����" SortExpression="PartCode">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkEditPart_Click" CommandName='<%# Eval("PartCode")%>'
                            CommandArgument='<%# Eval("PartID")%>'><%# Eval("PartCode")%></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����" Visible="False">
                    <ItemTemplate>
                        <%# Eval("PartID")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="���Ӣ������" SortExpression="PartEnglishName" Visible="False">
                    <ItemTemplate>
                        <%# Eval("PartEnglishName")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" Width="0px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�������" SortExpression="PartChineseName">
                    <ItemTemplate>
                        <%# Eval("PartChineseName")%></ItemTemplate>
                    <ControlStyle />
                    <FooterStyle />
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��λ" ItemStyle-Width="50px">
                    <ItemTemplate>
                        <%# Eval("WorkLocation")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px">
                    </ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��λ">
                         <ItemTemplate>
                        <%# Eval("Dloc")%></ItemTemplate>
                    <ControlStyle Width="50px" />
                    <HeaderStyle HorizontalAlign="center" Width="50px" />
                    <ItemStyle Width="50px">
                    </ItemStyle>           
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ѭ���̵㼶��">
                    <ItemTemplate>
                        <%# Eval("LevelName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��ѭ���̵����">
                    <ItemTemplate>
                        <%# Eval("CycleCountTimes")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <%# Eval("Specs")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FollowUp">
                    <ItemTemplate>
                        <%# Eval("Followup")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <%# Eval("PlantCode")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <%# Eval("Workshops")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <%# Eval("Segments")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�������">
                    <ItemTemplate>
                        <%# Eval("CategoryName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����״̬">
                    <ItemTemplate>
                        <%# Eval("StatusName")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DUNS">
                    <ItemTemplate>
                        <%# Eval("DUNS")%></ItemTemplate>
                    <HeaderStyle HorizontalAlign="center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����޸��û�">
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
