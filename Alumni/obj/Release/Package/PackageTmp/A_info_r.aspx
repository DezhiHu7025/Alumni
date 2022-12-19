<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_info_r.aspx.cs" Inherits="Alumni.A_info_r" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>校友入校申请单</title>
   <link href="css/New.css" rel="stylesheet" />
</head>
<body style="background: url('/img/backgroundtexture1.png') repeat;">
   <div id="main">
          <div class="top"><img src="img/logo_XY.png"/></div>
    <form id="form1" runat="server" >  
         <div class="contect">
          <asp:ScriptManager ID="ScriptManager1" runat="server" 
            EnablePartialRendering="False">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" style="display:inline;position:relative;" runat="server" UpdateMode="Always">
               <ContentTemplate> 
            <h3>KCISEC SchoolFellow  Applicat Page Of Get Into School| 康桥校友入校申请页面</h3>
                <br />        
       
                 <div id="page1">
                              <asp:HiddenField ID="HiddenField1" runat="server" />  <%--课程ID--%>
                                <asp:HiddenField ID="HiddenField2" runat="server" /> <%-- 课程原始金额--%>
                                <asp:HiddenField ID="HiddenField3" runat="server" /> <%--课程最大报名数量--%>
                                 <asp:HiddenField ID="HiddenField4" runat="server" /> <%--表单名称--%>
                                 <asp:HiddenField ID="HiddenField7" runat="server" /><%--表单编号--%>
                <p class="kuai">
                     <span class="title_bold">1.填单教职员 Teacher Name&nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please Confirm&nbsp;&nbsp;|&nbsp;请确认</span><br />
                     <asp:Label ID="fillInTeacher" runat="server" Width="50%" Text="Label" ForeColor="#3366CC" class="ant-input"></asp:Label>
                </p>
                     <p class="kuai" style="display:none">
                     <span class="title_bold">填单教职员AD Teacher Name&nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please Confirm&nbsp;&nbsp;|&nbsp;请确认</span><br />
                     <asp:Label ID="fillInTeacherAccount" runat="server" Width="50%" Text="Label" ForeColor="#3366CC" class="ant-input"></asp:Label>
                </p>
                <br />
                     <p class="kuai">
                     <span class="title_bold">2.填单教职员部门 Teacher Department&nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                      <asp:TextBox ID="Teacher_deptName" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                </p>
                <br />
                     <p class="kuai">
                     <span class="title_bold">3.校友学号  KCISEC Student Number  &nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                      <asp:TextBox ID="oldSchool_empno" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                    </p>
                    <br />
                     <p class="kuai">
                     <span class="title_bold">4.校友中文名 SchoolFellow Chinese Name  &nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                       <asp:TextBox ID="oldSchool_name" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                    </p>
                    <br />
                     <p class="kuai">
                     <span class="title_bold">5.校友离校班级 SchoolFellow Class &nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                       <asp:TextBox ID="oldSchool_Lclass" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                </p>
                <br />
                       <p class="kuai">
                     <span class="title_bold">6.校友入校日期 SchoolFellow Get Into Date&nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please Choose&nbsp;&nbsp;|&nbsp;请选择</span><br />
                       <asp:DropDownList ID="select_year" runat="server" OnSelectedIndexChanged="DownList1_SelectedIndexChanged" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>年
                                                <asp:DropDownList ID="select_month" runat="server" OnSelectedIndexChanged="DownList2_SelectedIndexChanged" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>月
                                                <asp:DropDownList ID="select_day" runat="server" Font-Size="14px" ForeColor="#666666"></asp:DropDownList>日
                </p>
                <br />
                      <p class="kuai">
                     <span class="title_bold">7.校友电话 SchoolFellow Telephone Number &nbsp;&nbsp;<span class="red">*</span></span>
                      <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                        <asp:TextBox ID="oldStudent_Phone" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                </p>
                <br />
                     <p class="kuai">
                     <span class="title_bold">8.其他拜访师长姓名 Other Teacher Name&nbsp;&nbsp;<span class="red"></span></span>
                      <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                         <asp:TextBox ID="otherTeacher_Name" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                </p>
                <br />
                     <p class="kuai">
                     <span class="title_bold">9.其他需求或备注 Other Demand Or Remarks&nbsp;&nbsp;<span class="red"></span></span>
                      <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                         <asp:TextBox ID="remarks_needs" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                </p>
                <br />
     
                         </div>
                    
                          
                          <div class="butom">
                             
                          <a href="javascript:history.go(-1)" class="button">登出</a>  |
                                <asp:Button ID="btn_post_R" runat="server" Text="提交表单" OnClick="baoming_Click"  class="button" />
                          
                        </div>  
                
             </ContentTemplate>
        </asp:UpdatePanel>
            </div>
     </form>
</div>
  <script src="js/info_r.js"></script>
</body>
</html>
