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
        private const int _pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["AddList"] != null)
                Session.Remove("AddList");
            if (HttpContext.Current.Session["Summary"] != null)
                Session.Remove("Summary");
            if (HttpContext.Current.Session["DeleteList"] != null)
                Session.Remove("DeleteList");
            if (HttpContext.Current.Session["ID"] != null)
                Session.Remove("ID");

            string pageIndexText = this.Request.QueryString["page"];
            int pageIndex = (string.IsNullOrWhiteSpace(pageIndexText)) ? 1 : Convert.ToInt32(pageIndexText);

            if (!IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                if (!string.IsNullOrWhiteSpace(keyword))
                    this.txtTitle.Text = keyword;
                string startDate = this.Request.QueryString["since"];
                if (!string.IsNullOrWhiteSpace(startDate))
                    this.txtStartDate.Text = startDate;

                string endDate = this.Request.QueryString["until"];
                if (!string.IsNullOrWhiteSpace(endDate))
                    this.txtEndDate.Text = endDate;

                var list = this._qMgr.GetQList(keyword, startDate, endDate, _pageSize, pageIndex, out int totalRows);
                this.ProcessPager(keyword, pageIndex, totalRows);

                foreach (var item in list)
                {
                    if (item.ViewLimit)
                    {
                        if (item.StartDate > DateTime.Now || item.EndDate.AddDays(1) < DateTime.Now)
                            item.ViewLimit = false;
                    }
                }

                this.GridQList.DataSource = list;
                this.GridQList.DataBind();

                if (list.Count == 0)
                    this.lblMsgNoList.Visible = true;

                for (int i = 0; i < list.Count; i++)
                {
                    if (GridQList.Rows[i].Cells[3].Text == "False")
                        GridQList.Rows[i].Cells[3].Text = "已關閉";
                    else
                        GridQList.Rows[i].Cells[3].Text = "開放";

                    //CheckBox cb = (CheckBox)GridQList.Rows[i].Cells[2].Controls[0];
                    //if (!cb.Checked)
                    //{
                    //    GridQList.Rows[i].Cells[1].Text = list[i].Caption;
                    //}

                }
            }

        }

        private void ProcessPager(string keyword, int pageIndex, int totalRows)
        {
            int pageCount = (totalRows / _pageSize);
            if ((totalRows % _pageSize) > 0)
                pageCount += 1;

            string url = Request.Url.LocalPath;
            string paramKeyword = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
                paramKeyword = "&keyword=" + keyword;

            this.aLinkFirst.HRef = url + "?page=1" + paramKeyword;
            this.aLinkPrev.HRef = url + "?page=" + (pageIndex - 1) + paramKeyword;
            if (pageIndex < pageCount)
                this.aLinkPrev.Visible = false;

            this.aLinkNext.HRef = url + "?page=" + (pageIndex + 1) + paramKeyword;
            if (pageIndex >= pageCount)
                this.aLinkNext.Visible = false;


            this.aLinkPage1.HRef = url + "?page=" + (pageIndex - 2) + paramKeyword;
            this.aLinkPage1.InnerText = (pageIndex - 2).ToString();
            if (pageIndex <= 2)
                this.aLinkPage1.Visible = false;

            this.aLinkPage2.HRef = url + "?page=" + (pageIndex - 1) + paramKeyword;
            this.aLinkPage2.InnerText = (pageIndex - 1).ToString();
            if (pageIndex <= 1)
                this.aLinkPage2.Visible = false;

            this.aLinkPage3.HRef = "";
            this.aLinkPage3.InnerText = pageIndex.ToString();

            this.aLinkPage4.HRef = url + "?page=" + (pageIndex + 1) + paramKeyword;
            this.aLinkPage4.InnerText = (pageIndex + 1).ToString();
            if ((pageIndex + 1) > pageCount)
                this.aLinkPage4.Visible = false;

            this.aLinkPage5.HRef = url + "?page=" + (pageIndex + 2) + paramKeyword;
            this.aLinkPage5.InnerText = (pageIndex + 2).ToString();
            if ((pageIndex + 2) > pageCount)
                this.aLinkPage5.Visible = false;

            this.aLinkLast.HRef = url + "?page=" + pageCount + paramKeyword;

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtTitle.Text.Trim();
            string startDate = this.txtStartDate.Text.Replace("/", "-");
            string endDate = this.txtEndDate.Text.Replace("/", "-");

            if (string.IsNullOrWhiteSpace(keyword))
            {
                if (string.IsNullOrWhiteSpace(startDate))
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                        this.Response.Redirect("List.aspx");
                    else
                        this.Response.Redirect("List.aspx?until=" + endDate);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                        this.Response.Redirect("List.aspx?since=" + startDate);
                    else
                        this.Response.Redirect($"List.aspx?since={startDate}&until={endDate}");
                }

            }
            else
            {
                if (string.IsNullOrWhiteSpace(startDate))
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                        Response.Redirect("List.aspx?keyword=" + keyword);
                    else
                        this.Response.Redirect($"List.aspx?keyword={keyword}&until={endDate}");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(endDate))
                        Response.Redirect($"List.aspx?keyword={keyword}&since={startDate}");
                    else
                        this.Response.Redirect($"List.aspx?keyword={keyword}&since={startDate}&until={endDate}");
                }
            }
        }

        protected void GridQList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GoP3Button")
            {
                HttpContext.Current.Session["Page3Visible"] = "";
                string commentIdText = e.CommandArgument as string;
                Response.Redirect($"Detail.aspx?ID={commentIdText}");
            }
        }
    }
}