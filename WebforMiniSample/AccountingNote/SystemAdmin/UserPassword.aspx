<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="_1.UserDetail.UserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a><br />
                    <a href="UserList.aspx">會員管理</a>
                </td>
                <td>
                    <!--這裡放主要內容-->
                    <table>
                        <tr>
                            <th>帳號</th>
                            <td>
                                <asp:Literal runat="server" ID="ltAccount"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>舊密碼</th>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>確認密碼</th>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>                            
                            <th>新密碼</th>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnNewPassword" runat="server" Text="變更密碼" OnClick="btnNewPassword_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
