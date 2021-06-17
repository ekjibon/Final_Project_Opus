using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("ItemCategory", Schema = "SCM")]
    public class ItemCategory:Base
    {
        public int? parentId { get; set; }

        public int? isParent { get; set; }

        [MaxLength(250)]
        public string categoryName { get; set; }

        public string categoryDescription { get; set; }
        public string categoryPrefix { get; set; }
    }
}
