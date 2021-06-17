using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ItemTypeViewModel
    {
        public int itemTypeId { get; set; }
        public string itemTypeName { get; set; }
        public IEnumerable<ItemType> itemTypes { get; set; }
    }
}
