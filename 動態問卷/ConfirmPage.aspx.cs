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
    public partial class ConfirmPage : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        private AnswerManager _aMgr = new AnswerManager();
        private List<QuestionModel> _questionList = new List<QuestionModel>();
        private int _questionNumber = 1;
        private Guid _qID;
        AnswerSummaryModel _asModel = new AnswerSummaryModel();
        List<AnswerContentModel> _acList = new List<AnswerContentModel>();


        protected void Page_Load(object sender, EventArgs e)
        {
            _asModel = HttpContext.Current.Session["UserInfo"] as AnswerSummaryModel;
            _acList = HttpContext.Current.Session["AnswerList"] as List<AnswerContentModel>;

            string questionnaireIDString = Request.QueryString["ID"];

            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                _qID = questionnaireID;

                if (!IsPostBack)
                {
                    SummaryModel qs = _qMgr.GetQuestionnaireSummary(questionnaireID);
                    _questionList = _qMgr.GetQuestionsList(questionnaireID);

                    this.lblCaption.Text = qs.Caption;
                    this.lblLimit.Text = qs.ViewLimit.ToString();
                    this.lblDate.Text = qs.StartDate.ToString() + "～" + qs.EndDate.ToString();

                    this.lblName.Text = _asModel.Name;
                    this.lblPhone.Text = _asModel.Phone;
                    this.lblEmail.Text = _asModel.Email;
                    this.lblAge.Text = _asModel.Age.ToString();


                    for (int i = 0; i < _questionList.Count; i++)
                    {
                        if (_questionList.Exists(x => x.QuestionID == _acList[i].QuestionID))
                        {
                            QuestionModel question = _questionList.Find(x => x.QuestionID.ToString().Contains(_acList[i].QuestionID.ToString()));


                            this.plcQuestions.Controls.Add(new Panel() { ID = $"panel{question.QuestionID}" });

                            FindControl($"panel{question.QuestionID}").Controls.Add(new Literal() { Text = _questionNumber + ". " + question.Question + "<br />" });
                            _questionNumber++;

                            switch (question.QType)
                            {
                                case 1:
                                case 2:
                                    string[] arrContent1 = question.AnswerOption.Trim().Split(';');
                                    string[] arrOption1 = _acList[i].Answer.Trim().Split(';');
                                    for (int k = 0; k < (arrOption1.Length - 1); k++)
                                    {
                                        string number = arrOption1[k].Remove(0, 12);
                                        for (int j = 0; j < arrContent1.Length; j++)
                                        {
                                            if (number == j.ToString())
                                            {
                                                FindControl($"panel{question.QuestionID}").Controls.Add(new Literal() { Text = arrContent1[j] + "<br />" });
                                            }
                                        }

                                    }
                                    break;

                                case 3:
                                    FindControl($"panel{question.QuestionID}").Controls.Add(new Literal() { Text = _acList[i].Answer + "<br />" });
                                    break;

                                default:
                                    break;
                            }
                        }

                    }


                }
            }
            else
                Response.Redirect("List.aspx");
        }

        protected void btnRevise_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Form.aspx?ID={_qID}");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // 寫進資料庫!!
            _aMgr.CreateAnswerSummary(_asModel);
            foreach (var item in _acList)
            {
                _aMgr.CreateAnswerContent(item);

            }
            Session.Remove("UserInfo");
            Session.Remove("AnswerList");

            Response.Redirect($"List.aspx");

        }
    }
}