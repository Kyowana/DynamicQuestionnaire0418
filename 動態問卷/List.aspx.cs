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
    public partial class List : System.Web.UI.Page
    {
        private QuestionnaireManager _qMgr = new QuestionnaireManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            string keyword = this.Request.QueryString["keyword"];

            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    this.txtTitle.Text = keyword;
                    List<SummaryModel> sList = this._qMgr.GetSearchedList(keyword);
                    this.GridQList.DataSource = sList;
                    this.GridQList.DataBind();
                }
                else
                {
                    List<SummaryModel> qList = _qMgr.GetQList();
                    this.GridQList.DataSource = qList;
                    this.GridQList.DataBind();

                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtTitle.Text.Trim();

            if (string.IsNullOrWhiteSpace(this.txtTitle.Text))
                this.Response.Redirect("List.aspx");
            else
                Response.Redirect("List.aspx?keyword=" + keyword);
        }
    }
}