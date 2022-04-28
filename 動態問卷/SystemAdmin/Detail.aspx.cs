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
        private List<Guid> _delIdList = new List<Guid>();
        private SummaryModel _qs;
        private static bool _isEditMode;
        private enum PageStatus
        {
            Page01,
            Page02,
            Page03,
            Page04
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (HttpContext.Current.Session["ID"] != null)
                _QID = (Guid)HttpContext.Current.Session["ID"];

            _qs = HttpContext.Current.Session["Summary"] as SummaryModel;

            if (HttpContext.Current.Session["DeleteList"] != null)
                _delIdList = HttpContext.Current.Session["DeleteList"] as List<Guid>;

            if (HttpContext.Current.Session["AddList"] != null)
                _questionList = HttpContext.Current.Session["AddList"] as List<QuestionModel>;
            else
                _questionList = new List<QuestionModel>();

            // !isPostback
            //if (!this.IsPostBack)
            //{
            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                // Edit mode
                _isEditMode = true;
                _QID = questionnaireID;

                if (HttpContext.Current.Session["Summary"] != null)
                    _qs = HttpContext.Current.Session["Summary"] as SummaryModel;
                else
                    _qs = _qMgr.GetQuestionnaireSummary(questionnaireID);

                if (HttpContext.Current.Session["AddList"] != null)
                    _questionList = HttpContext.Current.Session["AddList"] as List<QuestionModel>;
                else
                {
                    _questionList = _qMgr.GetQuestionsList(questionnaireID);
                    HttpContext.Current.Session["AddList"] = _questionList;
                }
                if (!IsPostBack)
                {
                    this.txtCaption.Text = _qs.Caption;
                    this.txtDescription.Text = _qs.Description;
                    this.txtStartDate.Text = _qs.StartDate.ToString("yyyy-MM-dd");
                    this.txtEndDate.Text = _qs.EndDate.ToString("yyyy-MM-dd");
                    this.ckbLimit.Checked = _qs.ViewLimit;

                    if (_questionList.Count > 0)
                        InitQuestionsList();
                    else
                        this.plcNoQuestions.Visible = true;
                }


                //if (_questionList != null)
                //{
                //    this.GridViewQuestionList.DataSource = _questionList;
                //    this.GridViewQuestionList.DataBind();
                //}
                //else
                //{
                //    this.plcNoQuestions.Visible = true;
                //}
            }
            else
            {
                _isEditMode = false;

                if (!this.IsPostBack)
                {
                    // Create mode
                    Guid newquestionnaireID = Guid.NewGuid();
                    HttpContext.Current.Session["ID"] = newquestionnaireID;

                    if (_questionList.Count > 0)
                        InitQuestionsList();
                    else
                        this.plcNoQuestions.Visible = true;
                }

            }

            //}

            // Postback
            // 顯示在下方Grid (ajax)
            //if (_questionList.Count > 0)
            //    InitQuestionsList();
            //else
            //    this.plcNoQuestions.Visible = true;

        }

        private void InitQuestionsList()
        {
            this.GridViewQuestionList.DataSource = _questionList;
            this.GridViewQuestionList.DataBind();
        }

        private void InitAnswerList()
        {
            //this.GridAnswerList.DataSource = ;
            this.GridAnswerList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.page01.Visible = false;
            this.page02.Visible = true;

            if (this.ddlQtype.SelectedValue == "1" || this.ddlQtype.SelectedValue == "2")
            {
                if (string.IsNullOrWhiteSpace(this.txtAnswer.Text))
                {
                    this.lblMsg.Visible = true;
                    return;
                }
                else
                    this.lblMsg.Visible = false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.txtAnswer.Text))
                    this.txtAnswer.Text = "";
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
            if (!string.IsNullOrWhiteSpace(this.hfNowQuestionID.Value))
            {
                // 有: 編輯問題
                if (_questionList.Exists(x => x.QuestionID.ToString() == this.hfNowQuestionID.Value))
                {
                    QuestionModel question = _questionList.Find(x => x.QuestionID.ToString().Contains(this.hfNowQuestionID.Value));
                    _questionList.Remove(question);

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
                        _questionList.Add(question);
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
            this.ckbRequired.Checked = false;
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

            if (!_isEditMode)
                _qMgr.CreateQuestionnaire(_qs);
            else
                _qMgr.UpdateSummary(_qs);
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                // 若為新增問卷模式，直接新增問卷
                //_qMgr.CreateQuestionnaire(_qs);
                if (_questionList.Count > 0)
                {
                    foreach (var item in _questionList)
                    {
                        _qMgr.CreateQuestion(item);
                    }
                }
            }
            else
            {
                // 若為編輯模式
                //_qMgr.UpdateSummary(_qs);
                
                if (_delIdList.Count > 0)
                {
                    foreach (var item in _delIdList)
                    {
                        if (_qMgr.FindQuestion(item) != null)
                        {
                            _qMgr.DeleteQuestion(item);
                        }
                    }

                }

                if (_questionList.Count > 0)
                {
                    foreach (var item in _questionList)
                    {
                        if (_qMgr.FindQuestion(item.QuestionID) != null)
                        {
                            _qMgr.UpdateQuestion(item);
                        }
                        else
                            _qMgr.CreateQuestion(item);
                    }

                }
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
                this.ckbRequired.Checked = question.IsRequired;
                this.ddlQtype.SelectedValue = question.QType.ToString();
                this.hfNowQuestionID.Value = question.QuestionID.ToString();
            }

        }

        protected void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            List<Guid> deleteQuestionIdList = new List<Guid>();
            foreach (GridViewRow gRow in this.GridViewQuestionList.Rows)
            {
                CheckBox ckbDel = gRow.FindControl("ckbDel") as CheckBox;
                HiddenField hfID = gRow.FindControl("hfQuestionID") as HiddenField;

                if (ckbDel != null && hfID != null)
                {
                    if (ckbDel.Checked)
                    {
                        if (Guid.TryParse(hfID.Value, out Guid id))
                            deleteQuestionIdList.Add(id);
                    }
                }
            }

            if (deleteQuestionIdList.Count > 0)
            {
                foreach (var item in deleteQuestionIdList)
                {
                    QuestionModel question = _questionList.Find(x => x.QuestionID.ToString().Contains(item.ToString()));
                    _questionList.Remove(question);
                    _delIdList.Add(item);
                }
                HttpContext.Current.Session["DeleteList"] = _delIdList;
            }
            InitQuestionsList();
        }

        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

        private void ChangeStatus(PageStatus status)
        {
            this.lbtnPage01.Enabled = (status != PageStatus.Page01);   // 當括弧內為true/false
            this.lbtnPage02.Enabled = (status != PageStatus.Page02);
            this.lbtnPage03.Enabled = (status != PageStatus.Page03);

            this.page01.Visible = (status == PageStatus.Page01);
            this.page02.Visible = (status == PageStatus.Page02);
            this.page03.Visible = (status == PageStatus.Page03);
        }

        protected void lbtnPage01_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Page01);
        }

        protected void lbtnPage02_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Page02);
        }

        protected void lbtnPage03_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Page03);
        }

        protected void lbtnPage04_Click(object sender, EventArgs e)
        {
            this.ChangeStatus(PageStatus.Page04);
        }

        protected void GridAnswerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoLbtn")
            {
                string commentIdText = e.CommandArgument as string;
                this.plcPage03_1.Visible = false;
                this.plcPage03_2.Visible = true;
                // 讀出作答內容
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}