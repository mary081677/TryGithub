<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!--PlaceHolder是個容器 負責裝載控制項，所有跟登入有關的控制項放入；
            進入頁面檢查使用者是否登入過，-->
        <asp:PlaceHolder runat="server" ID="plcLogin" Visible="false">
            Account: 
            <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
            <br />
            Password:
            <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <br />
            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        </asp:PlaceHolder>
    </form>
</body>
</html>
