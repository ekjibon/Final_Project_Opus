using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMReport.Models
{
    public class ItemSummaryViewModel
    {
        //Item summary report
        public Unit unit { get; set; }
        [MaxLength(250)]
        public string unitName { get; set; }

        [MaxLength(250)]
        public string itemName { get; set; }
        public Item Item { get; set; }

        //RequsitionDetails
        public Decimal? openingBalance { get; set; }

        public int? stockStatusId { get; set; } //1 for stockin 2 for stock out 3 for opening stock in

        public int PurchaseReturn { get; set; }
        public int IssueReturn { get; set; }
        public int Damages { get; set; }
        public int Obsolete { get; set; }
        //RequsitionDetails
        public Decimal? ClosingBalance { get; set; }

        //public RequisitionMaster requisitionMaster { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public RequisitionDetail requisitionDetails { get; set; }
        public ItemSpecification ItemSpecification { get; set; }
        public StockMaster stockMaster { get; set; }
        public StockDetails stockDetails { get; set; }

        public IEnumerable<Company> companies { get; set; }

        //Item wise issue details
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IsuueDate { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public string ReceivedBy { get; set; }
        public Decimal? Qty { get; set; }

    }
}
