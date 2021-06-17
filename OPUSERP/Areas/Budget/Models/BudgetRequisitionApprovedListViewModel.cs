using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class BudgetRequisitionApprovedListViewModel
    {
        public string requsitionNo { get; set; }
        public int? Id { get; set; }
        public string yearName { get; set; }
        public DateTime? requsitionDate { get; set; }
        public int? status { get; set; }
        public decimal? grandTotal { get; set; }
    }
}
