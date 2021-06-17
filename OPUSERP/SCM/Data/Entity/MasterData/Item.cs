using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("Item", Schema = "SCM")]
    public class Item:Base
    {
        public int? parentId { get; set; }
        public ItemCategory parent { get; set; }

        public int? itemTypeId { get; set; }
        public ItemType itemType { get; set; }

        public int? unitId { get; set; }
        public Unit unit { get; set; }

        public int? isParent { get; set; }   
        [MaxLength(250)]
        public string itemName { get; set; }
        [MaxLength(20)]
        public string itemCode { get; set; }
        public string itemDescription { get; set; }
        public int? accountLedgerId { get; set; }
        public int? reOrderLevel { get; set; }
        [MaxLength(20)]
        public string itemShortName { get; set; }
        public string fileName { get; set; }
        public int? isMostUsed { get; set; }
        public int? isBuild { get; set; }
    }
}
