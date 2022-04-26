using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 動態問卷.Managers;
using 動態問卷.Models;

namespace 動態問卷
{
    public partial class Form : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        private List<QuestionModel> _questionList = new List<QuestionModel>();
        private int _questionNumber = 1;
        private int _optionCount = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                if (!IsPostBack)
                {
                    SummaryModel qs = _qMgr.GetQuestionnaireSummary(questionnaireID);
                    _questionList = _qMgr.GetQuestionsList(questionnaireID);

                    this.lblCaption.Text = qs.Caption;
                    this.lblDescription.Text = qs.Description;
                    this.lblLimit.Text = qs.ViewLimit.ToString();
                    this.lblDate.Text = qs.StartDate.ToString() + "～" + qs.EndDate.ToString();
                    this.lblQuestionsCount.Text = $"共 {_questionList.Count} 個問題";

                    foreach (var item in _questionList)
                    {
                        this.plcQuestions.Controls.Add(new Panel() { ID = $"panel{item.QuestionID}" });
                        FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" });
                        _questionNumber++;

                        switch (item.QType)
                        {
                            case 1:
                                string[] arrContent = item.AnswerOption.Trim().Split(';');
                                //string contentWithBr = string.Join("<br/>", arrContent);
                                foreach (var content in arrContent)
                                {
                                    FindControl($"panel{item.QuestionID}").Controls.Add(new RadioButton() { ID = $"option{_optionCount}", Text = content + "<br />" });
                                    _optionCount++;
                                }
                                break;

                            case 2:
                                string[] arrContent2 = item.AnswerOption.Trim().Split(';');
                                foreach (var content in arrContent2)
                                {
                                    FindControl($"panel{item.QuestionID}").Controls.Add(new CheckBox() { ID = $"option{_optionCount}", Text = content + "<br />" });
                                    _optionCount++;
                                }
                                break;

                            case 3:
                                FindControl($"panel{item.QuestionID}").Controls.Add(new TextBox() { ID = "txtAnswer" });
                                FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });
                                break;

                            case 4:
                                FindControl($"panel{item.QuestionID}").Controls.Add(new TextBox() { ID = "txtMultiline", TextMode = TextBoxMode.MultiLine });
                                FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                Response.Redirect("List.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //foreach (var item in _questionList)
            //{
            //    foreach (var panel in this.plcQuestions.Controls)
            //    {
            //        Control control = FindControl($"panel{item.QuestionID}");

            //        foreach (var control in panel)
            //        {

            //        }
            //    }

            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}