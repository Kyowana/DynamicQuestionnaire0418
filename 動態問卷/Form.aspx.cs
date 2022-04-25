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

        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];
            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {
                SummaryModel qs = _qMgr.GetQuestionnaireSummary(questionnaireID);
                List<QuestionModel> questionList = _qMgr.GetQuestionsList(questionnaireID);

                this.lblCaption.Text = qs.Caption;
                this.lblDescription.Text = qs.Description;
                this.lblLimit.Text = qs.ViewLimit.ToString();
                this.lblDate.Text = qs.StartDate.ToString() + "～" + qs.EndDate.ToString();

            }
        }
    }
}