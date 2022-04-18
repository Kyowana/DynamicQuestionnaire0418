using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class QuestionModel
    {
        public Guid QID { get; set; }
        public int QNumber { get; set; }
        public string Question { get; set; }
        public int QType { get; set; }
        public bool IsRequired { get; set; }
    }
}