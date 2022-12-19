<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="course.aspx.cs" Inherits="Alumni.course" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>线上校友专区</title>
    <link href="css/app.css" rel="stylesheet" />
</head>
<body class="framework7-root">
    <form id="form1" runat="server"></form>
                <div class="views  toolbar-through">
                    <div id="homeView" class="view view-main" data-page="course">
                         <div class="pages ">
                             <div class="page course page-on-center" data-page="course">
                                 <div class="page-content">
        <div class="tabs">
            <div id="tab2" class="tab active">
                <div class="tabs">
                    <div id="tabclass2" class="tab tabclass2 active">
                        <div class="list-block media-list course-card">
                           <div style="background: #920783; padding-bottom:8px;padding-top:2px;">
                          <div>
                                     <p ><img src="img/logo.png"  style="width:45%; height:70%"  />
                                         <a class="dantou">线上校友专区</a>
                                     </p>
                                </div>
                                
						   </div>
                               <div >
                                
                                <table width="100%" border="1px" class="tb1">
                    <tr>
                        <td style="text-align:left;padding-top:100px;">
                            *教职员须知：<br />
                            1、请教职员至校友专区填写「校友入校申请」表单<br />
                            2、必须有教职员全程陪同，若想拜访其他师长，则需陪同交接至其他师长，并告知此规范。<br />
                            3、请务必将讯息填写完整，若有其他欲拜访老师，也一并填写至表单中。<br />
                            4、有入班需求者，请与师长事先提出，如未在事前申请上获得核准而临时要求入班， 师长可直接婉拒。<br /> 
                            5、提醒校友须全程遵守校规，勿打扰日常教学。
                        </td>
                    </tr>
                    <tr style="background-color:#cc5599;">
                        <td style="text-align:left; vertical-align:middle; height:30px; padding-left:5px;" >
                            <span style="font-size:18px; color:White;">
                                填写入校申请单登录页
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
                                        222
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; font-size:14px;">
                                        密码 (Password)&nbsp;
                                    </td>
                                    <td style="text-align:left;">
                                       22
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; font-size:14px;">
                                        网域 (Domain)&nbsp;
                                    </td>
                                    <td style="text-align:left;">
                                        333  
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
                                3333
                        </td>
                    </tr>
                </table>
                           
						   </div>
                             <ul>
                            <asp:PlaceHolder ID="PlaceHolderList" runat="server" EnableViewState="False"></asp:PlaceHolder>
                            </ul>
                       </div>
                    </div>
                </div>
            </div>
            
        </div>
  </div>

                        <div class="toolbar tabbar">
                            <div class="toolbar-inner">
                                <a href="course.aspx" class="tab-link ajax active">
                                    <i class="icon icon-class"></i>课程
                                </a>

                                <a href="myorder.aspx" class="tab-link ajax">
                                    <i class="icon icon-me"></i>我的
                                </a>
                            </div>
                        </div>
                   </div>
           </div>
  
</body>
</html>
