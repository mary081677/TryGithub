<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="_1.UserDetail.UserDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                                <asp:Literal ID="ltAccount" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>等級</th>
                            <td>
                                <asp:Literal ID="ltUserLevel" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>建立時間</th>
                            <td>
                                <asp:Literal ID="ltdatatime" runat="server" ></asp:Literal></td>
                        </tr>
            </table>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <br />
            <asp:Button ID="btnPassword" runat="server" Text="前往變更密碼" OnClick="btnPassword_Click" />
            <br />
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
            </td>
            </tr>
        </table>
    </form>
</body>
</html>
