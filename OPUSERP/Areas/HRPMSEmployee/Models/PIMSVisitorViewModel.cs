using Microsoft.AspNetCore.Http;
using OPUSERP.HRPMS.Data.Entity.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PIMSVisitorViewModel
    {
        public int? Id { get; set; }
        public string subject { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string imageUrlstring { get; set; }
        public string passportUrlstring { get; set; }
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
        public IFormFile imageUrl { get; set; }
        public IFormFile passportUrl { get; set; }
        public string remarks { get; set; }

        public IEnumerable<PIMSVisitor> PIMSVisitors { get; set; }
        public PIMSVisitor pIMSVisitor { get; set; }
    }
}
