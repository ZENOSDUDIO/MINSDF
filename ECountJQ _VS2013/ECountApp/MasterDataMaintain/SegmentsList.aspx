<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SegmentsList.aspx.cs" Inherits="MasterDataMaintain_SegmentsList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="SCS.Web.UI.WebControls.Toolbar" Namespace="SCS.Web.UI.WebControls"
    TagPrefix="SCS" %>

<%@ Register src="../Common/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="../App_Themes/Default.css" />

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <script type="text/javascript">

        function toolbar_ButtonClicked(sender, eventArgs) {
            var selectedIndex = eventArgs.get_selectedIndex();
            switch (selectedIndex) {
                case 0:
                    //loop for selected items

                    var selectedSegments = '';
                    var gvSegments = $get('gvSegments');
                    var i;
                    for (i = 1; i < gvSegments.rows.length; i++) {
                        if (gvSegments.rows[i].cells[0].childNodes[0].checked) {
                            selectedSegments += ',' + gvSegments.rows[i].cells[1].innerText;

                        }
                    }
                    selectedSegments = selectedSegments.substr(1);
                    setReturnValue(selectedSegments);
                    eventArgs.set_cancel(true);
                    break;
            }
        }    
    </script>
    <div>
     <table width="100%">
            <tr>
                <td>
                     <SCS:Toolbar ID="Toolbar1" runat="server" CssClass="toolbar" EnableClientApi="true"
                        OnClientButtonClick="toolbar_ButtonClicked">
                        <ButtonCssClasses CssClass="button" CssClassEnabled="button_enabled" CssClassDisabled="button_disabled"
                            CssClassSelected="button_selected" />
                        <Items>
                            <SCS:ToolbarButton CommandName="ok" ImageUrl="~/App_Themes/Images/icn_ok.gif"
                                Text="确定" />
                        </Items>
                    </SCS:Toolbar>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="divContainer">
                        <asp:GridView ID="gvSegments" runat="server" AutoGenerateColumns="False" 
                            onprerender="gvSegments_PreRender" onrowdatabound="gvSegments_RowDataBound" >
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SegmentCode" HeaderText="工段代码" />
                                <asp:BoundField DataField="SegmentName" HeaderText="工段名称" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <uc1:ModalDialog ID="ModalDialog1" runat="server" />
    
    </div>
    </form>
</body>
</html>
