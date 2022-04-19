using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class QuestionModel
    {
        public Guid QID { get; set; }
        public Guid QuestionID { get; set; }
        public int QuestionNumber { get; set; }
        public string Question { get; set; }
        public int QType { get; set; }    // 改成enum
        public bool IsRequired { get; set; }
        public DateTime CreateDate { get; set; }
    }
}