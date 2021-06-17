using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.POS.Data.Entity;

namespace OPUSERP.Data.Entity.MasterData
{
    [Table("StoreContact", Schema = "Purchase")]
    public class StoreContact : Base
    {
        public int? storeId { get; set; }
        public Store store { get; set; }

        public string department { get; set; }

        public string designation { get; set; }

        public string personName { get; set; }

        public string phoneNumber { get; set; }

        public string mobileNumber { get; set; }
    }
}
