﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stastic.aspx.cs" Inherits="動態問卷.Stastic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>前台</title>
    <style>
        .frame {
            width: 50%;
            border: 1px solid black;
            float:left;
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
        <div>
            <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <asp:Label ID="lblNoAnsMsg" runat="server" Text="尚未有人作答" Visible="false"></asp:Label>
        <asp:PlaceHolder ID="plcQuestions" runat="server"></asp:PlaceHolder>



    </form>
</body>
</html>
