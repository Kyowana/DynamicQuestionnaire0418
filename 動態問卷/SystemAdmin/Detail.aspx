﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="動態問卷.SystemAdmin.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="page01" runat="server">
            問卷名稱<asp:TextBox ID="txtCaption" runat="server" /><br />
            描述內容<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /><br />
            開始時間<asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" /><br />
            結束時間<asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" /><br />
            已啟用<asp:CheckBox ID="ckbLimit" runat="server" /><br />
            <asp:Button ID="btnCancel1" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click" />
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page02" runat="server" Visible="false">
            問題<asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>                
            <asp:DropDownList ID="ddlQtype" runat="server" OnSelectedIndexChanged="ddlQtype_SelectedIndexChanged">
                <asp:ListItem Value="1">單選方塊</asp:ListItem>
                <asp:ListItem Value="2">核取方塊</asp:ListItem>
                <asp:ListItem Value="3">單行輸入</asp:ListItem>
                <asp:ListItem Value="4">多行輸入</asp:ListItem>
                <asp:ListItem Value="5">下拉式選單</asp:ListItem>
                <asp:ListItem Value="6">日期</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="ckbRequired" runat="server" Text="必填" /><br />
            <asp:PlaceHolder ID="plcOptions" runat="server" Visible="true">
                新增<asp:TextBox ID="txtCtlNumber" runat="server" TextMode="Number">3</asp:TextBox>個選項<br />
                <asp:Button ID="btnAddOptions" runat="server" Text="確定新增" OnClick="btnAddOptions_Click" />
            </asp:PlaceHolder><br />
            回答<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click" /><br />

            <asp:GridView ID="GridViewQuestionList" runat="server">
                <Columns>
                    <asp:BoundField DataField="QuestionNumber" HeaderText="#" />
                    <asp:BoundField DataField="Question" HeaderText="問題" />
                    <asp:BoundField DataField="QType" HeaderText="種類" />
                    <asp:BoundField DataField="IsRequired" HeaderText="必填" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div>
                                <input type="hidden" class="hfID" value="<%# Eval("QuestionID") %>" />
                                <button runat="server" id="btnEdit" type="button">編輯</button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:PlaceHolder ID="plcNoQuestions" runat="server" Visible="false">
                <asp:Literal ID="Literal1" runat="server">
                    尚未新增問題
                </asp:Literal>
            </asp:PlaceHolder>

            <asp:Button ID="btnCancel2" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click" />

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page03" runat="server" Visible="false"></asp:PlaceHolder>

        <asp:PlaceHolder ID="page04" runat="server" Visible="false"></asp:PlaceHolder>
    </div>



    </form>

</body>
</html>
