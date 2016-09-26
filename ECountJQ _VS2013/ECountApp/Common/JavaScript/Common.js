
var onDialogOK;
var onDialogCancel;

function SetVisibility(objID) {
    var obj = document.getElementById(objID);
    if (obj == null) return false;

    if (obj.style.display != "none") {
        obj.style.display = "none";
    }
    else {
        obj.style.display = "block";
    }
    return false;
}

function closeDialog() {
    window.parent.document.getElementById('btnModalDialogCancel').click();
}

function closeDialogOnSave() {
    alert('保存成功！');
    window.parent.document.getElementById('btnModalDialogCancel').click();
}

function refresh(controlID) {
    $get(controlID).click();
}

function selectAll(containerID,cbSelectID) {
    var e = event.srcElement;
    var grid = document.getElementById(containerID)
    var items = grid.getElementsByTagName("input");

    for (var i = 0; i < items.length; i++) {
        var idx = items[i].id.indexOf(cbSelectID);
        if (items[i].type == "checkbox" && idx>0 && items[i].disabled==false) {            
                items[i].checked = e.checked;
        }
    }

}

function selectItem(cbSelectID, cbSelectAll, containerID) {
    var all = document.getElementById(cbSelectAll);
    var e = event.srcElement;
    if (!e.checked)
        all.checked = false;
    else {
        var grid = document.getElementById(containerID)
        var items = grid.getElementsByTagName("input");
        var count = 0;
        var scount = 0;
        for (var i = 0; i < items.length; i++) {
            var idx = items[i].id.indexOf(cbSelectID);
            
            if (idx>0 && items[i].type == "checkbox") {

                ++count;
                    if (items[i].checked||items[i].disabled==true)
                        ++scount;                
            }
        }
        if (scount == count) {
            all.checked = true;
        }
        else
            all.checked = false;
    }
}
/*function showModal(scriptOnOK, scriptOnCancel) {
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
}

function dialogOK() {
    if (onDialogOK != null) {
        eval(onDialogOK);
    }
}

function getReturnValue() {
    return $get('hdnReturnValue').value;
}

function setReturnValue(value) {
    window.parent.document.getElementById('hdnReturnValue').value = value;
    window.parent.document.getElementById('btnModalDialogOK').click();
}*/