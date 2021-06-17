using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("PackageDetail", Schema = "Distribution")]
    public class PackageDetail : Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public int? quantity { get; set; }

        public decimal? discountPersent { get; set; }
        public int? packageMasterId { get; set; }
        public PackageMaster packageMaster { get; set; }
        public int? isfree { get; set; }
    }
}
