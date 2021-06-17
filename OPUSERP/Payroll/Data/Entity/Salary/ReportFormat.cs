using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("ReportFormat", Schema = "Payroll")]
    public class ReportFormat : Base
    {
        
        public string reportTypeName { get; set; }
        public string reportFormat { get; set; }
        

    }
}
