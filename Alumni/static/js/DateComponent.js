;
(function (root, factory) {
	//amd
	if (typeof define === 'function' && define.amd) {
		define(['jquery'], factory);
	} else if (typeof exports === 'object') { //umd
		module.exports = factory($);
	} else {
		root.DateComponent = factory(window.Zepto || window.jQuery || $);
	}
})(window, function ($) {
  var DateComponent = function(defaultValues) {
    this.date = defaultValues.defaultDate || new Date();
    this.currentSelectedMonth = this.date.getMonth();  //当前显示的月份
    this.currentSelectedYear = this.date.getFullYear(); //当前显示的年份
    this.currentSelectedDate = this.date.getDate(); //当前选中的日期
    this.monthDay = [31, 28 + this.isLeap(this.currentSelectedYear), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];  //数组中的每一项代表每个月的天数
    this.dateBoardMask = null;
    this.callback = defaultValues.callback || null;
    this.init();
  }
  
  DateComponent.prototype = {
    constructor : DateComponent,   //必须指定constructor属性，否则原型链会被切断
    dateBoardMaskTpl:  '<div class="date-mask">'+
                          '<div id="date-view">'+
                              '<div style="padding:10px 18px;text-align:center;border-bottom:1px solid lightgrey;color:lightgrey">'+
                                  '<i class="glyphicon glyphicon-chevron-left" style="float:left"></i>'+
                                  '<span id="headTitle" style="color:#313f52">2018-08-10</span>'+
                                  '<i class="glyphicon glyphicon-chevron-right" style="float:right"></i>'+
                              '</div>'+
                              '<table id = "dateBoardTitle" style="width:100%;text-align:center;">'+
                                  '<tr><td>日</td><td>一</td><td>二</td><td>三</td><td>四</td><td>五</td><td>六</td></tr>'+
                              '</table>'+
                              '<div style="padding-bottom:10px;background-color:#f9f9f9">'+
                                  '<table id ="dateBoard" style="width:100%;text-align:center;"></table>'+
                              '</div>'+
                              '<div style="text-align: center;color:white">'+ 
                                  '<div id="date-cancel" class="date-btn" style="float:left;background-color:black">取消</div>'+
                                  '<div id="date-confirm" class="date-btn" style="float:right;background-color:red">确定</div>'+
                              '<div>'+
                          '</div>'+
                      '</div>',
    getPrevMonth : function () {  //获取上一个月
      if(this.currentSelectedMonth === 0){
        this.currentSelectedYear -= 1;
        this.currentSelectedMonth = 11;
      }else{
        this.currentSelectedMonth -= 1;
      }
      var days = this.monthDay[this.currentSelectedMonth];
      if(this.currentSelectedDate > days){
        this.currentSelectedDate = days;
      }
      this.renderDateBoard(this.currentSelectedYear,this.currentSelectedMonth,this.currentSelectedDate);
    },
    getNextMonth : function () {  //获取下一个月
      if(this.currentSelectedMonth === 11){
        this.currentSelectedYear += 1;
        this.currentSelectedMonth = 0;
      }else{
        this.currentSelectedMonth += 1;
      }
      var days = this.monthDay[this.currentSelectedMonth];
      if(this.currentSelectedDate > days){
        this.currentSelectedDate = days;
      }
      this.renderDateBoard(this.currentSelectedYear,this.currentSelectedMonth,this.currentSelectedDate);
    },
    isLeap : function(year){  //判断是否为闰年
      let res;
      return ((year % 100 == 0) ? res = (year % 400 == 0 ? 1 : 0) : res = (year % 4 == 0) ? 1 : 0);
    },
    renderDateBoard : function(year,month,date){
      $('#headTitle').html(year + '年' + (month + 1) + '月');
      var tempDate = new Date(year, month,1);
      var day = tempDate.getDay();
      var board_html = '';
      var i = 0;
      var arr = [];
      for( i = 0; i < day; i++){
              arr.push('<td></td>');
      }
      for(i = 1 ; i < this.monthDay[month] + 1; i++){
        arr.push('<td align="center"><div id="'+year+month+i+'">'+i+'</div></td>');
      }
      for( i = 0, len = 7 - arr.length % 7; i < len && len !== 7; i++){
        arr.push("<td></td>");
      }
      for( i = 0 ; i < arr.length; i ++){
        if(i === 0){
          board_html += '<tr>' + arr[i];
        }else if(i === arr.length -1){
          board_html += arr[i] + '</tr>'
        }else if(i % 7 === 6){
          board_html += arr[i] + '</tr><tr>'
        }else{
          board_html += arr[i]
        }
      }
      $("#dateBoard").html(board_html);
      $('#'+year+month+date).addClass('date-selected');
    },
    bindEvents : function(){
      var _this = this;
      $(".glyphicon-chevron-left").click(function(){
        _this.getPrevMonth();	
      })
      $(".glyphicon-chevron-right").click(function(){
        _this.getNextMonth();
      })
      $("#dateBoard").click(function(event){
        var target = $(event.target);
        var id = target.context.id;
        if(!target.is('#dateBoard td div') || target.hasClass('date-selected')){return};// 如果点中的是已经选中的日期或者点击的区域无效，责忽略
        $('.date-selected').removeClass('date-selected');
        target.addClass('date-selected');
        _this.currentSelectedDate = parseInt($('#'+id).html())
      })
      $(" #date-cancel").click(function(){
        _this.cancel();
      })
      $(" #date-confirm").click(function(){
        _this.confirm();
      })
      this.dateBoardMask.click(function(event){
        var target = $(event.target);
        if(target.is('.date-mask')){
          _this.cancel();
        }
      })
    },
    init : function(){
       console.log('inint');
       if(!this.dateBoardMask){
         this.dateBoardMask = $(this.dateBoardMaskTpl);
         $('body').append(this.dateBoardMask);
       }
       this.dateBoardMask.toggleClass('date-mask-display')
       this.renderDateBoard(this.currentSelectedYear,this.currentSelectedMonth,this.currentSelectedDate);
       this.bindEvents();
    },
    confirm : function(){
       if(this.callback){
         this.callback(this.currentSelectedYear,this.currentSelectedMonth,this.currentSelectedDate);
       }
       this.cancel();
    },
    cancel : function(){
       this.dateBoardMask.toggleClass('date-mask-display')
       this.dateBoardMask.remove();
       this.dateBoardMask = null;
    }
  }
  return DateComponent;
});