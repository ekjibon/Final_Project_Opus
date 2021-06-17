using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("RelCustomerArea", Schema = "Distribution")]
    public class RelCustomerArea : Base
    {
        public int? areaId { get; set; }

        public Area area { get; set; }
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }
    }
}
