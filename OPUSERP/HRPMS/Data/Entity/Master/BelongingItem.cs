using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    public class BelongingItem:Base
    {
        public int? itemId { get; set; }
        public Item item { get; set; }

        public string ItemName { get; set; }
        public string ItemCode { get; set; }


        public string brandName { get; set; }
        public string modelName { get; set; }
        public string serialNumber { get; set; }
        public DateTime? dateOfProcurement { get; set; }
        public DateTime? dateOfSubmission { get; set; }
        public DateTime? dateOfLastPhysicalVerification { get; set; }
    }
}
