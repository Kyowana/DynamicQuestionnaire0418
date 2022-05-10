using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class FaqModel
    {
        public Guid FaqID { get; set; }
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public string AnswerOption { get; set; }
        public int QType { get; set; }
        public bool IsRequired { get; set; }
    }
}