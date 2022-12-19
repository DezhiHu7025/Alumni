<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_myOrder.aspx.cs" Inherits="Alumni.A_myOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>我的表单</title>
     <link href="css/New.css" rel="stylesheet" />
     <link href="css/list.css" rel="stylesheet" />

</head>
<body>
<div id="main">
    <div class="top"><img src="img/logo_XY.png"/></div>
    <form id="form1" runat="server" >  
                       <div  class="contect">
                            <div style="text-align:left; padding:10px 0 0 10px;">
                             请输入填单时登记的学号或手机号：<br />
                                <asp:TextBox ID="txtKeyword" runat="server" CssClass="TextBoxNormal" Height="24px"></asp:TextBox>
                                <asp:Button ID="ButtonSearch" runat="server" Text="搜寻" Font-Names="微軟正黑體" 
                                    Height="28px" onclick="ButtonSearch_Click" Width="100px" /><br />
                            </div>
                            <div id="page1">
                                 
                                     
                                         <asp:PlaceHolder ID="PlaceHolderList" runat="server" EnableViewState="False"></asp:PlaceHolder>
                                     
                                 
                                      
                           </div>
                        </div>
        
                        <div class="butom">
                             
                          <a href="A_New.aspx" class="button">返回</a>
                          
                        </div> 
                   
    </form>
</div>
</body>
</html>
