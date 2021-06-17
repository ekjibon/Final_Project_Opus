using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class RebateSlabTypeViewModel
    {
        public int rebateslabTypeId { get; set; }
        public string slabTypeName { get; set; }
        public decimal? minValue { get; set; }
        public decimal? maxValue { get; set; }

        public IEnumerable<RebateSlabType> rebateSlabTypes { get; set; }
        public RebateSlabType rebateSlabType { get; set; }
   
    }
}
