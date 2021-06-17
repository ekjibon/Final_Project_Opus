using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class BonusTypeViewModel
    {
        public int editId { get; set; }
        public string bonusTypeName { get; set; }        

        public IEnumerable<BonusType> bonusTypesList { get; set; }
        public BonusType bonusType { get; set; }
    }
}
