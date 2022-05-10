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
    public partial class FaqList : System.Web.UI.Page
    {
        private FaqManager _fMgr = new FaqManager();
        private List<FaqModel> _faqList = new List<FaqModel>();
        protected void Page_Load(object sender, EventArgs e)
        {

            _faqList = _fMgr.GetFaqList();

            if (!IsPostBack)
            {
                InitFaqList();
            }

        }

        private void InitFaqList()
        {
            this.GridViewFaqList.DataSource = _faqList;
            this.GridViewFaqList.DataBind();
        }

        protected void GridViewFaqList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditButton")
            {
                string commentIdText = e.CommandArgument as string;
                FaqModel faq = _faqList.Find(x => x.FaqID.ToString().Contains(commentIdText));

                // 將值帶回上方
                this.txtQuestion.Text = faq.Question;
                this.txtAnswer.Text = faq.AnswerOption;
                this.ddlQtype.SelectedValue = faq.QType.ToString();
                this.ckbRequired.Checked = faq.IsRequired;
                this.hfNowFaqID.Value = faq.FaqID.ToString();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
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

            FaqModel faq = new FaqModel()
            {
                Question = this.txtQuestion.Text.Trim(),
                AnswerOption = this.txtAnswer.Text.Trim(),
                QType = Convert.ToInt32(this.ddlQtype.SelectedValue),
                IsRequired = this.ckbRequired.Checked
            };

            if (!string.IsNullOrWhiteSpace(this.hfNowFaqID.Value))
                _fMgr.UpdateFaq(faq);
            else
                _fMgr.CreateFaq(faq);


            // 清空輸入框內容
            this.hfNowFaqID.Value = "";
            this.txtQuestion.Text = "";
            this.txtAnswer.Text = "";
            this.ddlQtype.SelectedValue = "1";
            this.ckbRequired.Checked = false;

            this.Response.Redirect(this.Request.RawUrl);
        }

        protected void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            List<Guid> deleteFaqIdList = new List<Guid>();
            foreach (GridViewRow gRow in this.GridViewFaqList.Rows)
            {
                CheckBox ckbDel = gRow.FindControl("ckbDel") as CheckBox;
                HiddenField hfID = gRow.FindControl("hfFaqID") as HiddenField;

                if (ckbDel != null && hfID != null)
                {
                    if (ckbDel.Checked)
                    {
                        if (Guid.TryParse(hfID.Value, out Guid id))
                            deleteFaqIdList.Add(id);
                    }
                }
            }

            if (deleteFaqIdList.Count > 0)
            {
                foreach (var item in deleteFaqIdList)
                {
                    _fMgr.DeleteFaq(item);
                }
            }

            this.Response.Redirect(this.Request.RawUrl);
        }
    }
}