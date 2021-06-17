using OPUSERP.Data.Entity;
using OPUSERP.POS.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.MasterData
{
    [Table("StoreAssign", Schema = "Purchase")]
    public class StoreAssign : Base
    {
        public int? storeId { get; set; }
        public Store store { get; set; }

        public string aspnetuserId { get; set; }
        [ForeignKey("aspnetuserId")]
        public virtual ApplicationUser aspnetuser { get; set; }

        public string isDefault { get; set; }

    }
}
