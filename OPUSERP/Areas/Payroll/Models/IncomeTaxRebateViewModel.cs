using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class IncomeTaxRebateViewModel
    {
        public int rebateIncomeTaxId { get; set; }
        public int slabTypeId { get; set; }
   

        public int taxYearId { get; set; }
   

        public decimal slabRebateAmount { get; set; }
        public decimal taxRate { get; set; }
        public int? sortOrder { get; set; }
    
        public string slabRebateText { get; set; }
        public IEnumerable<TaxYear> taxYearsList { get; set; }
        public TaxYear taxYear { get; set; }
        public IEnumerable<SlabRebate> slabRebates { get; set; }
        public SlabRebate slabRebate { get; set; }
        public IEnumerable<RebateSlabType> rebateSlabTypes { get; set; }
    }
}
