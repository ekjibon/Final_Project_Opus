using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class ReportFormatViewModel
    {
        public int reportFormatId { get; set; }
        public string reportTypeName { get; set; }
        public string reportFormat { get; set; }        

        public IEnumerable<ReportFormat> reportFormats{ get; set; }
        public ReportFormat reportFormatbyname { get; set; }
    }
}
