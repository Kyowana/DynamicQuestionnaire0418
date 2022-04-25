<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="動態問卷.ConfirmPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblLimit" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Label ID="lblAnswer" runat="server" Text=""></asp:Label>
        </div>

        <div>
            <asp:Button ID="btnRevise" runat="server" Text="修改" />
            <asp:Button ID="btnSubmit" runat="server" Text="送出" />
        </div>
    </form>
</body>
</html>
