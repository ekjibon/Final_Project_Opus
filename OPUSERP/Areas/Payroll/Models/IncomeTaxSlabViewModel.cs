using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class IncomeTaxSlabViewModel
    {
        public int slabIncomeTaxId { get; set; }
        public int slabTypeId { get; set; }
   

        public int taxYearId { get; set; }
   
        public int employeeInfoId { get; set; }
        public decimal slabAmount { get; set; }
        public decimal taxRate { get; set; }
        public int? sortOrder { get; set; }
    
        public string slabText { get; set; }
        public IEnumerable<TaxYear> taxYearsList { get; set; }
        public TaxYear taxYear { get; set; }
        public IEnumerable<SlabIncomeTax> slabIncomeTaxes { get; set; }
        public IEnumerable<SlabIncomeTaxAssign> slabIncomeTaxeAssigns { get; set; }
        public SlabIncomeTax slabIncomeTax { get; set; }
        public IEnumerable<SlabType> slabTypes { get; set; }
    }
}
