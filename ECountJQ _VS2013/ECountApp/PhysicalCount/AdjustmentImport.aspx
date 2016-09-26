<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustmentImport.aspx.cs" Inherits="PhysicalCount_AdjustmentImport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Common/UCFileUpload.ascx" tagname="UCFileUpload" tagprefix="uc1" %>
<%@ Register Src="../Common/ModalDialog.ascx" TagName="ModalDialog" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        
    <uc2:ModalDialog ID="ModalDialog1" runat="server" />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/ImportTemplate/AdjustmentImport.xls" Target="self">模板下载</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" valign="middle" >
                        <%--<asp:Button ID="btnUpload" Style="display: none" runat="server" Text="上传" OnClick="btnUpload_Click" />--%>
                        &nbsp;
                        <uc1:UCFileUpload ID="UCFileUpload1" runat="server" />
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td><asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False"
                            EnableViewState="False" OnPreRender="gvItems_PreRender"
                            Width="98%">
                            <Columns>
                                <asp:BoundField DataField="NotificationNo" HeaderText="通知单号" />
                                <asp:BoundField DataField="PartNo" HeaderText="零件号" />
                                <asp:BoundField DataField="PlantNo" HeaderText="工厂代码" />
                                <asp:BoundField DataField="DUNS" HeaderText="供应商DUNS" />
                                <%--<asp:BoundField DataField="TypeName" HeaderText="调整标志" />--%>
                                <asp:BoundField DataField="SLOCID" HeaderText="SAP储区域代码" />
                                <asp:BoundField DataField="AvailableAdjust" HeaderText="Available调整值" />
                                <asp:BoundField DataField="QIAdjust" HeaderText="QI调整值" />
                                <asp:BoundField DataField="BlockAdjust" HeaderText="Block调整值" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel> </div>
    </form>
</body>
</html>
