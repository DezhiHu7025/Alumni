<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_New_Recruit.aspx.cs" Inherits="Alumni.A_New_Recruit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>校友应聘</title>
    <link href="css/list.css" rel="stylesheet" />
    <link href="css/New.css" rel="stylesheet" />
</head>
<body >
<div id="main">
      <div class="top"><img src="img/logo_XY.png"/></div>
    <form id="form1" runat="server"></form>
   <div class="contect">
                    <h3>KCISEC Alumni Resume Delivery | 康桥校友简历投递</h3>
                    <br />
                        
                        <asp:PlaceHolder ID="PlaceHolderList4" runat="server" EnableViewState="False"></asp:PlaceHolder>
                         
                         <div class="butom">
                         <!--<a href="javascript:history.go(-1)" class="button">返回</a>  |-->
                            <a href="A_New.aspx" class="button">返回 Back 
                            </a> |
                           <a href="http://portal.kcistz.org.cn/Alumni_recruit/Account/LogIn" class="button">投递 Delivery</a>
                        </div>
         
                    
      </div>
</div>               
</body>
</html>

