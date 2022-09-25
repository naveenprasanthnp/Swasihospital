//---------------------AlertMessage---------------------------
function ShowSuccessMessage(msgcontent) {
    Lobibox.notify('success', {
        size: 'mini',
        rounded: true,
        delayIndicator: false,
        msg: msgcontent
    });
}

function ShowInfoMessage(msgcontent) {
    Lobibox.notify('info', {
        size: 'mini',
        rounded: true,
        delayIndicator: false,
        msg: msgcontent
    });
}

function ShowWarningMessage(msgcontent) {
    Lobibox.notify('warning', {
        size: 'mini',
        rounded: true,
        delayIndicator: false,
        msg: msgcontent
    });
}

function ShowErrorMessage(msgcontent) {
    Lobibox.notify('error', {
        size: 'mini',
        rounded: true,
        delayIndicator: false,
        msg: msgcontent
    });
}
//-------------------------------------------------
