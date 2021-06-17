using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("LoanScheduleDetail", Schema = "Payroll")]
    public class LoanScheduleDetail:Base
    {
        public int? loanScheduleMasterId { get; set; }
        public LoanScheduleMaster loanScheduleMaster { get; set; }

        public DateTime? scheduleDate { get; set; }
        public DateTime? paidDate { get; set; }
        public decimal? scheduleAmount { get; set; }
        public decimal? paidAmount { get; set; }

        [DefaultValue(0)]
        public int? isPaid { get; set; }
    }
}
