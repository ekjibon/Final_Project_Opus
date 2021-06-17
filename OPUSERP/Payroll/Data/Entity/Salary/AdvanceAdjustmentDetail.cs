using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("AdvanceAdjustmentDetail", Schema = "Payroll")]
    public class AdvanceAdjustmentDetail:Base
    {
        public int? advanceAdjustmentId { get; set; }
        public AdvanceAdjustment advanceAdjustment { get; set; }

        public DateTime? scheduleDate { get; set; }
        public DateTime? paidDate { get; set; }
        public decimal? scheduleAmount { get; set; }
        public decimal? paidAmount { get; set; }

        [DefaultValue(0)]
        public int? isPaid { get; set; }
    }
}
