<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_New.aspx.cs" Inherits="Alumni.A_New" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <meta name="viewport" content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width, height=device-height, target-densitydpi=device-dpi" />
     <title>线上校友专区</title>
    <link href="http://portal.kcisxa.org.cn/library/images/XA_logo.png" rel="SHORTCUT ICON" />
    <link href="http://portal.kcisxa.org.cn/library/images/XA_logo.png" rel="icon" type="image/png" />
    <meta http-equiv="expires" content="0"/>
    <meta http-equiv="pragma" content="no-cache"/>
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta name="robots" content="none"/>
    <meta name="googlebot" content="noarchive"/>
    <!-- DON'T TOUCH THIS SECTION -->
    <link href="css/owl.carousel.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="css/owl.theme.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="css/colorbox.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="css/ui.totop.css" rel="stylesheet" type="text/css" media="screen,projection" />
    <link href="css/style1.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="css/index.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="css/index/jquery.ui.core.min.css" rel="stylesheet" type="text/css"/>
    <link href="css/index/jquery.ui.theme.min.css" rel="stylesheet" type="text/css"/>
    <link href="css/index/jquery.ui.tabs.min.css" rel="stylesheet" type="text/css"/>
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/owl.carousel.js"></script>
    <script src="js/jquery.colorbox-min.js"></script>
    <script src="js/jquery.ui.totop.js"></script>
    <script src="js/initial.js"></script>
    <script src="js/index.js"></script>
    <script src="js/index/jquery.ui-1.10.4.tabs.min.js"></script>
    <!-- END OF DON'T TOUCH -->  

</head>
<body>
     <div style="text-align:center;align-content:center;width:100%;">
        <!-- header content -->
        <header>



            <!-- 康桥学校图书馆 logo -->
            <div class="logo" style="margin:0px;padding:0px">
 
                    <img alt="康桥学校线上校友专区 :: 昆山校区" src="img/logo_XY2.png" />
                
            </div>

            <a id="menu-icon"></a>
        </header>

        <!-- 分馆照片+搜寻列 -->
        <div id="banner" style="margin:0px;padding:0px">
     
                <img src="img/KS-XY-02.jpg"/>
           
        </div>

        <!-- menu 选单 -->
        <nav id="menu" style='display:none'>

            <span class="hover-line"></span>
        </nav>

        <!-- 内容 -->
        <div  style="margin:0px;padding:0px;text-align:center">
            <div id="hot-service" style="margin-top:5px;padding:0px;text-align:center">
                <h3 class="title">:: Alumni Form Application  校友表单申请:: </h3>
                <ul class="remove-text-nodes" style="text-align:center">
                    <li> 
                        <asp:PlaceHolder ID="PlaceHolderList1" runat="server" EnableViewState="False"></asp:PlaceHolder>

                    </li>
                   
                    <li>
                       <asp:PlaceHolder ID="PlaceHolderList2" runat="server" EnableViewState="False"></asp:PlaceHolder>
                    </li>

                    <li>
                       <asp:PlaceHolder ID="PlaceHolderList3" runat="server" EnableViewState="False"></asp:PlaceHolder>
                    </li>
                   
                    <li>
                        <a href="http://portal.kcistz.org.cn/alumni_association/" title="联络信息更新" target='联络信息更新'>
                            <img style='width:30%;height:30%' alt="联络信息更新" src="img/Links_GoodReads.png" />
                            <p>Alumni Information<br/>校友联络信息更新</p>
                        </a>
                    </li>
                    <li>
                        <a href="A_myOrder.aspx" title="我的单据" >
                            <img style='width:30%;height:30%' alt="联络信息更新" src="img/Links_MagazineList.png" />
                            <p>My FormList<br/>我的单据</p>
                        </a>
                    </li>
                    <!--
                        <li>
                         <asp:PlaceHolder ID="PlaceHolderList4" runat="server" EnableViewState="False"></asp:PlaceHolder>
                    </li>校友应聘
                        -->
                    


                </ul>
            </div>

            <div class="clear"></div>

        </div>
        <!-- 内容 End -->
    </div>
 

    <!-- footer -->
    <footer>

        <!-- Webmap -->
        <div id="web-map">
            
        <p style="text-align:center">
          
        </p>
        </div>
        <!-- Webmap End -->
        <!-- Copyright -->
        <!-- Copyright End -->
    </footer>
            <!--            <div class="toolbar tabbar">
                            <div class="toolbar-inner">
                                <a href="A_New.aspx" class="tab-link ajax ">
                                    <i class="icon icon-class"></i>申请表单
                                </a>
                                |
                                <a href="A_myOrder.aspx" class="tab-link ajax">
                                    <i class="icon icon-me"></i>我的表单
                                </a>
                            </div>
                        </div>
                   </div>
           </div>-->
  
</body>
</html>

