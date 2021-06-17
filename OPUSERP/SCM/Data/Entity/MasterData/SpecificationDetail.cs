using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("SpecificationDetail", Schema = "SCM")]
    public class SpecificationDetail:Base
    {
        public int? specificationCategoryId { get; set; }
        public SpecificationCategory specificationCategory { get; set; }

        [MaxLength(250)]
        public string value { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
    }
}
