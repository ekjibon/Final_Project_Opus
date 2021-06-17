using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class BelongingsItemViewModel
    {
        public int id { get; set; }
        public int? itemId { get; set; }
        public string itemName { get; set; }
        public string itemCode { get; set; }


        public string brandName { get; set; }
        public string modelName { get; set; }
        public string serialNumber { get; set; }
        public DateTime? dateOfProcurement { get; set; }
        public DateTime? dateOfSubmission { get; set; }
        public DateTime? dateOfLastPhysicalVerification { get; set; }



        public IEnumerable<BelongingItem> belongingItems { get; set; }
    }
}
