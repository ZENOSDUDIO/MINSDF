<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StocktakeResultImport.aspx.cs"
    Inherits="SGM.ECountJQ.UPG.Web.Pages.StocktakeResultImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="style/Default.css" />
    <script type="text/javascript" language="javascript" src="Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/popup.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="divError" visible="false">
    </div>
    <div runat="server" id="divMain">
        <table class="EditTable" width="100%" border="0" cellpadding="0" cellspacing="1">
            <tr>
                <td class="TextCell" style="width: 100px; text-align: right;">
                    选择文件上传：
                </td>
                <td class="ControlCell">
                    <asp:FileUpload ID="fuImportFile" runat="server" Width="200px" />&nbsp;<asp:Button
                        ID="btnUpload" runat="server" Text="确定" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr runat="server">
                <td colspan="2" id="tdImportError" visible="false" class="TextCell">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
