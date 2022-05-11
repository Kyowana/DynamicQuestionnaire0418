using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
        private AnswerManager _aMgr = new AnswerManager();
        private Guid _QID;
        private List<QuestionModel> _questionList = new List<QuestionModel>();
        private List<Guid> _delIdList = new List<Guid>();
        private SummaryModel _qs;
        private static bool _isEditMode;
        private int _questionNumber = 1;
        private const int _pageSize = 10;
        private List<AnswerSummaryModel> _asList = new List<AnswerSummaryModel>();

        private FaqManager _fMgr = new FaqManager();
        private List<FaqModel> _faqList = new List<FaqModel>();
        private enum PageStatus
        {
            Page01,
            Page02,
            Page03,
            Page04
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageIndexText = this.Request.QueryString["page"];
            int pageIndex = (string.IsNullOrWhiteSpace(pageIndexText)) ? 1 : Convert.ToInt32(pageIndexText);
            if (!string.IsNullOrWhiteSpace(pageIndexText))
            {
                this.page01.Visible = false;
                this.page03.Visible = true;
            }

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

            _faqList = _fMgr.GetFaqList();

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

                    InitFaqList();
                }

                _asList = _aMgr.GetAList(questionnaireID, _pageSize, pageIndex, out int totalRows);
                this.ProcessPager(pageIndex, totalRows);
                InitAnswerList();

                InitStasticPage();

                if (_aMgr.GetAList(_QID).Count > 0)
                {
                    this.ddlFaq.Enabled = false;
                    this.txtQuestion.Enabled = false;
                    this.ddlQtype.Enabled = false;
                    this.ckbRequired.Enabled = false;
                    this.txtAnswer.Enabled = false;
                    this.btnAdd.Enabled = false;
                    this.btnDeleteQuestion.Enabled = false;
                    this.lblCannotEditMsg.Visible = true;                    
                }

            }
            else
            {
                _isEditMode = false;

                if (!this.IsPostBack)
                {
                    // Create mode
                    Guid newquestionnaireID = Guid.NewGuid();
                    HttpContext.Current.Session["ID"] = newquestionnaireID;

                    this.txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.ckbLimit.Checked = true;

                    if (_questionList.Count > 0)
                        InitQuestionsList();
                    else
                        this.plcNoQuestions.Visible = true;

                    InitFaqList();
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

        private void InitFaqList()
        {
            foreach (var item in _faqList)
            {
                ListItem listItem = new ListItem() { Value = $"{item.QuestionNumber}", Text = $"{item.Question}" };
                this.ddlFaq.Items.Add(listItem);
            }
        }

        private void InitStasticPage()
        {
            if (_aMgr.GetAList(_QID).Count == 0)
            {
                this.lblNoAnsMsg.Visible = true;
                return;
            }

            foreach (var item in _questionList)
            {
                this.plcQuestions.Controls.Add(new Panel() { ID = $"panel{item.QuestionID}" });

                List<AnswerContentModel> acList = _aMgr.GetAnswerListIn1Question(item.QuestionID);

                switch (item.QType)
                {
                    case 1:
                        Literal ltlQuestion1 = new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" };
                        if (item.IsRequired)
                            ltlQuestion1.Text = _questionNumber + ". " + item.Question + " (必填) <br />";

                        FindControl($"panel{item.QuestionID}").Controls.Add(ltlQuestion1);
                        _questionNumber++;

                        string[] arrContent1 = item.AnswerOption.Trim().Split(';');
                        int rbdCount = 0;
                        foreach (var content in arrContent1)
                        {
                            Literal ltlRdbOption = new Literal() { Text = content + "<br />" };
                            FindControl($"panel{item.QuestionID}").Controls.Add(ltlRdbOption);

                            FindControl($"panel{item.QuestionID}").Controls.Add(new Panel() { ID = $"{item.QuestionID}_rdb{rbdCount}", CssClass = "frame" });

                            int c = acList.Count(x => x.Answer.ToString().Contains($"{item.QuestionID}_AnsRdbOption{rbdCount}"));
                            int ttl = 0;
                            foreach (var ac in acList)
                            {
                                if (!string.IsNullOrEmpty(ac.Answer))
                                    ttl++;
                            }

                            Panel pnl = new Panel() { CssClass = "strip", ID = $"pnl{item.QuestionID}_AnsRdbOption{rbdCount}" };
                            decimal ratio = 0;
                            if (ttl > 0)
                                ratio = Math.Round((decimal)c / ttl, 2);

                            pnl.Style["width"] = $"{ratio * 100}%";
                            FindControl($"{item.QuestionID}_rdb{rbdCount}").Controls.Add(pnl);
                            FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = $"{ratio * 100} % ({c}) <br /><br />" });

                            rbdCount++;
                        }

                        break;

                    case 2:
                        Literal ltlQuestion2 = new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" };
                        if (item.IsRequired)
                            ltlQuestion2.Text = _questionNumber + ". " + item.Question + " (必填) <br />";

                        FindControl($"panel{item.QuestionID}").Controls.Add(ltlQuestion2);
                        _questionNumber++;

                        string[] arrContent2 = item.AnswerOption.Trim().Split(';');
                        int ckbCount = 0;
                        foreach (var content in arrContent2)
                        {
                            Literal ltlRdbOption = new Literal() { Text = content + "<br />" };
                            FindControl($"panel{item.QuestionID}").Controls.Add(ltlRdbOption);

                            FindControl($"panel{item.QuestionID}").Controls.Add(new Panel() { ID = $"{item.QuestionID}_ckb{ckbCount}", CssClass = "frame" });

                            int c = acList.Count(x => x.Answer.ToString().Contains($"{item.QuestionID}_AnsCkbOption{ckbCount}"));
                            int ttl = 0;
                            foreach (var ac in acList)
                            {
                                if (!string.IsNullOrEmpty(ac.Answer))
                                    ttl++;
                            }

                            Panel pnl = new Panel() { CssClass = "strip", ID = $"pnl{item.QuestionID}_AnsCkbOption{ckbCount}" };
                            decimal ratio = 0;
                            if (ttl > 0)
                                ratio = Math.Round((decimal)c / ttl, 2);

                            pnl.Style["width"] = $"{ratio * 100}%";
                            FindControl($"{item.QuestionID}_ckb{ckbCount}").Controls.Add(pnl);
                            FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = $"{ratio * 100} % ({c}) <br /><br />" });

                            ckbCount++;
                        }
                        break;

                    case 3:
                        _questionNumber++;
                        break;

                    default:
                        break;
                }
            }
        }

        private void InitQuestionsList()
        {
            this.GridViewQuestionList.DataSource = _questionList;
            this.GridViewQuestionList.DataBind();
        }

        private void InitAnswerList()
        {
            for (int i = 0; i < _asList.Count; i++)
            {
                _asList[i].SerialNumber = _asList.Count - i;
            }
            
            this.GridAnswerList.DataSource = _asList;
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
            this.ddlFaq.SelectedValue = "0";
            ClearInput();

            InitQuestionsList();



        }

        private void ClearInput()
        {
            this.hfNowQuestionID.Value = "";
            this.txtQuestion.Text = "";
            this.txtAnswer.Text = "";
            this.ckbRequired.Checked = false;
            this.ddlQtype.SelectedValue = "1";
        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCaption.Text) || string.IsNullOrWhiteSpace(txtStartDate.Text) || string.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                this.lblPage1Msg.Visible = true;
                return;
            }
            else
                this.lblPage1Msg.Visible = false;

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

            //if (!_isEditMode)
            //    _qMgr.CreateQuestionnaire(_qs);
            //else
            //    _qMgr.UpdateSummary(_qs);
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
                _qMgr.CreateQuestionnaire(_qs);
            else
                _qMgr.UpdateSummary(_qs);

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

            Session.Remove("Summary");
            Session.Remove("AddList");
            Session.Remove("DeleteList");

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
            this.lbtnPage04.Enabled = (status != PageStatus.Page04);

            this.page01.Visible = (status == PageStatus.Page01);
            this.page02.Visible = (status == PageStatus.Page02);
            this.page03.Visible = (status == PageStatus.Page03);
            this.page04.Visible = (status == PageStatus.Page04);
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
            this.plcPage03_1.Visible = true;
            this.plcPage03_2.Visible = false;
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
                AnswerSummaryModel asModel = _asList.Find(x => x.AnswerID.ToString().Contains(commentIdText));
                this.txtName.Text = asModel.Name;
                this.txtPhone.Text = asModel.Phone;
                this.txtEmail.Text = asModel.Email;
                this.txtAge.Text = asModel.Age.ToString();
                this.lblSubmitDate.Text = "填寫時間" + asModel.SubmitDate.ToString("yyyy/MM/dd HH:mm:ss");

                _questionList = _qMgr.GetQuestionsList(_QID);

                foreach (var item in _questionList)
                {
                    AnswerContentModel acModel = _aMgr.GetAnswerContent(item.QuestionID, asModel.AnswerID);

                    this.plcPage03_2.Controls.Add(new Panel() { ID = $"panelPage3{item.QuestionID}" });

                    Literal ltlQuestion = new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" };
                    if (item.IsRequired)
                        ltlQuestion.Text = _questionNumber + ". " + item.Question + " (必填) <br />";

                    FindControl($"panelPage3{item.QuestionID}").Controls.Add(ltlQuestion);
                    _questionNumber++;

                    switch (item.QType)
                    {
                        case 1:
                            string[] arrContent = item.AnswerOption.Trim().Split(';');
                            int rdbCount = 0;
                            foreach (var content in arrContent)
                            {
                                RadioButton rdb = new RadioButton() { ID = $"{item.QuestionID}_AnsRdbOption{rdbCount}", Text = content + "<br />", GroupName = $"op{item.QuestionID}", Enabled = false };
                                FindControl($"panelPage3{item.QuestionID}").Controls.Add(rdb);

                                if (acModel != null)
                                {
                                    string[] arrOption1 = acModel.Answer.Trim().Split(';');
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
                                CheckBox ckb = new CheckBox() { ID = $"{item.QuestionID}_AnsCkbOption{ckbCount}", Text = content + "<br />", Enabled = false };
                                FindControl($"panelPage3{item.QuestionID}").Controls.Add(ckb);

                                if (acModel != null)
                                {
                                    string[] arrOption1 = acModel.Answer.Trim().Split(';');
                                    if (arrOption1.Contains(ckb.ID))
                                        ckb.Checked = true;
                                }
                                ckbCount++;
                            }
                            break;

                        case 3:
                            TextBox txb = new TextBox() { ID = $"AnsTxt_{item.QuestionID}", Enabled = false };
                            FindControl($"panelPage3{item.QuestionID}").Controls.Add(txb);
                            FindControl($"panelPage3{item.QuestionID}").Controls.Add(new Literal() { Text = "<br />" });

                            if (acModel != null)
                            {
                                txb.Text = acModel.Answer;
                            }
                            break;

                        default:
                            break;
                    }
                }

                this.plcPage03_1.Visible = false;
                this.plcPage03_2.Visible = true;
                // 讀出作答內容
            }
        }

        private void ProcessPager(int pageIndex, int totalRows)
        {
            int pageCount = (totalRows / _pageSize);
            if ((totalRows % _pageSize) > 0)
                pageCount += 1;

            string url = Request.Url.LocalPath;


            this.aLinkFirst.HRef = url + "?ID=" + _QID + "&page=1";
            this.aLinkPrev.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex - 1);
            if (pageIndex <= pageCount)
                this.aLinkPrev.Visible = false;

            this.aLinkNext.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex + 1);
            if (pageIndex >= pageCount)
                this.aLinkNext.Visible = false;


            this.aLinkPage1.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex - 2);
            this.aLinkPage1.InnerText = (pageIndex - 2).ToString();
            if (pageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex - 1);
            this.aLinkPage2.InnerText = (pageIndex - 1).ToString();
            if (pageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = pageIndex.ToString();

            this.aLinkPage4.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex + 1);
            this.aLinkPage4.InnerText = (pageIndex + 1).ToString();
            if ((pageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?ID=" + _QID + "&page=" + (pageIndex + 2);
            this.aLinkPage5.InnerText = (pageIndex + 2).ToString();
            if ((pageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;

            this.aLinkLast.HRef = url + "?ID=" + _QID + "&page=" + pageCount;

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (_aMgr.GetAList(_QID).Count == 0)
            {
                this.lblNoData.Visible = true;
                return;
            }

            //存檔到指定目錄
            if(!Directory.Exists("C:\\Test"))
                Directory.CreateDirectory("C:\\Test");
            string filePath = $"C:\\Test\\{_qs.Caption}{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";
            //string filePath = $"D:\\CSharpClass\\{_qs.Caption}{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";

            DataTable dt = new DataTable();
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("電話", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("年齡", typeof(string));
            dt.Columns.Add("送出時間", typeof(string));

            foreach (var question in _questionList)
            {
                dt.Columns.Add(question.Question, typeof(string));
            }

            List<AnswerSummaryModel> asList = _aMgr.GetAList(_QID);

            for (int i = 0; i < asList.Count; i++)  //第 i 人的答案
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                dr[0] = asList[i].Name;
                dr[1] = asList[i].Phone;
                dr[2] = asList[i].Email;
                dr[3] = asList[i].Age;
                dr[4] = asList[i].SubmitDate;

                for (int m = 0; m < _questionList.Count; m++)  // 第 m 題
                {
                    AnswerContentModel ac = _aMgr.GetAnswerContent(_questionList[m].QuestionID, asList[i].AnswerID);

                    dr[5 + m] = ac.Answer;

                    int qType = _qMgr.FindQuestion(ac.QuestionID).QType;
                    if (qType != 3)
                    {
                        string[] arrAnswer = _qMgr.FindQuestion(ac.QuestionID).AnswerOption.Trim().Split(';');
                        string[] arrAnswerOptionID = ac.Answer.Trim().Split(';');
                        List<string> listOptionNumber = new List<string>();
                        List<string> listOptionContent = new List<string>();

                        for (int k = 0; k < (arrAnswerOptionID.Length - 1); k++)
                        {
                            listOptionNumber.Add(arrAnswerOptionID[k].Remove(0, 49));
                        }
                        for (int j = 0; j < arrAnswer.Length; j++)
                        {
                            //if (Convert.ToInt32(listOptionNumber.Count) > j)
                            //{
                            for (int n = 0; n < listOptionNumber.Count; n++)
                            {
                                if (Convert.ToInt32(listOptionNumber[n]) == j)
                                {
                                    listOptionContent.Add(arrAnswer[j]);
                                }

                            }

                            //}
                        }
                        string stringOptionContent = string.Join(";", listOptionContent);
                        dr[5 + m] = stringOptionContent;
                    }

                }
            }

            SaveCsv(dt, filePath);

            this.lblCompleteExport.Text = "已將作答結果匯出至C:\\Test資料夾";
            this.lblCompleteExport.Visible = true;
        }

        public static void SaveCsv(DataTable dt, string filePath)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(filePath + dt.TableName + ".csv", FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                var data = string.Empty;
                //寫出列名稱
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName;
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                //寫出各行資料
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    data = string.Empty;
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        data += dt.Rows[i][j].ToString();
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        protected void ddlFaq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlFaq.SelectedValue == "0")
            {
                ClearInput();
            }
            else
            {
                for (int i = 0; i < _faqList.Count; i++)
                {
                    if (this.ddlFaq.SelectedValue == _faqList[i].QuestionNumber.ToString())
                    {
                        this.txtQuestion.Text = _faqList[i].Question;
                        this.txtAnswer.Text = _faqList[i].AnswerOption;
                        this.ddlQtype.SelectedValue = _faqList[i].QType.ToString();
                        this.ckbRequired.Checked = _faqList[i].IsRequired;

                    }
                }

            }

        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Session.Remove("Summary");
            Session.Remove("AddList");
            Session.Remove("DeleteList");
            ClearInput();
        }
    }
}