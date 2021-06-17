using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class GratuityReportViewModel
    {
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public string joiningDate { get; set; }
        public string uptoDate { get; set; }
       
        public decimal? grossAmount { get; set; }
        public int? basicAmount { get; set; }
        public decimal? totalDay { get; set; }
        public decimal? fractionalYear { get; set; }
        public decimal? roundYear { get; set; }
        public decimal? gratuityAmount { get; set; }
       
    }
}
