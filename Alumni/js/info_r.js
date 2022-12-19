var s1 = document.getElementById("Teacher_deptName");
var s2 = document.getElementById("oldSchool_empno");
var s3 = document.getElementById("oldSchool_name");
var s4 = document.getElementById("oldSchool_Lclass");
var s5 = document.getElementById("oldStudent_Phone");
//var s6 = document.getElementById("txt_mm");
//var s7 = document.getElementById("txt_UseFor");
//var s8 = document.getElementById("txt_Copies");
//var s9 = document.getElementById("txt_takeWay");
// var s10 = document.getElementById("txt_SendAdress");
//var s11 = document.getElementById("txt_Email");
//var s12 = document.getElementById("txt_Cphone");
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
