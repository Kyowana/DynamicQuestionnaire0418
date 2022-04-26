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
            <h3><asp:Label ID="lblCaption" runat="server" Text=""></asp:Label></h3>
            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label><br /><br />
        </div>
        <div>
            <table>
                <tr>
                    <td>姓名</td>
                    <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>手機</td>
                    <td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td><asp:TextBox ID="txtAge" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <br /><br />
        <asp:PlaceHolder ID="plcQuestions" runat="server">
        </asp:PlaceHolder>

        <div>
            <br />
            <asp:Label ID="lblQuestionsCount" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
            <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />
        </div>

    </form>
</body>
</html>
