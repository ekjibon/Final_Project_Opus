using OPUSERP.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class UniversalCodaXLTempleteViewModel
    {
        public string date { get; set; }
        public string year { get; set; }
        public int? salaryPeriodId { get; set; }
        public string documentDescription { get; set; }
        public string cc { get; set; }
        public string doc { get; set; }
        public string docnum { get; set; }
        public string line { get; set; }
        public string el1 { get; set; }
        public string el2 { get; set; }
        public string el3 { get; set; }
        public string el4 { get; set; }
        public string cur { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public string linedescription { get; set; }
 



    }
}
