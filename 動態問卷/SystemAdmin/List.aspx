<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="動態問卷.SystemAdmin.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後台 - 問卷管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            問卷標題<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
            開始 / 結束日期<asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox><asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>&emsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
        </div>
        <div>
            <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
            <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" /><br />
            <asp:Label ID="lblMsgNoList" runat="server" Text="尚未有資料" Visible="false"></asp:Label>
            <asp:GridView ID="GridQList" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbDel" runat="server" />
                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("QID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SerialNumber" HeaderText="#" />
                    <asp:TemplateField HeaderText="問卷">
                        <ItemTemplate>
                            <a href="Detail.aspx?ID=<%# Eval("QID") %>"><%# Eval("Caption") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ViewLimit" HeaderText="狀態" />
                    <asp:BoundField DataField="StartDate" HeaderText="開始時間" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="EndDate" HeaderText="結束時間" DataFormatString="{0:d}" />
                    <asp:TemplateField HeaderText="觀看統計">
                        <ItemTemplate>
                            <a href="Detail.aspx?ID=<%# Eval("QID") %>">前往</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div>
                <a runat="server" id="aLinkFirst" href="MapList.aspx?page=1">第一頁</a>
                <a runat="server" id="aLinkPrev" href="">上一頁</a>
                <a runat="server" id="aLinkPage1" href="">1</a>
                <a runat="server" id="aLinkPage2" href="">2</a>
                <a runat="server" id="aLinkPage3" href="">3</a>
                <a runat="server" id="aLinkPage4" href="">4</a>
                <a runat="server" id="aLinkPage5" href="">5</a>
                <a runat="server" id="aLinkNext" href="">下一頁</a>
                <a runat="server" id="aLinkLast" href="">最末頁</a>
            </div>

        </div>
    </form>
</body>
</html>
