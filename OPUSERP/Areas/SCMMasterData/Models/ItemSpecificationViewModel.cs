using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ItemSpecificationViewModel
    {
        public int itemIdspec { get; set; }
        public string[] itemSpecificationName { get; set; }
        public int?[] specificationCategoryId { get; set; }
        public string[] CategoryValue { get; set; }
    }
}
