<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_FillIn_R.aspx.cs" Inherits="Alumni.A_FillIn_R" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>入校申请单登录页</title>
    
    <link href="css/style_login.css" rel="stylesheet" />
    <script type="text/javascript">
        function SkipNext() {
            if (event.keyCode == 13) {
                {
                    event.keyCode = 9;
                }
            }
        }
    </script>


</head>
<body style="background: url('~/img/backgroundtexture1.png') repeat;">	

    <div class="message warning">
          
        <div class="inset">		
            <div class="login-head">
                <img class="logo_denglu" src="img/logo_XY.png" />
                <h4>
                    <i class="fa fa-user-o"></i>&nbsp;校友入校申请 &nbsp; <asp:Label ID="lblDEMO" runat="server" Text="( DEMO Site )"></asp:Label>
                </h4>
                <!--<div class="alert-close"></div>-->
            </div>   
    <form id="form1" runat="server" class="form_login">
            <ContentTemplate> 
                    
        
                        
                           
                 <ul>
                    <li><span class="log_txt">账号（Account）:</span></li>
                    <li> <asp:TextBox ID="txtAccount" runat="server" 
                                            class="input_css" size="20"
                                            AutoCompleteType="LastName" MaxLength="30"  
                                            ></asp:TextBox></li>
                    <li><span class="log_txt">密码 (Password):</span></li>
                    <li> <asp:TextBox ID="txtPassword" textmode="Password" runat="server" 
                                           class="input_css" MaxLength="20"  size="20"
                                           ></asp:TextBox>

                    </li>
                     <li><span class="log_txt">网域 (Domain):</span></li>
                    <li>  <asp:DropDownList ID="DropDownListDomain" runat="server" Font-Names="微軟正黑體"  class="input_css">
                                            <asp:ListItem Value="192.168.80.222" Selected="True" >KCIS</asp:ListItem>
                          </asp:DropDownList>  
                    </li>
                </ul>
                <div class="submit">
                     <!--<a href="javascript:history.go(-1)" class="item-link">返回</a>  |-->
                    <asp:Button ID="ButtonLogin" runat="server" Text="登录 (Login)"
                                    onclick="ButtonLogin_Click" class="button_css"  />
                    <div class="clear"></div>
                </div>   
               <asp:Literal ID="LiteralResult" runat="server" EnableViewState="False"></asp:Literal>
             </ContentTemplate>
    </form>
            </div>
    </div>
 <div class="footer">
        <p>
            Copyright © 2019 K.C.I.S.E.C　昆山康桥学校，未经授权禁止转贴、节录标注<br />
            校区地址：江苏省昆山市花桥经济开发区西环路500号<br />
            技术支持/TEL：86-512-82695509
        </p>
    </div>
</body>
</html>

