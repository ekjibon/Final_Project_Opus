using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class CompanyBudgetViewModel
    {
        public int Id { get; set; }
        public string ratingYearId { get; set; }
        public int? companyId { get; set; }
        public decimal? initialAmount { get; set; }
        public decimal? surveillanceAmount { get; set; }
        public IEnumerable<Year> years { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<CompanyBudgets> companyBudgets { get; set; }
    }
}
