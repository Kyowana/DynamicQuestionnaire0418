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
            // 回答者資料
            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("SendAnswer", context.Request.QueryString["Action"], true) == 0)
            {
                // 回答內容
                string answerContents = context.Request.Form["AnswerContents"];
                string[] arrAnswer = answerContents.Trim().Split(',');

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
                            QuestionID = arrQuestionID[i],
                            Answer = arrAnswer[i],
                        };
                        acList.Add(ac);
                    }

                    HttpContext.Current.Session["AnswerList"] = acList;



                    //AnswerSummaryModel asModel = new AnswerSummaryModel()
                    //{
                    //    QID = qID,
                    //    Name = name,
                    //    Phone = phone,
                    //    Email = email,
                    //    Age = Convert.ToInt32(age)
                    //};
                    // Manager 存入session
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