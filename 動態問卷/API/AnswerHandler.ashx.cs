using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 動態問卷.Managers;
using 動態問卷.Models;

namespace 動態問卷.API
{
    /// <summary>
    /// AnswerHandler 的摘要描述
    /// </summary>
    public class AnswerHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        public void ProcessRequest(HttpContext context)
        {
            // 回答者資料+內容
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("SendAnswer", context.Request.QueryString["Action"], true) == 0)
            {
                string name = context.Request.Form["Name"];
                string phone = context.Request.Form["Phone"];
                string email = context.Request.Form["Email"];
                string age = context.Request.Form["Age"];

                string answerContents = context.Request.Form["AnswerContents"];
                string[] arrAnswer = answerContents.Trim().Split(',');

                Guid answerID = Guid.NewGuid();

                string qIDString = context.Request.Form["QID"]; ;
                if (Guid.TryParse(qIDString, out Guid qID))
                {
                    List<QuestionModel> questionList = _qMgr.GetQuestionsList(qID);
                    List<Guid> questionGuidList = new List<Guid>();

                    foreach (var item in questionList)
                    {
                        questionGuidList.Add(item.QuestionID);
                    }

                    Guid[] arrQuestionID = questionGuidList.ToArray();

                    List<AnswerContentModel> acList = new List<AnswerContentModel>();
                    for (int i = 0; i < questionGuidList.Count; i++)
                    {
                        AnswerContentModel ac = new AnswerContentModel()
                        {
                            AnswerID = answerID,
                            QuestionID = arrQuestionID[i],
                            Answer = arrAnswer[i],
                        };
                        acList.Add(ac);
                    }

                    HttpContext.Current.Session["AnswerList"] = acList;

                    AnswerSummaryModel asModel = new AnswerSummaryModel()
                    {
                        AnswerID = answerID,
                        QID = qID,
                        Name = name,
                        Phone = phone,
                        Email = email,
                        Age = Convert.ToInt32(age)
                    };

                    HttpContext.Current.Session["UserInfo"] = asModel;

                    context.Response.ContentType = "text/plain";
                    context.Response.Write("OK");
                }
                else
                    return;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}