<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A_schoolReportUseradmin.aspx.cs" Inherits="Alumni.A_schoolReportUseradmin" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>成绩单</title>
</head>
<body style="background: url('/img/backgroundtexture1.png') repeat;">
    <SCRIPT LANGUAGE="JavaScript">
        function openwin(id) {
            OpenWindow = window.open("zhuizong.aspx?id=" + id, "", "height=600, width=550,toolbar=no ,scrollbars=yes,menubar=no");

        }
        function openwinupdate(id) {
            OpenWindow = window.open("zhuizong_update.aspx?id=" + id, "", "height=600, width=550,toolbar=no ,scrollbars=yes,menubar=no");

        }
　　</SCRIPT> 
    <form id="form1" runat="server">
    <center>
    <div style="width:100%; font-family:微軟正黑體;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" 
            EnablePartialRendering="False">
        </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="height:100px;">
                当前用户：<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#3366CC"></asp:Label>
            </div>
            <div style="text-align:right;">
                <fieldset style=" padding:5px; text-align:center;">
                <legend style="font-size:13px; color:#cc5599;">搜索条件</legend>
                    
                    　　　单据类型：
                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="微軟正黑體" 
                            AutoPostBack="True" 
                        onselectedindexchanged="ddlStatus_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <br />
                    学生手机号/学生学号：
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="TextBoxNormal" Height="24px"></asp:TextBox>
                    <asp:Button ID="ButtonSearch" runat="server" Text="搜索" Font-Names="微軟正黑體" 
                        Height="28px" onclick="ButtonSearch_Click" Width="100px" />
                   
                    <asp:Button ID="ButtonExport" runat="server" Text="下载汇出" Font-Names="微軟正黑體" 
                        Height="28px" onclick="ButtonExport_Click" Width="150px" 
                        ForeColor="#006600" />
                </fieldset>
            </div>

            <table rules="all" border="1" style="width:1520px;"  class="tb1">
            <tr style="height:40px; background-color:#CC5599;width:100%;">
                <td colspan="19" style="text-align:center; ">
                    <span style="color:White; font-size:20px;">成绩单单据后台数据</span>
                </td>
            </tr>
                <tr style="background-color:#6B696B; height:30px; font-size:14px;">
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">单据名称</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">审核状态</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">学号</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">姓名</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">护照英文名</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">成绩单类型</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">申请学年</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">申请学期</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">申请用途</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">申请份数</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">收取方式</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">邮寄地址或邮箱</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">手机号</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">提交时间</span></td>
                
            </tr>
            <!--
                <tr style="background-color:#6B696B; height:30px; font-size:14px;">
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">支付项目ID</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">支付项目</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">开课时间</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">学号</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">学生姓名</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">性别</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">生日</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">国籍/籍贯</span></td>
                <td align="center" style="color:Black; width:10%;"><span style="color:White;">居住地</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">现就读年级</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">现就读学校</span></td>
                <td align="center" style="color:Black; width:6%;"><span style="color:White;">家长姓名</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">联系电话</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">实付金额</span></td>
                <td align="center" style="color:Black; width:4%;"><span style="color:White;">是否住宿</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">交易单号</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">实付时间</span></td>
                <td align="center" style="color:Black; width:8%;"><span style="color:White;">交易状态</span></td>
                <td align="center" style="color:Black; width:5%;"><span style="color:White;">介绍人</span></td>
            </tr>
                -->
            
            <asp:PlaceHolder ID="PlaceHolderList" runat="server" EnableViewState="False"></asp:PlaceHolder>
            </table>

             
           
        
            <table border="0" width="100%">
                <tr>
                    <td align="right" style="font-size:13px; height:20px; width:20%;">&nbsp;</td>
                    <td align="center" style="font-size:13px; height:20px; ">
                        <asp:LinkButton ID="lbtnPrevious" runat="server" CommandName="Previous" 
                                OnClick="NavigationButtonClick" PostBackUrl="#" 
                            EnableViewState="False" Font-Size="14px" Font-Underline="False">&lt;&lt;&lt;Prev 上一頁</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;<asp:Literal ID="LiteralNum" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Next" 
                                OnClick="NavigationButtonClick" PostBackUrl="#" 
                            EnableViewState="False" Font-Size="14px" Font-Underline="False">下一頁 Next&gt;&gt;&gt;</asp:LinkButton>&nbsp;
                    </td>
                    <td align="right" style="font-size:13px; height:20px; width:20%;">
                        Go to Page
                        <asp:DropDownList ID="ddlPageNum" runat="server" 
                            onselectedindexchanged="ddlPageNum_SelectedIndexChanged" 
                            AutoPostBack="True" Font-Names="微軟正黑體">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Literal ID="LiteralJS" runat="server" EnableViewState="False"></asp:Literal>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </center>
    </form>
</body>
</html>
