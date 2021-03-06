using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using 動態問卷.Managers;
using 動態問卷.Models;

namespace 動態問卷
{
    public partial class Stastic : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        private AnswerManager _aMgr = new AnswerManager();
        private List<QuestionModel> _questionList = new List<QuestionModel>();

        private int _questionNumber = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];

            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                SummaryModel qs = _qMgr.GetQuestionnaireSummary(questionnaireID);
                this.lblCaption.Text = qs.Caption;

                _questionList = _qMgr.GetQuestionsList(questionnaireID);

                if (_aMgr.GetAList(questionnaireID).Count == 0)
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
                            FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" });
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
                                FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = $"{ratio*100} % ({c}) <br /><br />" });

                                rbdCount++;
                            }

                            break;

                        case 2:
                            FindControl($"panel{item.QuestionID}").Controls.Add(new Literal() { Text = _questionNumber + ". " + item.Question + "<br />" });
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




        }
    }
}