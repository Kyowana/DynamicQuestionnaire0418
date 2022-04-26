<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="動態問卷.SystemAdmin.Detail" %>

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
            <asp:Button ID="btnCancel1" runat="server" Text="取消" OnClick="btnCancel1_Click" />
            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click" />
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page02" runat="server" Visible="false">
            <asp:HiddenField ID="hfNowQuestionID" runat="server" Value="" />
            問題<asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>                
            <asp:DropDownList ID="ddlQtype" runat="server">
                <asp:ListItem Value="1">單選方塊</asp:ListItem>
                <asp:ListItem Value="2">核取方塊</asp:ListItem>
                <asp:ListItem Value="3">單行輸入</asp:ListItem>
                <asp:ListItem Value="4">多行輸入</asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox ID="ckbRequired" runat="server" Text="必填" /><br />
            
            回答<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox> (多個答案以;分隔) 
            <asp:Button ID="btnAdd" runat="server" Text="加入" OnClick="btnAdd_Click" /><br />
            <asp:Label ID="lblMsg" runat="server" Text="請輸入回答選項" ForeColor="Red" Visible="false"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnDeleteQuestion" runat="server" Text="刪除" OnClick="btnDeleteQuestion_Click" /><br />

            <asp:GridView ID="GridViewQuestionList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewQuestionList_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbDel" runat="server" />
                            <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuestionNumber" HeaderText="#" />
                    <asp:BoundField DataField="Question" HeaderText="問題" />
                    <asp:BoundField DataField="QType" HeaderText="種類" />
                    <asp:CheckBoxField DataField="IsRequired" HeaderText="必填" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div>
                                <%--<button runat="server" id="btnEdit" type="button">編輯</button>--%>
                                <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="EditButton" CommandArgument='<%# Eval("QuestionID") %>' UseSubmitBehavior="False" />
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
            <br />
            <asp:Button ID="btnCancel2" runat="server" Text="取消" />
            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click" />

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page03" runat="server" Visible="false"></asp:PlaceHolder>

        <asp:PlaceHolder ID="page04" runat="server" Visible="false"></asp:PlaceHolder>
    </div>



    </form>

</body>
</html>
