<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="動態問卷.List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>前台</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            問卷標題<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
            開始 / 結束日期<asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox><asp:TextBox ID="txtFinDate" runat="server"></asp:TextBox>&emsp;
        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
        </div>
        <div>
            <asp:GridView ID="GridQList" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SerialNumber" HeaderText="#" />
                    <asp:TemplateField HeaderText="問卷">
                        <ItemTemplate>
                            <a href="Form.aspx?ID=<%# Eval("QID") %>"><%# Eval("Caption") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="ViewLimit" HeaderText="狀態" />
                    <asp:BoundField DataField="StartDate" HeaderText="開始時間" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="EndDate" HeaderText="結束時間" DataFormatString="{0:d}" />
                    <asp:TemplateField HeaderText="觀看統計">
                        <ItemTemplate>
                            <a href="Form.aspx?ID=<%# Eval("QID") %>">前往</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
    </form>
</body>
</html>
