using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 動態問卷.Models
{
    public class SummaryModel
    {
        public Guid QID { get; set; }
        public int SerialNumber { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ViewLimit { get; set; }
    }
}