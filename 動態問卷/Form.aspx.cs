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
        //private int _optionCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                if (!IsPostBack)
                {
                    SummaryModel qs = _qMgr.GetQuestionnaireSummary(questionnaireID);
                    _questionList = _qMgr.GetQuestionsList(questionnaireID);

                    this.hfQID.Value = questionnaireIDString;
                    this.lblCaption.Text = qs.Caption;
                    this.lblDescription.Text = qs.Description;
                    this.lblLimit.Text = qs.ViewLimit.ToString();
                    this.lblDate.Text = qs.StartDate.ToString() + "～" + qs.EndDate.ToString();
                    this.lblQuestionsCount.Text = $"共 {_questionList.Count} 個問題";

                    foreach (var item in _questionList)
                    {
                        this.plcQuestions.Controls.Add(new Panel() { ID = $"panel{item.QuestionID}" });
                        // 加入必填

                        FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" });
                        _questionNumber++;

                        switch (item.QType)
                        {
                            case 1:
                                string[] arrContent = item.AnswerOption.Trim().Split(';');
                                //string contentWithBr = string.Join("<br/>", arrContent);
                                //FindControl($"panel{item.QuestionID}").Controls.Add(new RadioButtonList() { ID = $"AnsRbl_{item.QuestionID}" });
                                //RadioButtonList rbl = FindControl($"AnsRbl_{item.QuestionID}") as RadioButtonList;
                                //foreach (var content in arrContent)
                                //{
                                //    rbl.Items.Add(content);
                                //}
                                //FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });
                                int rdbCount = 0;
                                foreach (var content in arrContent)
                                {
                                    FindControl($"panel{item.QuestionID}").Controls.Add(new RadioButton() { ID = $"AnsRdbOption{rdbCount}", Text = content + "<br />", GroupName = $"op{item.QuestionID}" });
                                    //FindControl($"panel{item.QuestionID}").Controls.Add(new HiddenField() { Value = item.QuestionID.ToString() });
                                    rdbCount++;
                                }
                                break;

                            case 2:
                                string[] arrContent2 = item.AnswerOption.Trim().Split(';');
                                int ckbCount = 0;
                                foreach (var content in arrContent2)
                                {
                                    FindControl($"panel{item.QuestionID}").Controls.Add(new CheckBox() { ID = $"AnsCkbOption{ckbCount}", Text = content + "<br />" });
                                    ckbCount++;
                                }
                                break;

                            case 3:
                                FindControl($"panel{item.QuestionID}").Controls.Add(new TextBox() { ID = $"AnsTxt_{item.QuestionID}" });
                                FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });
                                break;

                            case 4:
                                FindControl($"panel{item.QuestionID}").Controls.Add(new TextBox() { ID = $"AnsTxtMultiline_{item.QuestionID}", TextMode = TextBoxMode.MultiLine });
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


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    // 檢查必填

        //    // 將回答summary存入session
        //}
    }
}