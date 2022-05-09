﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaqList.aspx.cs" Inherits="動態問卷.SystemAdmin.FaqList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hfNowFaqID" runat="server" Value="" />
            問題<asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>                
            <asp:DropDownList ID="ddlQtype" runat="server">
                <asp:ListItem Value="1">單選方塊</asp:ListItem>
                <asp:ListItem Value="2">核取方塊</asp:ListItem>
                <asp:ListItem Value="3">單行輸入</asp:ListItem>
            </asp:DropDownList>
            <%--<asp:CheckBox ID="ckbRequired" runat="server" Text="必填" /><br />--%>
            <br />
            回答<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox> (多個答案以;分隔) 
            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" Text="請輸入回答選項" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <br />
        </div>
            <asp:Button ID="btnDeleteQuestion" runat="server" Text="刪除" OnClick="btnDeleteQuestion_Click" /><br />

        <asp:GridView ID="GridViewFaqList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewFaqList_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbDel" runat="server" />
                            <asp:HiddenField ID="hfFaqID" runat="server" Value='<%# Eval("FaqID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="QuestionNumber" HeaderText="#" />--%>
                    <asp:BoundField DataField="Question" HeaderText="問題" />
                    <asp:BoundField DataField="QType" HeaderText="種類" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div>
                                <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="EditButton" CommandArgument='<%# Eval("FaqID") %>' UseSubmitBehavior="False" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

    </form>
</body>
</html>
