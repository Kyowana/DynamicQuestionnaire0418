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
        private AnswerSummaryModel _asModel = new AnswerSummaryModel();
        private List<AnswerContentModel> _acList = new List<AnswerContentModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];

            if (HttpContext.Current.Session["UserInfo"] != null)
                _asModel = HttpContext.Current.Session["UserInfo"] as AnswerSummaryModel;
            if (HttpContext.Current.Session["AnswerList"] != null)
                _acList = HttpContext.Current.Session["AnswerList"] as List<AnswerContentModel>;

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
                                int rdbCount = 0;
                                foreach (var content in arrContent)
                                {
                                    FindControl($"panel{item.QuestionID}").Controls.Add(new RadioButton() { ID = $"AnsRdbOption{rdbCount}", Text = content + "<br />", GroupName = $"op{item.QuestionID}" });
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