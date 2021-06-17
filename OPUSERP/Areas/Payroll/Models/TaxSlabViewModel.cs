using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class TaxSlabViewModel
    {
        public int slabId { get; set; }       
        public string slabTypeName { get; set; }
    
        public IEnumerable<SlabType> slabTypes { get; set; }
        public SlabType slabType { get; set; }
   
    }
}
