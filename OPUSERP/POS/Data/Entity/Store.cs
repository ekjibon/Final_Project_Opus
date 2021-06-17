using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("Store", Schema = "POS")]
    public class Store : Base
    {
        [MaxLength(350)]
        public string storeName { get; set; }

        public int? storeTypeId { get; set; }
        public StoreType storeType { get; set; }

        public string licenseNumber { get; set; }

        public string email { get; set; }

        public string alternativeEmail { get; set; }

        public string mobile { get; set; }

        public string phone { get; set; }


        
    }
}
