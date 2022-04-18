<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="動態問卷.SystemAdmin.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        問卷標題<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
        開始 / 結束日期<asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox><asp:TextBox ID="txtFinDate" runat="server"></asp:TextBox>&emsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜尋" />
    </div>
    <div>
        <asp:Button ID="btnDelete" runat="server" Text="刪除" />
        <asp:Button ID="btnCreate" runat="server" Text="新增" />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="SerialNumber" HeaderText="#" InsertVisible="False" ReadOnly="True" SortExpression="SerialNumber" />
                <asp:BoundField DataField="Caption" HeaderText="問卷" SortExpression="Caption" />
                <asp:CheckBoxField DataField="ViewLimit" HeaderText="狀態" SortExpression="ViewLimit" />
                <asp:BoundField DataField="StartDate" HeaderText="開始時間" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="結束時間" SortExpression="EndDate" />
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DynamicQuestionnaireConnectionString %>" SelectCommand="SELECT [Caption], [StartDate], [EndDate], [ViewLimit], [SerialNumber] FROM [QSummarys]"></asp:SqlDataSource>

        <asp:Repeater ID="rptFormList" runat="server" OnItemCommand="rptFormList_ItemCommand">
            <HeaderTemplate>
                # \n
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chbDel" runat="server" />
                <%# Eval("QID") %>
                <%# Eval("ViewLimit") %>
                <%# Eval("StartDate") %>
                <%# Eval("EndDate") %>
                <%# Eval("QID") %>

            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
