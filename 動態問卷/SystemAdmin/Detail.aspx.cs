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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            QuestionModel q = new QuestionModel()
            {
                //QID,
                Question = this.txtQuestion.Text,
                QType = Convert.ToInt32(this.ddlQtype.SelectedValue),
                IsRequired = this.ckbRequired.Checked
            };

            _qMgr.CreateQuestion(q);

            List<QuestionModel> questionList = new List<QuestionModel>();
            

        }

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            this.page1.Visible = false;
            this.page2.Visible = true;
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            this.page2.Visible = false;
            this.page3.Visible = true;
        }
    }
}