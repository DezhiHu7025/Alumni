function selectDpList(dp) {
    var sIndex = dp.selectedIndex;//返回选中是第几项 0,1....
    var sText = dp.options[dp.selectedIndex].text;//返回选中的文本--文本1,文本2 ...
    var sValue = dp.value;//返回选中的文本--v1,v2 ...
    if (sValue == "正式") {
        alert('正式成绩单：加盖学校公章、含校长签字，可用于签证、公证、申请学校等，需3-5个工作日开具；仅公对公提供，不对个人提供。')
    } else if (sValue == "非正式") {
        alert('非正式成绩单：无学校公章、无校长签字，仅供家长了解学生在校学习情况，需1-3工作开具，可提供给个人。')
    }
}
function selectDpListWay(dp) {
    var sIndex = dp.selectedIndex;//返回选中是第几项 0,1....
    var sText = dp.options[dp.selectedIndex].text;//返回选中的文本--文本1,文本2 ...
    var sValue = dp.value;//返回选中的文本--v1,v2 ...
    if (sValue == "邮寄") {
        alert('正式成绩单仅接受 公对公 email或邮寄地址（如学校、签证处、公证处等），私人地址不寄送！非正式成绩单不受此限制。')
    } else if (sValue == "邮箱") {
        alert('正式成绩单仅接受 公对公 email或邮寄地址（如学校、签证处、公证处等），私人地址不寄送！非正式成绩单不受此限制。')
    }
}


var s1 = document.getElementById("text_stu_empno");
var s2 = document.getElementById("text_stu_name");
var s3 = document.getElementById("txt_passportEname");
var s4 = document.getElementById("txt_reportCard");
var s5 = document.getElementById("txt_yyyy");
var s6 = document.getElementById("txt_mm");
var s7 = document.getElementById("txt_UseFor");
var s8 = document.getElementById("txt_Copies");
var s9 = document.getElementById("txt_takeWay");
var s10 = document.getElementById("txt_SendAdress");
var s11 = document.getElementById("txt_Cphone");
s1.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s1.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s1.onkeydown = function () {
    this.style.color = '#333'
}
s2.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s2.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s2.onkeydown = function () {
    this.style.color = '#333'
}
s3.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s3.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s3.onkeydown = function () {
    this.style.color = '#333'
}
s4.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s4.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s4.onkeydown = function () {
    this.style.color = '#333'
}
s5.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s5.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s5.onkeydown = function () {
    this.style.color = '#333'
}
s6.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s6.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s6.onkeydown = function () {
    this.style.color = '#333'
}
s7.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s7.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s7.onkeydown = function () {
    this.style.color = '#333'
}
s8.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s8.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s8.onkeydown = function () {
    this.style.color = '#333'
}
s9.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s9.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s9.onkeydown = function () {
    this.style.color = '#333'
}
s10.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s10.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s10.onkeydown = function () {
    this.style.color = '#333'
}

 s11.onfocus = function () {
    if (this.value == this.defaultValue)
        this.value = ''
};
s11.onblur = function () {
    if (/^\s*$/.test(this.value)) {
        this.value = this.defaultValue;
        this.style.color = '#aaa'
    }
}
s11.onkeydown = function () {
    this.style.color = '#333'
}

