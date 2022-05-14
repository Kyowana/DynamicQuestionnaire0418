<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="動態問卷.Stastic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>前台</title>
    <style>
        .frame {
            width: 700px;
            border: 1px solid black;
            float: left;
            height: 20px;
        }

        .strip {
            border: 1px solid red;
            background-color: red;
            height: 20px;
            font-weight: 700;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float: left; margin-right: 40px">
            <a href="List.aspx">回到問卷列表</a><br />
        </div>
        <div style="float: left">
            <div>
                <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <asp:Label ID="lblNoAnsMsg" runat="server" Text="尚未有人作答" Visible="false"></asp:Label>
            <asp:PlaceHolder ID="plcQuestions" runat="server"></asp:PlaceHolder>

        </div>

    </form>
</body>
</html>
