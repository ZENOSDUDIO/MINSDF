<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Waiting.ascx.cs" Inherits="Common_Waiting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    function showWaitingModal() {
        $find('modalPopupWaiting').show();
    }
    function closeWaitingModal() {
        $find('modalPopupWaiting').hide();
    }
</script>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
    TargetControlID="lblShow" PopupControlID="divWaiting" 
    BehaviorID="modalPopupWaiting" BackgroundCssClass="ModalPopupBG">
</cc1:ModalPopupExtender>
<asp:Label ID="lblShow" style="display:none" runat="server" Text="" EnableViewState="False"></asp:Label>
<div id="divWaiting" style="display:none">
    <img alt="" src="../App_Themes/Images/loading.gif" 
        style="width: 214px; height: 15px" /><BR><font color="White" size="10" style="font-weight: bold">处理中，请稍候......</font></div>


