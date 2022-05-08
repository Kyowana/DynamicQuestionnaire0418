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
            {
                _asModel = HttpContext.Current.Session["UserInfo"] as AnswerSummaryModel;
                this.txtName.Text = _asModel.Name;
                this.txtPhone.Text = _asModel.Phone;
                this.txtEmail.Text = _asModel.Email;
                this.txtAge.Text = _asModel.Age.ToString();
            }
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

                        Literal ltlQuestion = new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" };
                        if (item.IsRequired)
                            ltlQuestion.Text = _questionNumber + ". " + item.Question + " (必填) <br />";

                        FindControl($"panel{item.QuestionID}").Controls.Add(ltlQuestion);
                        _questionNumber++;

                        switch (item.QType)
                        {
                            case 1:
                                string[] arrContent = item.AnswerOption.Trim().Split(';');
                                int rdbCount = 0;
                                foreach (var content in arrContent)
                                {
                                    RadioButton rdb = new RadioButton() { ID = $"{item.QuestionID}_AnsRdbOption{rdbCount}", Text = content + "<br />", GroupName = $"op{item.QuestionID}" };
                                    if (item.IsRequired)
                                        rdb.CssClass = "required";

                                    FindControl($"panel{item.QuestionID}").Controls.Add(rdb);
                                    AnswerContentModel answer1 = _acList.Find(x => x.QuestionID == item.QuestionID);
                                    if (answer1 != null)
                                    {
                                        string[] arrOption1 = answer1.Answer.Trim().Split(';');
                                        if (arrOption1.Contains(rdb.ID))
                                            rdb.Checked = true;
                                    }
                                    rdbCount++;
                                }
                                break;

                            case 2:
                                string[] arrContent2 = item.AnswerOption.Trim().Split(';');
                                int ckbCount = 0;
                                foreach (var content in arrContent2)
                                {
                                    CheckBox ckb = new CheckBox() { ID = $"{item.QuestionID}_AnsCkbOption{ckbCount}", Text = content + "<br />" };
                                    if (item.IsRequired)
                                        ckb.CssClass = "required";

                                    FindControl($"panel{item.QuestionID}").Controls.Add(ckb);
                                    AnswerContentModel answer2 = _acList.Find(x => x.QuestionID == item.QuestionID);
                                    if (answer2 != null)
                                    {
                                        string[] arrOption1 = answer2.Answer.Trim().Split(';');
                                        if (arrOption1.Contains(ckb.ID))
                                            ckb.Checked = true;
                                    }
                                    ckbCount++;
                                }
                                break;

                            case 3:
                                TextBox txb = new TextBox() { ID = $"AnsTxt_{item.QuestionID}" };
                                if (item.IsRequired)
                                    txb.CssClass = "required";

                                FindControl($"panel{item.QuestionID}").Controls.Add(txb);
                                FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });

                                AnswerContentModel answer3 = _acList.Find(x => x.QuestionID == item.QuestionID);
                                if (answer3 != null)
                                {
                                    txb.Text = answer3.Answer;
                                }
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