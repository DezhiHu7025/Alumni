<%@ Page   EnableViewStateMac="false" Language="C#" AutoEventWireup="true" CodeBehind="Alumniuseradmin.aspx.cs" Inherits="Alumni.Alumniuseradmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>线上校友专区</title>
</head>
<body>
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
            <div style="text-align:left;">
                <fieldset style="width:900px; padding:5px; text-align:left;">
                <legend style="font-size:13px; color:#cc5599;">搜尋條件</legend>
                    
                    　　　课程：
                    <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="微軟正黑體" 
                            AutoPostBack="True" 
                        onselectedindexchanged="ddlStatus_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <br />
                    学生姓名/家长手机号/交易单号：
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="TextBoxNormal" Height="24px"></asp:TextBox>
                    <asp:Button ID="ButtonSearch" runat="server" Text="搜尋" Font-Names="微軟正黑體" 
                        Height="28px" onclick="ButtonSearch_Click" Width="100px" />
                    <asp:Button ID="ButtonExport" runat="server" Text="匯出资料" Font-Names="微軟正黑體" 
                        Height="28px" onclick="ButtonExport_Click" Width="150px" 
                        ForeColor="#006600" />
                </fieldset>
            </div>

            <table rules="all" border="1" style="width:2000px;"  class="tb1">
            <tr style="height:40px; background-color:#CC5599;">
                <td colspan="19" style="text-align:center; width:100%;">
                    <span style="color:White; font-size:20px;">线上校友专区（校 外）</span>
                </td>
            </tr>
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
