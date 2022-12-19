
/* --視窗控制 --*/
function CloseWindow() {
    window.open('', '_self', '');
    window.close();
}

function reloadOpener() {
    try {
        //判斷是否為showModalDialog視窗
        if (window.dialogArguments == null) {
            window.opener.location.href = window.opener.location.href;
        }
        else {
            window.dialogArguments.location.href = window.dialogArguments.location.href;
        }
    }
    catch (e) {
        //
    }
}

function WindowResize(width, height) {
    if (screen.availHeight >= 700 && height >= 600) {
        height = 700;
    }
    
    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        window.dialogHeight = height + 'px';
        window.dialogWidth = width + 'px';
    }
    else {
        window.resizeTo(width, height);
    }
}

//最大化視窗
function win_maximization() {
    var width = window.screen.availWidth;
    var height = window.screen.availHeight;
    try {
        window.resizeTo(width, height);
        window.moveTo(0, 0);
    }
    catch (e) {
        return;
    }
}

/* --- 輸入控制 --- */
function SkipNext() {
    if (event.keyCode == 13) {
        {
            event.keyCode = 9;
        }
    }
}

/* --- ui控制 --- */
function hideMoveWindow(itemID) {
    var o = document.getElementById(itemID);
    o.style.display = 'none';
}

function showMoveWindow(itemID) {
    tempX = event.clientX - 80;
    tempY = event.clientY + 15;
    var obj = document.getElementById(itemID);
    obj.style.display = 'block';
    obj.style.pixelLeft = tempX;
    obj.style.pixelTop = tempY;
}

