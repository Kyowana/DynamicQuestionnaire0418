<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmPage.aspx.cs" Inherits="動態問卷.ConfirmPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>前台確認頁</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>前台</h1>
        <div style="float: left; margin-right: 40px">
            <a href="List.aspx">回到問卷列表</a><br />
        </div>
        <div style="float: left">
            <div>
                <asp:Label ID="lblLimit" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
            </div>
            <div>
                <table>
                    <tr>
                        <td>姓名</td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td>手機</td>
                        <td>
                            <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Email</td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>年齡</td>
                        <td>
                            <asp:Label ID="lblAge" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <asp:PlaceHolder ID="plcQuestions" runat="server"></asp:PlaceHolder>

            <div>
                <asp:Button ID="btnRevise" runat="server" Text="修改" OnClick="btnRevise_Click" />
                <asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </form>
</body>
</html>
