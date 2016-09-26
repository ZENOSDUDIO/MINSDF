<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartDetails.ascx.cs" Inherits="BizDataMaintain_PartDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>


<script type="text/javascript">
    function popupWindow() {
        showDialog('../BizDataMaintain/SupplierSelect.aspx', 625, 365, 'bindReturnValue();');
    }
    function bindReturnValue() {
        var v = getReturnValue();
        var s = v.split('∑');
        document.getElementById('<%= hidSupplierID.ClientID%>').value = unescape(s[0]);
        document.getElementById('<%= txtSupplier.ClientID%>').value = unescape(s[1]);
    }

    function selectWorkshops() {
        var ddlPlantID = $get('<%= ddlPlantID.ClientID%>');
        var plantID = ddlPlantID.options[ddlPlantID.selectedIndex].value;
        if (plantID == '') {
            alert("请先选择工厂！");
           $get('btnSelectWorkShops').disabled = true;
        }
        else
        {
        var selectedWorkshops = $get('<%= txtWorkshops.ClientID%>').value;
        var url='WorkshopsList.aspx?PlantID=' + plantID + '&SelectedWorkshops=' + selectedWorkshops;
        showDialog(url, 600, 350, 'setSelectedWorkshops();');
        }
    }
    
    function setSelectedWorkshops() {
        var selectedWorkshops = getReturnValue();
        if (selectedWorkshops == '') {
            $get('btnSelectSegment').disabled = true;
        }
        else {
            $get('btnSelectSegment').disabled = false;
        }
        $get('<%= txtWorkshops.ClientID%>').value = selectedWorkshops;
        $get('<%= txtSegments.ClientID%>').value = '';
    }

    function selectSegments() {
        var selectedWorkshops = $get('<%= txtWorkshops.ClientID%>').value;
        var ddlPlantID = $get('ctl00_ContentPlaceHolder1_PartDetails1_ddlPlantID');
        var plantID = ddlPlantID.options[ddlPlantID.selectedIndex].value;
        if ((plantID == '') || (selectedWorkshops == '')) {
            alert("请先选择车间！");
            $get('btnSelectSegment').disabled = true;
        }
        else {
            var selectedSegments = $get('<%= txtSegments.ClientID%>').value;
            var url = 'SegmentsList.aspx?SelectedSegments=' + selectedSegments + '&SelectedWorkshops=' + selectedWorkshops;
            showDialog(url, 600, 350, 'setSelectedSegments();');
        }
    }
    
    function setSelectedSegments() {
        var selectedSegments = getReturnValue();
        $get('<%= txtSegments.ClientID%>').value = selectedSegments;
    }

//    function selectSegments() {
//        showDialog('WorkshopsList.aspx', 600, 350, 'getSelectedWorkshops();');
//    }
//    function getSelectedWorkshops() {
//        var selectedWorkshops = getReturnValue();
//        //        var s = v.split('∑');
//        //        document.getElementById('<%= hidSupplierID.ClientID%>').value = unescape(s[0]);
//        //        document.getElementById('<%= txtSupplier.ClientID%>').value = unescape(s[1]);
//        $get('txtWorkshops').value = selectedWorkshops;
//    }

    var gvGroupID = '<%= gvGroup.ClientID%>';    
</script>

