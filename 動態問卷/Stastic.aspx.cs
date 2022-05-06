using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 動態問卷
{
    public partial class Stastic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string questionnaireIDString = Request.QueryString["ID"];

            if (Guid.TryParse(questionnaireIDString, out Guid questionnaireID))
            {

            }




        }
    }
}