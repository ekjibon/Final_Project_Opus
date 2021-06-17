using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("StoreType", Schema = "POS")]
    public class StoreType : Base
    {
        [MaxLength(250)]
        public string storeTypeName { get; set; }

    }
}
