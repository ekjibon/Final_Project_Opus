using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("CSItemCategory", Schema = "SCM")]
    public class CSItemCategory:Base
    {
        [StringLength(50)]
        public string itemCatName { get; set; }

        [StringLength(50)]
        public string itemCatNameBN { get; set; }

        public int? shortOrder { get; set; }
    }
}
