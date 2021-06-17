using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Visitor
{
    [Table("PIMSVisitor", Schema = "HR")]
    public class PIMSVisitor:Base
    {
        public string subject { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string nationality { get; set; }
        public string gander { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string organization { get; set; }
        public string organizationPhone { get; set; }
        public string organizationEmail { get; set; }
        public string organizationAddress { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public DateTime? letterSendingDate { get; set; }
        public DateTime? arrivalDate { get; set; }
        public DateTime? deparchurDate { get; set; }
        public string passportNumber { get; set; }
        public string imageUrl { get; set; }
        public string passportUrl { get; set; }
        public string remarks { get; set; }
    }
}
