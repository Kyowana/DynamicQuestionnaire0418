using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 動態問卷.Managers;
using 動態問卷.Models;

namespace 動態問卷.SystemAdmin
{
    public partial class Detail : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        private Guid _QID;
        private List<QuestionModel> _questionList = new List<QuestionModel>();
        private SummaryModel _qs;
        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (HttpContext.Current.Session["ID"] != null)
                _QID = (Guid)HttpContext.Current.Session["ID"];

            _qs = HttpContext.Current.Session["Summary"] as SummaryModel;

            if (HttpContext.Current.Session["AddList"] != null)
                _questionList = HttpContext.Current.Session["AddList"] as List<QuestionModel>;
            else
                _questionList = new List<QuestionModel>();

            // !isPostback
            if (!this.IsPostBack)
            {
                if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
                {
                    // Edit mode
                    _QID = questionnaireID;
                    List<QuestionModel> questionList = _qMgr.GetQuestionsList(questionnaireID);
                    if (questionList != null)
                    {
                        this.GridViewQuestionList.DataSource = questionList;
                        this.GridViewQuestionList.DataBind();
                    }
                    else
                    {
                        this.plcNoQuestions.Visible = true;
                    }
                }
                else
                {
                    // Create mode
                    Guid newquestionnaireID = Guid.NewGuid();
                    HttpContext.Current.Session["ID"] = newquestionnaireID;

                }

            }

            // Postback
            // 顯示在下方Grid (ajax)
            if (_questionList != null)
            {
                InitQuestionsList();
            }

        }

        private void InitQuestionsList()
        {
            this.GridViewQuestionList.DataSource = _questionList;
            this.GridViewQuestionList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.page01.Visible = false;
            this.page02.Visible = true;

            if (this.ddlQtype.SelectedValue == "1" || this.ddlQtype.SelectedValue == "2" || this.ddlQtype.SelectedValue == "5")
            {
                if (string.IsNullOrWhiteSpace(this.txtAnswer.Text))
                {
                    this.lblMsg.Visible = true;
                    return;
                }
                else
                    this.lblMsg.Visible = false;
            }

            //_questionList = new List<QuestionModel>();
            QuestionModel q = new QuestionModel()
            {
                QID = _QID,
                QuestionID = Guid.NewGuid(),
                Question = this.txtQuestion.Text,
                AnswerOption = this.txtAnswer.Text,
                QType = Convert.ToInt32(this.ddlQtype.SelectedValue),
                IsRequired = this.ckbRequired.Checked,
                CreateDate = DateTime.Now
            };

            // 放一個hf存現在編輯中的問題ID
            // 檢查list內是否已有此ID
            if (string.IsNullOrWhiteSpace(this.hfNowQuestionID.Value))
            {
                // 有: 編輯問題
                if (_questionList.Exists(x => x.QuestionID.ToString() == this.hfNowQuestionID.Value))
                {
                    QuestionModel question = _questionList.Find(x => x.QuestionID.ToString().Contains(this.hfNowQuestionID.Value));
                    if (Guid.TryParse(this.hfNowQuestionID.Value, out Guid nowQuestionID))
                    {
                        question = new QuestionModel()
                        {
                            QID = _QID,
                            QuestionID = nowQuestionID,
                            Question = this.txtQuestion.Text,
                            AnswerOption = this.txtAnswer.Text,
                            QType = Convert.ToInt32(this.ddlQtype.SelectedValue),
                            IsRequired = this.ckbRequired.Checked,
                            CreateDate = DateTime.Now
                        };

                    }
                }
            }
            else
            {
                // 無: 新增問題
                _questionList.Add(q);
            }
            HttpContext.Current.Session["AddList"] = _questionList;

            // 清空輸入框內容
            this.hfNowQuestionID.Value = "";
            this.txtQuestion.Text = "";
            this.txtAnswer.Text = "";
            this.ddlQtype.SelectedValue = "1";

            InitQuestionsList();



        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            SummaryModel qs = new SummaryModel()
            {
                QID = _QID,
                Caption = this.txtCaption.Text,
                Description = this.txtDescription.Text,
                StartDate = Convert.ToDateTime(this.txtStartDate.Text),
                EndDate = Convert.ToDateTime(this.txtEndDate.Text),
                ViewLimit = this.ckbLimit.Checked
            };

            HttpContext.Current.Session["Summary"] = qs;

            this.page01.Visible = false;
            this.page02.Visible = true;
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            _qMgr.CreateQuestionnaire(_qs);

            foreach (var item in _questionList)
            {
                _qMgr.CreateQuestion(item);

            }
            this.page02.Visible = false;
            this.page03.Visible = true;
        }

        protected void GridViewQuestionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.page01.Visible = false;
            this.page02.Visible = true;

            if (e.CommandName == "EditButton")
            {
                string commentIdText = e.CommandArgument as string;
                QuestionModel question = _questionList.Find(x => x.QuestionID.ToString().Contains(commentIdText));

                // 將值帶回上方
                this.txtQuestion.Text = question.Question;
                this.txtAnswer.Text = question.AnswerOption;
                this.ddlQtype.SelectedValue = question.QType.ToString();
                this.hfNowQuestionID.Value = question.QuestionID.ToString();
            }

        }
    }
}