using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class AnswerContentModel
    {
        public Guid ID { get; set; }
        public Guid AnswerID { get; set; }
        public Guid QuestionID { get; set; }
        public string Answer { get; set; }
    }
}