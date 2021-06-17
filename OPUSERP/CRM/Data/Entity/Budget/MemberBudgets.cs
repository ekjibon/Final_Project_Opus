using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Data.Entity.Budget
{
    [Table("MemberBudgets", Schema = "CRM")]
    public class MemberBudgets:Base
    {
        public int? teamBudgetsId { get; set; }
        public TeamBudgets teamBudgets { get; set; }

        public int? teamId { get; set; }
        public Team team { get; set; }

        public decimal? initialAmount { get; set; }
        public decimal? surveillence { get; set; }

    }
}
