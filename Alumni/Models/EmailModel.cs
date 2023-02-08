using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alumni.Models
{
    public class EmailModel
    {
        public string pid { get; set; }
        
        public string emailid { get; set; }

        public string actiontype { get; set; }

        public string toaddr { get; set; }

        public string toname { get; set; }

        public string strSystem { get; set; }

        public string subject { get; set; }

        public string remark { get; set; }

        public string body { get; set; }

        public string attch { get; set; }
    }
}