using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("SpecificationCategory", Schema = "SCM")]
    public class SpecificationCategory:Base
    {
        [MaxLength(250)]
        public string specificationCategoryName { get; set; }

        public int? itemCategoryId { get; set; }
        public ItemCategory itemCategory { get; set; }
    }
}
