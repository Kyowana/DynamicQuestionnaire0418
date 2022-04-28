<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="動態問卷.Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfQID" runat="server" />
        <div>
            <asp:Label ID="lblLimit" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <h3>
                <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label></h3>
            <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label><br />
            <br />
        </div>
        <div>
            <table>
                <tr>
                    <td>姓名</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>手機</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>年齡</td>
                    <td>
                        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <asp:PlaceHolder ID="plcQuestions" runat="server"></asp:PlaceHolder>

        <div>
            <br />
            <asp:Label ID="lblQuestionsCount" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" />
            <%--<asp:Button ID="btnSubmit" runat="server" Text="送出" OnClick="btnSubmit_Click" />--%>
            <button type="submit" id="btnSubmit">送出</button>
        </div>

    </form>


    <script>
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                var container = $("#<%= this.plcQuestions.ClientID %>");

                var questionContainer = $("div[id^='panel']");
                var allQuestionContainer = questionContainer.get();

                var allAnswer = "";
                for (var question of allQuestionContainer) {
                    var answersList = $("input[id^='Ans']", question).get();
                    var ret = question.id.slice(-36);
                    var answer = "";

                    for (var item of answersList) {
                        var aaa = $(`input[name='op${ret}']`);
                        //if ($(`input[name='op${ret}']`).length > 0) {
                        //    var method = $(`input[name='op${ret}']:checked`).val();
                        //    if (typeof (method) == "undefined") {
                        //        answer += " ;";
                        //    }
                        //}
                        //else if ($("input[id^='AnsCkbOption']", question).length > 0) {

                        //}

                        if (item.type == "radio" && item.checked) {
                            answer += item.id + ";";
                        }
                        if (item.type == "checkbox" && item.checked) {
                            answer += item.id + ";";
                        }
                        if (item.type == "text") {
                            answer += item.value;
                        }
                    }
                    allAnswer += answer + ",";
                }


                var answerData = {
                    "QID": $("#hfQID").val(),
                    "Name": $("#<%= this.txtName.ClientID %>").val(),
                    "Phone": $("#<%= this.txtPhone.ClientID %>").val(),
                    "Email": $("#<%= this.txtEmail.ClientID %>").val(),
                    "Age": $("#<%= this.txtAge.ClientID %>").val(),
                    "AnswerContents": allAnswer
                };


                $.ajax({
                    url: "/API/AnswerHandler.ashx?Action=SendAnswer",
                    method: "POST",
                    data: answerData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "OK") {
                            window.location = "ConfirmPage.aspx?ID=" + $("#hfQID").val();
                        }
                        else {
                            alert("提交失敗。");
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            });

        })
    </script>
</body>
</html>
