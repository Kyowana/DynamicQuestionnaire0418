<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="動態問卷.Form" %>

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
            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
        </div>

        <div>
            <asp:Button ID="btnCancel" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit" runat="server" Text="送出" />
        </div>
    </form>
</body>
</html>
