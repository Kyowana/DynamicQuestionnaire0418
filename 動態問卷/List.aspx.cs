﻿using System;
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
        private const int _pageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
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

                this.GridQList.DataSource = list;
                this.GridQList.DataBind();

                for (int i = 0; i < list.Count; i++)
                {
                    CheckBox cb = (CheckBox)GridQList.Rows[i].Cells[2].Controls[0];
                    if (!cb.Checked)
                    {
                        GridQList.Rows[i].Cells[1].Text = list[i].Caption;
                    }

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
            if (pageIndex <= pageCount)
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
    }
}