<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCFileUpload.ascx.cs"
    Inherits="Common_UCFileUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
//    var btnUploadID;
    function uploadError(sender, args) {
//        $get('divResult').style.display = 'list-item';
        $get('waitingModal').hide();
        $get('divResult').innerHTML = "<li>文件上传失败</li>"
    }

    function StartUpload(sender, args) {
        $get('divResult').innerHTML="";//.style.display = 'none'; 
        $find('waitingModal').show();
    }

    function UploadComplete(sender, args) {
//        $get('divResult').style.display = 'list-item'; 
        $get('<%= btnUpload.ClientID %>').click();
    }
    //    function ShowWaitingMsg() {
    //        $find('waitingModal').show();
    //    }

    //    function ShowWaitingMsg() {
    //        $get('waitingModal').show();
    //    }
</script>

<cc1:ModalPopupExtender ID="panelUploading_ModalPopupExtender" runat="server" BackgroundCssClass="ModalPopupBG"
    DynamicServicePath="" Enabled="True" TargetControlID="btnModal" BehaviorID="waitingModal"
    PopupControlID="panelUploading" X="150" Y="200">
</cc1:ModalPopupExtender>
<asp:Button ID="btnModal" runat="server" Style="display: none" />
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
<asp:Button ID="btnUpload" Style="display: none" runat="server" Text="上传" OnClick="btnUpload_Click" />
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<div class="divContainer">
<table>
    <tr>
        <td nowrap="nowrap" valign="middle" style="width: 99px">
            选择文件上传：
        </td>
        <td>
            <cc1:AsyncFileUpload ID="fileImport" runat="server" UploaderStyle="Traditional" Width="500px"
                OnClientUploadComplete="UploadComplete" OnClientUploadError="uploadError" OnClientUploadStarted="StartUpload" />
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap" valign="top" style="width: 99px">
            上传结果：
        </td>
        <td>
            <div id="divInfo" style="height: 100px; overflow: auto">
                <asp:Panel runat="server" Style="background-color: Transparent;display:none" ID="panelUploading" 
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/loading.gif" />&nbsp;正在上传,请稍候......</asp:Panel>
                <div id="divResult">
                    <asp:BulletedList ID="bllResultInfo" runat="server" EnableViewState="False" ForeColor="Black"
                        Width="90%" DisplayMode="HyperLink">
                    </asp:BulletedList>
            </div>
                      </div>
        </td>
    </tr>
</table>
</div>