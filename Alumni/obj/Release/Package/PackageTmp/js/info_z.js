var s1 = document.getElementById("text_stu_empno");
var s2 = document.getElementById("text_stu_name");
var s3 = document.getElementById("txt_IDcard");
//var s4 = document.getElementById("txt_gread");
var s5 = document.getElementById("txt_IDcard_number");
var s6 = document.getElementById("txt_passportEname");
var s7 = document.getElementById("txt_Newphone");
var s8 = document.getElementById("txt_Hcountry");
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
//s4.onfocus = function () {
//    if (this.value == this.defaultValue)
//        this.value = ''
//};
//s4.onblur = function () {
//    if (/^\s*$/.test(this.value)) {
//        this.value = this.defaultValue;
//        this.style.color = '#aaa'
//    }
//}
//s4.onkeydown = function () {
//    this.style.color = '#333'
//}
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