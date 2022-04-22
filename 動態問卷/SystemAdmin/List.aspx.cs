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

            if (!IsPostBack)
            {
                List<SummaryModel> qList = _qMgr.GetQList();
                this.GridQList.DataSource = qList;
                this.GridQList.DataBind();

            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> deleteQuestionnaireIdList = new List<Guid>();
            foreach (GridViewRow gRow in this.GridQList.Rows)
            {
                CheckBox ckbDel = gRow.FindControl("ckbDel") as CheckBox;
                HiddenField hfID = gRow.FindControl("hfID") as HiddenField;

                if (ckbDel != null && hfID != null)
                {
                    if (ckbDel.Checked)
                    {
                        if (Guid.TryParse(hfID.Value, out Guid id))
                            deleteQuestionnaireIdList.Add(id);
                    }
                }
            }

            if (deleteQuestionnaireIdList.Count > 0)
            {
                // 刪除：答案 → 題目 → 問卷 (寫在manager裡?)
                foreach (var item in deleteQuestionnaireIdList)
                {
                    _qMgr.DeleteSummary(item);

                }
                this.Response.Redirect(this.Request.RawUrl);    // 保留原?search=參數(使看得出有刪除)
            }
        }
    }
}