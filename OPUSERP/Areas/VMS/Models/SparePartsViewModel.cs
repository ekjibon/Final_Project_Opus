using System.Collections.Generic;

using OPUSERP.VMS.Data.Entity.CarManagement;

namespace OPUSERP.Areas.VMS.Models
{
    public class SparePartsViewModel
    {
        public int? ID { get; set; }

        public string brandName { get; set; }

        public string modelName { get; set; }

        public string partsName { get; set; }

        //public SparePartsLN flang { get; set; }

        public IEnumerable<SpareParts> spareParts { get; set; }
    }
}
