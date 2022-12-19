<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_New_d1.aspx.cs" Inherits="Alumni.A_New_d1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>校友入校申请</title>
    <link href="css/list.css" rel="stylesheet" />
    <link href="css/New.css" rel="stylesheet" />
</head>
<body >
<div id="main">
      <div class="top"><img src="img/logo_XY2.png"/></div>
    <form id="form1" runat="server"></form>
   <div class="contect">
                    <h3>KCISEC Alumni entering school notice | 康桥校友入校申请须知</h3>
                    <br />
                        
                        <asp:PlaceHolder ID="PlaceHolderList" runat="server" EnableViewState="False"></asp:PlaceHolder>
                         
                         <div class="butom">
                         <!--<a href="javascript:history.go(-1)" class="button">返回</a>  |-->
                            <a href="A_New.aspx" class="button">返回 Back 
                            </a> |
                           <a href="A_FillIn_R.aspx?id=<%=id%>" class="button">教职员申请 Apply by faculty</a>
                        </div>
         
                    
      </div>
</div>               
</body>
</html>
