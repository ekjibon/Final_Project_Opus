using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class SpecificationCategoryViewModel
    {
        public int specificationCategoryId { get; set; }

        public string specificationCategoryName { get; set; }
        public int? parentId { get; set; }

        public IEnumerable<SpecificationCategory> specificationCategories { get; set; }
    }
}
