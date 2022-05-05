using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class CSVModel
    {
        public Guid AnswerID { get; set; }
        public Guid QID { get; set; }
        //public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime SubmitDate { get; set; }
        public string Question { get; set; }
        public string AnswerOption { get; set; }
        public Guid QuestionID { get; set; }
        public string Answer { get; set; }
    }
}