<table cellspacing="5" cellpadding="0" width="100%" border="0">
    <asp:HiddenField ID="hidPartID" runat="server" />
    <tr>
        <td colspan="6">
            <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" OnButtonClicked="Toolbar1_ButtonClicked"
                EnableClientApi="true" Width="98%">
                <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                    CssClassSelected="button_selected" />
                <Items>
                    <SCS:ToolbarButton CausesValidation="True" CommandName="save" ImageUrl="~/App_Themes/Images/Toolbar/Save.gif"
                        Text="保存" ValidationGroup="save" />
                    <SCS:ToolbarButton CausesValidation="False" ImageUrl="~/App_Themes/Images/Toolbar/left.gif"
                        Text="返回" CommandName="return" />
                </Items>
            </SCS:Toolbar>
        </td>
    </tr>
    <tr>
        <td >
            零件号
        </td>
        <td>
            <asp:TextBox ID="txtPartCode" runat="server" Width="120px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="零件号不能为空"
                ControlToValidate="txtPartCode" Display="None" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
        </td>
        <td>
            供应商DUNS
        </td>
        <td>
            <asp:HiddenField ID="hidSupplierID" runat="server" />
            <asp:TextBox ID="txtSupplier" runat="server" Width="100px"  onpaste="return false;"
                            onkeypress="return false;"></asp:TextBox>
            &nbsp;<input id="Button1" type="button" value="..." 
                onclick="popupWindow();return false;" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="供应商必选"
                ControlToValidate="txtSupplier" Display="None" SetFocusOnError="True" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2"
                HighlightCssClass="EditError" PopupPosition="BottomLeft">
            </cc1:ValidatorCalloutExtender>
        </td>
        <td nowrap="nowrap">
            物料状态
        </td>
        <td>
            <asp:DropDownList ID="ddlPartStatus" runat="server" Width="120px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPartStatus"
                Display="None" ErrorMessage="物料状态必选" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator7"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            零件中文名
        </td>
        <td>
            <asp:TextBox ID="txtPartChineseName" runat="server" Width="120px"></asp:TextBox>
        </td>
        <td>
            零件英文名
        </td>
        <td>
            <asp:TextBox ID="txtPartEnglishName" runat="server" Width="120px"></asp:TextBox>
        </td>
        <td nowrap="nowrap">
            车型
        </td>
        <td>
            <asp:TextBox ID="txtSpecs" runat="server" Width="120px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            物料类别</td>
        <td>
            <asp:DropDownList ID="ddlCategoryID" runat="server" Width="120px">
            </asp:DropDownList>
        </td>
        <td>
            Follow Up
        </td>
        <td>
            <asp:TextBox ID="txtFollowUp" runat="server" Width="120px"></asp:TextBox>
        </td>
        <td nowrap="nowrap">
            循环盘点级别
        </td>
        <td>
            <asp:DropDownList ID="ddlCycleCountLevel" runat="server" Width="120px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCycleCountLevel"
                Display="None" ErrorMessage="循环盘点级别必选" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator8"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            工厂</td>
        <td>
                    <asp:DropDownList ID="ddlPlantID" runat="server" Width="120px" OnSelectedIndexChanged="ddlPlantID_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="工厂必选"
                ControlToValidate="ddlPlantID" Display="None" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RequiredFieldValidator3"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
            <%--<asp:DropDownList ID="ddlWorkshopID" runat="server" onselectedindexchanged="ddlWorkshopID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
        </td>
        <td>
            &nbsp;车间</td>
        <td>
           <asp:TextBox ID="txtWorkshops" runat="server" Width="100px" onpaste="return false;"
                            onkeypress="return false;">
           </asp:TextBox>
        &nbsp;
        <input id="btnSelectWorkShops" type="button" value="..." 
                onclick="selectWorkshops();" />
        </td>
        <td>
            工段</td>
        <td>
            <asp:TextBox ID="txtSegments" runat="server" Width="100px"  onpaste="return false;"
                            onkeypress="return false;">
           </asp:TextBox>
        &nbsp;
        <input id="btnSelectSegment" onclick="selectSegments();" type="button" value="..." />
        </td>
    </tr>
    <tr>
        
        <td>
            循环盘点次数</td>
        <td >
            <asp:TextBox ID="txtCycleCountTimes" runat="server" Width="120px"></asp:TextBox>
            <cc1:FilteredTextBoxExtender ID="txtCycleCountTimes_FilteredTextBoxExtender" 
                runat="server" Enabled="True" FilterType="Numbers" 
                TargetControlID="txtCycleCountTimes">
            </cc1:FilteredTextBoxExtender>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCycleCountTimes"
                    Display="None" ErrorMessage="必须是整数" SetFocusOnError="True" ValidationExpression="^\d+$"
                    ValidationGroup="save"></asp:RegularExpressionValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RegularExpressionValidator1"
                HighlightCssClass="EditError">
            </cc1:ValidatorCalloutExtender>
        </td><td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>工位</td>
        <td colspan="5">
	            <asp:TextBox ID="txtWorkLocation1" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtWorkLocation2" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtWorkLocation3" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtWorkLocation4" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtWorkLocation5" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtWorkLocation6" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtWorkLocation7" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtWorkLocation8" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtWorkLocation9" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtWorkLocation10" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtWorkLocation11" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtWorkLocation12" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtWorkLocation13" runat="server" Width="50px"></asp:TextBox>
             <asp:TextBox ID="txtWorkLocation14" runat="server" Width="50px">
             </asp:TextBox><asp:TextBox ID="txtWorkLocation15" runat="server" Width="50px">
             </asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>库位</td>
        <td colspan="5">
	            <asp:TextBox ID="txtDloc1" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtDloc2" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtDloc3" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtDloc4" runat="server" Width="50px"></asp:TextBox>
	            <asp:TextBox ID="txtDloc5" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtDloc6" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtDloc7" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtDloc8" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtDloc9" runat="server" Width="50px"></asp:TextBox>
                <asp:TextBox ID="txtDloc10" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtDloc11" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtDloc12" runat="server" Width="50px"></asp:TextBox>
            <asp:TextBox ID="txtDloc13" runat="server" Width="50px"></asp:TextBox>
             <asp:TextBox ID="txtDloc14" runat="server" Width="50px">
             </asp:TextBox><asp:TextBox ID="txtDloc15" runat="server" Width="50px">
             </asp:TextBox>
        </td>
    </tr>
    <span lang="zh-cn">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    
    
    
    
    </span>
    <tr>
        <td>
            描述
        </td>
        <td colspan="5">
            <asp:TextBox ID="txtDescription" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                Width="100%" >
                <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                    <HeaderTemplate>
                    <label style="font-size: small">零件组</label>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="divContainer" style="height:200px">
                        <asp:GridView ID="gvGroup" runat="server" DataKeyNames="GroupID" 
                            onprerender="gvGroup_PreRender"><Columns><asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelect" runat="server" onclick="selectItem('cbSelect', 'cbSelectAll', gvGroupID)"/>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <input type="checkbox" id="cbSelectAll"  onclick="selectAll(gvGroupID,'cbSelect')" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                <asp:BoundField DataField="GroupName" HeaderText="零件组名称" />
                                <asp:BoundField DataField="Description" HeaderText="描述" />
                            </Columns>
                        </asp:GridView></div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </td>
    </tr>
</table>
