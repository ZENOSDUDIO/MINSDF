<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalDialog.ascx.cs" Inherits="ModalDialog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    var onDialogOK;
    var onDialogCancel;
    function showDialog(url, width, height, scriptOnOK, scriptOnCancel, x, y) {
        if (url.toString().indexOf("?", 0) > 0) {
            url += '&';
        }
        else {
            url += '?'
        }
        url += 'tmpID=' + Math.random(); 
        if ($get('iframeModal').src != url) {
            $get('iframeModal').src = url;
        }
        var onOK;
        var onCancel;
        if (scriptOnOK != null) {
            onOK = scriptOnOK;
        }
        if (scriptOnCancel != null) {
            onCancel = scriptOnCancel;
        }
        showModal(onOK, onCancel);
        var left;
        if (x != null) {
            left = x;
        }
        else {
            left = Math.round((document.body.offsetWidth - width) / 2) - document.body.scrollLeft;
        }
        var top;
        if (y != null) {
            top = y;
        }
        else {
            top = Math.round((document.body.offsetHeight - height) / 2) - document.body.scrollTop;
        }
        //alert("offsetheight:"+document.body.offsetHeight+";height:" + height + ";width:" + width + ";scrollleft:" + document.body.scrollLeft + ";scrolltop:" + document.body.scrollTop + ";left:" + left + ";top:" + top);
        //Sys.UI.DomElement.setLocation($get('<%= Panel1.ClientID %>'), top, left);
        $get('<%= Panel1.ClientID %>').style.top = top;
        $get('<%= Panel1.ClientID %>').style.left = left;
        $get('<%= Panel1.ClientID %>').style.width = width;
        $get('<%= Panel1.ClientID %>').style.height = height;
        $get('iframeModal').style.height = height - 29;
    }

    function showModal(scriptOnOK, scriptOnCancel) {
        if (scriptOnOK != null) {
            onDialogOK = scriptOnOK;
        }
        if (scriptOnCancel != null) {
            onDialogCancel = scriptOnCancel;
        }
        $find('ModalPopupExtender').show();
    }

    function dialogCancel() {
        if (onDialogCancel != null) {
            eval(onDialogCancel);
        }
        $get('iframeModal').src = "/ECountApp/Common/PageLoading.aspx";
    }

    function dialogOK() {
        if (onDialogOK != null) {
            eval(onDialogOK);
        }
        $get('iframeModal').src = "/ECountApp/Common/PageLoading.aspx";
    }

    function getReturnValue() {
        return $get('hdnReturnValue').value;
    }

    function setReturnValue(value) {
        window.parent.document.getElementById('hdnReturnValue').value = value;
        window.parent.document.getElementById('btnModalDialogOK').click();
    }
    function showWaitingModal() {
        $find('modalPopupWaiting').show();
    }
    function closeWaitingModal() {
        $find('modalPopupWaiting').hide();
    }
</script>

<asp:Label ID="lblShowDialog" Style="display: none" runat="server" Text="Label"></asp:Label>
<cc1:ModalPopupExtender ID="btnShow_ModalPopupExtender" runat="server" CancelControlID="btnModalDialogCancel"
    DynamicServicePath="" Enabled="True" OkControlID="btnModalDialogOK" PopupControlID="Panel1"
    TargetControlID="lblShowDialog" OnCancelScript="dialogCancel();" BehaviorID="ModalPopupExtender"
    OnOkScript="dialogOK();" BackgroundCssClass="ModalPopupBG" RepositionMode="None"
    PopupDragHandleControlID="divTitle">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" Style="display: none; position: absolute;" runat="server"
    BorderColor="#666666" BorderStyle="Ridge" BorderWidth="1px" Width="0" Height="0" BackColor="White">
    <div id="divTitle" class="DialogTitleBar">
        <div><img alt="" src="/ECountApp/App_Themes/Images/Close.gif" id="btnModalDialogCancel"
            style="cursor: hand" /></div></div>
    <div style="overflow: auto; height: 100%">
        <iframe id="iframeModal" scrolling="auto" width="100%" frameborder="no" style="padding: 0px;
            margin: 0px; border-style: none;"></iframe>
        <input id="btnModalDialogOK" value="OK" style="display: none" type="button" />
    </div>
</asp:Panel>
<input id="hdnReturnValue" type="hidden" />
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblShow"
    PopupControlID="divWaiting" BehaviorID="modalPopupWaiting" BackgroundCssClass="ModalPopupBG">
</cc1:ModalPopupExtender>
<asp:Label ID="lblShow" Style="display: none" runat="server" Text="" EnableViewState="False"></asp:Label>
<div id="divWaiting" style="display: none">
    <img alt="" src="../App_Themes/Images/loading.gif" style="width: 214px; height: 15px" /><br>
    <font color="White" size="10" style="font-weight: bold"></font>
</div>
