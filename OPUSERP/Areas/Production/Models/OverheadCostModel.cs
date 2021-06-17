using OPUSERP.Production.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Production.Models
{
    public class OverheadCostModel
    {
        public int? overheadCostId { get; set; }
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public int? haveLedgerRelation { get; set; }
        public int? ledgerRelationId { get; set; }
        public IEnumerable<OverheadCost> overheadCosts { get; set; }
    }
}
