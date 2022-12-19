<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myorder.aspx.cs" Inherits="Alumni.myorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>我的预约</title>
    <link href="css/app.css" rel="stylesheet" />
</head>
<body class="framework7-root">
    <form id="form1" runat="server" style="display:inline;position:relative;">  
     <div class="views  toolbar-through">
            <div id="homeView" class="view view-main" data-page="order">
                <div class="pages ">
                    <div class="page order navbar-fixed page-on-center" data-page="order">
                        <div class="navbar">
                            <div class="navbar-inner">
                                <div class="left"></div>
                                <div class="center" style="left: 0px;">我的预约</div>
                                <div class="right">
                                </div>
                            </div>
                        </div>
                        <div class="page-content">
                             <div style="text-align:left; padding:10px 0 0 10px;">
                             请输入 学生姓名/您的预约手机号<br />
                                <asp:TextBox ID="txtKeyword" runat="server" CssClass="TextBoxNormal" Height="24px"></asp:TextBox>
                                <asp:Button ID="ButtonSearch" runat="server" Text="搜寻" Font-Names="微軟正黑體" 
                                    Height="28px" onclick="ButtonSearch_Click" Width="100px" /><br />
                             </div>
                            <asp:PlaceHolder ID="PlaceHolderList" runat="server" EnableViewState="False"></asp:PlaceHolder>
                        </div>
                        <div class="toolbar order">
                            <div class="toolbar-inner">
                                <a href="course.aspx" class="item-link">返回</a> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
