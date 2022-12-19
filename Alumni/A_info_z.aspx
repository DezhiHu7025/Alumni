<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_info_z.aspx.cs" Inherits="Alumni.A_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>在读/转出证明申请页面</title>
   
    <link href="css/New.css" rel="stylesheet" />
</head>

<body style="background: url('/img/backgroundtexture1.png') repeat;">
       <div id="main">
          <!--<div class="top"><img src="img/logo_denglu.png"/></div>-->
           <div class="top"><img src="img/logo_XY.png"/></div>
    <form id="form1" runat="server" >
        <div class="contect">
          <asp:ScriptManager ID="ScriptManager1" runat="server" 
            EnablePartialRendering="False">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" style="display:inline;position:relative;" runat="server" UpdateMode="Always">
               <ContentTemplate> 
             <h3>KCISEC SchoolFellow  IN / Transfer Out Certificate Application Page| 康桥校友在读/转出证明申请页面</h3>
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
                        <span class="title_bold">3.证件类型 Type Of ID&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please Choose&nbsp;&nbsp;|&nbsp;请选择</span><br />
                          <asp:DropDownList ID="txt_IDcard" runat="server" Width="50%" Font-Size="14px" ForeColor="#666666" class="ant-input">
                                                        <asp:ListItem value="0">---请选择证件类型---</asp:ListItem>
                                                        <asp:ListItem value="身份证">身份证</asp:ListItem>
                                                        <asp:ListItem value="台胞证">台胞证</asp:ListItem>
                                                        <asp:ListItem value="居住证">居住证</asp:ListItem>
                         </asp:DropDownList>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">4.证件号 ID Number&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                          <asp:TextBox ID="txt_IDcard_number" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">5.护照英文名 English Name Of Passport &nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                          <asp:TextBox ID="txt_passportEname" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">6.户籍地 Registered Residence&nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                         <asp:TextBox ID="txt_Hcountry" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">7.邮寄地址 Send By Post Address&nbsp;&nbsp;</span>
                        <br /><span class="hui">Please write in&nbsp;Chinese&nbsp;|&nbsp;请用中文填写</span><br />
                         <asp:TextBox ID="txt_adress" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox><br /><span class="red">*(提示：邮寄到付)</span>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">8.邮箱 Email &nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                        <asp:TextBox ID="txt_Email" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
                 </p>
                 <br />
                <p class="kuai">
                        <span class="title_bold">9.联系电话 Telephone Number &nbsp;&nbsp;<span class="red">*</span></span>
                        <br /><span class="hui">Please write in&nbsp;&nbsp;|&nbsp;请填写</span><br />
                        <asp:TextBox ID="txt_Newphone" runat="server" Width="50%" BorderWidth="0px" Font-Size="14px" ForeColor="Gray" class="ant-input"></asp:TextBox>
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
  <script src="js/info_z.js"></script>
</body>
</html>

