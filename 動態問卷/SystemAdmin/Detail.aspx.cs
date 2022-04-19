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
        private List<QuestionModel> _questionList;
        private SummaryModel _qs;
        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (HttpContext.Current.Session["ID"] != null)
                _QID = (Guid)HttpContext.Current.Session["ID"];

            _qs = HttpContext.Current.Session["Summary"] as SummaryModel;

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
            _questionList = HttpContext.Current.Session["AddList"] as List<QuestionModel>;
            if (_questionList != null)
            {
                this.GridViewQuestionList.DataSource = _questionList;
                this.GridViewQuestionList.DataBind();

            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.page01.Visible = false;
            this.page02.Visible = true;

            _questionList = new List<QuestionModel>();
            QuestionModel q = new QuestionModel()
            {
                QID = _QID,
                Question = this.txtQuestion.Text,
                QType = Convert.ToInt32(this.ddlQtype.SelectedValue),
                IsRequired = this.ckbRequired.Checked,
                CreateDate = DateTime.Now
            };

            _questionList.Add(q);
            HttpContext.Current.Session["AddList"] = _questionList;

            



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
            _qMgr.CreateQuestion(_questionList);
            this.page02.Visible = false;
            this.page03.Visible = true;
        }

        protected void ddlQtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// 如何立即改變??

            //if (this.ddlQtype.SelectedValue == "1" || this.ddlQtype.SelectedValue == "2")
            //    this.plcOptions.Visible = true;
            //else
            //    this.plcOptions.Visible = false;

        }

        protected void btnAddOptions_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(this.txtCtlNumber.Text);
            for (int i = 0; i < n; i++)
            {
                this.plcOptions.Controls.Add(new Literal() { ID = "ltl" + i, Text = "<br />第" + (i + 1) + "個選項" });
                this.plcOptions.Controls.Add(new TextBox() { ID = "txtOption" + i });
            }
            // 加入清除鈕

        }
    }
}