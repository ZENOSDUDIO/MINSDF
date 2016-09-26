<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlantDetails.aspx.cs" Inherits="MasterDataMaintain_PlantDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="UserControl/PlantDetails.ascx" TagName="PlantDetails" TagPrefix="uc1" %>
<%--<%@ Register src="../Common/ModalDialog.ascx" tagname="ModalDialog" tagprefix="uc2" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Default.css" rel="stylesheet" type="text/css" />
    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <uc1:PlantDetails ID="PlantDetails1" runat="server" />
    </div>
   <%-- <uc2:ModalDialog ID="ModalDialog1" runat="server" />--%>
    </form>
</body>
</html>
