<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showFor_Z.aspx.cs" Inherits="Alumni.showFor_Z" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>在读/转出证明审核页面</title>
    <link href="css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="views  toolbar-through">
        <div id="homeView" class="view view-main" data-page="order">
           <div class="pages ">
              
                        <div class="navbar">
                            <div class="navbar-inner">
                                <div class="left"></div>
                                <div class="center" style="left: 0px;">请审核该单据信息</div>
                                <div class="right">
                                </div>
                            </div>
                        </div>
            <div class="page-content">
                   <div class="list-block">
                                <ul>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">表单名称:</div>
                                            <div class="item-after" style="color: #920783;">
                                                <asp:Label ID="lbl_form_name" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">学号:</div>
                                            <div class="item-after" style="color: #920783;">
                                                <asp:Label ID="lbl_stunum" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">审核状态:</div>
                                            <div class="item-after" style="color: #920783;">
                                                <asp:Label ID="lbl_opinion" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </li>
                                   
                                </ul>
                            </div>
                    <div class="list-block">
                                <ul>
                                    <li class="item-content">
                                        <div class="item-inner">
                                            <div class="item-title">审核意见:</div>
                                            <div class="item-after">
                                                 <asp:RadioButton ID="RadioButton1" GroupName="option" runat="server" Width="18px" />&nbsp;<span style="font-size:14px; line-height:34px; color:Gray;">审核通过</span>&nbsp;&nbsp;
                                                 <asp:RadioButton ID="RadioButton2" GroupName="option" runat="server" Width="18px" />&nbsp;<span style="font-size:14px; line-height:34px; color:Gray;">审核不通过</span>
                                            </div>
                                        </div>
                                    </li>
                                     
                                </ul>
                            </div>
               
            </div>
       
            <div class="toolbar order">
             <div class="toolbar-inner">
                   <a href="javascript:history.go(-1)" class="item-link">返回</a>  |
                   <asp:Button ID="btn_post" runat="server" Text="提交审核结果" OnClick="review_Click"  BorderWidth="0px" BackColor="#920783" BorderColor="#920783" BorderStyle="None" CssClass="item-link" Font-Size="14px" ForeColor="White" Width="50%" />
            </div>
            </div>
           </div>
       </div>
    </div>

    </form>
</body>
</html>
