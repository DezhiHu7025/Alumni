<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkpay.aspx.cs" Inherits="Alumni.checkpay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,user-scalable=no,initial-scale=1,minimum-scale=1,maximum-scale=1" />
    <title>线上校友专区</title>
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
                                <div class="center" style="left: 0px;">温馨提示</div>
                                <div class="right">
                                </div>
                            </div>
                        </div>
                        <div class="page-content">
                            <!-- 提示 -->
                            <div class="content-block">
<%--                                <p><b>退费标准：</b></p><p>1.开营两周前，全额退费。</p><p>2.开营前两周至营队课程进行一半前，依上课日数比例核退80％费用。</p><p>3.课程进行一半后，不再退费。</p><p>4.开营期间请假不退费。</p><p><br /><br /></p>--%>
                                    <p><b>退费标准：</b></p><p>1.开营前五个工作日前退营，退活动总费用的100%；</p><p>2.开营前前五个工作日至营队进行一半， 依未上課日數比例核退费用的80%；</p><p>3.开营后課程进行一半后退营，不予退款；</p><p><br /><br /></p>

                                 <p><b>活动中心咨询电话：</b></p><p> 厉伟莉老师（0512-82696647）</p><p> 邹一帆老师（0512-82696649）</p><p> 毛志超 （0512-82696650）</p>
                            </div>
                        </div>
                        <div class="toolbar order">
                            <div class="toolbar-inner">
                                <a href="javascript:history.go(-1)" class="item-link">放弃</a>  |
                                <a href="info.aspx?id=<%=id%>" class="item-link ">同意<br /></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
