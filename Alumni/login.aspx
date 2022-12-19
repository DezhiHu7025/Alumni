<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Alumni.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>线上校友专区</title>
    <link href="css/logincss.css" rel="stylesheet" type="text/css" />
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
<body>
    <form id="form1" runat="server">
<div style="font-family: 微軟正黑體;">
        <center>
            <div style="width:800px;">
                <table width="100%" border="1px" class="tb1">
                    <tr>
                        <td style="text-align:left;">
                            <img alt="" src="img/logo_denglu.png" height="50px" />
                        </td>
                    </tr>
                    <tr style="background-color:#cc5599;">
                        <td style="text-align:left; vertical-align:middle; height:30px; padding-left:5px;" >
                            <span style="font-size:18px; color:White;">
                                线上校友专区
                                &nbsp;
                                <asp:Label ID="lblDEMO" runat="server" Text="( DEMO Site )"></asp:Label>
                            </span>
                        </td>
                    </tr>
                    <tr style="background-color:#ececec;">
                        <td align ="center">
                            <br />
                        <br />
                        <div>
                            <table border="0">
                                <tr>
                                    <td style="text-align:left; font-size:14px;">
                                        帐号 (Account)&nbsp;</td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtAccount" runat="server" 
                                            style="border-style:groove; width:140px; height:20px;" 
                                            AutoCompleteType="LastName" MaxLength="30" CssClass="TextBoxNormal" 
                                            Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; font-size:14px;">
                                        密码 (Password)&nbsp;
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPassword" textmode="Password" runat="server" 
                                            style="border-style:groove; width:140px; height:20px;" MaxLength="20" 
                                            CssClass="TextBoxNormal" Height="25px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; font-size:14px;">
                                        网域 (Domain)&nbsp;
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="DropDownListDomain" runat="server" Font-Names="微軟正黑體">
                                            <asp:ListItem Value="192.168.80.222" Selected="True">KCIS</asp:ListItem>
                                        </asp:DropDownList>     
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </div>
                        </td>
                    </tr>
                    <tr style="background-color:#cc5599">
                        <td style="height:30px; text-align:center; vertical-align:middle;">
                                <asp:Button ID="ButtonLogin" runat="server" Text="登录 (Login)"
                                    onclick="ButtonLogin_Click" CssClass="ButtonC" Width="110px" 
                                    Font-Size="15px" Height="30px" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
    <center>
        <asp:Literal ID="LiteralResult" runat="server" EnableViewState="False"></asp:Literal>
    </center>
    </form>
</body>
</html>
