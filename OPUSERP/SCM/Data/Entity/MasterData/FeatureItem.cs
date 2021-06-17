using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("FeatureItems", Schema = "SCM")]
    public class FeatureItem:Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public string userId { get; set; }
        public DateTime? date { get; set; }
        public int? statusId { get; set; }
        public int? shortOrder { get; set; }
    }
}
