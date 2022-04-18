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
    public partial class List : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();
        protected void Page_Load(object sender, EventArgs e)
        {


            List<SummaryModel> qList = _qMgr.GetQList();
            this.rptFormList.DataSource = qList;
            this.rptFormList.DataBind();
        }

        protected void rptFormList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if ()
        }
    }
}