<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="動態問卷.SystemAdmin.Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>後台 - 問卷管理</title>
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
            <asp:LinkButton ID="lbtnPage01" runat="server" Enabled="false" OnClick="lbtnPage01_Click">問卷</asp:LinkButton>
            <asp:LinkButton ID="lbtnPage02" runat="server" OnClick="lbtnPage02_Click">問題</asp:LinkButton>
            <asp:LinkButton ID="lbtnPage03" runat="server" OnClick="lbtnPage03_Click">填寫資料</asp:LinkButton>
            <asp:LinkButton ID="lbtnPage04" runat="server" OnClick="lbtnPage04_Click">統計</asp:LinkButton>
        </div>

    <div>
        <asp:PlaceHolder ID="page01" runat="server">
            問卷名稱<asp:TextBox ID="txtCaption" runat="server" /><br />
            描述內容<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /><br />
            開始時間<asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" /><br />
            結束時間<asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" /><br />
            已啟用<asp:CheckBox ID="ckbLimit" runat="server" /><br />
            <asp:Label ID="lblPage1Msg" runat="server" Text="問卷名稱及開始/結束時間為必填" ForeColor="Red" Visible="false"></asp:Label><br />
            <asp:Button ID="btnCancel1" runat="server" Text="取消" OnClick="btnCancel1_Click" />
            <asp:Button ID="btnSubmit1" runat="server" Text="送出" OnClick="btnSubmit1_Click" />
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page02" runat="server" Visible="false">
            <div>
                種類<asp:DropDownList ID="ddlFaq" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaq_SelectedIndexChanged">
                    <asp:ListItem Value="0">自訂問題</asp:ListItem>
                  </asp:DropDownList>
            </div>
            <asp:HiddenField ID="hfNowQuestionID" runat="server" Value="" />
            問題<asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>                
            <asp:DropDownList ID="ddlQtype" runat="server">
                <asp:ListItem Value="1">單選方塊</asp:ListItem>
                <asp:ListItem Value="2">核取方塊</asp:ListItem>
                <asp:ListItem Value="3">單行輸入</asp:ListItem>
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
            <asp:Button ID="btnCancel2" runat="server" Text="取消" OnClick="btnCancel2_Click" />
            <asp:Button ID="btnSubmit2" runat="server" Text="送出" OnClick="btnSubmit2_Click" />

        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page03" runat="server" Visible="false">
            <asp:PlaceHolder ID="plcPage03_1" runat="server">
                <asp:Button ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click" /><br />
                <br />
                <asp:GridView ID="GridAnswerList" runat="server" AutoGenerateColumns="False" OnRowCommand="GridAnswerList_RowCommand">
                    <Columns>
                        <%--<asp:BoundField DataField="SerialNumber" HeaderText="#" />--%>
                        <asp:BoundField DataField="Name" HeaderText="姓名" />
                        <asp:BoundField DataField="SubmitDate" HeaderText="填寫時間" DataFormatString="{0:g}" />
                        <asp:TemplateField HeaderText="觀看細節">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbtGo" runat="server" CommandName="GoLbtn" CommandArgument='<%# Eval("AnswerID") %>'>前往</asp:LinkButton>
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

            </asp:PlaceHolder>

            <asp:PlaceHolder ID="plcPage03_2" runat="server" Visible ="false">
                姓名&emsp;<asp:TextBox ID="txtName" runat="server" Enabled="false" />&emsp;
                手機&emsp;<asp:TextBox ID="txtPhone" runat="server" Enabled="false" />&emsp;<br />
                Email&emsp;<asp:TextBox ID="txtEmail" runat="server" Enabled="false" />&emsp;
                年齡&emsp;<asp:TextBox ID="txtAge" runat="server" Enabled="false" />&emsp;<br />
                <asp:Label ID="lblSubmitDate" runat="server" Text="Label"></asp:Label><br />
                <br />
            </asp:PlaceHolder>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="page04" runat="server" Visible="false">
            <asp:PlaceHolder ID="plcQuestions" runat="server"></asp:PlaceHolder>
        </asp:PlaceHolder>
    </div>



    </form>

</body>
</html>
