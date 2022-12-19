<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_info_c.aspx.cs" Inherits="Alumni.A_info_c" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>成绩单申请页面</title>
    
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
               <h3>KCISEC SchoolFellow Transcript Application Page| 康桥校友成绩单申请页面</h3>
                <br />
                  <div id="page1">
                              <asp:HiddenField ID="HiddenField1" runat="server" />  <%--课程ID--%>
                                <asp:HiddenField ID="HiddenField2" runat="server" /> <%-- 课程原始金额--%>
                                <asp:HiddenField ID="HiddenField3" runat="server" /> <%--课程最大报名数量--%>
                                 <asp:HiddenField ID="HiddenField4" runat="server" /> <%--表单名称--%>
                                 <asp:HiddenField ID="HiddenField7" runat="server" /><%--表单编号--%>
                      <p class="kuai">
                        <span class="title_bold">1.康桥学号  KCISEC Student Number&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                        <asp:TextBox ID="text_stu_empno" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                      </p>
                      <br />
                      <p class="kuai">
                        <span class="title_bold">2.中文名 Chinese Name&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                         <asp:TextBox ID="text_stu_name" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                      </p>
                      <br />
                      <p class="kuai">
                        <span class="title_bold">3.护照英文名 Passport English Name&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                        <asp:TextBox ID="txt_passportEname" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                      </p>
                      <br />
                      <p class="kuai">
                        <span class="title_bold">4.成绩单类型 Transcript Type&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please &nbsp;chose&nbsp;|&nbsp;请选择</span><br />
                        <asp:DropDownList  ID="txt_reportCard" runat="server" Width="50%" Font-Size="14px" ForeColor="#666666"  onchange="JavaScript:selectDpList(this)" class="ant-input">
                                    <asp:ListItem value="0" >---请选择成绩单类型---</asp:ListItem>
                                    <asp:ListItem value="正式">正式</asp:ListItem>
                                    <asp:ListItem value="非正式">非正式</asp:ListItem>
                         </asp:DropDownList>
                       </p>
                      <br />
                       <p class="kuai">
                        <span class="title_bold">5.申请学年/学期 Apply School Year/Semester&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please &nbsp;chose&nbsp;|&nbsp;请选择</span><br />
                        <asp:DropDownList ID="txt_yyyy" runat="server" Width="25%" Font-Size="14px" ForeColor="#666666" class="ant-input">
                                                        <asp:ListItem value="0">---请选择申请学年---</asp:ListItem>
                                                        <asp:ListItem value="G6">G6</asp:ListItem>
                                                        <asp:ListItem value="G7">G7</asp:ListItem>
                                                        <asp:ListItem value="G8">G8</asp:ListItem>
                                                        <asp:ListItem value="G9">G9</asp:ListItem>
                                                        <asp:ListItem value="G10">G10</asp:ListItem>
                                                        <asp:ListItem value="G11">G11</asp:ListItem>
                                                        <asp:ListItem value="G12">G12</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="txt_mm" runat="server" Width="25%" Font-Size="14px" ForeColor="#666666" class="ant-input">
                                                        <asp:ListItem value="0">---请选择申请学年---</asp:ListItem>
                                                        <asp:ListItem value="G7">G7</asp:ListItem>
                                                        <asp:ListItem value="G8">G8</asp:ListItem>
                                                        <asp:ListItem value="G9">G9</asp:ListItem>
                                                        <asp:ListItem value="G10">G10</asp:ListItem>
                                                        <asp:ListItem value="G11">G11</asp:ListItem>
                                                        <asp:ListItem value="G12">G12</asp:ListItem>
                                                    </asp:DropDownList>
                       </p>
                      <br />
                       <p class="kuai">
                        <span class="title_bold">6.申请用途 Apply For&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please &nbsp;chose&nbsp;|&nbsp;请选择</span><br />
                        <asp:DropDownList ID="txt_UseFor" runat="server" Width="50%" Font-Size="14px" ForeColor="#666666" class="ant-input">
                                                        <asp:ListItem value="0">---请选择申请用途---</asp:ListItem>
                                                        <asp:ListItem value="签证">签证</asp:ListItem>
                                                        <asp:ListItem value="申请学校">申请学校</asp:ListItem>
                                                        <asp:ListItem value="公正">公正</asp:ListItem>
                                                        <asp:ListItem value="其他">其他</asp:ListItem>
                          </asp:DropDownList>
                       </p>
                      <br />
                       <p class="kuai">
                        <span class="title_bold">7.申请份数 Apply Number Of Copies&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please &nbsp;chose&nbsp;|&nbsp;请选择</span><br />
                       <asp:DropDownList ID="txt_Copies" runat="server" Width="50%" Font-Size="14px" ForeColor="#666666" class="ant-input">
                                                        <asp:ListItem value="0">---请选择申请份数---</asp:ListItem>
                                                        <asp:ListItem value="1">1</asp:ListItem>
                                                        <asp:ListItem value="2">2</asp:ListItem>
                                                        <asp:ListItem value="3">3</asp:ListItem>
                                                        <asp:ListItem value="4">4</asp:ListItem>
                                                        <asp:ListItem value="5">5</asp:ListItem>
                                                    </asp:DropDownList>份
                       </p>
                      <br />
                        <p class="kuai">
                        <span class="title_bold">8.收取成绩单方式 Receiving Mode&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please &nbsp;chose&nbsp;|&nbsp;请选择</span><br />
                       <asp:DropDownList ID="txt_takeWay" runat="server" Width="50%" Font-Size="14px" ForeColor="#666666" class="ant-input" onchange="JavaScript:selectDpListWay(this)">
                                                        <asp:ListItem value="0">---请选择收取方式---</asp:ListItem>
                                                        <asp:ListItem value="邮寄">邮寄</asp:ListItem>
                                                        <asp:ListItem value="邮箱">邮箱</asp:ListItem>
                                                    </asp:DropDownList>
                       </p>
                      <br />
                        <p class="kuai">
                        <span class="title_bold">9.邮寄/邮箱地址 Post/Emails Address&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                      <asp:TextBox ID="txt_SendAdress" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                       </p>
                      <br />
                      <p class="kuai">
                        <span class="title_bold">10.联系电话 Telephone Number&nbsp;&nbsp;<span class="red"></span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                       <asp:TextBox ID="txt_Cphone" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                       </p>
                      <br />
                           </div>
                          
                              
                         <div class="butom">
                             
                         <!--<a href="javascript:history.go(-1)" class="button">返回</a>  |-->
                             <a href="A_New.aspx" class="button">返回</a>  |
                                <asp:Button ID="btn_post" runat="server" Text="提交表单" OnClick="baoming_Click"  class="button" />
                          
                        </div>
               
       
             </ContentTemplate>
        </asp:UpdatePanel>
        </div>
     </form>
 </div>
 <script src="js/info_c.js" ></script>
</body>
</html>

