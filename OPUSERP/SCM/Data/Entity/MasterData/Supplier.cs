using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("Suppliers", Schema = "SCM")]
    public class Supplier:Base
    {
        public int? supplierTypeId { get; set; }
        public SupplierType supplierType { get; set; }

        public int? organizationTypeId { get; set; }
        public OrganizationType organizationType { get; set; }

        [MaxLength(250)]
        public string supplierName { get; set; }
        [MaxLength(20)]
        public string supplierCode { get; set; }
        [MaxLength(20)]
        public string tinNo { get; set; }
        [MaxLength(20)]
        public string vatRegNo { get; set; }
    }
}
