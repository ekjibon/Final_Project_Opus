using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("RelSupplierCustomerResourse", Schema = "OSales")]
    public class RelSupplierCustomerResourse:Base
    {
        public int? organizationId { get; set; }
        public Organization organization { get; set; } //for Supplier

        public int? resourceId { get; set; }
        public Resource resource { get; set; } //for Customer

        public int? roleTypeId { get; set; }
        public RoleType roleType { get; set; }

        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }

        public int? distributorTypeId { get; set; }
        public DistributorType distributorType { get; set; }

        public int? LeadsId { get; set; }
        public Leads Leads { get; set; }

    }
}
