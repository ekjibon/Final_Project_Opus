using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.VehicleInfo
{
    [Table("MajorOverholding", Schema = "HR")]
    public class MajorOverholding : Base
    {
        public int? vehicleEntryId { get; set; }
        public VehicleEntry vehicleEntry { get; set; }

        public string type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public int cost { get; set; }
    }
}
