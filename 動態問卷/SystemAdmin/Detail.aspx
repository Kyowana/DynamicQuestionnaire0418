<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="動態問卷.SystemAdmin.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <div>
        <asp:PlaceHolder ID="page1" runat="server">
            問卷名稱<asp:TextBox ID="txtCaption" runat="server" /><br />
            描述內容<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /><br />
            開始時間<asp:TextBox ID="txtStartDate" runat="server" TextMode="DateTime" /><br />
            結束時間<asp:TextBox ID="txtEndDate" runat="server" TextMode="DateTime" /><br />
            已啟用<asp:CheckBox ID="ckbLimit" runat="server" /><br />
            <asp:Button ID="btnCancel1" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click" />
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page2" runat="server" Visible="false">
            問題<asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
            <asp:DropDownList ID="ddlQtype" runat="server">
                <asp:ListItem Value="1">單選方塊</asp:ListItem>
                <asp:ListItem Value="2">核取方塊</asp:ListItem>
                <asp:ListItem Value="3">單行輸入</asp:ListItem>
                <asp:ListItem Value="4">多行輸入</asp:ListItem>
                <asp:ListItem Value="5">下拉式選單</asp:ListItem>
                <asp:ListItem Value="6">日期</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="ckbRequired" runat="server" Text="必填" /><br />
            回答<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click" /><br />

            <asp:GridView ID="GridView1" runat="server"></asp:GridView>

            <asp:Button ID="btnCancel2" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click" />

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page3" runat="server" Visible="false"></asp:PlaceHolder>

        <asp:PlaceHolder ID="page4" runat="server" Visible="false"></asp:PlaceHolder>
    </div>
</body>
</html>
