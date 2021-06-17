using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Data.Entity.Budget
{
    [Table("CompanyBudgets", Schema = "CRM")]
    public class CompanyBudgets:Base
    {
        public int? yearId { get; set; }
        public Year year { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }
        public decimal? initialAmount { get; set; }
        public decimal? surveillence { get; set; }
    }
}
