<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="Alumni.info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>线上校友专区</title>
    <link href="css/app.css" rel="stylesheet" />
</head>

<body class="framework7-root">
    <form id="form1" runat="server" style="display:inline;position:relative;">  
          <asp:ScriptManager ID="ScriptManager1" runat="server" 
            EnablePartialRendering="False">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" style="display:inline;position:relative;" runat="server" UpdateMode="Always">
               <ContentTemplate> 
        <div class="views  toolbar-through">
            <div id="homeView" class="view view-main" data-page="order">
                <div class="pages ">
                    <div class="page order navbar-fixed page-on-center" data-page="order">
                        <div class="navbar">
                            <div class="navbar-inner">
                                <div class="left"></div>
                                <div class="center" style="left: 0px;">请填写信息</div>
                                <div class="right">
                                </div>
                            </div>
                        </div>
                        <div class="page-content">
                            <div class="list-block">
                              <asp:HiddenField ID="HiddenField1" runat="server" />  <%--课程ID--%>
                                <asp:HiddenField ID="HiddenField2" runat="server" /> <%-- 课程原始金额--%>
                                <asp:HiddenField ID="HiddenField3" runat="server" /> <%--课程最大报名数量--%>
                                 <asp:HiddenField ID="HiddenField4" runat="server" /> <%--课程名称--%>
                                 <asp:HiddenField ID="HiddenField7" runat="server" /><%--课程编号--%>
                                <ul>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">学生姓名:</div>
                                            <div class="item-after">
                                                <asp:TextBox ID="text_stu_name" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">性别:</div>
                                            <div class="item-after">
                                                 <asp:RadioButton ID="RadioButton1" GroupName="sex" runat="server" Width="18px" />&nbsp;<span style="font-size:14px; line-height:34px; color:Gray;">男生</span>&nbsp;&nbsp;
                                                 <asp:RadioButton ID="RadioButton2" GroupName="sex" runat="server" Width="18px" />&nbsp;<span style="font-size:14px; line-height:34px; color:Gray;">女生</span>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">生日:</div>
                                            <div class="item-after">
                                                <asp:DropDownList ID="drp_year" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>年
                                                <asp:DropDownList ID="drp_month" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>月
                                                <asp:DropDownList ID="drp_day" runat="server" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>日
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title label">国籍/籍贯:</div>
                                           <div class="item-after">
                                                 <asp:TextBox ID="txt_country" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                            </div>
                                        </div>
                                        </li>
                                        <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">居住地:</div>
                                                <div class="item-after">
                                                     <asp:TextBox ID="txt_add" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">现就读年级:</div>
                                                <div class="item-after" >
                                                    <%--<asp:TextBox ID="txt_gread" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="drp_gread" runat="server" Width="100%" Font-Size="14px" ForeColor="#666666">
                                                        <asp:ListItem value="0">---请选择在读年级---</asp:ListItem>
                                                        <asp:ListItem value="1">一年级G1</asp:ListItem>
                                                        <asp:ListItem value="2">二年级G2</asp:ListItem>
                                                        <asp:ListItem value="3">三年级G3</asp:ListItem>
                                                        <asp:ListItem value="4">四年级G4</asp:ListItem>
                                                        <asp:ListItem value="5">五年级G5</asp:ListItem>
                                                        <asp:ListItem value="6">六年级G6</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">现就读学校:</div>
                                                <div class="item-after">
                                                    <asp:TextBox ID="txt_school" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">家长姓名:</div>
                                                <div class="item-after">
                                                    <asp:TextBox ID="txt_parents" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">联系电话:</div>
                                                <div class="item-after">
                                                    <asp:TextBox ID="txt_phone" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                    <li class="item-content">
                                            <div class="item-inner">
                                                <div class="item-title">介绍人:</div>
                                                <div class="item-after">
                                                    <asp:TextBox ID="txt_introducer" runat="server" Width="100%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray"></asp:TextBox>
                                                </div>
                                            </div>
                                        </li>
                                </ul>      
                            </div>
                            <div class="list-block">
                                <ul>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">支付项目:</div>
                                            <div class="item-after" style="color: #920783;">
                                                <asp:Label ID="lbl_course" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">开课时间:</div>
                                            <div class="item-after" style="color: #920783;">
                                                <asp:Label ID="lbl_time" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content" id="Iszhusu" runat="server" >
                                        <div class="item-inner">
                                            <div class="item-title">是否选择住宿<span style="font-size:12px;">（一周费用500元）</span>:</div>
                                            <div class="item-after" style="color: #920783;">
                                                  <asp:DropDownList ID="dbl_Iszhusu" runat="server" Font-Names="微軟正黑體" 
                                                        AutoPostBack="True" 
                                                    onselectedindexchanged="dbl_Iszhusu_SelectedIndexChanged" Font-Size="14px" ForeColor="Red">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">实付金额:</div>
                                            <div class="item-after" style="color: #920783;">
                                               <%-- ¥666--%><asp:Label ID="lbl_fee" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                    <label class="label-radio item-content">
                                        <input type="radio" value="" checked="checked" />
                                        <div class="item-inner">
                                            <div class="item-title">微信支付</div>
                                        </div>
                                    </label>
                                </ul>
                            </div>
                            <!-- 提示 -->
                           <%-- <div class="content-block-title">温馨提示：</div>
                            <div class="content-block">
                                <p>1、咨询电话：0512-82696647 厉老师；0512-82696648 唐老师。</p>
                                <p>2、预定成功后，课程开始前取消预定的，退款80%的培训费用；开课后取消预定的，不予退费。</p>
                                <p>3、请确定您填写的资料准确无误。</p>
                                <p>4、您可通过查看“我的预约”中查看支付的课程。</p>
                            </div>--%>
                        </div>
                        <div class="toolbar order">
                            <div class="toolbar-inner">
                                <a href="javascript:history.go(-1)" class="item-link">返回</a>  |
                                <asp:Button ID="btn_post" runat="server" Text="提交订单" OnClick="baoming_Click"  BorderWidth="0px" BackColor="#920783" BorderColor="#920783" BorderStyle="None" CssClass="item-link" Font-Size="14px" ForeColor="White" Width="50%" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                   
             </ContentTemplate>
        </asp:UpdatePanel>
     </form>
  <script>
      var s1 = document.getElementById("text_stu_name");
      var s2 = document.getElementById("txt_country");
      var s3 = document.getElementById("txt_add");
      //var s4 = document.getElementById("txt_gread");
      var s5 = document.getElementById("txt_school");
      var s6 = document.getElementById("txt_parents");
      var s7 = document.getElementById("txt_phone");
      var s8 = document.getElementById("txt_introducer");
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
</script>
</body>
</html>

