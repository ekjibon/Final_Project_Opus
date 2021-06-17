using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class AllMemberJson
    {
        public int memberId { get; set; }
        public string imageUrl { get; set; }
        public string companies { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string mobileNumberOffice { get; set; }
        public string emailAddress { get; set; }
        public string action { get; set; }
        public string organization { get; set; }
        public string address { get; set; }
        public string membertype { get; set; }
    }
}
