using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class SCMFeatureItemViewModel
    {
        public int Id { get; set; }
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public string userId { get; set; }
        public DateTime? date { get; set; }
        public int? statusId { get; set; }
        public int? shortOrder { get; set; }

        public IEnumerable<FeatureItem>  featureItems { get; set; }
    }
}
